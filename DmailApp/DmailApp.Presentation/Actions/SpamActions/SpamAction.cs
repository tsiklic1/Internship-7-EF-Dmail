using DmailApp.Presentation.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DmailApp.Presentation.Actions.SpamActions
{
    public class SpamAction : BaseMenuAction
    {
        public SpamAction(IList<IAction> actions) : base(actions)
        {
            Name = "Spam menu";
        }
    }
}
