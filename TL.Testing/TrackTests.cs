using LanguageExt;
using TL.Domain;
using TL.Domain.ValueObjects.TrackValueObjects;
using Xunit;

namespace TL.Testing;

public class TrackTests
{
    [Fact]
    public void Valid_Track_Test()
    {
        var title = TrackTitle.Create("The First Song");
        var number = TrkNumber.Create(1);

        var track = (title, number)
            .Apply(Track.Create);

        track.Match(
            Succ: t =>
            {
                Assert.True(t.Title.Value == "The First Song");
                Assert.True(t.TrkNumber.Value == 1);
            },
            Fail: _ => Assert.True(false, "should not get here"));
    }

    [Fact]
    public void Invalid_Track_Test()
    {
        var title = TrackTitle.Create("");
        var number = TrkNumber.Create(0);

        var track = (title, number)
            .Apply(Track.Create);

        track.Match(
            Succ: _ => Assert.True(false, "invalid track"),
            Fail: errors =>
            {
                Assert.True(errors.Count == 2);
                Assert.True(errors.Head.Message == "Track title cannot be empty");
                Assert.True(errors.Tail.Head.Message == "Invalid track number");
            });
    }

    [Fact]
    public void Valid_Update_Track_Test()
    {
        var title = TrackTitle.Create("The First Song");
        var number = TrkNumber.Create(1);

        var originalTrack = (title, number)
            .Apply(Track.Create);
        
        var newTitle = TrackTitle.Create("The Second Song");
        var newNumber = TrkNumber.Create(2);

        var newTrack =
            from t in newTitle
            from n in newNumber
            from ot in originalTrack
            select ot.Update(t, n);

        originalTrack.Match(
            Succ: t =>
                newTrack.Match(Succ: _ =>
                    {
                        Assert.True(t.Title.Value == "The Second Song");
                        Assert.True(t.TrkNumber.Value == 2);
                    },
                    Fail: _ => Assert.True(false, "valid track")),
            Fail: _ => Assert.True(false, "valid track"));
    }

    [Fact]
    public void Invalid_Update_Track_Test()
    {
        var title = TrackTitle.Create("The First Song");
        var number = TrkNumber.Create(1);

        var originalTrack = (title, number)
            .Apply(Track.Create);
        
        var newTitle = TrackTitle.Create("");
        var newNumber = TrkNumber.Create(0);

        var newTrack =
            from t in newTitle
            from n in newNumber
            from ot in originalTrack
            select ot.Update(t, n);
        
        Assert.True(newTrack.IsFail);

        originalTrack.Match(Succ: _ =>
                newTrack.Match(
                    Succ: _ => Assert.True(false, "not valid"),
                    Fail: errors =>
                    {
                        Assert.True(errors.Count > 0);
                    }),
            Fail: _ => Assert.True(false, "original track valid"));
    }
}