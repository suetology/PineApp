using System.Net.Http;
using System.Threading.Tasks;

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
        //  var url = "api/Decks";
        
        //var response = await _client.GetAsync(url);
        
        //response.EnsureSuccessStatusCode(); // Status Code 200-299
       // Assert.Equal("application/json", response.Content.Headers.ContentType.ToString());
        
       // var responseString = await response.Content.ReadAsStringAsync();
    }
}