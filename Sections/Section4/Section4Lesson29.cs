namespace parallel
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    internal static class Section4Lesson29
    {

        public static void FirstTest()
        {
            var semaphore = new SemaphoreSlim(2, 10);

            for (int i = 0; i < 20; i++)
            {
                Task.Factory.StartNew(() =>
                {
                    Console.WriteLine($"Entering task {Task.CurrentId}");
                    // ReleaseCount in the semaphore decreases
                    semaphore.Wait();
                    Console.WriteLine($"Processing task {Task.CurrentId}");
                });
            }

            while (semaphore.CurrentCount <= 2)
            {
                Console.WriteLine($"Semaphore count: {semaphore.CurrentCount}");
                Console.ReadKey();
                semaphore.Release(2);
            }

            Console.WriteLine("Main program done.");
            Console.ReadKey();
        }
    }
}
