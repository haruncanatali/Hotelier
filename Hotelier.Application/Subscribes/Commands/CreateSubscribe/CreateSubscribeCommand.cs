using Hotelier.Application.Common.Interfaces;
using Hotelier.Application.Users.Commands.CreateUser;
using Hotelier.Domain.Entities;
using MediatR;

namespace Hotelier.Application.Subscribes.Commands.CreateSubscribe;

public class CreateSubscribeCommand : IRequest<Unit>
{
    public string Mail { get; set; }
    
    public class Handler : IRequestHandler<CreateSubscribeCommand, Unit>
    {
        private readonly IApplicationContext _context;

        public Handler(IApplicationContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(CreateSubscribeCommand request, CancellationToken cancellationToken)
        {
            await _context.Subscribes.AddAsync(new Subscribe { Mail = request.Mail });
            await _context.SaveChangesAsync(cancellationToken);
            
            return Unit.Value;
        }
    }
}