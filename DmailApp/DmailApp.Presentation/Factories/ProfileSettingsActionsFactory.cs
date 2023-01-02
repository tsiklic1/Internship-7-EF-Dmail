using DmailApp.Domain.Factories;
using DmailApp.Domain.Repositories;
using DmailApp.Presentation.Abstractions;
using DmailApp.Presentation.Actions;
using DmailApp.Presentation.Actions.ProfileSettingsActions;

namespace DmailApp.Presentation.Factories
{
    public class ProfileSettingsActionsFactory
    {
        public static ProfileSettingsAction Create(string adress)
        {
            var actions = new List<IAction>
            {
                new ViewProfileAction(RepositoryFactory.Create<UserRepository>(), RepositoryFactory.Create<ReceiversMailsRepository>(), RepositoryFactory.Create<MailRepository>(), RepositoryFactory.Create<UsersSpamsRepository>(), adress),
                new ExitMenuAction()
            };

            var menuAction = new ProfileSettingsAction(actions);
            return menuAction;
        }
    }
}
