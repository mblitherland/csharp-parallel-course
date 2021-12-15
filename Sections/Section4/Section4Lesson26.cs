namespace parallel
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    internal static class Section4Lesson26
    {

        static Barrier barrier = new Barrier(2, b =>
        {
            Console.WriteLine($"Phase number {b.CurrentPhaseNumber} is finished");
        });

        public static void Water()
        {
            Console.WriteLine("Putting the kettle on (takes a bit longer)");
            Thread.Sleep(2000);
            // Want to pour the water, but need a cup here.
            barrier.SignalAndWait();
            Console.WriteLine("Pouring water into the cup");
            barrier.SignalAndWait();
            Console.WriteLine("Putting the kettle away");
        }

        public static void Cup()
        {
            Console.WriteLine("Finding the nice cup (fast)");
            // Want the water to be boiling
            barrier.SignalAndWait();
            Console.WriteLine("Adding tea");
            barrier.SignalAndWait();
            Console.WriteLine("Adding the sugar");
        }

        public static void FirstTest()
        {
            var water = Task.Factory.StartNew(Water);
            var cup = Task.Factory.StartNew(Cup);

            var tea = Task.Factory.ContinueWhenAll(new [] {water, cup}, tasks =>
            {
                Console.WriteLine("Enjoy your cup of tea");
            });

            tea.Wait();

            Console.WriteLine("Main program done.");
            Console.ReadKey();
        }
    }
}
