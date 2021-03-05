using System;

namespace AsyncExample
{
    class Program
    {
        static  void Main(string[] args)
        {
             SyncExample.Run().Wait();
        }
    }
}
