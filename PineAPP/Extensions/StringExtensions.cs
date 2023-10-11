using Microsoft.IdentityModel.Tokens;

namespace PineAPP.Extensions;

public static class StringExtensions
{
    public static bool ContainsAnyOfChars(this string str, IEnumerable<char> charsToCheck)
    {
        if (charsToCheck == null || str.IsNullOrEmpty())
            return false;
        
        var toCheck = charsToCheck.ToList();

        foreach (var c in toCheck)
            if (str.Contains(c))
                return true;

        return false;
    }
}