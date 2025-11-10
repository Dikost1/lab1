using System;
using System.Collections.Generic;
using NUnit.Framework;
using System.Reflection;

// Тесты для базового класса MobileDevice
[TestFixture]
public class MobileDeviceTests
{
    // Тест конструктора по умолчанию - проверяет, что все поля инициализируются корректными значениями по умолчанию
    [Test]
    public void MobileDevice_Constructor_DefaultValues()
    {
        // Создаем мобильное устройство с параметрами по умолчанию
        var device = new MobileDevice();
        
        // Проверяем, что все поля установлены в значения по умолчанию
        Assert.That(device.Brand, Is.EqualTo("Generic"));
        Assert.That(device.Model, Is.EqualTo("Model"));
        Assert.That(device.Color, Is.EqualTo("Black"));
        Assert.That(device.Price, Is.EqualTo(0m));
        Assert.That(device.BatteryCapacity, Is.EqualTo(5000));
        Assert.That(device.OperatingSystem, Is.EqualTo("Unknown"));
    }

    // Тест конструктора с пользовательскими параметрами - проверяет корректную инициализацию всех полей
    [Test]
    public void MobileDevice_Constructor_CustomValues()
    {
        // Создаем мобильное устройство с пользовательскими параметрами
        var device = new MobileDevice("Apple", "iPhone 15", "White", 99999m, 4000, "iOS");
        
        // Проверяем, что все поля установлены в переданные значения
        Assert.That(device.Brand, Is.EqualTo("Apple"));
        Assert.That(device.Model, Is.EqualTo("iPhone 15"));
        Assert.That(device.Color, Is.EqualTo("White"));
        Assert.That(device.Price, Is.EqualTo(99999m));
        Assert.That(device.BatteryCapacity, Is.EqualTo(4000));
        Assert.That(device.OperatingSystem, Is.EqualTo("iOS"));
    }

    [Test]
    public void MobileDevice_Brand_EmptyValue_ThrowsException()
    {
        var device = new MobileDevice();
        // Попытка установить пустую строку должна вызвать исключение
        Assert.Throws<ArgumentException>(() => device.Brand = "");
    }

    [Test]
    public void MobileDevice_Price_NegativeValue_ThrowsException()
    {
        var device = new MobileDevice();
        // Попытка установить отрицательную цену должна вызвать исключение
        Assert.Throws<ArgumentOutOfRangeException>(() => device.Price = -100m);
    }

    [Test]
    public void MobileDevice_BatteryCapacity_TooSmall_ThrowsException()
    {
        var device = new MobileDevice();
        // Попытка установить емкость батареи меньше 1000 мАч должна вызвать исключение
        Assert.Throws<ArgumentOutOfRangeException>(() => device.BatteryCapacity = 500);
    }

    [Test]
    public void MobileDevice_BatteryCapacity_TooLarge_ThrowsException()
    {
        var device = new MobileDevice();
        // Попытка установить емкость батареи больше 50000 мАч должна вызвать исключение
        Assert.Throws<ArgumentOutOfRangeException>(() => device.BatteryCapacity = 60000);
    }

    [Test]
    public void MobileDevice_UpdatePrice_UpdatesCorrectly()
    {
        // Создаем мобильное устройство с начальной ценой
        var device = new MobileDevice("Samsung", "Galaxy S24", "Black", 80000m, 4000, "Android");
        
        // Обновляем цену
        device.UpdatePrice(75000m);
        
        // Проверяем, что цена обновилась корректно
        Assert.That(device.Price, Is.EqualTo(75000m));
    }

    [Test]
    public void MobileDevice_UpdateBatteryCapacity_UpdatesCorrectly()
    {
        // Создаем мобильное устройство с начальной емкостью батареи
        var device = new MobileDevice("Xiaomi", "Mi 13", "Blue", 50000m, 4500, "Android");
        
        // Обновляем емкость батареи
        device.UpdateBatteryCapacity(5000);
        
        // Проверяем, что емкость обновилась корректно
        Assert.That(device.BatteryCapacity, Is.EqualTo(5000));
    }

    [Test]
    public void MobileDevice_PrintInfo_OutputsCorrectFormat()
    {
        // Создаем мобильное устройство для тестирования
        var device = new MobileDevice("Apple", "iPhone 15", "Blue", 99999m, 4000, "iOS");
        
        // Перенаправляем вывод консоли в StringWriter для проверки
        var stringWriter = new System.IO.StringWriter();
        Console.SetOut(stringWriter);
        
        // Вызываем метод печати информации
        device.PrintInfo();
        
        // Получаем вывод и проверяем его содержимое
        string output = stringWriter.ToString();
        Assert.That(output, Does.Contain("Мобильное устройство: Apple iPhone 15"));
        Assert.That(output, Does.Contain("4000"));
        Assert.That(output, Does.Contain("iOS"));
        
        // Восстанавливаем стандартный вывод консоли
        Console.SetOut(new System.IO.StreamWriter(Console.OpenStandardOutput()) { AutoFlush = true });
    }
}

