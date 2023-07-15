using Hotelier.Application.Common.Interfaces;
using Hotelier.Application.Common.Models;
using Hotelier.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Hotelier.Application.Rooms.Commands.CreateRoom;

public class CreateRoomCommand : IRequest<Unit>
{
    public IFormFile CoverImage { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string RoomNumber { get; set; }
    public decimal Price { get; set; }
    public int BedCount { get; set; }
    public int BathCount { get; set; }
    public bool WiFi { get; set; }

    public class Handler : IRequestHandler<CreateRoomCommand, Unit>
    {
        private readonly IApplicationContext _context;
        private readonly IFileService _fileService;

        public Handler(IApplicationContext context, IFileService fileService)
        {
            _context = context;
            _fileService = fileService;
        }

        public async Task<Unit> Handle(CreateRoomCommand request, CancellationToken cancellationToken)
        {
            Room entity = new Room
            {
                Title = request.Title,
                Description = request.Description,
                RoomNumber = request.RoomNumber,
                Price = request.Price,
                BedCount = request.BedCount,
                BathCount = request.BathCount,
                WiFi = request.WiFi,
                CoverImage = _fileService.UploadPhoto(request.CoverImage,PhotoPaths.RoomCoverImagePath)
            };

            await _context.Rooms.AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            
            return Unit.Value;
        }
    }
}