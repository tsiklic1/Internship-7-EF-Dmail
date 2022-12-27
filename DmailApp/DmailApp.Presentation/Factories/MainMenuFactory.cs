using DmailApp.Presentation.Abstractions;
using DmailApp.Presentation.Actions;
using DmailApp.Presentation.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DmailApp.Presentation.Factories
{
    public class MainMenuFactory
    {
        public static IList<IAction> CreateActions(string adress)
        {
            var actions = new List<IAction>
            {
                IncomingMailActionsFactory.Create(adress),
                //OutgoingMailActionsFactory.Create(adress),
                //SpamMailActionsFactory.Create(),
                //SendNewMailActionsFactory.Create(),
                //SendNewEventActionsFactory.Create(),
                //ProfileSettingsActionsFactory.Create(),
                //LogOutActionsFactory.Create()
                new ExitMenuAction()
            };

            actions.SetActionIndexes();
            return actions;
        }
    }
}
