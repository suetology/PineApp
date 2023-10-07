using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PineAPP.Data;
using PineAPP.Models;
using PineAPP.Models.Dto;

namespace PineAPP.Controllers;

[Route("api/Decks")]
[ApiController]
public class DecksController : ControllerBase
{
    private readonly ApplicationDbContext _db;

    public DecksController(ApplicationDbContext db)
    {
        _db = db;
    }

    [HttpGet]
    public async Task<ActionResult<ApiResponse<List<Deck>>>> GetAllDecks()
    {
        var decks = await _db.Decks.Include(deck => deck.Cards).ToListAsync();
        var response = new ApiResponse<List<Deck>>(HttpStatusCode.OK, isSuccess: true, decks);
        return Ok(response);
    }

    [HttpGet("Personal/{creatorId:int}")]
    public async Task<ActionResult<ApiResponse<List<Deck>>>> GetPersonalDecks(int creatorId)
    {
        var decks = await _db.Decks
                            .Where(deck => deck.IsPersonal && deck.CreatorId == creatorId)
                            .Include(deck => deck.Cards)
                            .ToListAsync();
        
        var response = new ApiResponse<List<Deck>>(HttpStatusCode.OK, isSuccess: true, decks);
        return Ok(response);
    }
    
    [HttpGet("Community")]
    public async Task<ActionResult<ApiResponse<List<Deck>>>> GetCommunityDecks()
    {
        var decks = await _db.Decks
            .Where(deck => !deck.IsPersonal)
            .Include(deck => deck.Cards)
            .ToListAsync();
        
        var response = new ApiResponse<List<Deck>>(HttpStatusCode.OK, isSuccess: true, decks);
        return Ok(response);
    }
    
    [HttpGet("{deckId:int}", Name = "GetDeck")]
    public async Task<ActionResult<ApiResponse<Deck>>> GetDeckById(int deckId)
    {
        var deck = await _db.Decks
            .Where(deck => deck.Id == deckId)
            .Include(deck => deck.Cards)
            .FirstOrDefaultAsync();
        
        var response = new ApiResponse<Deck>(HttpStatusCode.OK, isSuccess: true, deck);
        return Ok(response);
    } 
    
    [HttpPost("Add")]
    public async Task<ActionResult<ApiResponse<List<Deck>>>> AddDeck([FromBody] CreateDeckDTO createDeckDto)
    {
    try
    {
        var response = new ApiResponse<List<Deck>>(
        isSuccess: false,
        statusCode: HttpStatusCode.BadRequest,
        result: null, // You can specify the result as needed
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

        _db.Decks.Add(createNewDeck);
        _db.SaveChanges();

        response = new ApiResponse<List<Deck>>(
        isSuccess: true,
        statusCode: HttpStatusCode.Created,
        result: new List<Deck> { createNewDeck },
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

        return StatusCode((int)HttpStatusCode.InternalServerError, response);
    }
}

    [HttpDelete("Delete/{deckId:int}")]
    public async Task<ActionResult<ApiResponse<List<Deck>>>> DeleteDeckById(int deckId)
    {
        try
        {
            var response = new ApiResponse<List<Deck>>(
            isSuccess : false,
            statusCode : HttpStatusCode.InternalServerError,
            result : null,
            errorMessage: "Deck id is 0, Deck does not exist/is invalid");

            if (ModelState.IsValid)
            {
                Deck Deck = await _db.Decks.FindAsync(deckId);
                if (deckId == 0)
                {
                    return BadRequest(response);
                }

                response = new ApiResponse<List<Deck>>(
                isSuccess: true,
                statusCode: HttpStatusCode.NoContent,
                result: null,
                errorMessage: null);

                _db.Decks.Remove(Deck);
                _db.SaveChanges();
                return Ok(response);
            }
        }
        catch (Exception e)
        {
            var response = new ApiResponse<List<Deck>>(
            isSuccess: false,
            statusCode: HttpStatusCode.InternalServerError,
            result: null,
            errorMessage: e.ToString());
        
            return BadRequest(response);
        }

        return NotFound();
    }
    
    [HttpPut("Update/{deckId}")]
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
    
                _db.Decks.Update(existingDeck);
                await _db.SaveChangesAsync();
    
                return Ok(existingDeck);
            }
            catch (Exception e)
            {
                var response = new ApiResponse<List<Deck>>(
                    isSuccess: false,
                    statusCode: HttpStatusCode.InternalServerError,
                    result: null,
                    errorMessage: e.ToString());
        
                return BadRequest(response);
            }
            
        }

    [HttpPost("Add/Card")]
    public async Task<ActionResult<ApiResponse<Card>>> AddCard([FromBody] CreateCardDTO createCard)
    {
        var response = new ApiResponse<Card>(
        isSuccess: false,
        statusCode: HttpStatusCode.BadRequest,
        result: null, // You can specify the result as needed
        errorMessage: "CreateDeckDTO is null.");

        try
        {

            if(ModelState.IsValid)
            {
                if(createCard == null)
                {
                    return BadRequest(response);
                }    
            }

            Card card = new()
            {
                Front = createCard.Front,
                Back = createCard.Back,
                DeckId = createCard.DeckId
            };

            _db.Cards.Add(card);
            _db.SaveChanges();

            response = new ApiResponse<Card>(
            isSuccess: true,
            statusCode: HttpStatusCode.Created,
            result: null,
            errorMessage: null);

            //return CreatedAtRoute("GetCard", new { deckId = card.DeckId }, response);       
            return Ok(response);
        } catch(Exception e)
        {
            response = new ApiResponse<Card>(
            isSuccess: false,
            statusCode: HttpStatusCode.InternalServerError,
            result: null,
            errorMessage: e.ToString()
        );

            return BadRequest(response);
        }
    }
    
    [HttpDelete("Delete/Card/{cardId:int}")]
    public async Task<ActionResult<ApiResponse<Card>>> DeleteCardById(int cardId)
    {
        try
        {
            var response = new ApiResponse<Card>(
                isSuccess : false,
                statusCode : HttpStatusCode.InternalServerError,
                result : null,
                errorMessage: "Card id is 0, Deck does not exist/is invalid");

            if (ModelState.IsValid)
            {
                Card card = await _db.Cards.FindAsync(cardId);
                if (cardId == 0)
                {
                    return BadRequest(response);
                }

                response = new ApiResponse<Card>(
                    isSuccess: true,
                    statusCode: HttpStatusCode.NoContent,
                    result: null,
                    errorMessage: null);

                _db.Cards.Remove(card);
                _db.SaveChanges();
                return Ok(response);
            }
        }
        catch (Exception e)
        {
            var response = new ApiResponse<Card>(
                isSuccess: false,
                statusCode: HttpStatusCode.InternalServerError,
                result: null,
                errorMessage: e.ToString());
        
            return BadRequest(response);
        }

        return NotFound();
    }
}

//TO DO

//Make it so when you are on the /create endpoint it redirects you to the new deck you are creating
//Implement Delete functionality
//Implement Add Cards functionality