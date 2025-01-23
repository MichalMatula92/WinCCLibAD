using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serilog;

namespace WinCC.AD.gRPCClient
{
    public sealed class Logger
    {
        private static readonly Logger _instance = new Logger();
        private ILogger _logger;

        public Logger()
        {
            _logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.File(Config.LogFilePath
                    , outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff} [{Level:u3}] {Message}{NewLine}{Exception}"
                    , fileSizeLimitBytes: 5242880
                    , rollOnFileSizeLimit: true
                    , retainedFileCountLimit: 10
                    //,rollingInterval: RollingInterval.Day
                    )
                .CreateLogger();
        }

        public static ILogger GetInstance()
        {
            return _instance._logger;
        }
    }
}
