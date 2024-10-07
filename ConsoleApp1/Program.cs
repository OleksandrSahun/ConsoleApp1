using System;

namespace TaxiService
{
    // Абстрактний клас AbstractPerson
    abstract class AbstractPerson
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }

        protected AbstractPerson(string name, string phoneNumber)
        {
            Name = name;
            PhoneNumber = phoneNumber;
        }

        // Абстрактний метод для логіну
        public abstract bool Login(string phoneNumber);
    }

    // Інтерфейс RideInterface
    interface RideInterface
    {
        void BuildRoute(string currentLocation, string destination);
        double CalculateRideCost(double distance);
        void MarkComplete();
    }

    // Клас автомобіля
    class Car
    {
        public string Model { get; private set; }
        public string LicenseNumber { get; private set; }

        public Car(string model, string licenseNumber)
        {
            Model = model;
            LicenseNumber = licenseNumber;
        }
    }

    // Клас клієнта, наслідує AbstractPerson
    class Client : AbstractPerson
    {
        public string Destination { get; private set; }
        public double Balance { get; private set; }

        public Client(string name, string phoneNumber, string destination, double balance)
            : base(name, phoneNumber)
        {
            Destination = destination;
            Balance = balance;
        }

        // Публікація замовлення
        public void RequestRide(TaxiDriver driver, string currentLocation)
        {
            Console.WriteLine($"{Name} опублікував замовлення до {Destination}.");
            driver.AcceptOrder(this, currentLocation);
        }

        // Оплата поїздки
        public bool PayForRide(double amount)
        {
            if (Balance >= amount)
            {
                Balance -= amount;
                Console.WriteLine($"{Name} оплатив {amount} грн. Залишок на рахунку: {Balance} грн.");
                return true;
            }
            else
            {
                Console.WriteLine($"{Name} не може оплатити поїздку. Недостатньо коштів.");
                return false;
            }
        }

        public override bool Login(string phoneNumber)
        {
            if (PhoneNumber == phoneNumber)
            {
                Console.WriteLine($"{Name} успішно увійшов у систему як клієнт.");
                return true;
            }
            Console.WriteLine("Помилка входу.");
            return false;
        }
    }

    // Клас водія таксі, наслідує AbstractPerson та реалізує інтерфейс RideInterface
    class TaxiDriver : AbstractPerson, RideInterface
    {
        public Car Car { get; private set; }
        private string CurrentRoute { get; set; }
        private double Distance { get; set; }

        private const double BaseFare = 10.0; // Базова плата
        private const double FarePerKm = 5.0; // Ціна за кілометр

        public TaxiDriver(string name, string phoneNumber, Car car)
            : base(name, phoneNumber)
        {
            Car = car;
        }

        // Прийняття замовлення
        public void AcceptOrder(Client client, string currentLocation)
        {
            Console.WriteLine($"{Name} прийняв замовлення від {client.Name}.");
            BuildRoute(currentLocation, client.Destination);
            Distance = new Random().Next(5, 20); // Генерація відстані 5-20 км для прикладу
            double cost = CalculateRideCost(Distance);
            Console.WriteLine($"Відстань: {Distance} км. Вартість поїздки: {cost} грн.");

            // Платіж
            if (client.PayForRide(cost))
            {
                MarkComplete();
            }
            else
            {
                Console.WriteLine("Поїздку не завершено через несплату.");
            }
        }

        // Побудова маршруту
        public void BuildRoute(string currentLocation, string destination)
        {
            CurrentRoute = $"{currentLocation} -> {destination}";
            Console.WriteLine($"{Name} будує маршрут: {CurrentRoute}.");
        }

        // Розрахунок вартості поїздки
        public double CalculateRideCost(double distance)
        {
            return BaseFare + (FarePerKm * distance);
        }

        // Завершення поїздки
        public void MarkComplete()
        {
            Console.WriteLine($"{Name} завершив поїздку за маршрутом: {CurrentRoute}.");
            CurrentRoute = null;
        }

        public override bool Login(string phoneNumber)
        {
            if (PhoneNumber == phoneNumber)
            {
                Console.WriteLine($"{Name} успішно увійшов у систему як водій.");
                return true;
            }
            Console.WriteLine("Помилка входу.");
            return false;
        }
    }

    // Клас програми
    class Program
    {
        static void Main(string[] args)
        {
            // Створюємо машину
            Car car = new Car("Toyota Camry", "XYZ123");

            // Створюємо водія
            TaxiDriver driver = new TaxiDriver("Аліса Сміт", "098-765-4321", car);
            if (!driver.Login("098-765-4321")) return;

            // Створюємо клієнта
            Client client = new Client("Джон Доу", "123-456-7890", "Центральний парк", 60.00);
            if (!client.Login("123-456-7890")) return;

            // Клієнт запитує поїздку
            client.RequestRide(driver, "Таймс-Сквер");
        }
    }
}
