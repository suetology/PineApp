namespace LocalizationMicroservice.Services;

public interface IResourcesReaderService
{
    Dictionary<string, string> GetStringsFromResource(string culture);
}