using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RealEstateApp.Core.Application.Dtos.Accounts;
using TaskManagement.Core.Domain.Settings;
using TaskManagement.Infraestructure.Identity.Interfaces;

namespace TaskManagement.Infraestructure.Identity.Services;

public class JwtServices : IJwtServices
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly JwtSettings _jwtSettings;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public JwtServices(IOptions<JwtSettings> jwtSettings, UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor)
    {
        _jwtSettings = jwtSettings.Value;
        _userManager = userManager;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<JwtSecurityToken> GetSecurityToken(ApplicationUser user)
    {
        var userClaims = await _userManager.GetClaimsAsync(user);
        var roles = await _userManager.GetRolesAsync(user);

        var roleClaims = new List<Claim>();

        foreach (var role in roles)
        {
            roleClaims.Add(new Claim("roles", role));
        }
        var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid", user.Id)
            }
            .Union(userClaims)
            .Union(roleClaims);

        var symmectricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
        var signingCredetials = new SigningCredentials(symmectricSecurityKey, SecurityAlgorithms.HmacSha256);

        var jwtSecurityToken = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
            signingCredentials: signingCredetials);

        return jwtSecurityToken;
    }

    public string GetIdUser()
    {
        try
        {
            string authorization = _httpContextAccessor.HttpContext.Request.Headers["Authorization"];
            var tokenRequest = authorization.Replace("Bearer", "").Trim();
            var jwtToken  = new JwtSecurityTokenHandler().ReadJwtToken(tokenRequest);
            var idUser =  jwtToken.Claims.FirstOrDefault(x=> x.Type == "uid");
            return idUser.Value;
        }
        catch (Exception ex)
        {
            throw new ApplicationException(ex.Message, ex);
        }
    }

    public RefreshToken GenerateRefreshToken()
    {
        return new RefreshToken
        {
            Token = RandomTokenString(),
            Expires = DateTime.UtcNow.AddDays(7),
            Created = DateTime.UtcNow
        };
    }

    private string RandomTokenString()
    {
        using var rngCryptoServiceProvider = new RNGCryptoServiceProvider();
        var ramdomBytes = new byte[40];
        rngCryptoServiceProvider.GetBytes(ramdomBytes);

        return BitConverter.ToString(ramdomBytes).Replace("-", "");
    }
}