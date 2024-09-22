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
    class TaxiDriver : AbstractPerson, RideInterface
    {
        public string CarModel { get; set; }
        public string LicenseNumber { get; set; }

        public TaxiDriver(string name, string phoneNumber, string carModel, string licenseNumber) 
            : base(name, phoneNumber)
        {
            CarModel = carModel;
            LicenseNumber = licenseNumber;
        }

        // Прийняття замовлення
        public void AcceptOrder()
        {
            Console.WriteLine($"{Name} has accepted an order.");
        }

        // Перевантажений метод прийняття замовлення з пріоритетом
        public void AcceptOrder(int priority)
        {
            Console.WriteLine($"{Name} has accepted an order with priority {priority}.");
        }

        // Побудова маршруту
        public void BuildRoute()
        {
            Console.WriteLine($"{Name} is building a route.");
        }

        // Завершення поїздки
        public void MarkComplete()
        {
            Console.WriteLine($"{Name} has completed the ride.");
        }

        // Перевизначений метод логіну
        public override void Login()
        {
            Console.WriteLine($"{Name} has logged in as a taxi driver.");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Створення клієнта
            Client client = new Client("John Doe", "123-456-7890", "Central Park", 20.5);
            client.Login();
            client.PublishOrder();
            client.PayForRide();

            // Створення водія таксі
            TaxiDriver driver = new TaxiDriver("Alice Smith", "098-765-4321", "Toyota Camry", "XYZ123");
            driver.Login();
            driver.AcceptOrder();
            driver.BuildRoute();
            driver.MarkComplete();
        }
    }
}
