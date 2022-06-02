// Decompiled with JetBrains decompiler
// Type: JCCoreLib3.AppInfo
// Assembly: JCCoreLib3, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86B13A0B-23CB-4E52-86DD-8B43177166C4
// Assembly location: C:\Users\FX1\Downloads\JCCoreLib3.dll

using System;
using System.Diagnostics;
using System.IO;

namespace JCCoreLib3
{
  public class AppInfo
  {
    private AppInfo()
    {
    }

    public static string GetAppName() => Path.GetFileName(Process.GetCurrentProcess().MainModule.ModuleName).Replace(".vshost.exe", "");

    public static string GetAppBasePath() => AppDomain.CurrentDomain.BaseDirectory;

    private static string GetIniPath() => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ini");

    private static string GetLogPath() => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "log");

    private static string GetCurrentProcessWorkingSet64InKB() => (AppInfo.GetCurrentProcessWorkingSet64() / 1024L).ToString("0,000 K");

    private static long GetCurrentProcessWorkingSet64() => Process.GetCurrentProcess().WorkingSet64;

    public static string GetMethodName(int frame = 1) => new StackTrace().GetFrame(frame).GetMethod().Name;

    public override string ToString() => "hello";
  }
}
