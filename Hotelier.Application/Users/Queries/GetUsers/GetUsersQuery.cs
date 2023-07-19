using Hotelier.Application.Users.Queries.Dtos;
using MediatR;

namespace Hotelier.Application.Users.Queries.GetUsers;

public class GetUsersQuery : IRequest<GetUsersVm>
{
    public int Page { get; set; }
    public int PageSize { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public DateTime? Birthdate { get; set; }
    public string? Email { get; set; }
}