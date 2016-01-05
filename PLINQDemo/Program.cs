using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace PLINQDemo
{
    class Program
    {
        static void Main(string[] args)
        {

            AsParallelAndSelect ap = new AsParallelAndSelect();
            //ap.Run();

            ForceParallel fpl = new ForceParallel();
            fpl.Run();

            ParallelSpellchecker ps = new ParallelSpellchecker();
            //ps.Run();

            ParallelSpellcheckerUsingThreadLocal asu = new ParallelSpellcheckerUsingThreadLocal();
            //asu.Run();

            FunctionalPurity fp = new FunctionalPurity();
            //fp.Run();

            IOIntensiveFunctions iof = new IOIntensiveFunctions();
            //iof.Run();

            Cancellation cl = new Cancellation();
            //cl.Run();
            //cl.Run2();

            OptimizingPLINQ op = new OptimizingPLINQ();
            //op.Run();
            //op.Run2();

            //IEnumerable<int> numbers = Enumerable.Range(3, 100000 - 3);
            //long startTime = DateTime.Now.Ticks; //Nanosecond
            //var a = numbers.Where(x => x % 3 == 0);
            //long endTime = DateTime.Now.Ticks; //Nanosecond
            //Console.WriteLine(endTime - startTime);

            //var d = "ADdcbdasdf".AsParallel().Take(5).WithExecutionMode(ParallelExecutionMode.ForceParallelism);
            //foreach (var item in d)
            //{
            //    Console.WriteLine(item);
            //}
        }
    }
}
