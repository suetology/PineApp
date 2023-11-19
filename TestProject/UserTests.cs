using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using PineAPP.Controllers;
using PineAPP.Models;
using PineAPP.Models.Dto;
using PineAPP.Services;
using PineAPP.Services.Factories;
using PineAPP.Services.Repositories;

namespace PineApp.TestProject;

public class UserTests
{
        
    [Fact]
    public async Task GetAllUsers_ReturnsOkResult_WithUsers()
    {
        var mockLogger = new Mock<ILogger<UsersController>>();
        var mockUsersRepository = new Mock<IUsersRepository>();
        var mockUserFactory = new Mock<IUserFactory>();
        var controller = new UsersController(mockLogger.Object, mockUsersRepository.Object, mockUserFactory.Object);
        
        var mockUser1 = new User
        {
            UserId = 1,
            Email = "test1@example.com",
            UserName = "TestUser1",
            Password = "TestPassword1235"
        };
        
        var mockUser2 = new User
        {
            UserId = 2,
            Email = "test2@example.com",
            UserName = "TestUser2",
            Password = "TestPassword1234"
        };
        
        var mockUsers = new List<User> { mockUser1, mockUser2 };
        mockUsersRepository.Setup(repo => repo.GetAll()).Returns(mockUsers);
            
        var result = controller.GetAllUsers();
            
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(mockUsers, okResult.Value);
    }
    
    [Fact]
    public void GetAllUsers_WhenExceptionOccurs_ShouldReturnInternalServerError()
    {
        // Arrange
        var mockRepo = new Mock<IUsersRepository>();
        var mockLogger = new Mock<ILogger<UsersController>>();
        var mockFactory = new Mock<IUserFactory>();
        var mockValidationService = new Mock<IUserValidationService>();
    
        mockRepo.Setup(repo => repo.GetAll()).Throws(new Exception());
        
        var controller = new UsersController(mockLogger.Object, mockRepo.Object, mockFactory.Object);
    
        // Act
        var result = controller.GetAllUsers();

        // Assert
        var statusCodeResult = Assert.IsType<ObjectResult>(result);
        Assert.Equal((int)HttpStatusCode.InternalServerError, statusCodeResult.StatusCode);
    }
    
