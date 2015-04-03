using System;
using System.Threading.Tasks;

namespace TaskOperations
{
    class Wait
    {
        public void WaitExample()
        {
            // WAITING FOR TASK TO FINISH
            // Call .Wait on task object
            Task t = Task.Factory.StartNew(
                () =>
                {
                    // perform computation
                });

            t.Wait(); // waits until t completes
            Console.WriteLine(t.Status); // RanToCompletion, Cancelled, Faulted

            // Wait, WaitAll, WaitAny
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
                Console.WriteLine("data output: {0}", min);

                t2.Wait();
                Console.WriteLine("data output: {0}", max);

                t3.Wait();
                Console.WriteLine("data output: {0}", avg);
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }
    }
}
