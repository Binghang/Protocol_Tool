using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace _61850_Client_v1._0a
{
    public struct Data_Struct_61850
    {
        public string Frontend;
        public string ServerName;
        public string TagName;
        public string Backend;
        public string BackendDataType;
        public string BackendStartAddr;
        public string BackendQuantity;
        public string SocketPort;
    }

    public class atop61850Client : atop61850Base
    {
        private List<Data_Struct_61850> Data_61850 = new List<Data_Struct_61850>();
        private string CID_Path = @"\\10.0.0.187\Document\International Link\PG59XX\QA\eNode Database\MB-50\MBES-50EC\M61-P14-ed2-t5-server";
        private atopSocketClient Backend_Socket = new atopSocketClient();
        private atopSocketClient Test_Socket = new atopSocketClient();
        /// <summary>
        /// 建立與後端的連線
        /// </summary>
        /// <param name="BackendIP">Backend Socket IP</param>
        /// <param name="BackendPort">Backend Socket Port</param>
        public atop61850Client(Backend_Socket_Information[] Socket_Info)
        {
            //Backend_Socket.Connect("", 505);
            /* Backend 有四個那第一個 Backend 就是 Backend_Socket 的陣列第0個 */
            for (int i = 0; i < Socket_Info.Length; i++)
            {
                //Array.Resize(ref Backend_Socket,1);
               // Backend_Socket[i].Connect(Socket_Info[i].IP_Address, Socket_Info[i].Port);
            }
        }

        public bool Create(string ServerIED)
        {
            try
            {
                string[] FI = Directory.GetFiles(CID_Path);
                bool File_Flag = false;

                foreach (var item in FI)
                {
                    if (ServerIED.Trim().Replace("ServerIED", "").PadLeft(2, '0') == new FileInfo(item).Name.Split('-').Last().Replace("server", "").Replace(".cid", "").PadLeft(2, '0'))
                    {
                        File_Flag = true;
                        if (base.ImportSCL(ServerIED, item))
                        {
                            atopLog.WriteLog(atopLogMode.XelasCommandInfo, $"Import Success : {ServerIED}");
                            if (base.StartSCLClient(ServerIED))
                            {
                                atopLog.WriteLog(atopLogMode.XelasCommandInfo, $"Start Success : {ServerIED}");
                                if (base.Associate(ServerIED))
                                    atopLog.WriteLog(atopLogMode.XelasCommandInfo, $"Associate Success : {ServerIED}");
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

        /* Test Patten */
        public void StartTest(string Patten_Path)
        {

        }

        /* Start Test 1st Poll Static 2st Poll Change 3st Poll Control*/
        public void Start_Reliability(int FirstCase, int SecondCase, int ThirdCase)
        {
            atopCSV.Load(@"D:\61850Test.xls");
            Backend_Socket.Connect("192.168.4.82", 8000);
            Backend_Socket.Send("Setting",false);
            Backend_Socket.Send("Backend01 ETH *.*.*.* 502 192.168.4.84 1 512",false);


            /* 分析資料 */
            foreach (var item in atopDataMapping.Data)
            {
                string[] Data = item.Split(' ');
                Data_61850.Add(new Data_Struct_61850
                {
                    Frontend = Data[0],
                    ServerName = Data[1],
                    TagName = Data[2],
                    Backend = Data[3],
                    BackendDataType = Data[4],
                    BackendStartAddr = Data[5],
                    BackendQuantity = Data[6],
                    SocketPort = Data[7],
                });
            }
            Backend_Socket.Send("Mapping",false);
            /* Add Backend Point*/
            foreach (var item in atopDataMapping.VisualMapping)
            {
                Backend_Socket.Send(string.Format("61850-01-01 {0} {1} {2} Modbus-01 {3} {4} {5} 512", item.IEC61850_SERVERNAME, item.IEC61850_TAGNAME, item.IEC61850_DATATYPE,item.FUNCTION,item.ADDRESS, item.RANGE),false);
            }

            if (Backend_Socket.Send("END",true).Contains("Harness is ready"))
            {
                Test_Socket.Connect("192.168.4.82", 512);
            } 
            /* 取的當下測試開始的時間 */
            DateTime Curried_Time = DateTime.Now;

            DateTime dtPoll_Static = DateTime.Now.AddSeconds(FirstCase);
            DateTime dtPoll_Change = dtPoll_Static.AddSeconds(SecondCase);
            DateTime dtPoll_Control = dtPoll_Change.AddSeconds(ThirdCase);

            bool poll_static = false;
            bool poll_change = false;
            bool poll_control = false;

            while (true)
            {
                if (DateTime.Now < dtPoll_Static)
                {
                    if (!poll_static)
                    {
                        poll_static = true;
                        atopLog.WriteLog(atopLogMode.SystemInformation, "Poll Static Start");
                    }

                }
                else if ((DateTime.Now > dtPoll_Static) && (DateTime.Now < dtPoll_Change))
                {
                    if (!poll_change)
                    {
                        poll_static = false;
                        poll_change = true;
                        atopLog.WriteLog(atopLogMode.SystemInformation, "Poll Change Start");
                    }

                }
                else if ((DateTime.Now > dtPoll_Change) && (DateTime.Now < dtPoll_Control))
                {
                    if (!poll_control)
                    {
                        poll_change = false;
                        poll_control = true;
                        atopLog.WriteLog(atopLogMode.SystemInformation, "Poll Control Start");
                    }
                }
                else
                {
                    Console.WriteLine("End");
                    break;
                }

                if (poll_static)
                    Poll_Static();
                else if (poll_change)
                    Poll_Chang();
                else if (poll_control)
                    Poll_Control();

            }
        }

        public void Poll_Static()
        {
            foreach (var item in atopDataMapping.VisualMapping)
            {
                /* Read Frontend Data */
                string Carried_Data = base.GetValue(item.IEC61850_SERVERNAME, item.IEC61850_TAGNAME, item.IEC61850_DATATYPE);
                /* Read Backend Data */
                string Backend_Data = Test_Socket.Send(string.Format("Get {0} {1} {2}", atopHarnessCommand.GetValue( item.FUNCTION), item.ADDRESS, item.RANGE),true);
                atopLog.WriteLog(atopLogMode.XelasCommandInfo, $"Get Backend Value : {Backend_Data}");
                /* Compare Data */
                if (Carried_Data != Backend_Data)
                {
                    /* 測試失敗 */
                    atopLog.WriteLog(atopLogMode.TestFail, $"{item.IEC61850_TAGNAME} Value: {Carried_Data} , Backend Value : {Backend_Data}");
                }
                else
                {

                }
            }
        }

        public void Poll_Chang()
        {
            foreach (var item in atopDataMapping.VisualMapping)
            {
                /* Change Backend Data */
                string Carried_Data = Test_Socket.Send(string.Format("Set {0} {1} {2}", item.FUNCTION, item.ADDRESS, item.RANGE),true);
                /* Read Frontend Data */
                string Frontend_Data = base.GetValue(item.IEC61850_SERVERNAME, item.IEC61850_TAGNAME, item.IEC61850_DATATYPE);
                /* Compare Data */
                if (Carried_Data != Frontend_Data)
                {
                    /* 測試失敗 */
                    //atopLog.WriteLog(atopLogMode.TestFail, $"{item.IEC61850_TAGNAME} Value: {Carried_Data} , Backend Value : {Backend_Data}");
                }
            }
        }

        public void Poll_Control()
        {
            foreach (var item in atopDataMapping.VisualMapping)
            {
                /* Write Frontend Data */
                if (base.SetData(item.IEC61850_SERVERNAME, item.IEC61850_TAGNAME, item.IEC61850_DATATYPE, "123"))
                {
                    /* Read Backend Datat */
                    string Backend_Data = Test_Socket.Send(string.Format("Get {0} {1} {2}", item.FUNCTION, item.ADDRESS, item.RANGE),false);
                    /* Compare Data */
                    if ("123" != Backend_Data)
                    {
                        /* 測試失敗 */
                        //atopLog.WriteLog(atopLogMode.TestFail, $"{item.IEC61850_TAGNAME} Value: {Carried_Data} , Backend Value : {Backend_Data}");
                    }
                }
            }
        }
    }
}
