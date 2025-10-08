using System;

namespace OOP_YourSurname.lab2vN
{
    class BankAccount
    {
        // Приватне поле
        private decimal balance;

        // Публічна властивість з валідацією (баланс >= 0)
        public decimal Balance
        {
            get => balance;
            set
            {
                if (value >= 0)
                    balance = value;
                else
                    throw new ArgumentException("Баланс не може бути від'ємним");
            }
        }

        // Конструктор
        public BankAccount(decimal initialBalance)
        {
            Balance = initialBalance;  // Використовуємо властивість з валідацією
        }

        // Індексатор: повертає Balance якщо ключ "balance"
        public decimal this[string key]
        {
            get
            {
                if (key.ToLower() == "balance")
                    return balance;
                else
                    throw new ArgumentException("Невідомий індексатор");
            }
            set
            {
                if (key.ToLower() == "balance")
                    Balance = value;
                else
                    throw new ArgumentException("Невідомий індексатор");
            }
        }

        // Перевантаження оператора + (поповнення рахунку)
        public static BankAccount operator +(BankAccount account, decimal amount)
        {
            if (amount < 0)
                throw new ArgumentException("Сума поповнення не може бути від'ємною");

            return new BankAccount(account.Balance + amount);
        }

        // Перевантаження оператора - (зняття коштів)
        public static BankAccount operator -(BankAccount account, decimal amount)
        {
            if (amount < 0)
                throw new ArgumentException("Сума зняття не може бути від'ємною");

            decimal newBalance = account.Balance - amount;
            if (newBalance < 0)
                throw new InvalidOperationException("Недостатньо коштів для зняття");

            return new BankAccount(newBalance);
        }

        public override string ToString()
        {
            return $"Баланс: {balance} грн";
        }
    }

    class Program
    {
        static void Main()
        {
            // Створення об'єкта
            BankAccount account = new BankAccount(1000);
            Console.WriteLine(account); // Баланс: 1000 грн

            // Поповнення через оператор +
            account = account + 500;
            Console.WriteLine(account); // Баланс: 1500 грн

            // Зняття через оператор -
            account = account - 200;
            Console.WriteLine(account); // Баланс: 1300 грн

            // Використання індексатора
            Console.WriteLine($"Баланс через індексатор: {account["balance"]}");

            // Зміна балансу через індексатор
            account["balance"] = 2000;
            Console.WriteLine(account);

            // Спроба поставити від'ємний баланс викличе помилку
            // account.Balance = -100; // Викличе виключення ArgumentException
        }
    }
}
