using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PLINQDemo.ParallelSpellcheckerFolder
{
    public class RandomMutiThread
    {
        public void Run()
        {
            Random rd = new Random();

            //var allocationRandom = new ThreadLocal<Random>(() => new Random());

            var data = Enumerable.Range(0, 10000).AsParallel()
                //.Select(x => new MyStruct() { Value = x, Rand = allocationRandom.Value.Next() })
                .Select(x => new MyStruct() { Value = x, Rand = rd.Next() })
                .Where(x => x.Rand == 0);

            foreach (var item in data)
            {
                Console.WriteLine(item.Rand);
            }
        }

        struct MyStruct
        {
            public int Value { get; set; }
            public int Rand { get; set; }
        }
    }
}
