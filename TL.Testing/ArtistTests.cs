using TL.Domain;
using TL.Domain.ValueObjects.ArtistValueObjects;
using Xunit;

namespace TL.Testing;

public class ArtistTests
{
    [Fact]
    public void Valid_Artist_Test()
    {
        var name = ArtistName.Create("Liz Carroll");
        var artist = name.Map(Artist.Create);

        artist.Match(
            Succ: a => Assert.True(a.Name.Value == "Liz Carroll"),
            Fail: _ => Assert.True(false, "valid artist"));
    }

    [Fact]
    public void Invalid_Artist_Test()
    {
        var name = ArtistName.Create("");
        var artist = name.Map(Artist.Create);

        artist.Match(
            Succ: _ => Assert.True(false, "invalid name"),
            Fail: errors =>
            {
                Assert.True(errors.Count == 1);
                Assert.True(errors.Head.Message == "Artist name cannot be empty");
            });
    }

    [Fact]
    public void Valid_Update_Artist_Test()
    {
        var name = ArtistName.Create("Liz Carroll");
        var originalArtist = name.Map(Artist.Create);

        var newName = ArtistName.Create("John Doyle");

        var newArtist =
            from n in newName
            from a in originalArtist
            select a.Update(n);

        originalArtist.Match(
            Succ: a =>
            {
                newArtist.Match(
                    Succ: _ => Assert.True(a.Name.Value == "John Doyle"),
                    Fail: _ => Assert.True(false, "valid name"));
            },
            Fail: _ => Assert.True(false, "valid artist"));
    }

    [Fact]
    public void Invalid_Update_Artist_Test()
    {
        var name = ArtistName.Create("Liz Carroll");
        var originalArtist = name.Map(Artist.Create);

        var newName = ArtistName.Create("");

        var newArtist =
            from n in newName
            from a in originalArtist
            select a.Update(n);

        originalArtist.Match(
            Succ: a =>
            {
                newArtist.Match(
                    Succ: _ => Assert.True(false, "invalid name"),
                    Fail: errors =>
                    {
                        Assert.True(errors.Count > 0);
                    });
            },
            Fail: _ => Assert.True(false, "valid artist"));
    }
}