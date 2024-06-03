using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using TaskManagement.Core.Application;
using TaskManagement.Core.Application.Dtos.Accounts;
using TaskManagement.Core.Application.Interfaces;
using TaskManagement.Core.Domain.Settings;
using TaskManagement.Infraestructure.Identity.Context;
using TaskManagement.Infraestructure.Identity.Interfaces;

namespace TaskManagement.Infraestructure.Identity.Services;

public class AccountService : IAccountService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IdentityContext _identityContext;
    private readonly IJwtServices _jwtServices;

    /// <summary>
    /// Metodo constructor
    /// </summary>
    /// <param name="userManager"></param>
    /// <param name="signInManager"></param>
    /// <param name="identityContext"></param>
    /// <param name="jwtServices"></param>
    public AccountService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,
        IdentityContext identityContext, IJwtServices jwtServices)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _identityContext = identityContext;
        _jwtServices = jwtServices;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <param name="origin"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<RegisterResponse> RegisterAsync(RegisterRequest request, string? origin)
    {
        try
        {
            var response = new RegisterResponse()
            {
                HasError = false,
                Error = new List<string>()
            };
            //Llamando el metodo validate, para asegurarnos que las validaciones que hicismos pasen.
            var validate = await ValidateUser(request, response);
            if (validate.HasError) return validate;

            var user = new ApplicationUser
            {
                UserName = request.UserName,
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                IsActive = true,
                EmailConfirmed = true
            };

            //Mandando a crear el usuario.
            var result = await _userManager.CreateAsync(user, request.Password);
            //Si el usuario se creo exitosamente, le asignamos el rol de User al registro. 
            if (result.Succeeded)
            {
                response.IdUser = user.Id;
                await _userManager.AddToRoleAsync(user, Roles.User.ToString());
                return response;
            }

            //En caso de que el usuario no se cree existosamente, iteramos en la lista de identityErros y agregamos la
            //la descripcion de cada error a la lista de errores del response.
            foreach (var error in result.Errors)
            {
                response.Error.Add(error.Description);
            }

            response.HasError = true;
            return response;
        }
        catch (Exception ex)
        {
            throw new ApplicationException($"Error al intentar registrar un nuevo usuario{ex.Message} {ex.StackTrace}",
                ex);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<AuthenticationResponse> AuthenticationAsync(AuthenticationRequest request)
    {
        try
        {
            var response = new AuthenticationResponse()
            {
                Error = new List<string>(),
                HasError = false
            };
            //Validando que el usuario exista.
            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user == null)
            {
                SetError(response, $"El usuario {request.UserName} no existe");
                return response;
            }

            //Validando si la contraseña que enviaron en el request, es la misma del usuario en Base de datos.
            var checkPassword =
                await _signInManager.PasswordSignInAsync(user.UserName, request.Password, false,
                    lockoutOnFailure: false);

            if (!checkPassword.Succeeded)
            {
                SetError(response, $"La contraseña para el usuario{request.UserName} es incorrecta");
                return response;
            }

            //Obteniendo los roles del usuario.
            var roles = await _userManager.GetRolesAsync(user).ConfigureAwait(false);

            response.Id = user.Id;
            response.Email = user.Email;
            response.UserName = user.UserName;
            response.FirstName = user.FirstName;
            response.LastName = user.LastName;
            response.IsActive = user.IsActive;
            response.Roles = roles.ToList();

            //Asignando el Json Web Token.
            var jwtSecutiryToken = await _jwtServices.GetSecurityToken(user);
            response.JwToken = new JwtSecurityTokenHandler().WriteToken(jwtSecutiryToken);
            response.RefreshToken = _jwtServices.GenerateRefreshToken().Token;

            return response;
        }
        catch (Exception ex)
        {
            throw new ApplicationException(ex.Message, ex);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <param name="response"></param>
    /// <returns></returns>
    /// <exception cref="ApplicationException"></exception>
    private async Task<RegisterResponse> ValidateUser(RegisterRequest request, RegisterResponse response)
    {
        try
        {
            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user != null)
            {
                SetError(response, $"El nombre de usuario{request.UserName} ya  esta en uso");
            }

            var userEmail = await _userManager.FindByEmailAsync(request.Email);
            if (userEmail != null)
            {
                SetError(response, $"El correo {request.Email} ya esta en uso");
            }

            return response;
        }
        catch (Exception e)
        {
            throw new ApplicationException(e.Message, e);
        }
    }

    private dynamic SetError(dynamic model, string error)
    {
        model.HasError = true;
        model.Error?.Add(error);
        return model;
    }

    public string GetIdUser()
    {
        try
        {
            return _jwtServices.GetIdUser();
        }
        catch (Exception ex)
        {
            throw new ApplicationException(ex.Message, ex);
        }
    }
}