using System;

class BankAccount
{
    private string owner;
    private string accountNumber;
    private decimal balance;

    public string Owner => owner;
    public string AccountNumber => accountNumber;
    public decimal Balance => balance;

    public BankAccount(string owner, string accountNumber)
    {
        this.owner = owner;
        this.accountNumber = accountNumber;
        this.balance = 0;
    }

    public void Deposit(decimal amount)
    {
        if (amount <= 0)
        {
            Console.WriteLine("Сума внесення має бути більшою за нуль.");
            return;
        }
        balance += amount;
        Console.WriteLine($"Внесено {amount}. Поточний баланс: {balance}");
    }

    public void Withdraw(decimal amount)
    {
        if (amount <= 0)
        {
            Console.WriteLine("Сума зняття має бути більшою за нуль.");
            return;
        }
        if (amount > balance)
        {
            Console.WriteLine("Недостатньо коштів на рахунку.");
            return;
        }
        balance -= amount;
        Console.WriteLine($"Знято {amount}. Поточний баланс: {balance}");
    }
}

class Program
{
    static void Main(string[] args)
    {
        BankAccount account = new BankAccount("Іван Іваненко", "UA123456789");

        Console.WriteLine($"Власник: {account.Owner}");
        Console.WriteLine($"Номер рахунку: {account.AccountNumber}");
        Console.WriteLine($"Початковий баланс: {account.Balance}");

        account.Deposit(1000);
        account.Withdraw(500);
        account.Withdraw(600);

        Console.WriteLine($"Кінцевий баланс: {account.Balance}");
    }
}
