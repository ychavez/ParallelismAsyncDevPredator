using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ParallelismExample.Data;

namespace ParallelismExample
{

    public class DataGenerator
    {

        public static IEnumerable<int> GetNumbers(int count)
        {
            for (int i = 0; i < count; i++)
            {
                yield return new Random().Next(1000);
            }
        }
        public static IEnumerable<Person> GetPersons(int count)
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            for (int i = 0; i < count; i++)
            {
                yield return new Person
                {
                    Age = random.Next(1000),
                    Name = new string(Enumerable.Repeat(chars, 5)
                    .Select(s => s[random.Next(s.Length)]).ToArray())
                };
            }

    }

}
}