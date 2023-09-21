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
    private readonly ApiResponse _response;

    public DecksController(ApplicationDbContext db)
    {
        _db = db;
        _response = new ApiResponse();
    }

    [HttpGet]
    public async Task<ActionResult<ApiResponse>> GetAllDecks()
    {
        _response.Result = await _db.Decks.Include(deck => deck.Cards).ToListAsync();
        _response.StatusCode = System.Net.HttpStatusCode.OK;
        _response.IsSuccess = true;
        
        return Ok(_response);
    }

    [HttpGet("Personal/{creatorId:int}")]
    public async Task<ActionResult<ApiResponse>> GetPersonalDecks(int creatorId)
    {
        _response.Result = await _db.Decks
                            .Where(deck => deck.IsPersonal && deck.CreatorId == creatorId)
                            .Include(deck => deck.Cards)
                            .ToListAsync();
        _response.StatusCode = System.Net.HttpStatusCode.OK;
        _response.IsSuccess = true;

        return Ok(_response);
    }
    
    [HttpGet("Community")]
    public async Task<ActionResult<ApiResponse>> GetCommunityDecks()
    {
        _response.Result = await _db.Decks
            .Where(deck => !deck.IsPersonal)
            .Include(deck => deck.Cards)
            .ToListAsync();
        _response.StatusCode = System.Net.HttpStatusCode.OK;
        _response.IsSuccess = true;

        return Ok(_response);
    }
    
    [HttpGet("{deckId:int}")]
    public async Task<ActionResult<ApiResponse>> GetDeckById(int deckId)
    {
        _response.Result = await _db.Decks
            .Where(deck => deck.Id == deckId)
            .Include(deck => deck.Cards)
            .ToListAsync();
        _response.StatusCode = System.Net.HttpStatusCode.OK;
        _response.IsSuccess = true;

        return Ok(_response);
    }
}