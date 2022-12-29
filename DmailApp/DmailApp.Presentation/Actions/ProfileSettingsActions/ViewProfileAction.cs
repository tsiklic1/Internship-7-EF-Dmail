using DmailApp.Domain.Repositories;
using DmailApp.Presentation.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DmailApp.Presentation.Actions.ProfileSettingsActions
{
    public class ViewProfileAction : IAction
    {
        private readonly UserRepository _userRepository;
        private readonly ReceiversMailsRepository _receiversMailsRepository;
        private readonly MailRepository _mailRepository;

        public int MenuIndex { get; set; }
        public string Name { get; set; } = "View Profile";
        public string Adress { get; set; }

        public ViewProfileAction(UserRepository userRepository, ReceiversMailsRepository receiversMailsRepository, MailRepository mailRepository, string adress)
        {
            _userRepository = userRepository;
            _receiversMailsRepository = receiversMailsRepository;
            _mailRepository = mailRepository;
            Adress = adress;
        }

        public void Open()
        {
            Console.WriteLine($"You sent mails to: ");

            //funkcija koja dohvaća sve kojima je ovi brat sla

            var loggedInId = _userRepository.GetIdByAdress(Adress);

            var receiverIdsList = _receiversMailsRepository.GetAllReceiversIdsBySenderId(loggedInId);            

            List<int> receiverIdsNoDuplicates = receiverIdsList.Distinct().ToList();

            foreach (var receiverId in receiverIdsNoDuplicates)
            {
                Console.WriteLine(_userRepository.GetById(receiverId).Adress);
            }

            Console.WriteLine($"\nYou received mails from: ");

            //funkcija koja dohvaća sve koji su njemu slali

            var senderIdsList = _receiversMailsRepository.GetAllSendersByReceiverId(loggedInId);
            List<int> senderIdsNoDuplicates = senderIdsList.Distinct().ToList();

            foreach (var item in senderIdsNoDuplicates)
            {
                Console.WriteLine(_userRepository.GetById(item).Adress);
            }

            //dodat opciju da filtrira spam i li ne spam 

            //dodat opciju da označi kao spam ili ne spam

            Console.WriteLine("Filter <y>");
            var filterChoice = Console.ReadLine();

            if (filterChoice != "y")
            {
                return;
            }
        }
    }
}
