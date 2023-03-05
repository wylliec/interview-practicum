using System.Threading.Tasks;
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

    private Server _sut;

    public async Task TestServer(string order, string expected) {
        var actual = await _sut.TakeOrder(order);
        Assert.AreEqual(expected, actual);
    }

    [Test]
    public async Task ErrorNoTimeOfDay()
    {
        await this.TestServer(order: "1,2", expected: "error");
    }

    [Test]
    public async Task ErrorBadTimeOfDay()
    {
        await this.TestServer(order: "noon,1", expected: "error");
    }

    [Test]
    public async Task CanServeEggWhitespace()
    {
        await this.TestServer(order: "   morning    ,          1    ", expected: "egg");
    }


    [Test]
    public async Task CanServeEggToastCoffee()
    {
        await this.TestServer(order: "morning, 1, 2, 3", expected: "egg,toast,coffee");
    }

    [Test]
    public async Task CanServe3Coffee()
    {
        await this.TestServer(order: "Morning,3,3,3", expected: "coffee(x3)");
    }

    [Test]
    public async Task CanServeEggToastCoffeex2()
    {
        await this.TestServer(order: "morning ,1,3,2,3", expected: "egg,toast,coffee(x2)");
    }

    [Test]
    public async Task ErrorMoreThanOneToast()
    {
        await this.TestServer(order: "morning, 1, 2, 2", expected: "error");
    }

    [Test]
    public async Task ErrorNoBreakfastDessert()
    {
        await this.TestServer(order: "morning, 1, 2, 4", expected: "error");
    }

    [Test]
    public async Task ErrorBadDishInput()
    {
        await this.TestServer(order: "evening,one", expected: "error");
    }

    [Test]
    public async Task CanServeSteak()
    {
        await this.TestServer(order: "evening,1", expected: "steak");
    }

    [Test]
    public async Task CanServe2Potatoes()
    {
        await this.TestServer(order: "evening,2,2", expected: "potato(x2)");
    }

    [Test]
    public async Task CanServeSteakPotatoWineCake()
    {
        await this.TestServer(order: "evening,1,2,3,4", expected: "steak,potato,wine,cake");
    }

    [Test]
    public async Task CanServeSteakPotatox2Cake()
    {
        await this.TestServer(order: "evening,1,2,2,4", expected: "steak,potato(x2),cake");
    }

    [Test]
    public async Task ErrorWrongDish()
    {
        await this.TestServer(order: "evening,1,2,3,5", expected: "error");
    }

    [Test]
    public async Task ErrorMoreThanOneSteak()
    {
        await this.TestServer(order: "evening,1,1,2,3", expected: "error");
    }

    [Test]
    public async Task ErrorMoreThanOneWine()
    {
        await this.TestServer(order: "evening,1,3,2,3", expected: "error");
    }
}