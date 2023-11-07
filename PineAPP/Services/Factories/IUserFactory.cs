using PineAPP.Models;

namespace PineAPP.Services.Factories;

public interface IUserFactory
{
    User CreateUser(string email, string password, string username);
}