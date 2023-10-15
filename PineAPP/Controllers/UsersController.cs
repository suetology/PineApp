using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PineAPP.Data;
using PineAPP.Models;

namespace PineAPP.Controllers;

[ApiController]
[Route("api/Users")]
public class UsersController : ControllerBase
{
    private readonly ApplicationDbContext _db;

    public UsersController(ApplicationDbContext db)
    {
        _db = db;
    }

    [HttpGet]
    public async Task<ActionResult<ApiResponse<List<User>>>> GetAllUsers()
    {
        var allUsers = await _db.Users.ToListAsync();
        var response = new ApiResponse<List<User>>(HttpStatusCode.OK, isSuccess: true, allUsers);
        return Ok(response);
    }

    [HttpGet("userById/{userId:int}")]
    public async Task<ActionResult<ApiResponse<User>>> GetUserById(int userId)
    {
        var user = await _db.Users
            .Where(user => user.UserId == userId)
            .FirstOrDefaultAsync();
        var response = new ApiResponse<User>(HttpStatusCode.OK, isSuccess: true, user);
        
        return Ok(response);
    }
}