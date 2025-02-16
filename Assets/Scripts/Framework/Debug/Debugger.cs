using System;
using System.IO;
using UnityEngine;

public class Debugger
{
    private static string logFilePath = Application.persistentDataPath + "/GameLog.txt";

    /// <summary>
    /// 调试日志
    /// </summary>
    /// <param name="message">打印信息</param>
    /// <param name="enableFileLogging">是否存储本地 => 默认不存储</param>
    /// <param name="enableConsoleLogging">是否打印到控制台 => 默认打印</param>
    public static void Info(string message, bool enableFileLogging = false, bool enableConsoleLogging = true)
    {
        Log(message, E_LogLevel.Info, E_LogColor.White, enableFileLogging, enableConsoleLogging);
    }

    /// <summary>
    /// 警告日志
    /// </summary>
    /// <param name="message">打印信息</param>
    /// <param name="enableFileLogging">是否存储本地 => 默认不存储</param>
    /// <param name="enableConsoleLogging">是否打印到控制台 => 默认打印</param>
    public static void Warning(string message, bool enableFileLogging = false, bool enableConsoleLogging = true)
    {
        Log(message, E_LogLevel.Warning, E_LogColor.Yellow, enableFileLogging, enableConsoleLogging);
    }

    /// <summary>
    /// 错误日志
    /// </summary>
    /// <param name="message">打印信息</param>
    /// <param name="enableFileLogging">是否存储本地 => 默认存储</param>
    /// <param name="enableConsoleLogging">是否打印到控制台 => 默认打印</param>
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
}