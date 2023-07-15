using Hotelier.Application.Common.Interfaces;
using Hotelier.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Hotelier.Application.Services.Commands.DeleteService;

public class DeleteServiceCommand : IRequest<Unit>
{
    public long Id { get; set; }

    public class Handler : IRequestHandler<DeleteServiceCommand, Unit>
    {
        private readonly IApplicationContext _context;

        public Handler(IApplicationContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteServiceCommand request, CancellationToken cancellationToken)
        {
            Service? entity = await _context.Services
                .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

            if (entity != null)
            {
                _context.Services.Remove(entity);
                await _context.SaveChangesAsync(cancellationToken);
            }
            
            return Unit.Value;
        }
    }
}