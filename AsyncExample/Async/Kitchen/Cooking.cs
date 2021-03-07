using System.Threading;
using System.Threading.Tasks;

namespace AsyncExample.Async.Kitchen
{
    public class Cooking
    {
        public static void PourCoffee() { Thread.Sleep(1000); System.Console.WriteLine("Coffe Ready"); }
        public static void HeatPan() { Thread.Sleep(1000); System.Console.WriteLine("Pan Ready"); }
        public static void FryEggs(){ Thread.Sleep(1000); System.Console.WriteLine("Eggs Ready"); }
        public static void FryBacon() { Thread.Sleep(1000); System.Console.WriteLine("Bacon Ready"); }
        public static void ToastBread() { Thread.Sleep(1000); System.Console.WriteLine("Bread Ready"); }
        public static void JamBread() { Thread.Sleep(1000); System.Console.WriteLine("Bread Jammed"); }


        public static async Task PourCoffeeAsync() { await Task.Delay(1000); System.Console.WriteLine("Coffe Ready"); }
        public static async Task HeatPanAsync() { await Task.Delay(1000); System.Console.WriteLine("Pan Ready"); }
        public static async Task FryEggsAsync() { await Task.Delay(1000); System.Console.WriteLine("Bacon Ready"); }        public static async Task FryBaconAsync() => await Task.Delay(1000);
        public static async Task ToastBreadAsync(){ await Task.Delay(1000); System.Console.WriteLine("Bread Ready"); }
        public static async Task JamBreadAsync() { await Task.Delay(1000); System.Console.WriteLine("Bread Jammed"); }
        
        
    }
}