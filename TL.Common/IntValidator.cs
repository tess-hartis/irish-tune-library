namespace TL.Common;

public static class IntValidator
{
    public static bool IsValidInt(this int number)
    {
        //checks if all characters are numbers
        foreach (char c in number.ToString())
        {
            if (!Char.IsNumber(c))
            {
                return false;
            }
        }
        
        //checks if number is zero
        if (number < 1)
        {
            return false;
        }

        //checks if the number starts with zero
        // var numberString = number.ToString();
        // if (numberString.StartsWith("0"))
        // {
        //     return false;
        // }

        return true;
    }

    public static bool IsValidYear(this int year)
    {
        //checks if all characters are numbers
        foreach (char c in year.ToString())
        {
            if (!char.IsNumber(c))
            {
                return false;
            }
        }

        if (year > 3000)
        {
            return false;
        }

        return true;
    }
}