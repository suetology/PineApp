using System.Net.Http;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace PineApp.TestProject;

public class IntegrationTests : IClassFixture<PineAppWebApplicationFactory>
{
    private readonly HttpClient _client;
    
    public IntegrationTests(PineAppWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }
    
    [Fact]
    public async Task Get_EndpointReturnsSuccessAndCorrectContentType()
    {
        var url = "api/Decks";
        
        var response = await _client.GetAsync(url);
        
        response.EnsureSuccessStatusCode(); // Status Code 200-299

        var headerContentType = response.Content.Headers.ContentType?.ToString();
        var content = await response.Content.ReadAsStringAsync();
        
        Assert.Equal("application/json; charset=utf-8", headerContentType);
        Assert.Equal("""
                     [{"id":1,"name":"Simple Math (Community)","description":"A few cards to test your basic math skills","isPersonal":false,"creatorId":1,"cards":[{"id":1,"front":"2 + 2 = ?","back":"4","deckId":1,"examples":"","totalCardsInDeck":0,"currentCardIndex":0},{"id":2,"front":"5 - 2 = ?","back":"3","deckId":1,"examples":"","totalCardsInDeck":0,"currentCardIndex":0},{"id":3,"front":"4 * 3 = ?","back":"12","deckId":1,"examples":"","totalCardsInDeck":0,"currentCardIndex":0}],"correct":0,"wrong":0},{"id":2,"name":"Simple Math (Personal)","description":"A few cards to test your basic math skills","isPersonal":true,"creatorId":1,"cards":[{"id":4,"front":"2 + 2 = ?","back":"4","deckId":2,"examples":"","totalCardsInDeck":0,"currentCardIndex":0},{"id":5,"front":"5 - 2 = ?","back":"3","deckId":2,"examples":"","totalCardsInDeck":0,"currentCardIndex":0},{"id":6,"front":"4 * 3 = ?","back":"12","deckId":2,"examples":"","totalCardsInDeck":0,"currentCardIndex":0}],"correct":0,"wrong":0}]
                     """, content);
    }
}