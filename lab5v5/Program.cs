using System;
using System.Collections.Generic;
using System.Linq;

// Власний виняток для некоректного рейтингу
public class InvalidRatingException : Exception
{
    public InvalidRatingException(string message) : base(message) { }
}

// Клас, що представляє рейтинг
public class Rating
{
    public double Value { get; set; }
    public string User { get; set; }

    public Rating(double value, string user)
    {
        if (value < 0 || value > 10)
            throw new InvalidRatingException("Рейтинг повинен бути в межах від 0 до 10.");
        Value = value;
        User = user;
    }
}

// Клас фільму, який містить список рейтингів
public class Movie
{
    public string Title { get; set; }
    public List<Rating> Ratings { get; set; } = new List<Rating>();

    public Movie(string title)
    {
        Title = title;
    }

    // Метод для додавання рейтингу до фільму
    public void AddRating(Rating rating)
    {
        Ratings.Add(rating);
    }

    // Обчислення середнього рейтингу з відсіканням мінімуму та максимуму (якщо рейтингів >= 5)
    public double GetAverageRating()
    {
        if (Ratings.Count < 5)
            return Ratings.Average(r => r.Value);

        var orderedRatings = Ratings.OrderBy(r => r.Value).Skip(1).Take(Ratings.Count - 2);
        return orderedRatings.Average(r => r.Value);
    }
}

// Узагальнений репозиторій для зберігання фільмів або рейтингів
public class Repository<T> where T : class
{
    private List<T> _items = new List<T>();

    public void Add(T item)
    {
        _items.Add(item);
    }

    public void Remove(T item)
    {
        _items.Remove(item);
    }

    public T Find(Func<T, bool> predicate)
    {
        return _items.FirstOrDefault(predicate);
    }

    public IEnumerable<T> All()
    {
        return _items;
    }

    public IEnumerable<T> Where(Func<T, bool> predicate)
    {
        return _items.Where(predicate);
    }
}

// Статичний клас з методом MaxBy для пошуку елементу з максимальним значенням по певному критерію
public static class Extensions
{
    public static T MaxBy<T>(this IEnumerable<T> source, Func<T, double> selector)
    {
        return source.OrderByDescending(selector).FirstOrDefault();
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Створення репозиторію фільмів
        var movieRepo = new Repository<Movie>();
        var movie = new Movie("Inception");

        // Додавання рейтингів до фільму
        movie.AddRating(new Rating(9, "User1"));
        movie.AddRating(new Rating(7, "User2"));
        movie.AddRating(new Rating(8, "User3"));
        movie.AddRating(new Rating(6, "User4"));
        movie.AddRating(new Rating(10, "User5"));

        // Додавання фільму в репозиторій
        movieRepo.Add(movie);

        try
        {
            // Виведення середнього рейтингу фільму
            Console.WriteLine($"Середній рейтинг фільму {movie.Title}: {movie.GetAverageRating():F2}");
        }
        catch (InvalidRatingException ex)
        {
            // Обробка винятку, якщо рейтинг не коректний
            Console.WriteLine($"Помилка: {ex.Message}");
        }

        // Демонстрація використання MaxBy для пошуку найбільш високого рейтингу
        var highestRated = movie.Ratings.MaxBy(r => r.Value);
        Console.WriteLine($"Найвищий рейтинг поставив {highestRated.User} з оцінкою {highestRated.Value}");
    }
}
