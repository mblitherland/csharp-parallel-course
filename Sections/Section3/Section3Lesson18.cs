namespace parallel
{
    using System;
    using System.Collections.Concurrent;

    internal static class Section3Lesson18
    {

        public static void FirstTest()
        {
            var q = new ConcurrentQueue<int>();
            q.Enqueue(1);
            q.Enqueue(2);

            int result;
            if (q.TryDequeue(out result))
            {
                Console.WriteLine($"Removed element {result}");
            }
            else
            {
                Console.WriteLine("Failed to remove an element from the queue");
            }

            if (q.TryPeek(out result))
            {
                Console.WriteLine($"Front of the queue is {result}");
            }
            else
            {
                Console.WriteLine("Failed to try peek.");
            }

            Console.WriteLine("Main program done.");
            Console.ReadKey();
        }
    }
}
