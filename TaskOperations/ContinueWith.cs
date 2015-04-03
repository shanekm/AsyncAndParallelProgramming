using System.Threading.Tasks;

namespace TaskOperations
{
    class ContinueWith
    {
        public void ContinueWithExample()
        {
            decimal min = 0;
            Task<decimal> t1 = Task.Factory.StartNew(
                () =>
                    {
                        min = 1;
                        return min;
                    });

            // TAKE ONE
            // Suppose t2 needs some result
            //Task<decimal> t2 = Task.Factory.StartNew(
            //    () =>
            //        {
            //            t1.Wait(); // NEED RESULT FROM t1 in order to use it. Instead use ContinueWith
            //        decimal max = min;
            //        return max;
            //    });

            // TAKE TWO
            // .Net optimizes queing, knowing that t1 needs to complete before t2 starts
            // most likely both will run on the same thread
            Task<decimal> t2 = t1.ContinueWith((antecedent) => { // ContinueWith to wait for t1.Result
                decimal max = antecedent.Result; // Grab result from t1
                        return max;
                    });

        }
    }
}
