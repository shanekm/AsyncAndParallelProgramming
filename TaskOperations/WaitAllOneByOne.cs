using System.Collections.Generic;
using System.Threading.Tasks;

namespace TaskOperations
{
    class WaitAllOneByOne
    {
        // Pattern to process one by one tasks that will wait for all to finish
        // Use this pattern when
        // 1. Some may fail - discard/retry
        // 2. Overlap computation with result processing - aka hide latency
        public void WaitAllOneByOneExample()
        {
            List<Task<int>> tasks = new List<Task<int>>();

            for (int i = 0; i < N; i++) // Start N tasks
            {
                tasks.Add(Task.Factory.StartNew<int>(
                    () =>
                        {
                            // code
                            int result = 1;
                            return result;
                        }));
            }

            while (tasks.Count > 0) // Wait for all, one by one
            {
                int index = Task.WaitAny(tasks.ToArray());
                // process tasks[index].Result;

                tasks.RemoveAt(index);
            }
        }
    }
}
