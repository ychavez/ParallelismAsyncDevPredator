using System;
using System.Threading.Tasks;

namespace AsyncExample
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var reportarProgreso = new Progress<int>(ReportProgress);
            //  await PaymentAsync.RunAsync(500,reportarProgreso);
           // await PaymentAsync.RunAsync(100);
           await PaymentAsync.RunAsync();
            // await Async.Kitchen.Test.BreakFastAsync();
            // await new PaymentAsync().RunAsync(1,1);
        }

        private static void ReportProgress(int progress)
        {
            Console.Clear();
            System.Console.WriteLine($"{progress }% procesado");
        }



    }
}