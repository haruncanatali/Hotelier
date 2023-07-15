using Hotelier.Application.Common.Interfaces;
using Hotelier.Application.Common.Models;
using Hotelier.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Hotelier.Application.Staffs.Commands.UpdateStaff;

public class UpdateStaffCommand : IRequest<Unit>
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Title { get; set; }
    public string SocialMediaFacebook { get; set; }
    public string SocialMediaInstagram { get; set; }
    public string SocialMediaTwitter { get; set; }
    public IFormFile? Image { get; set; }

    public class Handler : IRequestHandler<UpdateStaffCommand, Unit>
    {
        private readonly IApplicationContext _context;
        private readonly IFileService _fileService;

        public Handler(IApplicationContext context, IFileService fileService)
        {
            _context = context;
            _fileService = fileService;
        }

        public async Task<Unit> Handle(UpdateStaffCommand request, CancellationToken cancellationToken)
        {
            Staff? entity = await _context.Staffs
                .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

            if (entity != null)
            {
                entity.Name = request.Name;
                entity.Title = request.Title;
                entity.SocialMediaFacebook = request.SocialMediaFacebook;
                entity.SocialMediaInstagram = request.SocialMediaInstagram;
                entity.SocialMediaTwitter = request.SocialMediaTwitter;

                if (request.Image != null)
                {
                    entity.Image = _fileService.UploadPhoto(request.Image, PhotoPaths.StaffImagePath);
                }

                _context.Staffs.Update(entity);
                await _context.SaveChangesAsync(cancellationToken);
            }
            
            return Unit.Value;
        }
    }
}