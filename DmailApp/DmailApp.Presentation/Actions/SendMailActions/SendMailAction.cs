using DmailApp.Presentation.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DmailApp.Presentation.Actions.SendMailActions
{
    public class SendMailAction : BaseMenuAction
    {

        public SendMailAction(IList<IAction> actions) : base(actions)
        {
            Name = "Send menu";
        }
    }
}
