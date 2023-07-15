using Hotelier.Application.Common.Interfaces;
using Hotelier.Application.Users.Commands.UpdateUser;
using Hotelier.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Hotelier.Application.Subscribes.Commands.UpdateSubscribe;

public class UpdateSubscribeCommand : IRequest<Unit>
{
    public long Id { get; set; }
    public string Mail { get; set; }
    
    public class Handler : IRequestHandler<UpdateSubscribeCommand, Unit>
    {
        private readonly IApplicationContext _context;

        public Handler(IApplicationContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateSubscribeCommand request, CancellationToken cancellationToken)
        {
            Subscribe? entity = await _context.Subscribes
                .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

            if (entity != null)
            {
                entity.Mail = request.Mail;

                _context.Subscribes.Update(entity);
                await _context.SaveChangesAsync(cancellationToken);
            }
            
            return Unit.Value;
        }
    }
}