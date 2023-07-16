using AutoMapper;
using Hotelier.Application.Common.Interfaces;
using Hotelier.Application.Common.Models;
using Hotelier.Application.Users.Queries.Dtos;
using Hotelier.Domain.IdentityEntities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Hotelier.Application.Users.Commands.CreateUser;

public class CreateUserCommand : IRequest<UserDto>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Address { get; set; }
    public DateTime Birthdate { get; set; }
    public IFormFile ProfilePhoto { get; set; }
    public string RoleName { get; set; }

    public class Handler : IRequestHandler<CreateUserCommand, UserDto>
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly IApplicationContext _context;
        private readonly IFileService _fileService;
        private readonly IMapper _mapper;

        public Handler(UserManager<User> userManager, RoleManager<Role> roleManager, IApplicationContext context, 
            IFileService fileService, IMapper mapper)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
            _fileService = fileService;
            _mapper = mapper;
        }

        public async Task<UserDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            UserDto result = null;
            
            bool dublicateControl = _context.Users.Any(x => x.Email == request.Email);
            
            if (dublicateControl)
            {
                return result;
            }

            string profilePhoto = String.Empty;
            if (request.ProfilePhoto != null)
            {
                profilePhoto = _fileService.UploadPhoto(request.ProfilePhoto,PhotoPaths.UserProfilePhotoPath);
            }

            User entity = new()
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                UserName = request.Email,
                Email = request.Email,
                Address = request.Address,
                ProfilePhoto = profilePhoto,
                Birthdate = request.Birthdate,
                RefreshToken = String.Empty,
                RefreshTokenExpiredTime = DateTime.Now,
            };

            var response = await _userManager.CreateAsync(entity, request.Password);

            if (response.Succeeded)
            {
                Role? role = await _roleManager.FindByNameAsync(request.RoleName);
            
                if (role != null)
                {
                    await _userManager.AddToRoleAsync(entity, request.RoleName);
                }
                else
                {
                    await _userManager.AddToRoleAsync(entity, "Normal");
                }
            }

            result = _mapper.Map<UserDto>(entity);
            
            return result;
        }
    }
}