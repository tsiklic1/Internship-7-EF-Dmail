using DmailApp.Presentation.Abstractions;
using DmailApp.Presentation.Actions;
using DmailApp.Presentation.Extensions;

namespace DmailApp.Presentation.Factories
{
    public class MainMenuFactory
    {
        public static IList<IAction> CreateActions(string adress)
        {
            var actions = new List<IAction>
            {
                IncomingMailActionsFactory.Create(adress),
                OutgoingMailActionsFactory.Create(adress),
                SpamActionsFactory.Create(adress),
                SendNewMailActionsFactory.Create(adress),
                ProfileSettingsActionsFactory.Create(adress),
                new LogOutAction("Log out")
            };

            actions.SetActionIndexes();
            return actions;
        }
    }
}
