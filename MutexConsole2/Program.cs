Mutex mutex = new Mutex(false, "OneTrueLock");
Console.WriteLine("Acquiring mutex for MutexConsole2");
mutex.WaitOne();
Console.WriteLine("Mutex acquired by MutexConsole2");
mutex.ReleaseMutex();
Console.WriteLine("Mutex released by MutexConsole2");