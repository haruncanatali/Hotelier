using Hotelier.Application.Common.Interfaces;
using Hotelier.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Hotelier.Application.Staffs.Commands.DeleteStaff;

public class DeleteStaffCommand : IRequest<Unit>
{
    public long Id { get; set; }

    public class Handler : IRequestHandler<DeleteStaffCommand, Unit>
    {
        private readonly IApplicationContext _context;

        public Handler(IApplicationContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteStaffCommand request, CancellationToken cancellationToken)
        {
            Staff? entity = await _context.Staffs
                .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

            if (entity != null)
            {
                _context.Staffs.Remove(entity);
                await _context.SaveChangesAsync(cancellationToken);
            }
            
            return Unit.Value;
        }
    }
}