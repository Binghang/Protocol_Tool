using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
namespace _61850_Client_v1._0a
{
  
    public static class atopCygwin
    {
        public static Dictionary<string, string> Command_Folder = new Dictionary<string, string>();
        public static string Send(string Command, string Shell_Command)
        {
            Process Carried_Out = new Process();
            ProcessStartInfo Carried_Out_Info = new System.Diagnostics.ProcessStartInfo(@"C:\cygwin64\bin\bash.exe");
            Carried_Out_Info.Arguments = Command;
            string Carried_Folder = Command_Folder[Shell_Command];
            Carried_Out_Info.WorkingDirectory = @"C:\cygwin64\opt\xelas\iec61850\client\" + Carried_Folder;
            Carried_Out_Info.RedirectStandardOutput = true;
            Carried_Out_Info.RedirectStandardError = true;
            Carried_Out_Info.UseShellExecute = false;
            Carried_Out.StartInfo = Carried_Out_Info;
            //執行程式
            Carried_Out.Start();
            string OutputData = Carried_Out.StandardOutput.ReadToEnd();
            //Console.WriteLine(OutputData);
            Carried_Out.WaitForExit();
            return OutputData;
        }

        public static void CreateCommandFolderMapping()
        {
            Command_Folder.Add("start_server.sh", "scripts");
            Command_Folder.Add("start_client.sh", "scripts");
            Command_Folder.Add("del_server.sh", "sql");
            Command_Folder.Add("del_client.sh", "sql");
            Command_Folder.Add("import.sh", "sql");
            Command_Folder.Add("importsim.sh", "sql");
            Command_Folder.Add("connect.sh", "sql");
            Command_Folder.Add("show.sh", "sql");
            Command_Folder.Add("show2.sh", "sql");
            Command_Folder.Add("submit.sh", "sql");
            Command_Folder.Add("update.sh", "sql");
        }
    }
}
