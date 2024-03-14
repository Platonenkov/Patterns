// <copyright file="Program.cs" company="O_o">
// Copyright (c) Company. All rights reserved.
// </copyright>

namespace Observer;

internal class Program
{
    private static void Main()
    {
        var stock = new Stock();
        var bank = new Bank(name: "ЮнитБанк", stock);
        Console.WriteLine($"Банк: {bank.Name}");
        var broker = new Broker(name: "Иван Иваныч", stock);
        Console.WriteLine($"Брокер: {broker.Name}");

        // имитация торгов
        stock.Market();

        // брокер прекращает наблюдать за торгами
        broker.StopTrade();

        // имитация торгов
        stock.Market();

        Console.Read();
    }
}

internal interface IObserver
{
    void Update(object ob);
}

internal interface IObservable
{
    void RegisterObserver(IObserver o);

    void RemoveObserver(IObserver o);

    void NotifyObservers();
}

internal class Stock : IObservable
{
    private readonly StockInfo sInfo; // информация о торгах

    private readonly List<IObserver> observers;

    public Stock()
    {
        this.observers = new List<IObserver>();
        this.sInfo = new StockInfo();
    }

    public void RegisterObserver(IObserver o) { this.observers.Add(o); }

    public void RemoveObserver(IObserver o) { this.observers.Remove(o); }

    public void NotifyObservers()
    {
        foreach (var o in this.observers)
        {
            o.Update(this.sInfo);
        }
    }

    public void Market()
    {
        var rnd = new Random();
        this.sInfo.Usd = rnd.Next(minValue: 20, maxValue: 40);
        this.sInfo.Euro = rnd.Next(minValue: 30, maxValue: 50);
        this.NotifyObservers();
    }
}

internal class StockInfo
{
    public int Usd { get; set; }

    public int Euro { get; set; }
}

internal class Broker : IObserver
{
    public string Name { get; set; }

    private IObservable? stock;

    public Broker(string name, IObservable? obs)
    {
        this.Name = name;
        this.stock = obs;
        this.stock!.RegisterObserver(this);
    }

    public void Update(object ob)
    {
        var sInfo = (StockInfo)ob;

        Console.WriteLine(
            sInfo.Usd > 30
                ? "Брокер {0} продает доллары;  Курс доллара: {1}"
                : "Брокер {0} покупает доллары;  Курс доллара: {1}",
            this.Name,
            sInfo.Usd);
    }

    public void StopTrade()
    {
        this.stock!.RemoveObserver(this);
        this.stock = null;
    }
}

internal class Bank : IObserver
{
    public string Name { get; set; }

    private readonly IObservable? stock;

    public Bank(string name, IObservable? obs)
    {
        this.Name = name;
        this.stock = obs;
        this.stock!.RegisterObserver(this);
    }

    public void Update(object ob)
    {
        var sInfo = (StockInfo)ob;

        if (sInfo.Euro > 40)
        {
            Console.WriteLine(format: "Банк {0} продает евро;  Курс евро: {1}", this.Name, sInfo.Euro);
        }
        else
        {
            Console.WriteLine(format: "Банк {0} покупает евро;  Курс евро: {1}", this.Name, sInfo.Euro);
        }
    }
}
