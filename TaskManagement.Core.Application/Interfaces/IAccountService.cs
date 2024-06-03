using RealEstateApp.Core.Application.Dtos.Accounts;
using TaskManagement.Core.Application.Dtos.Accounts;

namespace TaskManagement.Core.Application.Interfaces;

public interface IAccountService
{
    Task<RegisterResponse> RegisterAsync(RegisterRequest request, string? origin);
    Task<AuthenticationResponse> AuthenticationAsync(AuthenticationRequest request);

    string GetIdUser();
}