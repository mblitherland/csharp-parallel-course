namespace parallel
{
    using System;
    using System.Collections.Concurrent;
    using System.Linq;
    using System.Threading.Tasks;
    using BenchmarkDotNet.Attributes;
    using BenchmarkDotNet.Running;

    public class Section5Lesson35
    {

        [Benchmark]
        public void SquareEachValue()
        {
            const int count = 100000;
            var values = Enumerable.Range(0, count);
            var results = new int[count];
            Parallel.ForEach(values, x => { results[x] = (int) Math.Pow(x, 2); });
        }

        [Benchmark]
        public void SquareEachValueChunked()
        {
            const int count = 100000;
            var values = Enumerable.Range(0, count);
            var results = new int[count];

            var part = Partitioner.Create(0, count, 10000);

            Parallel.ForEach(part, range =>
            {
                for (int i = range.Item1; i < range.Item2; i++)
                {
                    results[i] = (int) Math.Pow(i, 2);
                }

            });
        }

        public static void FirstTest()
        {
            var summary = BenchmarkRunner.Run<Section5Lesson35>();
            Console.WriteLine(summary);

            Console.WriteLine("Main program done");
            Console.ReadKey();
        }
    }
}
