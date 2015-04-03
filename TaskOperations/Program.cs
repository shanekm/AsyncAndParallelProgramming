namespace TaskOperations
{
    using System;
    using System.Threading.Tasks;

    class Program
    {
        static void Main(string[] args)
        {
            // Wait
            WaitForTask();

            // Harvest result
            Task<int> t = Task.Factory.StartNew(
                () =>
                    {
                        // do some computation
                        int result = 1;
                        return result;
                    });

            int r = t.Result; // will wait here until returned value is computed
        }

        public static void WaitForTask()
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

        public void TaskReturningValue()
        {
            Task<decimal> t1 = Task.Factory.StartNew(
                () =>
                {
                    decimal min = 1;
                    return min;
                });

            Task<decimal> t2 = Task.Factory.StartNew(
                () =>
                {
                    decimal max = 1;
                    return max;
                });

            Task<decimal> t3 = Task.Factory.StartNew(
                () =>
                    {
                        decimal avg = 1 + 1;
                        return avg;
                    });

            // No need to wait for task to finish
            // T.Result implies that it will wait for Result of a Task
            Console.WriteLine("data output: {0}", t1.Result);
            Console.WriteLine("data output: {0}", t2.Result);
            Console.WriteLine("data output: {0}", t3.Result);

        }
    }
}
