using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using System.Threading;
namespace _61850_Client_v1._0a
{
    public partial class frm61850Client : Form
    {
        private string CID_Path = @"\\10.0.0.187\Document\International Link\PG59XX\QA\eNode Database\MB-50\MBES-50EC\M61-P14-ed2-t5-server";
        private Dictionary<string, string> CID_Item = new Dictionary<string, string>();
        private atop61850Client _61850Client;
        private DateTime Curried_Time;
        public frm61850Client()
        {
            InitializeComponent();
        }
        
        private void frm61850Client_Load(object sender, EventArgs e)
        {
            timer1.Interval = 1000;
            //PrintCIDFiles();
            Backend_Socket_Information[] SI = new Backend_Socket_Information[1] {
                new Backend_Socket_Information {
                    IP_Address = "192.168.4.222",
                    Port = 505
                }
            };
            _61850Client = new atop61850Client(SI);
        }
        
        private void PrintCIDFiles()
        {
            string[] CID_Files = Directory.GetFiles(CID_Path);

            ckbCID_Files.Items.Clear();

            foreach (var item in CID_Files)
            {
                CID_Item.Add(new FileInfo(item).Name, item);
                ckbCID_Files.Items.Add(new FileInfo(item).Name);
            }
        }

        [STAThread]
        private void btnImport_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < ckbCID_Files.CheckedItems.Count; i++)
            {
                _61850Client.ImportSCL(ckbCID_Files.CheckedItems[i].ToString().Replace(".cid",""), CID_Item[ckbCID_Files.CheckedItems[i].ToString()]);
                _61850Client.StartSCLClient(ckbCID_Files.CheckedItems[i].ToString().Replace(".cid", ""));
            }

        }

        [STAThread]
        private void btnReadXML_Click(object sender, EventArgs e)
        {
            OpenFileDialog OFD = new OpenFileDialog();
            OFD.Title = "Select CID File";
            OFD.Filter = "CID file (*.*)|*.cid";
            OFD.RestoreDirectory = true;
            if (OFD.ShowDialog() == DialogResult.OK)
            {
                string File_Path = OFD.FileName;
                txtCID_Path.Text = File_Path;
            }

            if (txtCID_Path.Text != null)
            {
                /* 讀取CID File */
                atopXML.Read(txtCID_Path.Text);
            }
            else
            {
                atopLog.WriteLog(atopLogMode.SystemError, "CID File load fail!");
            }
        }

        private void btnStartTest_Click(object sender, EventArgs e)
        {
            /* Wireshark catch packetge*/
            atopWireshark wireShark = new atopWireshark();
            Thread WS = new Thread(wireShark.Setup);
            WS.IsBackground = true;
            WS.Start();
            /* Run Time */
            Curried_Time = DateTime.Now;
            timer1.Enabled = true;
            /* Start Process */
            Thread Process = new Thread(Process_Start);
            Process.Start();
        }

        private void Process_Start()
        {
            _61850Client.Start_Reliability(10, 60, 5);
        }
    

        private void timer1_Tick(object sender, EventArgs e)
        {
            TimeSpan RunTime = new TimeSpan(DateTime.Now.Ticks - Curried_Time.Ticks);
            lblRuntime.Text = $"{RunTime.Hours.ToString().PadLeft(2,'0')}:{RunTime.Minutes.ToString().PadLeft(2, '0')}:{RunTime.Seconds.ToString().PadLeft(2, '0')}";
        }
    }
}
