using System.Net;
using System.Net.Mail;
using Microsoft.AspNetCore.Mvc;
using NotificationsService.Data;
using NotificationsService.Services;

namespace NotificationsService.Controllers;

[ApiController]
[Route("Notifications")]
public class NotificationsController : ControllerBase
{
    private readonly ApplicationDbContext _db;
    private readonly IEmailService _emailService;

    public NotificationsController(
        ApplicationDbContext db, 
        IEmailService emailService)
    {
        _db = db;
        _emailService = emailService;
    }
    
    [HttpPost]
    public ActionResult BroadcastNotification([FromBody] string message)
    {
        try
        {
            foreach (var user in _db.Users)
                _emailService.SendEmail(user.Email, message);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }

        return Ok();
    }
}