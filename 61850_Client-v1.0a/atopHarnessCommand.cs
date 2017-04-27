using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _61850_Client_v1._0a
{
    public static class atopHarnessCommand
    {
        public static Dictionary<string, string> dicCommand = new Dictionary<string, string>();


        public static void Create()
        {
            dicCommand.Add("Coil", "smbcoil");
            dicCommand.Add("Disc", "smbdinput");
            dicCommand.Add("HReg", "smbhreg");
            dicCommand.Add("IReg", "smbireg");
            dicCommand.Add("WCoi", "smbcoil");
            dicCommand.Add("WHRe", "smbhreg");
        }

        public static string GetValue(string Key)
        {
            if (dicCommand.Count == 0)
            {
                Create();
            }
            return dicCommand[Key];
        }

    }
}