// Тесты для производного класса Laptop
[TestFixture]
public class LaptopTests
{
    [Test]
    public void Laptop_Constructor_DefaultValues()
    {
        // Создаем ноутбук с параметрами по умолчанию
        var laptop = new Laptop();
        
        // Проверяем базовые поля (наследованные от MobileDevice)
        Assert.That(laptop.Brand, Is.EqualTo("Generic"));
        Assert.That(laptop.Model, Is.EqualTo("Laptop"));
        Assert.That(laptop.Color, Is.EqualTo("Black"));
        Assert.That(laptop.Price, Is.EqualTo(0m));
        Assert.That(laptop.BatteryCapacity, Is.EqualTo(5000));
        Assert.That(laptop.OperatingSystem, Is.EqualTo("Windows"));
        
        // Проверяем специфичные для ноутбука поля
        Assert.That(laptop.ScreenSizeInches, Is.EqualTo(15.6));
        Assert.That(laptop.Processor, Is.EqualTo("Intel Core i5"));
        Assert.That(laptop.RamGB, Is.EqualTo(8));
        Assert.That(laptop.StorageGB, Is.EqualTo(512));
        Assert.That(laptop.GraphicsCard, Is.EqualTo("Integrated"));
    }

    [Test]
    public void Laptop_Constructor_CustomValues()
    {
        // Создаем ноутбук с пользовательскими параметрами
        var laptop = new Laptop("Apple", "MacBook Pro", "Silver", 250000m, 8000, "macOS", 16.0, "Apple M3 Pro", 32, 1024, "Integrated");
        
        // Проверяем базовые поля
        Assert.That(laptop.Brand, Is.EqualTo("Apple"));
        Assert.That(laptop.Model, Is.EqualTo("MacBook Pro"));
        Assert.That(laptop.Color, Is.EqualTo("Silver"));
        Assert.That(laptop.Price, Is.EqualTo(250000m));
        Assert.That(laptop.BatteryCapacity, Is.EqualTo(8000));
        Assert.That(laptop.OperatingSystem, Is.EqualTo("macOS"));
        
        // Проверяем специфичные для ноутбука поля
        Assert.That(laptop.ScreenSizeInches, Is.EqualTo(16.0));
        Assert.That(laptop.Processor, Is.EqualTo("Apple M3 Pro"));
        Assert.That(laptop.RamGB, Is.EqualTo(32));
        Assert.That(laptop.StorageGB, Is.EqualTo(1024));
        Assert.That(laptop.GraphicsCard, Is.EqualTo("Integrated"));
    }

    [Test]
    public void Laptop_ScreenSizeInches_TooSmall_ThrowsException()
    {
        var laptop = new Laptop();
        // Попытка установить размер экрана меньше 10 дюймов должна вызвать исключение
        Assert.Throws<ArgumentOutOfRangeException>(() => laptop.ScreenSizeInches = 8.0);
    }

    [Test]
    public void Laptop_ScreenSizeInches_TooLarge_ThrowsException()
    {
        var laptop = new Laptop();
        // Попытка установить размер экрана больше 21 дюйма должна вызвать исключение
        Assert.Throws<ArgumentOutOfRangeException>(() => laptop.ScreenSizeInches = 25.0);
    }

    [Test]
    public void Laptop_RamGB_TooSmall_ThrowsException()
    {
        var laptop = new Laptop();
        // Попытка установить ОЗУ меньше 4 ГБ должна вызвать исключение
        Assert.Throws<ArgumentOutOfRangeException>(() => laptop.RamGB = 2);
    }

    [Test]
    public void Laptop_StorageGB_TooSmall_ThrowsException()
    {
        var laptop = new Laptop();
        // Попытка установить объем накопителя меньше 64 ГБ должна вызвать исключение
        Assert.Throws<ArgumentOutOfRangeException>(() => laptop.StorageGB = 32);
    }

    [Test]
    public void Laptop_Processor_EmptyValue_ThrowsException()
    {
        var laptop = new Laptop();
        // Попытка установить пустой процессор должна вызвать исключение
        Assert.Throws<ArgumentException>(() => laptop.Processor = "");
    }

    [Test]
    public void Laptop_UpdateScreenSize_UpdatesCorrectly()
    {
        // Создаем ноутбук с начальным размером экрана
        var laptop = new Laptop("Lenovo", "ThinkPad", "Black", 120000m, 6000, "Windows", 14.0, "Intel Core i7", 16, 512, "NVIDIA RTX 4050");
        
        // Обновляем размер экрана
        laptop.UpdateScreenSize(15.6);
        
        // Проверяем, что размер экрана обновился корректно
        Assert.That(laptop.ScreenSizeInches, Is.EqualTo(15.6));
    }

