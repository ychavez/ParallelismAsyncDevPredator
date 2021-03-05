using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AsyncExample
{
    public class SyncExample
    {

        public static async Task Run()
        {

            var PayCards = DataGenerator.GetPendingPayments(10).ToList();
            var client = new RestService();
            var result = new List<(int, bool)>();
            foreach (var payment in PayCards)
            {
                result.Add(await client.Pay(payment));
            }
            System.Console.WriteLine(result);

        }
    }
}