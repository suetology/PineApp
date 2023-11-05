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
using PineAPP.Services;
using PineAPP.Services.Repositories;

namespace PineAPP.Controllers;

[ApiController]
[Route("api/Users")]
public class UsersController : ControllerBase
{
    private readonly ILogger<UsersController> _logger;
    private readonly IUsersRepository _usersRepository;
    private readonly IUserValidationService _userValidationService;

    public UsersController(
        ILogger<UsersController> logger, 
        IUsersRepository usersRepository, 
        IUserValidationService userValidationService)
    {
        _logger = logger;
        _usersRepository = usersRepository;
        _userValidationService = userValidationService;
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

            var newUser = new User()
            {
                Email = createUserDto.Email,
                Password = createUserDto.Password,
                UserName = createUserDto.UserName
            };
            
            _userValidationService.ValidateUser(newUser);
            
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