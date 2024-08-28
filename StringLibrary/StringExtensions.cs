namespace StringLibrary;

public static class StringExtensions
{
    public static bool StartsWithUpper(this string? str) =>
        !string.IsNullOrWhiteSpace(str) && char.IsUpper(str[0]);
}