    [Fact]
    public void GetUserById_WhenCalled_ReturnsOkResult_WithUser()
    {
        // Arrange
        var mockLogger = new Mock<ILogger<UsersController>>();
        var mockUsersRepository = new Mock<IUsersRepository>();
        var mockUserFactory = new Mock<IUserFactory>();
        var controller = new UsersController(mockLogger.Object, mockUsersRepository.Object, mockUserFactory.Object);
    
        var userId = 1; // Example user ID
        var mockUser = new User
        {
            UserId = 1,
            Email = "test@example.com",
            UserName = "TestUser",
            Password = "TestPassword123"
        };
        mockUsersRepository.Setup(repo => repo.GetById(userId)).Returns(mockUser);
    
        // Act
        var result = controller.GetUserById(userId);
    
        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(mockUser, okResult.Value);
    }
    [Fact]
    public void GetUserById_WhenExceptionOccurs_ShouldReturnInternalServerError()
    {
        // Arrange
        var mockLogger = new Mock<ILogger<UsersController>>();
        var mockUsersRepository = new Mock<IUsersRepository>();
        var mockUserFactory = new Mock<IUserFactory>();
        var controller = new UsersController(mockLogger.Object, mockUsersRepository.Object, mockUserFactory.Object);

        var userId = 1; // Example user ID
        var exceptionToThrow = new Exception("Test exception");
        mockUsersRepository.Setup(repo => repo.GetById(userId)).Throws(exceptionToThrow);

        // Act
        var result = controller.GetUserById(userId);

        // Assert
        var statusCodeResult = Assert.IsType<ObjectResult>(result);
        Assert.Equal((int)HttpStatusCode.InternalServerError, statusCodeResult.StatusCode);
        Assert.Equal(exceptionToThrow.Message, statusCodeResult.Value);
        
    }
    [Fact]
    public void GetUserByEmail_WhenCalled_ReturnsOkResult_WithUser()
    {
        // Arrange
        var mockLogger = new Mock<ILogger<UsersController>>();
        var mockUsersRepository = new Mock<IUsersRepository>();
        var mockUserFactory = new Mock<IUserFactory>();
        var controller = new UsersController(mockLogger.Object, mockUsersRepository.Object, mockUserFactory.Object);
    
        var email = "test@example.com"; // Example email
        var mockUser = new User
        {
            UserId = 1,
            Email = "test@example.com",
            UserName = "TestUser",
            Password = "TestPassword123"
        };
        mockUsersRepository.Setup(repo => repo.GetByEmail(email)).Returns(mockUser);
    
        // Act
        var result = controller.GetUserByEmail(email);
    
        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(mockUser, okResult.Value);
    }
    [Fact]
    public void GetUserByEmail_WhenExceptionOccurs_ShouldReturnInternalServerError()
    {
        // Arrange
        var mockLogger = new Mock<ILogger<UsersController>>();
        var mockUsersRepository = new Mock<IUsersRepository>();
        var mockUserFactory = new Mock<IUserFactory>();
        var controller = new UsersController(mockLogger.Object, mockUsersRepository.Object, mockUserFactory.Object);

        var email = "test@example.com"; 
        var exceptionToThrow = new Exception("Test exception");
        mockUsersRepository.Setup(repo => repo.GetByEmail(email)).Throws(exceptionToThrow);

        // Act
        var result = controller.GetUserByEmail(email);

        // Assert
        var statusCodeResult = Assert.IsType<ObjectResult>(result);
        Assert.Equal((int)HttpStatusCode.InternalServerError, statusCodeResult.StatusCode);
        Assert.Equal(exceptionToThrow.Message, statusCodeResult.Value);
        
    }
    [Fact]
    public async Task AddUser_WhenCalled_ReturnsOkResult_WithNewUser()
    {
        // Arrange
        var mockLogger = new Mock<ILogger<UsersController>>();
        var mockUsersRepository = new Mock<IUsersRepository>();
        var mockUserFactory = new Mock<IUserFactory>();
        var controller = new UsersController(mockLogger.Object, mockUsersRepository.Object, mockUserFactory.Object);
        controller.ModelState.Clear(); // Ensure the ModelState is cleared

        var createUserDto = new CreateUserDTO
        {
            Email = "test@example.com",
            UserName = "TestUser",
            Password = "TestPassword123"
        };
        var mockUser = new User
        {
            UserId = 1,
            Email = "test@example.com",
            UserName = "TestUser",
            Password = "TestPassword123"
        };
        mockUserFactory.Setup(factory => factory.CreateUser(createUserDto.Email, createUserDto.Password, createUserDto.UserName))
            .Returns(mockUser);

        // Act
        var result = await controller.AddUser(createUserDto);
    
        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(mockUser, okResult.Value);
    }
    
    [Fact]
    public async Task AddUser_WhenExceptionOccurs_ShouldReturnInternalServerError()
    {
        // Arrange
        var mockLogger = new Mock<ILogger<UsersController>>();
        var mockUsersRepository = new Mock<IUsersRepository>();
        var mockUserFactory = new Mock<IUserFactory>();
        var controller = new UsersController(mockLogger.Object, mockUsersRepository.Object, mockUserFactory.Object);
        controller.ModelState.Clear(); 

        var createUserDto = new CreateUserDTO
        {
            Email = "newuser@example.com",
            UserName = "NewUser",
            Password = "NewPassword123"
        };
        var exceptionToThrow = new Exception("Test exception");
        mockUserFactory.Setup(factory => factory.CreateUser(createUserDto.Email, createUserDto.Password, createUserDto.UserName))
            .Throws(exceptionToThrow);

        // Act
        var result = await controller.AddUser(createUserDto);

        // Assert
        var statusCodeResult = Assert.IsType<ObjectResult>(result);
        Assert.Equal((int)HttpStatusCode.InternalServerError, statusCodeResult.StatusCode);
        Assert.Equal(exceptionToThrow.Message, statusCodeResult.Value);
        
    }

    [Fact]
    public async Task AddUser_UserIsNull_ReturnsBadRequest()
    {
        // Arrange
        var mockLogger = new Mock<ILogger<UsersController>>();
        var mockUsersRepository = new Mock<IUsersRepository>();
        var mockUserFactory = new Mock<IUserFactory>();
        var controller = new UsersController(mockLogger.Object, mockUsersRepository.Object, mockUserFactory.Object);
        controller.ModelState.Clear(); 

        CreateUserDTO createUserDto = null; // Set createUserDto to null

        // Act
        var result = await controller.AddUser(createUserDto);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("CreateUserDto is null.", badRequestResult.Value);
    }
}