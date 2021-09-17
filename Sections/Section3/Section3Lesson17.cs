namespace parallel
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    internal static class Section3Lesson17
    {
        private static ConcurrentDictionary<string, string> capitals = new ConcurrentDictionary<string, string>();

        private static void AddParis()
        {
            bool status = capitals.TryAdd("France", "Paris");
            string who = Task.CurrentId.HasValue ? $"Task {Task.CurrentId}" : "Main thread";
            Console.WriteLine($"{who} {(status ? "added" : "did not add")} the element");
        }

        public static void FirstTest()
        {
            // Add the element in thread and the method will report back who successfully added it.
            var tasks = new List<Task>();
            for (int i = 0; i < 10; i++)
            {
                tasks.Add(Task.Factory.StartNew(AddParis));
            }
            // Adding directly works of course.
            // AddParis();
            Task.WaitAll(tasks.ToArray());

            capitals["Russia"] = "Leningrad";
            ShowCapital("Russia");

            capitals["Russia"] = "Petrograd";
            ShowCapital("Russia");
            ShowCapital("France");

            capitals.AddOrUpdate("Russia", "Moscow",
                (key, oldValue) => $"{oldValue} -> Moscow");
            ShowCapital("Russia");

            capitals["Sweden"] = "Uppsala";
            var capitalOfSweden = capitals.GetOrAdd("Sweden", "Stockholm");
            Console.WriteLine($"Capital of Sweden is {capitalOfSweden}");
            ShowCapital("Sweden");

            const string toRemove = "Russia";
            string removed;
            var didRemove = capitals.TryRemove("Russia", out removed);
            if (didRemove)
            {
                Console.WriteLine($"I removed {removed}");
            }
            else
            {
                Console.WriteLine($"I failed to remove the capital of {toRemove}");
            }
            ShowCapital(toRemove);

            Console.WriteLine("Iterating through capitals");
            foreach (var entry in capitals)
            {
                Console.WriteLine($" - {entry.Value} is the capital of {entry.Key}");
            }

            Console.WriteLine("Main program done.");
            Console.ReadKey();
        }

        public static void ShowCapital(string capital)
        {
            string matchedCapital;
            var didGet = capitals.TryGetValue(capital, out matchedCapital);
            if (didGet)
            {
                Console.WriteLine($"{capital}: {matchedCapital}");
            }
            else
            {
                Console.WriteLine($"Didn't find a match for {capital}");
            }
        }
    }
}
