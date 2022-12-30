using DmailApp.Data;
using DmailApp.Data.Entities.Models.Mails;
using DmailApp.Domain.Enums;
using DmailApp.Domain.Models;
using Microsoft.EntityFrameworkCore;
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
            return SaveChanges();
        }

        public List<MailTitleWithSenderAdress> GetReadMails(string adress)
        {
            var titlesWithSenders = DbContext.Mails
                .Include(r => r.ReceiversMails)
                .ThenInclude(u => u.Receiver)
                .Where(r => r.WasRead)
                .OrderBy(r => r.DateTimeOfSending)
                .Select(r => new MailTitleWithSenderAdress
                {
                    Id = r.Id,
                    Title= r.Title,
                    SenderAdress = r.Sender.Adress,
                    Receivers = r.ReceiversMails
                            .Select(u => u.Receiver)
                            .ToList()
                })
                .Where(r => r.Receivers.Any(g => g.Adress == adress))
                .ToList();

            return titlesWithSenders;
        }

        public List<MailTitleWithSenderAdress> GetReadSpamMails(string adress, List<int> spamIds)
        {
            var mails = DbContext.Mails
                .Include(m => m.ReceiversMails)
                .ThenInclude(rm => rm.Receiver)
                .ThenInclude(r => r.UsersSpamsSpam)
                .Where(m => m.WasRead && spamIds.Contains(m.SenderId))
                .Select(m => new MailTitleWithSenderAdress
                {
                    Id = m.Id,
                    Title = m.Title,
                    SenderAdress = m.Sender.Adress,
                    Receivers = m.ReceiversMails
                            .Select(u => u.Receiver)
                            .ToList()

                })
                .Where(r => r.Receivers.Any(g => g.Adress == adress))
                .ToList();
            return mails;
        }

        public List<MailTitleWithSenderAdress> GetUnreadSpamMails(string adress, List<int> spamIds)
        {
            var mails = DbContext.Mails
                .Include(m => m.ReceiversMails)
                .ThenInclude(rm => rm.Receiver)
                .ThenInclude(r => r.UsersSpamsSpam)
                .Where(m => !m.WasRead && spamIds.Contains(m.SenderId))
                .Select(m => new MailTitleWithSenderAdress
                {
                    Id = m.Id,
                    Title = m.Title,
                    SenderAdress = m.Sender.Adress,
                    Receivers = m.ReceiversMails
                            .Select(u => u.Receiver)
                            .ToList()

                })
                .Where(r => r.Receivers.Any(g => g.Adress == adress))
                .ToList();
            return mails;
        }

        public List<MailTitleWithSenderAdress> GetUnreadMails(string adress)
        {
            var titlesWithSenders = DbContext.Mails
                .Include(r => r.ReceiversMails)
                .ThenInclude(u => u.Receiver)
                .Where(r => r.WasRead == false)
                .OrderBy(r => r.DateTimeOfSending)
                .Select(r => new MailTitleWithSenderAdress
                {
                    Id = r.Id,
                    Title = r.Title,
                    SenderAdress = r.Sender.Adress,
                    Receivers = r.ReceiversMails
                            .Select(u => u.Receiver)
                            .ToList()
                })
                .Where(r => r.Receivers.Any(g => g.Adress == adress))
                .ToList();

            return titlesWithSenders;
        }

        public List<MailTitleWithSenderAdress> SearchMailsByString(string adress, string text)
        {
            var titlesWithSenders = DbContext.Mails
                .Include(r => r.ReceiversMails)
                .ThenInclude(u => u.Receiver)
                .Where(u => u.Sender.Adress.Contains(text))
                .OrderBy(r => r.DateTimeOfSending)
                .Select(r => new MailTitleWithSenderAdress
                {
                    Id = r.Id,
                    Title = r.Title,
                    SenderAdress = r.Sender.Adress,
                    Receivers = r.ReceiversMails
                        .Select(u => u.Receiver)
                        .ToList()

                })
                .Where(r => r.Receivers.Any(g => g.Adress == adress))
                .ToList();
            return titlesWithSenders;                
        }

        public List<MailTitleWithSenderAdress> SearchSpamByString(string adress, string text, List<int> spamIds)
        {
            var titlesWithSenders = DbContext.Mails
                .Include(r => r.ReceiversMails)
                .ThenInclude(u => u.Receiver)
                .Where(u => u.Sender.Adress.Contains(text) && spamIds.Contains(u.SenderId))
                .OrderBy(r => r.DateTimeOfSending)
                .Select(r => new MailTitleWithSenderAdress
                {
                    Id = r.Id,
                    Title = r.Title,
                    SenderAdress = r.Sender.Adress,
                    Receivers = r.ReceiversMails
                        .Select(u => u.Receiver)
                        .ToList()

                })
                .Where(r => r.Receivers.Any(g => g.Adress == adress))
                .ToList();
            return titlesWithSenders;
        }

        public Mail ShowMailById(int idOfChosenMail)
        {
            var mailToShow = DbContext.Mails
                .Include(m => m.Sender)
                .Include(m => m.ReceiversMails)
                .Where(x => x.Id == idOfChosenMail)
                .FirstOrDefault();

            return mailToShow;
        }

        public List<MailTitleWithSenderAdress> GetOutgoingMails(string adress)
        {
            var titlesWithReceivers = DbContext.Mails
                .Include(m => m.ReceiversMails)
                .ThenInclude(r => r.Receiver)
                .Where(m => m.Sender.Adress == adress)
                .OrderBy(m => m.DateTimeOfSending)
                .Select(m => new MailTitleWithSenderAdress
                {
                    Id = m.Id,
                    Title = m.Title,
                    SenderAdress = m.Sender.Adress,
                    Receivers = m.ReceiversMails
                        .Select(u => u.Receiver)
                        .ToList()
                })
                .ToList();
            return titlesWithReceivers;
        }

        public int GetFirstFreeId()
        {
            var mailIds = DbContext.Mails
                .Select(m => m.Id)
                .ToList();
            return mailIds.Max() + 1;
        }

        

    }
}
