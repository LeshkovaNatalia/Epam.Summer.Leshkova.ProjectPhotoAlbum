using System;
using NLog;

namespace Logger
{
    public class NLogLogger : ILogger
    {
        private static NLog.Logger _log;

        public NLogLogger()
        {
            _log = LogManager.GetCurrentClassLogger();
        }
        public void Debug(string msg)
        {
            _log.Debug(msg);
        }

        public void Error(Exception ex)
        {
            _log.Error(ex);
        }

        public void Error(string msg, Exception ex)
        {
            _log.Error(msg, ex);
        }

        public string GetMessage(string msg, object obj)
        {
            return string.Format("{0} {1} {2}", DateTime.Now.Date.ToShortDateString(), obj.GetType(), msg);
        }

        public void Info(object msg)
        {
            _log.Info(msg);
        }
    }
}
