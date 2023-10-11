using Microsoft.IdentityModel.Tokens;

namespace PineAPP.Extensions;

public static class StringExtensions
{
    public static bool ContainsAnyOfChars(this string str, List<char> charsToCheck)
    {
        if (str.IsNullOrEmpty() || charsToCheck == null || !charsToCheck.Any())
            return false;

        foreach (var c in charsToCheck)
            if (str.Contains(c))
                return true;

        return false;
    }
}