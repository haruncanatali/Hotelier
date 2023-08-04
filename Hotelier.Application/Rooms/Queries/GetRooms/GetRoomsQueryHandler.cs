using System.Globalization;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Hotelier.Application.Common.Interfaces;
using Hotelier.Application.Rooms.Queries.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Hotelier.Application.Rooms.Queries.GetRooms;

public class GetRoomsQueryHandler : IRequestHandler<GetRoomsQuery,GetRoomsVm>
{
    private readonly IApplicationContext _context;
    private readonly IMapper _mapper;

    public GetRoomsQueryHandler(IApplicationContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<GetRoomsVm> Handle(GetRoomsQuery request, CancellationToken cancellationToken)
    {
        int count = await _context.Rooms.CountAsync(cancellationToken);

        double pageCount = Math.Ceiling((double)count / (double)request.PageSize);
        
        List<RoomDto>? result = await _context.Rooms
            .Where(c =>
                (request.Title == null || c.Title.ToLower().Contains(request.Title.ToLower())) &&
                (request.RoomNumber == null || c.RoomNumber.ToLower().Contains(request.RoomNumber.ToLower())) &&
                (request.Price == null || c.Price == request.Price) &&
                (request.BedCount == null || c.BedCount == request.BedCount) &&
                (request.BathCount == null || c.BathCount == request.BathCount) &&
                (request.WiFi == null || c.WiFi == request.WiFi))
            .OrderByDescending(c=>c.CreatedAt)
            .ProjectTo<RoomDto>(_mapper.ConfigurationProvider)
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);

        return new GetRoomsVm
        {
            Rooms = result,
            CurrentPage = request.Page,
            Next = request.Page < pageCount,
            Previous = request.Page>1,
            PageCount = int.Parse(pageCount.ToString(CultureInfo.InvariantCulture))
        };
    }
}