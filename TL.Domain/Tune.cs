namespace TL.Domain;

public class Tune
{
    private Tune(){ }
    
    public int Id { get; private set; }
    public string Title { get;  private set; }

    private List<string> _alternateTitles = new List<string>();
    public List<string> AlternateTitles => _alternateTitles;
    public TuneTypeEnum TuneType { get; private set; }
    public TuneKeyEnum TuneKey { get; private set; }
    public string Composer { get; private set; }
    public DateOnly DateAdded { get; private set; }

    public static Tune CreateTune(string title, string composer, TuneTypeEnum type, TuneKeyEnum key)
    {
        var tune = new Tune
        {
            Title = title,
            TuneType = type,
            TuneKey = key,
            Composer = composer,
            DateAdded = DateOnly.FromDateTime(DateTime.Today)
        };

        if (string.IsNullOrWhiteSpace(title))
            throw new FormatException("Tune title cannot be empty");
        
        if (title.Length > 75)
            throw new FormatException("Tune title must be 75 characters or fewer");
        
        if (string.IsNullOrWhiteSpace(composer))
            throw new FormatException("Tune composer cannot be empty");
        
        if (composer.Length > 50)
            throw new FormatException("Tune composer must be 50 characters or fewer");

        if (!Enum.IsDefined(typeof(TuneTypeEnum), type))
            throw new ArgumentException(string.Format("Invalid tune type"));
        
        if (!Enum.IsDefined(typeof(TuneKeyEnum), key))
            throw new ArgumentException(string.Format("Invalid tune key"));
        
        return tune;
    }

    public Tune(string title, TuneTypeEnum type, TuneKeyEnum key, string composer)
    {
        CreateTune(title, composer, type, key);
    }

    public void AddAlternateTitle(string title)
    {
        
        if (string.IsNullOrWhiteSpace(title))
            throw new FormatException("Alternate title cannot be empty");

        if (title.Length > 75)
            throw new FormatException("Alternate title must be 75 characters or fewer");

        _alternateTitles.Add(title);
        
    }

    public void RemoveAlternateTitle(string title)
    {
        if (!_alternateTitles.Contains(title))
            throw new InvalidOperationException("The alternate title was not found");
        
        _alternateTitles.Remove(title);
    }

    public void UpdateTitle(string title)
    {

        if (string.IsNullOrWhiteSpace(title))
            throw new FormatException("Tune title cannot be empty");
        
        if (title.Length > 75)
            throw new FormatException("Tune title must be 75 characters or fewer");
        
        Title = title;
    }

    public void UpdateType(TuneTypeEnum type)
    {
        if (!Enum.IsDefined(typeof(TuneTypeEnum), type))
            throw new ArgumentException(string.Format("Invalid tune type"));

        TuneType = type;
    }

    public void UpdateKey(TuneKeyEnum key)
    {
        if (!Enum.IsDefined(typeof(TuneKeyEnum), key))
            throw new ArgumentException(string.Format("Invalid tune key"));

        TuneKey = key;
    }

    public void UpdateComposer(string composer)
    {
        if (string.IsNullOrWhiteSpace(composer))
            throw new FormatException("Tune composer cannot be empty");
        
        if (composer.Length > 50)
            throw new FormatException("Tune composer must be 50 characters or fewer");

        Composer = composer;
    }
    
    
}