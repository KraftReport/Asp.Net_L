using Serilog;

namespace TableProjectComponentServiceTestWebAPI.CustomException
{
    public class Logger
    {
        public Logger()
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo
                .File("Log/logFile.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();
        }

        public void Info(string message)
        {
            Log.Information(message);
        }
    }
}
