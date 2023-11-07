using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PineAPP.Data;
using PineAPP.Models;
using PineAPP.Models.Dto;
using PineAPP.Services.Repositories;
using PineAPP.Services.Factories;

namespace PineAPP.Controllers;

[Route("api/Card")]
[ApiController]
public class CardsController : ControllerBase
{
    private readonly ICardsRepository _cardsRepository;
    private readonly ICardFactory _cardFactory;

    public CardsController(
        ICardsRepository cardsRepository, 
        ICardFactory cardFactory)
    {
        _cardsRepository = cardsRepository;
        _cardFactory = cardFactory;
    }

    [HttpPost]
    public async Task<ActionResult> AddCard([FromBody] CreateCardDTO createCard)
    {
        try
        {
            if(ModelState.IsValid)
            {
                if (createCard == null)
                    return BadRequest("CreateDeckDTO is null.");
            }

            var card = _cardFactory.CreateCard(
                front: createCard.Front, 
                back: createCard.Back, 
                deckId: createCard.DeckId);

            _cardsRepository.Add(card);
            await _cardsRepository.SaveChangesAsync();

            //return CreatedAtRoute("GetCard", new { deckId = card.DeckId }, response);       
            return Ok(card);
        } 
        catch(Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPut("{cardId:int}")]
    public async Task<ActionResult> UpdateCardById(int cardId, [FromBody] UpdateCardDTO updateCard)
    {
        try
        {
            if (cardId == 0 || updateCard == null)
                return BadRequest("Wrong ID or Card");

            var existingCard = _cardsRepository.GetById(cardId);

            if (existingCard == null)
                return NotFound("Does not exist");

            existingCard.Front = updateCard.Front;
            existingCard.Back = updateCard.Back;

            _cardsRepository.Update(existingCard);
            await _cardsRepository.SaveChangesAsync();
    
            return Ok(existingCard);
        }
        catch(Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [HttpDelete("{cardId:int}")]
    public async Task<ActionResult> DeleteCardById(int cardId)
    {
        try
        {
            if (ModelState.IsValid)
            {
                var card = _cardsRepository.GetById(cardId);
                if (cardId == 0 || card is null)
                {
                    return BadRequest("Card id is 0, Deck does not exist/is invalid");
                }

                _cardsRepository.Remove(card);
                await _cardsRepository.SaveChangesAsync();
                return Ok();
            }
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }

        return NotFound();
    }
    
    [HttpGet("{cardId:int}")]
    public ActionResult GetCardById(int cardId)
    {
        var card = _cardsRepository.GetById(cardId);
        if (card == null)
            return NotFound("Card not found");
    
        card.TotalCardsInDeck = _cardsRepository.GetTotalCardsInCurrentDeck(card);
        card.CurrentCardIndex = _cardsRepository.GetCurrentCardIndex(card);
        
        return Ok(card);
    }
}


