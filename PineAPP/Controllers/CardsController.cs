using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PineAPP.Data;
using PineAPP.Models;
using PineAPP.Models.Dto;

namespace PineAPP.Controllers;

[Route("api/Card")]
[ApiController]
public class CardsController : ControllerBase
{
    private readonly ApplicationDbContext _db;

    public CardsController(ApplicationDbContext db)
    {
        _db = db;
    }

    [HttpPost]
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
            await _db.SaveChangesAsync();

            response = new ApiResponse<Card>(
            isSuccess: true,
            statusCode: HttpStatusCode.Created,
            result: card,
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

    [HttpPut("{cardId:int}")]
    public async Task<ActionResult<ApiResponse<Card>>> UpdateCardById(int cardId, [FromBody] UpdateCardDTO updateCard)
    {
        try
        {
            if (cardId == 0 || updateCard == null)
            {
                return BadRequest("Wrong ID or Card");
            }

            var existingCard = await _db.Cards.FindAsync(cardId);

            if (existingCard == null)
            {
                return NotFound("Does not exist");
            }

            existingCard.Front = updateCard.Front;
            existingCard.Back = updateCard.Back;

            _db.Cards.Update(existingCard);
            await _db.SaveChangesAsync();
    
            return Ok(existingCard);
        }
        catch(Exception e)
        {
            var response = new ApiResponse<Card>(
                isSuccess: false,
                statusCode: HttpStatusCode.InternalServerError,
                result: null,
                errorMessage: e.ToString());

            return BadRequest(response);
        }
    }
    
    [HttpDelete("{cardId:int}")]
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
                if (cardId == 0 || card is null)
                {
                    return BadRequest(response);
                }

                response = new ApiResponse<Card>(
                    isSuccess: true,
                    statusCode: HttpStatusCode.NoContent,
                    result: null,
                    errorMessage: null);

                _db.Cards.Remove(card);
                await _db.SaveChangesAsync();
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
    
    [HttpGet("{cardId:int}")]
    public async Task<ActionResult<ApiResponse<Card>>> GetCardById(int cardId)
    {
        var card = await _db.Cards.FindAsync(cardId);
        if (card == null)
        {
            return NotFound("Card not found");
        }
    
        var totalCardsInDeck = await _db.Cards.Where(c => c.DeckId == card.DeckId).CountAsync();
        var currentCardIndex = await _db.Cards.Where(c => c.DeckId == card.DeckId && c.Id <= cardId).CountAsync();
        
        card.TotalCardsInDeck = totalCardsInDeck;
        card.CurrentCardIndex = currentCardIndex;
        
        return Ok(card);
    }
}


