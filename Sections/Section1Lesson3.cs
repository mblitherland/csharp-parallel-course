namespace parallel
{
    using System;
    using System.Threading.Tasks;

    internal static class Section1Lesson3
    {

        public static void Write(char c)
        {
            for (int i = 1000; i > 0; i--)
            {
                Console.Write(c);
            }
        }

        public static void FirstTest()
        {
            Task.Factory.StartNew(() => Write('.'));

            var t = new Task(() => Write('?'));
            t.Start();

            Write('-');

            Console.WriteLine("Main program done.");
            Console.ReadKey();
        }

        public static void Write(object o)
        {
            for (int i = 1000; i > 0; i--)
            {
                Console.Write(o);
            }
        }

        public static void SecondTest()
        {
            Task t = new Task(Write, "hi");
            t.Start();
            Task.Factory.StartNew(Write, 123);
            Console.WriteLine("Main program done.");
            Console.ReadKey();
        }

        public static int TextLength(object o)
        {
            Console.WriteLine($"\nTask with id {Task.CurrentId} processing object {o}");
            return o.ToString().Length;
        }

        public static void ThirdTest()
        {
            string text1 = "testing", text2 = "this";
            var task1 = new Task<int>(TextLength, text1);
            task1.Start();
            Task<int> task2 = Task.Factory.StartNew(TextLength, text2);

            Console.WriteLine($"Length of '{text1}' is {task1.Result}");
            Console.WriteLine($"Length of '{text2}' is {task2.Result}");

            Console.WriteLine("Main program done.");
            Console.ReadKey();
        }

    }

}
