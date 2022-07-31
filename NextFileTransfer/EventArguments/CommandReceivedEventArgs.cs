using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextFileTransfer.EventArguments
{
    public class CommandReceivedEventArgs : EventArgs
    {
        public string Command { get; set; }

        public CommandReceivedEventArgs(string command)
        {
            Command = command;
        }
    }
}
