using System;
using System.Threading.Tasks;

namespace TaskOperations
{
    class ReturnValue
    {
        public void ReturnValueExample()
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
