using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLINQDemo
{
    // 使用原始的Linq
    public class FunctionalPurity
    {
        public void Run()  
        { 
            // 因為執行在平行執行緒上，要注意thread-unsafe的操作，特別是寫一個變數，是有副作用的。
            int i = 0;

            List<int> list = new List<int>()
            {
                1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1
            };

            // 這就是一個thread-unsafe的code，因為可能會取道同一個i的值
            var query = from n
                        in Enumerable.Range(0,10).AsParallel()
                        //    in list.AsParallel()
                        select n * i++;

            // 盡量使用這種寫法
            var query2 = Enumerable.Range(0, 50).AsParallel().Select((n, index) => n * index);

            foreach (var item in query)
            {
                Console.WriteLine(item.ToString());
            }

            // 最好的效能為，任何方法呼叫查詢操作，應該是thread-safe

        }
    }
}
