using Microsoft.IdentityModel.Tokens;

namespace PineAPP.Extensions;

public static class StringExtensions
{
    public static bool ContainsAnyOfChars(this string str, IEnumerable<char> charsToCheck)
    {
        if (charsToCheck == null || str.IsNullOrEmpty())
            return false;
        
        var toCheck = charsToCheck.ToList();

        return toCheck.Any(c => str.Contains(c));
    }
}