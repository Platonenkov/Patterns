// <copyright file="Program.cs" company="O_o">
// Copyright (c) Company. All rights reserved.
// </copyright>

namespace IoC;

internal class Program
{
    private static void Main()
    {
        var serviceWithFileLogger = new ProductService(new FileLogger());
        var serviceWithDatabaseLogger = new ProductService(new DatabaseLogger());

        serviceWithFileLogger.Log(nameof(serviceWithFileLogger));
        serviceWithDatabaseLogger.Log(nameof(serviceWithDatabaseLogger));
        Console.ReadLine();
    }
}

public interface ILogger
{
    void Log(string message);
}

public class FileLogger : ILogger
{
    public void Log(string message)
    {
        Console.WriteLine(value: "Inside Log method of FileLogger.");
        LogToFile(message);
    }

    private static void LogToFile(string message)
    {
        Console.WriteLine(format: "Method: LogToFile, Text: {0}", message);
    }
}

public class DatabaseLogger : ILogger
{
    public void Log(string message)
    {
        Console.WriteLine(value: "Inside Log method of DatabaseLogger.");
        LogToDatabase(message);
    }

    private static void LogToDatabase(string message)
    {
        Console.WriteLine(format: "Method: LogToDatabase, Text: {0}", message);
    }
}

public class ProductService
{
    private readonly ILogger logger;

    public ProductService(ILogger logger) { this.logger = logger; }

    public void Log(string message) { this.logger.Log(message); }
}
