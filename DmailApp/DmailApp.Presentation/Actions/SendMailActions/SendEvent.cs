using DmailApp.Data.Entities.Models;
using DmailApp.Data.Entities.Models.Mails;
using DmailApp.Data.Enums;
using DmailApp.Domain.Repositories;
using DmailApp.Presentation.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace DmailApp.Presentation.Actions.SendMailActions
{
    public class SendEvent : IAction
    {
        private readonly UserRepository _userRepository;
        private readonly ReceiversMailsRepository _receiversMailsRepository;
        private readonly MailRepository _mailRepository;

        public int MenuIndex { get; set; }
        public string Name { get; set; } = "Send event mail";
        public string Adress { get; set; }

        public SendEvent(UserRepository userRepository, ReceiversMailsRepository receiversMailsRepository, MailRepository mailRepository, string adress)
        {
            _userRepository = userRepository;
            _receiversMailsRepository = receiversMailsRepository;
            _mailRepository = mailRepository;
            Adress = adress;
        }

        public void Open()
        {
            Console.WriteLine("Adresses (separator <, >):");
            var receiversString = Console.ReadLine();
            Console.WriteLine("Title: ");
            var title = Console.ReadLine();
            if (title == "")
            {
                Console.WriteLine("Title can't be empty");
                return;
            }
            Console.WriteLine();
            DateTime dateTimeOfEvent = new DateTime();
            int hoursOfEvent;

            Console.WriteLine("Date of event (mm/dd/yyyy)");
            var inputtedDate = DateTime.TryParse(Console.ReadLine(), out dateTimeOfEvent);
            if (!inputtedDate || dateTimeOfEvent < DateTime.Now)
            {
                Console.WriteLine("Incorrect date input");
                return;
            }
            Console.WriteLine("Hour of event:");
            var inputtedHours = int.TryParse(Console.ReadLine(), out hoursOfEvent);
            if (!inputtedHours || hoursOfEvent < 0 || hoursOfEvent > 24)
            {
                Console.WriteLine("Incorrect hours input");
                return;
            }

            var dateTimeOfEventWithHours = DateTime.SpecifyKind(dateTimeOfEvent.AddHours(hoursOfEvent), DateTimeKind.Utc);

            //ovdi je sad sve dobro uneseno
            string[] adresses = receiversString.Split(", ");

            var firstFreeId = _mailRepository.GetFirstFreeId();

            var mailToSend = new EventMail(title)
            {
                Id = firstFreeId,
                DateTimeOfSending = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc),
                SenderId = _userRepository.GetIdByAdress(Adress),
                EventTime = dateTimeOfEventWithHours
            };

            _mailRepository.Add(mailToSend);

            foreach (var item in adresses)
            {
                if (_userRepository.CheckIfAdressExistsInDb(item))
                {
                    var newReceiverMail = new ReceiversEventMails
                    {
                        MailId = mailToSend.Id,
                        ReceiverId = _userRepository.GetIdByAdress(item),
                        Status = StatusEnum.NoAnswer
                    };

                    _receiversMailsRepository.Add(newReceiverMail);
                }
                else
                {
                    Console.WriteLine($"Adress {item} doesn't exist");
                }

            }
        }
    }
}
