using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.ComponentModel;
using System.Threading;

namespace _61850_Client_v1._0a
{
    public class RequestHandler
    {
        private int ClinetNo;
        private Socket SocketClient;

        public RequestHandler(int clientNo , Socket socketClient)
        {
            this.ClinetNo = clientNo;
            this.SocketClient = socketClient;
        }

        public void doCommunication()
        {
            BackgroundWorker bgwSocket_Client = new BackgroundWorker();
            bgwSocket_Client.DoWork += BgwSocket_Client_DoWork;
            bgwSocket_Client.RunWorkerAsync();
        }

        private void BgwSocket_Client_DoWork(object sender, DoWorkEventArgs e)
        {
            int intAcceptData;
            byte[] clientData = new byte[1024];
            while (this.SocketClient.Connected)
            {
                intAcceptData = this.SocketClient.Receive(clientData);
                if (intAcceptData > 0)
                {
                    string ClientData = Encoding.Default.GetString(clientData, 0, intAcceptData);

                }
            }
        }
    }

    public class TcpListener
    {
        Socket socket;

        public TcpListener(Socket s) { socket = s; }

        public void run()
        {
            try
            {
                int IntAcceptData;

                byte[] clientData = new byte[1024];

                while (true)
                {
                    // 程式會被 hand 在此, 等待接收來自 Client 端傳來的資料
                    IntAcceptData = socket.Receive(clientData);
                    if (IntAcceptData > 0)
                    {
                        // 因為Client端過來, 所以將Byte陣列轉成字串列印出來看看~
                        string ClientData = Encoding.Default.GetString(clientData, 0, IntAcceptData);
                        //string ReturnData = Excute(ClientData);
                        //socket.Send(System.Text.Encoding.Default.GetBytes(ReturnData + "\r\n"));
                    }

                }
            }
            catch (Exception exError)
            {
               
            }
        }
    }
    
    class atopSocketServer
    {
        public static Socket[] SocketServers;       //一般而言 Server 端都會設計成可以多人同時連線.
        public static int SocketClientIndex;        //定義一個指標用來判斷現下有哪一個空的 Socket 可以分配給 Client 端連線;


        static void SocketServerListen()
        {
            Array.Resize(ref SocketServers, 1);

            SocketServers[0] = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            // 其中 LocalIP 和 SPort 分別為 string 和 int 型態, 前者為 Server端的IP, 後者Server端的Port
           // SocketServers[0].Bind(new IPEndPoint(IPAddress.Parse(LocalIP), LocalPort));

            SocketServers[0].Listen(255);      // 進行聆聽; Listen( )為允許 Client
            Console.WriteLine("Start Listen...");
            while (true)
            {
                Socket client = SocketServers[0].Accept();
                TcpListener listener = new TcpListener(client);
                Thread thread = new Thread(new ThreadStart(listener.run));
                thread.Start();
            }
        }

        /// <summary>
        /// 等待Client連線
        /// </summary>
        static void SocketWaitAccept()
        {
            // 判斷目前是否有空的 Socket 可以提供給Client端連線
            bool FlagFinded = false;

            for (int i = 1; i < SocketServers.Length - 1; i++)
            {
                // SckSs[i] 若不為 null 表示已被實作過, 判斷是否有 Client 端連線
                if (SocketServers[i] != null)
                {
                    // 如果目前第 i 個 Socket 若沒有人連線, 便可提供給下一個 Client 進行連線
                    if (SocketServers[i].Connected == false)
                    {
                        SocketClientIndex = i;
                        FlagFinded = true;
                        break;
                    }
                }
            }

            // 如果 FlagFinded 為 false 表示目前並沒有多餘的 Socket 可供 Client 連線
            if (!FlagFinded)
            {
                // 增加 Socket 的數目以供下一個 Client 端進行連線
                SocketClientIndex = SocketServers.Length;
                Array.Resize(ref SocketServers, SocketClientIndex + 1);
            }

            // 以下兩行為多執行緒的寫法, 因為接下來 Server 端的部份要使用 Accept() 讓 Cleint 進行連線;

            // 該執行緒有需要時再產生即可, 因此定義為區域性的 Thread. 命名為 SckSAcceptTd;
            // 在 new Thread( ) 裡為要多執行緒去執行的函數. 這裡命名為 SckSAcceptProc;



            Thread SckSAcceptTd = new Thread(SocketAcceptProc);

            SckSAcceptTd.Start();  // 開始執行 SckSAcceptTd 這個執行緒

            // 這裡要點出 SckSacceptTd 這個執行緒會在 Start() 之後開始執行 SckSAcceptProc 裡的程式碼, 同時主程式的執行緒也會繼續往下執行各做各的.

            // 主程式不用等到 SckSAcceptProc 的程式碼執行完便會繼續往下執行.
        }

