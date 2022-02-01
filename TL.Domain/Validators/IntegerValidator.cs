namespace TL.Domain.Validators;

public static class IntegerValidator
{
    
    public static bool ValidYear(this int year)
        {
            foreach (char c in year.ToString())
            {
                if (!char.IsNumber(c))
                    return false;
            }
            int num = year;
            return num <= 2100 && num >= 1800;
        }

        public static bool IsValidTrackNum(this int track)
        {
            foreach (char c in track.ToString())
            {
                if (!char.IsNumber(c))
                    return false;
            }
            int num = track;
            return num >= 1 && num <= 100;
        }
    
}