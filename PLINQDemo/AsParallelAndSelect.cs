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
            string str = "abcdef";
            var data = "abcdef".AsParallel();
            data = data.AsOrdered().Select(x => char.ToUpper(x));
            data.AsSequential().Select(x => char.ToLower(x));
                //.AsSequential().Select(x => char.ToLower(x));
            foreach (var item in data)
            {
                Console.WriteLine(item);
            }
        }
    }
}
