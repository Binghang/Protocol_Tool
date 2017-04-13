using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _61850_Client_v1._0a
{
    public static class atop61850DataType
    {
        public static Dictionary<string, string> DataType_61850 = new Dictionary<string, string>();

        private static void CreateDictionary()
        {
            DataType_61850.Add("SP", "BOOLEAN");
            DataType_61850.Add("DP", "BITSTRING");
            DataType_61850.Add("Integer 8", "INTEGER");
            DataType_61850.Add("Integer 32", "INTEGER");
            DataType_61850.Add("Unsigned 8", "INTEGER");
            DataType_61850.Add("Unsigned 16", "UNSIGNED");
            DataType_61850.Add("Unsigned 32", "UNSIGNED");
            DataType_61850.Add("FT", "FLOAT");
        }

        public static string GetDataType(string Type)
        {
            return DataType_61850[Type];
        }
    }
}
