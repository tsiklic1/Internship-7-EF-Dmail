using DmailApp.Data;
using DmailApp.Data.Entities.Models;
using DmailApp.Data.Enums;
using DmailApp.Domain.Enums;

namespace DmailApp.Domain.Repositories
{
    public class ReceiversMailsRepository : BaseRepository
    {
        public ReceiversMailsRepository(DmailAppDbContext dbContext) : base(dbContext)
        {
        }

        public ResponseResultType Add(ReceiversMails receiverMail)
        {
            DbContext.ReceiversMails.Add(receiverMail);

            return SaveChanges();
        }

        public ResponseResultType Delete(int id)
        {
            var receiverMailToDelete = DbContext.ReceiversMails.Find(id);
            if (receiverMailToDelete is null)
            {
                return ResponseResultType.NotFound;
            }

            DbContext.ReceiversMails.Remove(receiverMailToDelete);

            return SaveChanges();
        }
        
        public StatusEnum? GetStatusByCompositeKey(int mailId, int receiverId)
        {
            var receiverMail = DbContext.ReceiversMails
                .Where(x => x.MailId == mailId && x.ReceiverId == receiverId)
                .FirstOrDefault();

            if (!(receiverMail is ReceiversEventMails))
            {
                return null;
            }

            var receiverEventMail = (ReceiversEventMails)receiverMail;
            var status = receiverEventMail.Status;
            return status;
        }

        public List<int> GetAllReceiversIdsBySenderId(int senderId)
        {
            var receiversIds = DbContext.ReceiversMails
                .Where(r => r.SentMail.SenderId == senderId)
                .Select(r => r.ReceiverId)
                .ToList();

            return receiversIds;
        }
        
        public List<int> GetAllSendersByReceiverId(int receiverId)
        {
            var senderIds = DbContext.ReceiversMails
                .Where(r => r.ReceiverId == receiverId)
                .Select(r => r.SentMail.SenderId)
                .ToList();

            return senderIds;
        }

        public ResponseResultType UpdateStatus(int receiverId, int mailId, StatusEnum status)
        {
            var receiverMail = DbContext.ReceiversEventMails
                .Where(m => m.MailId == mailId && m.ReceiverId == receiverId)
                .FirstOrDefault();

            if (receiverMail is null)
            {
                Console.WriteLine("receiverMailIsNull");
                return ResponseResultType.NotFound;
            }

            receiverMail.Status = status;
            return SaveChanges();
        }

    }
}
