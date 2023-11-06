using Microsoft.AspNetCore.Mvc;
using Moq;
using PineAPP.Controllers;
using PineAPP.Models;
using PineAPP.Models.Dto;
using PineAPP.Services.Repositories;

namespace PineApp.TestProject;

public class CardTests
{
    [Fact]
    public async Task AddCard_WithValidCard_ReturnsOk()
    {
        // Arrange data
        var mockRepo = new Mock<ICardsRepository>();
        var controller = new CardsController(mockRepo.Object);
        var newCard = new CreateCardDTO { Front = "Question?", Back = "Answer", DeckId = 1 };

        // Act, invoke method
        var result = await controller.AddCard(newCard);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnCard = Assert.IsType<Card>(okResult.Value);
        Assert.Equal(newCard.Front, returnCard.Front);
        Assert.Equal(newCard.Back, returnCard.Back);
        mockRepo.Verify(x => x.Add(It.IsAny<Card>()), Times.Once);
        mockRepo.Verify(x => x.SaveChangesAsync(), Times.Once);
    }
    
    [Fact]
    public async Task DeleteCard_WithExistingCard_ReturnsOk()
    {
        // Arrange
        var mockRepo = new Mock<ICardsRepository>();
        var controller = new CardsController(mockRepo.Object);
        var cardId = 1;
        var card = new Card { Id = cardId, Front = "Question?", Back = "Answer", DeckId = 1 };
        mockRepo.Setup(repo => repo.GetById(cardId)).Returns(card);

        // Act
        var result = await controller.DeleteCardById(cardId);

        // Assert
        Assert.IsType<OkResult>(result);
        mockRepo.Verify(x => x.Remove(card), Times.Once);
        mockRepo.Verify(x => x.SaveChangesAsync(), Times.Once);
    }
    
    [Fact]
    public async Task UpdateCard_WithExistingCard_ReturnsUpdatedCard()
    {
        // Arrange
        var mockRepo = new Mock<ICardsRepository>();
        var controller = new CardsController(mockRepo.Object);
        var cardId = 1;
        var existingCard = new Card { Id = cardId, Front = "Original Question?", Back = "Original Answer", DeckId = 1 };
        var updateCardDTO = new UpdateCardDTO { Front = "Updated Question?", Back = "Updated Answer" };
        mockRepo.Setup(repo => repo.GetById(cardId)).Returns(existingCard);

        // Act
        var result = await controller.UpdateCardById(cardId, updateCardDTO);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var updatedCard = Assert.IsType<Card>(okResult.Value);
        Assert.Equal(updateCardDTO.Front, updatedCard.Front);
        Assert.Equal(updateCardDTO.Back, updatedCard.Back);
        mockRepo.Verify(x => x.Update(It.IsAny<Card>()), Times.Once);
        mockRepo.Verify(x => x.SaveChangesAsync(), Times.Once);
    }
    
    [Fact]
    public void GetCard_WithExistingCard_ReturnsCard()
    {
        // Arrange
        var mockRepo = new Mock<ICardsRepository>();
        var controller = new CardsController(mockRepo.Object);
        var cardId = 1;
        var expectedCard = new Card { Id = cardId, Front = "Question?", Back = "Answer", DeckId = 1 };
        mockRepo.Setup(repo => repo.GetById(cardId)).Returns(expectedCard);

        // Act
        var result = controller.GetCardById(cardId);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnCard = Assert.IsType<Card>(okResult.Value);
        Assert.Equal(expectedCard, returnCard);
    }
    
    [Fact]
    public async Task AddCard_WithInvalidModel_ReturnsBadRequest()
    {
        // Arrange
        var mockRepo = new Mock<ICardsRepository>();
        var controller = new CardsController(mockRepo.Object);
        controller.ModelState.AddModelError("error", "some error"); // Force a model state error

        // Act
        var result = await controller.AddCard(null); // Pass null to trigger the BadRequest

        // Assert
        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task AddCard_WithException_ReturnsBadRequest()
    {
        // Arrange
        var mockRepo = new Mock<ICardsRepository>();
        mockRepo.Setup(repo => repo.SaveChangesAsync()).Throws(new Exception("Some exception message"));
        var controller = new CardsController(mockRepo.Object);
        var createCardDto = new CreateCardDTO { Front = "Front", Back = "Back", DeckId = 1 };

        // Act
        var result = await controller.AddCard(createCardDto);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("Some exception message", badRequestResult.Value);
    }
    
    [Fact]
    public async Task UpdateCardById_WithNonExistingCard_ReturnsNotFound()
    {
        // Arrange
        var mockRepo = new Mock<ICardsRepository>();
        mockRepo.Setup(repo => repo.GetById(It.IsAny<int>())).Returns((Card)null);
        var controller = new CardsController(mockRepo.Object);
        var updateCardDto = new UpdateCardDTO { Front = "New Front", Back = "New Back" };

        // Act
        var result = await controller.UpdateCardById(12341230, updateCardDto);

        // Assert
        Assert.IsType<NotFoundObjectResult>(result);
    }
    
    [Fact]
    public async Task DeleteCardById_WithInvalidId_ReturnsBadRequest()
    {
        // Arrange
        var mockRepo = new Mock<ICardsRepository>();
        var controller = new CardsController(mockRepo.Object);

        // Act
        var result = await controller.DeleteCardById(0);

        // Assert
        Assert.IsType<BadRequestObjectResult>(result);
    }


}