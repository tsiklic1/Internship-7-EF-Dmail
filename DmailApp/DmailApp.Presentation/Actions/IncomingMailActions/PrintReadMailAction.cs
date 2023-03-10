using DmailApp.Domain.Repositories;
using DmailApp.Presentation.Abstractions;
using DmailApp.Presentation.Enums;

namespace DmailApp.Presentation.Actions.IncomingMailActions
{
    public class PrintReadMailAction : PrintAction, IAction
    {
        public string Name { get; set; } = "Print Read Mails";
        public PrintReadMailAction(UserRepository userRepository, ReceiversMailsRepository receiversMailsRepository, MailRepository mailRepository, UsersSpamsRepository usersSpamsRepository, string adress) : 
            base(userRepository, receiversMailsRepository, mailRepository, usersSpamsRepository, adress)
        {}

        public override void Open()
        {
            var listOfSpamUsers = _usersSpamsRepository.GetSpamAccounts(Adress);

            var listOfSpamIds = new List<int>();
            foreach (var item in listOfSpamUsers)
            {
                listOfSpamIds.Add(item.SpamId);
            }

            var mails = _mailRepository.GetReadMails(Adress, listOfSpamIds);
            var index = 1;
            foreach (var mail in mails)
            {
                Console.WriteLine($"{index} - {mail.Title} - {mail.SenderAdress}");
                index++;
            }

            if (mails.Count() == 0)
            {
                Console.WriteLine("No read incoming mail");
                return;
            }

            var choice = -1;
            while (choice != (int)IncomingMailActionEnum.Exit)
            {
                Console.WriteLine($"{(int)IncomingMailActionEnum.DetailedView}. Detailed view\n{(int)IncomingMailActionEnum.Filter}. Filter\n{(int)IncomingMailActionEnum.Exit}. Exit");
                var isValidInput = int.TryParse(Console.ReadLine(), out choice);
                if (!isValidInput)
                {
                    continue;
                }
                switch (choice)
                {
                    case (int)IncomingMailActionEnum.DetailedView:
                        DetailedViev(mails);
                        break;
                    case (int)IncomingMailActionEnum.Filter:
                        Filter(mails);
                        break;
                    case (int)IncomingMailActionEnum.Exit:
                        Console.WriteLine("Exit");
                        break;
                    default:
                        Console.WriteLine("Please select valid option");
                        break;
                }
            }

        }

    }
}
