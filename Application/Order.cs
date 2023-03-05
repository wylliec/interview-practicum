using System.Collections.Generic;

namespace Application;

/// <summary>
///     Represents a customer order with time of day and dishes ordered
/// </summary>
public class Order
{
    public Order(TimeOfDay time)
    {
        Dishes = new List<int>();
        Time = time;
    }

    /// <summary>
    ///     Time of day when the order was placed
    /// </summary>
    public TimeOfDay Time { get; }

    /// <summary>
    ///     List of integer representation of dish type
    /// </summary>
    public List<int> Dishes { get; }
}