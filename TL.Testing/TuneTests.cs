using LanguageExt;
using TL.Domain;
using TL.Domain.ValueObjects.TuneValueObjects;

namespace TL.Testing;
using Xunit;


public class TuneTests
{
    [Fact]
    public void Valid_Tune_Test()
    {
        var title = TuneTitle.Create("Cup of Tea");
        var composer = TuneComposer.Create("Traditional");
        var type = TuneTypeValueObj.Create("Reel");
        var key = TuneKeyValueObj.Create("Emin");

        var tune = (title, composer, type, key)
            .Apply(Tune.Create);

        tune.Match(
            Succ: t =>
            {
                Assert.True(t.Title.Value == "Cup of Tea");
                Assert.True(t.Composer.Value == "Traditional");
                Assert.True(t.TuneType.Value == "Reel");
                Assert.True(t.TuneKey.Value == "Emin");
            },
            Fail: _ => Assert.True(false, "should not get here"));
    }
    
    [Fact]
    public void Invalid_TuneTitle_Test()
    {
        var title = TuneTitle.Create("");
        var composer = TuneComposer.Create("Traditional");
        var type = TuneTypeValueObj.Create("Reel");
        var key = TuneKeyValueObj.Create("Emin");

        var tune = (title, composer, type, key)
            .Apply(Tune.Create);
        
        Assert.True(tune.IsFail);

        tune.Match(
            Succ: _ => Assert.True(false, "should not get here"),
            Fail: errors =>
            {
                Assert.True(errors.Count == 1);
                Assert.True(errors.Head == "Title cannot be empty");
            });
    }
    
    [Fact]
    public void Invalid_TuneTitle_And_Composer_Test()
    {
        var title = TuneTitle.Create("");
        var composer = TuneComposer.Create("");
        var type = TuneTypeValueObj.Create("Reel");
        var key = TuneKeyValueObj.Create("Emin");

        var tune = (title, composer, type, key)
            .Apply(Tune.Create);
        
        Assert.True(tune.IsFail);

        tune.Match(
            Succ: _ => Assert.True(false, "should not get here"),
            Fail: errors =>
            {
                Assert.True(errors.Count == 2);
                Assert.True(errors.Head.Message == "Title cannot be empty");
                Assert.True(errors.Tail.Head.Message == "Composer name cannot be empty");
            });
    }
    

    [Fact]
    public void Valid_Alternate_Title_Test()
    {
        var title = TuneTitle.Create("Cup of Tea");
        var composer = TuneComposer.Create("Traditional");
        var type = TuneTypeValueObj.Create("Reel");
        var key = TuneKeyValueObj.Create("Emin");
        var altTitle = TuneTitle.Create("New Title");

        var tune = (title, composer, type, key)
            .Apply(Tune.Create);

        var result =
            from a in altTitle
            from t in tune
            select t.AddAlternateTitle(a);

        tune.Match(
            Succ: t =>
            {
                result.Match(
                    Succ: _ => Assert.True(t.AlternateTitles.Count == 1),
                    Fail: _ => Assert.True(false, "alt title is valid"));
            },
            Fail: _ => Assert.True(false, "tune is valid"));
    }

    [Fact]
    public void Invalid_AlternateTitle_Test()
    {
        var title = TuneTitle.Create("Cup of Tea");
        var composer = TuneComposer.Create("Traditional");
        var type = TuneTypeValueObj.Create("Reel");
        var key = TuneKeyValueObj.Create("Emin");
        var altTitle = TuneTitle.Create("");

        var tune = (title, composer, type, key)
            .Apply(Tune.Create);

        var result =
            from a in altTitle
            from t in tune
            select t.AddAlternateTitle(a);

        tune.Match(
            Succ: t =>
            {
                result.Match(
                    Succ: _ => Assert.True(false, "alt title is invalid"),
                    Fail: _ => Assert.True(t.AlternateTitles.Count == 0));
            },
            Fail: _ => Assert.True(false, "tune is valid"));
    }

    [Fact]
    public void Valid_Update_Tune_Test()
    {
        var title = TuneTitle.Create("Cup of Tea");
        var composer = TuneComposer.Create("Traditional");
        var type = TuneTypeValueObj.Create("Reel");
        var key = TuneKeyValueObj.Create("Emin");
        
        var originalTune = (title, composer, type, key)
            .Apply(Tune.Create);
        
        var newTitle = TuneTitle.Create("Green Grove");
        var newComposer = TuneComposer.Create("John Doyle");
        var newType = TuneTypeValueObj.Create("Jig");
        var newKey = TuneKeyValueObj.Create("Amin");

        var updatedTune =
            from t in newTitle
            from c in newComposer
            from tt in newType
            from k in newKey
            from ot in originalTune
            select ot.Update(t, c, tt, k);

        originalTune.Match(
            Succ: ot =>
            {
                updatedTune.Match(Succ: _ =>
                    {
                        Assert.True(ot.Title.Value == "Green Grove");
                        Assert.True(ot.Composer.Value == "John Doyle");
                        Assert.True(ot.TuneType.Value == "Jig");
                        Assert.True(ot.TuneKey.Value == "Amin");
                    },
                    Fail: _ => Assert.True(false, "update valid"));
            },
            Fail: _ => Assert.True(false, "original tune valid"));
    }

    [Fact]
    public void Invalid_Update_Tune_Test()
    {
        var title = TuneTitle.Create("Cup of Tea");
        var composer = TuneComposer.Create("Traditional");
        var type = TuneTypeValueObj.Create("Reel");
        var key = TuneKeyValueObj.Create("Emin");
        
        var originalTune = (title, composer, type, key)
            .Apply(Tune.Create);
        
        var newTitle = TuneTitle.Create("");
        var newComposer = TuneComposer.Create("");
        var newType = TuneTypeValueObj.Create("");
        var newKey = TuneKeyValueObj.Create("");

        var updatedTune =
            from t in newTitle
            from c in newComposer
            from tt in newType
            from k in newKey
            from ot in originalTune
            select ot.Update(t, c, tt, k);
        
        Assert.True(updatedTune.IsFail);

        originalTune.Match(
            Succ: ot =>
            {
                updatedTune.Match(
                    Succ: _ => Assert.True(false, "invalid update"),
                    Fail: errors => Assert.True(errors.Count > 0));
            },
            Fail: _ => Assert.True(false, "original tune valid"));
    }
}