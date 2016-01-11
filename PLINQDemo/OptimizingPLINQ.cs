using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PLINQDemo
{
    // 效能調教
    public class OptimizingPLINQ
    {
        // 輸出端優化
        public void Run() 
        { 
            // 輸出端優化
            // PLINQ的優點就是可以很方便地從平行作業將結果收集起來，變成單一序列的輸出
            // 通常會在foreach執行一些方法，而這些方法會在每個元素跑一次

            // 用迴圈來逐一查看結果，則必須將背景工作執行緒的結果序列化製列舉程式執行緒上。
            long startTime = DateTime.Now.Ticks; //Nanosecond
            var data = "abcdef".AsParallel().AsOrdered().Select(x => char.ToUpper(x));
            foreach (var item in data)
            {
                this.DoSomething(item);
            }
            long endTime = DateTime.Now.Ticks; //Nanosecond

            Console.WriteLine("Time1 : " + (endTime - startTime).ToString());

            /*------------------------優化------------------------*/

            // 但是如果只是想根據每個執行緒的結果來執行某個動作，可以使用ForAll方法，在多個執行緒上執行此工作
            // 以這個例子為例，如果不在乎輸出的順序，可以使用ForAll 方法

            long startTime2 = DateTime.Now.Ticks; //Nanosecond
            
            "abcdef".AsParallel().AsOrdered().Select(x => char.ToUpper(x)).ForAll(c => { this.DoSomething(c); });

            long endTime2 = DateTime.Now.Ticks; //Nanosecond

            Console.WriteLine("Time2 : " + (endTime2 - startTime2).ToString());
            
        }

        private void DoSomething(char c) 
        {
            Thread.Sleep(500);
            Console.WriteLine(c);
        }

        public void Run2()
        {
            
            var nums = Enumerable.Range(0, 10000000).ToArray();

            // 開啟負載平衡，告訴PLINQ要使用區塊分割。
            Partitioner<int> customPartitioner = Partitioner.Create(nums, true);

            long startTime = DateTime.Now.Ticks; //Nanosecond

            var q = (from x in customPartitioner.AsParallel()
                    select x * Math.PI).ToArray();

            long endTime = DateTime.Now.Ticks; //Nanosecond

            Console.WriteLine("Time1 : " + (endTime - startTime).ToString());

            //From what I understand, PLINQ will choose range or chunk partitioning depending on whether 
            //the source sequence is an IList or not. If it is an IList, the bounds are known and elements 
            //can be accessed by index, so PLINQ chooses range partitioning to divide the list evenly 
            //between threads. For instance, if you have 1000 items in your list and you use 4 threads, 
            //each thread will have 250 items to process. On the other hand, if the source sequence 
            //is not an IList, PLINQ can't use range partitioning because it doesn't know what the 
            //ranges would be; so it uses chunk partitioning instead.
            //In your case, if you have an IList and you want to force chunk partitioning, you 
            //can just make it look like a simple IEnumerable: instead of writing this:
        }
    }
}
