namespace parallel
{
    using System;
    using System.Threading;

    internal class Section7Lesson44
    {

        public int CalculateValue()
        {
            Thread.Sleep(5000);
            return 123;
        }

        public static void FirstTest()
        {

            Console.WriteLine("Main program done");
            Console.ReadKey();
        }
    }
}
