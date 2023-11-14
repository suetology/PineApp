using System.Net;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using PineAPP.Data;
using PineAPP.Exceptions;
using PineAPP.Models;
using PineAPP.Services;
using PineAPP.Services.Repositories;
using PineAPP.Services.Factories;

namespace PineAPP.Controllers;

[Route("api/Upload")]
[ApiController]
public class UploadController : ControllerBase
{
    private readonly IDecksRepository _decksRepository;
    private readonly ICardsRepository _cardsRepository;
    private readonly IDeckBuilderService _deckBuilderService;

    public UploadController(
        IDecksRepository decksRepository, 
        ICardsRepository cardsRepository,
        IDeckBuilderService deckBuilderService)
    {
        _decksRepository = decksRepository;
        _cardsRepository = cardsRepository;
        _deckBuilderService = deckBuilderService;
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
            var deck = _deckBuilderService.CreateDeckFromString(content);
            
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