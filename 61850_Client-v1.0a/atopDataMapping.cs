using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _61850_Client_v1._0a
{


    public static class atopDataMapping
    {
        private static bool _isLoad = false;

        public static List<string> Data = new List<string>();
        public static List<DataStruct61850> VisualMapping = new List<DataStruct61850>();
        public static bool IsLoad
        {
            get { return _isLoad; }
            set { _isLoad = value; }
        }

        public static bool Load_Config(string Path)
        {
            try
            {
                string[] lines = System.IO.File.ReadAllLines(Path);

                foreach (string line in lines)
                {
                    Data.Add(line);
                }
                _isLoad = true;
                return true;
            }
            catch (Exception exError)
            {
                atopLog.WriteLog(atopLogMode.SystemError, exError.Message);
                return false;
            }
        }
    }
}
