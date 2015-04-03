using System.Threading.Tasks;

namespace AsyncAndParallelProgramming
{
    using System;
    using System.Security.Cryptography.X509Certificates;

    public class LambdaExpressions
    {
        public void Run()
        {
            // Original statement
            //statement1;
            //statement2;
            //statement3;

            // Lambda, pass lamba expression to the Factory.Startnew() method
            // Parallel code
            Task.Factory.StartNew(
               /* no parameters */ () =>
                {
                    //statement1;
                    //statement2;
                    //statement3;
                });

            // MEANS
            //Task.Factory.StartNew( delegate ); // delegate => object, that points to the method to be called (function pointer)

            // THIS MATCHES THE FOLLOWING
            // this is what compiler does/generates
            //private sealed class c_DisplayClass1
            //{
            //    public void b_0() // no params
            //    {
            //        statement1;
            //        statement2;
            //        statement3;
            //    }
            //}

            // Behind the scenes
            // Lambda expression == custom class + delegate

            // CLOSURE
            int x, y, z;
            Task.Factory.StartNew(
                () =>
                    {
                        //compute1(x); // compiler needs to update x.. etc (closure)
                        //compute2(y); // no need to specify what x, y, z is etc. compiler figures it out for you
                        //compute1(z);
                    });

            // How are x,y,z variables passed?
            // ALWAYS PASSED BY REFERENCE
            int x1 = 100;
            Task.Factory.StartNew( // Split between main and worker thread
                () =>
                    {
                        x1 = x1 + 1;
                    });

            // What will it write? 100 or 101?
            // depends who wins the race after the SPLIT of execution
            // Only one console output but may be 100 or 101 because Task only updates x1 however main continues to console.writeline
            // if Task finishes first then it will update x1 and writeline 101, otherwise 100
            Console.WriteLine(x1);
        }
    }
}
