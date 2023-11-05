using System.Net;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using PineAPP.Data;
using PineAPP.Exceptions;
using PineAPP.Models;
using PineAPP.Services;
using PineAPP.Services.Repositories;

namespace PineAPP.Controllers;

[Route("api/Upload")]
[ApiController]
public class UploadController : ControllerBase
{
    private readonly IDecksRepository _decksRepository;
    private readonly ICardsRepository _cardsRepository;
    private readonly IDeckValidationService _deckValidationService;

    public UploadController(
        IDecksRepository decksRepository, 
        ICardsRepository cardsRepository,
        IDeckValidationService deckValidationService)
    {
        _decksRepository = decksRepository;
        _cardsRepository = cardsRepository;
        _deckValidationService = deckValidationService;
    }

    [HttpPost]
    public async Task<ActionResult> UploadDeck(IFormFile file)
    {
        if (file == null || file.Length == 0)
            return BadRequest("File is null or empty");

        using var reader = new StreamReader(file.OpenReadStream());
        var content = await reader.ReadToEndAsync();
        
        try
        {
            var deckBuilder = new DeckBuilderService(1);  //temporary hardcoded user id
            var deck = deckBuilder.CreateDeckFromString(content);

            _deckValidationService.ValidateDeck(deck);
            
            _decksRepository.Add(deck);
            _cardsRepository.AddRange(deck.Cards);
            
            await _decksRepository.SaveChangesAsync();

            return CreatedAtRoute("GetDeck", new { deckId = deck.Id }, deck);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}   