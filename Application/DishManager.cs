using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application;

/// <summary>
///     Converts parsed orders to lists of dishes
/// </summary>
public class DishManager : IDishManager
{
    private IDictionary<TimeOfDay, IDictionary<int, string>> dishTypeToName;

    public DishManager() {
        dishTypeToName = new Dictionary<TimeOfDay, IDictionary<int, string>>();
        dishTypeToName[TimeOfDay.MORNING] = new Dictionary<int, string>{
            [1] = "egg",
            [2] = "toast",
            [3] = "coffee",
        };
        dishTypeToName[TimeOfDay.EVENING] = new Dictionary<int, string>{
            [1] = "steak",
            [2] = "potato",
            [3] = "wine",
            [4] = "cake",
        };
    }

    /// <summary>
    ///     Takes an Order object, counts the number of each item,
    ///     sorts the order by dish type, and builds a list of dishes to be returned.
    /// </summary>
    /// <param name="order">The parsed order object</param>
    /// <returns>List of Dishes with dish name and count</returns>
    public async Task<List<Dish>> GetDishes(Order order)
    {
        return await Task.Run(() => order.Dishes
            .GroupBy(x => x)
            .ToDictionary(g => g.Key, g => g.Count())
            .OrderBy(kv => kv.Key) // sort by dish type
            .Select(kv => this.OrderToDish(order.Time, kv.Key, kv.Value))
            .ToList());
    }

    /// <summary>
    ///     Constructs Dish object with dishName and count, if legal,
    ///     otherwise throws an exception (dishType not valid or multiples not allowed)
    /// </summary>
    /// <param name="time">time of day when order was placed</param>
    /// <param name="dishType">int, represents a dishtype</param>
    /// <param name="count">number of dishes</param>
    /// <returns>The constructed Dish object</returns>
    private Dish OrderToDish(TimeOfDay time, int dishType, int count)
    {
        var dishName = this.GetDishName(time, dishType);
        if (count > 1 && !this.IsMultipleAllowed(time, dishType))
        {
            throw new ArgumentException($"Multiple {dishName}(s) not allowed");
        }
        return new Dish(dishName, count);
    }

    /// <summary>
    ///     Gets the name of the dish by looking up the dish type number
    ///     and time of day in a pre-defined dictionary.
    ///     Throws exception if dish type not valid.
    /// </summary>
    /// <param name="time">time of day when order was placed</param>
    /// <param name="dishType">int, represents a dishtype</param>
    /// <returns>name of the dish</returns>
    private string GetDishName(TimeOfDay time, int dishType)
    {
        // throws exception for dishType out of range
        return this.dishTypeToName[time][dishType];
    }


    /// <summary>
    ///     Returns whether multiples of this dish type is allowed
    /// </summary>
    /// <param name="time">time of day when order was placed</param>
    /// <param name="dishType">int, represents a dishtype</param>
    /// <returns>whether multiples of this dish type is allowed</returns>
    private bool IsMultipleAllowed(TimeOfDay time, int dishType)
    {
        return (time == TimeOfDay.MORNING && dishType == 3) || (time == TimeOfDay.EVENING && dishType == 2);
    }
}