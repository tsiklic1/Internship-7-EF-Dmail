using DmailApp.Data.Entities.Models;

namespace DmailApp.Domain.Models
{
    public class MailTitleWithSenderAdress
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string SenderAdress { get; set; }
        public ICollection<User> Receivers { get; set; } = new List<User>();
    }
}
