// <copyright file="Program.cs" company="O_o">
// Copyright (c) Company. All rights reserved.
// </copyright>

namespace Adatpter;

internal class Program
{
    private static void Main()
    {
        // путешественник
        var driver = new Driver();

        // машина
        var auto = new Auto();

        // отправляемся в путешествие
        driver.Travel(auto);

        // встретились пески, надо использовать верблюда
        var camel = new Camel();

        // используем адаптер
        ITransport camelTransport = new CamelToTransportAdapter(camel);

        // продолжаем путь по пескам пустыни
        driver.Travel(camelTransport);

        Console.Read();
    }
}

internal interface ITransport
{
    void Drive();
}

// класс машины
internal class Auto : ITransport
{
    public void Drive() { Console.WriteLine(value: "Машина едет по дороге"); }
}

internal class Driver
{
    public void Travel(ITransport transport) { transport.Drive(); }
}

// интерфейс животного
internal interface IAnimal
{
    void Move();
}

// класс верблюда
internal class Camel : IAnimal
{
    public void Move() { Console.WriteLine(value: "Верблюд идет по пескам пустыни"); }
}

// Адаптер от Camel к ITransport
internal class CamelToTransportAdapter : ITransport
{
    private readonly Camel camel;

    public CamelToTransportAdapter(Camel c) { this.camel = c; }

    public void Drive() { this.camel.Move(); }
}
