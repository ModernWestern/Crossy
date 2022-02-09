using System;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Text.RegularExpressions;

public static class StringExtensions
{
    public static string RemoveAccentsAndWhiteSpaces(this string str)
    {
        return str.RemoveAccents().RemoveWhiteSpaces();
    }

    public static string RemoveWhiteSpaces(this string str)
    {
        return str.ToCharArray().Where(c => !char.IsWhiteSpace(c)).Select(c => c.ToString()).Aggregate((a, b) => a + b);
    }

    public static string RemoveAccents(this string str)
    {
        StringBuilder sb = new StringBuilder();

        var charArray = str.Normalize(NormalizationForm.FormD).ToCharArray();

        foreach (char c in charArray)
        {
            if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
            {
                sb.Append(c);
            }
        }

        return sb.ToString();
    }

    // https://stackoverflow.com/questions/5796383/insert-spaces-between-words-on-a-camel-cased-token
    /// <summary>
    /// {Ll} is Unicode Character Category "Letter lowercase" (as opposed to {Lu} "Letter uppercase").
    /// P is a negative match, while p is a positive match, so \P{Ll} is literally "Not lowercase" and p{Ll} is "Lowercase".
    /// So this regex splits on two patterns. 1: "Uppercase, Uppercase, Lowercase" (which would match the MMa in IBMMake and result in IBM Make), and 2. "Lowercase, Uppercase" (which would match on the eS in MakeStuff). That covers all camelcase breakpoints.
    /// TIP: Replace space with hyphen and call ToLower to produce HTML5 data attribute names.
    /// </summary>
    public static string DeconstructCase(this string str)
    {
        return Regex.Replace(Regex.Replace(str, @"(\P{Ll})(\P{Ll}\p{Ll})", "$1 $2"), @"(\p{Ll})(\P{Ll})", "$1 $2");
    }

    public static bool ContainsAny(this string str, StringComparison comparisonType, params string[] keywords)
    {
        return keywords.Any(keyword => str.IndexOf(keyword, comparisonType) >= 0);
    }

    public static bool ContainsAny(this string str, params string[] keywords)
    {
        return keywords.Any(keyword => str.IndexOf(keyword, StringComparison.CurrentCultureIgnoreCase) >= 0);
    }
}