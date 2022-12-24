using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DmailApp.Data.Entities.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Adress { get; set; } = null!;
        public string Password { get; set; } = null!;
        public User(string adress, string password) 
        {
            Adress = adress;
            Password = password;
        }
        //1 to many with mail (user can send multiple mails)
        public ICollection<Mail> SentMails { get; } = new List<Mail>();

    }
}
