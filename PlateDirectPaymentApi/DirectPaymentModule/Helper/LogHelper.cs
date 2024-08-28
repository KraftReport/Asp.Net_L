
using Serilog;

namespace PlateDirectPaymentApi.DirectPaymentModule.Helper
{
    public class LogHelper
    {
        public LogHelper()
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo
                .File("DirectPaymentModule/Helper/log.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();
        }

        public void Info(string message)
        {
            Log.Information(message);
        }
    }
}
