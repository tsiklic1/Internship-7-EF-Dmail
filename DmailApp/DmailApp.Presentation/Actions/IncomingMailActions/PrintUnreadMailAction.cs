using DmailApp.Data.Entities.Models;
using DmailApp.Data.Entities.Models.Mails;
using DmailApp.Data.Enums;
using DmailApp.Domain.Models;
using DmailApp.Domain.Repositories;
using DmailApp.Presentation.Abstractions;
using DmailApp.Presentation.Actions.SendMailActions;
using DmailApp.Presentation.Enums;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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
        private readonly UsersSpamsRepository _usersSpamsRepository;

        public int MenuIndex { get; set; }
        public string Name { get; set; } = "Print Unread Mails";
        public string Adress { get; set; }

        public PrintUnreadMailAction(UserRepository userRepository, ReceiversMailsRepository receiversMailsRepository, MailRepository mailRepository, UsersSpamsRepository usersSpamsRepository, string adress)
        {
            _userRepository = userRepository;
            _receiversMailsRepository = receiversMailsRepository;
            _mailRepository = mailRepository;
            _usersSpamsRepository = usersSpamsRepository;
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

        public void DetailedViev(List<MailTitleWithSenderAdress> mails)
        {
            Console.WriteLine("Mail serial number:");
            var isValidNumber = int.TryParse(Console.ReadLine(), out var mailNum);
            if (!isValidNumber)
            {
                Console.WriteLine("Please insert number");
                return;
            }
            if (mailNum > mails.Count() || mailNum < 1)
            {
                Console.WriteLine("No mail with that id");
                return;
            }
            int idOfChosenMail = mails[mailNum - 1].Id;
            var mailToShow = _mailRepository.ShowMailById(idOfChosenMail);
            if (mailToShow == null)
            {
                Console.WriteLine("Wrong id input");
                return;
            }
            mailToShow.WasRead = true;

            if (mailToShow is TextMail)
            {
                Console.WriteLine("textMail");
                var textMailToShow = (TextMail)mailToShow;
                Console.WriteLine($"{textMailToShow.Title}\n{textMailToShow.DateTimeOfSending}\n{textMailToShow.Sender.Adress}\n{textMailToShow.Content}");
            }

            else if (mailToShow is EventMail)
            {
                string joinedAdresses = "";
                Console.WriteLine("eventMail");
                var eventMailToShow = (EventMail)mailToShow;
                Console.WriteLine($"{eventMailToShow.Title}\n{eventMailToShow.EventTime}\n{eventMailToShow.Sender.Adress}");
                foreach (var item in eventMailToShow.ReceiversMails)
                {
                    joinedAdresses += item.Receiver.Adress + " ";
                }
                Console.WriteLine(joinedAdresses);
                var idOfLoggedInUser = _userRepository.GetIdByAdress(Adress);
                var status = _receiversMailsRepository.GetStatusByCompositeKey(idOfChosenMail, idOfLoggedInUser);
                Console.WriteLine(status);
            }
            _mailRepository.Update(mailToShow, idOfChosenMail);

            //sad se ovdi triba otvorit izbornik za 4 akcije
            var detailedViewChoice = "";
            Console.WriteLine($"1. Mark as NotRead\n2. Mark as spam\n3. Delete mail\n4. Reply\n5. Exit");
            detailedViewChoice = Console.ReadLine();
            switch (detailedViewChoice)
            {
                case "1":
                    MarkAsNotRead(mailToShow, idOfChosenMail);
                    break;
                case "2":
                    MarkAsSpam(_userRepository.GetIdByAdress(Adress), mailToShow.SenderId);
                    break;
                case "3":
                    DeleteMail(mailToShow);
                    break;
                case "4":
                    Reply(mailToShow);
                    break;
                case "5":
                    Console.WriteLine("Exit");
                    break;
                default:
                    break;
            }
        }

        public void DeleteMail(Mail mailToDelete)
        {
            _mailRepository.Delete(mailToDelete.Id);
        }
        public void Filter(List<MailTitleWithSenderAdress> mails)
        {
            Console.WriteLine("1. Text mails\n2. Event mails");
            var choiceOfFilter = Console.ReadLine();
            if (choiceOfFilter == "1")
            {
                Console.WriteLine("Unread text mails:");
                foreach (var item in mails)
                {
                    var mailToShow = _mailRepository.ShowMailById(item.Id);
                    if (mailToShow is TextMail)
                    {
                        Console.WriteLine($"{item.Title} - {item.SenderAdress}");
                    }
                }
            }
            else if (choiceOfFilter == "2")
            {
                Console.WriteLine("Unread event mails:");
                foreach (var item in mails)
                {
                    var mailToShow = _mailRepository.ShowMailById(item.Id);
                    if (mailToShow is EventMail)
                    {
                        Console.WriteLine($"{item.Title} - {item.SenderAdress}");
                    }
                }
            }
            else
            {
                Console.WriteLine("Incorrect input");
            }


        }
        public void MarkAsNotRead(Mail mailToShow, int idOfChosenMail)
        {
            mailToShow.WasRead = false;
            _mailRepository.Update(mailToShow, idOfChosenMail);
        }

        public void MarkAsSpam(int userId, int spamId)
        {
            //dodat u usersspams par receiverId senderId
            if (_usersSpamsRepository.CheckIfUserSpamPairExists(userId, spamId))
            {
                Console.WriteLine("already is spam");
                return;
            }

            var newUserSpam = new UsersSpams()
            {
                UserId = userId,
                SpamId = spamId
            };
            _usersSpamsRepository.Add(newUserSpam);
        }

        public void Reply(Mail mail)
        {
            var firstFreeId = _mailRepository.GetFirstFreeId();
            var replyTitle = $"Reply to {mail.Title}";
            var replyContent = "reply";
            if (mail is TextMail)
            {
                Console.WriteLine("Content of reply:");
                replyContent = Console.ReadLine();
                if (replyContent == "")
                {
                    Console.WriteLine("Content can't be empty");
                    return;
                }

            }

            else if (mail is EventMail)
            {
                Console.WriteLine("1. Accept event\n2. Decline event");
                var eventReply = Console.ReadLine();

                if (eventReply == "1")
                {
                    replyContent = $"{Adress} accepted your invitation";
                    //receiver mail pair - mail.SenderId, mail.Id
                    _receiversMailsRepository.UpdateStatus(_userRepository.GetIdByAdress(Adress), mail.Id, StatusEnum.Accepted);
                    Console.WriteLine(_receiversMailsRepository.GetStatusByCompositeKey(mail.Id, mail.SenderId));
                }
                else if (eventReply == "2")
                {
                    replyContent = $"{Adress} declined your invitation";
                    _receiversMailsRepository.UpdateStatus(_userRepository.GetIdByAdress(Adress), mail.Id, StatusEnum.Declined);

                }
                else {
                    Console.WriteLine("Incorrect input");
                }
            }

            var replyMail = new TextMail(replyTitle, replyContent)
            {
                Id = firstFreeId,
                DateTimeOfSending = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc),
                SenderId = _userRepository.GetIdByAdress(Adress)
            };
            _mailRepository.Add(replyMail);
            var newReceiverMail = new ReceiversMails
            {
                MailId = replyMail.Id,
                ReceiverId = mail.SenderId
            };

            _receiversMailsRepository.Add(newReceiverMail);            
        }
    

        
    }
}
