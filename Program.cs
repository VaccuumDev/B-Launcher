global using Serilog;
using Avalonia;
//using Serilog;
using System;
using System.IO;

namespace BW_Launcher;

sealed class Program
{

    [STAThread]
    public static void Main(string[] args)
    {
        using var log = new LoggerConfiguration()
            .WriteTo.File(Path.Combine(AppContext.BaseDirectory, "bw-launcher-runtime-log.txt")
            , rollingInterval: RollingInterval.Day, outputTemplate:
        "{Timestamp:HH:mm:ss} :: [{Level:u3}] >> {Message:lj}{NewLine}{Exception}")
            .CreateLogger();
        Log.Logger = log;

        try
        {
            BuildAvaloniaApp().StartWithClassicDesktopLifetime(args);
        }
        catch (Exception ex)
        {
            File.WriteAllText(Path.Combine(AppContext.BaseDirectory, "bw-launcher-crash-log.txt"), DateTime.UtcNow + " - " + ex.ToString());
            throw;
        }
    }

    public static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .WithInterFont()
            .LogToTrace();
}
