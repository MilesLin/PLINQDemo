using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLINQDemo
{
    public class MeasurePerformance
    {
        public void Run() 
        {
            var source = Enumerable.Range(0, 10000000);
            
            Stopwatch sw = Stopwatch.StartNew();

            foreach (var item in source.AsParallel().Where(x => x % 3 == 0).Select(x => Math.Sqrt(x)))
            {

            }

            sw.Stop();
            Console.WriteLine("Parallel Consume Time : {0} ms", sw.ElapsedMilliseconds);

            sw.Restart();
            foreach (var item in source.Where(x => x % 3 == 0).Select(x => Math.Sqrt(x)))
            {

            }

            sw.Stop();
            Console.WriteLine("Sequential Consume Time : {0} ms", sw.ElapsedMilliseconds);

        }
    }
}
