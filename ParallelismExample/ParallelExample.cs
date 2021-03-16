using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ParallelismExample.Data;

namespace ParallelismExample
{
    public class ParallelExample
    {

        public static void Run()
        {
            // SimpleArrayTransformation();
            //MultiThreadArrayTransformation();
            //MultithreadEachTransformation();
            EachTransformation();
        }

        #region Parallel For


        public static void SimpleArrayTransformation()
        {
            int arrayNumbersCount = 10;
            var numbers = DataGenerator.GetNumbers(arrayNumbersCount).ToArray();
            Stopwatch timmer = new Stopwatch();
            timmer.Start();
            System.Console.WriteLine("starting for");
            for (int i = 0; i < arrayNumbersCount; i++)
            {
                numbers[i] = longCalculation(numbers[i]);
                System.Console.WriteLine($"Computing value {numbers[i]}  thread {Thread.CurrentThread.ManagedThreadId}");
            }
            timmer.Stop();
            System.Console.WriteLine($"Normal {timmer.Elapsed}");

        }
        public static void MultiThreadArrayTransformation()
        {
            int arrayNumbersCount = 10;
            var numbers = DataGenerator.GetNumbers(arrayNumbersCount).ToArray();
            Stopwatch timmer = new Stopwatch();
            timmer.Start();
            System.Console.WriteLine("starting for");
            Parallel.For(0, arrayNumbersCount, i =>
            {
                numbers[i] = longCalculation(numbers[i]);
                System.Console.WriteLine($"Computing value {numbers[i]}  thread {Thread.CurrentThread.ManagedThreadId}");
            });

            timmer.Stop();
            System.Console.WriteLine($"Multithread {timmer.Elapsed}");
        }
 #endregion
 #region Parallel ForEach
        public static void MultithreadEachTransformation()
        {
            var persons = DataGenerator.GetPersons(10);
            Stopwatch timmer = new Stopwatch();
            timmer.Start();
            Parallel.ForEach(persons, person =>
            {
                person = SumAge(person, 20);
            });
            System.Console.WriteLine($"Multithread {timmer.Elapsed}");
        }
        public static void EachTransformation()
        {
            var persons = DataGenerator.GetPersons(10);
            Stopwatch timmer = new Stopwatch();
            timmer.Start();
            persons.ToList().ForEach(x =>
            {
                x = SumAge(x, 20);
            });
            System.Console.WriteLine($"Multithread {timmer.Elapsed}");
        }

#endregion
        private static int longCalculation(int number)
        {
            Thread.Sleep(1000);
            return number + new Random().Next(100);
        }
        private static Person SumAge(Person person, int sumAge)
        {
            Thread.Sleep(1000);
            person.Age = person.Age + sumAge;
            return person;
        }
    }
   
}