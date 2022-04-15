using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application;

public class Server : IServer
{
  private IDishManager _dishManager;

  public Server()
  {
    _dishManager = new DishManager();
  }

  public Task<string> TakeOrder(string unparsedOrder)
  {
    try
    {
      var order = ParseOrder(unparsedOrder);
      var dishes = _dishManager.GetDishes(order).Result;
      var returnValue = FormatOutput(dishes);
      return Task.FromResult(returnValue);
    }
    catch
    {
      return Task.FromResult("error");
    }
  }


  private Order ParseOrder(string unparsedOrder)
  {
    var returnValue = new Order
    {
      Dishes = new List<int>()
    };

    var orderItems = unparsedOrder.Split(',');
    foreach (var orderItem in orderItems)
    {
      var parsedOrder = int.Parse(orderItem);
      returnValue.Dishes.Add(parsedOrder);
    }

    return returnValue;
  }

  private string FormatOutput(List<Dish> dishes)
  {
    var returnValue = "";

    foreach (var dish in dishes)
      returnValue = returnValue + string.Format(",{0}{1}", dish.DishName, GetMultiple(dish.Count));

    if (returnValue.StartsWith(",")) returnValue = returnValue.TrimStart(',');

    return returnValue;
  }

  private object GetMultiple(int count)
  {
    if (count > 1) return string.Format("(x{0})", count);
    return "";
  }
}
