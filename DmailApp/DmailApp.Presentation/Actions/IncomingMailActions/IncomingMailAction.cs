using DmailApp.Presentation.Abstractions;

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
