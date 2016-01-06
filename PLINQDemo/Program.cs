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
            //fpl.Run();

            ParallelSpellchecker ps = new ParallelSpellchecker();
            //ps.Run();

            ParallelSpellcheckerUsingThreadLocal asu = new ParallelSpellcheckerUsingThreadLocal();
            //asu.Run();

            FunctionalPurity fp = new FunctionalPurity();
            //fp.Run();

            MergeOptions mo = new MergeOptions();
            mo.Run();

            IOIntensiveFunctions iof = new IOIntensiveFunctions();
            //iof.Run();

            Cancellation cl = new Cancellation();
            //cl.Run();
            //cl.Run2();

            OptimizingPLINQ op = new OptimizingPLINQ();
            //op.Run();
            //op.Run2();

            HandleAggregateException hae = new HandleAggregateException();
            //hae.Run();
            //hae.Run2();

            MeasurePerformance mp = new MeasurePerformance();
            //mp.Run();
        }
    }
}
