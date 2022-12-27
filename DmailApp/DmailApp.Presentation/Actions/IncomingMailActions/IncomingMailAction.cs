using DmailApp.Presentation.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DmailApp.Presentation.Actions.IncomingMailActions
{
    public class IncomingMailAction : BaseMenuAction
    {
        public IncomingMailAction(IList<IAction> actions) : base(actions)
        {
            Name = "Incoming mails menu";
        }


    }
}
