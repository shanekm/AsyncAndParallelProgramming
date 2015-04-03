using System.Collections.Generic;
using System;

namespace StockHistory
{
    using System.Net;
    using System.Threading;
    using System.Threading.Tasks;

    public class StockData
    {
        public string DataSource { get; private set; }
        public List<decimal> Prices { get; private set; }

        public StockData(string dataSource, List<decimal> prices)
        {
            this.DataSource = dataSource;
            this.Prices = prices;
        }
    }

    public class DownloadData
    {
        private static StockData GetDataFromInternet(string symbol, int numYearsOfHistory)
        {
            // Old prior to .net 4.0 way of doing things
            IAsyncResult[] iars = new IAsyncResult[3];
            WaitHandle[] handles = new WaitHandle[3];
            
            try
            {
                // TAKE ONE
                // call web api services here
                // To implement
                //iars[0] = GetDataFromYahoo(symbol, numYearsOfHistory);
                //iars[1] = GetDataFromMSN(symbol, numYearsOfHistory);
                //iars[2] = GetDataFromNasdaq(symbol, numYearsOfHistory);

                // wait for first to finish (any)
                for (int i = 0; i < iars.Length; i++)
                {
                    //handles[i] = iars[i].AsyncState as RequestState).Done;
                }

                int index = WaitHandle.WaitAny(handles, 15 * 1000 /* 15 secs */);


                // TAKE TWO
                // Implementing TPL
                Task yahoo = GetDataFromYahoo(symbol, numYearsOfHistory);
                Task msn = GetDataFromMSN(symbol, numYearsOfHistory);
                Task nasdaq = GetDataFromNasdaq(symbol, numYearsOfHistory);

                // Wait for first one to finish (any first one)
                Task.WaitAny(new Task[] { yahoo, msn, nasdaq });

            }
            catch (Exception ex)
            {
                string msg = string.Format("Unable to initiate set of web requests: {0}", ex.Message);
                throw new ApplicationException(msg);
            }
        }

        // TAKE ONE
        // Returns old IAsyncResult
        //private static IAsyncResult GetDataFromYahoo(string symbol, int numYearsOfHistory)
        //{
        //    // Createws HttpWebRequest and calls yahoo
        //    IAsyncResult iar = new AsyncCallback(WebClient);
        //}

        // TAKE TWO
        // converting above to Task result
        private static Task GetDataFromYahoo(string symbol, int numYearsOfHistory)
        {
            string url = "http://localhost";
            HttpWebRequest webRequest = (HttpWebRequest)HttpWebRequest.Create(url);
            
            // call web service etc
            AsyncCallback callBack = new AsyncCallback();
            IAsyncResult iar; // not implemented obviously

            // .NET 4.0 facade task
            TaskCompletionSource tc = new TaskCompletionSource(iar);
            return tc.Task;
        }

        private static Task GetDataFromMSN(string symbol, int numYearsOfHistory)
        {
            string url = "http://localhost";
            HttpWebRequest webRequest = (HttpWebRequest)HttpWebRequest.Create(url);

            // call web service etc
            AsyncCallback callBack = new AsyncCallback();
            IAsyncResult iar; // not implemented obviously

            // .NET 4.0 facade task
            TaskCompletionSource tc = new TaskCompletionSource(iar);
            return tc.Task;
        }

        private static Task GetDataFromNasdaq(string symbol, int numYearsOfHistory)
        {
            string url = "http://localhost";
            HttpWebRequest webRequest = (HttpWebRequest)HttpWebRequest.Create(url);

            // call web service etc
            AsyncCallback callBack = new AsyncCallback();
            IAsyncResult iar; // not implemented obviously

            // .NET 4.0 facade task
            TaskCompletionSource tc = new TaskCompletionSource(iar);
            return tc.Task;
        }
    }
}
