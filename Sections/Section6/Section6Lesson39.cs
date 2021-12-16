namespace parallel
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    internal static class Section6Lesson39
    {

        public static void FirstTest()
        {
            var cts = new CancellationTokenSource();

            var items = ParallelEnumerable.Range(1, 20);

            var results = items.WithCancellation(cts.Token).Select(x =>
            {
                double result = Math.Log10(x);

                if (result > 1)
                {
                    // throw new InvalidOperationException();
                    cts.Cancel();
                }

                Console.WriteLine($"x = {x}, tid = {Task.CurrentId}");
                return result;
            });

            try
            {
                foreach (var c in results)
                {
                    Console.WriteLine($"result = {c}");
                }
            }
            catch (AggregateException ae)
            {
                ae.Handle(e =>
                {
                    Console.WriteLine($"{e.GetType().Name}: {e.Message}");
                    return true;
                });
            }
            catch (OperationCanceledException oce)
            {
                Console.WriteLine($"{oce.GetType().Name}: {oce.Message}");
            }


            Console.WriteLine("Main program done");
            Console.ReadKey();
        }
    }
}
