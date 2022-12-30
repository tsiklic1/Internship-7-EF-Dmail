using DmailApp.Domain.Factories;
using DmailApp.Domain.Repositories;
using DmailApp.Presentation.Abstractions;
using DmailApp.Presentation.Actions;
using DmailApp.Presentation.Actions.IncomingMailActions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DmailApp.Presentation.Factories
{
    public class IncomingMailActionsFactory
    {
        public static IncomingMailAction Create(string adress)
        {
            var actions = new List<IAction>
            {
                new PrintReadMailAction(RepositoryFactory.Create<UserRepository>(), RepositoryFactory.Create<ReceiversMailsRepository>(), RepositoryFactory.Create<MailRepository>(), adress),
                new PrintUnreadMailAction(RepositoryFactory.Create<UserRepository>(), RepositoryFactory.Create<ReceiversMailsRepository>(), RepositoryFactory.Create<MailRepository>(), RepositoryFactory.Create<UsersSpamsRepository>(), adress),
                new MailFromSenderAction(RepositoryFactory.Create<MailRepository>(), adress),
                new ExitMenuAction()

            };

            var menuAction = new IncomingMailAction(actions);
            return menuAction;
        }
    }
}
