using Serilog;

namespace SerializeDeserializeDemo.Helper
{
    public class LogService
    {
        public LogService()
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo
                .File("Helper/logs/log.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();
        }

        public void Info(string message)
        {
            Log.Information(message);
        }
    }
}
