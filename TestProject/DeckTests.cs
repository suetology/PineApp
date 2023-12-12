using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
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

public class DeckTests
{
    [Fact]
    public void GetAllDecks_ShouldReturnOkObjectResult_WithListOfDecks()
    {
        // Arrange
        var mockRepo = new Mock<IDecksRepository>();
        var mockLogger = new Mock<ILogger<DecksController>>();
        var mockFactory = new Mock<IDeckFactory>();
        var mockValidationService = new Mock<IDeckValidationService>();
        var mockNotificationsClient = new Mock<INotificationClient>();
        var decks = new List<Deck> { new Deck(), new Deck() };
    
        mockRepo.Setup(repo => repo.GetAllWithCards()).Returns(decks);
    
        var controller = new DecksController(mockLogger.Object, mockRepo.Object, mockFactory.Object, mockValidationService.Object, mockNotificationsClient.Object);
    
        // Act
        var result = controller.GetAllDecks();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedDecks = Assert.IsType<List<Deck>>(okResult.Value);
        Assert.Equal(2, returnedDecks.Count);
    }
    
    [Fact]
    public void GetAllDecks_WhenExceptionOccurs_ShouldReturnInternalServerError()
    {
        // Arrange
        var mockRepo = new Mock<IDecksRepository>();
        var mockLogger = new Mock<ILogger<DecksController>>();
        var mockValidationService = new Mock<IDeckValidationService>();
        var mockNotificationsClient = new Mock<INotificationClient>();
    
        mockRepo.Setup(repo => repo.GetAllWithCards()).Throws(new Exception());
    
        var mockFactory = new Mock<IDeckFactory>();
        var controller = new DecksController(mockLogger.Object, mockRepo.Object, mockFactory.Object, mockValidationService.Object, mockNotificationsClient.Object);
    
        // Act
        var result = controller.GetAllDecks();

        // Assert
        var statusCodeResult = Assert.IsType<ObjectResult>(result);
        Assert.Equal((int)HttpStatusCode.InternalServerError, statusCodeResult.StatusCode);
    }
    
    [Fact]
    public async Task AddDeck_WhenModelStateIsInvalid_ShouldReturnBadRequest()
    {
        // Arrange
        var mockRepo = new Mock<IDecksRepository>();
        var mockLogger = new Mock<ILogger<DecksController>>();
        var mockValidationService = new Mock<IDeckValidationService>();
        var mockNotificationsClient = new Mock<INotificationClient>();
    
        var mockFactory = new Mock<IDeckFactory>();
        var controller = new DecksController(mockLogger.Object, mockRepo.Object, mockFactory.Object, mockValidationService.Object, mockNotificationsClient.Object);
        controller.ModelState.AddModelError("Name", "Required");
    
        // Act
        var result = await controller.AddDeck(null);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal(400, badRequestResult.StatusCode); 
    }
    
