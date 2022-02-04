using Microsoft.EntityFrameworkCore;
using TL.Data;
using TL.Domain;

namespace TL.Repository;

public interface ITuneTrackService
{
    Task AddExistingTuneToTrack(int trackId, int tuneId);
    Task RemoveTuneFromTrack(int trackId, int tuneId);
    Task<IEnumerable<Track>> FindTracksByTune(int tuneId);
}

public class TuneTrackService : ITuneTrackService
{
    private readonly TuneLibraryContext _context;
    private readonly ITuneRepository _tuneRepository;
    private readonly ITrackRepository _trackRepository;

    public TuneTrackService(TuneLibraryContext context, ITuneRepository tuneRepository, ITrackRepository trackRepository)
    {
        _context = context;
        _tuneRepository = tuneRepository;
        _trackRepository = trackRepository;
    }

    private async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
    
    
    public async Task AddExistingTuneToTrack(int trackId, int tuneId)
    {
        var track = await _trackRepository.FindAsync(trackId);
        var tune = await _tuneRepository.FindAsync(tuneId);
        track.AddTune(tune);
        await SaveChangesAsync();
    }

    public async Task RemoveTuneFromTrack(int trackId, int tuneId)
    {
        var track = await _trackRepository.FindAsync(trackId);
        var tune = await _tuneRepository.FindAsync(tuneId);
        track.RemoveTune(tune);
        await SaveChangesAsync();
    }
    
    public async Task<IEnumerable<Track>> FindTracksByTune(int tuneId)
    {
        var tune = await _tuneRepository.FindAsync(tuneId);
        var tracks = await _trackRepository
            .GetByWhere(x => x.TuneList.Contains(tune)).ToListAsync();
        return tracks;
    }
}