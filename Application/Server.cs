using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application;


/// <summary>
///     Takes a raw order and serves the formatted dishes in the order
/// </summary>
public class Server : IServer
{
    private readonly IDishManager _dishManager;

    public Server()
    {
        _dishManager = new DishManager();
    }

    /// <summary>
    ///     From an unparsed order, such as "evening,1,2,3" returns a comma
    ///     separated list of dishes with their counts if count > 0
    /// </summary>
    /// <param name="unparsedOrder">for example: evening,1,2,3</param>
    /// <returns>for example: steak,potato,wine</returns>
    public async Task<string> TakeOrder(string unparsedOrder)
    {
        try
        {
            var order = this.ParseOrder(unparsedOrder);
            var dishes = await _dishManager.GetDishes(order);
            var output = this.FormatOutput(dishes);
            return output;
        }
        catch
        {
            return "error";
        }
    }


    /// <summary>
    ///     Parses raw string input into structured Order object
    /// </summary>
    /// <param name="unparsedOrder">for example: evening,1,2,3</param>
    /// <returns>Structured Order with time of day and list of dish types</returns>
    private Order ParseOrder(string unparsedOrder)
    {
        var orderItems = unparsedOrder.Split(',');
        if(orderItems.Length < 2) {
            throw new ArgumentException("Insufficient input: expected time of day and at least one dish");
        }

        var timeStr = orderItems.First().Trim().ToUpper();
        bool success = TimeOfDay.TryParse(timeStr, out TimeOfDay time);
        // Exclude case where an int parses as an enum
        if (!success || int.TryParse(timeStr, out _)) {
            throw new FormatException($"Failed to parse TimeOfDay enum {timeStr}");
        }
        var order = new Order(time);

        foreach (var orderItem in orderItems.Skip(1))
        {
            var dishType = int.Parse(orderItem);
            order.Dishes.Add(dishType);
        }

        return order;
    }

    /// <summary>
    ///     Formats an ordered list of dishes to string output
    ///     noting whether count is greater than 1
    /// </summary>
    /// <param name="dishes">List of Dish objects with name and count</param>
    /// <returns>for example: steak,potato,wine</returns>
    private string FormatOutput(IList<Dish> dishes)
    {
        return string.Join(",", dishes.Select(dish => $"{dish.DishName}{this.GetMultiple(dish.Count)}"));
    }

    /// <summary>
    ///     generates string output of count if count is greater than 1
    ///     otherwise returns empty string
    /// </summary>
    /// <param name="count">number of dishes</param>
    /// <returns>string representation of count if greater than 1</returns>
    private string GetMultiple(int count)
    {
        return count > 1 ? $"(x{count})" : "";
    }
}