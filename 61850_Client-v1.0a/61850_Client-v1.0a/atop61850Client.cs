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
    public class atop61850Client : atop61850Base
    {
        private string CID_Path = @"\\10.0.0.187\Document\International Link\PG59XX\QA\eNode Database\MB-50\MBES-50EC\M61-P14-ed2-t5-server";
        private atopSocketClient Backend_Socket = new atopSocketClient();
        /// <summary>
        /// 建立與後端的連線
        /// </summary>
        /// <param name="BackendIP">Backend Socket IP</param>
        /// <param name="BackendPort">Backend Socket Port</param>
        public atop61850Client(string BackendIP, int BackendPort)
        {
            // Backend_Socket.Connect(BackendIP, BackendPort);
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

        }

        public void Poll_Chang()
        {

        }

        public void Poll_Control()
        {

        }
    }
}
