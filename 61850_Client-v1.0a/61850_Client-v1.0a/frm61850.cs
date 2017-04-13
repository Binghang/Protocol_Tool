using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _61850_Client_v1._0a
{
    public partial class frm61850 : Form
    {
        public frm61850()
        {
            InitializeComponent();
        }

        private void btn61850_Client_Click(object sender, EventArgs e)
        {
            frm61850Client _61850Client = new frm61850Client();
            _61850Client.ShowDialog();

            Console.WriteLine("61850 Client");
        }

        private void btn61850_Server_Click(object sender, EventArgs e)
        {
            Console.WriteLine("61850 Server");
        }
    }
}
