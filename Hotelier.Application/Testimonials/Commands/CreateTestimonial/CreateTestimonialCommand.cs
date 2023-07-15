using Hotelier.Application.Common.Interfaces;
using Hotelier.Application.Common.Models;
using Hotelier.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Hotelier.Application.Testimonials.Commands.CreateTestimonial;

public class CreateTestimonialCommand : IRequest<Unit>
{
    public string Name { get; set; }
    public IFormFile Image { get; set; }
    public string Type { get; set; }
    public string Description { get; set; }

    public class Handler : IRequestHandler<CreateTestimonialCommand, Unit>
    {
        private readonly IApplicationContext _context;
        private readonly IFileService _fileService;
        
        public async Task<Unit> Handle(CreateTestimonialCommand request, CancellationToken cancellationToken)
        {
            Testimonial entity = new Testimonial
            {
                Name = request.Name,
                Type = request.Type,
                Description = request.Description,
                Image = _fileService.UploadPhoto(request.Image, PhotoPaths.TestimonialImagePath)
            };

            await _context.Testimonials.AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            
            return Unit.Value;
        }
    }
}