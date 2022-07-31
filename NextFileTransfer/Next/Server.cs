using NextFileTransfer.Commands;
using NextFileTransfer.EventArguments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace NextFileTransfer.Next
{
    public class Server
    {
        private Dictionary<string, ICommand> Commands { get; } = new Dictionary<string, ICommand>();

        public event EventHandler<CommandReceivedEventArgs> CommandReceived;

        private TcpListener listener = new TcpListener(IPAddress.Parse("192.168.0.10"), 9000);
        private List<Client> Clients { get; } = new List<Client>();
        private bool running = true;

        public async void Start()
        {
            Commands.Add("echo", new Echo());

            listener.Start();

            while (running)
            {
                Socket socket = await listener.AcceptSocketAsync();

                Client client = new Client() { Socket = socket };
                Clients.Add(client);

                client.CommandReceived += Client_CommandReceived;
                client.Start();
            }
        }

        public void Close(Client client)
        { 
            
        }

        private void Client_CommandReceived(object sender, CommandReceivedEventArgs e)
        {
            string commandName = null;
            Client client = sender as Client;

            if (e.Command.StartsWith("echo"))
            {
                commandName = "echo";
            }

            ICommand command = Commands[commandName];
            command.Process(client, e.Command);

            //client.Socket.Close();
            //Clients.Remove(client);

            CommandReceived?.Invoke(sender, e);
        }
    }
}
