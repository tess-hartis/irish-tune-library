namespace TL.Domain.Validators;

public static class StringValidators
{
    public static bool IsValidNameOrTitle(this string nameOrTitle)
    {
        if (string.IsNullOrWhiteSpace(nameOrTitle))
            return false;

        if (nameOrTitle.Length > 75)
            return false;

        if (nameOrTitle.Length < 2)
            return false;

        return true;
    }
}