    [Fact]
    public async Task AddDeck_WhenModelStateIsValid_ShouldReturnCreatedAtRouteResult()
    {
        // Arrange
        var mockRepo = new Mock<IDecksRepository>();
        var mockLogger = new Mock<ILogger<DecksController>>();
        var mockValidationService = new Mock<IDeckValidationService>();

        var newDeckDto = new CreateDeckDTO
        {
            Name = "Test Deck",
            Description = "Test Description",
            IsPersonal = true,
            CreatorId = 1
        };

        var mockDeck = new Deck
        {
            Name = "Test Deck",
            Description = "Test Description",
            IsPersonal = true,
            CreatorId = 1
        };
        
        //mock repo simulating deck added successfully
        mockRepo.Setup(repo => repo.Add(It.IsAny<Deck>())).Verifiable();

        var mockFactory = new Mock<IDeckFactory>();
        var mockNotificationsClient = new Mock<INotificationClient>();
        mockFactory.Setup(f => f.CreateDeck("Test Deck", "Test Description", true, 1, default)).Returns(mockDeck);
        var controller = new DecksController(mockLogger.Object, mockRepo.Object, mockFactory.Object, mockValidationService.Object, mockNotificationsClient.Object);

        // Act
        var result = await controller.AddDeck(newDeckDto);

        // Assert
        var objectResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(200, objectResult.StatusCode); // HTTP 201 Created
        
        //verifying if the method was called, and only once
        mockRepo.Verify(repo => repo.Add(It.IsAny<Deck>()), Times.Once);
        
        var deck = Assert.IsType<Deck>(objectResult.Value);
        Assert.Equal(newDeckDto.Name, deck.Name); 
    }

    
    [Fact]
    public async Task DeleteDeckById_WhenDeckExists_ShouldReturnOkWithDeletedDeck()
    {
        // Arrange
        var mockRepo = new Mock<IDecksRepository>();
        var mockLogger = new Mock<ILogger<DecksController>>();
        var mockValidationService = new Mock<IDeckValidationService>();
        var mockNotificationsClient = new Mock<INotificationClient>();
        var deck = new Deck { Id = 1 };
    
        mockRepo.Setup(repo => repo.GetDeckByIdWithCards(1)).Returns(deck);
    
        var mockFactory = new Mock<IDeckFactory>();
        var controller = new DecksController(mockLogger.Object, mockRepo.Object, mockFactory.Object, mockValidationService.Object, mockNotificationsClient.Object);
    
        // Act
        var result = await controller.DeleteDeckById(1);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedDeck = Assert.IsType<Deck>(okResult.Value);
        Assert.Equal(deck.Id, returnedDeck.Id);
    }
    
    [Fact]
    public async Task DeleteDeckById_WhenDeckDoesNotExist_ShouldReturnNotFound()
    {
        // Arrange
        var mockRepo = new Mock<IDecksRepository>();
        var mockLogger = new Mock<ILogger<DecksController>>();
        var mockValidationService = new Mock<IDeckValidationService>();
        var mockNotificationsClient = new Mock<INotificationClient>();
    
        mockRepo.Setup(repo => repo.GetDeckByIdWithCards(It.IsAny<int>())).Returns((Deck)null);
    
        var mockFactory = new Mock<IDeckFactory>();
        var controller = new DecksController(mockLogger.Object, mockRepo.Object, mockFactory.Object, mockValidationService.Object, mockNotificationsClient.Object);
    
        // Act
        var result = await controller.DeleteDeckById(1);

        // Assert
        Assert.IsType<NotFoundObjectResult>(result);
    }
    
    [Fact]
    public void GetDeckById_WhenDeckExists_ShouldReturnOkWithDeck()
    {
        // Arrange
        var mockRepo = new Mock<IDecksRepository>();
        var mockLogger = new Mock<ILogger<DecksController>>();
        var mockValidationService = new Mock<IDeckValidationService>();
        var mockNotificationsClient = new Mock<INotificationClient>();
        var deck = new Deck { Id = 1 };

        mockRepo.Setup(repo => repo.GetDeckByIdWithCards(1)).Returns(deck);

        var mockFactory = new Mock<IDeckFactory>();
        var controller = new DecksController(mockLogger.Object, mockRepo.Object, mockFactory.Object, mockValidationService.Object, mockNotificationsClient.Object);

        // Act
        var result = controller.GetDeckById(1);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedDeck = Assert.IsType<Deck>(okResult.Value);
        Assert.Equal(deck.Id, returnedDeck.Id);
    }
    
