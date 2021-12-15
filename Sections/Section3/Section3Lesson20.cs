namespace parallel
{

    // stack LIFO
    // queue FIFO
    // bag No Ordering

    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    internal static class Section3Lesson20
    {

        public static void FirstTest()
        {
            
            var bag = new ConcurrentBag<int>();
            var tasks = new List<Task>();
            for (int i = 0; i < 10; i++)
            {
                var i1 = i;
                tasks.Add(Task.Factory.StartNew(() =>
                {
                    bag.Add(i1);
                    Console.WriteLine($"{Task.CurrentId} has added {i1}");
                    int result;
                    if (bag.TryPeek(out result))
                    {
                        Console.WriteLine($"{Task.CurrentId} has peeked the value {result}");
                    }
                }));
            }

            Task.WaitAll(tasks.ToArray());

            int last;
            if (bag.TryTake(out last))
            {
                Console.WriteLine($"I got {last}");
            }

            Console.WriteLine("Main program done.");
            Console.ReadKey();
        }
    }
}
