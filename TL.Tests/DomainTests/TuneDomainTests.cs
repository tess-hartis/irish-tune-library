using System;
using NUnit.Framework;
using TL.Domain;

namespace TL.Tests.DomainTests;

[TestFixture]
public class TuneDomainTests
{
    [Test]
    public void CanCreateTune()
    {
        var tune = Tune.CreateTune("Title", "Composer", TuneTypeEnum.Air, TuneKeyEnum.Ador);
        
        Assert.AreEqual("Title", tune.Title);
        Assert.AreEqual("Composer", tune.Composer);
        Assert.AreEqual(TuneTypeEnum.Air, tune.TuneType);
        Assert.AreEqual(TuneKeyEnum.Ador, tune.TuneKey);
    }

    [Test]
    public void Cannot_Create_When_Title_Invalid()
    {
        Assert.Throws<FormatException>(() => 
            Tune.CreateTune("", "Composer", TuneTypeEnum.Air, TuneKeyEnum.Ador));
    }
    
    [Test]
    public void Cannot_Create_When_Composer_Invalid()
    {
        Assert.Throws<FormatException>(() => 
            Tune.CreateTune("Title", "", TuneTypeEnum.Air, TuneKeyEnum.Ador));
    }

    

    
    
    
}