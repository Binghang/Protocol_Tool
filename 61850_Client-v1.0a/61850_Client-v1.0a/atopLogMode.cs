using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _61850_Client_v1._0a
{
    public enum atopLogMode
    {
        /// <summary>
        /// 61850 client test is fail
        /// </summary>
        TestFail,
        
        /// <summary>
        /// 60850 client test is success
        /// </summary>
        TestSuccess,

        /// <summary>
        /// Execute Xelas Script is fail
        /// </summary>
        XelasCommandError,

        /// <summary>
        /// Execute Xelas Script return message
        /// </summary>
        XelasCommandInfo,

        /// <summary>
        /// Program execute information
        /// </summary>
        SystemInformation,

        /// <summary>
        /// Program execption
        /// </summary>
        SystemError,
    }
}
