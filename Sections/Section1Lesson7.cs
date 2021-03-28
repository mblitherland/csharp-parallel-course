namespace parallel
{
    using System;
    using System.Threading.Tasks;

    internal static class Section1Lesson7
    {
        public static void Demo()
        {
            var t = Task.Factory.StartNew(() => 
            {
                throw new InvalidOperationException("Can't do this!") { Source = "t" };
            });

            var t2 = Task.Factory.StartNew(() => 
            {
                throw new AccessViolationException("Can't access this!") { Source = "t2" };
            });

            try
            {
                Task.WaitAll(t, t2);
            }
            catch (AggregateException ae)
            {
                // foreach (var e in ae.InnerExceptions)
                // {
                //     Console.WriteLine($"Excpetion {e.GetType()} from {e.Source}");
                // }
                ae.Handle(e => {
                    if (e is InvalidOperationException)
                    {
                        Console.WriteLine("Invalid op!");
                        return true;
                    }
                    else{
                        return false;
                    }
                });
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
                foreach (var e in ae.InnerExceptions)
                {
                    Console.WriteLine($"Handled elsewhere: {e.GetType()}");
                }
            }

            Console.WriteLine("Main program done");
            Console.ReadKey();
        }
    }
}
