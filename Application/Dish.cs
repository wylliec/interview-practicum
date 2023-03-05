namespace Application;

/// <summary>
///     Represents a dish with name and number of times the dish has been ordered
/// </summary>
public class Dish
{
    public Dish(string dishName, int count)
    {
        this.DishName = dishName;
        this.Count = count;
    }

    /// <summary>
    ///     Name of the dish as a string
    /// </summary>
    public string DishName { get; }
    
    /// <summary>
    ///     Number of dishes of this type in the order
    /// </summary>
    public int Count { get; set; }
}