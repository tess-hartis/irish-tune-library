using LanguageExt;
using TL.Domain.ValueObjects.AlbumValueObjects;

namespace TL.Domain;

public class Album
{
    public int Id { get; }
    public AlbumTitle Title { get; private set; }
    public AlbumYear Year { get; private set; }
   
    
    private List<Artist> _artists = new List<Artist>();
    public IEnumerable<Artist> Artists => _artists;
    
    private List<Track> _tracks = new List<Track>();
    public IEnumerable<Track> TrackListing => _tracks;
    
    
    public static Album Create(AlbumTitle title, AlbumYear year)
    {
        var album = new Album()
        {
            Title = title,
            Year = year
        };
        
        return album;
    }

    private Album(){ }
    
    public Unit AddArtist(Artist artist)
    {
        _artists.Add(artist);
        return Unit.Default;
    }

    public bool RemoveArtist(Artist artist)
    {
       return _artists.Remove(artist);
    }

    public Album Update(AlbumTitle title, AlbumYear year)
    {
        Title = title;
        Year = year;
        return this;
    }

    public bool AddTrack(Track track)
    {
        var duplicateTrackNumber = _tracks.Exists(x =>
            x.TrkNumber.Value == track.TrkNumber.Value);

        if (duplicateTrackNumber)
            return false;

        _tracks.Add(track);
        track.SetAlbum(this);
        return true;
    }




}