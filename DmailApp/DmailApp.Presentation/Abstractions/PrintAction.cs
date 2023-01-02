using DmailApp.Data.Entities.Models.Mails;
using DmailApp.Data.Entities.Models;
using DmailApp.Data.Enums;
using DmailApp.Domain.Models;
using DmailApp.Domain.Repositories;

namespace DmailApp.Presentation.Abstractions
{
    public abstract class PrintAction : IAction
    {
        protected readonly UserRepository _userRepository;
        protected readonly ReceiversMailsRepository _receiversMailsRepository;
        protected readonly MailRepository _mailRepository;
        protected readonly UsersSpamsRepository _usersSpamsRepository;

        public int MenuIndex { get; set; }
        public string Name { get; set; } = "Print Read Mails";
        public string Adress { get; set; }

        public PrintAction(UserRepository userRepository, ReceiversMailsRepository receiversMailsRepository, MailRepository mailRepository, UsersSpamsRepository usersSpamsRepository, string adress)
        {
            _userRepository = userRepository;
            _receiversMailsRepository = receiversMailsRepository;
            _mailRepository = mailRepository;
            _usersSpamsRepository = usersSpamsRepository;
            Adress = adress;
        }

        public virtual void Open()
        {}

        public virtual void DetailedViev(List<MailTitleWithSenderAdress> mails)
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
                Console.WriteLine("No mail with that ordinal number");
                return;
            }
            int idOfChosenMail = mails[mailNum - 1].Id;
            var mailToShow = _mailRepository.ShowMailById(idOfChosenMail);
            if (mailToShow == null)
            {
                Console.WriteLine("Wrong input (can't access mail that was deleted)");
                return;
            }
            Console.WriteLine("Confirm? <y>");
            if (Console.ReadLine() != "y")
            {
                Console.WriteLine("Exit");
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
                    Console.WriteLine("wrong input");
                    break;
            }
        }

        public virtual bool DeleteMail(Mail mailToDelete)
        {
            Console.WriteLine("Confirm? <y>");
            if (Console.ReadLine() != "y")
            {
                Console.WriteLine("Exit");
                return false;
            }
            Console.WriteLine("Deleted");
            _mailRepository.Delete(mailToDelete.Id);
            return true;
        }

        public virtual void Filter(List<MailTitleWithSenderAdress> mails)
        {
            Console.WriteLine("1. Text mails\n2. Event mails");
            var choiceOfFilter = Console.ReadLine();
            if (choiceOfFilter == "1")
            {
                Console.WriteLine("Read text mails:");
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
                Console.WriteLine("Read event mails:");
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
        public virtual void MarkAsNotRead(Mail mailToShow, int idOfChosenMail)
        {
            Console.WriteLine("Confirm? <y>");
            if (Console.ReadLine() != "y")
            {
                Console.WriteLine("Exit");
                return;
            }
            mailToShow.WasRead = false;
            _mailRepository.Update(mailToShow, idOfChosenMail);
        }

        public virtual void MarkAsSpam(int userId, int spamId)
        {
            Console.WriteLine("Confirm? <y>");
            if (Console.ReadLine() != "y")
            {
                Console.WriteLine("Exit");
                return;
            }
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

        public virtual void Reply(Mail mail)
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
                    _receiversMailsRepository.UpdateStatus(_userRepository.GetIdByAdress(Adress), mail.Id, StatusEnum.Accepted);
                    Console.WriteLine(_receiversMailsRepository.GetStatusByCompositeKey(mail.Id, mail.SenderId));
                }
                else if (eventReply == "2")
                {
                    replyContent = $"{Adress} declined your invitation";
                    _receiversMailsRepository.UpdateStatus(_userRepository.GetIdByAdress(Adress), mail.Id, StatusEnum.Declined);

                }
                else
                {
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
