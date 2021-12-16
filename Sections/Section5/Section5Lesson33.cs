namespace parallel
{
    using System;
    using System.Threading.Tasks;

    internal static class Section5Lesson33
    {
        private static ParallelLoopResult result;

        public static void Demo()
        {
            // you can use a cancellation token and add it in a parallel options object

            result = Parallel.For(0, 20, (x, state) =>
            {
                Console.WriteLine($"{x}[{Task.CurrentId}]");

                if (x == 2)
                {
                    // Polite
                    state.Break();
                    // Less polite
                    // state.Stop();
                    // RUDE!
                    // throw new Exception();
                }
            });

            Console.WriteLine($"Was loop completed? {result.IsCompleted}");
            if (result.LowestBreakIteration.HasValue)
            {
                Console.WriteLine($"Lowest break iteration is {result.LowestBreakIteration}");
            }

        }

        public static void FirstTest()
        {
            try
            {
                Demo();
            }
            catch (AggregateException ae)
            {
                ae.Handle(e =>
                {
                    Console.WriteLine(e.Message);
                    return true;
                });
            }

            Console.WriteLine("Main program done.");
            Console.ReadKey();
        }

    }

}
