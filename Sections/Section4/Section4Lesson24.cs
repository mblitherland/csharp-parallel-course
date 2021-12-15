namespace parallel
{
    using System;
    using System.Threading.Tasks;

    internal static class Section4Lesson24
    {

        public static void SecondTest()
        {
            var task1 = Task.Factory.StartNew(() => "Task 1");
            var task2 = Task.Factory.StartNew(() => "Task 2");

            // There is also a ContinueWhenAny that returns when the first task is done
            var task3 = Task.Factory.ContinueWhenAll(new [] {task1, task2},
            tasks =>
            {
                Console.WriteLine("Tasks completed:");
                foreach (var t in tasks)
                {
                    Console.WriteLine($" - {t.Result}");
                }
                Console.WriteLine("All tasks done");
            });

            task3.Wait();

            Console.WriteLine("Main program done.");
            Console.ReadKey();
        }

        public static void FirstTest()
        {
            var task = Task.Factory.StartNew(() =>
            {
                Console.WriteLine("Boiling water");
            });

            task.ContinueWith(t =>
            {
                Console.WriteLine($"Completed task {t.Id}, pour water into cup");
            });

            Console.WriteLine("Main program done.");
            Console.ReadKey();
        }
    }
}
