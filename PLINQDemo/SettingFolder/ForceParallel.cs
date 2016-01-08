using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PLINQDemo.SettingFolder
{
    public class ForceParallel
    {
        public void Run()
        {
            Stopwatch sw = Stopwatch.StartNew();

            var data = Enumerable.Range(0, 10000000)
                .AsParallel()
                //.WithExecutionMode(ParallelExecutionMode.ForceParallelism)
                .Where(x => x % 2 == 0)
                .Select((x, index) => ExpensiveMethod(x, index)).ToArray();
            sw.Stop();

            Console.WriteLine("Consume Time : {0} ms", sw.ElapsedMilliseconds);

        }

        private string ExpensiveMethod(int x, int index)
        {
            return (x * 10 + index + 2).ToString();
        }

    }
}
