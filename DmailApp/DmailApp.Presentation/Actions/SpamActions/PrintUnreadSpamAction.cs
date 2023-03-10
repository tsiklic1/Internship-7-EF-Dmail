using DmailApp.Data.Entities.Models.Mails;
using DmailApp.Data.Entities.Models;
using DmailApp.Data.Enums;
using DmailApp.Domain.Models;
using DmailApp.Domain.Repositories;
using DmailApp.Presentation.Abstractions;
using DmailApp.Presentation.Actions.IncomingMailActions;
using DmailApp.Presentation.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DmailApp.Presentation.Actions.SpamActions
{
    public class PrintUnreadSpamAction : PrintAction, IAction
    {
        public string Name { get; set; } = "Print Unread Spam";
        
        public PrintUnreadSpamAction(UserRepository userRepository, ReceiversMailsRepository receiversMailsRepository, MailRepository mailRepository, UsersSpamsRepository usersSpamsRepository, string adress) :
            base(userRepository, receiversMailsRepository, mailRepository, usersSpamsRepository, adress)
        { }

        public override void Open()
        {
            var listOfSpamUsers = _usersSpamsRepository.GetSpamAccounts(Adress);

            var listOfSpamIds = new List<int>();
            foreach (var item in listOfSpamUsers)
            {
                listOfSpamIds.Add(item.SpamId);
            }

            var mails = _mailRepository.GetUnreadSpamMails(Adress, listOfSpamIds);
            var index = 1;
            Console.WriteLine("Not read spam mails: ");
            foreach (var mail in mails)
            {
                Console.WriteLine($"{index} - {mail.Title} - {mail.SenderAdress}");
                index++;
            }

            if (mails.Count() == 0)
            {
                Console.WriteLine("No unread spam mail");
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
