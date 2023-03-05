using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application;

public interface IDishManager
{
    /// <summary>
    ///     Constructs a list of dishes, each dish with a name and a count
    /// </summary>
    /// <param name="order">Parsed order object</param>
    /// <returns>List of dishes with their names and counts</returns>
    Task<List<Dish>> GetDishes(Order order);
}