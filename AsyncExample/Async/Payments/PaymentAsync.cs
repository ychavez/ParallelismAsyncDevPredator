using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncExample
{
    public class PaymentAsync
    {
        public static async Task Run()
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();

            //Obtenemos una lista de pagos pendientes
            var PayCards = DataGenerator.GetPendingPayments(10).ToList();
            var client = new RestService();
            var result = new List<(int, bool)>();
            //Ejecutamos cada uno de los pagos en un ciclo For
            foreach (var payment in PayCards)
            {
                result.Add(await client.Pay(payment));
            }
            System.Console.WriteLine($"Transcurrieron {stopWatch.Elapsed} Segundos");

        }
        public static async Task RunAsync()
        {
             const int paimentsAmount = 10000;
            try
            {
                var stopWatch = new Stopwatch();
                stopWatch.Start();

                //Obtenemos una lista de pagos pendientes
                var PayCards = DataGenerator.GetPendingPayments(paimentsAmount).ToList();
                var client = new RestService();
                var PaymentTasks = new List<Task<(int, bool)>>();
                //Llenamos la lista de tareas con las tareas de pago
                foreach (var payment in PayCards)
                {

                    PaymentTasks.Add(client.Pay(payment));
                }
                // esperamos a que todas terminen
                var result = await Task.WhenAll<(int, bool)>(PaymentTasks);
                System.Console.WriteLine($"Transcurrieron {stopWatch.Elapsed} Segundos");

            }
            catch (System.Exception ex)
            {

                System.Console.WriteLine(ex.ToString());
            }


        }

        public static async Task RunAsync(int concurrenceLimit)
        {
            const int paimentsAmount = 10000;
            try
            {
                //Declaramos un semaforo donde solo puede haber 3 tareas concurrentes
                var semaphore = new SemaphoreSlim(concurrenceLimit);
                var stopWatch = new Stopwatch();
                stopWatch.Start();

                //Obtenemos una lista de pagos pendientes
                var PayCards = DataGenerator.GetPendingPayments(paimentsAmount).ToList();
                var client = new RestService();
                var PaymentTasks = new List<Task<(int, bool)>>();
                //Llenamos la lista de tareas con las tareas de pago


                PaymentTasks = PayCards.Select(async payment =>
                {
                    await semaphore.WaitAsync();
                    try
                    {
                        System.Console.WriteLine($"Tarea {payment} ejecutada");
                        return await client.Pay(payment);
                    }
                    finally
                    {
                        semaphore.Release();
                    }
                }).ToList();

                // esperamos a que todas terminen
                var result = await Task.WhenAll<(int, bool)>(PaymentTasks);
                System.Console.WriteLine($"Transcurrieron {stopWatch.Elapsed} Segundos");

            }
            catch (System.Exception ex)
            {

                System.Console.WriteLine(ex.ToString());
            }

        }
        
        public static async Task RunAsync(int concurrenceLimit, IProgress<int> progress)
        {
            const int paimentsAmount = 10000;
            try
            {
                //Declaramos un semaforo donde solo puede haber 3 tareas concurrentes
                var semaphore = new SemaphoreSlim(concurrenceLimit);
                var stopWatch = new Stopwatch();
                stopWatch.Start();

                //Obtenemos una lista de pagos pendientes
                var PayCards = DataGenerator.GetPendingPayments(paimentsAmount).ToList();
                var client = new RestService();
                var PaymentTasks = new List<Task<(int, bool)>>();
                //Llenamos la lista de tareas con las tareas de pago

                var intProgreso = 0;
                PaymentTasks = PayCards.Select(async payment =>
                {
                    await semaphore.WaitAsync();
                    
                    try
                    {
                        System.Console.WriteLine($"Tarea {payment} ejecutada");
                        if (progress != null)
                        {
                            intProgreso ++;
                            progress.Report((paimentsAmount/intProgreso)*100);
                        }
                        return await client.Pay(payment);

                    }
                    finally
                    {
                        semaphore.Release();
                    }
                }).ToList();

                // esperamos a que todas terminen
                var result = await Task.WhenAll<(int, bool)>(PaymentTasks);
                System.Console.WriteLine($"Transcurrieron {stopWatch.Elapsed} Segundos");

            }
            catch (System.Exception ex)
            {

                System.Console.WriteLine(ex.ToString());
            }

        }
        

    }
}