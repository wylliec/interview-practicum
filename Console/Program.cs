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
            var output = await Task.Run(() => server.TakeOrder(unparsedOrder).Result);
            System.Console.WriteLine(output);
        }
    }
}