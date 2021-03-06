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
            // MultiThreadArrayTransformation();
            //  MultithreadEachTransformation();
            //  EachTransformation();
            //  MultiThreadArrayTransformation(2);
            LockExample();
            LockExample();
            LockExample();
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
            System.Console.WriteLine("starting parallel for");
            Parallel.For(0, arrayNumbersCount, i =>
            {
                numbers[i] = longCalculation(numbers[i]);
                System.Console.WriteLine($"Computing value {numbers[i]}  thread {Thread.CurrentThread.ManagedThreadId}");
            });

            timmer.Stop();
            System.Console.WriteLine($"Multithread {timmer.Elapsed}");
        }
        public static void MultiThreadArrayTransformation(int maxParallelism)
        {
            int arrayNumbersCount = 10;
            var numbers = DataGenerator.GetNumbers(arrayNumbersCount).ToArray();
            Stopwatch timmer = new Stopwatch();
            timmer.Start();
            System.Console.WriteLine($"starting parralel for with {maxParallelism} thread's");
            Parallel.For(0, arrayNumbersCount, new ParallelOptions { MaxDegreeOfParallelism = maxParallelism }, i =>
             {
                 numbers[i] = longCalculation(numbers[i]);
                 System.Console.WriteLine($"Computing value {numbers[i]}  thread {Thread.CurrentThread.ManagedThreadId}");
             });

            timmer.Stop();
            System.Console.WriteLine($"Multithread {timmer.Elapsed}");
        }

        /// <summary>
        /// Metodo atomico, siempre regresa el mismo valor
        /// metodo no autosuficiente
        /// </summary>
        public static void LockExample()
        {
            int increment = 0;
            int sumed = 0;
            var Locker = new object();
            Parallel.For(0, 10000, i =>
            {
                lock (Locker)
                {
                    increment++;
                    sumed = sumed + i;
                }
            });
            Console.WriteLine($"sumed {sumed} incremented {increment}");
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