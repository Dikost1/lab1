using System;

public enum FuelType
{
    Gasoline,
    Diesel,
    LPG,
    CNG
}

public static class FuelTypeExtensions
{
    public static string ToRussianString(this FuelType fuelType)
    {
        switch (fuelType)
        {
            case FuelType.Gasoline: return "Бензин";
            case FuelType.Diesel:   return "Дизель";
            case FuelType.LPG:      return "Газ (LPG)";
            case FuelType.CNG:      return "Метан (CNG)";
            default: return "Неизвестно";
        }
    }
}

public sealed class Car
{
    // ---- неизменяемые после создания ----
    public string Manufacturer { get; }
    public string Model { get; }
    public int Year { get; }
    public FuelType RecommendedFuel { get; }
    public double CurbMassKg { get; }
    public double EnginePowerKw { get; }
    public double TankCapacityL { get; }
    public string Vin { get; }

    // ---- изменяемые ----
    public int PeopleCount { get; private set; }
    public double PeopleMassKg { get; private set; }
    public double FuelLiters { get; private set; }

    // ---- коэффициенты для расчёта ----
    private const double BaseKmPerL = 14.0;
    private const double PowerPenaltyPer100kW = 2.0;
    private const double MassPenaltyPer1000kg = 1.5;
    private const double MinKmPerL = 2.0;

    // ---- конструктор ----
    public Car(
        string manufacturer,
        string model,
        int year,
        FuelType recommendedFuel,
        double curbMassKg,
        double enginePowerKw,
        double fuelLiters,
        double tankCapacityL,
        string vin)
    {
        if (string.IsNullOrWhiteSpace(manufacturer)) throw new ArgumentException("Производитель обязателен.");
        if (string.IsNullOrWhiteSpace(model)) throw new ArgumentException("Модель обязательна.");
        if (year < 1886 || year > DateTime.Now.Year + 1) throw new ArgumentOutOfRangeException(nameof(year), "Некорректный год выпуска.");
        if (curbMassKg <= 0) throw new ArgumentOutOfRangeException(nameof(curbMassKg), "Масса должна быть положительной.");
        if (enginePowerKw <= 0) throw new ArgumentOutOfRangeException(nameof(enginePowerKw), "Мощность должна быть положительной.");
        if (tankCapacityL <= 0) throw new ArgumentOutOfRangeException(nameof(tankCapacityL), "Вместимость бака должна быть положительной.");
        if (fuelLiters < 0 || fuelLiters > tankCapacityL) throw new ArgumentOutOfRangeException(nameof(fuelLiters), "Топливо должно быть в пределах [0, вместимость бака].");
        if (string.IsNullOrWhiteSpace(vin)) throw new ArgumentException("VIN обязателен.");

        Manufacturer = manufacturer.Trim();
        Model = model.Trim();
        Year = year;
        RecommendedFuel = recommendedFuel;
        CurbMassKg = curbMassKg;
        EnginePowerKw = enginePowerKw;
        TankCapacityL = tankCapacityL;
        Vin = vin.Trim();

        FuelLiters = fuelLiters;
        PeopleCount = 0;
        PeopleMassKg = 0.0;
    }

    // ---- методы ----

    public void SetPassengers(int peopleCount, double peopleMassKg)
    {
        if (peopleCount < 0) throw new ArgumentOutOfRangeException(nameof(peopleCount), "Количество людей не может быть отрицательным.");
        if (peopleMassKg < 0) throw new ArgumentOutOfRangeException(nameof(peopleMassKg), "Масса пассажиров не может быть отрицательной.");
        if (peopleCount == 0 && peopleMassKg > 0) throw new ArgumentException("Масса не может быть > 0 при нуле пассажиров.");
        PeopleCount = peopleCount;
        PeopleMassKg = peopleMassKg;
    }

    public void SetFuel(double liters)
    {
        if (liters < 0 || liters > TankCapacityL)
            throw new ArgumentOutOfRangeException(nameof(liters), "Топливо должно быть в пределах [0, вместимость бака].");
        FuelLiters = liters;
    }

    public double RangeKm()
    {
        double totalMassKg = CurbMassKg + PeopleMassKg;
        double powerPenalty = (EnginePowerKw / 100.0) * PowerPenaltyPer100kW;
        double massPenalty = (totalMassKg / 1000.0) * MassPenaltyPer1000kg;
        double effectiveKmPerL = Math.Max(MinKmPerL, BaseKmPerL - powerPenalty - massPenalty);
        return FuelLiters * effectiveKmPerL;
    }

    public void UnloadAllPassengers()
    {
        PeopleCount = 0;
        PeopleMassKg = 0.0;
    }
}

class Program
{
    static void Main()
    {
        Car car = new Car(
            manufacturer: "Toyota",
            model: "Camry",
            year: 2020,
            recommendedFuel: FuelType.Gasoline,
            curbMassKg: 1600,
            enginePowerKw: 150,
            fuelLiters: 40,
            tankCapacityL: 60,
            vin: "123456789ABCDEFG"
        );

        car.SetPassengers(3, 210); // три человека, 210 кг суммарно

        Console.WriteLine($"Производитель: {car.Manufacturer}");
        Console.WriteLine($"Модель: {car.Model}");
        Console.WriteLine($"Год выпуска: {car.Year}");
        Console.WriteLine($"Рекомендуемое топливо: {car.RecommendedFuel.ToRussianString()}");
        Console.WriteLine($"Масса автомобиля (без людей): {car.CurbMassKg} кг");
        Console.WriteLine($"Количество пассажиров: {car.PeopleCount}");
        Console.WriteLine($"Суммарная масса пассажиров: {car.PeopleMassKg} кг");
        Console.WriteLine($"Мощность двигателя: {car.EnginePowerKw} кВт");
        Console.WriteLine($"Текущий объём топлива: {car.FuelLiters} л");
        Console.WriteLine($"Вместимость бака: {car.TankCapacityL} л");
        Console.WriteLine($"VIN: {car.Vin}");
        Console.WriteLine($"Запас хода: {car.RangeKm():F1} км");

        car.UnloadAllPassengers();
        Console.WriteLine("Все пассажиры высажены.");
        Console.WriteLine($"Количество пассажиров: {car.PeopleCount}, суммарная масса: {car.PeopleMassKg} кг");
    }
}
