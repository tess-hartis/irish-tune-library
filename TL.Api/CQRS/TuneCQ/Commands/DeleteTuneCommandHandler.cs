using LanguageExt;
using static LanguageExt.Prelude;
using MediatR;
using TL.Domain;
using TL.Repository;
using Unit = LanguageExt.Unit;

namespace TL.Api.CQRS.TuneCQ.Commands;

public class DeleteTuneCommand : IRequest<Option<Unit>>
{
    public int Id;

    public DeleteTuneCommand(int id)
    {
        Id = id;
    }
}
public class DeleteTuneCommandHandler : IRequestHandler<DeleteTuneCommand, Option<Unit>>
{
    private readonly ITuneRepository _tuneRepository;

    public DeleteTuneCommandHandler(ITuneRepository tuneRepository)
    {
        _tuneRepository = tuneRepository;
    }

    public async Task<Option<Unit>> Handle(DeleteTuneCommand command, CancellationToken cancellationToken)
    {
        var tune = await _tuneRepository.FindAsync(command.Id);
        ignore(tune.Map(async t => await _tuneRepository.DeleteAsync(t)));
        return tune.Map(t => unit);
        
    }
}