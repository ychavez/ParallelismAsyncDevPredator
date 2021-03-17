using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParallelismExample
{
    public class ParrallelLinq
    {
        public static void Run()
        {
            LinQ();
            ParallelLinQ();
            ParallelLinQ(1);
            ParallelLinQ(2);
        }

        public static void LinQ()
        {
            var data = DataGenerator.GetPersons(10_000_000);
            Console.WriteLine("Process start");
            var watch = new Stopwatch();
            watch.Start();
            var result = data.Where(x => x.Name.ToUpper().StartsWith("JA")).ToList();
            watch.Stop();
            Console.WriteLine($"Elapsed Time {watch.Elapsed}");
        }
        public static void ParallelLinQ()
        {
            var data = DataGenerator.GetPersons(10_000_000);
            Console.WriteLine("Parallel Process start");
            var watch = new Stopwatch();
            watch.Start();
            var result = data.AsParallel().Where(x => x.Name.ToUpper().StartsWith("JA")).ToList();
            watch.Stop();
            Console.WriteLine($"Parallel Elapsed Time {watch.Elapsed}");
        }
        public static void ParallelLinQ(int parallelism)
        {
            var data = DataGenerator.GetPersons(10_000_000);
            Console.WriteLine($"Parallel with {parallelism} thread's Process start");
            var watch = new Stopwatch();
            watch.Start();
            var result = data.AsParallel().WithDegreeOfParallelism(parallelism).Where(x => x.Name.ToUpper().StartsWith("JA")).ToList();
            watch.Stop();
            Console.WriteLine($"Parallel with {parallelism} thread's  Elapsed Time {watch.Elapsed}");
        }

    }
}
