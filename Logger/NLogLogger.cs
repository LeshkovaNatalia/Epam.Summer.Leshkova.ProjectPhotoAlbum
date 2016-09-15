using System;
using NLog;

namespace Logger
{
    public class NLogLogger : ILogger
    {
        #region Fields
        private static NLog.Logger _log;
        #endregion

        #region Ctors
        public NLogLogger()
        {
            _log = LogManager.GetCurrentClassLogger();
        }
        #endregion

        #region Public Methods

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

        public void Info(object msg)
        {
            _log.Info(msg);
        }

        public string GetMessage(string msg, object obj)
        {
            return string.Format("{0} {1} {2}", DateTime.Now.Date.ToShortDateString(), obj.GetType(), msg);
        }

        #endregion
    }
}
