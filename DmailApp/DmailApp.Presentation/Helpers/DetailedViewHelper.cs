using DmailApp.Data.Entities.Models.Mails;
using DmailApp.Domain.Models;
using DmailApp.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DmailApp.Presentation.Helpers
{
    public static class DetailedViewHelper
    {
        public static void DetailedView(List<MailTitleWithSenderAdress> mails, MailRepository mailRepository, UserRepository userRepository, ReceiversMailsRepository _receiversMailsRepository, string adress)
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
            var mailToShow = mailRepository.ShowMailById(idOfChosenMail);
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
                var idOfLoggedInUser = userRepository.GetIdByAdress(adress);
                var status = _receiversMailsRepository.GetStatusByCompositeKey(idOfChosenMail, idOfLoggedInUser);
                Console.WriteLine(status);
            }
            mailRepository.Update(mailToShow, idOfChosenMail);
        }
    }
}
