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
        private atopSocketClient[] Backend_Socket;
        /// <summary>
        /// 建立與後端的連線
        /// </summary>
        /// <param name="BackendIP">Backend Socket IP</param>
        /// <param name="BackendPort">Backend Socket Port</param>
        public atop61850Client(Backend_Socket_Information[] Socket_Info)
        {
            /* Backend 有四個那第一個 Backend 就是 Backend_Socket 的陣列第0個 */
            for (int i = 0; i < Socket_Info.Length; i++)
            {
                Array.Resize(ref Backend_Socket,1);
                Backend_Socket[i].Connect(Socket_Info[i].IP_Address, Socket_Info[i].Port);
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
                            atopLog.WriteLog(atopLogMode.XelasCommandError, $"Import Success : {ServerIED}");
                            if (base.StartSCLServer(ServerIED))
                            {
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

        /* Test Patten */
        public void StartTest(string Patten_Path)
        {

        }

        /* Start Test 1st Poll Static 2st Poll Change 3st Poll Control*/
        public void Start_Reliability(int FirstCase, int SecondCase, int ThirdCase)
        {
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

            /* Add Backend Point*/
            foreach (var item in Data_61850)
            {
                Backend_Socket[Convert.ToInt16(item.SocketPort)].Send(string.Format("Add {0} {1} {2}", item.BackendDataType, item.BackendStartAddr, item.BackendQuantity));
            }

            /* 取的當下測試開始的時間 */
            DateTime Curried_Time = DateTime.Now;

            DateTime dtPoll_Static = DateTime.Now.AddSeconds(FirstCase);
            DateTime dtPoll_Change = dtPoll_Static.AddSeconds(SecondCase);
            DateTime dtPoll_Control = dtPoll_Change.AddSeconds(ThirdCase);
            while (true)
            {
                if (DateTime.Now < dtPoll_Static)
                    Poll_Static();
                else if ((DateTime.Now > dtPoll_Static) && (DateTime.Now < dtPoll_Change))
                    Poll_Chang();
                else if ((DateTime.Now > dtPoll_Change) && (DateTime.Now < dtPoll_Control))
                    Poll_Control();
                else
                {
                    Console.WriteLine("End");
                    break;
                }
            }
        }

        public void Poll_Static()
        {
            foreach (var item in Data_61850)
            {
                /* Read Frontend Data */
                string Carried_Data = base.GetValue(item.ServerName, item.TagName, item.BackendStartAddr);
                /* Read Backend Data */
                string Backend_Data = Backend_Socket[Convert.ToInt16(item.SocketPort)].Send(string.Format("Get {0} {1} {2}", item.BackendStartAddr, item.BackendDataType, item.BackendQuantity));
                /* Compare Data */
                if (Carried_Data != Backend_Data)
                {
                    /* 測試失敗 */
                }
            }
        }

        public void Poll_Chang()
        {
            foreach (var item in Data_61850)
            {
                /* Change Backend Data */
                string Carried_Data = Backend_Socket[Convert.ToInt16(item.SocketPort)].Send(string.Format("Set {0} {1} {2}", item.BackendStartAddr, item.BackendDataType, item.BackendQuantity));
                /* Read Frontend Data */
                string Frontend_Data = base.GetValue(item.ServerName, item.TagName, item.Frontend);
                /* Compare Data */
                if (Carried_Data != Frontend_Data)
                {
                    /* 測試失敗 */
                }
            }
        }

        public void Poll_Control()
        {
            foreach (var item in Data_61850)
            {
                /* Write Frontend Data */
                if (base.SetData(item.ServerName, item.TagName, item.ServerName, "123"))
                {
                    /* Read Backend Datat */
                    string Backend_Data = Backend_Socket[Convert.ToInt16(item.SocketPort)].Send(string.Format("Get {0} {1} {2}", item.BackendStartAddr, item.BackendDataType, item.BackendQuantity));
                    /* Compare Data */
                    if ("123" != Backend_Data)
                    {
                        /* 測試失敗 */
                    }
                }
            }
        }
    }
}
