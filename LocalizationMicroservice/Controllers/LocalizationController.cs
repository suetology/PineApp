using LocalizationMicroservice.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace LocalizationMicroservice.Controllers;

[Route("Localization")]
[ApiController]
public class LocalizationController : ControllerBase
{
    private readonly ILocalizationService _localizationService;

    public LocalizationController(ILocalizationService localizationService)
    {
        _localizationService = localizationService;
    }

    [HttpGet("{culture}")]
    public ActionResult GetLocalizedStrings(string culture)
    {
        try
        {
            var localizedStrings = _localizationService.GetStrings(culture);
            return Ok(localizedStrings);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}