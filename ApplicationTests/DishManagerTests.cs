using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application;
using NUnit.Framework;

namespace ApplicationTests;

[TestFixture]
public class DishManagerTests
{
    [SetUp]
    public void Setup()
    {
        _sut = new DishManager();
    }

    private DishManager _sut;

    private async Task TestGetDishes(Order order, string[] expected, int[] expectedCount = null)
    {
        var actual = await _sut.GetDishes(order);

        Assert.AreEqual(expected.Length, actual.Count);
        for (int i = 0; i < expected.Length; i++)
        {
            Assert.AreEqual(expected[i], actual[i].DishName);
            if (expectedCount == null)
            {
                Assert.AreEqual(1, actual[i].Count);
            }
            else
            {
                Assert.AreEqual(expectedCount[i], actual[i].Count);
            }
        }
    }

    [Test]
    public async Task EmptyListReturnsEmptyList()
    {
        var order = new Order(TimeOfDay.MORNING);

        var expected = new string [] {};

        await this.TestGetDishes(order, expected);
    }

    [Test]
    public async Task ListWithMorning123ReturnsEggToastCoffee()
    {
        var order = new Order(TimeOfDay.MORNING);
        order.Dishes.AddRange(new int [] {1, 2, 3});

        var expected = new string [] {"egg", "toast", "coffee"};

        await this.TestGetDishes(order, expected);
    }

    [Test]
    public async Task ListWithMorning33133233ReturnsEggToastCoffee()
    {
        var order = new Order(TimeOfDay.MORNING);
        order.Dishes.AddRange(new int [] {3, 3, 1, 3, 3, 2, 3, 3});

        var expected = new string [] {"egg", "toast", "coffee"};
        var expectedCount = new int [] {1, 1, 6};

        await this.TestGetDishes(order, expected, expectedCount);
    }

    [Test]
    public void ErrorListWithMorning1234ThrowsOutOfBounds()
    {
        var order = new Order(TimeOfDay.MORNING);
        order.Dishes.AddRange(new int [] {1, 2, 3, 4});

        var ex = Assert.ThrowsAsync<KeyNotFoundException>(async () => await _sut.GetDishes(order));
        Assert.AreEqual("The given key '4' was not present in the dictionary.", ex.Message);
    }

    [Test]
    public void ErrorListWithMorning111ThrowsDuplicateEggs()
    {
        var order = new Order(TimeOfDay.MORNING);
        order.Dishes.AddRange(new int [] {1, 1, 1});

        var ex = Assert.ThrowsAsync<ArgumentException>(async () => await _sut.GetDishes(order));
        Assert.AreEqual("Multiple egg(s) not allowed", ex.Message);
    }

    [Test]
    public async Task ListWithEvening1ReturnsOneSteak()
    {
        var order = new Order(TimeOfDay.EVENING);
        order.Dishes.AddRange(new int [] {1});

        var expected = new string [] {"steak"};

        await this.TestGetDishes(order, expected);
    }

    [Test]
    public async Task ListWithEvening1234ReturnsSteakPotatoWineCake()
    {
        var order = new Order(TimeOfDay.EVENING);
        order.Dishes.AddRange(new int [] {1, 2, 3, 4});

        var expected = new string [] {"steak", "potato", "wine", "cake"};

        await this.TestGetDishes(order, expected);
    }
}