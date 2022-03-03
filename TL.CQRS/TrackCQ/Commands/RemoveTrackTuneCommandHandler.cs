using LanguageExt;
using MediatR;
using TL.Repository;
using static LanguageExt.Prelude;
using Unit = LanguageExt.Unit;

 namespace TL.CQRS.TrackCQ.Commands;

 public class RemoveTrackTuneCommand : IRequest<Option<Unit>>
 {
     public int TrackTuneId { get; set; }

     public RemoveTrackTuneCommand(int trackTuneId)
     {
         TrackTuneId = trackTuneId;
     }
 }
 public class RemoveTrackTuneCommandHandler : IRequestHandler<RemoveTrackTuneCommand, Option<Unit>>
 {
     private readonly ITrackTuneRepository _trackTuneRepository;

     public RemoveTrackTuneCommandHandler(ITrackTuneRepository trackTuneRepository)
     {
         _trackTuneRepository = trackTuneRepository;
     }

     public async Task<Option<Unit>> Handle(RemoveTrackTuneCommand command, CancellationToken cancellationToken)
     {
         var trackTune = await _trackTuneRepository.FindAsync(command.TrackTuneId);
         ignore(trackTune.Map(async t => await _trackTuneRepository.DeleteAsync(t)));
         return trackTune.Map(t => unit);
     }
 }