    [Fact]
    public void GetDeckById_WhenDeckDoesNotExist_ShouldReturnNotFound()
    {
        // Arrange
        var mockRepo = new Mock<IDecksRepository>();
        var mockLogger = new Mock<ILogger<DecksController>>();
        var mockValidationService = new Mock<IDeckValidationService>();
        var mockNotificationsClient = new Mock<INotificationClient>();

        // Setup the repository to return null when GetDeckByIdWithCards is called with any integer.
        mockRepo.Setup(repo => repo.GetDeckByIdWithCards(It.IsAny<int>())).Returns((Deck)null);

        var mockFactory = new Mock<IDeckFactory>();
        var controller = new DecksController(mockLogger.Object, mockRepo.Object, mockFactory.Object, mockValidationService.Object, mockNotificationsClient.Object);

        // Act
        var result = controller.GetDeckById(1);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }

    
    [Fact]
    public void GetAllDecksById_WhenDecksExist_ShouldReturnOkWithDecks()
    {
        // Arrange
        var mockRepo = new Mock<IDecksRepository>();
        var mockLogger = new Mock<ILogger<DecksController>>();
        var mockValidationService = new Mock<IDeckValidationService>();
        var mockNotificationsClient = new Mock<INotificationClient>();
        var decks = new List<Deck> { new Deck(), new Deck() };

        mockRepo.Setup(repo => repo.GetDecksByCreatorId(It.IsAny<int>())).Returns(decks);

        var mockFactory = new Mock<IDeckFactory>();
        var controller = new DecksController(mockLogger.Object, mockRepo.Object, mockFactory.Object, mockValidationService.Object, mockNotificationsClient.Object);

        // Act
        var result = controller.GetAllDecksById(1);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedDecks = Assert.IsType<List<Deck>>(okResult.Value);
        Assert.Equal(2, returnedDecks.Count);
    }
    
    [Fact]
    public void GetPersonalDecks_WithValidCreatorId_ReturnsOkWithDecks()
    {
        // Arrange
        var mockRepo = new Mock<IDecksRepository>();
        var mockLogger = new Mock<ILogger<DecksController>>();
        var mockValidationService = new Mock<IDeckValidationService>();
        var mockNotificationsClient = new Mock<INotificationClient>();
        var personalDecks = new List<Deck> { new Deck(), new Deck() };

        mockRepo.Setup(repo => repo.GetPersonalDecks(It.IsAny<int>())).Returns(personalDecks);
    
        var mockFactory = new Mock<IDeckFactory>();
        var controller = new DecksController(mockLogger.Object, mockRepo.Object, mockFactory.Object, mockValidationService.Object, mockNotificationsClient.Object);

        // Act
        var result = controller.GetPersonalDecks(1);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedDecks = Assert.IsType<List<Deck>>(okResult.Value);
        Assert.Equal(2, returnedDecks.Count); // Assuming there are 2 personal decks returned
    }

    [Fact]
    public void GetCommunityDecks_ReturnsOkWithDecks()
    {
        // Arrange
        var mockRepo = new Mock<IDecksRepository>();
        var mockLogger = new Mock<ILogger<DecksController>>();
        var mockValidationService = new Mock<IDeckValidationService>();
        var mockNotificationsClient = new Mock<INotificationClient>();
        var communityDecks = new List<Deck> { new Deck(), new Deck() };

        mockRepo.Setup(repo => repo.GetCommunityDecks()).Returns(communityDecks);
    
        var mockFactory = new Mock<IDeckFactory>();
        var controller = new DecksController(mockLogger.Object, mockRepo.Object, mockFactory.Object, mockValidationService.Object, mockNotificationsClient.Object);

        // Act
        var result = controller.GetCommunityDecks();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedDecks = Assert.IsType<List<Deck>>(okResult.Value);
        Assert.Equal(2, returnedDecks.Count); // Assuming there are 2 community decks returned
    }
    
