# lock statement

https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/statements/lock

* in-process
* does not allow `async/await` 

``` sh
dotnet run --project LockConsole
```

# Mutex

https://learn.microsoft.com/en-us/dotnet/api/system.threading.mutex?view=net-8.0

* cross-process. OS level lock

``` sh
# Run both applications on the same machine
dotnet run --project MutexConsole1
dotnet run --project MutexConsole2
```

``` sh
# Run applications on different machines
dotnet run --project MutexConsole1
docker build -f MutexConsole2/Dockerfile -t mutextconsole2:latest .
docker run --rm docker.io/library/mutextconsole2:latest
```

# SemaphoreSlim

https://learn.microsoft.com/en-us/dotnet/api/system.threading.semaphoreslim?view=net-8.0

* in-process
* allow multiple slots 
* allow `async/await`


``` sh
# single slot only
dotnet run --project SemaphoreConsole 1

# 2 slots at a time
dotnet run --project SemaphoreConsole 2
```

# Distributed Lock 

* cross-machine
* backed by a central data store, e.g. Redis or MSSQL
* expiry
 
(TBC)