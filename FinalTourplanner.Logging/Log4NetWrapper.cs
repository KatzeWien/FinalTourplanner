using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalTourplanner.Logging
{
    public class Log4NetWrapper : ILoggingService
    {
        private readonly ILog log;
        public Log4NetWrapper(ILog log)
        {
            this.log = log;
        }
        public static Log4NetWrapper CreateLogger(string path)
        {
            if(!File.Exists(path))
            {
                throw new ArgumentException("Does not exists", nameof(path));
            }
            log4net.Config.XmlConfigurator.Configure(new FileInfo(path));
            var logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            return new Log4NetWrapper(logger);
        }
        public void Debug(string message) => log.Debug(message);
        public void Info(string message) => log.Info(message);
        public void Fatal(string message) => log.Fatal(message);
        public void Error(string message) => log.Error(message);
        public void Warn(string message) => log.Warn(message);
    }
}
