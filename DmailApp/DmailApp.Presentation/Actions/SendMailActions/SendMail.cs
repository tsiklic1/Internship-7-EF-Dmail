using DmailApp.Data.Entities.Models;
using DmailApp.Data.Entities.Models.Mails;
using DmailApp.Domain.Repositories;
using DmailApp.Presentation.Abstractions;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace DmailApp.Presentation.Actions.SendMailActions
{
    public class SendMail : IAction
    {
        private readonly UserRepository _userRepository;
        private readonly ReceiversMailsRepository _receiversMailsRepository;
        private readonly MailRepository _mailRepository;

        public int MenuIndex { get; set; }
        public string Name { get; set; } = "Send text mail";
        public string Adress { get; set; }

        public SendMail(UserRepository userRepository, ReceiversMailsRepository receiversMailsRepository, MailRepository mailRepository, string adress)
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
            Console.WriteLine("Content: ");
            var content = Console.ReadLine();
            if (content == "")
            {
                Console.WriteLine("Content can't be empty");
                return;
            }

            string[] adresses = receiversString.Split(", ");

            var firstFreeId = _mailRepository.GetFirstFreeId();

            var mailToSend = new TextMail(title, content)
            {
                Id = firstFreeId,
                DateTimeOfSending = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc),
                SenderId = _userRepository.GetIdByAdress(Adress)
            };

            _mailRepository.Add(mailToSend);

            foreach (var item in adresses)
            {
                if (_userRepository.CheckIfAdressExistsInDb(item))
                {
                     var newReceiverMail = new ReceiversMails
                    {
                        MailId = mailToSend.Id,
                        ReceiverId = _userRepository.GetIdByAdress(item)
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
