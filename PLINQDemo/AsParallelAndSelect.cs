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
            // AsOrdered
            // AsSequential
            // 
            string[] array ={ "Adams","Arthur","Buchanan","Bush",
                                 "Carter","Cleveland","Clinton","Coolidge","Eisenhower",
                                 "Hayes","Hoover","McKinley","Roosevelt",
                                 "Taft","Taylor","Wilson"};
            
            foreach (var item in array.AsParallel().Select(x => x.ToLower()))
            {
                Console.WriteLine(item);
            }

        }

    }
    

}
