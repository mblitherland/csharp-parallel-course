namespace parallel
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    internal static class Section6Lesson38
    {

        public static void FirstTest()
        {
            const int count = 50;

            var items = Enumerable.Range(1, count).ToArray();
            var results = new int[count];

            items.AsParallel().ForAll(x =>
                {
                    int newValue = x*x*x;
                    Console.WriteLine($"{x} => {newValue} (task {Task.CurrentId})\t");
                    results[x - 1] = newValue;
                });

            // foreach (var i in results)
            // {
            //     Console.Write($"{i}\t");
            // }

            // This changes the parallel behavior to try and retain the order
            var cubes = items.AsParallel().AsOrdered().Select(x => x*x*x);
            foreach (var i in cubes)
            {
                Console.Write($"{i}\t");
            }

            Console.WriteLine("Main program done.");
            Console.ReadKey();
        }
    }
}
