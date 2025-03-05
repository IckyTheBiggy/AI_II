public static class Logger
{
    public static void Log(string message)
    {
        Console.WriteLine("[LOG]" + message);
    }

    public static void LogWarning(string message)
    {
        Console.WriteLine("[WARNING]" + message);
    }
    
    public static void LogError(string message)
    {
        Console.WriteLine("[ERROR]" + message);
    }
}