        static void SocketAcceptProc()
        {
            // 這裡加入 try 是因為 SckSs[0] 若被 Close 的話, SckSs[0].Accept() 會產生錯誤
            try
            {
                SocketServers[SocketClientIndex] = SocketServers[0].Accept(); // 等待Client 端連線

                // 為什麼 Accept 部份要用多執行緒, 因為 SckSs[0] 會停在這一行程式碼直到有 Client 端連上線, 並分配給 SckSs[SckCIndex] 給 Client 連線之後程式才會繼續往下, 
                // 若是將 Accept 寫在主執行緒裡, 在沒有Client連上來之前, 主程式將會被hand在這一行無法再做任何事了!

                // 能來這表示有 Client 連上線. 記錄該 Client 對應的 SckCIndex

                int Scki = SocketClientIndex;

                // 再產生另一個執行緒等待下一個 Client 連線
                SocketWaitAccept();

                long IntAcceptData;

                byte[] clientData = new byte[1024];

                while (true)
                {
                    // 程式會被 hand 在此, 等待接收來自 Client 端傳來的資料
                    IntAcceptData = SocketServers[Scki].Receive(clientData);

                    // 因為Client端過來, 所以將Byte陣列轉成字串列印出來看看~
                    string ClientData = Encoding.Default.GetString(clientData);
                    string[] Data = ClientData.Split(' ');
                    //switch (Data[0].Trim())
                    //{
                    //    case "Read":
                    //        if (UpdateVisualMapping(Data[1], Convert.ToInt16(Data[2]), Convert.ToInt16(Data[3])))
                    //        {
                    //            List<string> data = Get61850MasterValue(Data[1], Convert.ToInt16(Data[2]), Convert.ToInt16(Data[3]));
                    //            char[] returnValue = new char[20];
                    //            string ReturnValue = string.Empty;
                    //            for (int i = 0; i < data.Count(); i++)
                    //            {
                    //                if (data[i] == null || data[i] == "NULL")
                    //                {
                    //                    ReturnValue += "0 ";
                    //                }
                    //                else
                    //                {
                    //                    ReturnValue += data[i].Trim() + " ";
                    //                }
                    //            }
                    //            SocketServers[Scki].Send(System.Text.Encoding.Default.GetBytes(ReturnValue));
                    //        }
                    //        break;
                    //    case "Update":
                    //        //更新Xales的資料庫
                    //        if (UpdateXalesServer(Data[1], Convert.ToInt16(Data[2]), Convert.ToInt16(Data[3])))
                    //        {
                    //            SocketServers[Scki].Send(System.Text.Encoding.Default.GetBytes("0"));
                    //        }
                    //        else
                    //        {
                    //            SocketServers[Scki].Send(System.Text.Encoding.Default.GetBytes("1"));
                    //        }
                    //        break;
                    //}
                }
            }
            catch (Exception exError)
            {
                if (SocketServers[SocketClientIndex].Connected == false)
                {
                    SocketServers[SocketClientIndex].Close();
                }
            }
        }

    }
}
