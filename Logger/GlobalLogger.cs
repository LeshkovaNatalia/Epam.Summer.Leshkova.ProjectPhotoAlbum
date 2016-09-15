using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logger
{
    public class GlobalLogger
    {
        #region Fields
        private static ILogger _logger = new NLogLogger();
        #endregion

        #region Ctors
        public static ILogger Logger
        {
            get { return _logger; }
            internal set { _logger = value; }
        }
        #endregion
    }
}
