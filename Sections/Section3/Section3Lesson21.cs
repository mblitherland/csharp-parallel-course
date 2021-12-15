using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace parallel
{

    internal static class Section3Lesson21
    {

        static BlockingCollection<int> messages = 
            new BlockingCollection<int>(new ConcurrentBag<int>(), 10);

        static CancellationTokenSource cts = new CancellationTokenSource();

        static Random random = new Random();

        public static void FirstTest()
        {
            var test = Task.Factory.StartNew(ProduceAndConsume, cts.Token);

            Console.WriteLine("Hit any key to terminate.");
            Console.ReadKey();
            cts.Cancel();
        }

        public static void ProduceAndConsume()
        {
            var consumer = Task.Factory.StartNew(RunConsumer);
            var producer = Task.Factory.StartNew(RunProducer);

            try
            {
                Task.WaitAll(new[] {producer, consumer}, cts.Token);
            }
            catch (AggregateException ae)
            {
                ae.Handle(e => true);
            }
        }

        private static void RunConsumer()
        {
            foreach (var item in messages.GetConsumingEnumerable())
            {
                cts.Token.ThrowIfCancellationRequested();
                Console.WriteLine($"-{item}\t");
                Thread.Sleep(random.Next(1000));
            }
        }

        private static void RunProducer()
        {
            while (true)
            {
                cts.Token.ThrowIfCancellationRequested();
                int i = random.Next(100);
                messages.Add(i);
                Console.WriteLine($"+{i}\t");
                // Having a shorter sleep here will cause the collection to fill up
                // and adding more will block until one is read.
                Thread.Sleep(random.Next(100));
            }
        }
    }
}
