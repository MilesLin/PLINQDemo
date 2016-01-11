using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PLINQDemo.SettingFolder
{
    public class MergeOptions
    {
        public void Run()
        {
            var data = Enumerable.Range(0, 10)
            .AsParallel()
            .WithMergeOptions(ParallelMergeOptions.FullyBuffered)
            .Select(x => 
            {
                Thread.Sleep(1000);
                return x;
            });

            Stopwatch sw = Stopwatch.StartNew();

            foreach (var item in data)
            {
                Console.WriteLine("Value: {0}, Time: {1}", item, sw.ElapsedMilliseconds);
            }

        }

    }
}
