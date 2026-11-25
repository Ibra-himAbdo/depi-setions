using Serilog;

namespace Demo.API.Logging;

public static class LoggingConfig
{
    public static void ConfigureLogging()
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Information)
            .MinimumLevel.Override("Microsoft.AspNetCore", Serilog.Events.LogEventLevel.Information)
            .MinimumLevel.Override("Microsoft.EntityFrameworkCore", Serilog.Events.LogEventLevel.Information)
            .Enrich.FromLogContext()
            .WriteTo.Console(outputTemplate: "{Timestamp:HH:mm:ss} [{Level:u3}] {Message:lj}{NewLine}{Exception}")
            .WriteTo.Logger(logger => ConfigureLevelFile(logger, Serilog.Events.LogEventLevel.Debug, "Logs/debug-.log"))
            .WriteTo.Logger(logger => ConfigureLevelFile(logger, Serilog.Events.LogEventLevel.Information, "Logs/information-.log"))
            .WriteTo.Logger(logger => ConfigureLevelFile(logger, Serilog.Events.LogEventLevel.Warning, "Logs/warning-.log"))
            .WriteTo.Logger(logger => ConfigureLevelFile(logger, Serilog.Events.LogEventLevel.Error, "Logs/error-.log"))
            .WriteTo.Logger(logger => ConfigureLevelFile(logger, Serilog.Events.LogEventLevel.Fatal, "Logs/fatal-.log"))
            .CreateLogger();
    }

    private static void ConfigureLevelFile(
        LoggerConfiguration logger,
        Serilog.Events.LogEventLevel level,
        string path)
    {
        logger
            .Filter.ByIncludingOnly(logEvent => logEvent.Level == level)
            .WriteTo.File(
                path: path,
                rollingInterval: RollingInterval.Day,
                retainedFileCountLimit: 30,
                shared: true,
                outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level:u3}] {SourceContext} {Message:lj}{NewLine}{Exception}");
    }
}
