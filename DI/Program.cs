// <copyright file="Program.cs" company="O_o">
// Copyright (c) Company. All rights reserved.
// </copyright>

namespace DI;

using Microsoft.Extensions.DependencyInjection;

internal class Program
{
    private static void Main()
    {
        var services = new ServiceCollection()
            .AddTransient<ILogService, SimpleLogService>()
            .AddScoped<SimpleService>();

        using var serviceProvider = services.BuildServiceProvider();

        // получаем сервис ILogService
        var logService = serviceProvider.GetService<ILogService>();
        var service = serviceProvider.GetRequiredService<SimpleService>();

        // используем сервис
        logService?.Write(message: "Hello world");
        service.Write(message: "from new service");
        Console.ReadLine();
    }
}

internal interface ILogService
{
    void Write(string message);
}

internal class SimpleLogService : ILogService
{
    public void Write(string message) { Console.WriteLine(message); }
}

internal class SimpleService
{
    private readonly ILogService service;

    public SimpleService(ILogService service) { this.service = service; }

    public void Write(string message) { this.service.Write(message); }
}
