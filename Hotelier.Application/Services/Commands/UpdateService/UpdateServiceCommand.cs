using Hotelier.Application.Common.Interfaces;
using Hotelier.Application.Common.Models;
using Hotelier.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Hotelier.Application.Services.Commands.UpdateService;

public class UpdateServiceCommand : IRequest<Unit>
{
    public long Id { get; set; }
    public IFormFile? Icon { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }

    public class Handler : IRequestHandler<UpdateServiceCommand, Unit>
    {
        private readonly IApplicationContext _context;
        private readonly IFileService _fileService;

        public Handler(IApplicationContext context, IFileService fileService)
        {
            _context = context;
            _fileService = fileService;
        }

        public async Task<Unit> Handle(UpdateServiceCommand request, CancellationToken cancellationToken)
        {
            Service? entity = await _context.Services
                .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

            if (entity != null)
            {
                entity.Title = request.Title;
                entity.Description = request.Description;

                if (request.Icon != null)
                {
                    entity.Icon = _fileService.UploadPhoto(request.Icon, PhotoPaths.ServiceIconPath);
                }

                _context.Services.Update(entity);
                await _context.SaveChangesAsync(cancellationToken);
            }
            
            return Unit.Value;
        }
    }
}