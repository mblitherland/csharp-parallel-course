namespace parallel
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    internal static class Section4Lesson25
    {

        public static void FirstTest()
        {
            var parent = new Task(() =>
            {
                // detached - this child is in the same pool alongside the parent
                // var child = new Task(() =>
                // {
                //     Console.WriteLine("Child task starting");
                //     Thread.Sleep(3000);
                //     Console.WriteLine("Child task finished");
                // });
                
                // this child is attached to the parent
                var child = new Task(() =>
                {
                    Console.WriteLine("Child task starting");
                    Thread.Sleep(3000);
                    Console.WriteLine("Child task finished");
                    // Uncomment to get to faulted state
                    // throw new Exception();
                }, TaskCreationOptions.AttachedToParent);

                var completionHandler = child.ContinueWith(t =>
                {
                    Console.WriteLine($"Hooray, task {t.Id}'s state is {t.Status}");
                }, TaskContinuationOptions.AttachedToParent | TaskContinuationOptions.OnlyOnRanToCompletion);

                var failHandler = child.ContinueWith(t =>
                {
                    Console.WriteLine($"Oops, task {t.Id}'s state is {t.Status}");
                }, TaskContinuationOptions.AttachedToParent | TaskContinuationOptions.OnlyOnFaulted);

                child.Start();
            });

            parent.Start();

            try
            {
                parent.Wait();
            }
            catch (AggregateException ae)
            {
                ae.Handle(e => true);
            }

            Console.WriteLine("Main program done.");
            Console.ReadKey();
        }
    }
}
