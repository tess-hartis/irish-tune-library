namespace TL.Common;

public static class IntValidator
{
    public static bool IsValidYear(this int year)
    {
        foreach (char c in year.ToString())
        {
            if (!char.IsNumber(c))
            {
                return false;
            }
        }

        switch (year)
        {
            case > 2100:
                return false;
            case < 1800:
                return false;
        }

        return true;
    }

    public static bool IsValidTrackNumber(this int track)
    {
        foreach (char c in track.ToString())
        {
            if (!char.IsNumber(c))
            {
                return false;
            }
        }
        
        switch (track)
        {
            case < 1:
                return false;
            case > 100:
                return false;
        }

        return true;
    }
}