namespace TL.Common;

public static class StringValidator
{
    public static bool IsValidNameOrTitle(this string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return false;
        }

        if (name.Length >= 20 && name.Length < 2)
        {
            return false;
        }

        if (name.StartsWith(" "))
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}