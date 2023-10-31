using System.ComponentModel.DataAnnotations;
using System.Net;
using Azure.Core;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PineAPP.Data;
using PineAPP.Extensions;
using PineAPP.Models;
using PineAPP.Models.Dto;

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

    [HttpGet("GetUserById/{userId:int}")]
    public async Task<ActionResult<ApiResponse<User>>> GetUserById(int userId)
    {
        var user = await _db.Users
            .Where(user => user.UserId == userId)
            .FirstOrDefaultAsync();
        var response = new ApiResponse<User>(HttpStatusCode.OK, isSuccess: true, user);
        
        return Ok(response);
    }

    [HttpGet("GetUserByEmail/{email}")]
    public async Task<ActionResult<ApiResponse<User>>> GetUserByEmail(string email)
    {
        var user = await _db.Users
            .Where(user => user.Email == email)
            .FirstOrDefaultAsync();
        var response = new ApiResponse<User>(HttpStatusCode.OK, isSuccess: true, user);
        return Ok(response);
    }

    [HttpPost("Add")]
    public async Task<ActionResult<ApiResponse<User>>> AddUser([FromBody] CreateUserDTO createUserDto)
    {
        try
        {
            var response = new ApiResponse<User>(
                isSuccess: false,
                statusCode: HttpStatusCode.BadRequest,
                result: null,
                errorMessage: "CreateUserDto is null."
            );

            if (ModelState.IsValid)
            {
                if (createUserDto == null)
                {
                    return BadRequest(response);
                }
            }

            User newUser = new User()
            {
                Email = createUserDto.Email,
                Password = createUserDto.Password,
                UserName = createUserDto.UserName
            };

            // Check if email is valid
            if (!CreateUserDTO.IsEmailValid(newUser.Email))
            {
                response = new ApiResponse<User>(
                    isSuccess: false,
                    statusCode: HttpStatusCode.BadRequest,
                    result: null,
                    errorMessage: "Provided email is not valid"
                );
                return BadRequest(response);
            }

            // Check for forbidden character
            if (newUser.UserName.ContainsAnyOfChars(new List<char>(){'*', '&', '@', '%', ',', '.', '/'}))
            {
                response = new ApiResponse<User>(
                    isSuccess: false,
                    statusCode: HttpStatusCode.BadRequest,
                    result: null,
                    errorMessage: "Username contains forbidden characters."
                );
                return BadRequest(response);
            }

            // Check if Username of Email is taken
            if (_db.Users.Any(entry => entry.UserName == newUser.UserName))
            {
                response = new ApiResponse<User>(
                    isSuccess: false,
                    statusCode: HttpStatusCode.BadRequest,
                    result: null,
                    errorMessage: "Username is already taken."
                );
                return BadRequest(response);
            }
            if (_db.Users.Any(entry => entry.Email == newUser.Email))
            {
                response = new ApiResponse<User>(
                    isSuccess: false,
                    statusCode: HttpStatusCode.BadRequest,
                    result: null,
                    errorMessage: "An account with this email already exists."
                );
                return BadRequest(response);
            }

            
            _db.Users.Add(newUser);
            _db.SaveChanges();

            response = new ApiResponse<User>(
                isSuccess: true,
                statusCode: HttpStatusCode.Created,
                result: newUser, // fix here
                errorMessage: null
            );

            return Ok(response);
        }
        catch (Exception e)
        {
            var response = new ApiResponse<List<Deck>>(
                isSuccess: false,
                statusCode: HttpStatusCode.InternalServerError,
                result: null,
                errorMessage: e.ToString());

            return BadRequest(response);
        }
    }
    
}