using System;
using System.Collections.Generic;
using System.Globalization;

// Базовый класс: Мобильные устройства
public class MobileDevice
{
    // Поля: производитель, модель, цвет, цена, емкость батареи, ОС
    private string _brand = string.Empty;
    private string _model = string.Empty;
    private string _color = string.Empty;
    private decimal _price;
    private int _batteryCapacity; // мАч
    private string _operatingSystem = string.Empty;

    // Свойство: бренд (не пустая строка)
    public string Brand
    {
        get => _brand;
        set => _brand = ValidateNonEmpty(value, nameof(Brand));
    }

    // Свойство: модель (не пустая строка)
    public string Model
    {
        get => _model;
        set => _model = ValidateNonEmpty(value, nameof(Model));
    }

    // Свойство: цвет (не пустая строка)
    public string Color
    {
        get => _color;
        set => _color = ValidateNonEmpty(value, nameof(Color));
    }

    // Свойство: цена (0..1_000_000)
    public decimal Price
    {
        get => _price;
        set
        {
            if (value < 0m || value > 1_000_000m)
            {
                throw new ArgumentOutOfRangeException(nameof(Price), "Цена должна быть в диапазоне [0; 1_000_000].");
            }
            _price = value;
        }
    }

    // Свойство: емкость батареи (1000..50000 мАч)
    public int BatteryCapacity
    {
        get => _batteryCapacity;
        set
        {
            if (value < 1000 || value > 50000)
            {
                throw new ArgumentOutOfRangeException(nameof(BatteryCapacity), "Емкость батареи должна быть в диапазоне [1000; 50000] мАч.");
            }
            _batteryCapacity = value;
        }
    }

    // Свойство: операционная система (не пустая строка)
    public string OperatingSystem
    {
        get => _operatingSystem;
        set => _operatingSystem = ValidateNonEmpty(value, nameof(OperatingSystem));
    }

    // Конструктор с параметрами по умолчанию
    public MobileDevice(
        string brand = "Generic",
        string model = "Model",
        string color = "Black",
        decimal price = 0m,
        int batteryCapacity = 5000,
        string operatingSystem = "Unknown")
    {
        Brand = brand;
        Model = model;
        Color = color;
        Price = price;
        BatteryCapacity = batteryCapacity;
        OperatingSystem = operatingSystem;
    }

    // Функции-доступоры (дублируют свойства — по требованию задания)
    public string GetBrand() => Brand;
    public string GetModel() => Model;
    public string GetColor() => Color;
    public decimal GetPrice() => Price;
    public int GetBatteryCapacity() => BatteryCapacity;
    public string GetOperatingSystem() => OperatingSystem;

    // Функции-мутаторы (изменение характеристик)
    public void UpdatePrice(decimal newPrice) => Price = newPrice;
    public void UpdateColor(string newColor) => Color = newColor;
    public void UpdateBatteryCapacity(int newCapacity) => BatteryCapacity = newCapacity;

    // Проверка на непустую строку
    protected static string ValidateNonEmpty(string value, string fieldName)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException($"{fieldName} не может быть пустым.");
        }
        return value.Trim();
    }

    // Виртуальная печать сведений (для полиморфизма)
    public virtual void PrintInfo()
    {
        Console.WriteLine($"Мобильное устройство: {Brand} {Model}, цвет: {Color}, цена: {Price.ToString(CultureInfo.InvariantCulture)}, батарея: {BatteryCapacity} мАч, ОС: {OperatingSystem}");
    }
}

// Производный класс: Ноутбук
public class Laptop : MobileDevice
{
    // Доп. поля: размер экрана, процессор, ОЗУ, объем накопителя, видеокарта
    private double _screenSizeInches;
    private string _processor = string.Empty;
    private int _ramGB;
    private int _storageGB;
    private string _graphicsCard = string.Empty;

