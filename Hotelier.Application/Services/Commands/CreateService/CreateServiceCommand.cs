using Hotelier.Application.Common.Interfaces;
using Hotelier.Application.Common.Models;
using Hotelier.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Hotelier.Application.Services.Commands.CreateService;

public class CreateServiceCommand : IRequest<Unit>
{
    public IFormFile Icon { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }

    public class Handler : IRequestHandler<CreateServiceCommand, Unit>
    {
        private readonly IApplicationContext _context;
        private readonly IFileService _fileService;

        public Handler(IApplicationContext context, IFileService fileService)
        {
            _context = context;
            _fileService = fileService;
        }

        public async Task<Unit> Handle(CreateServiceCommand request, CancellationToken cancellationToken)
        {
            Service entity = new Service
            {
                Title = request.Title,
                Description = request.Description,
                Icon = _fileService.UploadPhoto(request.Icon,PhotoPaths.ServiceIconPath)
            };

            await _context.Services.AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            
            return Unit.Value;
        }
    }
}