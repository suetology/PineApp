namespace LocalizationMicroservice.Services;

public class LocalizationService : ILocalizationService
{
    private readonly Dictionary<string, Dictionary<string, string>> _resourceCache = new ();
    private readonly IResourcesReaderService _resourcesReaderService;
    
    public LocalizationService(IResourcesReaderService resourcesReaderService)
    {
        _resourcesReaderService = resourcesReaderService;
    }

    public Dictionary<string, string> GetStrings(string culture)
    {
        if (_resourceCache.ContainsKey(culture))
            return _resourceCache[culture];

        var strings = _resourcesReaderService.GetStringsFromResource(culture);
        _resourceCache.Add(culture, strings);

        return strings;
    }
}