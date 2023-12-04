using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PineAPP.Models;
using PineAPP.Models.Dto;
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
        
        var decks = JsonConvert.DeserializeObject<List<Deck>>(content);
        Assert.NotNull(decks);
        Assert.True(decks.Any());
    }
    [Fact]
    public async Task Get_SpecificDeck_ReturnsSuccessAndCorrectData()
    {
        var response = await _client.GetAsync("api/Decks/1");

        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();
        var deck = JsonConvert.DeserializeObject<Deck>(content);

        Assert.NotNull(deck);
        Assert.Equal(1, deck.Id);
    }
    [Fact]
    public async Task Get_SpecificDeck_ReturnsFailure_NotFound()
    {
        var response = await _client.GetAsync("api/Decks/3");

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        
    }
    [Fact]
    public async Task Get_GetCommunityDecks_ReturnsSuccess()
    {
        var response = await _client.GetAsync("api/Decks/Community");
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();

        var decks = JsonConvert.DeserializeObject<List<Deck>>(content);
        Assert.NotNull(decks);
        Assert.True(decks.Any());
    }
    
    [Fact]
    public async Task Get_GetPersonalDecks_ReturnsSuccess()
    {
        var response = await _client.GetAsync("api/Decks/Personal/1");
        response.EnsureSuccessStatusCode();
        
        var content = await response.Content.ReadAsStringAsync();
        
        var decks = JsonConvert.DeserializeObject<List<Deck>>(content);
        Assert.NotNull(decks);
        Assert.True(decks.Any());
    }
    
    [Fact]
    public async Task Get_GetPersonalDecks_ReturnsFailure()
    {
        var response = await _client.GetAsync("api/Decks/Personal/3");
        response.EnsureSuccessStatusCode();
        
        var content = await response.Content.ReadAsStringAsync();
        
        var decks = JsonConvert.DeserializeObject<List<Deck>>(content);
        Assert.Empty(decks);
        Assert.False(decks.Any());
    }
    
    [Fact]
    public async Task Post_CreateDeck_ReturnsSuccessAndCorrectData()
    {
        var newDeck = new CreateDeckDTO { Name = "New Test Deck", Description = "Description for new test deck", IsPersonal = true, CreatorId = 1 };
        var jsonContent = new StringContent(JsonConvert.SerializeObject(newDeck), Encoding.UTF8, "application/json");

        var response = await _client.PostAsync("api/Decks", jsonContent);

        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();
        var createdDeck = JsonConvert.DeserializeObject<Deck>(content);

        Assert.NotNull(createdDeck);
        Assert.Equal(newDeck.Name, createdDeck.Name);
    }
    [Fact]
    public async Task Post_CreateDeck_ReturnsBadRequestForInvalidData()
    {
        var invalidDeck = new CreateDeckDTO { Description = "Invalid deck without name", IsPersonal = true, CreatorId = 1 };
        var jsonContent = new StringContent(JsonConvert.SerializeObject(invalidDeck), Encoding.UTF8, "application/json");

        var response = await _client.PostAsync("api/Decks", jsonContent);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }
    [Fact]
    public async Task Put_UpdateDeck_ReturnsSuccess()
    {
        var updatedDeck = new CreateDeckDTO { Name = "Updated Test Deck", Description = "Updated description", IsPersonal = false };
        var jsonContent = new StringContent(JsonConvert.SerializeObject(updatedDeck), Encoding.UTF8, "application/json");

        var response = await _client.PutAsync("api/Decks/1", jsonContent);

        response.EnsureSuccessStatusCode();

        // Optionally, retrieve the deck to confirm updates
        var getResponse = await _client.GetAsync("api/Decks/1");
        getResponse.EnsureSuccessStatusCode();
        var content = await getResponse.Content.ReadAsStringAsync();
        var deck = JsonConvert.DeserializeObject<Deck>(content);

        Assert.Equal(updatedDeck.Name, deck.Name);
    }
    [Fact]
    public async Task Put_UpdateDeck_ReturnsNotFoundForNonExistentDeck()
    {
        // Assuming there is no deck with ID 999
        var updatedDeck = new CreateDeckDTO { Name = "Updated Test Deck", Description = "Updated description", IsPersonal = false };
        var jsonContent = new StringContent(JsonConvert.SerializeObject(updatedDeck), Encoding.UTF8, "application/json");

        var response = await _client.PutAsync("api/Decks/999", jsonContent);

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
    [Fact]
    public async Task Delete_Deck_ReturnsSuccess()
    {
        var response = await _client.DeleteAsync("api/Decks/2");

        response.EnsureSuccessStatusCode();

        // Optionally, try to retrieve the deleted deck to confirm deletion
        var getResponse = await _client.GetAsync("api/Decks/2");
        Assert.Equal(HttpStatusCode.NotFound, getResponse.StatusCode);
    }
    [Fact]
    public async Task Delete_Deck_ReturnsNotFoundForNonExistentDeck()
    {
        // Assuming there is no deck with ID 999
        var response = await _client.DeleteAsync("api/Decks/999");

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
    [Fact]
    public async Task GetAllUsers_ReturnsSuccessAndCorrectData()
    {
        var response = await _client.GetAsync("api/Users");

        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        var users = JsonConvert.DeserializeObject<List<User>>(content);

        Assert.NotNull(users);
    }
    [Fact]
    public async Task AddUser_ReturnsSuccessAndCorrectData()
    {
        var newUser = new CreateUserDTO { Email = "test@test.com", UserName = "testuser", Password = "password123" };
        var jsonContent = new StringContent(JsonConvert.SerializeObject(newUser), Encoding.UTF8, "application/json");

        var response = await _client.PostAsync("api/Users/Add", jsonContent);

        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        var user = JsonConvert.DeserializeObject<User>(content);

        Assert.Equal(newUser.Email, user.Email);
    }
    [Fact]
    public async Task GetUserById_InvalidId_ReturnsNoContent()
    {
        var response = await _client.GetAsync("api/Users/GetUserById/999");

        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
    }
    [Fact]
    public async Task AddCard_ReturnsSuccessAndCorrectData()
    {
        var newCard = new CreateCardDTO { Front = "Test Front", Back = "Test Back", DeckId = 1 };
        var jsonContent = new StringContent(JsonConvert.SerializeObject(newCard), Encoding.UTF8, "application/json");

        var response = await _client.PostAsync("api/Card", jsonContent);

        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        var card = JsonConvert.DeserializeObject<Card>(content);

        Assert.Equal(newCard.Front, card.Front);
    }
    [Fact]
    public async Task UpdateCard_ReturnsSuccess()
    {
        var updatedCard = new UpdateCardDTO { Front = "Updated Front", Back = "Updated Back" };
        var jsonContent = new StringContent(JsonConvert.SerializeObject(updatedCard), Encoding.UTF8, "application/json");

        var response = await _client.PutAsync("api/Card/1", jsonContent);

        response.EnsureSuccessStatusCode();
        // Optionally, retrieve the card to confirm updates
    }
    [Fact]
    public async Task DeleteCard_InvalidId_ReturnsBadRequest()
    {
        var response = await _client.DeleteAsync("api/Card/999");

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }
}