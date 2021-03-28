namespace parallel
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    internal static class Section1Lesson6
    {
        public static void FirstTest()
        {
            var cts = new CancellationTokenSource();
            var token = cts.Token;

            var t = new Task(() => {
                Console.WriteLine("I take 5 seconds");

                for (int i = 0; i < 5; i ++)
                {
                    token.ThrowIfCancellationRequested();
                    Thread.Sleep(1000);
                }

                Console.WriteLine("I'm done");
            });
            t.Start();

            Task t2 = Task.Factory.StartNew(() => Thread.Sleep(3000), token);

            // Wait for all tasks to complete
            // Task.WaitAll(t, t2);

            // Wait for the first task to complete
            Task.WaitAny(t, t2);

            Console.WriteLine($"Task t  status is {t.Status}");
            Console.WriteLine($"Task t2 status is {t2.Status}");

            // Other ways to wait on a task
            // t.Wait();
            // t.Wait(token);

            Console.WriteLine("Main program done.");
            Console.ReadKey();
        }
    }
}
