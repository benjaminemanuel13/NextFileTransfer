using NextFileTransfer.Next;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextFileTransfer.Commands
{
    public class Echo : BaseCommand, ICommand
    {
        public string Process(Client client, string commandLine)
        { 
            client.SendTextData(commandLine);

            return commandLine;
        }
    }
}
