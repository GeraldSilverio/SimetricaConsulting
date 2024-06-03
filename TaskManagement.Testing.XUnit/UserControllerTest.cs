using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Core.Application.Dtos.Accounts;
using TaskManagement.Core.Application.Interfaces;
using TaskManagement.Core.Application.Wrappers;
using TaskManagement.Presentation.WebApi.Controllers;

namespace TaskManagement.Testing.XUnit
{
    //public class UserControllerTest
    //{
    //    private readonly Mock<IAccountService> _accountServiceMock;
    //    private readonly UserController _controller;

    //    public UserControllerTests()
    //    {
    //        _accountServiceMock = new Mock<IAccountService>();
    //        _controller = new UserController(_accountServiceMock.Object, null);
    //    }

    //    [Fact]
    //    public async Task RegisterDeveloper_ShouldReturnBadRequest_WhenRegistrationFails()
    //    {
    //        // Arrange
    //        var registerRequest = new RegisterRequest();
    //        _accountServiceMock.Setup(service => service.RegisterAsync(registerRequest, null))
    //            .ReturnsAsync(new Response<string>("Error message", true));

    //        // Act
    //        var result = await _controller.RegisterDeveloper(registerRequest);

    //        // Assert
    //        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
    //        Assert.Equal(StatusCodes.Status400BadRequest, badRequestResult.StatusCode);
    //    }

    //    [Fact]
    //    public async Task RegisterDeveloper_ShouldReturnCreated_WhenRegistrationSucceeds()
    //    {
    //        // Arrange
    //        var registerRequest = new RegisterRequest();
    //        _accountServiceMock.Setup(service => service.RegisterAsync(registerRequest, null))
    //            .ReturnsAsync(new Response<string>(new, null));

    //        // Act
    //        var result = await _controller.RegisterDeveloper(registerRequest);

    //        // Assert
    //        var createdResult = Assert.IsType<ObjectResult>(result);
    //        Assert.Equal(StatusCodes.Status201Created, createdResult.StatusCode);
    //    }

    //    [Fact]
    //    public async Task Authentication_ShouldReturnBadRequest_WhenAuthenticationFails()
    //    {
    //        // Arrange
    //        var authenticationRequest = new AuthenticationRequest();
    //        _accountServiceMock.Setup(service => service.AuthenticationAsync(authenticationRequest))
    //            .ReturnsAsync(new Response<AuthenticationResponse>("Error message", true));

    //        // Act
    //        var result = await _controller.Authentication(authenticationRequest);

    //        // Assert
    //        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
    //        Assert.Equal(StatusCodes.Status400BadRequest, badRequestResult.StatusCode);
    //    }

    //    [Fact]
    //    public async Task Authentication_ShouldReturnOk_WhenAuthenticationSucceeds()
    //    {
    //        // Arrange
    //        var authenticationRequest = new AuthenticationRequest();
    //        _accountServiceMock.Setup(service => service.AuthenticationAsync(authenticationRequest))
    //            .ReturnsAsync(new Response<AuthenticationResponse>(new AuthenticationResponse(),));

    //        // Act
    //        var result = await _controller.Authentication(authenticationRequest);

    //        // Assert
    //        var okResult = Assert.IsType<OkObjectResult>(result);
    //        Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
    //    }
    //}
}
