using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PLINQDemo
{
    public class ParallelSpellchecker
    {
        public void Run() 
        {
            if (!File.Exists("WordLookup.txt"))    // Contains about 150,000 words
                new WebClient().DownloadFile(
                  "http://www.albahari.com/ispell/allwords.txt", "WordLookup.txt");

            var wordLookup = new HashSet<string>(
              File.ReadAllLines("WordLookup.txt"),
              StringComparer.InvariantCultureIgnoreCase);
            // 載入150000字
            string[] wordList = wordLookup.ToArray();

            // 將字隨機取100W個出來
            var random = new Random();
            string[] wordsToTest = Enumerable.Range(0, 1000000)
              .Select(i => wordList[random.Next(0, wordList.Length)])
              .ToArray();

            // 亂打兩個值，然後找出這兩個值
            wordsToTest[12345] = "woozsh";     // Introduce a couple
            wordsToTest[23456] = "wubsie";     // of spelling mistakes.

            var query = wordsToTest
                      .AsParallel()
                      .Select((word, index) => new IndexedWord { Word = word, Index = index })
                      .Where(iword => !wordLookup.Contains(iword.Word)) // 文章有說，Contains 讓query 能觸發平行處理的一個點
                      .OrderBy(iword => iword.Index);

            var startTime = DateTime.Now.Ticks;
            foreach (var item in query)
            {
                Console.WriteLine(item.Word + "  " + item.Index);
            }
            var endTime = DateTime.Now.Ticks;
            Console.WriteLine("Time : " + (endTime - startTime).ToString());

        }
    }

    //We could simplify the query slightly by 
    //using an anonymous type instead of the 
    //IndexedWord struct. However, this would 
    //degrade performance because anonymous 
    //types (being classes and therefore reference types) 
    //incur the cost of heap-based allocation and subsequent garbage collection.
    //The difference might not be enough to matter with sequential queries, 
    //but with parallel queries, favoring stack-based allocation can be quite 
    //advantageous. 
    //This is because stack-based allocation 
    //is highly parallelizable (as each thread has its own stack), 
    //whereas all threads must compete for the same heap — managed by a single memory manager and garbage collector.
    // 為什麼不用匿名型別，因為匿名型別是一個參考型別，參考型別的物件存在累堆上，而壘堆只有一個
    // 因為struct是存在堆疊上，而每一個執行緒，都有一個堆疊，所以建議使用struct來接資料
    struct IndexedWord { public string Word; public int Index; }
}
