using NextFileTransfer.Next;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextFileTransfer.Commands
{
    public interface ICommand
    {
        string Process(Client client, string commandText);
    }
}
