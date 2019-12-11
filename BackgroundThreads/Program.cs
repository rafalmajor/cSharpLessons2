using System;
using System.Threading.Tasks;

namespace BackgroundThreads
{
    class Program
    {
        static void Main(string[] args)
        {
            for (int i = 1; i < 11; i++)
            {
                Do(i);
            }

            Console.WriteLine("End of Hello World!");
        }

        public async static Task Do(int index)
        {
            await DoAsync(index).ConfigureAwait(true);
        }

        public static Task DoAsync(int index)
        {
            return Task.Factory.StartNew(
                () => { Console.WriteLine($"Task {index}"); });
        }
    }
}
