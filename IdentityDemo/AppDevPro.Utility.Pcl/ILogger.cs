using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppDevPro.Utility.Pcl
{
    public interface ILogger
    {
        void Log(string msg);

        /// <summary>
        /// Display console debugging info. DateTime.Now, calling this. , string msg
        /// </summary>
        /// <param name="from"></param>
        /// <param name="msg"></param>
        void Log(object from, string msg);

        /// <summary>
        /// Display console debuggin information. DateTime.Now, From(generally this), stringMsg, anotheStringrMsg
        /// </summary>
        /// <param name="from"></param>
        /// <param name="msg"></param>
        /// <param name="anotherMsg"></param>
        void Log(object from, string msg, string anotherMsg);
    }
}
