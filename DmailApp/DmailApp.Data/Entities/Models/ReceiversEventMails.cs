using DmailApp.Data.Enums;

namespace DmailApp.Data.Entities.Models
{
    public class ReceiversEventMails : ReceiversMails
    {
        public StatusEnum Status { get; set; } = StatusEnum.NoAnswer;
    }
}
