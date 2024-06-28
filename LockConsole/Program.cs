using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

object _lock = new object();
var _sharedResource = 0;

await RunWithoutLock();

_sharedResource = 0;

await RunWithLock(_lock);

async Task RunWithoutLock()
{
    Console.WriteLine($"===== Run without lock =====");

    List<Task> tasks = new List<Task>();

    for (int i = 0; i < 5; i++)
    {
        var id = i;
        tasks.Add(Task.Run(() => Run(id)));
    }

    await Task.WhenAll(tasks);

    Console.WriteLine($"_sharedResource: {_sharedResource}");
}

async Task RunWithLock(object _lock)
{
    Console.WriteLine($"===== Run with lock =====");

    List<Task> tasks = new List<Task>();

    for (int i = 0; i < 5; i++)
    {
        var id = i;
        tasks.Add(Task.Run(() =>
        {
            //  


            lock (_lock)
            {
                Run(id); // does not support async/await, e.g. await Task.Delay(100);
            }
        }));
    }

    await Task.WhenAll(tasks);

    Console.WriteLine($"_sharedResource: {_sharedResource}");
}

void Run(int id)
{
    Console.WriteLine($"Start id: {id}");

    var r = _sharedResource;

    HeavyComputation();

    _sharedResource = r + 1;

    Console.WriteLine($"End id: {id}");
}

void HeavyComputation()
{
    var count = RandomNumberGenerator.GetInt32(100);
    for (int i = 0; i < count; i++)
    {
        byte[] salt = RandomNumberGenerator.GetBytes(128 / 8);
        string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: "some_password",
            salt: salt,
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 100000,
            numBytesRequested: 256 / 8));
    }
}
