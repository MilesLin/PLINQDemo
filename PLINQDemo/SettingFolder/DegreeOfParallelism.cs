using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace PLINQDemo.SettingFolder
{
    public class DegreeOfParallelism
    {        
        public void Run() 
        {            
            string[] presidents ={ "Adams","Arthur","Buchanan","Bush",
                                 "Carter","Cleveland","Clinton","Coolidge","Eisenhower",
                                 "Hayes","Hoover","McKinley","Roosevelt",
                                 "Taft","Taylor","Wilson"};

            var data = presidents.AsParallel()
                .WithDegreeOfParallelism(2)
                .Where(x => x.Contains("o"));

            foreach (var item in data)
            {
                Console.WriteLine("Parallel result: {0}", item);
            }

        }
        
        

    }
}
