using System;
using System.IO;

public static class Logger
{
    private static string logFilePath = "user_actions.log"; // Default log file name

    public static void LogAction(string actionMessage)
    {
        try
        {
            string logEntry = $"{DateTime.Now}: {actionMessage}";
            File.AppendAllText(logFilePath, logEntry + Environment.NewLine);
            Console.WriteLine($"Logged: {logEntry}"); // Also print to console for immediate feedback
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error logging action: {ex.Message}");
        }
    }

    // Optional: A method to set a custom log file path
    public static void SetLogFilePath(string path)
    {
        if (!string.IsNullOrWhiteSpace(path))
        {
            logFilePath = path;
        }
        else
        {
            Console.WriteLine("Log file path cannot be empty or null.");
        }
    }
}
