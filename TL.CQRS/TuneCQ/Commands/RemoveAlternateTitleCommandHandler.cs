using LanguageExt;
using MediatR;
using TL.Repository;
using static LanguageExt.Prelude;

namespace TL.CQRS.TuneCQ.Commands;

public class RemoveAlternateTitleCommand : IRequest<Option<bool>>
{
    public int Id { get; set; }
    public string AlternateTitle { get; }

    public RemoveAlternateTitleCommand(int id, string alternateTitle)
    {
        Id = id;
        AlternateTitle = alternateTitle;
    }
}
public class RemoveAlternateTitleCommandHandler : 
    IRequestHandler<RemoveAlternateTitleCommand, Option<bool>>
{
    private readonly ITuneRepository _tuneRepository;

    public RemoveAlternateTitleCommandHandler(ITuneRepository tuneRepository)
    {
        _tuneRepository = tuneRepository;
    }

    public async Task<Option<bool>> Handle
        (RemoveAlternateTitleCommand command, CancellationToken cancellationToken)
    {
        
        var tune = await _tuneRepository.FindAsync(command.Id);
        var possible = Some(command.AlternateTitle);
        
        var result =
            from t in tune
            from p in possible
            select t.RemoveAlternateTitle(t.AlternateTitles.FirstOrDefault(x => x.Value == p));
        
        ignore( result.Map(async x => await _tuneRepository.SaveAsync()));
        
        return result;
    }
}