using DmailApp.Data.Entities.Models.Mails;
using DmailApp.Domain.Repositories;
using DmailApp.Presentation.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DmailApp.Presentation.Actions.SpamActions
{
    public class PrintReadSpamAction : IAction
    {
        private readonly UserRepository _userRepository;
        private readonly ReceiversMailsRepository _receiversMailsRepository;
        private readonly MailRepository _mailRepository;
        private readonly UsersSpamsRepository _usersSpamsRepository;

        public int MenuIndex { get; set; }
        public string Name { get; set; } = "Print Read Spam";
        public string Adress { get; set; }

        public PrintReadSpamAction(UserRepository userRepository, ReceiversMailsRepository receiversMailsRepository, MailRepository mailRepository,UsersSpamsRepository usersSpamsRepository, string adress)
        {
            _userRepository = userRepository;
            _receiversMailsRepository = receiversMailsRepository;
            _mailRepository = mailRepository;
            _usersSpamsRepository = usersSpamsRepository;
            Adress = adress;
        }

        public void Open()
        {
            //za usera dobavit sve spam usere u listu
            //onda sa ton lison gledat mailove koje su ti ljudi slali
            var listOfSpamUsers = _usersSpamsRepository.GetSpamAccounts(Adress);
            //dobia si sve id-ove spam usera za logiranog usera
            //sad triba isprintat sve mailove kojima je SenderId jednak ovin SpamId iz ove listOfSpamUsers
            var listOfSpamIds = new List<int>();
            foreach (var item in listOfSpamUsers)
            {
                listOfSpamIds.Add(item.SpamId);
            }

            var readSpamMails = _mailRepository.GetReadSpamMails(Adress, listOfSpamIds);
            var index = 1;
            Console.WriteLine("Read spam mails: ");
            foreach (var mail in readSpamMails)
            {
                Console.WriteLine($"{index} - {mail.Title} - {mail.SenderAdress}");
                index++;
            }
            //napravit da se dalje ponaša ko ulazna pošta


        }
    }
}
