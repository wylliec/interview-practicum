using Application;
using NUnit.Framework;

namespace ApplicationTests;

[TestFixture]
public class ServerTests
{
    [SetUp]
    public void Setup()
    {
        _sut = new Server();
    }

    [TearDown]
    public void Teardown()
    {
    }

    private Server _sut;

    [Test]
    public void ErrorGetsReturnedWithBadInput()
    {
        var order = "one";
        var expected = "error";
        var actual = _sut.TakeOrder(order).Result;
        Assert.AreEqual(expected, actual);
    }

    [Test]
    public void CanServeSteak()
    {
        var order = "1";
        var expected = "steak";
        var actual = _sut.TakeOrder(order).Result;
        Assert.AreEqual(expected, actual);
    }

    [Test]
    public void CanServe2Potatoes()
    {
        var order = "2,2";
        var expected = "potato(x2)";
        var actual = _sut.TakeOrder(order).Result;
        Assert.AreEqual(expected, actual);
    }

    [Test]
    public void CanServeSteakPotatoWineCake()
    {
        var order = "1,2,3,4";
        var expected = "steak,potato,wine,cake";
        var actual = _sut.TakeOrder(order).Result;
        Assert.AreEqual(expected, actual);
    }

    [Test]
    public void CanServeSteakPotatox2Cake()
    {
        var order = "1,2,2,4";
        var expected = "steak,potato(x2),cake";
        var actual = _sut.TakeOrder(order).Result;
        Assert.AreEqual(expected, actual);
    }

    [Test]
    public void CanGenerateErrorWithWrongDish()
    {
        var order = "1,2,3,5";
        var expected = "error";
        var actual = _sut.TakeOrder(order).Result;
        Assert.AreEqual(expected, actual);
    }

    [Test]
    public void CanGenerateErrorWhenTryingToServerMoreThanOneSteak()
    {
        var order = "1,1,2,3";
        var expected = "error";
        var actual = _sut.TakeOrder(order).Result;
        Assert.AreEqual(expected, actual);
    }
}