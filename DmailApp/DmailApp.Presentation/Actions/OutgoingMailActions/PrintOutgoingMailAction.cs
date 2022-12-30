using DmailApp.Data.Entities.Models.Mails;
using DmailApp.Domain.Models;
using DmailApp.Domain.Repositories;
using DmailApp.Presentation.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DmailApp.Presentation.Actions.OutgoingMailActions
{
    public class PrintOutgoingMailAction : IAction
    {
        private readonly UserRepository _userRepository;
        private readonly ReceiversMailsRepository _receiversMailsRepository;
        private readonly MailRepository _mailRepository;

        public int MenuIndex { get; set; }
        public string Name { get; set; } = "Print Outgoing Mails";
        public string Adress { get; set; }

        public PrintOutgoingMailAction(UserRepository userRepository, ReceiversMailsRepository receiversMailsRepository, MailRepository mailRepository, string adress)
        {
            _userRepository = userRepository;
            _receiversMailsRepository = receiversMailsRepository;
            _mailRepository = mailRepository;
            Adress = adress;
        }

        public void Open()
        {
            var mails = _mailRepository.GetOutgoingMails(Adress);
            var index = 1;
            foreach (var mail in mails)
            {                
                foreach (var item in mail.Receivers)
                {
                    Console.WriteLine($"{index} - {mail.Title} - {item.Adress}");
                    
                }
                index++;
            }
            Console.WriteLine("See detailed view of mail? <y>");
            string seeDetailedView = Console.ReadLine();
            if (!(seeDetailedView == "y")) 
            {
                Console.WriteLine("Exit");
                return;
            }
            DetailedViev(mails);

            //funkcionalnost brisanja maila
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
            Console.WriteLine("Delete this mail? <y>");
            var deleteChoice = Console.ReadLine();
            if (deleteChoice == "y") 
            {
                DeleteMail(mailToShow);
            }

        }

        public void DeleteMail(Mail mailToDelete)
        {
            _mailRepository.Delete(mailToDelete.Id);
        }
    }
}
