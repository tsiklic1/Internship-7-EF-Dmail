using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DmailApp.Data.Entities.Models
{
    public class EventMail : Mail
    {
        public DateTime EventTime { get; set; }
        public EventMail (string title, DateTime eventTime) : base(title) 
        {
            EventTime= eventTime;
        }
    }
}
