Mutex mutex = new Mutex(false, "OneTrueLock");
Console.WriteLine("Acquiring mutex for MutexConsole1");
mutex.WaitOne();
Console.WriteLine("Mutex acquired by MutexConsole1.");
Console.ReadLine();
mutex.ReleaseMutex();
Console.WriteLine("Mutex released by MutexConsole1");