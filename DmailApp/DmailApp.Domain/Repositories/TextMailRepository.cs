using DmailApp.Data;
using DmailApp.Data.Entities.Models.Mails;
using DmailApp.Domain.Enums;

namespace DmailApp.Domain.Repositories
{
    public class TextMailRepository : BaseRepository
    {
        public TextMailRepository(DmailAppDbContext dbContext) : base(dbContext)
        {
        }

        public ResponseResultType Add(TextMail textMail)
        {
            DbContext.TextMails.Add(textMail);

            return SaveChanges();
        }

        public ResponseResultType Delete(int id)
        {
            var textMailToDelete = DbContext.TextMails.Find(id);
            if (textMailToDelete is null)
            {
                return ResponseResultType.NotFound;
            }

            DbContext.TextMails.Remove(textMailToDelete);

            return SaveChanges();
        }

        public ResponseResultType Update(TextMail textMail, int id)
        {
            var textMailToUpdate = DbContext.TextMails.Find(id);
            if (textMailToUpdate is null)
            {
                return ResponseResultType.NotFound;
            }

            textMailToUpdate.Title = textMail.Title;
            textMailToUpdate.DateTimeOfSending = textMail.DateTimeOfSending;
            textMailToUpdate.WasRead = textMail.WasRead;
            textMailToUpdate.SenderId = textMail.SenderId;
            textMailToUpdate.Content= textMail.Content;
            return SaveChanges();
        }

    }
}
