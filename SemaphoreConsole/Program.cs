using System.Security.Cryptography;
var count = args.Length > 0 ? int.Parse(args[0]) : 1;

SemaphoreSlim semaphore = new(count, count);
var _sharedResource = 0;

List<Task> tasks = [];

Console.WriteLine($"===== Run without lock =====");

for (int i = 0; i < 5; i++)
{
    var id = i;
    tasks.Add(Run(id));
}

await Task.WhenAll(tasks);

Console.WriteLine($"_sharedResource: {_sharedResource}");

_sharedResource = 0;
Console.WriteLine($"===== Run with lock =====");

for (int i = 0; i < 5; i++)
{
    var id = i;
    tasks.Add(Task.Run(async () =>
    {
        await semaphore.WaitAsync();

        try
        {
            await Run(id);
        }
        finally
        {
            semaphore.Release();
        }
    }));
}

await Task.WhenAll(tasks);

Console.WriteLine($"_sharedResource: {_sharedResource}");

async Task Run(int id)
{
    Console.WriteLine($"Start id: {id}");

    var r = _sharedResource;

    await Task.Delay(RandomNumberGenerator.GetInt32(1000));

    _sharedResource = r + 1;

    Console.WriteLine($"End id: {id}");
}