using System;
using System.IO;
using System.Net.Http;
using System.Threading;

public class FileProcessor
{
    private int _failCount = 0;

    public void UpdateConfig(string path, string key, string value)
    {
        _failCount++;

        Console.WriteLine($"[FileProcessor] Спроба #{_failCount}");

        if (_failCount <= 2)
        {
            throw new IOException("Тимчасова помилка доступу до файлу.");
        }

        Console.WriteLine($"Файл '{path}' оновлено: {key} = {value}");
    }
}

public class NetworkClient
{
    private int _failCount = 0;

    public bool SendConfigUpdate(string url, string configJson)
    {
        _failCount++;

        Console.WriteLine($"[NetworkClient] Спроба #{_failCount}");

        if (_failCount <= 3)
        {
            throw new HttpRequestException("Тимчасова мережева помилка.");
        }

        Console.WriteLine($"Конфігурацію успішно відправлено на {url}");
        return true;
    }
}

public static class RetryHelper
{
    public static T ExecuteWithRetry<T>(
        Func<T> operation,
        int retryCount = 3,
        TimeSpan initialDelay = default,
        Func<Exception, bool> shouldRetry = null)
    {
        if (initialDelay == default)
            initialDelay = TimeSpan.FromMilliseconds(500);

        int attempt = 0;

        while (true)
        {
            try
            {
                attempt++;
                Console.WriteLine($"\n--- Виконання спроби {attempt} ---");
                return operation();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка: {ex.GetType().Name} – {ex.Message}");

                if (attempt > retryCount || (shouldRetry != null && !shouldRetry(ex)))
                {
                    Console.WriteLine("Повторні спроби припинено.\n");
                    throw;
                }

                var delay = TimeSpan.FromMilliseconds(initialDelay.TotalMilliseconds * Math.Pow(2, attempt - 1));
                Console.WriteLine($"Очікування {delay.TotalMilliseconds} мс перед повторною спробою...");
                Thread.Sleep(delay);
            }
        }
    }
}

class Program
{
    static void Main()
    {
        var fileProcessor = new FileProcessor();
        var networkClient = new NetworkClient();

        Func<Exception, bool> retryCondition = (ex) =>
            ex is IOException || ex is HttpRequestException;

        Console.WriteLine("=== Оновлення конфігурації (FileProcessor) ===");
        RetryHelper.ExecuteWithRetry(
            () => {
                fileProcessor.UpdateConfig("config.json", "LogLevel", "Debug");
                return true;
            },
            retryCount: 5,
            initialDelay: TimeSpan.FromMilliseconds(300),
            shouldRetry: retryCondition
        );

        Console.WriteLine("\n=== Надсилання оновлення (NetworkClient) ===");
        RetryHelper.ExecuteWithRetry(
            () => networkClient.SendConfigUpdate("https://api.example.com/update", "{ \"LogLevel\": \"Debug\" }"),
            retryCount: 5,
            initialDelay: TimeSpan.FromMilliseconds(300),
            shouldRetry: retryCondition
        );

        Console.WriteLine("\n=== Роботу завершено! ===");
    }
}
