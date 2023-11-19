using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using PineAPP.Controllers;
using PineAPP.Models;
using PineAPP.Services;
using PineAPP.Services.Repositories;

namespace PineApp.TestProject;

public class UploadTests
{
    [Fact]
    public async Task UploadDeck_ValidFile_ReturnsCreatedAtRouteResult()
    {
        // Arrange
        var mockDecksRepo = new Mock<IDecksRepository>();
        var mockCardsRepo = new Mock<ICardsRepository>();
        var mockDeckBuilderService = new Mock<IDeckBuilderService>();
        var controller = new UploadController(mockDecksRepo.Object, mockCardsRepo.Object, mockDeckBuilderService.Object);

        var mockFile = new Mock<IFormFile>();
        var fileName = "test.txt";
        var fileContent = "File content here";
        var ms = new MemoryStream();
        var writer = new StreamWriter(ms);
        writer.Write(fileContent);
        writer.Flush();
        ms.Position = 0;

        mockFile.Setup(_ => _.OpenReadStream()).Returns(ms);
        mockFile.Setup(_ => _.FileName).Returns(fileName);
        mockFile.Setup(_ => _.Length).Returns(ms.Length);

        var mockDeck = new Deck
        {
            Name = "Test Deck",
            Description = "Test Description",
            IsPersonal = true,
            CreatorId = 1
        };
        mockDeckBuilderService.Setup(s => s.CreateDeckFromString(fileContent)).Returns(mockDeck);

        // Act
        var result = await controller.UploadDeck(mockFile.Object);

        // Assert
        var createdAtRouteResult = Assert.IsType<CreatedAtRouteResult>(result);
        Assert.Equal(mockDeck, createdAtRouteResult.Value);
    }
    
    [Fact]
    public async Task UploadDeck_WhenExceptionOccurs_ShouldReturnBadRequest()
    {
        // Arrange
        var mockDecksRepo = new Mock<IDecksRepository>();
        var mockCardsRepo = new Mock<ICardsRepository>();
        var mockDeckBuilderService = new Mock<IDeckBuilderService>();
        var controller = new UploadController(mockDecksRepo.Object, mockCardsRepo.Object, mockDeckBuilderService.Object);

        var mockFile = new Mock<IFormFile>();
        var fileName = "test.txt";
        var fileContent = "File content here";
        var ms = new MemoryStream();
        var writer = new StreamWriter(ms);
        writer.Write(fileContent);
        writer.Flush();
        ms.Position = 0;

        mockFile.Setup(_ => _.OpenReadStream()).Returns(ms);
        mockFile.Setup(_ => _.FileName).Returns(fileName);
        mockFile.Setup(_ => _.Length).Returns(ms.Length);

        var exceptionMessage = "Test exception";
        mockDeckBuilderService.Setup(s => s.CreateDeckFromString(It.IsAny<string>()))
            .Throws(new Exception(exceptionMessage));

        // Act
        var result = await controller.UploadDeck(mockFile.Object);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal(exceptionMessage, badRequestResult.Value);
    }
    
    [Fact]
    public async Task UploadDeck_FileIsNull_ReturnsBadRequest()
    {
        // Arrange
        var mockDecksRepo = new Mock<IDecksRepository>();
        var mockCardsRepo = new Mock<ICardsRepository>();
        var mockDeckBuilderService = new Mock<IDeckBuilderService>();
        var controller = new UploadController(mockDecksRepo.Object, mockCardsRepo.Object, mockDeckBuilderService.Object);

        IFormFile file = null; // File is set to null

        // Act
        var result = await controller.UploadDeck(file);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("File is null or empty", badRequestResult.Value);
    }

}