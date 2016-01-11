using System;
using System.Collections.Generic;
using System.Linq;

namespace PLINQDemo
{
    public class HandleAggregateException
    {
        public void Run()
        {
            // 呼叫端try-catch
            var data = this.GetData();

            try
            {
                data.AsParallel().Select(x => new { value = x }).ForAll(x =>
                {
                    var result = x.value.Substring(2, 1);
                    Console.WriteLine(result);
                });
            }
            catch (AggregateException ae)
            {
                foreach (var ex in ae.InnerExceptions)
                {
                    Console.WriteLine("aggregateException: " + ex.Message);
                }
            }
        }

        public void Run2()
        {
            //在委派內放置try-catch，透過這種方式，可以立即攔截Exception，Exception就不會拋到AggregateException
            var data = this.GetData();

            try
            {
                data.AsParallel().Select(x => new { value = x }).ForAll(x =>
                {
                    try
                    {
                        var result = x.value.Substring(2, 1);
                        Console.WriteLine(result);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                });
            }
            catch (AggregateException ae)
            {
                foreach (var ex in ae.InnerExceptions)
                {
                    Console.WriteLine("aggregateException: " + ex.Message);
                }
            }
        }

        private List<string> GetData()
        {
            return new List<string>()
            {
                "a",
                "1234",
                null,
                "miles"
            };
        }
    }
}