using NextFileTransfer.Next;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NextFileTransfer
{
    public partial class MainForm : Form
    {
        private static Server server = new Server();

        public MainForm()
        {
            InitializeComponent();

            server.CommandReceived += Server_CommandReceived;
            server.Start();
        }

        private void Server_CommandReceived(object sender, EventArguments.CommandReceivedEventArgs e)
        {
            //Sender will be Client that received command.

            output.Text += e.Command + "\r\n";
        }
    }
}
