using Microsoft.EntityFrameworkCore;
using TL.Data;
using TL.Domain;
using TL.Domain.Exceptions;
using TL.Domain.Validators;

namespace TL.Repository;

public interface ITuneRepository : IGenericRepository<Tune>
{
    Task UpdateTune(int id, string title, string composer, TuneTypeEnum type, TuneKeyEnum key);
    Task AddAlternateTitle(int id, string title);
    Task RemoveAlternateTitle(int id, string title);
}

public class TuneRepository : GenericRepository<Tune>, ITuneRepository
{
    public TuneRepository(TuneLibraryContext context) : base(context)
    {
       
    }

    private readonly TuneValidator _validator = new TuneValidator();
    
    public async Task UpdateTune(int id, string title, string composer, TuneTypeEnum type, TuneKeyEnum key)
    {
        var tune = await FindAsync(id);
        tune.UpdateTitle(title);
        tune.UpdateComposer(composer);
        tune.UpdateType(type);
        tune.UpdateKey(key);
        
        var errors = new List<string>();
        var results = await _validator.ValidateAsync(tune);
        if (results.IsValid == false)
        {
            foreach (var validationFailure in results.Errors)
            {
                errors.Add($"{validationFailure.ErrorMessage}");
            }
            
            throw new InvalidEntityException(string.Join(", ", errors));
        }
        await SaveAsync();
    }

    public async Task<bool> AddAlternateTitle(int id, string title)
    {
        var tune = await FindAsync(id);
        tune.AddAlternateTitle(title);
        return await SaveAsync() > 0;
    }

    public async Task<bool> RemoveAlternateTitle(int id, string title)
    {
        var tune = await FindAsync(id);
        tune.RemoveAlternateTitle(title);
        return await SaveAsync() > 0;
    }
    
    // public async Task<List<Tune>> AnthonyExampleRegex(string query)
    // {
    //     var regex = new Regex("???? + query + ???");
    //     
    //     var list = await _context.Tunes
    //         .Where(x => regex.IsMatch(x.Title)).ToListAsync();
    //     
    //     return list;
    // }
}