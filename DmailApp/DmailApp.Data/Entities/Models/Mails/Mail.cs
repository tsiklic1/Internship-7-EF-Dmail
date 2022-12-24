using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DmailApp.Data.Entities.Models.Mails
{
    public class Mail
    {
        public int Id { get; set; }

        public string Title { get; set; } = "Title";
        public DateTime DateTimeOfSending { get; set; }
        public bool WasRead { get; set; } = false;

        //1 to many with user
        public int SenderId { get; set; }
        public User? Sender { get; set; }

        public Mail(string title)
        {
            Title = title;
        }
        //many to many with user (receiver)
        public ICollection<ReceiversMails> ReceiversMails { get; set; } = new List<ReceiversMails>();


    }
}
