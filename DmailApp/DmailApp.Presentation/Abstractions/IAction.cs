using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DmailApp.Presentation.Abstractions
{
    public interface IAction
    {
        int MenuIndex { get; set; }
        string Name { get; set; }
        void Open();
    }
}
