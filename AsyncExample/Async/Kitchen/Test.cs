using System.Diagnostics;
using System.Threading.Tasks;
using AsyncExample.Async.Kitchen;

namespace AsyncExample.Async.Kitchen
{
    public class Test
    {
        public static void BreakFast()
        {
            var time = Stopwatch.StartNew();
            Cooking.PourCoffee();
            Cooking.HeatPan();
            Cooking.FryEggs();
            Cooking.FryBacon();
            Cooking.ToastBread();
            Cooking.JamBread();
            time.Stop();
            System.Console.WriteLine(time.Elapsed);
        }
        public static async Task BreakFastAsync()
        {  
            var time = Stopwatch.StartNew();
           //1 Encendemos la cafetera
            var coffeeTask = Cooking.PourCoffeeAsync();
           //2 Esperamos a que se caliente el sarten 
            await Cooking.HeatPanAsync();
           //3 Colocamos a freir los huevos con tocino y el a tostar el pan
            var eggsTask = Cooking.FryEggsAsync();
            var baconTask = Cooking.FryBaconAsync();
            var breadTask = Cooking.ToastBreadAsync();
            await breadTask;
            //4 Colocamos mermelada en el pan
            var jamBreadTask = Cooking.JamBreadAsync();
           // Verificamos que todo este terminado
            await jamBreadTask;
            await coffeeTask;
            await eggsTask;
            await baconTask;

            time.Stop();
            System.Console.WriteLine(time.Elapsed);
        }
    }
}