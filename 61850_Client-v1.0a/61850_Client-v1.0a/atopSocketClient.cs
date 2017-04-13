using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
namespace _61850_Client_v1._0a
{
    public class atopSocketClient
    {
        private Socket _ClientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        private byte[] _recieveBuffer = new byte[1024];

        public void Connect(string IP, int Port)
        {
            try
            {
                _ClientSocket.Connect(new IPEndPoint(IPAddress.Parse(IP), Port));
            }
            catch (Exception exError)
            {
                atopLog.WriteLog( atopLogMode.SystemError, exError.Message);
            }
        }

        public bool IsConnected
        {
            get { return _ClientSocket.Connected; }
        }

        public string Send(string Data)
        {
            /* string to bytes */
            byte[] _data = System.Text.Encoding.Default.GetBytes(Data);
            /* 傳送資料到Backend */
            _ClientSocket.Send(_data);
            /* 取得Buffer的資料數量 */
            int bytesRec =  _ClientSocket.Receive(_recieveBuffer);
            /* 回傳的資料 */
            return Data = Encoding.ASCII.GetString(_recieveBuffer);
        }

        public void Close()
        {
            _ClientSocket.Shutdown(SocketShutdown.Both);
            _ClientSocket.Close();
        }
    }
}
