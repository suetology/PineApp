using System.Net;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using PineAPP.Data;
using PineAPP.Exceptions;
using PineAPP.Models;

namespace PineAPP.Controllers;

[Route("api/Upload")]
[ApiController]
public class UploadController : ControllerBase
{
    private readonly ApplicationDbContext _db;

    public UploadController(ApplicationDbContext db)
    {
        _db = db;
    }

    [HttpPost]
    public async Task<ActionResult<ApiResponse<Deck>>> UploadDeck(IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            var response = new ApiResponse<Deck>(
                statusCode: HttpStatusCode.BadRequest,
                isSuccess: false,
                errorMessage: "File is null or empty"
            );
            return BadRequest(response);
        }

        using var reader = new StreamReader(file.OpenReadStream());
        var content = reader.ReadToEnd();
        
        try
        {
            var deckBuilder = new DeckBuilder();
            var deck = deckBuilder.CreateDeckFromString(content);

            _db.Decks.Add(deck);
            await _db.SaveChangesAsync();
            
            var response = new ApiResponse<Deck>(
                statusCode: HttpStatusCode.Created,
                isSuccess: true,
                result: deck
            );

            return CreatedAtRoute("GetDeck", new { deckId = deck.Id }, response);
        }
        catch (InvalidFormatException ex)
        {
            var response = new ApiResponse<Deck>(
                statusCode: HttpStatusCode.BadRequest,
                isSuccess: false,
                errorMessage: ex.Message
            );

            return BadRequest(response);
        }
    }
}