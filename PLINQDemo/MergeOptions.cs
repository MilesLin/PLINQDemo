using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PLINQDemo
{
    public class MergeOptions
    {
        public void Run() 
        {
            var nums = Enumerable.Range(1, 100000);

            // 取代原本自動AutoBuffered選項
            var scanLine = from n in nums.AsParallel()
                           .WithMergeOptions(ParallelMergeOptions.NotBuffered)
                           where n % 2 == 0
                           select ExpensiveFunc(n);

            Stopwatch sw = Stopwatch.StartNew();

            foreach (var item in scanLine)
            {
                
            }
            Console.WriteLine("Elapsed time:{0}ms", sw.ElapsedMilliseconds);
        }

        private string ExpensiveFunc(int n)
        {
            Thread.SpinWait(10000);
            return n.ToString();
        }
    }
}
