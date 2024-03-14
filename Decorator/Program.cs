// <copyright file="Program.cs" company="O_o">
// Copyright (c) Company. All rights reserved.
// </copyright>

namespace Decorator;

internal class Program
{
    private static void Main()
    {
        Pizza pizza1 = new ItalianPizza();
        pizza1 = new TomatoPizza(pizza1); // итальянская пицца с томатами
        Console.WriteLine(format: "Название: {0}", pizza1.Name);
        Console.WriteLine(format: "Цена: {0}", pizza1.GetCost());

        Pizza pizza2 = new ItalianPizza();
        pizza2 = new CheesePizza(pizza2); // итальянская пиццы с сыром
        Console.WriteLine(format: "Название: {0}", pizza2.Name);
        Console.WriteLine(format: "Цена: {0}", pizza2.GetCost());

        Pizza pizza3 = new BulgerianPizza();
        pizza3 = new TomatoPizza(pizza3);
        pizza3 = new CheesePizza(pizza3); // болгарская пиццы с томатами и сыром
        Console.WriteLine(format: "Название: {0}", pizza3.Name);
        Console.WriteLine(format: "Цена: {0}", pizza3.GetCost());

        Console.ReadLine();
    }
}

internal abstract class Pizza
{
    public Pizza(string n) { this.Name = n; }

    public string Name { get; protected set; }

    public abstract int GetCost();
}

internal class ItalianPizza : Pizza
{
    public ItalianPizza() : base(n: "Итальянская пицца") { }

    public override int GetCost() { return 10; }
}

internal class BulgerianPizza : Pizza
{
    public BulgerianPizza()
        : base(n: "Болгарская пицца")
    {
    }

    public override int GetCost() { return 8; }
}

internal abstract class PizzaDecorator : Pizza
{
    protected Pizza pizza;

    public PizzaDecorator(string n, Pizza pizza) : base(n) { this.pizza = pizza; }
}

internal class TomatoPizza : PizzaDecorator
{
    public TomatoPizza(Pizza p)
        : base(p.Name + ", с томатами", p)
    {
    }

    public override int GetCost() { return this.pizza.GetCost() + 3; }
}

internal class CheesePizza : PizzaDecorator
{
    public CheesePizza(Pizza p)
        : base(p.Name + ", с сыром", p)
    {
    }

    public override int GetCost() { return this.pizza.GetCost() + 5; }
}
