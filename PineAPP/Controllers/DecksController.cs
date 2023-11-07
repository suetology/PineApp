using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PineAPP.Data;
using PineAPP.Models;
using PineAPP.Models.Dto;

namespace PineAPP.Controllers;

[Route("api/Decks")]
[ApiController]
public class DecksController : ControllerBase
{
    private readonly ApplicationDbContext _db;
    private readonly ILogger<DecksController> _logger;

    public DecksController(ApplicationDbContext db, ILogger<DecksController> logger)
    {
        _db = db;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<ApiResponse<List<Deck>>>> GetAllDecks()
    {
        try
        {
            var decks = await _db.Decks.Include(deck => deck.Cards).ToListAsync();
            var response = new ApiResponse<List<Deck>>(HttpStatusCode.OK, isSuccess: true, decks);
            return Ok(response);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred in endpoint GetAllDecks");
            var response = new ApiResponse<List<Deck>>(HttpStatusCode.InternalServerError, isSuccess: false);
            return StatusCode((int)HttpStatusCode.InternalServerError, response);
        }
        
    }
    
    [HttpGet("All/{creatorId:int}")]
    public async Task<ActionResult<ApiResponse<List<Deck>>>> GetAllDecksById(int creatorId)
    {
        try
        {
            var decks = await _db.Decks
                .Where(deck => (deck.IsPersonal && deck.CreatorId == creatorId) || !deck.IsPersonal)
                .Include(deck => deck.Cards).ToListAsync();
        
            var response = new ApiResponse<List<Deck>>(HttpStatusCode.OK, isSuccess: true, decks);
            return Ok(response);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred in endpoint GetAllDecksById");
            var response = new ApiResponse<List<Deck>>(HttpStatusCode.InternalServerError, isSuccess: false);
            return StatusCode((int)HttpStatusCode.InternalServerError, response);
        }
        
    }

    [HttpGet("Personal/{creatorId:int}")]
    public async Task<ActionResult<ApiResponse<List<Deck>>>> GetPersonalDecks(int creatorId)
    {
        try
        {
            var decks = await _db.Decks
                .Where(deck => deck.IsPersonal && deck.CreatorId == creatorId)
                .Include(deck => deck.Cards)
                .ToListAsync();
        
            var response = new ApiResponse<List<Deck>>(HttpStatusCode.OK, isSuccess: true, decks);
            return Ok(response);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred in endpoint GetPersonalDecks");
            var response = new ApiResponse<List<Deck>>(HttpStatusCode.InternalServerError, isSuccess: false);
            return StatusCode((int)HttpStatusCode.InternalServerError, response);        }
        
    }
    
    [HttpGet("Community")]
    public async Task<ActionResult<ApiResponse<List<Deck>>>> GetCommunityDecks()
    {
        try
        {
            var decks = await _db.Decks
                .Where(deck => !deck.IsPersonal)
                .Include(deck => deck.Cards)
                .ToListAsync();
        
            var response = new ApiResponse<List<Deck>>(HttpStatusCode.OK, isSuccess: true, decks);
            return Ok(response);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred in endpoint GetCommunityDecks");
            var response = new ApiResponse<List<Deck>>(HttpStatusCode.InternalServerError, isSuccess: false);
            return StatusCode((int)HttpStatusCode.InternalServerError, response);  
        }
        
    }
    
    [HttpGet("{deckId:int}", Name = "GetDeck")]
    public async Task<ActionResult<ApiResponse<Deck>>> GetDeckById(int deckId)
    {
        try
        {
            var deck = await _db.Decks
                .Where(deck => deck.Id == deckId)
                .Include(deck => deck.Cards)
                .FirstOrDefaultAsync();
        
            var response = new ApiResponse<Deck>(HttpStatusCode.OK, isSuccess: true, deck);
            return Ok(response);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred in endpoint GetDeckById");
            var response = new ApiResponse<Deck>(HttpStatusCode.InternalServerError, isSuccess: false);
            return StatusCode((int)HttpStatusCode.InternalServerError, response); 
        }
        
    } 
    
    [HttpPost]
    public async Task<ActionResult<ApiResponse<Deck>>> AddDeck([FromBody] CreateDeckDTO createDeckDto)
    {
        try
        {
            var response = new ApiResponse<Deck>(
            isSuccess: false,
            statusCode: HttpStatusCode.BadRequest,
            result: null,
            errorMessage: "CreateDeckDTO is null.");

            if (ModelState.IsValid)
            {
                if (createDeckDto == null)
                {
                    return BadRequest(response);
                }
            }

            Deck createNewDeck = new()
            {
                Name = createDeckDto.Name,
                Description = createDeckDto.Description,
                IsPersonal = createDeckDto.IsPersonal,
                CreatorId = createDeckDto.CreatorId,
            };

            if (DeckBuilder.ContainsForbiddenCharacters(createNewDeck.Name))
            {
                response = new ApiResponse<Deck>(
                    statusCode: HttpStatusCode.BadRequest,
                    isSuccess: false,
                    errorMessage: "Deck name contains forbidden characters"
                );
                return BadRequest(response);
            }
            
            if (Enumerable.Any(_db.Decks, d => d.Equals(createNewDeck)))
            {
                response = new ApiResponse<Deck>(
                    statusCode: HttpStatusCode.Conflict,
                    isSuccess: false,
                    errorMessage: "Deck with such name already exists"
                );
                return Conflict(response);
            }

            _db.Decks.Add(createNewDeck);
            await _db.SaveChangesAsync();

            response = new ApiResponse<Deck>(
            isSuccess: true,
            statusCode: HttpStatusCode.Created,
            result: createNewDeck,
            errorMessage: null);

            return CreatedAtRoute("GetDeck", new { deckId = createNewDeck.Id }, response);
        }
        catch (Exception e)
        {
            var response = new ApiResponse<List<Deck>>(
                isSuccess: false,
                statusCode: HttpStatusCode.InternalServerError,
                result: null,
                errorMessage: e.ToString());

            _logger.LogError(e, "An error occurred in endpoint AddDeck");
            return StatusCode((int)HttpStatusCode.InternalServerError, response);
        }
    }

    [HttpDelete("{deckId:int}")]
    public async Task<ActionResult<ApiResponse<Deck>>> DeleteDeckById(int deckId)
    {
        try
        {
            var response = new ApiResponse<Deck>(
            isSuccess : false,
            statusCode : HttpStatusCode.InternalServerError,
            result : null,
            errorMessage: "Deck id is 0, Deck does not exist/is invalid");

            if (ModelState.IsValid)
            {
                Deck deck = await _db.Decks.FindAsync(deckId);
                if (deckId == 0 || deck is null)
                {
                    return BadRequest(response);
                }

                response = new ApiResponse<Deck>(
                isSuccess: true,
                statusCode: HttpStatusCode.NoContent,
                result: deck,
                errorMessage: null);

                _db.Decks.Remove(deck);
                await _db.SaveChangesAsync();
                return Ok(response);
            }
        }
        catch (Exception e)
        {
            var response = new ApiResponse<Deck>(
            isSuccess: false,
            statusCode: HttpStatusCode.InternalServerError,
            result: null,
            errorMessage: e.ToString());
        
            _logger.LogError(e, "An error occurred in endpoint DeleteDeckById");
            return StatusCode((int)HttpStatusCode.InternalServerError, response);
        }

        return NotFound();
    }
    
    [HttpPut("{deckId}")]
    public async Task<ActionResult<ApiResponse<Deck>>> UpdateDeckById(int deckId,[FromBody] CreateDeckDTO createDeckDto)
    {
        try
        {
            if (createDeckDto == null)
            {
                return BadRequest("Invalid request");
            }
    
            var existingDeck = await _db.Decks.FindAsync(deckId);
    
            if (existingDeck == null)
            {
                return NotFound("Deck not found");
            }
                
            existingDeck.IsPersonal = createDeckDto.IsPersonal;
            existingDeck.Description = createDeckDto.Description;
            existingDeck.Name = createDeckDto.Name;
            existingDeck.Correct = createDeckDto.Correct;
            existingDeck.Wrong = createDeckDto.Wrong;
            
            var response = new ApiResponse<Deck>(
                statusCode: HttpStatusCode.OK,
                isSuccess: true,
                errorMessage: null,
                result: existingDeck
            );
                
            if (DeckBuilder.ContainsForbiddenCharacters(existingDeck.Name))
            {
                response = new ApiResponse<Deck>(
                    statusCode: HttpStatusCode.BadRequest,
                    isSuccess: false,
                    errorMessage: "Deck name contains forbidden characters"
                );
                return BadRequest(response);
            }
                    
            _db.Decks.Update(existingDeck);
            await _db.SaveChangesAsync();
    
            return Ok(response);
        }
        catch (Exception e)
        {
            var response = new ApiResponse<Deck>(
                isSuccess: false,
                statusCode: HttpStatusCode.InternalServerError,
                result: null,
                errorMessage: e.ToString());
        
            _logger.LogError(e, "An error occurred in endpoint UpdateDeckById");
            return StatusCode((int)HttpStatusCode.InternalServerError, response);        
        }
            
    }

    [HttpGet("Search/{keyword}")]
    public async Task<ActionResult<ApiResponse<List<Deck>>>> SearchDecks(string keyword)
    {
        try
        {
            var decks = await _db.Decks
                .Where(deck => deck.Name.Contains(keyword))
                .Include(deck => deck.Cards)
                .ToListAsync();

            var response = new ApiResponse<List<Deck>>(HttpStatusCode.OK, isSuccess: true, decks);
            return Ok(response);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred in endpoint SearchDecks");
            var response = new ApiResponse<List<Deck>>(HttpStatusCode.InternalServerError, isSuccess: false);
            return StatusCode((int)HttpStatusCode.InternalServerError, response);        
        }
        
    }
}


