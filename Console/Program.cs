using System.Threading.Tasks;
using Application;

namespace Console;

internal class Program
{
    private static async Task Main()
    {
        var server = new Server();
        while (true)
        {
            var unparsedOrder = System.Console.ReadLine();
            if (unparsedOrder == "")
            {
                break;
            }
            var output = await server.TakeOrder(unparsedOrder);
            System.Console.WriteLine(output);
        }
    }
}