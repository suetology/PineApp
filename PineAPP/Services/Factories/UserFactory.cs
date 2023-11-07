using PineAPP.Models;

namespace PineAPP.Services.Factories;

public class UserFactory : IUserFactory
{
    private readonly IUserValidationService _userValidationService;

    public UserFactory(IUserValidationService userValidationService)
    {
        _userValidationService = userValidationService;
    }

    public User CreateUser(string email, string password, string username)
    {
        var user = new User()
        {
            Email = email,
            Password = password,
            UserName = username
        };

        _userValidationService.ValidateUser(user);

        return user;
    }   
}