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
using PineAPP.Services.Repositories;
using PineAPP.Services.Factories;

namespace PineAPP.Controllers;

[ApiController]
[Route("api/Users")]
public class UsersController : ControllerBase
{
    private readonly ILogger<UsersController> _logger;
    private readonly IUsersRepository _usersRepository;
    private readonly IUserFactory _userFactory;

    public UsersController(
        ILogger<UsersController> logger, 
        IUsersRepository usersRepository, 
        IUserFactory userFactory)
    {
        _logger = logger;
        _usersRepository = usersRepository;
        _userFactory = userFactory;
    }

    [HttpGet]
    public ActionResult GetAllUsers()
    {
        try
        {
            var allUsers = _usersRepository.GetAll();
            return Ok(allUsers);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred in endpoint GetAllUsers");
            return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
        }
    }

    [HttpGet("GetUserById/{userId:int}")]
    public ActionResult GetUserById(int userId)
    {
        try
        {
            var user = _usersRepository.GetById(userId);
            return Ok(user);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred in endpoint GetUserById");
            return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
        }
    }

    [HttpGet("GetUserByEmail/{email}")]
    public ActionResult GetUserByEmail(string email)
    {
        try
        {
            var user = _usersRepository.GetByEmail(email);
            return Ok(user);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred in endpoint GetUserByEmail");
            return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
        }
    }

    [HttpPost("Add")]
    public async Task<ActionResult> AddUser([FromBody] CreateUserDTO createUserDto)
    {
        try
        {
            if (ModelState.IsValid)
            {
                if (createUserDto == null)
                {
                    return BadRequest("CreateUserDto is null.");
                }
            }

            var newUser = _userFactory.CreateUser(createUserDto.Email, createUserDto.Password, createUserDto.UserName);
            
            _usersRepository.Add(newUser);
            await _usersRepository.SaveChangesAsync();

            return Ok(newUser);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred in endpoint AddUser");
            return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
        }
    }
    
}