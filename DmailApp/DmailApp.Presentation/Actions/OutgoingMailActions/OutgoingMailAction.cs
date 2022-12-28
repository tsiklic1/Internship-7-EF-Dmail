using DmailApp.Presentation.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DmailApp.Presentation.Actions.OutgoingMailActions
{
    public class OutgoingMailAction : BaseMenuAction
    {
        public OutgoingMailAction(IList<IAction> actions) : base(actions)
        {
            Name = "Outgoing mails menu";
        }
    }
}
