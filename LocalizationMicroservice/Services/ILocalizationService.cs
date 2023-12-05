namespace LocalizationMicroservice.Services;

public interface ILocalizationService
{
    Dictionary<string, string> GetStrings(string culture);
}