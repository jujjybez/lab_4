using System;
using System.Collections;

public class Car
{
    public string Name { get; set; }
    public int ProductionYear { get; set; }
    public int MaxSpeed { get; set; }
}

public class CarComparer : IComparer<Car>
{
    private string sortBy;
    public CarComparer(string sortBy) { this.sortBy = sortBy; }
    public int Compare(Car x, Car y)
    {
        switch (sortBy)
        {
            case "Name":
                return string.Compare(x.Name, y.Name);
            case "ProductionYear":
                return x.ProductionYear.CompareTo(y.ProductionYear);
            case "MaxSpeed":
                return x.MaxSpeed.CompareTo(y.MaxSpeed);
            default:
                throw new ArgumentException("Invalid sort option");
        }
    }
}

public class CarCatalog : IEnumerable<Car>
{
    private Car[] cars;
    public CarCatalog(Car[] cars) { this.cars = cars; }

    // Прямой проход с первого элемента до последнего
    public IEnumerator<Car> GetEnumerator()
    {
        for (int i = 0; i < cars.Length; i++) { yield return cars[i]; }
    }

    // Обратный проход от последнего к первому
    public IEnumerable<Car> GetReverseEnumerator()
    {
        for (int i = cars.Length - 1; i >= 0; i--) { yield return cars[i]; }
    }

    // Проход по элементам массива с фильтром по году выпуска
    public IEnumerable<Car> GetByProductionYearEnumerator(int year)
    {
        foreach (Car car in cars)
        {
            if (car.ProductionYear == year) { yield return car; }
        }
    }

    // Проход по элементам массива с фильтром по максимальной скорости
    public IEnumerable<Car> GetByMaxSpeedEnumerator(int speed)
    {
        foreach (Car car in cars)
        {
            if (car.MaxSpeed == speed) { yield return car; }
        }
    }

    IEnumerator IEnumerable.GetEnumerator() { return GetEnumerator(); }
}

class Program
{
    static void Main()
    {
        Car[] cars = new Car[]
        {
            new Car { Name = "Toyota", ProductionYear = 2019, MaxSpeed = 180 },
            new Car { Name = "BMW", ProductionYear = 2018, MaxSpeed = 200 },
            new Car { Name = "Maserati", ProductionYear = 2020, MaxSpeed = 300 }
        };

        // Сортировка по названию
        Array.Sort(cars, new CarComparer("Name"));
        Console.WriteLine("Sort by name:");
        foreach (var car in cars) { Console.WriteLine($"{car.Name}, {car.ProductionYear}, {car.MaxSpeed}"); }

        // Сортировка по году выпуска
        Array.Sort(cars, new CarComparer("ProductionYear"));
        Console.WriteLine("\nSort by year of production:");
        foreach (var car in cars) { Console.WriteLine($"{car.Name}, {car.ProductionYear}, {car.MaxSpeed}"); }

        // Сортировка по максимальной скорости
        Array.Sort(cars, new CarComparer("MaxSpeed"));
        Console.WriteLine("\nSort by max speed:");
        foreach (var car in cars) { Console.WriteLine($"{car.Name}, {car.ProductionYear}, {car.MaxSpeed}"); }

        Console.WriteLine();

        CarCatalog catalog = new CarCatalog(cars);

        foreach (Car car in catalog) { Console.WriteLine(car.Name); }
        Console.WriteLine();

        foreach (Car car in catalog.GetReverseEnumerator()) { Console.WriteLine(car.Name); }
        Console.WriteLine();

        foreach (Car car in catalog.GetByProductionYearEnumerator(2020)) { Console.WriteLine(car.Name); }
        Console.WriteLine();

        foreach (Car car in catalog.GetByMaxSpeedEnumerator(200)) { Console.WriteLine(car.Name); }
        Console.WriteLine();
    }
}
