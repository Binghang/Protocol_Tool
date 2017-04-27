using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;
namespace _61850_Client_v1._0a
{
   public  class atopWireshark
    {
        private string _pcapPath = @"D:\Test\PC_Test";

        private Process WireShark = new Process();

        public void Setup()
        {
            try
            {
                WireShark.StartInfo.FileName = @"C:\Program Files\Wireshark\tshark.exe";
                WireShark.StartInfo.Arguments = string.Format(" -i " + 1 + " -a filesize:" + 30 + " -b files:" + 3 +  " -w " + _pcapPath);

                WireShark.StartInfo.RedirectStandardOutput = true;
                WireShark.StartInfo.UseShellExecute = false;
                WireShark.OutputDataReceived += WireShark_OutputDataReceived;
                WireShark.Start();

                //StreamReader WireSharkStream = WireShark.StandardOutput;
                ////Read the standard output of the spawned process
                //string mystring = WireSharkStream.ReadLine();
                //Console.WriteLine(mystring);

                WireShark.WaitForExit();
                WireShark.Close();
            }
            catch (Exception exError)
            {
                atopLog.WriteLog(atopLogMode.SystemError, exError.Message);
            }
        }

        private void WireShark_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            
        }
    }
}
