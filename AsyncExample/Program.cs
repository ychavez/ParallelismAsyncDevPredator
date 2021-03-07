using System;
using System.Threading.Tasks;

namespace AsyncExample
{
    class Program
    {
        static async Task Main(string[] args)
        {
              await PaymentAsync.RunAsync(100);
             //await Async.Kitchen.Test.BreakFastAsync();
        }
    }
}
