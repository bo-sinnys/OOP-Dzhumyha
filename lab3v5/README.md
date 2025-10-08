1. Базовий клас Device

Базовий клас повинен містити властивість для споживаної потужності (PowerUsage) та метод для обчислення сумарного енергоспоживання за добу.

using System;

public class Device
{
    // Потужність у ватах
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

2. Похідний клас Laptop

Клас Laptop буде похідним від класу Device і може мати додаткові властивості або методи, але для цього прикладу додаткові методи не обов'язкові.

public class Laptop : Device
{
    // Конструктор, який передає параметри батьківському класу
    public Laptop(double powerUsage) : base(powerUsage) { }

    // Перевизначений метод для підрахунку енергоспоживання за добу
    public override double CalculateDailyEnergyConsumption()
    {
        // Можливо, ноутбук працює не 24 години, а 8 годин на день
        return PowerUsage * 8; // Потужність * 8 годин
    }
}

3. Похідний клас Fridge

Клас Fridge також буде похідним від класу Device. Холодильник, як правило, споживає енергію 24 години на добу, тому можна використовувати стандартний метод обчислення.

public class Fridge : Device
{
    // Конструктор, який передає параметри батьківському класу
    public Fridge(double powerUsage) : base(powerUsage) { }

    // Перевизначений метод для підрахунку енергоспоживання за добу
    public override double CalculateDailyEnergyConsumption()
    {
        // Холодильник працює постійно
        return PowerUsage * 24; // Потужність * 24 години
    }
}

4. Демонстрація роботи в Program.cs

Тепер ви можете створити об'єкти класів Laptop та Fridge і обчислити їх сумарне енергоспоживання за добу.

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

Виведення результатів:

При запуску програми на екран виведеться енергоспоживання для кожного пристрою та загальна сума за добу:

Laptop daily energy consumption: 400 Wh
Fridge daily energy consumption: 3600 Wh
Total daily energy consumption: 4000 Wh

Пояснення:

Базовий клас Device: Має властивість PowerUsage і метод CalculateDailyEnergyConsumption(), який розраховує енергоспоживання за добу.

Похідні класи Laptop і Fridge: Оскільки ці пристрої можуть споживати енергію по-різному (наприклад, ноутбук працює не 24 години, а 8 годин), ми перевизначили метод CalculateDailyEnergyConsumption для кожного з класів.

Програма: Створює об'єкти для ноутбука та холодильника, виводить їх енергоспоживання за добу та сумарне значення.