    // Размер экрана (10..21 дюймов)
    public double ScreenSizeInches
    {
        get => _screenSizeInches;
        set
        {
            if (value < 10.0 || value > 21.0)
            {
                throw new ArgumentOutOfRangeException(nameof(ScreenSizeInches), "Размер экрана должен быть в диапазоне [10; 21] дюймов.");
            }
            _screenSizeInches = value;
        }
    }

    // Процессор (строка, не пустая)
    public string Processor
    {
        get => _processor;
        set => _processor = ValidateNonEmpty(value, nameof(Processor));
    }

    // ОЗУ (4..128 ГБ)
    public int RamGB
    {
        get => _ramGB;
        set
        {
            if (value < 4 || value > 128)
            {
                throw new ArgumentOutOfRangeException(nameof(RamGB), "ОЗУ должна быть в диапазоне [4; 128] ГБ.");
            }
            _ramGB = value;
        }
    }

    // Объем накопителя (64..8192 ГБ)
    public int StorageGB
    {
        get => _storageGB;
        set
        {
            if (value < 64 || value > 8192)
            {
                throw new ArgumentOutOfRangeException(nameof(StorageGB), "Объем накопителя должен быть в диапазоне [64; 8192] ГБ.");
            }
            _storageGB = value;
        }
    }

    // Видеокарта (строка, не пустая)
    public string GraphicsCard
    {
        get => _graphicsCard;
        set => _graphicsCard = ValidateNonEmpty(value, nameof(GraphicsCard));
    }

    // Конструктор с параметрами по умолчанию
    public Laptop(
        string brand = "Generic",
        string model = "Laptop",
        string color = "Black",
        decimal price = 0m,
        int batteryCapacity = 5000,
        string operatingSystem = "Windows",
        double screenSizeInches = 15.6,
        string processor = "Intel Core i5",
        int ramGB = 8,
        int storageGB = 512,
        string graphicsCard = "Integrated") : base(brand, model, color, price, batteryCapacity, operatingSystem)
    {
        ScreenSizeInches = screenSizeInches;
        Processor = processor;
        RamGB = ramGB;
        StorageGB = storageGB;
        GraphicsCard = graphicsCard;
    }

    // Доступоры
    public double GetScreenSizeInches() => ScreenSizeInches;
    public string GetProcessor() => Processor;
    public int GetRamGB() => RamGB;
    public int GetStorageGB() => StorageGB;
    public string GetGraphicsCard() => GraphicsCard;

    // Мутаторы
    public void UpdateScreenSize(double newSize) => ScreenSizeInches = newSize;
    public void UpdateProcessor(string newProcessor) => Processor = newProcessor;
    public void UpdateRam(int newRam) => RamGB = newRam;
    public void UpdateStorage(int newStorage) => StorageGB = newStorage;
    public void UpdateGraphicsCard(string newGraphics) => GraphicsCard = newGraphics;

    // Переопределённая печать сведений
    public override void PrintInfo()
    {
        Console.WriteLine(
            $"Ноутбук: {Brand} {Model}, экран: {ScreenSizeInches.ToString(CultureInfo.InvariantCulture)}\", процессор: {Processor}, ОЗУ: {RamGB} ГБ, накопитель: {StorageGB} ГБ, видеокарта: {GraphicsCard}, батарея: {BatteryCapacity} мАч, ОС: {OperatingSystem}, цвет: {Color}, цена: {Price.ToString(CultureInfo.InvariantCulture)}");
    }
}

// Производный класс: Электронная книга
public class EBook : MobileDevice
{
    // Доп. поля: размер экрана, объем памяти, поддерживаемые форматы, подсветка
    private double _screenSizeInches;
    private int _memoryGB;
    private string _supportedFormats = string.Empty;
    private bool _hasBacklight;

