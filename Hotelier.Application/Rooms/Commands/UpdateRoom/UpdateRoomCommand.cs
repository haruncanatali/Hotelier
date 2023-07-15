using Hotelier.Application.Common.Interfaces;
using Hotelier.Application.Common.Models;
using Hotelier.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Hotelier.Application.Rooms.Commands.UpdateRoom;

public class UpdateRoomCommand : IRequest<Unit>
{
    public long Id { get; set; }
    public IFormFile? CoverImage { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string RoomNumber { get; set; }
    public decimal Price { get; set; }
    public int BedCount { get; set; }
    public int BathCount { get; set; }
    public bool WiFi { get; set; }

    public class Handler : IRequestHandler<UpdateRoomCommand, Unit>
    {
        private readonly IApplicationContext _context;
        private readonly IFileService _fileService;

        public Handler(IApplicationContext context, IFileService fileService)
        {
            _context = context;
            _fileService = fileService;
        }

        public async Task<Unit> Handle(UpdateRoomCommand request, CancellationToken cancellationToken)
        {
            Room? entity = await _context.Rooms
                .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

            if (entity != null)
            {
                entity.Title = request.Title;
                entity.Description = request.Description;
                entity.RoomNumber = request.RoomNumber;
                entity.Price = request.Price;
                entity.BedCount = request.BedCount;
                entity.BathCount = request.BathCount;
                entity.WiFi = request.WiFi;

                if (request.CoverImage != null)
                {
                    entity.CoverImage = _fileService.UploadPhoto(request.CoverImage, PhotoPaths.RoomCoverImagePath);
                }

                _context.Rooms.Update(entity);
                await _context.SaveChangesAsync(cancellationToken);
            }
            
            return Unit.Value;
        }
    }
}