using Hotelier.Application.Common.Interfaces;
using Hotelier.Application.Common.Models;
using Hotelier.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Hotelier.Application.Staffs.Commands.CreateStaff;

public class CreateStaffCommand : IRequest<Unit>
{
    public string Name { get; set; }
    public string Title { get; set; }
    public string SocialMediaFacebook { get; set; }
    public string SocialMediaInstagram { get; set; }
    public string SocialMediaTwitter { get; set; }
    public IFormFile Image { get; set; }

    public class Handler : IRequestHandler<CreateStaffCommand, Unit>
    {
        private readonly IApplicationContext _context;
        private readonly IFileService _fileService;

        public Handler(IApplicationContext context, IFileService fileService)
        {
            _context = context;
            _fileService = fileService;
        }

        public async Task<Unit> Handle(CreateStaffCommand request, CancellationToken cancellationToken)
        {
            Staff? entity = new Staff
            {
                Name = request.Name,
                Title = request.Title,
                SocialMediaFacebook = request.SocialMediaFacebook,
                SocialMediaInstagram = request.SocialMediaInstagram,
                SocialMediaTwitter = request.SocialMediaTwitter,
                Image = _fileService.UploadPhoto(request.Image,PhotoPaths.StaffImagePath)
            };

            await _context.Staffs.AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            
            return Unit.Value;
        }
    }
}