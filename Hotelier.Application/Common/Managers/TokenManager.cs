using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Hotelier.Application.Auth.Queries.Dtos;
using Hotelier.Application.Common.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using User = Hotelier.Domain.IdentityEntities.User;

namespace Hotelier.Application.Common.Managers;

public class TokenManager
{
    private readonly TokenSetting _tokenSetting;
    private readonly UserManager<User> _userManager;

    public TokenManager(IOptions<TokenSetting> tokenSetting, UserManager<User> userManager)
    {
        _tokenSetting = tokenSetting.Value;
        _userManager = userManager;
    }

    public async Task<LoginDto> GenerateToken(User appUser)
    {
        List<Claim> claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, appUser.Id.ToString()),
            new Claim(ClaimTypes.Name, appUser.UserName),
        };
        
        string responseRole = (await _userManager.GetRolesAsync(appUser)).FirstOrDefault();
        if (!string.IsNullOrEmpty(responseRole))
        {
            claims.Add(new Claim(ClaimTypes.Role, responseRole));
        }

        SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenSetting.Key));
        SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        DateTime tokenExpire = DateTime.Now.AddMinutes(_tokenSetting.TokenValidityTime);

        JwtSecurityToken token = new JwtSecurityToken(
            _tokenSetting.Issuer,
            _tokenSetting.Audience,
            claims,
            expires: tokenExpire,
            signingCredentials: credentials
        );

        var refreshToken = CreateRefreshToken();

        appUser.RefreshToken = refreshToken;

        LoginDto summary = new LoginDto()
        {
            User = new Auth.Queries.Dtos.User
            {
                Email = appUser.Email,
                Id = appUser.Id,
                Role = responseRole
            },
            AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
            RefreshToken = refreshToken
        };

        await _userManager.UpdateAsync(appUser);
        return summary;
    }

    private string CreateRefreshToken()
    {
        byte[] bytes = new byte[32];
        using (RandomNumberGenerator random = RandomNumberGenerator.Create())
        {
            random.GetBytes(bytes);
            return Guid.NewGuid().ToString("N") + string.Concat(bytes.Select(x => x.ToString("x2")));
        }
    }
}