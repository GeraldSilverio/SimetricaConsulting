using System.IdentityModel.Tokens.Jwt;
using RealEstateApp.Core.Application.Dtos.Accounts;

namespace TaskManagement.Infraestructure.Identity.Interfaces;

public interface IJwtServices
{
    Task<JwtSecurityToken> GetSecurityToken(ApplicationUser user);

    string GetIdUser();
    RefreshToken GenerateRefreshToken();
}