    // Размер экрана (5..13 дюймов)
    public double ScreenSizeInches
    {
        get => _screenSizeInches;
        set
        {
            if (value < 5.0 || value > 13.0)
            {
                throw new ArgumentOutOfRangeException(nameof(ScreenSizeInches), "Размер экрана должен быть в диапазоне [5; 13] дюймов.");
            }
            _screenSizeInches = value;
        }
    }

    // Объем памяти (4..512 ГБ)
    public int MemoryGB
    {
        get => _memoryGB;
        set
        {
            if (value < 4 || value > 512)
            {
                throw new ArgumentOutOfRangeException(nameof(MemoryGB), "Объем памяти должен быть в диапазоне [4; 512] ГБ.");
            }
            _memoryGB = value;
        }
    }

    // Поддерживаемые форматы (строка, не пустая)
    public string SupportedFormats
    {
        get => _supportedFormats;
        set => _supportedFormats = ValidateNonEmpty(value, nameof(SupportedFormats));
    }

    // Встроенная подсветка
    public bool HasBacklight
    {
        get => _hasBacklight;
        set => _hasBacklight = value;
    }

    // Конструктор с параметрами по умолчанию
    public EBook(
        string brand = "Generic",
        string model = "EBook",
        string color = "Black",
        decimal price = 0m,
        int batteryCapacity = 1500,
        string operatingSystem = "Custom",
        double screenSizeInches = 6.0,
        int memoryGB = 32,
        string supportedFormats = "EPUB, PDF, MOBI",
        bool hasBacklight = true) : base(brand, model, color, price, batteryCapacity, operatingSystem)
    {
        ScreenSizeInches = screenSizeInches;
        MemoryGB = memoryGB;
        SupportedFormats = supportedFormats;
        HasBacklight = hasBacklight;
    }

    // Доступоры
    public double GetScreenSizeInches() => ScreenSizeInches;
    public int GetMemoryGB() => MemoryGB;
    public string GetSupportedFormats() => SupportedFormats;
    public bool GetHasBacklight() => HasBacklight;

    // Мутаторы
    public void UpdateScreenSize(double newSize) => ScreenSizeInches = newSize;
    public void UpdateMemory(int newMemory) => MemoryGB = newMemory;
    public void UpdateSupportedFormats(string newFormats) => SupportedFormats = newFormats;
    public void UpdateBacklight(bool newBacklight) => HasBacklight = newBacklight;

    // Переопределённая печать сведений
    public override void PrintInfo()
    {
        Console.WriteLine(
            $"Электронная книга: {Brand} {Model}, экран: {ScreenSizeInches.ToString(CultureInfo.InvariantCulture)}\", память: {MemoryGB} ГБ, форматы: {SupportedFormats}, подсветка: {(HasBacklight ? "да" : "нет")}, батарея: {BatteryCapacity} мАч, ОС: {OperatingSystem}, цвет: {Color}, цена: {Price.ToString(CultureInfo.InvariantCulture)}");
    }
}

public static class Program
{
    // Глобальный список мобильных устройств для работы с ними
    private static List<MobileDevice> devices = new List<MobileDevice>();

    public static void Main()
    {
        CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;

        // Интерактивное меню
        RunInteractiveMenu();
    }

    private static void PrintAllDevices()
    {
        for (int i = 0; i < devices.Count; i++)
        {
            Console.Write($"{i + 1}. ");
            devices[i].PrintInfo();
        }
    }

