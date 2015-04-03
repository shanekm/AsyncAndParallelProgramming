namespace TaskOperations
{
    using System.Threading.Tasks;

    class ContinueWhenAll
    {
        public void ContinueWhenAllExample()
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
                    decimal max = 2;
                    return max;
                });

            Task<decimal>[] tasks = { t1, t2 };

            Task.Factory.ContinueWhenAll(
                tasks,
                (setOfTasks) =>
                    {
                        decimal result = 0;
                        foreach (Task<decimal> task in setOfTasks)
                        {
                            result += task.Result;
                        }
                    });

        }
    }
}
