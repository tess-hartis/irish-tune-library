using LanguageExt;
using TL.Domain;
using TL.Domain.ValueObjects.AlbumValueObjects;
using Xunit;

namespace TL.Testing;

public class AlbumTests
{
    [Fact]
    public void Valid_Album_Test()
    {
        var title = AlbumTitle.Create("Bothy Band");
        var year = AlbumYear.Create(2000);

        var album = (title, year)
            .Apply(Album.Create);

        album.Match(
            Succ: a =>
            {
                Assert.True(a.Title.Value == "Bothy Band");
                Assert.True(a.Year.Value == 2000);
            },
            Fail: _ => Assert.True(false, "should not get here"));
    }

    [Fact]
    public void Invalid_Album_Test()
    {
        var title = AlbumTitle.Create("");
        var year = AlbumYear.Create(2300);

        var album = (title, year)
            .Apply(Album.Create);

        album.Match(
            Succ: _ => Assert.True(false, "invalid"),
            Fail: errors =>
            {
                Assert.True(errors.Count == 2);
                Assert.True(errors.Head.Message == "Album title cannot be empty");
                Assert.True(errors.Tail.Head.Message == "Invalid year");
            });
    }

    [Fact]
    public void Valid_Update_Album_Test()
    {
        var title = AlbumTitle.Create("Bothy Band");
        var year = AlbumYear.Create(2000);

        var originalAlbum = (title, year)
            .Apply(Album.Create);

        var newTitle = AlbumTitle.Create("Planxty");
        var newYear = AlbumYear.Create(2001);

        var newAlbum =
            from t in newTitle
            from y in newYear
            from a in originalAlbum
            select a.Update(t, y);

        originalAlbum.Match(
            Succ: oa =>
            {
                newAlbum.Match(Succ: _ =>
                    {
                        Assert.True(oa.Title.Value == "Planxty");
                        Assert.True(oa.Year.Value == 2001);
                    },
                    Fail: _ => Assert.True(false, "valid album"));
            },
            Fail: _ => Assert.True(false, "valid album"));
    }

    [Fact]
    public void Invalid_Update_Album_Test()
    {
        var title = AlbumTitle.Create("Bothy Band");
        var year = AlbumYear.Create(2000);

        var originalAlbum = (title, year)
            .Apply(Album.Create);

        var newTitle = AlbumTitle.Create("");
        var newYear = AlbumYear.Create(2001);

        var newAlbum =
            from t in newTitle
            from y in newYear
            from a in originalAlbum
            select a.Update(t, y);
        
        Assert.True(newAlbum.IsFail);

        originalAlbum.Match(
            Succ: oa =>
            {
                newAlbum.Match(
                    Succ: _ => Assert.True(false, "invalid title"), 
                    Fail: errors =>
                    {
                        Assert.True(errors.Count > 0);
                    });
            },
            Fail: _ => Assert.True(false, "valid album"));
    }
}