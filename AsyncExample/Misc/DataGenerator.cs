using System;
using System.Collections.Generic;

namespace AsyncExample
{
    public class DataGenerator
    {
        public static IEnumerable<int> GetPendingPayments(int count){
            for (int i = 0; i < count; i++)
            {
                yield return new Random().Next(1000);
            }
            
        }
    }
}