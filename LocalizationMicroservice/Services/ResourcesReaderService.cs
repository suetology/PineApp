using System.Text.Json;

namespace LocalizationMicroservice.Services;

public class ResourcesReaderService : IResourcesReaderService
{
    private readonly string _resourcesPath;

    public ResourcesReaderService(string resourcesPath)
    {
        _resourcesPath = resourcesPath;
    }

    public Dictionary<string, string> GetStringsFromResource(string culture)
    {
        var filepath = _resourcesPath + culture + ".json";

        if (!File.Exists(filepath))
            throw new FileNotFoundException("Unknown culture");
        
        var jsonString = File.ReadAllText(filepath);
        var strings = JsonSerializer.Deserialize<Dictionary<string, string>>(jsonString);

        if (strings == null)
            throw new FileLoadException("File '" + filepath + "' is corrupted");

        return strings;
    }
}
