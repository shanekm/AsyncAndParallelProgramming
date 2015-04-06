namespace TaskExceptionHandling
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Cryptography.X509Certificates;
    using System.Threading.Tasks;

    class Program
    {
        static void Main(string[] args)
        {
            // RULES
            // 1. You should design to "observe" all unhandled exceptions 
            //      HOW?:
            //      => call .Wait or .Result - exception re-thrown at this point or try
            //          try { tasks[i].Wait() } catch (AggregateException ae) { code }
            //          try { var r = tasks[i].Result; } catch (AggregateException ae) { code }
            //      => call Task.WaitAll - exception(s) re-thrown when all have finished or
            //          try { tasks[i].WaitAll() } catch (AggregateException ae) { code }
            //      => touch task's .Exception property after task has completed or
            //          if ( tasks[i].Exception != null ) { code }
            //      => subscribe to TaskScheduler.UnobservedTaskException
                

            //TAKE ONE - no exception handling
            Task<int> t = Task.Factory.StartNew(() => 1);
            int r1 = t.Result; // exception is re-thrown here

            // TAKE TWO
            // Exception handling
            try
            {
                int r2 = t.Result;
            }
            catch (AggregateException ae)
            {
                Console.WriteLine(ae.InnerException.Message);
            }

            // Wait()
            try
            {
                t.Wait();
            }
            catch (AggregateException ae)
            {
                ae.Flatten(); // Flatten tree to process exceptions as the leaves
                foreach (Exception ex in ae.InnerExceptions)
                {
                    Console.WriteLine(ex.Message);
                }
                Console.WriteLine(ae.InnerException.Message);
            }

            // Last resort exceptions
            // Subscribe to TaskScheduler.UnobservedTaskException
            TaskScheduler.UnobservedTaskException += new EventHandler<UnobservedTaskExceptionEventArgs>(TaskUnobservedException_Handler);


            WaitAllExceptionHandling();
            WaitAllOneByOneExceptionHandling();
        }

        static void TaskUnobservedException_Handler(object sender, UnobservedTaskExceptionEventArgs e)
        {
            Console.WriteLine(e.Exception.Message);
            e.SetObserved();
        }

        // EXCEPTION HANDLING
        // With WaitAll()
        private static void WaitAllExceptionHandling()
        {
            decimal min, max;

            try
            {
                // Throw exception on purpose
                Task t_error = Task.Factory.StartNew(
                    () =>
                        {
                            int i = 0;
                            int j = 100 / i; // Will throw error
                        });


                // 3 tasks, all running in parallel
                Task t2 = Task.Factory.StartNew(
                    () =>
                        {
                            min = 1;
                            decimal result = min / 0; // Throw another exception
                        });

                Task t3 = Task.Factory.StartNew(
                    () =>
                        {
                            max = 2;
                        });


                Task[] tasks = { t_error, t2, t3 };
                Task.WaitAll(tasks); // Wait all with throw exceptions (one of techniques for catching exceptions)
            }
            
            // Adding exception handling
            catch (AggregateException ae)
            {
                ae.Flatten(); // May be multiple exceptions from 1 OR MORE tasks
                foreach (Exception ex in ae.InnerExceptions)
                {
                    Console.WriteLine("Tasking Error: {0}", ae.InnerException.Message);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: {0}", ex.Message);
            }
        }

        // EXCEPTION HANDLING
        // With OneByOne() => example: getting stock data
        // If first one errors out wait for the next to see if it worked (one by one)
        // but checking if the result was correct and Not an exception
        private static void WaitAllOneByOneExceptionHandling()
        {
            decimal min, max;

            try
            {
                // Throw exception on purpose
                Task<decimal> t_error = Task.Factory.StartNew(
                    () =>
                    {
                        decimal i = 0;
                        return 100 / i; // Will throw error
                    });


                // 3 tasks, all running in parallel
                Task<decimal> t2 = Task.Factory.StartNew(
                    () =>
                    {
                        min = 1;
                        return min / 0; // Throw another exception
                    });

                Task<decimal> t3 = Task.Factory.StartNew(
                    () =>
                    {
                        max = 2;
                        return max;
                    });


                Task<decimal>[] tasks = { t_error, t2, t3 };
                while (tasks.Length > 0)
                {
                    int index = Task.WaitAny(tasks);
                    Task<decimal> finished = tasks[index];

                    if (finished.Exception == null) // Succeeded? - Checking Exception property of a task
                    {
                        // return result
                        var result = finished.Result;
                    }

                    tasks = tasks.Where(x => x != finished).ToArray();
                }

                // All tasks have failed!
                throw new ApplicationException("all tasks have falied. Not one succeeded");
            }

            // Adding exception handling
            catch (AggregateException ae)
            {
                ae.Flatten(); // May be multiple exceptions from 1 OR MORE tasks
                foreach (Exception ex in ae.InnerExceptions)
                {
                    Console.WriteLine("Tasking Error: {0}", ae.InnerException.Message);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: {0}", ex.Message);
            }
        }
    }
}
