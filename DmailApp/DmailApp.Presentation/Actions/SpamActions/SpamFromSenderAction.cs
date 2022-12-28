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

        public int MenuIndex { get; set; }
        public string Name { get; set; } = "Search spam by sender adress";
        public string Adress { get; set; }

        public SpamFromSenderAction(MailRepository mailRepository, string adress)
        {
            _mailRepository = mailRepository;
            Adress = adress;
        }

        public void Open()
        {

        }
    }
}
