using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PineAPP.Data;
using PineAPP.Models;

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
    
    [HttpGet("{deckId:int}")]
    public async Task<ActionResult<ApiResponse<Deck>>> GetDeckById(int deckId)
    {
        var deck = await _db.Decks
            .Where(deck => deck.Id == deckId)
            .Include(deck => deck.Cards)
            .FirstOrDefaultAsync();
        
        var response = new ApiResponse<Deck>(HttpStatusCode.OK, isSuccess: true, deck);
        return Ok(response);
    }
}