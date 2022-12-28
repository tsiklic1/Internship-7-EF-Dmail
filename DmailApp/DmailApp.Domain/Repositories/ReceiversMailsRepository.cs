﻿using DmailApp.Data;
using DmailApp.Data.Entities.Models;
using DmailApp.Data.Enums;
using DmailApp.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public StautsEnum? GetStatusByCompositeKey(int mailId, int receiverId)
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
    }
}
