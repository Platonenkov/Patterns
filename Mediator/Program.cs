// <copyright file="Program.cs" company="O_o">
// Copyright (c) Company. All rights reserved.
// </copyright>

namespace Mediator;

internal class Program
{
    private static void Main()
    {
        var mediator = new ManagerMediator();
        Colleague customer = new CustomerColleague(mediator);
        Colleague programmer = new ProgrammerColleague(mediator);
        Colleague tester = new TesterColleague(mediator);
        mediator.Customer = customer;
        mediator.Programmer = programmer;
        mediator.Tester = tester;
        customer.Send(message: "Есть заказ, надо сделать программу");
        programmer.Send(message: "Программа готова, надо протестировать");
        tester.Send(message: "Программа протестирована и готова к продаже");

        Console.Read();
    }
}

internal abstract class Mediator
{
    public abstract void Send(string msg, Colleague colleague);
}

internal abstract class Colleague
{
    protected Mediator mediator;

    protected Colleague(Mediator mediator) { this.mediator = mediator; }

    public virtual void Send(string message) { this.mediator.Send(message, this); }

    public abstract void Notify(string message);
}

// класс заказчика
internal class CustomerColleague : Colleague
{
    public CustomerColleague(Mediator mediator)
        : base(mediator)
    {
    }

    public override void Notify(string message) { Console.WriteLine("Сообщение заказчику: " + message); }
}

// класс программиста
internal class ProgrammerColleague : Colleague
{
    public ProgrammerColleague(Mediator mediator)
        : base(mediator)
    {
    }

    public override void Notify(string message) { Console.WriteLine("Сообщение программисту: " + message); }
}

// класс тестера
internal class TesterColleague : Colleague
{
    public TesterColleague(Mediator mediator)
        : base(mediator)
    {
    }

    public override void Notify(string message) { Console.WriteLine("Сообщение тестеру: " + message); }
}

internal class ManagerMediator : Mediator
{
    public Colleague? Customer { get; set; }

    public Colleague? Programmer { get; set; }

    public Colleague? Tester { get; set; }

    public override void Send(string msg, Colleague colleague)
    {
        // если отправитель - заказчик, значит есть новый заказ
        // отправляем сообщение программисту - выполнить заказ
        if (this.Customer == colleague)
        {
            this.Programmer!.Notify(msg);
        }

        // если отправитель - программист, то можно приступать к тестированию
        // отправляем сообщение тестеру
        else if (this.Programmer == colleague)
        {
            this.Tester!.Notify(msg);
        }

        // если отправитель - тест, значит продукт готов
        // отправляем сообщение заказчику
        else if (this.Tester == colleague)
        {
            this.Customer!.Notify(msg);
        }
    }
}
