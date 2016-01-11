using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PLINQDemo.ParallelSpellcheckerFolder
{
    public class Spellchecker
    {
        public void Run() 
        {
            var wordLookup = new HashSet<string>(
              File.ReadAllLines("WordLookup.txt"),
              StringComparer.InvariantCultureIgnoreCase);            
            
            // 載入150000字
            string[] wordList = wordLookup.ToArray();

            // 將字隨機取100W個出來
            var startTime2 = DateTime.Now.Ticks;

            var random = new Random();
            string[] wordsToTest = Enumerable.Range(0, 1000000)
              .Select(i => wordList[random.Next(0, wordList.Length)])
              .ToArray();

            var endTime2 = DateTime.Now.Ticks;
            Console.WriteLine("WordsToTest Time : " + (endTime2 - startTime2).ToString());

            // 亂打兩個值，然後找出這兩個值
            wordsToTest[12345] = "woozsh";     // Introduce a couple
            wordsToTest[23456] = "wubsie";     // of spelling mistakes.

            var query = wordsToTest
                      .Select((word, index) => new IndexedWord { Word = word, Index = index })
                      .Where(iword => !wordLookup.Contains(iword.Word)) // 文章有說，Contains 讓query 能觸發平行處理的一個點
                      .OrderBy(iword => iword.Index);

            var startTime = DateTime.Now.Ticks;
            foreach (var item in query)
            {
                Console.WriteLine(item.Word + "  " + item.Index);
            }
            var endTime = DateTime.Now.Ticks;
            Console.WriteLine("Query Time : " + (endTime - startTime).ToString());

        }
    }

}
