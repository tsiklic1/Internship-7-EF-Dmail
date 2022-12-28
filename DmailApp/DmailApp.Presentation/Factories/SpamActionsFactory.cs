using DmailApp.Domain.Factories;
using DmailApp.Domain.Repositories;
using DmailApp.Presentation.Abstractions;
using DmailApp.Presentation.Actions.OutgoingMailActions;
using DmailApp.Presentation.Actions;
using DmailApp.Presentation.Actions.SpamActions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DmailApp.Presentation.Factories
{
    public class SpamActionsFactory
    {
        public static SpamAction Create(string adress)
        {
            var actions = new List<IAction>
            {
                new PrintReadSpamAction(RepositoryFactory.Create<UserRepository>(), RepositoryFactory.Create<ReceiversMailsRepository>(), RepositoryFactory.Create<MailRepository>(), RepositoryFactory.Create<UsersSpamsRepository>(), adress),
                new PrintUnreadSpamAction(RepositoryFactory.Create<UserRepository>(), RepositoryFactory.Create<ReceiversMailsRepository>(), RepositoryFactory.Create<MailRepository>(), adress),
                new SpamFromSenderAction(RepositoryFactory.Create<MailRepository>(), adress),
                new ExitMenuAction()

            };

            var menuAction = new SpamAction(actions);
            return menuAction;
        }
    }
}
