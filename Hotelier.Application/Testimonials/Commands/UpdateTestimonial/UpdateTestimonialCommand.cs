using Hotelier.Application.Common.Interfaces;
using Hotelier.Application.Common.Models;
using Hotelier.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Hotelier.Application.Testimonials.Commands.UpdateTestimonial;

public class UpdateTestimonialCommand : IRequest<Unit>
{
    public long Id { get; set; }
    public string Name { get; set; }
    public IFormFile? Image { get; set; }
    public string Type { get; set; }
    public string Description { get; set; }

    public class Handler : IRequestHandler<UpdateTestimonialCommand, Unit>
    {
        private readonly IApplicationContext _context;
        private readonly IFileService _fileService;

        public Handler(IApplicationContext context, IFileService fileService)
        {
            _context = context;
            _fileService = fileService;
        }

        public async Task<Unit> Handle(UpdateTestimonialCommand request, CancellationToken cancellationToken)
        {
            Testimonial? entity = await _context.Testimonials
                .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

            if (entity != null)
            {
                entity.Name = request.Name;
                entity.Type = request.Type;
                entity.Description = request.Description;

                if (request.Image != null)
                {
                    entity.Image = _fileService.UploadPhoto(request.Image, PhotoPaths.TestimonialImagePath);
                }

                _context.Testimonials.Update(entity);
                await _context.SaveChangesAsync(cancellationToken);
            }
            
            return Unit.Value;
        }
    }
}