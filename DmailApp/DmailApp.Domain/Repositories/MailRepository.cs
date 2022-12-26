using DmailApp.Data;
using DmailApp.Data.Entities.Models.Mails;
using DmailApp.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DmailApp.Domain.Repositories
{
    public class MailRepository : BaseRepository
    {
        public MailRepository(DmailAppDbContext dbContext) : base(dbContext)
        {
        }

        public ResponseResultType Add(Mail mail)
        {
            DbContext.Mails.Add(mail);

            return SaveChanges();
        }

        public ResponseResultType Delete(int id)
        {
            var mailToDelete = DbContext.Mails.Find(id);
            if (mailToDelete is null)
            {
                return ResponseResultType.NotFound;
            }

            DbContext.Mails.Remove(mailToDelete);

            return SaveChanges();
        }

        public ResponseResultType Update(Mail mail, int id)
        {
            var mailToUpdate = DbContext.Mails.Find(id);
            if (mailToUpdate is null)
            {
                return ResponseResultType.NotFound;
            }

            mailToUpdate.Title= mail.Title;
            mailToUpdate.DateTimeOfSending = mail.DateTimeOfSending;
            mailToUpdate.WasRead= mail.WasRead;
            mailToUpdate.SenderId= mail.SenderId;
            mailToUpdate.Sender = mail.Sender;

            return SaveChanges();
        }
    }
}
