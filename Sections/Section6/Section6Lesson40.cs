namespace parallel
{
    using System;
    using System.Linq;

    internal static class Section6Lesson40
    {

        public static void FirstTest()
        {
            var numbers = Enumerable.Range(1, 20).ToArray();

            var results = numbers
            .AsParallel()
            // To control merge options
            .WithMergeOptions(ParallelMergeOptions.NotBuffered)
            .Select(x =>
            {
                var result = Math.Log10(x);
                Console.WriteLine($"Produced {result}");
                return result;
            });

            foreach (var result in results)
            {
                Console.WriteLine($"Consumed {result}");
            }

            Console.WriteLine("Main program done.");
            Console.ReadKey();
        }
    }
}
