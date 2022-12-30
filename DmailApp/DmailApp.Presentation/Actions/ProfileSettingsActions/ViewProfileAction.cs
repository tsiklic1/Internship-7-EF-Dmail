using DmailApp.Domain.Repositories;
using DmailApp.Presentation.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DmailApp.Presentation.Actions.ProfileSettingsActions
{
    public class ViewProfileAction : IAction
    {
        private readonly UserRepository _userRepository;
        private readonly ReceiversMailsRepository _receiversMailsRepository;
        private readonly MailRepository _mailRepository;
        private readonly UsersSpamsRepository _usersSpamsRepository;

        public int MenuIndex { get; set; }
        public string Name { get; set; } = "View Profile";
        public string Adress { get; set; }

        public ViewProfileAction(UserRepository userRepository, ReceiversMailsRepository receiversMailsRepository, MailRepository mailRepository, UsersSpamsRepository usersSpamsRepository, string adress)
        {
            _userRepository = userRepository;
            _receiversMailsRepository = receiversMailsRepository;
            _mailRepository = mailRepository;
            _usersSpamsRepository = usersSpamsRepository;
            Adress = adress;
        }

        public void Open()
        {
            Console.WriteLine($"You sent mails to: ");

            var loggedInId = _userRepository.GetIdByAdress(Adress);

            var receiverIdsList = _receiversMailsRepository.GetAllReceiversIdsBySenderId(loggedInId);            

            List<int> receiverIdsNoDuplicates = receiverIdsList.Distinct().ToList();

            foreach (var receiverId in receiverIdsNoDuplicates)
            {
                Console.WriteLine(_userRepository.GetById(receiverId).Adress);
            }

            Console.WriteLine($"\nYou received mails from: ");

            var senderIdsList = _receiversMailsRepository.GetAllSendersByReceiverId(loggedInId);
            List<int> senderIdsNoDuplicates = senderIdsList.Distinct().ToList();

            foreach (var item in senderIdsNoDuplicates)
            {
                Console.WriteLine(_userRepository.GetById(item).Adress);
            }

            var allIds = receiverIdsNoDuplicates.Concat(senderIdsNoDuplicates).Distinct().ToList();
            Console.WriteLine("\nCombined:");
            var index = 1;
            foreach (var id in allIds)
            {
                Console.WriteLine($"{index}. {_userRepository.GetById(id).Adress}");
                index++;
            }

            Console.WriteLine("Filter <y>");
            var filterChoice = Console.ReadLine();
            var listOfSpamIds = GetListOfSpamIds();
            var listOfNotSpamIds = new List<int>();
            foreach (var item in allIds)
            {
                if (!listOfSpamIds.Contains(item))
                {
                    listOfNotSpamIds.Add(item);
                }
            }

            if (filterChoice == "y")
            {
                Console.WriteLine("1. Show users marked as spam\n2. Show users that aren't spam\n3. Exit");
                var choiceOfFilter = Console.ReadLine();
                switch (choiceOfFilter)
                {
                    case "1":  //možda čak ovo iz ovoga casea spremit vani pa da iman spremnu listu i onda iz nje mogu označavat kao spam tj provjeravat 
                        Console.WriteLine("Spam accounts: ");
                        

                        foreach (var item in listOfSpamIds)
                        {
                            Console.WriteLine(_userRepository.GetById(item).Adress);
                        }
                        break;
                    case "2":
                        Console.WriteLine("Non-spam accounts:");
                        foreach (var item in listOfNotSpamIds)
                        {
                            Console.WriteLine(_userRepository.GetById(item).Adress);
                        }
                        break;
                    case "3":
                        Console.WriteLine("Exit");
                        break;
                    default:
                        Console.WriteLine("Incorrect option");
                        break;
                }
            }

            Console.WriteLine("Acces mail <y>");
            var accesMailChoice = Console.ReadLine();
            if (accesMailChoice == "y")
            {
                Console.WriteLine("Choose mail number: ");
                var isValidInput = int.TryParse(Console.ReadLine(), out var mailNumber);
                if (!isValidInput || mailNumber<1 || mailNumber > allIds.Count())
                {
                    Console.WriteLine("Incorrect input");
                    return;
                }
                var idOfMail = allIds[mailNumber - 1];
                if (listOfSpamIds.Contains(idOfMail))
                {
                    Console.WriteLine("This user is spam. Mark as not spam? <y>");
                    var markAsNotSpam = Console.ReadLine();
                    if (markAsNotSpam == "y")
                    {
                        //izbrisat iz baze user spam par
                    }
                }
                else if (listOfNotSpamIds.Contains(idOfMail))
                {
                    Console.WriteLine("This user is not spam. Mark as spam? <y>");
                    var markAsSpam = Console.ReadLine();
                    if (markAsSpam == "y")
                    {
                        //dodat u bazu novi user spam par
                    }
                }
            }
            
        }

        public List<int> GetListOfSpamIds()
        {
            var spamAccounts = _usersSpamsRepository.GetSpamAccounts(Adress);

            List<int> listOfSpamIds = new List<int>();
            foreach (var item in spamAccounts)
            {
                listOfSpamIds.Add(item.SpamId);
            }

            return listOfSpamIds;
        }
    }
}
