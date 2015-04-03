using System.Threading.Tasks;

namespace TaskOperations
{
    class WaitAll
    {
        public void WaitAllExample()
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

            // WaitAll - wait for ALL to finish
            // All tasks have to complete
            Task[] tasks = { t1, t2, t3 };
            Task.WaitAll(tasks);
        }
    }
}
