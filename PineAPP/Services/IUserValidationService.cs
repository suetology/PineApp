using PineAPP.Models;

namespace PineAPP.Services;

public interface IUserValidationService
{
    public void ValidateUser(User user);
}