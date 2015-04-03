namespace TaskOperations
{
    using System;
    using System.Threading.Tasks;

    class Program
    {
        static void Main(string[] args)
        {
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
    }
}
