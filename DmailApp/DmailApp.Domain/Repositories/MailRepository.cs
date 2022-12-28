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

        public Mail ShowMailById(int idOfChosenMail)
        {
            var mailToShow = DbContext.Mails
                .Include(m => m.Sender)
                .Include(m => m.ReceiversMails)
                .Where(x => x.Id == idOfChosenMail)
                .FirstOrDefault();

            return mailToShow;
        }


        //public Mail ShowMailById(int idOfChosenMail)
        //{
        //    var mailToShow = DbContext.Mails
        //        .Include(m => m.Sender)
        //        .Where(x => x.Id == idOfChosenMail)
        //        .FirstOrDefault();

        //    return mailToShow;
        //}

    }
}
