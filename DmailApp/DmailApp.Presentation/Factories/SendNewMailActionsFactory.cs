using DmailApp.Domain.Factories;
using DmailApp.Domain.Repositories;
using DmailApp.Presentation.Abstractions;
using DmailApp.Presentation.Actions;
using DmailApp.Presentation.Actions.SendMailActions;

namespace DmailApp.Presentation.Factories
{
    public class SendNewMailActionsFactory
    {
        public static SendMailAction Create(string adress)
        {
            var actions = new List<IAction>
            {
                new SendMail(RepositoryFactory.Create<UserRepository>(), RepositoryFactory.Create<ReceiversMailsRepository>(), RepositoryFactory.Create<MailRepository>(), adress),
                new SendEvent(RepositoryFactory.Create<UserRepository>(), RepositoryFactory.Create<ReceiversMailsRepository>(), RepositoryFactory.Create<MailRepository>(), adress),
                new ExitMenuAction()
            };

            var menuAction = new SendMailAction(actions);
            return menuAction;
        }
    }
}
