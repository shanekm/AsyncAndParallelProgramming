namespace AsyncAndParallelProgramming
{
    using System;
    using System.Threading.Tasks;

    class Program
    {
        static void Main(string[] args)
        {
            // Creating a task
            // Task t = new Task(code); <= 'code' being computation to perform

            // here you have 2 code streams executing here (main and task t)
            // Task.Start(); <= tells .net that task 'can' start, then returns to caller here

            // TAKE ONE
            // Performing computaion on a new task
            Task t = new Task(
                () =>
                    {
                        // perform a long running task
                        int i = 1 + 1;
                    });

            // start task from current thread (UI thread/main)
            // will lock up main/UI thread until it finishes - WRONG!
            //t.Start(TaskScheduler.FromCurrentSynchronizationContext()); 

            // main thred will continue 
            // Display results 

            // TAKE TWO - splitting tasks into two
            // Responsiveness + Performance
            // Clicking twice or more will fire this off on a new thread. WORKS!
            string result = "";
            Task t1 = new Task(
                () =>
                    {
                        // perform a long running task/computation
                        result = "test" + "1";
                    });

            // Tells the compiler that once t1 finishes it shouls tart this (t2)
            Task t2 = t.ContinueWith(
                (antecedent) =>
                    {
                        result += " more string";

                        // Display results tasks BUT only after the first t1 finishes 
                    }, TaskScheduler.FromCurrentSynchronizationContext()); // Since this is displaying it needs to run in the UI/main tread

            t1.Start(); // start first worker thread 


            // TAKE THREE
            // Refactoring 
            // Adding counter for counting started worker threads (ajax spinner should start/stop when ALL worker threads stop)
            string result2 = "";

            // Shared resources between tasks/threads -- NOT thread safe
            int counter = 0;
            Task t1a = new Task(
                () =>
                {
                    // perform a long running task/computation
                    result = "test" + "1";
                    counter++; // read => add => then write (shared resource)
                });

            Task t2a = t.ContinueWith(
                (antecedent) =>
                {
                    result += " more string"; // This is OK, because we know that t2 will never run until t1 has finished
                    counter--; // read => substract => then write (shared resource) NOT THREAD SAFE => may throw error!

                    if (counter == 0)
                    {
                        // hide spinner ajax-load
                    }

                    // Display results tasks BUT only after the first t1 finishes 
                }, TaskScheduler.FromCurrentSynchronizationContext()); // Since this is displaying it needs to run in the UI/main tread
                // FromCurrentSynchContext => run on the current thread

            t1.Start(); // start first worker thread 


            // Testing
            int x = 100;
            Task.Factory.StartNew(
                () =>
                    {
                        x = x + 1;
                    });

            // Only one console output but may be 100 or 101
            Console.WriteLine(x);

            // TYPES OF TASKS
            // a. Code tasks => thready (require a thread to execute)
            // b. Facade over existing operations (asynch I/O) => can be threadless, hardware may perform operation

            // Wrapping a task in an operation
            //var existingOp = new TaskCompletionSource<ResultType>();
            //Task t_facade = existingOp.Task;

            //existingOp.SetResult(/*some result*/);

        }
    }
}
