using DmailApp.Domain.Repositories;
using DmailApp.Presentation.Abstractions;

namespace DmailApp.Presentation.Actions.IncomingMailActions
{
    public class MailFromSenderAction : IAction
    {
        private readonly MailRepository _mailRepository;

        public int MenuIndex { get; set; }
        public string Name { get; set; } = "Search by sender adress";
        public string Adress { get; set; }
        
        public MailFromSenderAction(MailRepository mailRepository, string adress)
        {
            _mailRepository = mailRepository;
            Adress = adress;
        }

        public void Open()
        {
            var index = 1;
            Console.WriteLine("Search for: ");
            var stringToSearchFor = Console.ReadLine();

            var mails = _mailRepository.SearchMailsByString(Adress, stringToSearchFor);
            foreach ( var mail in mails ) 
            {
                Console.WriteLine($"{index} - {mail.Title} - {mail.SenderAdress}");
                index++;
            }
        }
    }
}
