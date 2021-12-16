namespace parallel
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    internal static class Section5Lesson34
    {

        public static void FirstTest()
        {
            int sum = 0;

            Parallel.For(1, 1001,
                () => 0,
                (x, state, tls) =>
                {
                    tls += x;
                    Console.WriteLine($"Task {Task.CurrentId} has a value of {tls}");
                    return tls;
                },
                partialSum =>
                {
                    Console.WriteLine($"Partial value of task {Task.CurrentId} has a value of {partialSum}");
                    Interlocked.Add(ref sum, partialSum);
                });

            Console.WriteLine($"Sum of 1..1000 = {sum}");

            Console.WriteLine("Main program done");
            Console.ReadKey();
        }
    }
}
