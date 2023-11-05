using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PineAPP.Data;
using PineAPP.Models;
using PineAPP.Models.Dto;
using PineAPP.Services;
using PineAPP.Services.Repositories;

namespace PineAPP.Controllers;

[Route("api/Decks")]
[ApiController]
public class DecksController : ControllerBase
{
    private readonly ILogger<DecksController> _logger;
    private readonly IDecksRepository _decksRepository;
    private readonly IDeckValidationService _deckValidationService;

    public DecksController(
        ILogger<DecksController> logger, 
        IDecksRepository decksRepository,
        IDeckValidationService deckValidationService)
    {
        _logger = logger;
        _decksRepository = decksRepository;
        _deckValidationService = deckValidationService;
    }

    [HttpGet]
    public ActionResult GetAllDecks()
    {
        try
        {
            var decks = _decksRepository.GetAllWithCards();
            return Ok(decks);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred in endpoint GetAllDecks");
            return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
        }
        
    }
    
    [HttpGet("All/{creatorId:int}")]
    public ActionResult GetAllDecksById(int creatorId)
    {
        try
        {
            var decks = _decksRepository.GetDecksByCreatorId(creatorId);
            return Ok(decks);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred in endpoint GetAllDecksById");
            return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
        }
        
    }

    [HttpGet("Personal/{creatorId:int}")]
    public ActionResult GetPersonalDecks(int creatorId)
    {
        try
        {
            var decks = _decksRepository.GetPersonalDecks(creatorId);
            return Ok(decks);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred in endpoint GetPersonalDecks");
            return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);        
        }
        
    }
    
    [HttpGet("Community")]
    public ActionResult GetCommunityDecks()
    {
        try
        {
            var decks = _decksRepository.GetCommunityDecks();
            return Ok(decks);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred in endpoint GetCommunityDecks");
            return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);  
        }
        
    }
    
    [HttpGet("{deckId:int}", Name = "GetDeck")]
    public ActionResult GetDeckById(int deckId)
    {
        try
        {
            var deck = _decksRepository.GetDeckByIdWithCards(deckId);
            return Ok(deck);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred in endpoint GetDeckById");
            return StatusCode((int)HttpStatusCode.InternalServerError, e.Message); 
        }
        
    } 
    
    [HttpPost]
    public async Task<ActionResult> AddDeck([FromBody] CreateDeckDTO createDeckDto)
    {
        try
        {
            if (ModelState.IsValid)
            {
                if (createDeckDto == null)
                {
                    return BadRequest("CreateDeckDTO is null.");
                }
            }

            Deck createNewDeck = new()
            {
                Name = createDeckDto.Name,
                Description = createDeckDto.Description,
                IsPersonal = createDeckDto.IsPersonal,
                CreatorId = createDeckDto.CreatorId,
            };

            _deckValidationService.ValidateDeck(createNewDeck);
            
            _decksRepository.Add(createNewDeck);
            await _decksRepository.SaveChangesAsync();

            return CreatedAtRoute("GetDeck", new { deckId = createNewDeck.Id }, createNewDeck);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred in endpoint AddDeck");
            return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
        }
    }

    [HttpDelete("{deckId:int}")]
    public async Task<ActionResult> DeleteDeckById(int deckId)
    {
        try
        {
            if (ModelState.IsValid)
            {
                var deck = _decksRepository.GetDeckByIdWithCards(deckId);
                if (deckId == 0 || deck is null)
                    return BadRequest("Deck id is 0, Deck does not exist/is invalid");

                _decksRepository.Remove(deck);
                await _decksRepository.SaveChangesAsync();
                return Ok(deck);
            }
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred in endpoint DeleteDeckById");
            return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
        }

        return NotFound();
    }
    
    [HttpPut("{deckId:int}")]
    public async Task<ActionResult> UpdateDeckById(int deckId,[FromBody] CreateDeckDTO createDeckDto)
    {
        try
        {
            if (createDeckDto == null)
                return BadRequest("Invalid request");
    
            var existingDeck = _decksRepository.GetDeckByIdWithCards(deckId);
    
            if (existingDeck == null)
                return NotFound("Deck not found");
                
            existingDeck.IsPersonal = createDeckDto.IsPersonal;
            existingDeck.Description = createDeckDto.Description;
            existingDeck.Name = createDeckDto.Name;
            existingDeck.Correct = createDeckDto.Correct;
            existingDeck.Wrong = createDeckDto.Wrong;
                
            _deckValidationService.ValidateDeck(existingDeck);
                    
            _decksRepository.Update(existingDeck);
            await _decksRepository.SaveChangesAsync();
    
            return Ok(existingDeck);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred in endpoint UpdateDeckById");
            return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);        
        }
            
    }

    [HttpGet("Search/{keyword}")]
    public ActionResult SearchDecks(string keyword)
    {
        try
        {
            var decks = _decksRepository.Search(keyword);
            return Ok(decks);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred in endpoint SearchDecks");
            return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);        
        }
        
    }
}


