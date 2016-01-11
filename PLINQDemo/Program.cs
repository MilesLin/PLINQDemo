using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using PLINQDemo.ParallelSpellcheckerFolder;
using PLINQDemo.SettingFolder;

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

            Spellchecker s = new Spellchecker();
            //s.Run();

            ParallelSpellchecker ps = new ParallelSpellchecker();
            //ps.Run();

            RandomMutiThread rmt = new RandomMutiThread();
            //rmt.Run();

            ParallelSpellcheckerUsingThreadLocal asu = new ParallelSpellcheckerUsingThreadLocal();
            //asu.Run();

            FunctionalPurity fp = new FunctionalPurity();
            //fp.Run();

            MergeOptions mo = new MergeOptions();
            //mo.Run();

            DegreeOfParallelism iof = new DegreeOfParallelism();
            //iof.Run();

            Cancellation cl = new Cancellation();
            //cl.Run();
            //cl.Run2();

            OptimizingPLINQ op = new OptimizingPLINQ();
            //op.Run();
            //op.Run2();

            HandleAggregateException hae = new HandleAggregateException();
            hae.Run();
            hae.Run2();

            MeasurePerformance mp = new MeasurePerformance();
            //mp.Run();

            DoNotUsePLINQ dnup = new DoNotUsePLINQ();
            //dnup.Run();
        }
    }
}
