using DmailApp.Domain.Factories;
using DmailApp.Domain.Repositories;
using DmailApp.Presentation.Abstractions;
using DmailApp.Presentation.Actions;
using DmailApp.Presentation.Actions.OutgoingMailActions;

namespace DmailApp.Presentation.Factories
{
    public class OutgoingMailActionsFactory
    {
        public static OutgoingMailAction Create(string adress)
        {
            var actions = new List<IAction>
            {
                new PrintOutgoingMailAction(RepositoryFactory.Create<UserRepository>(), RepositoryFactory.Create<ReceiversMailsRepository>(), RepositoryFactory.Create<MailRepository>(), adress),
                new ExitMenuAction()

            };

            var menuAction = new OutgoingMailAction(actions);
            return menuAction;
        }
    }
}
