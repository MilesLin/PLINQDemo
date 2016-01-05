using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLINQDemo
{
    public class AsParallelAndSelect
    {
        public void Run()
        {
            // AsParallel() 用法， AsOrdered()。 
            var data = "abcdef".AsParallel().AsOrdered().Select(x => char.ToUpper(x));
            foreach (var item in data)
            {
                Console.WriteLine(item);
            }
        }
    }
}
