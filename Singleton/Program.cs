// <copyright file="Program.cs" company="O_o">
// Copyright (c) Company. All rights reserved.
// </copyright>

namespace Singleton;

internal class Program
{
    private static void Main()
    {
        var comp = new Computer();
        comp.Launch(osName: "Windows 8.1");
        Console.WriteLine(comp.Os.Name);

        // у нас не получится изменить ОС, так как объект уже создан    
        comp.Os = Os.GetInstance(name: "Windows 10");
        Console.WriteLine(comp.Os.Name);

        Console.ReadLine();
    }
}

internal class Computer
{
    public Os Os { get; set; } = null!;

    public void Launch(string osName) { this.Os = Os.GetInstance(osName); }
}

internal class Os
{
    private static Os instance = null!;

    public string Name { get; private set; }

    protected Os(string name) { this.Name = name; }

    public static Os GetInstance(string name) { return instance ??= new Os(name); }
}
