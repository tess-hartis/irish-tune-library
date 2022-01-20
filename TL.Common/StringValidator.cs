namespace TL.Common;

public static class StringValidator
{
    public static bool IsValidNameOrTitle(this string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return false;
        }

        if (name.Length >= 75 && name.Length < 75)
        {
            return false;
        }

        if (name.StartsWith(" "))
        {
            return false;
        } 
        
        return true;
        
    }
}