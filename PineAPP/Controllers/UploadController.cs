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
        ApiResponse<Deck> response;
        
        if (file == null || file.Length == 0)
        {
            response = new ApiResponse<Deck>(
                statusCode: HttpStatusCode.BadRequest,
                isSuccess: false,
                errorMessage: "File is null or empty"
            );
            return BadRequest(response);
        }

        using var reader = new StreamReader(file.OpenReadStream());
        var content = await reader.ReadToEndAsync();
        
        try
        {
            var deckBuilder = new DeckBuilder(1);  //temporary hardcoded user id
            var deck = deckBuilder.CreateDeckFromString(content);

            if (DeckBuilder.ContainsForbiddenCharacters(deck.Name))
            {
                response = new ApiResponse<Deck>(
                    statusCode: HttpStatusCode.BadRequest,
                    isSuccess: false,
                    errorMessage: "Deck name contains forbidden characters"
                );
                return BadRequest(response);
            }

            if (Enumerable.Any(_db.Decks, d => d.Equals(deck)))
            {
                response = new ApiResponse<Deck>(
                    statusCode: HttpStatusCode.Conflict,
                    isSuccess: false,
                    errorMessage: "Deck with such name already exists"
                );
                return Conflict(response);
            }
            
            _db.Decks.Add(deck);
            _db.Cards.AddRange(deck.Cards);
            await _db.SaveChangesAsync();
            
            response = new ApiResponse<Deck>(
                statusCode: HttpStatusCode.Created,
                isSuccess: true,
                result: deck
            );

            return CreatedAtRoute("GetDeck", new { deckId = deck.Id }, response);
        }
        catch (InvalidFormatException ex)
        {
            response = new ApiResponse<Deck>(
                statusCode: HttpStatusCode.BadRequest,
                isSuccess: false,
                errorMessage: ex.Message
            );

            return BadRequest(response);
        }
    }
}   