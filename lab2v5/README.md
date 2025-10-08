Звіт до лабораторної роботи
Тема: Інкапсуляція, Індекатори, Оператори
Мета роботи:

Ознайомлення з основами інкапсуляції, індексаторів та перевантаженням операторів у мові програмування C#.

1. Створення проєкту

Проєкт було створено за допомогою інтерфейсу командного рядка за наступною командою:

dotnet new console -o OOP-<Прізвище>/lab2vN


Де OOP-<Прізвище> — це унікальний ідентифікатор проєкту, а lab2vN — номер варіанту.

2. Створення класу з інкапсуляцією

Був створений клас із використанням інкапсуляції. У класі використовуються приватні поля та публічні властивості для доступу до них. Для доступу до даних через властивості використовуються методи get та set.

class BankAccount
{
    private decimal balance;

    public decimal Balance
    {
        get { return balance; }
        set 
        {
            if (value >= 0)
                balance = value;
            else
                throw new ArgumentException("Balance cannot be negative.");
        }
    }
}

3. Використання індексаторів

Був створений індексатор для доступу до елементів об'єкта за індексом або за ключем. Це дозволяє зручно працювати з класом, як із колекцією.

class BankAccount
{
    private decimal[] transactions = new decimal[10];

    public decimal this[int index]
    {
        get { return transactions[index]; }
        set { transactions[index] = value; }
    }
}

4. Перевантаження операторів

Перевантаження операторів дозволяє змінювати стандартну поведінку операторів. У даному випадку було перевантажено оператори для поповнення та зняття коштів:

class BankAccount
{
    private decimal balance;

    public static BankAccount operator +(BankAccount account, decimal amount)
    {
        account.balance += amount;
        return account;
    }

    public static BankAccount operator -(BankAccount account, decimal amount)
    {
        if (account.balance - amount >= 0)
        {
            account.balance -= amount;
        }
        else
        {
            throw new InvalidOperationException("Insufficient funds.");
        }
        return account;
    }
}

5. Демонстрація роботи класу

У файлі Program.cs було продемонстровано створення об'єкта класу BankAccount, доступ до його властивостей, а також використання індексаторів і перевантажених операторів:

class Program
{
    static void Main()
    {
        BankAccount account = new BankAccount();
        account.Balance = 1000;

        account = account + 500;  // Поповнення рахунку
        Console.WriteLine(account.Balance);

        account = account - 200;  // Зняття з рахунку
        Console.WriteLine(account.Balance);
    }
}

6. Висновки

У результаті виконання лабораторної роботи були здобуті навички з:

Створення класів із інкапсуляцією.

Використання властивостей для захисту даних.

Реалізації індексаторів для доступу до елементів об'єкта.

Перевантаження операторів для зміни стандартної поведінки операторів у класах.

Робота виконана успішно, усі вимоги завдання було реалізовано.

7. Коміт

Після завершення роботи був здійснений коміт з описом змін:

Lab2: Encapsulation, Indexers, Operators