    private static void RunInteractiveMenu()
    {
        while (true)
        {
            Console.WriteLine();
            Console.WriteLine("=== Меню ===");
            Console.WriteLine("1) Добавить ноутбук (Laptop)");
            Console.WriteLine("2) Добавить электронную книгу (EBook)");
            Console.WriteLine("3) Показать список устройств");
            Console.WriteLine("4) Изменить характеристики устройства");
            Console.WriteLine("5) Выход");
            Console.Write("Ваш выбор: ");
            string? choice = Console.ReadLine();
            Console.WriteLine();

            if (choice == "1")
            {
                // Ввод параметров и добавление ноутбука
                var laptop = CreateLaptopFromInput();
                devices.Add(laptop);
                Console.WriteLine("Ноутбук добавлен.");
            }
            else if (choice == "2")
            {
                // Ввод параметров и добавление электронной книги
                var ebook = CreateEBookFromInput();
                devices.Add(ebook);
                Console.WriteLine("Электронная книга добавлена.");
            }
            else if (choice == "3")
            {
                if (devices.Count == 0)
                {
                    Console.WriteLine("Список пуст.");
                }
                else
                {
                    Console.WriteLine("=== Список устройств ===");
                    PrintAllDevices();
                }
            }
            else if (choice == "4")
            {
                // Изменение характеристик устройства
                ModifyDevice();
            }
            else if (choice == "5")
            {
                devices.Clear();
                Console.WriteLine("Завершение работы.");
                break;
            }
            else
            {
                Console.WriteLine("Неизвестный пункт меню.");
            }
        }
    }

    private static void ModifyDevice()
    {
        if (devices.Count == 0)
        {
            Console.WriteLine("Список устройств пуст.");
            return;
        }

        Console.WriteLine("=== Выберите устройство для изменения ===");
        PrintAllDevices();
        
        int deviceIndex = ReadInt("Введите номер устройства: ", 1, devices.Count) - 1;
        var device = devices[deviceIndex];

        Console.WriteLine($"\nВыбрано устройство: {device.Brand} {device.Model}");
        Console.WriteLine("Текущие характеристики:");
        device.PrintInfo();

        // Меню изменения в зависимости от типа устройства
        if (device is Laptop laptop)
        {
            ModifyLaptop(laptop);
        }
        else if (device is EBook ebook)
        {
            ModifyEBook(ebook);
        }
        else
        {
            ModifyBaseDevice(device);
        }

        Console.WriteLine("\nОбновлённые характеристики:");
        device.PrintInfo();
    }

    private static void ModifyLaptop(Laptop laptop)
    {
        Console.WriteLine("\n=== Изменение характеристик ноутбука ===");
        Console.WriteLine("1) Изменить цену");
        Console.WriteLine("2) Изменить цвет");
        Console.WriteLine("3) Изменить емкость батареи");
        Console.WriteLine("4) Изменить операционную систему");
        Console.WriteLine("5) Изменить размер экрана");
        Console.WriteLine("6) Изменить процессор");
        Console.WriteLine("7) Изменить ОЗУ");
        Console.WriteLine("8) Изменить объем накопителя");
        Console.WriteLine("9) Изменить видеокарту");
        
        int choice = ReadInt("Выберите параметр для изменения: ", 1, 9);
        
        switch (choice)
        {
            case 1:
                decimal newPrice = ReadDecimal("Новая цена: ", 0m, 1_000_000m);
                laptop.UpdatePrice(newPrice);
                break;
            case 2:
                string newColor = ReadNonEmpty("Новый цвет: ");
                laptop.UpdateColor(newColor);
                break;
            case 3:
                int newBattery = ReadInt("Новая емкость батареи (мАч 1000-50000): ", 1000, 50000);
                laptop.UpdateBatteryCapacity(newBattery);
                break;
            case 4:
                string newOS = ReadNonEmpty("Новая операционная система: ");
                laptop.OperatingSystem = newOS;
                break;
            case 5:
                double newSize = ReadDouble("Новый размер экрана (дюймы 10-21): ", 10.0, 21.0);
                laptop.UpdateScreenSize(newSize);
                break;
            case 6:
                string newProcessor = ReadNonEmpty("Новый процессор: ");
                laptop.UpdateProcessor(newProcessor);
                break;
            case 7:
                int newRam = ReadInt("Новая ОЗУ (ГБ 4-128): ", 4, 128);
                laptop.UpdateRam(newRam);
                break;
            case 8:
                int newStorage = ReadInt("Новый объем накопителя (ГБ 64-8192): ", 64, 8192);
                laptop.UpdateStorage(newStorage);
                break;
            case 9:
                string newGraphics = ReadNonEmpty("Новая видеокарта: ");
                laptop.UpdateGraphicsCard(newGraphics);
                break;
        }
    }

