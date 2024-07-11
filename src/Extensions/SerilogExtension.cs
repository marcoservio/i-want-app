using Serilog.Events;
using Serilog.Sinks.MSSqlServer;

namespace IWantApp.Extensions;

public static class SerilogExtension
{
    public static void AddSerilog(this ConfigureHostBuilder host)
    {
        host.UseSerilog((context, configuration) =>
        {
            configuration
                .WriteTo.Console()
                 .WriteTo.Logger(lc => lc
                    .Filter.ByIncludingOnly(evt =>
                        evt.Level == LogEventLevel.Warning || evt.Level == LogEventLevel.Error)
                .WriteTo.MSSqlServer(
                    context.Configuration.GetConnectionString("SqlServer"),
                        sinkOptions: new MSSqlServerSinkOptions()
                        {
                            AutoCreateSqlTable = true,
                            TableName = "LogAPI",
                        }));
        });
    }
}
