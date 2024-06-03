using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TaskManagement.Core.Application.Dtos.Task;
using TaskManagement.Core.Application.Features.Task.Commands;
using TaskManagement.Core.Application.Features.Task.Queries;
using TaskManagement.Core.Application.Wrappers;
using TaskManagement.Presentation.WebApi.Controllers.V1;

namespace TaskManagement.Testing.XUnit;

public class TaskControllerTests
{
    private readonly Mock<IMediator> _mediatorMock;
    private readonly TaskController _controller;

    public TaskControllerTests()
    {
        _mediatorMock = new Mock<IMediator>();
        _controller = new TaskController(_mediatorMock.Object);
    }
    [Fact]
    public async Task Create_ShouldReturnCreatedStatus_WhenTaskIsCreated()
    {
        // Arrange
        var createTaskCommand = new CreateTaskCommand
        {
            Name = "Tast"
        };
        _mediatorMock.Setup(m => m.Send(It.IsAny<CreateTaskCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Response<int>(1));

        // Act
        var result = await _controller.Create(createTaskCommand);

        // Assert
        var createdResult = Assert.IsType<ObjectResult>(result);
        Assert.Equal(StatusCodes.Status201Created, createdResult.StatusCode);
    }
    [Fact]
    public async Task Create_ShouldReturnBadRequest_WhenTaskCreationFails()
    {
        // Arrange
        var createTaskCommand = new CreateTaskCommand
        {
            Name = "Tast"
        };
        _mediatorMock.Setup(m => m.Send(It.IsAny<CreateTaskCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Response<int>(0));

        // Act
        var result = await _controller.Create(createTaskCommand);

        // Assert
        var actionResult = Assert.IsType<ObjectResult>(result);
        Assert.Equal(StatusCodes.Status400BadRequest, actionResult.StatusCode);
    }

    [Fact]
    public async Task GetAll_ShouldReturnOkStatus_WithListOfTasks()
    {
        // Arrange
        var tasks = new List<TaskDto> { new TaskDto() };
        _mediatorMock.Setup(m => m.Send(It.IsAny<GetAllTasksQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Response<List<TaskDto>>(tasks));

        // Act
        var result = await _controller.GetAll();

        // Assert
        var actionResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(StatusCodes.Status200OK, (actionResult as OkObjectResult).StatusCode);
    }

    [Fact]
    public async Task GetAll_ShouldReturnInternalServerError_OnException()
    {
        // Arrange
        _mediatorMock.Setup(m => m.Send(It.IsAny<GetAllTasksQuery>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(new Exception("Test Exception"));

        // Act & Assert
        await Assert.ThrowsAsync<ApplicationException>(() => _controller.GetAll());
    }

    [Fact]
    public async Task GetById_ShouldReturnOkStatus_WithTask()
    {
        // Arrange
        var task = new TaskDto();
        _mediatorMock.Setup(m => m.Send(It.IsAny<GetTaskByIdQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Response<TaskDto>(task));

        // Act
        var result = await _controller.GetById(1);

        // Assert
        var actionResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(StatusCodes.Status200OK, (actionResult as OkObjectResult).StatusCode);
    }

    [Fact]
    public async Task GetById_ShouldReturnInternalServerError_OnException()
    {
        // Arrange
        _mediatorMock.Setup(m => m.Send(It.IsAny<GetTaskByIdQuery>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(new Exception("Test Exception"));

        // Act & Assert
        await Assert.ThrowsAsync<ApplicationException>(() => _controller.GetById(1));
    }

    [Fact]
    public async Task Delete_ShouldReturnNoContentStatus_WhenTaskIsDeleted()
    {
        // Arrange
        _mediatorMock.Setup(m => m.Send(It.IsAny<DeleteTaskCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Response<int>(1));

        // Act
        var result = await _controller.Delete(1);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task Delete_ShouldReturnInternalServerError_OnException()
    {
        // Arrange
        _mediatorMock.Setup(m => m.Send(It.IsAny<DeleteTaskCommand>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(new Exception("Test Exception"));

        // Act & Assert
        await Assert.ThrowsAsync<ApplicationException>(() => _controller.Delete(1));
    }

    [Fact]
    public async Task Update_ShouldReturnNoContentStatus_WhenTaskIsUpdated()
    {
        // Arrange
        var updateTaskCommand = new UpdateTaskCommand
        {
            Id = 1,
            Name = "Test",
            IdTaskStatus = 1
        };
        _mediatorMock.Setup(m => m.Send(It.IsAny<UpdateTaskCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Response<int>(1));

        // Act
        var result = await _controller.Update(1, updateTaskCommand);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task Update_ShouldReturnInternalServerError_OnException()
    {
        // Arrange
        var updateTaskCommand = new UpdateTaskCommand
        {
            Id = 1,
            Name = "Test",
            IdTaskStatus = 10000
        };
        _mediatorMock.Setup(m => m.Send(It.IsAny<UpdateTaskCommand>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(new Exception("Test Exception"));

        // Act & Assert
        await Assert.ThrowsAsync<ApplicationException>(() => _controller.Update(1, updateTaskCommand));
    }

    [Fact]
    public async Task Create_ShouldRequireAuthorization()
    {
        // Arrange
        var methodInfo = typeof(TaskController).GetMethod("Create");
        var authorizeAttribute = methodInfo.GetCustomAttributes(typeof(AuthorizeAttribute), false);

        // Assert
        Assert.NotNull(authorizeAttribute);
    }

    [Fact]
    public async Task GetAll_ShouldRequireAuthorization()
    {
        // Arrange
        var methodInfo = typeof(TaskController).GetMethod("GetAll");
        var authorizeAttribute = methodInfo.GetCustomAttributes(typeof(AuthorizeAttribute), false);

        // Assert
        Assert.NotNull(authorizeAttribute);
    }

    [Fact]
    public async Task GetById_ShouldRequireAuthorization()
    {
        // Arrange
        var methodInfo = typeof(TaskController).GetMethod("GetById");
        var authorizeAttribute = methodInfo.GetCustomAttributes(typeof(AuthorizeAttribute), false);

        // Assert
        Assert.NotNull(authorizeAttribute);
    }

    [Fact]
    public async Task Delete_ShouldRequireAuthorization()
    {
        // Arrange
        var methodInfo = typeof(TaskController).GetMethod("Delete");
        var authorizeAttribute = methodInfo.GetCustomAttributes(typeof(AuthorizeAttribute), false);

        // Assert
        Assert.NotNull(authorizeAttribute);
    }

    [Fact]
    public async Task Update_ShouldRequireAuthorization()
    {
        // Arrange
        var methodInfo = typeof(TaskController).GetMethod("Update");
        var authorizeAttribute = methodInfo.GetCustomAttributes(typeof(AuthorizeAttribute), false);

        // Assert
        Assert.NotNull(authorizeAttribute);
    }
}