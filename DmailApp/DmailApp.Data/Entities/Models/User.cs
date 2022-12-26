using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DmailApp.Data.Entities.Models.Mails;

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
        //1 to many with mails (user can send multiple mails)
        public ICollection<Mail> SentMails { get; } = new List<Mail>();

        //many to many with mails (user can receive multiple mails and a mail can be sent to multiple users)
        public ICollection<ReceiversMails> ReceiversMails { get; set; } = new List<ReceiversMails>();

        public ICollection<UsersSpams> UsersSpamsUser { get; set; } = new List<UsersSpams>();
        public ICollection<UsersSpams> UsersSpamsSpam { get; set; } = new List<UsersSpams>();





    }
}
