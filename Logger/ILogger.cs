using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logger
{
    public interface ILogger
    {
        void Error(Exception ex);
        void Info(object msg);
        void Debug(string msg);
        void Error(string msg, Exception ex);

        string GetMessage(string msg, object obj);
    }
}
