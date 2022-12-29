using DmailApp.Domain.Factories;
using DmailApp.Domain.Repositories;
using DmailApp.Presentation.Abstractions;
using DmailApp.Presentation.Actions.IncomingMailActions;
using DmailApp.Presentation.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DmailApp.Presentation.Actions.ProfileSettingsActions;

namespace DmailApp.Presentation.Factories
{
    public class ProfileSettingsActionsFactory
    {
        public static ProfileSettingsAction Create(string adress)
        {
            var actions = new List<IAction>
            {
                //new PrintReadMailAction(RepositoryFactory.Create<UserRepository>(), RepositoryFactory.Create<ReceiversMailsRepository>(), RepositoryFactory.Create<MailRepository>(), adress),
                //new PrintUnreadMailAction(RepositoryFactory.Create<UserRepository>(), RepositoryFactory.Create<ReceiversMailsRepository>(), RepositoryFactory.Create<MailRepository>(), adress),
                //new MailFromSenderAction(RepositoryFactory.Create<MailRepository>(), adress),
                new ViewProfileAction(RepositoryFactory.Create<UserRepository>(), RepositoryFactory.Create<ReceiversMailsRepository>(), RepositoryFactory.Create<MailRepository>(), adress),
                new ExitMenuAction()

            };

            var menuAction = new ProfileSettingsAction(actions);
            return menuAction;
        }
    }
}
