using PineAPP.Data;
using PineAPP.Models;

namespace PineAPP.Services.Repositories;

public class UsersRepository : Repository<User>, IUsersRepository
{
    public UsersRepository(ApplicationDbContext db) 
        : base(db)
    {
    }

    public User GetByEmail(string email)
    {
        var user = _db.Users.FirstOrDefault(user => user.Email == email);

        return user;
    }

    public bool UserExistsWithUsername(string username)
    {
        return _db.Users.Any(entry => entry.UserName == username);
    }

    public bool UserExistsWithEmail(string email)
    {
        return _db.Users.Any(entry => entry.Email == email);
    }
}