    [Test]
    public void Laptop_UpdateRam_UpdatesCorrectly()
    {
        // Создаем ноутбук с начальной ОЗУ
        var laptop = new Laptop("Dell", "XPS", "Silver", 150000m, 7000, "Windows", 15.6, "Intel Core i9", 16, 1024, "NVIDIA RTX 4060");
        
        // Обновляем ОЗУ
        laptop.UpdateRam(32);
        
        // Проверяем, что ОЗУ обновилась корректно
        Assert.That(laptop.RamGB, Is.EqualTo(32));
    }

    [Test]
    public void Laptop_PrintInfo_OutputsCorrectFormat()
    {
        // Создаем ноутбук для тестирования
        var laptop = new Laptop("Apple", "MacBook Pro", "Space Gray", 250000m, 8000, "macOS", 16.0, "Apple M3 Pro", 32, 1024, "Integrated");
        
        // Перенаправляем вывод консоли в StringWriter для проверки
        var stringWriter = new System.IO.StringWriter();
        Console.SetOut(stringWriter);
        
        // Вызываем метод печати информации
        laptop.PrintInfo();
        
        // Получаем вывод и проверяем его содержимое
        string output = stringWriter.ToString();
        Assert.That(output, Does.Contain("Ноутбук: Apple MacBook Pro"));
        Assert.That(output, Does.Contain("16"));
        Assert.That(output, Does.Contain("Apple M3 Pro"));
        Assert.That(output, Does.Contain("32"));
        Assert.That(output, Does.Contain("1024"));
        
        // Восстанавливаем стандартный вывод консоли
        Console.SetOut(new System.IO.StreamWriter(Console.OpenStandardOutput()) { AutoFlush = true });
    }
}

// Тесты для производного класса EBook
[TestFixture]
public class EBookTests
{
    [Test]
    public void EBook_Constructor_DefaultValues()
    {
        // Создаем электронную книгу с параметрами по умолчанию
        var ebook = new EBook();
        
        // Проверяем базовые поля (наследованные от MobileDevice)
        Assert.That(ebook.Brand, Is.EqualTo("Generic"));
        Assert.That(ebook.Model, Is.EqualTo("EBook"));
        Assert.That(ebook.Color, Is.EqualTo("Black"));
        Assert.That(ebook.Price, Is.EqualTo(0m));
        Assert.That(ebook.BatteryCapacity, Is.EqualTo(1500));
        Assert.That(ebook.OperatingSystem, Is.EqualTo("Custom"));
        
        // Проверяем специфичные для электронной книги поля
        Assert.That(ebook.ScreenSizeInches, Is.EqualTo(6.0));
        Assert.That(ebook.MemoryGB, Is.EqualTo(32));
        Assert.That(ebook.SupportedFormats, Is.EqualTo("EPUB, PDF, MOBI"));
        Assert.That(ebook.HasBacklight, Is.True);
    }

    [Test]
    public void EBook_Constructor_CustomValues()
    {
        // Создаем электронную книгу с пользовательскими параметрами
        var ebook = new EBook("Amazon", "Kindle Paperwhite", "Black", 15000m, 1700, "Custom", 6.8, 32, "EPUB, PDF, MOBI, AZW3", true);
        
        // Проверяем базовые поля
        Assert.That(ebook.Brand, Is.EqualTo("Amazon"));
        Assert.That(ebook.Model, Is.EqualTo("Kindle Paperwhite"));
        Assert.That(ebook.Color, Is.EqualTo("Black"));
        Assert.That(ebook.Price, Is.EqualTo(15000m));
        Assert.That(ebook.BatteryCapacity, Is.EqualTo(1700));
        Assert.That(ebook.OperatingSystem, Is.EqualTo("Custom"));
        
        // Проверяем специфичные для электронной книги поля
        Assert.That(ebook.ScreenSizeInches, Is.EqualTo(6.8));
        Assert.That(ebook.MemoryGB, Is.EqualTo(32));
        Assert.That(ebook.SupportedFormats, Is.EqualTo("EPUB, PDF, MOBI, AZW3"));
        Assert.That(ebook.HasBacklight, Is.True);
    }

    [Test]
    public void EBook_ScreenSizeInches_TooSmall_ThrowsException()
    {
        var ebook = new EBook();
        // Попытка установить размер экрана меньше 5 дюймов должна вызвать исключение
        Assert.Throws<ArgumentOutOfRangeException>(() => ebook.ScreenSizeInches = 4.0);
    }

    [Test]
    public void EBook_ScreenSizeInches_TooLarge_ThrowsException()
    {
        var ebook = new EBook();
        // Попытка установить размер экрана больше 13 дюймов должна вызвать исключение
        Assert.Throws<ArgumentOutOfRangeException>(() => ebook.ScreenSizeInches = 15.0);
    }

