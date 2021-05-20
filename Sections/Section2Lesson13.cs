namespace parallel
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    internal static class Section2Lesson13
    {

        public class BankAccountTheFourth
        {
            private int balance;

            public int Balance
            {
                get { return balance; }
                private set { balance = value; }
            }

            public void Deposit(int amount)
            {
                balance += amount;
            }

            public void Withdraw(int amount)
            {
                balance -= amount;
            }

            public void Transfer(BankAccountTheFourth where, int amount)
            {
                Balance -= amount;
                where.Deposit(amount);
            }
        }

        public static void FirstTest()
        {
            var tasks = new List<Task>();
            var ba = new BankAccountTheFourth();
            var ba2 = new BankAccountTheFourth();

            Mutex mutex = new Mutex();
            Mutex mutex2 = new Mutex();

            for (int i = 0; i < 10; i++)
            {
                tasks.Add(Task.Factory.StartNew(() =>
                {
                    for (int j = 0; j < 1000; j++)
                    {
                        bool haveLock = mutex.WaitOne();
                        try
                        {
                            ba.Deposit(1);
                        }
                        finally
                        {
                            if (haveLock)
                            {
                                mutex.ReleaseMutex();
                            }
                        }
                    }
                }));
                tasks.Add(Task.Factory.StartNew(() =>
                {
                    for (int j = 0; j < 1000; j++)
                    {
                        bool haveLock = mutex2.WaitOne();
                        try
                        {
                            ba2.Deposit(1);
                        }
                        finally
                        {
                            if (haveLock)
                            {
                                mutex2.ReleaseMutex();
                            }
                        }
                    }
                }));
                tasks.Add(Task.Factory.StartNew(() =>
                {
                    for (int i = 0; i < 1000; i++)
                    {
                        bool haveLock = WaitHandle.WaitAll(new[] { mutex, mutex2 });
                        try
                        {
                            ba.Transfer(ba2, 1);
                        }
                        finally
                        {
                            if (haveLock)
                            {
                                mutex.ReleaseMutex();
                                mutex2.ReleaseMutex();
                            }
                        }
                    }
                }));
            }

            Task.WaitAll(tasks.ToArray());
            Console.WriteLine($"Final balances are ba: {ba.Balance}, ba2: {ba2.Balance}.");
            Console.WriteLine("Main program done.");
            Console.ReadKey();
        }
             
        // This doesn't seem to work as described in the class. I'm not sure if
        // it's related to WSL or .net5.0 or there's some other error. It looks
        // correct compared to the demo.
        public static void SecondTest()
        {
            const string appName = "Section2Lession13";
            Mutex mutex;

            try
            {
                mutex = Mutex.OpenExisting(appName);
                Console.WriteLine($"Sorry, {appName} is already running");
            }
            catch (WaitHandleCannotBeOpenedException)
            {
                Console.WriteLine("We can run the program just fine");
                mutex = new Mutex(false, appName);
            }

            Console.WriteLine("Waiting on key press");
            Console.ReadKey();
            mutex.ReleaseMutex();
        }
    }
}
