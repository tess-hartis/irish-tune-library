using Microsoft.EntityFrameworkCore;
using TL.Data;
using TL.Domain;
using TL.Domain.Exceptions;
using TL.Domain.ValueObjects.TrackTuneValueObjects;

namespace TL.Repository;

public interface ITuneTrackService
{
    Task<TrackTune> AddExistingTuneToTrack(Track track, Tune tune, TrackTune trackTune);
    Task<Track> RemoveTuneFromTrack(Track track, TrackTune trackTune);
    Task<IEnumerable<Track>> FindTracksByTune(Tune tune);
    Task<IEnumerable<TrackTune>> GetTrackTunes(Track track);
}

public class TuneTrackService : ITuneTrackService
{
    private readonly TuneLibraryContext _context;
    private readonly ITrackTuneRepository _trackTuneRepository;
    

    public TuneTrackService(TuneLibraryContext context, ITrackTuneRepository trackTuneRepository)
    {
        _context = context;
        _trackTuneRepository = trackTuneRepository;
       
    }

    private async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
    
    
    public async Task<TrackTune> AddExistingTuneToTrack(Track track, Tune tune, TrackTune trackTune)
    {
        track.AddTune(trackTune);
        trackTune.SetTrackId(track.Id);
        trackTune.SetTuneId(tune.Id);
        trackTune.SetTitle(tune.Title.Value);
        
        await SaveChangesAsync();
        return trackTune;
    }

    public async Task<Track> RemoveTuneFromTrack(Track track, TrackTune trackTune)
    {
        track.RemoveTune(trackTune);
        await SaveChangesAsync();
        return track;
    }
    

    public async Task<IEnumerable<Track>> FindTracksByTune(Tune tune)
    {
        var trackTunes = await _trackTuneRepository
            .GetByWhere(x => x.TuneId == tune.Id).ToListAsync();

        var tracks = trackTunes.Select(t => t.Track);

        return tracks;
    }

    public async Task<IEnumerable<TrackTune>> GetTrackTunes(Track track)
    {
        return track.TrackTunes;
    }
}