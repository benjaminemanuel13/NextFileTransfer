using NextFileTransfer.EventArguments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NextFileTransfer.Next
{
    public class Client
    {
        public event EventHandler<CommandReceivedEventArgs> CommandReceived;

        public Socket Socket { get; set; }

        public async void Start()
        {
            while (true)
            {
                string command = await GetCommand();

                CommandReceived?.Invoke(this, new CommandReceivedEventArgs(command));
            }
        }

        private async Task<string> GetCommand()
        {
            byte[] buffer = new byte[4];

            for (int i = 0; i < 4; i++)
            {
                int avail = Socket.Available;

                while (avail == 0) {
                    Application.DoEvents();
                    avail = Socket.Available;
                }

                byte[] temp = new byte[avail];

                Socket.Receive(temp);

                for (int j = 0; j < avail; j++)
                {
                    buffer[i + j] = temp[j];
                }

                i += (avail - 1);
            }

            string lenstr = Encoding.UTF8.GetString(buffer);
            int length = int.Parse(lenstr);

            byte[] command = new byte[length];

            for (int i = 0; i < length; i++)
            {
                int avail = Socket.Available;

                while (avail == 0) {
                    Application.DoEvents();
                    avail = Socket.Available;
                }

                byte[] temp = new byte[avail];

                Socket.Receive(temp);

                for (int j = 0; j < avail; j++)
                {
                    command[i + j] = temp[j];
                }

                i += (avail - 1);
            }

            string commandstr = Encoding.UTF8.GetString(command);

            await Task.Run(() => { });

            return commandstr;
        }

        public bool SendTextData(string data)
        {
            int length = data.Length;
            string lenstr = length.ToString("000000");

            byte[] lenbyt = Encoding.UTF8.GetBytes(lenstr);
            Socket.Send(lenbyt);

            byte[] chrbyt = Encoding.UTF8.GetBytes(data);
            Socket.Send(chrbyt);
            
            return true;
        }
    }
}
