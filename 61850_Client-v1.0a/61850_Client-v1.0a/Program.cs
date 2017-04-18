using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;
using System.Windows.Forms;
namespace _61850_Client_v1._0a
{
    class Program
    {
        
        public static Dictionary<string, string> DataType_61850 = new Dictionary<string, string>();
        

        [STAThread]
        static void Main(string[] args)
        {
            Console.Clear();
            Console.WriteLine("Atop 61850");
            atopCygwin.CreateCommandFolderMapping();
            if (args != null && args.Length > 0)
            {
                Console.WriteLine(AppDomain.CurrentDomain.FriendlyName);

                Console.WriteLine(args[0].ToString());
            }

            frm61850 _61850 = new frm61850();
            Application.Run(_61850);
        }
        
        static void CreateDictionary()
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
    }
}
