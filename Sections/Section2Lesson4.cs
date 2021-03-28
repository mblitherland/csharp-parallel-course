using System;
using System.Diagnostics;
using System.Reflection.Metadata;
using System.Threading;
using System.Threading.Tasks;

namespace parallel
{

    internal static class Section2Lesson4
    {

        public static void FirstTest()
        {
            var cts = new CancellationTokenSource();
            var token = cts.Token;

            token.Register(() =>
            {
                Console.WriteLine("Cancellation has been requested.");
            });

            var t = new Task(() =>
            {
                int i = 0;
                while(true)
                {
                    // if (token.IsCancellationRequested)
                    // {
                    //     break;
                    // }

                    // This is the cannonical way of handling the cancellation.
                    token.ThrowIfCancellationRequested();
                    Console.WriteLine($"{i++}\t");
                }
            }, token);
            t.Start();

            Task.Factory.StartNew(() =>
            {
                token.WaitHandle.WaitOne();
                Console.WriteLine("Wait handle released, cancellation is requested");
            });

            Console.ReadKey();
            cts.Cancel();

            Console.WriteLine("Main program done.");
            Console.ReadKey();
        }

        public static void SecondTest()
        {
            var planned = new CancellationTokenSource();
            var preventative = new CancellationTokenSource();
            var emergency = new CancellationTokenSource();

            var paranoid = CancellationTokenSource.CreateLinkedTokenSource(
                planned.Token, preventative.Token, emergency.Token);

            Task.Factory.StartNew(() =>
            {
                int i = 0;
                while (true)
                {
                    paranoid.Token.ThrowIfCancellationRequested();
                    Console.WriteLine($"{i++}\t");
                    Thread.Sleep(1000);
                }
            });

            Console.ReadKey();
            emergency.Cancel();
            Console.WriteLine("Emergency cancel requested.");

            Console.WriteLine("Main program done.");
            Console.ReadKey();
        }
    }
}
