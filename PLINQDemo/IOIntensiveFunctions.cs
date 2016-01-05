using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace PLINQDemo
{
    public class IOIntensiveFunctions
    {
        // 通常執行查詢，花費時間都不再CPU，而是在等待某樣東西，像是網頁下載或者是等待硬碟回應等等....
        // PLINQ 可以很有效率的平行處理這樣查詢，只要在AsParallel之後使用WithDegreeOfParallelism。
        // 例如我們想要同時pinq六個網址。
        public void Run() 
        {
            var data = from site in new[]
            {
              "www.albahari.com",
              "www.linqpad.net",
              "www.oreilly.com",
              "www.takeonit.com",
              "stackoverflow.com",
              "www.rebeccarey.com"  
            }
            .AsParallel().WithDegreeOfParallelism(6)
            let p = new Ping().Send(site)
            select new
            {
                site,
                Result = p.Status,
                Time = p.RoundtripTime
            };

            foreach (var item in data)
            {
                Console.WriteLine(item.site);
                Console.WriteLine(item.Result);
                Console.WriteLine(item.Time);
            }

            //WithDegreeOfParallelism  強制PLINQ 同時跑指定的tasks數量，但是如果跑在雙核心上，PLINQ只會預設跑一次跑兩個task。

        }
        
        

    }
}
