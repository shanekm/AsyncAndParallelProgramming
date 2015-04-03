using System.Threading.Tasks;

namespace TaskOperations
{
    class ContinueWhenAny
    {
        public void ContinueWhenAnyExample()
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

            Task.Factory.ContinueWhenAny(
                tasks,
                winner =>
                    {
                        // grab result 
                        var result = winner.Result;
                    });
        }
    }
}
