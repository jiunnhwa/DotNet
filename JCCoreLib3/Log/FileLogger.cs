using System;
using System.IO;
using System.Threading;

namespace JCCoreLib3.Log
{
  public class FileLogger
  {
    private static readonly object locker = new object();
    private static DateTime LogDate = DateTime.Today;
    private static string LogFileFullName = FileLogger.GetLogFileName();

    private FileLogger()
    {
    }

    public static string GetLogFileName() => Path.Combine(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "log"), string.Format("{0}.{1}.log", (object) AppInfo.GetAppName(), (object) DateTime.Now.ToString("yyyyMMdd"))).Replace("file:\\", "");

    public static void Log(string message) => FileLogger.AppendLog(FileLogger.LogFileFullName, message);

    public static void LogNone(string message) => FileLogger.AppendLog(FileLogger.LogFileFullName, message, FileLogger.LogLevel.NONE);

    public static void LogInfo(string message) => FileLogger.AppendLog(FileLogger.LogFileFullName, message);

    public static void LogWarning(string message) => FileLogger.AppendLog(FileLogger.LogFileFullName, message, FileLogger.LogLevel.WARNING);

    public static void LogErr(string message) => FileLogger.AppendLog(FileLogger.LogFileFullName, message, FileLogger.LogLevel.ERROR);

    public static void LogDebug(string message) => FileLogger.AppendLog(FileLogger.LogFileFullName, message, FileLogger.LogLevel.DEBUG);

    public static void LogFatal(string message) => FileLogger.AppendLog(FileLogger.LogFileFullName, message, FileLogger.LogLevel.FATAL);

    public static void AppendLogMT(string LogFile, string message, FileLogger.LogLevel MsgLogLevel = FileLogger.LogLevel.INFO) => new Thread((ThreadStart) (() => FileLogger.AppendLog(FileLogger.LogFileFullName, message, MsgLogLevel))).Start();

    public static void AppendLog(string LogFile, string message, FileLogger.LogLevel MsgLogLevel = FileLogger.LogLevel.INFO)
    {
      int num = 0;
      if (DateTime.Today > FileLogger.LogDate)
      {
        FileLogger.LogDate = DateTime.Today;
        FileLogger.LogFileFullName = FileLogger.GetLogFileName();
      }
      DateTime now = DateTime.Now;
      long ticks = now.Ticks;
      while (true)
      {
        try
        {
          lock (FileLogger.locker)
          {
            using (StreamWriter streamWriter1 = File.AppendText(LogFile))
            {
              StreamWriter streamWriter2 = streamWriter1;
              now = DateTime.Now;
              string str = string.Format("{0,-25}|{1,-6}|{2}", (object) now.ToString("ddd yyyy-MM-dd HH:mm:ss").ToUpper(), (object) MsgLogLevel.ToString(), (object) message);
              streamWriter2.WriteLine(str);
              break;
            }
          }
        }
        catch (DirectoryNotFoundException ex)
        {
          Console.WriteLine(Utility.Formatting.ExceptionFormat((Exception) ex));
          Directory.CreateDirectory(Path.GetDirectoryName(FileLogger.LogFileFullName));
        }
        catch (IOException ex)
        {
          if (num++ >= 100)
            break;
          Console.WriteLine("tries:{0}\r\n{1}", (object) num, (object) Utility.Formatting.ExceptionFormat((Exception) ex));
          Thread.Sleep(new Random().Next(100, 1000));
        }
        catch (Exception ex)
        {
          Console.WriteLine(Utility.Formatting.ExceptionFormat(ex));
          string withoutExtension = Path.GetFileNameWithoutExtension(LogFile);
          FileLogger.AppendLogMT(LogFile.Replace(withoutExtension, withoutExtension + "[DEBUG]"), Utility.Formatting.ExceptionFormat(ex), FileLogger.LogLevel.FATAL);
          break;
        }
      }
    }

    public enum LogLevel
    {
      NONE,
      INFO,
      WARNING,
      DEBUG,
      ERROR,
      FATAL,
    }
  }
}
