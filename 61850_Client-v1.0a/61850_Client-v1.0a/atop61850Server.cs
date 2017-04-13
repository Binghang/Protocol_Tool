using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace _61850_Client_v1._0a
{
    public class atop61850Server : atop61850Base
    {
        private string CID_Path = @"\\10.0.0.187\Document\International Link\PG59XX\QA\eNode Database\MB-50\MBES-50EC\M61-P14-ed2-t5-server";

        public override bool ImportSCL(string ServerName, string Path)
        {
            return base.ImportSCL(ServerName, Path);
        }

        public override bool StartSCLClient(string ClientIED)
        {
            return base.StartSCLClient(ClientIED);
        }

        public override bool StartSCLServer(string ServerIED)
        {
            return base.StartSCLServer(ServerIED);
        }

        public override bool Associate(string ServerIED)
        {
            return base.Associate(ServerIED);
        }

        public bool Create(string ServerIED)
        {
            try
            {
                string[] FI = Directory.GetFiles(CID_Path);
                bool File_Flag = false;
                foreach (var item in FI)
                {
                    if (ServerIED.Trim().Replace("ServerIED","").PadLeft(2,'0') == new FileInfo(item).Name.Split('-').Last().Replace("server", "").Replace(".cid", "").PadLeft(2, '0'))
                    {
                        File_Flag = true;
                        if (base.ImportSCL(ServerIED, item))
                        {
                            atopLog.WriteLog(atopLogMode.XelasCommandError, $"Import Success : {ServerIED}");
                            if (base.StartSCLServer(ServerIED))
                            {
                                /* 這步驟需要確認 */
                                if (base.StartSCLClient(ServerIED))
                                {

                                }

                                atopLog.WriteLog(atopLogMode.XelasCommandInfo, $"Start Success : {ServerIED}");
                                if (base.Associate(ServerIED))
                                    atopLog.WriteLog(atopLogMode.XelasCommandError, $"Associate Success : {ServerIED}");
                                else
                                {
                                    atopLog.WriteLog(atopLogMode.XelasCommandError, $"Associate Fail : {ServerIED}");
                                    return false;
                                }
                            }
                            else
                            {
                                atopLog.WriteLog(atopLogMode.XelasCommandError, $"Start Fail : {ServerIED}");
                                return false;
                            }
                        }
                        else
                        {
                            atopLog.WriteLog(atopLogMode.XelasCommandError, $"Import Fail : {ServerIED}");
                            return false;
                        }
                    }
                }
                if (!File_Flag)
                {
                    atopLog.WriteLog(atopLogMode.XelasCommandError, $"Can not find file : {ServerIED}");
                    return false;
                }
                return true;
            }
            catch (Exception exError)
            {
                atopLog.WriteLog(atopLogMode.SystemError, exError.Message);
                return false;
            }
        }
        
      

        public bool UpdateValue()
        {


            return false;
        }

        private bool createDynFile(string ServerName, string TagName, string DataType, string Value)
        {
            string path = string.Format(@"C:\cygwin64\opt\xelas\iec61850\client\LD\{0}\{1}.txt.dyn", ServerName, ServerName);
            using (StreamWriter sw = new StreamWriter(path))
            {
                sw.Write(string.Format("{0},{1},{2} \r\n",TagName, DataType, Value));
                atopLog.WriteLog(atopLogMode.XelasCommandInfo, string.Format("Create dyn File : {0},{1},{2} \r\n", TagName, DataType, Value));
            }
            return true;

        }

        private string hexTodec(string hex)
        {
            string[] Hex = hex.Split(',');
            string Carry_Value = Hex[0].Replace('H', ' ').Trim();
            if(Carry_Value == "00")
                return "0";
            else
                return stringTobytearray(Carry_Value).ToString();
        }

        private string decTohex(string dec)
        {

            return string.Empty;
        }

        private int stringTobytearray(string hex)
        {
            hex = hex.ToLower().Replace("0x", "");
            int NumberChars = hex.Length;
            byte[] bytes = new byte[NumberChars / 2];
            for (int i = 0; i < NumberChars; i++)
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);

            string data = Convert.ToString(bytes[0], 2).PadLeft(8, '0').Remove(2, 6);
            return Convert.ToInt32(data, 2);
        }

    }
}
