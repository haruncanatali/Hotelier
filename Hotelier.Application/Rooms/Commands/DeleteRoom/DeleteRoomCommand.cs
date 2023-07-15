using Hotelier.Application.Common.Interfaces;
using Hotelier.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Hotelier.Application.Rooms.Commands.DeleteRoom;

public class DeleteRoomCommand : IRequest<Unit>
{
    public long Id { get; set; }

    public class Handler : IRequestHandler<DeleteRoomCommand, Unit>
    {
        private readonly IApplicationContext _context;

        public Handler(IApplicationContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteRoomCommand request, CancellationToken cancellationToken)
        {
            Room? entity = await _context.Rooms
                .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

            if (entity != null)
            {
                _context.Rooms.Remove(entity);
                await _context.SaveChangesAsync(cancellationToken);
            }
            
            return Unit.Value;
        }
    }
}