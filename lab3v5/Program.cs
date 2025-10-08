using System;

// Базовий клас Device
public class Device
{
    // Потужність в ватах
    public double PowerUsage { get; set; }

    // Конструктор
    public Device(double powerUsage)
    {
        PowerUsage = powerUsage;
    }

    // Метод для обчислення енергоспоживання за добу (24 години)
    public virtual double CalculateDailyEnergyConsumption()
    {
        return PowerUsage * 24; // Потужність * 24 години
    }
}

// Похідний клас Laptop
public class Laptop : Device
{
    // Конструктор, який передає параметри в базовий клас
    public Laptop(double powerUsage) : base(powerUsage) { }

    // Перевизначений метод для підрахунку енергоспоживання за добу
    public override double CalculateDailyEnergyConsumption()
    {
        // Можливо, ноутбук працює не 24 години, а 8 годин на день
        return PowerUsage * 8; // Потужність * 8 годин
    }
}

// Похідний клас Fridge
public class Fridge : Device
{
    // Конструктор, який передає параметри в базовий клас
    public Fridge(double powerUsage) : base(powerUsage) { }

    // Перевизначений метод для підрахунку енергоспоживання за добу
    public override double CalculateDailyEnergyConsumption()
    {
        // Холодильник працює постійно
        return PowerUsage * 24; // Потужність * 24 години
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Створення об'єктів
        Device laptop = new Laptop(50); // ноутбук споживає 50 ват
        Device fridge = new Fridge(150); // холодильник споживає 150 ват

        // Обчислення енергоспоживання
        Console.WriteLine($"Laptop daily energy consumption: {laptop.CalculateDailyEnergyConsumption()} Wh");
        Console.WriteLine($"Fridge daily energy consumption: {fridge.CalculateDailyEnergyConsumption()} Wh");

        // Сумарне енергоспоживання
        double totalEnergyConsumption = laptop.CalculateDailyEnergyConsumption() + fridge.CalculateDailyEnergyConsumption();
        Console.WriteLine($"Total daily energy consumption: {totalEnergyConsumption} Wh");
    }
}
