using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLINQDemo
{
    public class ForceParallel
    {
        public void Run() 
        {
            var data = Enumerable.Range(0, 100);
            var parallelQuery = (from p in data.AsParallel()
                                 .WithExecutionMode(ParallelExecutionMode.ForceParallelism)
                                 where p % 3 == 0
                                 select p)
                                .ToList();
        }

    }
}
