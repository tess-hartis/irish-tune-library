using TL.Common;

namespace TL.Domain;

public class Tune
{
    public int Id { get; set; }
    public string Title { get; set; }
    public List<string> AlternateTitles { get; set; }
    
    public TuneTypeEnum TuneType;
    
    public TuneKeyEnum TuneKey;
    public string Composer { get; set; }
    public DateOnly DateAdded { get; set; }
    public List<Track> FeaturedOn { get; set; }
    
    public Tune(string title, TuneTypeEnum tuneType, TuneKeyEnum tuneKey, 
        string composer)
    {
        if (title.IsValidNameOrTitle() & composer.IsValidNameOrTitle())
        {
            Id = new int();
            Title = title;
            TuneType = tuneType;
            TuneKey = tuneKey;
            Composer = composer;
            DateAdded = DateOnly.FromDateTime(DateTime.Now);
            AlternateTitles = new List<string>();
            FeaturedOn = new List<Track>();
        }
        else
        {
            throw new FormatException();
        }
    }

    public Tune()
    {
        
    }

    public void AddAlternateTitles(List<string> tunes)
    {
        foreach (var title in tunes)
        {
            if (title.IsValidNameOrTitle())
            {
                AlternateTitles.Add(title);
            }
            else
            {
                throw new FormatException();
            }
        }
    }
}