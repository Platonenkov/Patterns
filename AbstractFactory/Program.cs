// <copyright file="Program.cs" company="O_o">
// Copyright (c) Company. All rights reserved.
// </copyright>

namespace AbstractFactory;

internal class Program
{
    private static void Main()
    {
        var elf = new Hero(new ElfFactory());
        elf.Hit();
        elf.Run();

        var voin = new Hero(new VoinFactory());
        voin.Hit();
        voin.Run();

        Console.ReadLine();
    }
}

//абстрактный класс - оружие
internal abstract class Weapon
{
    public abstract void Hit();
}

// абстрактный класс движение
internal abstract class Movement
{
    public abstract void Move();
}

// класс арбалет
internal class Arbalet : Weapon
{
    public override void Hit() { Console.WriteLine(value: "Стреляем из арбалета"); }
}

// класс меч
internal class Sword : Weapon
{
    public override void Hit() { Console.WriteLine(value: "Бьем мечом"); }
}

// движение полета
internal class FlyMovement : Movement
{
    public override void Move() { Console.WriteLine(value: "Летим"); }
}

// движение - бег
internal class RunMovement : Movement
{
    public override void Move() { Console.WriteLine(value: "Бежим"); }
}

// класс абстрактной фабрики
internal abstract class HeroFactory
{
    public abstract Movement CreateMovement();

    public abstract Weapon CreateWeapon();
}

// Фабрика создания летящего героя с арбалетом
internal class ElfFactory : HeroFactory
{
    public override Movement CreateMovement() { return new FlyMovement(); }

    public override Weapon CreateWeapon() { return new Arbalet(); }
}

// Фабрика создания бегущего героя с мечом
internal class VoinFactory : HeroFactory
{
    public override Movement CreateMovement() { return new RunMovement(); }

    public override Weapon CreateWeapon() { return new Sword(); }
}

// клиент - сам супергерой
internal class Hero
{
    private readonly Weapon weapon;

    private readonly Movement movement;

    public Hero(HeroFactory factory)
    {
        this.weapon = factory.CreateWeapon();
        this.movement = factory.CreateMovement();
    }

    public void Run() { this.movement.Move(); }

    public void Hit() { this.weapon.Hit(); }
}
