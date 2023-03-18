using System;

namespace Pan.Konsol
{
    static class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("PAN Kırpıcı başlatıldı...");

            foreach (var arg in args)
            {
                Console.WriteLine(arg);
            }

            Console.ReadKey();
            
        }
    }
}