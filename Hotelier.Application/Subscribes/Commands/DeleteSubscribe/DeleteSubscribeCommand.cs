using Hotelier.Application.Common.Interfaces;
using Hotelier.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Hotelier.Application.Subscribes.Commands.DeleteSubscribe;

public class DeleteSubscribeCommand : IRequest<Unit>
{
    public long Id { get; set; }

    public class Handler : IRequestHandler<DeleteSubscribeCommand, Unit>
    {
        private readonly IApplicationContext _context;

        public Handler(IApplicationContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteSubscribeCommand request, CancellationToken cancellationToken)
        {
            Subscribe? entity = await _context.Subscribes
                .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

            if (entity != null)
            {
                _context.Subscribes.Remove(entity);
                await _context.SaveChangesAsync(cancellationToken);
            }
            
            return Unit.Value;
        }
    }
}