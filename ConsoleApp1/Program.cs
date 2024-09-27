using System;

namespace TaxiService
{
    // Абстрактний клас AbstractPerson
    abstract class AbstractPerson
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }

        public AbstractPerson(string name, string phoneNumber)
        {
            Name = name;
            PhoneNumber = phoneNumber;
        }

        // Абстрактний метод логіну
        public abstract void Login();
    }

    // Інтерфейс RideInterface
    interface RideInterface
    {
        void BuildRoute();
        void MarkComplete();
    }

    // Клас клієнта, наслідує AbstractPerson
    class Client : AbstractPerson
    {
        public string Destination { get; set; }
        public double PaymentAmount { get; set; }

        public Client(string name, string phoneNumber, string destination, double paymentAmount) 
            : base(name, phoneNumber)
        {
            Destination = destination;
            PaymentAmount = paymentAmount;
        }

        // Публікація замовлення
        public void PublishOrder()
        {
            Console.WriteLine($"{Name} has published an order to {Destination}.");
        }

        // Оплата поїздки
        public void PayForRide()
        {
            Console.WriteLine($"{Name} has paid {PaymentAmount} for the ride.");
        }

        public override void Login()
        {
            Console.WriteLine($"{Name} has logged in as a client.");
        }
    }

    // Клас водія таксі, наслідує AbstractPerson та реалізує інтерфейс RideInterface
// Класс Машина
class Car
{
    public string Model { get; set; }
    public string LicenseNumber { get; set; }

    public Car(string model, string licenseNumber)
    {
        Model = model;
        LicenseNumber = licenseNumber;
    }
}

// Класс водія таксі, наслідує AbstractPerson та використовує композицію
class TaxiDriver : AbstractPerson, RideInterface
{
    public Car Car { get; set; }

    public TaxiDriver(string name, string phoneNumber, Car car)
        : base(name, phoneNumber)
    {
        Car = car;
    }

    public void AcceptOrder()
    {
        Console.WriteLine($"{Name} has accepted an order.");
    }

    public void BuildRoute()
    {
        Console.WriteLine($"{Name} is building a route.");
    }

    public void MarkComplete()
    {
        Console.WriteLine($"{Name} has completed the ride.");
    }

    public override void Login()
    {
        Console.WriteLine($"{Name} has logged in as a taxi driver.");
    }
}

// В методі Main
class Program
{
    static void Main(string[] args)
    {
        // Создаём машину
        Car car = new Car("Toyota Camry", "XYZ123");

        // Створення водія таксі з композицією
        TaxiDriver driver = new TaxiDriver("Alice Smith", "098-765-4321", car);
        driver.Login();
        driver.AcceptOrder();
        driver.BuildRoute();
        driver.MarkComplete();
    }
}