    [Test]
    public void EBook_MemoryGB_TooSmall_ThrowsException()
    {
        var ebook = new EBook();
        // Попытка установить объем памяти меньше 4 ГБ должна вызвать исключение
        Assert.Throws<ArgumentOutOfRangeException>(() => ebook.MemoryGB = 2);
    }

    [Test]
    public void EBook_MemoryGB_TooLarge_ThrowsException()
    {
        var ebook = new EBook();
        // Попытка установить объем памяти больше 512 ГБ должна вызвать исключение
        Assert.Throws<ArgumentOutOfRangeException>(() => ebook.MemoryGB = 1024);
    }

    [Test]
    public void EBook_SupportedFormats_EmptyValue_ThrowsException()
    {
        var ebook = new EBook();
        // Попытка установить пустые поддерживаемые форматы должна вызвать исключение
        Assert.Throws<ArgumentException>(() => ebook.SupportedFormats = "");
    }

    [Test]
    public void EBook_UpdateScreenSize_UpdatesCorrectly()
    {
        // Создаем электронную книгу с начальным размером экрана
        var ebook = new EBook("Kobo", "Clara HD", "Black", 12000m, 1500, "Custom", 6.0, 8, "EPUB, PDF, MOBI", true);
        
        // Обновляем размер экрана
        ebook.UpdateScreenSize(6.8);
        
        // Проверяем, что размер экрана обновился корректно
        Assert.That(ebook.ScreenSizeInches, Is.EqualTo(6.8));
    }

    [Test]
    public void EBook_UpdateMemory_UpdatesCorrectly()
    {
        // Создаем электронную книгу с начальным объемом памяти
        var ebook = new EBook("PocketBook", "Touch HD 3", "Black", 18000m, 2000, "Custom", 6.0, 16, "EPUB, PDF, MOBI, FB2", false);
        
        // Обновляем объем памяти
        ebook.UpdateMemory(32);
        
        // Проверяем, что объем памяти обновился корректно
        Assert.That(ebook.MemoryGB, Is.EqualTo(32));
    }

    [Test]
    public void EBook_UpdateBacklight_UpdatesCorrectly()
    {
        // Создаем электронную книгу
        var ebook = new EBook("Onyx", "Boox Note Air", "Gray", 45000m, 3000, "Custom", 10.3, 64, "EPUB, PDF, MOBI, DJVU", true);
        
        // Обновляем подсветку
        ebook.UpdateBacklight(false);
        
        // Проверяем, что подсветка обновилась корректно
        Assert.That(ebook.HasBacklight, Is.False);
    }

    [Test]
    public void EBook_PrintInfo_OutputsCorrectFormat()
    {
        // Создаем электронную книгу для тестирования
        var ebook = new EBook("Amazon", "Kindle Oasis", "Champagne", 25000m, 1400, "Custom", 7.0, 32, "EPUB, PDF, MOBI, AZW3", true);
        
        // Перенаправляем вывод консоли в StringWriter для проверки
        var stringWriter = new System.IO.StringWriter();
        Console.SetOut(stringWriter);
        
        // Вызываем метод печати информации
        ebook.PrintInfo();
        
        // Получаем вывод и проверяем его содержимое
        string output = stringWriter.ToString();
        Assert.That(output, Does.Contain("Электронная книга: Amazon Kindle Oasis"));
        Assert.That(output, Does.Contain("7"));
        Assert.That(output, Does.Contain("32"));
        Assert.That(output, Does.Contain("EPUB, PDF, MOBI, AZW3"));
        Assert.That(output, Does.Contain("подсветка: да"));
        
        // Восстанавливаем стандартный вывод консоли
        Console.SetOut(new System.IO.StreamWriter(Console.OpenStandardOutput()) { AutoFlush = true });
    }

    [Test]
    public void EBook_PrintInfo_NoBacklight_OutputsCorrectFormat()
    {
        // Создаем электронную книгу без подсветки для тестирования
        var ebook = new EBook("PocketBook", "Basic Touch 2", "Black", 8000m, 1300, "Custom", 6.0, 8, "EPUB, PDF, FB2", false);
        
        // Перенаправляем вывод консоли в StringWriter для проверки
        var stringWriter = new System.IO.StringWriter();
        Console.SetOut(stringWriter);
        
        // Вызываем метод печати информации
        ebook.PrintInfo();
        
        // Получаем вывод и проверяем его содержимое
        string output = stringWriter.ToString();
        Assert.That(output, Does.Contain("подсветка: нет"));
        
        // Восстанавливаем стандартный вывод консоли
        Console.SetOut(new System.IO.StreamWriter(Console.OpenStandardOutput()) { AutoFlush = true });
    }
}
