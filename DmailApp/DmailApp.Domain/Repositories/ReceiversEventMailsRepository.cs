using DmailApp.Data.Entities.Models;
using DmailApp.Data;
using DmailApp.Domain.Enums;

namespace DmailApp.Domain.Repositories
{
    public class ReceiversEventMailsRepository : BaseRepository
    {
        public ReceiversEventMailsRepository(DmailAppDbContext dbContext) : base(dbContext)
        {
        }

        public ResponseResultType Add(ReceiversEventMails receiverMail)
        {
            DbContext.ReceiversEventMails.Add(receiverMail);

            return SaveChanges();
        }

        public ResponseResultType Delete(int id)
        {
            var receiverMailToDelete = DbContext.ReceiversEventMails.Find(id);
            if (receiverMailToDelete is null)
            {
                return ResponseResultType.NotFound;
            }

            DbContext.ReceiversEventMails.Remove(receiverMailToDelete);

            return SaveChanges();
        }
    }
}
