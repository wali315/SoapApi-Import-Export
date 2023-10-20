using System.IO;
using System;

public class Logger
{
    private string logFilePath;

    public Logger(string logFilePath)
    {
        if (logFilePath == null)
        {
            Console.WriteLine("logFilePath is null in Logger constructor.");
        }
        this.logFilePath = logFilePath;
    }

    public void LogMessage(string message)
    {
        if (logFilePath == null)
        {
            Console.WriteLine("logFilePath is null in LogMessage.");
        }

        using (StreamWriter writer = new StreamWriter(logFilePath, true))
        {
            writer.WriteLine($"{DateTime.Now} - {message}");
        }
    }
}