    private static void ModifyEBook(EBook ebook)
    {
        Console.WriteLine("\n=== Изменение характеристик электронной книги ===");
        Console.WriteLine("1) Изменить цену");
        Console.WriteLine("2) Изменить цвет");
        Console.WriteLine("3) Изменить емкость батареи");
        Console.WriteLine("4) Изменить операционную систему");
        Console.WriteLine("5) Изменить размер экрана");
        Console.WriteLine("6) Изменить объем памяти");
        Console.WriteLine("7) Изменить поддерживаемые форматы");
        Console.WriteLine("8) Изменить подсветку");
        
        int choice = ReadInt("Выберите параметр для изменения: ", 1, 8);
        
        switch (choice)
        {
            case 1:
                decimal newPrice = ReadDecimal("Новая цена: ", 0m, 1_000_000m);
                ebook.UpdatePrice(newPrice);
                break;
            case 2:
                string newColor = ReadNonEmpty("Новый цвет: ");
                ebook.UpdateColor(newColor);
                break;
            case 3:
                int newBattery = ReadInt("Новая емкость батареи (мАч 1000-50000): ", 1000, 50000);
                ebook.UpdateBatteryCapacity(newBattery);
                break;
            case 4:
                string newOS = ReadNonEmpty("Новая операционная система: ");
                ebook.OperatingSystem = newOS;
                break;
            case 5:
                double newSize = ReadDouble("Новый размер экрана (дюймы 5-13): ", 5.0, 13.0);
                ebook.UpdateScreenSize(newSize);
                break;
            case 6:
                int newMemory = ReadInt("Новый объем памяти (ГБ 4-512): ", 4, 512);
                ebook.UpdateMemory(newMemory);
                break;
            case 7:
                string newFormats = ReadNonEmpty("Новые поддерживаемые форматы: ");
                ebook.UpdateSupportedFormats(newFormats);
                break;
            case 8:
                bool newBacklight = ReadBool("Подсветка? (y/n): ");
                ebook.UpdateBacklight(newBacklight);
                break;
        }
    }

    private static void ModifyBaseDevice(MobileDevice device)
    {
        Console.WriteLine("\n=== Изменение характеристик устройства ===");
        Console.WriteLine("1) Изменить цену");
        Console.WriteLine("2) Изменить цвет");
        Console.WriteLine("3) Изменить емкость батареи");
        Console.WriteLine("4) Изменить операционную систему");
        
        int choice = ReadInt("Выберите параметр для изменения: ", 1, 4);
        
        switch (choice)
        {
            case 1:
                decimal newPrice = ReadDecimal("Новая цена: ", 0m, 1_000_000m);
                device.UpdatePrice(newPrice);
                break;
            case 2:
                string newColor = ReadNonEmpty("Новый цвет: ");
                device.UpdateColor(newColor);
                break;
            case 3:
                int newBattery = ReadInt("Новая емкость батареи (мАч 1000-50000): ", 1000, 50000);
                device.UpdateBatteryCapacity(newBattery);
                break;
            case 4:
                string newOS = ReadNonEmpty("Новая операционная система: ");
                device.OperatingSystem = newOS;
                break;
        }
    }

