using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PLINQDemo.ParallelSpellcheckerFolder
{
    public class ParallelSpellcheckerUsingThreadLocal
    {
        public void Run() 
        {
            //if (!File.Exists("WordLookup.txt"))    // Contains about 150,000 words
            //    new WebClient().DownloadFile(
            //      "http://www.albahari.com/ispell/allwords.txt", "WordLookup.txt");

            var wordLookup = new HashSet<string>(
              File.ReadAllLines("WordLookup.txt"),
              StringComparer.InvariantCultureIgnoreCase);

            // Random is not thread-safe,所以無法簡單的使用AsParallel，
            // 解決方法，使用locks 的方式包住random.Next
            // 參考網址:http://csharpindepth.com/Articles/Chapter12/Random.aspx
            var random = new Random();
            string[] wordList = wordLookup.ToArray();

            // Fortunately, the new ThreadLocal<T> class in .NET 4 makes it very easy to write providers which need to have a single instance per thread
            // 使用ThreadLocal，建立分開的Random object 給每一個Thread

            //new Random(參數)， 是為了確保如果兩個Random objects 被建立在很短的時間，會回傳不同的亂數序列
            var allocationRandom = new ThreadLocal<Random>(() => new Random(Guid.NewGuid().GetHashCode()));
            
            // 隨機取100W的值塞給wordsToTest
            string[] wordsToTest = Enumerable.Range(0, 1000000)
                .AsParallel()
              .Select(i => wordList[allocationRandom.Value.Next(0, wordList.Length)])
              .ToArray();            
                    
            var startTime = DateTime.Now.Ticks;

            var endTime = DateTime.Now.Ticks;
            Console.WriteLine("Time : " + (endTime - startTime).ToString());

        }
    }

}
