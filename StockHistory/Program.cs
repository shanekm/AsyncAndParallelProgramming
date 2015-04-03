using System;

namespace StockHistory
{
    using System.Threading.Tasks;

    class Program
    {
        static void Main(string[] args)
        {
            string version, platform, symbol;
            int numYearsOfHistory;

            ProcessCmdLineArgs(args, out version, out platform, out symbol);

            ProcessStockSymbol(symbol, numYearsOfHistory);

            Console.WriteLine();
            Console.WriteLine("DONE");
        }

        private static void ProcessStockSymbol(string symbol, int numYearsOfHistory)
        {
            decimal min, max, avg;
            min = max = avg = 0;

            try
            {
                // 3 tasks, all running in parallel
                Task t1 = Task.Factory.StartNew(
                    () =>
                        {
                            min = 1;
                        });

                Task t2 = Task.Factory.StartNew(
                    () =>
                        {
                            max = 2;
                        });

                Task t3 = Task.Factory.StartNew(
                    () =>
                        {
                            // In order to get AVG I need to wait for t1 and t2 to comlete
                            t1.Wait();
                            t2.Wait();
                            avg = min + max;
                        });


                // IMPORTANT order, avg needs to be computed AFTER 
                // Can NOT do output statement UNTIL t1,t2,t3 finish
                // Must wait for tasks to finish first
                t1.Wait();
                Console.WriteLine("data output: ", min);

                t2.Wait();
                Console.WriteLine("data output: ", max);

                t3.Wait();
                Console.WriteLine("data output: ", avg);
            }
            catch (Exception)
            {
                
                throw;
            }
        }
    }
}
