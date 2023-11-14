using PineAPP.Models;

namespace PineAPP.Services.Repositories;

public interface IUsersRepository : IRepository<User>
{
    User GetByEmail(string email);
    bool UserExistsWithUsername(string username);
    bool UserExistsWithEmail(string email);
}