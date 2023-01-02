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
        public ICollection<Mail> SentMails { get; } = new List<Mail>();
        public ICollection<ReceiversMails> ReceiversMails { get; set; } = new List<ReceiversMails>();

        public ICollection<UsersSpams> UsersSpamsUser { get; set; } = new List<UsersSpams>();
        public ICollection<UsersSpams> UsersSpamsSpam { get; set; } = new List<UsersSpams>();





    }
}
