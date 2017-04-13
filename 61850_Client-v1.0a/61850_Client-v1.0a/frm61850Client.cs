﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace _61850_Client_v1._0a
{
    public partial class frm61850Client : Form
    {
        private string CID_Path = @"\\10.0.0.187\Document\International Link\PG59XX\QA\eNode Database\MB-50\MBES-50EC\M61-P14-ed2-t5-server";
        private Dictionary<string, string> CID_Item = new Dictionary<string, string>();
        private atop61850Client _61850Client;
        public frm61850Client()
        {
            InitializeComponent();
        }

        private void frm61850Client_Load(object sender, EventArgs e)
        {
            PrintCIDFiles();
            _61850Client = new atop61850Client("192.168.4.223", 505);
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

        private void btnImport_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < ckbCID_Files.CheckedItems.Count; i++)
            {
                _61850Client.ImportSCL(ckbCID_Files.CheckedItems[i].ToString().Replace(".cid",""), CID_Item[ckbCID_Files.CheckedItems[i].ToString()]);
            }

            _61850Client.Start_Reliability(10, 10, 10);
        }
    }
}
