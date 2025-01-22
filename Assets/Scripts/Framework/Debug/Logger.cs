using System;
using System.IO;
using UnityEngine;

public class Logger
{
    private static string logFilePath = Application.persistentDataPath + "/GameLog.txt";

    public static void Info(string message, bool enableFileLogging = true, bool enableConsoleLogging = true)
    {
        Log(message, E_LogLevel.Info, E_LogColor.White, enableFileLogging, enableConsoleLogging);
    }

    public static void Warning(string message, bool enableFileLogging = true, bool enableConsoleLogging = true)
    {
        Log(message, E_LogLevel.Warning, E_LogColor.Yellow, enableFileLogging, enableConsoleLogging);
    }

    public static void Error(string message, bool enableFileLogging = true, bool enableConsoleLogging = true)
    {
        Log(message, E_LogLevel.Error, E_LogColor.Red, enableFileLogging, enableConsoleLogging);
    }

    private static void Log(string message, E_LogLevel logLevel, E_LogColor logColor, bool enableFileLogging = true,
        bool enableConsoleLogging = true)
    {
        string logMessage = message;
        string colorizedMessage = $"<color={GetColorString(logColor)}>{logMessage}</color>";

        // 控制台日志输出
        if (enableConsoleLogging)
        {
            switch (logLevel)
            {
                case E_LogLevel.Info:
                    Debug.Log(colorizedMessage);
                    break;
                case E_LogLevel.Warning:
                    Debug.LogWarning(colorizedMessage);
                    break;
                case E_LogLevel.Error:
                    Debug.LogError(colorizedMessage);
                    break;
            }
        }

        // 文件日志记录
        if (enableFileLogging)
        {
            try
            {
                string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                string saveMessage = $"[{timestamp}][{logLevel}] {logMessage}";
                File.AppendAllText(logFilePath, saveMessage + Environment.NewLine);
            }
            catch (Exception ex)
            {
                Debug.LogError($"Failed to write log to file: {ex.Message}");
            }
        }
    }

    private static string GetColorString(E_LogColor logColor)
    {
        return logColor switch
        {
            E_LogColor.White => "white",
            E_LogColor.Yellow => "yellow",
            E_LogColor.Red => "red",
            _ => "white",
        };
    }

    private enum E_LogLevel
    {
        Info,
        Warning,
        Error
    }

    private enum E_LogColor
    {
        White,
        Yellow,
        Red
    }
}