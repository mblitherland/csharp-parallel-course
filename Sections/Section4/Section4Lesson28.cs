namespace parallel
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    internal class Section4Lesson28
    {

        public static void FirstTest()
        {
            // Needs to manually reset the event, it won't wait on any subsequent calls once set.
            var evt = new ManualResetEventSlim();

            // AutoResetEvent is similar, but resets on its own.

            Task.Factory.StartNew(() =>
            {
                Console.WriteLine("Boiling water");
                Thread.Sleep(1000);
                Console.WriteLine("Water is boiling");
                evt.Set();
            });

            var makeTea = Task.Factory.StartNew(() =>
            {
                Console.WriteLine("Waiting for water");
                evt.Wait();
                Console.WriteLine("Here is your tea");
                evt.Wait();
                Console.WriteLine("Here is your tea 2");
                evt.Wait();
                Console.WriteLine("Here is your tea 3");
            });

            makeTea.Wait();

            Console.WriteLine("Main program done.");
            Console.ReadKey();
        }

        public static void SecondTest()
        {
            // If set to false it'll be false after a wait
            var evt = new AutoResetEvent(false);
            // If set to true it'll stick to true unless manually reset

            Task.Factory.StartNew(() =>
            {
                Console.WriteLine("Boiling water");
                Thread.Sleep(1000);
                Console.WriteLine("Water is boiling");
                evt.Set();
            });

            var makeTea = Task.Factory.StartNew(() =>
            {
                Console.WriteLine("Waiting for water");
                evt.WaitOne();
                Console.WriteLine("Here is your tea");
                // Wait one second
                var ok = evt.WaitOne(1000);
                if (ok)
                {
                    Console.WriteLine("Here is your tea 2");
                }
                else
                {
                    Console.WriteLine("No tea for you.");
                }
                // This stops here, because it automatically resets
                evt.WaitOne();
                Console.WriteLine("Here is your tea 3");
            });

            makeTea.Wait();

            Console.WriteLine("Main program done.");
            Console.ReadKey();
        }
    }
}
