using System.Threading.Tasks;

namespace TaskOperations
{
    class WaitAny
    {
        public void WaitAnyExample()
        {
            Task t1 = Task.Factory.StartNew(
                () =>
                {
                    decimal min = 1;
                });

            Task t2 = Task.Factory.StartNew(
                () =>
                {
                    decimal max = 2;
                });

            Task t3 = Task.Factory.StartNew(
                () =>
                {
                    // In order to get AVG I need to wait for t1 and t2 to comlete
                    t1.Wait();
                    t2.Wait();
                    decimal avg = 1 + 1;
                });

            Task[] tasks = { t1, t2, t3 };

            // WaitAny - wait for FIRST task to finish
            int index = Task.WaitAny(tasks);
            Task first = tasks[index];

            // return tasks[index].Result // Return result of that task that finished first
            // Now you can use index to grab result of task that finished first
        }
    }
}
