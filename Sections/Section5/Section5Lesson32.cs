namespace parallel
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    internal static class Section5Lesson32
    {
        public static IEnumerable<int> Range(int start, int end, int step)
        {
            for (int i = start; i < end; i += step)
            {
                yield return i;
            }
        }

        public static void FirstTest()
        {
            // Can be passed to the various parallel invocations
            var po = new ParallelOptions();

            var a = new Action(() => Console.WriteLine($"First {Task.CurrentId}"));
            var b = new Action(() => Console.WriteLine($"Second {Task.CurrentId}"));
            var c = new Action(() => Console.WriteLine($"Third {Task.CurrentId}"));

            Parallel.Invoke(a, b, c);

            // Lower bound is inclusive and the upper is exclusive
            Parallel.For(1, 11, i =>
            {
                Console.WriteLine($"{i*i} (task {Task.CurrentId})\t");
            });

            string[] words = {"oh", "what", "a", "night"};

            Parallel.ForEach(words, word =>
            {
                Console.WriteLine($"{word} is length {word.Length} (task {Task.CurrentId})");
            });

            Parallel.ForEach(Range(1, 20, 3), i =>
            {
                Console.WriteLine($"{i} (task {Task.CurrentId})\t");
            });

            Console.WriteLine("Main program complete.");
            Console.ReadKey();
        }
    }
}
