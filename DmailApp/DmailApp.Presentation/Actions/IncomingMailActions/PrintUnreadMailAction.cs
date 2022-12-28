using DmailApp.Data.Entities.Models.Mails;
using DmailApp.Domain.Models;
using DmailApp.Domain.Repositories;
using DmailApp.Presentation.Abstractions;
using DmailApp.Presentation.Enums;
using DmailApp.Presentation.Helpers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DmailApp.Presentation.Actions.IncomingMailActions
{
    public class PrintUnreadMailAction : IAction
    {
        private readonly UserRepository _userRepository;
        private readonly ReceiversMailsRepository _receiversMailsRepository;
        private readonly MailRepository _mailRepository;

        public int MenuIndex { get; set; }
        public string Name { get; set; } = "Print Unread Mails";
        public string Adress { get; set; }

        public PrintUnreadMailAction(UserRepository userRepository, ReceiversMailsRepository receiversMailsRepository, MailRepository mailRepository, string adress)
        {
            _userRepository = userRepository;
            _receiversMailsRepository = receiversMailsRepository;
            _mailRepository = mailRepository;
            Adress = adress;
        }   

        public void Open()
        {
            var mails = _mailRepository.GetUnreadMails(Adress);
            var index = 1;
            foreach (var mail in mails)
            {
                Console.WriteLine($"{index} - {mail.Title} - {mail.SenderAdress}");
                index++;
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

        public void DetailedViev(List<MailTitleWithSenderAdress> mails)
        {
            DetailedViewHelper.DetailedView(mails, _mailRepository, _userRepository, _receiversMailsRepository, Adress);
            //Console.WriteLine("Mail serial number:");
            //var isValidNumber = int.TryParse(Console.ReadLine(), out var mailNum);
            //if (!isValidNumber)
            //{
            //    Console.WriteLine("Please insert number");
            //    return;
            //}
            //if (mailNum > mails.Count() || mailNum < 1)
            //{
            //    Console.WriteLine("No mail with that id");
            //    return;
            //}
            //int idOfChosenMail = mails[mailNum - 1].Id;
            //var mailToShow = _mailRepository.ShowMailById(idOfChosenMail);
            //if (mailToShow == null)
            //{
            //    Console.WriteLine("Wrong id input");
            //    return;
            //}
            //mailToShow.WasRead = true;

            //if (mailToShow is TextMail)
            //{
            //    Console.WriteLine("textMail");
            //    var textMailToShow = (TextMail)mailToShow;
            //    Console.WriteLine($"{textMailToShow.Title}\n{textMailToShow.DateTimeOfSending}\n{textMailToShow.Sender.Adress}\n{textMailToShow.Content}");
            //}

            //else if (mailToShow is EventMail)
            //{
            //    string joinedAdresses = "";
            //    Console.WriteLine("eventMail");
            //    var eventMailToShow = (EventMail)mailToShow;
            //    Console.WriteLine($"{eventMailToShow.Title}\n{eventMailToShow.EventTime}\n{eventMailToShow.Sender.Adress}");
            //    foreach (var item in eventMailToShow.ReceiversMails)
            //    {
            //        joinedAdresses += item.Receiver.Adress + " ";
            //    }
            //    Console.WriteLine(joinedAdresses);
            //    var idOfLoggedInUser = _userRepository.GetIdByAdress(Adress);
            //    var status = _receiversMailsRepository.GetStatusByCompositeKey(idOfChosenMail, idOfLoggedInUser);
            //    Console.WriteLine(status);
            //}
            //_mailRepository.Update(mailToShow, idOfChosenMail);

            //sad se ovdi triba otvorit izbornik za 4 akcije
            var detailedViewChoice = "";
            Console.WriteLine($"1. Mark as NotRead\n2. Mark as spam\n3. Delete mail\n4. Reply\n5. Exit");
            switch (detailedViewChoice)
            {
                case "1":
                    break;
                case "2":
                    break;
                case "3":
                    break;
                case "4":
                    break;
                case "5":
                    Console.WriteLine("Exit");
                    break;
                default:
                    break;
            }



        }
    }
}
