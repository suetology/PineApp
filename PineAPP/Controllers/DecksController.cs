using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PineAPP.Data;
using PineAPP.Models;
using PineAPP.Models.Dto;
using PineAPP.Services;
using PineAPP.Services.Repositories;
using PineAPP.Services.Factories;

namespace PineAPP.Controllers;

[Route("api/Decks")]
[ApiController]
public class DecksController : ControllerBase
{
    private readonly ILogger<DecksController> _logger;
    private readonly IDecksRepository _decksRepository;
    private readonly IDeckFactory _deckFactory;
    private readonly IDeckValidationService _deckValidationService;
    private readonly INotificationClient _notificationClient;

    public DecksController(
        ILogger<DecksController> logger, 
        IDecksRepository decksRepository,
        IDeckFactory deckFactory,
        IDeckValidationService deckValidationService,
        INotificationClient notificationClient)
    {
        _logger = logger;
        _decksRepository = decksRepository;
        _deckFactory = deckFactory;
        _deckValidationService = deckValidationService;
        _notificationClient = notificationClient;
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
            if (deck == null)
            {
                return NotFound(); 
            }
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
            if (!ModelState.IsValid)
            {
                if (createDeckDto == null)
                {
                    return BadRequest(ModelState);
                }
            }

            var createNewDeck = _deckFactory.CreateDeck(
                createDeckDto.Name, 
                createDeckDto.Description, 
                createDeckDto.IsPersonal, 
                createDeckDto.CreatorId);
            
            _decksRepository.Add(createNewDeck);
            await _decksRepository.SaveChangesAsync();

            if (!createNewDeck.IsPersonal)
                await _notificationClient.SendNotification("The new " + createNewDeck.Name + " is created. Check it out!");

            return Ok(createNewDeck);
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
                    return NotFound("Deck id is 0, Deck does not exist/is invalid");

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
                
            _deckValidationService.CheckForForbiddenCharacters(existingDeck);
                    
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


