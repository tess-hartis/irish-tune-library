using Microsoft.EntityFrameworkCore;
using TL.Data;
using TL.Domain;
using TL.Domain.Exceptions;
using TL.Domain.ValueObjects.TrackTuneValueObjects;

namespace TL.Repository;

public interface ITuneTrackService
{
    Task<TrackTune> AddExistingTuneToTrack(int trackId, int tuneId, TrackTune trackTune);
    Task<Track> RemoveTuneFromTrack(int trackId, int tuneId);
    Task<IEnumerable<Track>> FindTracksByTune(int tuneId);
    Task<IEnumerable<TrackTune>> GetTrackTunes(int trackId);
}

public class TuneTrackService : ITuneTrackService
{
    private readonly TuneLibraryContext _context;
    private readonly ITuneRepository _tuneRepository;
    private readonly ITrackTuneRepository _trackTuneRepository;
    private readonly ITrackRepository _trackRepository;

    public TuneTrackService(TuneLibraryContext context,
        ITuneRepository tuneRepository,
        ITrackTuneRepository trackTuneRepository, 
        ITrackRepository trackRepository)
    {
        _context = context;
        _tuneRepository = tuneRepository;
        _trackTuneRepository = trackTuneRepository;
        _trackRepository = trackRepository;
    }

    private async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
    
    
    public async Task<TrackTune> AddExistingTuneToTrack(int trackId, int tuneId, TrackTune trackTune)
    {
        var track = await _trackRepository.FindAsync(trackId);
        var tune = await _tuneRepository.FindAsync(tuneId);
        track.AddTune(trackTune);
        trackTune.SetTrackId(track.Id);
        trackTune.SetTuneId(tune.Id);
        trackTune.SetTitle(tune.Title.Value);
        
        await SaveChangesAsync();
        return trackTune;
    }

    public async Task<Track> RemoveTuneFromTrack(int trackId, int tuneId)
    {
        var track = await _trackRepository.FindAsync(trackId);
        var trackTune = await FindTrackTune(track, tuneId);
        track.RemoveTune(trackTune);
        await SaveChangesAsync();
        return track;
    }

    private Task<TrackTune> FindTrackTune(Track track, int tuneId)
    {
        var trackTune = track.TrackTunes.FirstOrDefault(x => x.TuneId == tuneId);
        if (trackTune == null)
            throw new InvalidEntityException("The specified tune was not found on the track");
        
        return Task.FromResult(trackTune);
    }

    public async Task<IEnumerable<Track>> FindTracksByTune(int tuneId)
    {
        var tune = await _tuneRepository.FindAsync(tuneId);
        var trackTunes = await _trackTuneRepository
            .GetByWhere(x => x.TuneId == tune.Id).ToListAsync();

        var tracks = new List<Track>();

        foreach (var entity in trackTunes)
        {
            var track = await _trackRepository.FindAsync(entity.TrackId);
            tracks.Add(track);
        }

        return tracks;
    }

    public async Task<IEnumerable<TrackTune>> GetTrackTunes(int trackId)
    {
        var track = await _trackRepository.FindAsync(trackId);
        var list = new List<TrackTune>();
        foreach (var trackTune in track.TrackTunes)
        {
            list.Add(trackTune);
        }
    
        return list;
        
    }
}