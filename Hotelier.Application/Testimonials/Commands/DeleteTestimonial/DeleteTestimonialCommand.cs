using Hotelier.Application.Common.Interfaces;
using Hotelier.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Hotelier.Application.Testimonials.Commands.DeleteTestimonial;

public class DeleteTestimonialCommand : IRequest<Unit>
{
    public long Id { get; set; }

    public class Handler : IRequestHandler<DeleteTestimonialCommand, Unit>
    {
        private readonly IApplicationContext _context;

        public Handler(IApplicationContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteTestimonialCommand request, CancellationToken cancellationToken)
        {
            Testimonial? entity = await _context.Testimonials
                .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

            if (entity != null)
            {
                _context.Testimonials.Remove(entity);
                await _context.SaveChangesAsync(cancellationToken);
            }
            
            return Unit.Value;
        }
    }
}