    // Чтение и валидация пользовательского ввода
    private static Laptop CreateLaptopFromInput()
    {
        string brand = ReadNonEmpty("Бренд: ");
        string model = ReadNonEmpty("Модель: ");
        string color = ReadNonEmpty("Цвет: ");
        decimal price = ReadDecimal("Цена: ", 0m, 1_000_000m);
        int batteryCapacity = ReadInt("Емкость батареи (мАч 1000-50000): ", 1000, 50000);
        string operatingSystem = ReadNonEmpty("Операционная система: ");
        double screenSize = ReadDouble("Размер экрана (дюймы 10-21): ", 10.0, 21.0);
        string processor = ReadNonEmpty("Процессор: ");
        int ram = ReadInt("ОЗУ (ГБ 4-128): ", 4, 128);
        int storage = ReadInt("Объем накопителя (ГБ 64-8192): ", 64, 8192);
        string graphicsCard = ReadNonEmpty("Видеокарта: ");
        return new Laptop(brand, model, color, price, batteryCapacity, operatingSystem, screenSize, processor, ram, storage, graphicsCard);
    }

    private static EBook CreateEBookFromInput()
    {
        string brand = ReadNonEmpty("Бренд: ");
        string model = ReadNonEmpty("Модель: ");
        string color = ReadNonEmpty("Цвет: ");
        decimal price = ReadDecimal("Цена: ", 0m, 1_000_000m);
        int batteryCapacity = ReadInt("Емкость батареи (мАч 1000-50000): ", 1000, 50000);
        string operatingSystem = ReadNonEmpty("Операционная система: ");
        double screenSize = ReadDouble("Размер экрана (дюймы 5-13): ", 5.0, 13.0);
        int memory = ReadInt("Объем памяти (ГБ 4-512): ", 4, 512);
        string supportedFormats = ReadNonEmpty("Поддерживаемые форматы (например, EPUB, PDF, MOBI): ");
        bool hasBacklight = ReadBool("Встроенная подсветка? (y/n): ");
        return new EBook(brand, model, color, price, batteryCapacity, operatingSystem, screenSize, memory, supportedFormats, hasBacklight);
    }

    private static string ReadNonEmpty(string prompt)
    {
        // Читает непустую строку
        while (true)
        {
            Console.Write(prompt);
            string? s = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(s))
            {
                return s.Trim();
            }
            Console.WriteLine("Значение не может быть пустым.");
        }
    }

    private static decimal ReadDecimal(string prompt, decimal min, decimal max)
    {
        // Читает десятичное число в заданных границах
        while (true)
        {
            Console.Write(prompt);
            string? s = Console.ReadLine();
            if (decimal.TryParse(s, NumberStyles.Number, CultureInfo.InvariantCulture, out var value) && value >= min && value <= max)
            {
                return value;
            }
            Console.WriteLine($"Введите число в диапазоне [{min}; {max}].");
        }
    }

    private static int ReadInt(string prompt, int min, int max)
    {
        // Читает целое число в заданных границах
        while (true)
        {
            Console.Write(prompt);
            string? s = Console.ReadLine();
            if (int.TryParse(s, NumberStyles.Integer, CultureInfo.InvariantCulture, out var value) && value >= min && value <= max)
            {
                return value;
            }
            Console.WriteLine($"Введите целое число в диапазоне [{min}; {max}].");
        }
    }

    private static double ReadDouble(string prompt, double min, double max)
    {
        // Читает число с плавающей точкой в заданных границах
        while (true)
        {
            Console.Write(prompt);
            string? s = Console.ReadLine();
            if (double.TryParse(s, NumberStyles.Number, CultureInfo.InvariantCulture, out var value) && value >= min && value <= max)
            {
                return value;
            }
            Console.WriteLine($"Введите число в диапазоне [{min}; {max}] (используйте точку как разделитель).");
        }
    }

    private static bool ReadBool(string prompt)
    {
        // Читает логическое значение (y/n, да/нет)
        while (true)
        {
            Console.Write(prompt);
            string? s = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(s))
            {
                continue;
            }
            s = s.Trim().ToLowerInvariant();
            if (s == "y" || s == "yes" || s == "д" || s == "да") return true;
            if (s == "n" || s == "no" || s == "н" || s == "нет") return false;
            Console.WriteLine("Введите y/n.");
        }
    }
}
