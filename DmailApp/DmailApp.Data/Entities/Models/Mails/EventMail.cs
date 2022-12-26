using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DmailApp.Data.Entities.Models.Mails
{
    public class EventMail : Mail
    {
        public DateTime EventTime { get; set; }
        public EventMail(string title) : base(title)
        {
        }
    }
}
