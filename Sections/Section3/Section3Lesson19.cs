namespace parallel
{
    using System;
    using System.Collections.Concurrent;
    using System.Linq;

    internal static class Section3Lesson19
    {

        public static void FirstTest()
        {
            var stack = new ConcurrentStack<int>();
            stack.Push(1);
            stack.Push(2);
            stack.Push(3);
            stack.Push(4);

            int result;
            if (stack.TryPeek(out result))
            {
                Console.WriteLine($"Peeked variable {result}");
            }
            else
            {
                Console.WriteLine("Failed to get a peek");
            }

            if (stack.TryPop(out result))
            {
                Console.WriteLine($"Popped variable {result}");
            }
            else
            {
                Console.WriteLine("Didn't manage to pop a variable");
            }

            var items = new int[5];
            if (stack.TryPopRange(items, 0, 5) > 0)
            {
                var text = string.Join(", ", items.Select(i => i.ToString()));
                Console.WriteLine($"Got some items, yo, {text}");
            }
            else
            {
                Console.WriteLine("Couldn't pop range");
            }

            stack.Push(5);
            stack.Push(6);
            stack.Push(7);
            items = new int[5];
            var popped = stack.TryPopRange(items, 0, 5);
            if (popped > 0)
            {
                var text = string.Join(", ", items.Take(popped).Select(i => i.ToString()));
                Console.WriteLine($"Popped this new range: {text}. Contained {popped} items");
            }
            else
            {
                Console.WriteLine("Failed to pop range");
            }


            Console.WriteLine("Main program done.");
            Console.ReadKey();
        }
    }
}
