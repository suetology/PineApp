using System.Text.RegularExpressions;
using PineAPP.Exceptions;
using PineAPP.Extensions;
using PineAPP.Models;
using PineAPP.Services.Repositories;

namespace PineAPP.Services;

public class UserValidationService : IUserValidationService
{
    private static readonly List<char> InvalidCharacters = new List<char>() { '*', '&', '@', '%', ',', '.', '/' };
    private static readonly string EmailPattern = @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";
    
    private readonly IUsersRepository _usersRepository;

    public UserValidationService(IUsersRepository usersRepository)
    {
        _usersRepository = usersRepository;
    }

    public void ValidateUser(User user)
    {
        // Check if email is valid
        if (!Regex.IsMatch(user.Email, EmailPattern))
            throw new InvalidUserDataException("Provided email is not valid");

        // Check for forbidden character
        if (user.UserName.ContainsAnyOfChars(InvalidCharacters))
            throw new InvalidUserDataException("Username contains forbidden characters.");

        // Check if Username of Email is taken
        if (_usersRepository.UserExistsWithUsername(user.UserName))
            throw new InvalidUserDataException("Username is already taken.");
            
        if (_usersRepository.UserExistsWithEmail(user.Email))
            throw new InvalidUserDataException("An account with this email already exists.");
    }
}