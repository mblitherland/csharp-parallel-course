namespace parallel
{
    using System;
    using System.Threading;

    internal static class Section2Lesson14
    {
        static ReaderWriterLockSlim padlock = new ReaderWriterLockSlim();

        public static void FirstTest()
        {


            Console.WriteLine("Main program done.");
            Console.ReadKey();
        }
    }
}
