using TL.Domain.ValueObjects.TuneValueObjects;
using TL.Domain.ValueObjects.AlbumValueObjects;
using TL.Domain.ValueObjects.ArtistValueObjects;
using TL.Domain.ValueObjects.TrackValueObjects;
using Xunit;

namespace TL.Testing;

public class ValueObjectTests
{
    [Fact]
    public void Valid_TuneTitle_Test()
    {
        var title = TuneTitle.Create("The Nightingale");

        title.Match(
            Succ: t => Assert.True(t.Value == "The Nightingale"),
            Fail: err => Assert.True(false, "should not get here"));
    }

    [Fact]
    public void Invalid_TuneTitle_Test()
    {
        var title = TuneTitle.Create("");
        Assert.True(title.IsFail);

        title.Match(
            Succ: _ => Assert.True(false, "should not get here"),
            Fail: errors => Assert.True(errors.Count == 1));
    }

    [Fact]
    public void Valid_TuneComposer_Test()
    {
        var composer = TuneComposer.Create("Traditional");

        composer.Match(
            Succ: c => Assert.True(c.Value == "Traditional"),
            Fail: err => Assert.True(false, "should not get here"));
    }
    
    [Fact]
    public void Invalid_TuneComposer_Test()
    {
        var composer = TuneComposer.Create("");
        Assert.True(composer.IsFail);

        composer.Match(
            Succ: _ => Assert.True(false, "should not get here"),
            Fail: errors => Assert.True(errors.Count == 1));
    }
    
    [Fact]
    public void Valid_TuneType_Test()
    {
        var type = TuneTypeValueObj.Create("jig");

        type.Match(
            Succ: t => Assert.True(t.Value == "jig"),
            Fail: err => Assert.True(false, "should not get here."));
    }
    
    [Fact]
    public void Invalid_TuneType_Test()
    {
        var type = TuneTypeValueObj.Create("not a valid type");
        Assert.True(type.IsFail);

        type.Match(
            Succ: _ => Assert.True(false, "should not get here"),
            Fail: errors => Assert.True(errors.Count == 1));
    }
    
    [Fact]
    public void Valid_TuneKey_Test()
    {
        var key = TuneKeyValueObj.Create("DMaj");

        key.Match(
            Succ: k => Assert.True(k.Value == "DMaj"),
            Fail: err => Assert.True(false, "should not get here"));
    }
    
    [Fact]
    public void Invalid_TuneKey_Test()
    {
        var key = TuneKeyValueObj.Create("not a valid key");
        Assert.True(key.IsFail);

        key.Match(
            Succ: _ => Assert.True(false, "should not get here"),
            Fail: errors => Assert.True(errors.Count == 1));
    }
    
    [Fact]
    public void Valid_TrackTitle_Test()
    {
        var title = TrackTitle.Create("The Nightingale");

        title.Match(
            Succ: t => Assert.True(t.Value == "The Nightingale"),
            Fail: err => Assert.True(false, "should not get here"));
    }
    
    [Fact]
    public void Invalid_TrackTitle_Test()
    {
        var title = TrackTitle.Create("");
        Assert.True(title.IsFail);

        title.Match(
            Succ: _ => Assert.True(false, "should not get here"),
            Fail: errors => Assert.True(errors.Count == 1));
    }
    
    [Fact]
    public void Valid_TrackNumber_Test()
    {
        var number = TrkNumber.Create(4);

        number.Match(
            Succ: n => Assert.True(n.Value == 4),
            Fail: err => Assert.True(false, "should not get here"));
    }
    
    [Fact]
    public void Invalid_TrackNumber_Test()
    {
        var number = TrkNumber.Create(5000);
        Assert.True(number.IsFail);

        number.Match(
            Succ: _ => Assert.True(false, "should not get here"),
            Fail: errors => Assert.True(errors.Count == 1));
    }
    
    [Fact]
    public void Valid_AlbumTitle_Test()
    {
        var title = AlbumTitle.Create("A New Day");

        title.Match(
            Succ: t => Assert.True(t.Value == "A New Day"),
            Fail: err => Assert.True(false, "should not get here"));
    }
    
    [Fact]
    public void Invalid_AlbumTitle_Test()
    {
        var title = AlbumTitle.Create("");

        title.Match(
            Succ: _ => Assert.True(false, "should not get here"),
            Fail: errors => Assert.True(errors.Count == 1));
    }
    
    [Fact]
    public void Valid_AlbumYear_Test()
    {
        var year = AlbumYear.Create(2000);

        year.Match(
            Succ: y => Assert.True(y.Value == 2000),
            Fail: err => Assert.True(false, "should not get here"));
    }
    
    [Fact]
    public void Invalid_AlbumYear_Test()
    {
        var year = AlbumYear.Create(10000);
        Assert.True(year.IsFail);

        year.Match(
            Succ: _ => Assert.True(false, "should not get here"),
            Fail: errors => Assert.True(errors.Count == 1));
    }
    
    [Fact]
    public void Valid_ArtistName_Test()
    {
        var name = ArtistName.Create("Liz Carroll");

        name.Match(
            Succ: n => Assert.True(n.Value == "Liz Carroll"),
            Fail: err => Assert.True(false, "should not get here"));
    }
    
    [Fact]
    public void Invalid_ArtistName_Test()
    {
        var name = ArtistName.Create("");
        Assert.True(name.IsFail);

        name.Match(
            Succ: _ => Assert.True(false, "should not get here"),
            Fail: errors => Assert.True(errors.Count == 1));
    }
}