    [Fact]
    public async Task UpdateDeckById_WithValidData_ReturnsOkWithUpdatedDeck()
    {
        // Arrange
        var mockRepo = new Mock<IDecksRepository>();
        var mockLogger = new Mock<ILogger<DecksController>>();
        var mockValidationService = new Mock<IDeckValidationService>();
        var mockNotificationsClient = new Mock<INotificationClient>();
        var existingDeck = new Deck { Id = 1, Name = "Original Name" };
        var updateDeckDto = new CreateDeckDTO { Name = "Updated Name", Description = "Updated Description", IsPersonal = true, CreatorId = 1 };

        mockRepo.Setup(repo => repo.GetDeckByIdWithCards(It.IsAny<int>())).Returns(existingDeck);
        mockRepo.Setup(repo => repo.Update(It.IsAny<Deck>())).Verifiable();
        mockRepo.Setup(repo => repo.SaveChangesAsync()).Returns(Task.FromResult(1)).Verifiable(); // Assumes one change was made

    
        var mockFactory = new Mock<IDeckFactory>();
        var controller = new DecksController(mockLogger.Object, mockRepo.Object, mockFactory.Object, mockValidationService.Object, mockNotificationsClient.Object);

        // Act
        var result = await controller.UpdateDeckById(1, updateDeckDto);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var updatedDeck = Assert.IsType<Deck>(okResult.Value);
        Assert.Equal(updateDeckDto.Name, updatedDeck.Name); // Verifying that the name was updated
        mockRepo.Verify(repo => repo.Update(It.IsAny<Deck>()), Times.Once);
        mockRepo.Verify(repo => repo.SaveChangesAsync(), Times.Once);
    }

    [Fact]
    public async Task UpdateDeckById_WithInvalidIdOrDto_ReturnsBadRequest()
    {
        // Arrange
        var mockRepo = new Mock<IDecksRepository>();
        var mockLogger = new Mock<ILogger<DecksController>>();
        var mockValidationService = new Mock<IDeckValidationService>();
        var mockNotificationsClient = new Mock<INotificationClient>();
        var mockFactory = new Mock<IDeckFactory>();
        var controller = new DecksController(mockLogger.Object, mockRepo.Object, mockFactory.Object, mockValidationService.Object, mockNotificationsClient.Object);
        
        // Act
        var nullDtoResult = await controller.UpdateDeckById(1, null);

        // Assert
        Assert.IsType<BadRequestObjectResult>(nullDtoResult);
    }
    
    [Fact]
    public async Task UpdateDeckById_WhenDeckDoesNotExist_ReturnsNotFound()
    {
        // Arrange
        var mockRepo = new Mock<IDecksRepository>();
        var mockLogger = new Mock<ILogger<DecksController>>();
        var mockValidationService = new Mock<IDeckValidationService>();
        var mockNotificationsClient = new Mock<INotificationClient>();
        var mockFactory = new Mock<IDeckFactory>();
        var controller = new DecksController(mockLogger.Object, mockRepo.Object, mockFactory.Object, mockValidationService.Object, mockNotificationsClient.Object);
        var updateDeckDto = new CreateDeckDTO { Name = "Updated Name", Description = "Updated Description", IsPersonal = true, CreatorId = 1 };

        mockRepo.Setup(repo => repo.GetDeckByIdWithCards(It.IsAny<int>())).Returns((Deck)null);

        // Act
        var result = await controller.UpdateDeckById(1, updateDeckDto);

        // Assert
        Assert.IsType<NotFoundObjectResult>(result);
    }

    [Fact]
    public void GetAllDecks_WhenNoDecksExist_ShouldReturnOkWithEmptyList()
    {
        // Arrange
        var mockRepo = new Mock<IDecksRepository>();
        var mockLogger = new Mock<ILogger<DecksController>>();
        var mockValidationService = new Mock<IDeckValidationService>();
        var mockNotificationsClient = new Mock<INotificationClient>();
        var emptyDecks = new List<Deck>();

        mockRepo.Setup(repo => repo.GetAllWithCards()).Returns(emptyDecks);

        var mockFactory = new Mock<IDeckFactory>();
        var controller = new DecksController(mockLogger.Object, mockRepo.Object, mockFactory.Object, mockValidationService.Object, mockNotificationsClient.Object);

        // Act
        var result = controller.GetAllDecks();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedDecks = Assert.IsType<List<Deck>>(okResult.Value);
        Assert.Empty(returnedDecks); // The list should be empty since no decks exist
    }

}
