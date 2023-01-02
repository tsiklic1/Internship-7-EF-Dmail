using DmailApp.Data;
using DmailApp.Data.Entities.Models.Mails;
using DmailApp.Domain.Enums;

namespace DmailApp.Domain.Repositories
{
    public class EventMailRepository : BaseRepository
    {
        public EventMailRepository(DmailAppDbContext dbContext) : base(dbContext)
        {
        }

        public ResponseResultType Add(EventMail eventMail)
        {
            DbContext.EventMails.Add(eventMail);

            return SaveChanges();
        }

        public ResponseResultType Delete(int id)
        {
            var eventMailToDelete = DbContext.EventMails.Find(id);
            if (eventMailToDelete is null)
            {
                return ResponseResultType.NotFound;
            }

            DbContext.EventMails.Remove(eventMailToDelete);

            return SaveChanges();
        }

        public ResponseResultType Update(EventMail eventMail, int id)
        {
            var eventMailToUpdate = DbContext.EventMails.Find(id);
            if (eventMailToUpdate is null)
            {
                return ResponseResultType.NotFound;
            }

            eventMailToUpdate.Title = eventMail.Title;
            eventMailToUpdate.DateTimeOfSending = eventMail.DateTimeOfSending;
            eventMailToUpdate.WasRead = eventMail.WasRead;
            eventMailToUpdate.SenderId = eventMail.SenderId;
            eventMailToUpdate.EventTime = eventMail.EventTime;
            return SaveChanges();
        }
    }
}
