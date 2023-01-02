using DmailApp.Domain.Factories;
using DmailApp.Domain.Repositories;
using DmailApp.Presentation.Abstractions;
using DmailApp.Presentation.Actions;
using DmailApp.Presentation.Actions.SpamActions;

namespace DmailApp.Presentation.Factories
{
    public class SpamActionsFactory
    {
        public static SpamAction Create(string adress)
        {
            var actions = new List<IAction>
            {
                new PrintReadSpamAction(RepositoryFactory.Create<UserRepository>(), RepositoryFactory.Create<ReceiversMailsRepository>(), RepositoryFactory.Create<MailRepository>(), RepositoryFactory.Create<UsersSpamsRepository>(), adress),
                new PrintUnreadSpamAction(RepositoryFactory.Create<UserRepository>(), RepositoryFactory.Create<ReceiversMailsRepository>(), RepositoryFactory.Create<MailRepository>(),RepositoryFactory.Create<UsersSpamsRepository>(), adress),
                new SpamFromSenderAction(RepositoryFactory.Create<MailRepository>(),RepositoryFactory.Create<UsersSpamsRepository>(), adress),
                new ExitMenuAction()

            };

            var menuAction = new SpamAction(actions);
            return menuAction;
        }
    }
}
