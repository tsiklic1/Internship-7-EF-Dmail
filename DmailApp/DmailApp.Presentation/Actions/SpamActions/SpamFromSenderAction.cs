using DmailApp.Domain.Repositories;
using DmailApp.Presentation.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DmailApp.Presentation.Actions.SpamActions
{
    public class SpamFromSenderAction : IAction
    {
        private readonly MailRepository _mailRepository;
        private readonly UsersSpamsRepository _usersSpamsRepository;


        public int MenuIndex { get; set; }
        public string Name { get; set; } = "Search spam by sender adress";
        public string Adress { get; set; }

        public SpamFromSenderAction(MailRepository mailRepository, UsersSpamsRepository usersSpamsRepository, string adress)
        {
            _mailRepository = mailRepository;
            _usersSpamsRepository= usersSpamsRepository;
            Adress = adress;
        }

        public void Open()
        {
            var listOfSpamUsers = _usersSpamsRepository.GetSpamAccounts(Adress);
            var listOfSpamIds = new List<int>();
            foreach (var item in listOfSpamUsers)
            {
                listOfSpamIds.Add(item.SpamId);
            }

            var index = 1;
            Console.WriteLine("Search for: ");
            string stringToSearchFor = Console.ReadLine();

            var mails = _mailRepository.SearchSpamByString(Adress, stringToSearchFor, listOfSpamIds);
            foreach (var mail in mails)
            {
                Console.WriteLine($"{index} - {mail.Title} - {mail.SenderAdress}");
                index++;
            }
        }
    }
}
