using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLINQDemo
{
    public class DoNotUsePLINQ
    {
        public void Run() 
        {
            var watch = new Stopwatch();            

            // 回傳 100000000 個 10
            var data = Enumerable.Repeat(10, 100000000);
             
            watch.Start();
            var result = data.Average();
            watch.Stop();

            Console.WriteLine("Linear: {0} ms", watch.ElapsedMilliseconds);

            watch.Reset();

            watch.Start();

            var parallel = data.AsParallel().Average();
                        
            watch.Stop();

            Console.WriteLine("Parallel: {0} ms", watch.ElapsedMilliseconds);

        }
    }
}
