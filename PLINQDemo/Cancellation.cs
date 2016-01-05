using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PLINQDemo
{
    public class Cancellation
    {
        public void Run()
        {
            // 執行foreach loop取消PLINQ執行，是非常簡單的，簡單的方式可以用break;
            //// 查詢會自動執行enumerator的Disposed，且取消 (這段不確定是不是這樣。)

            // 用cancellation token 可從其他的執行緒取消執行。 insert a token, 使用WithCancellation after calling AsParallel
            // 然後傳述 CancellationTokenSource的Token property。其他的執行去可以在token source上呼叫取消，並且throws OperationCanceledException

            // 取消有兩種方式
            // 第一個範例說明如何取消大多由資料周遊所組成的查詢
            // 第二個範例說明如何取消使用者涵是在計算時需耗費高度資源的查詢

            // 範例一:
            int[] source = Enumerable.Range(1, 10000000).ToArray();
            CancellationTokenSource cts = new CancellationTokenSource();

            // 通常會在按下按鈕後，或者是某一個事件後做取消的動作
            // 這在邊先開一個非同步任務作取消的觸發動作
            Task.Factory.StartNew(
                () => this.UserClicksTheCancelButton(cts)
            );

            int[] results = null;
            try
            {
                results = (from num in source.AsParallel().WithCancellation(cts.Token)
                           where num % 3 == 0
                           orderby num descending
                           select num).ToArray();
            }
            catch (OperationCanceledException e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                cts.Dispose();
            }

            // 取消的一般指引如下
            // 1.如果您執行使用者委派取消，則應向 PLINQ 通知有關外部 CancellationToken 的資訊，並擲回 OperationCanceledException (externalCT)。
            // 2.如果取消執行，且未擲回其他例外狀況，則您應處理 OperationCanceledException，而不是 AggregateException。

        }

        private void UserClicksTheCancelButton(CancellationTokenSource cts)
        {
            // 使用者等了 300~500 ms ，按取消的事件
            Random rand = new Random();
            Thread.Sleep(rand.Next(300, 500));
            cts.Cancel();
        }

        public void Run2()
        {
            // 第二個範例說明如何取消使用者涵是在計算時需耗費高度資源的查詢

            int[] source = Enumerable.Range(1, 1000000).ToArray();

            using (CancellationTokenSource cts = new CancellationTokenSource())
            {
                // 通常會在按下按鈕後，或者是某一個事件後做取消的動作
                // 這在邊先開一個非同步任務作取消的觸發動作
                Task.Factory.StartNew(
                    () => this.UserClicksTheCancelButton2(cts)
                );

                double[] results = null;

                try
                {
                    results = (from num in source.AsParallel().WithCancellation(cts.Token)
                               where num % 3 == 0
                               select Function(num, cts.Token)).ToArray();
                }
                catch (OperationCanceledException e)
                {
                    Console.WriteLine(e.Message);
                }
                catch (AggregateException ae) 
                {
                    if (ae.InnerExceptions != null)
                    {
                        foreach (Exception e in ae.InnerExceptions)
                        {
                            Console.WriteLine(e.Message);
                        }
                    }
                }

                if (results != null)
                {
                    foreach (var item in results)
                    {
                        Console.WriteLine(item);
                    }
                }

                //Console.WriteLine();
                //Console.ReadKey();
            }
                        
        }

        // 同步執行的方法
        private double Function(int num, CancellationToken cts)
        {
            for (int i = 0; i < 5; i++)
            {
                // 假設執行很長時間
                Thread.SpinWait(5000000);

                // 檢查是否取消了
                cts.ThrowIfCancellationRequested();                
            }

            return Math.Sqrt(num);
        }

        private void UserClicksTheCancelButton2(CancellationTokenSource cts)
        {            
            //Random rand = new Random();
            //Thread.Sleep(rand.Next(300, 500));
            Console.WriteLine("Press 'c' to cancel");
            if (Console.ReadKey().KeyChar == 'c')
            {
                cts.Cancel();
            }
        }
    }
}

//IEnumerable<int> million = Enumerable.Range(3, 100000);
//var cancelSource = new CancellationTokenSource();

//var primeNumberQuery = from n
//                       in million.AsParallel().WithCancellation(cancelSource.Token)
//                       where Enumerable.Range(2, (int)Math.Sqrt(n)).All(i => n % i > 0) // Sqrt 為平方根
//                       select n;