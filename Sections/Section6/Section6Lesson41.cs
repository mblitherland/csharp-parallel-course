namespace parallel
{
    using System;
    using System.Linq;

    internal static class Section6Lesson41
    {

        public static void FirstTest()
        {
            // var sum = Enumerable.Range(1, 1000).Sum();

            // var sum = Enumerable.Range(1, 1000)
            // .Aggregate(0, (i, acc) => i + acc);

            var sum = ParallelEnumerable.Range(1, 1000)
            .Aggregate(
                0,
                (partialSum, i) => partialSum += i,
                (total, subtotals) => total += subtotals,
                i => i
            );

            Console.WriteLine($"Sum = {sum}");

            Console.WriteLine("Main program done.");
            Console.ReadKey();
        }
    }
}
