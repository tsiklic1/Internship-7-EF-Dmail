using DmailApp.Data.Entities.Models;
using DmailApp.Data.Entities.Models.Mails;
using DmailApp.Data.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DmailApp.Data.Seeds
{
    public static class DatabaseSeeder
    {
        public static void Seed(ModelBuilder builder)
        {
            builder.Entity<User>()
                .HasData(new List<User>
                {
                    new User("mirko@gmail.com", "12345")
                    {
                        UserId= 1,
                    },
                    new User("domagoj@gmail.com", "abcde")
                    {
                        UserId= 2,
                    },
                    new User("marko12@dump.com", "sifra")
                    {
                        UserId= 3,
                    },
                    new User("dragana.dump@gmail.com", "dragana1234")
                    {
                        UserId= 4,
                    },
                    new User("dunja@gmail.com", "dumpdump")
                    {
                        UserId= 5,
                    }
                });

            builder.Entity<TextMail>()
                .HasData(new List<TextMail>
                {
                    new TextMail("Pozdrav", "Dobar dan.")
                    {
                        Id = 1,
                        DateTimeOfSending = new DateTime(2022, 7, 11, 7, 0, 0, DateTimeKind.Utc),
                        SenderId = 1,

                    },
                    new TextMail("Izvještaj", "Poštovani, šaljem vam izvještaj.")
                    {
                        Id = 2,
                        DateTimeOfSending = new DateTime(2022, 8, 5, 9, 0, 0, DateTimeKind.Utc),
                        SenderId = 2,

                    },
                    new TextMail("Sika", "U dokumentu se nalazi slika zgrade fakulteta")
                    {
                        Id = 3,
                        DateTimeOfSending = new DateTime(2022, 7, 11, 7, 0, 0, DateTimeKind.Utc),
                        SenderId = 3,

                    },
                    new TextMail("YouTube video", "Hej, pogledaj ovaj video.")
                    {
                        Id = 4,
                        DateTimeOfSending = new DateTime(2022, 7, 11, 7, 0, 0, DateTimeKind.Utc),
                        SenderId = 4,

                    },
                    new TextMail("Kalendar", "Kalendar: Siječanj, veljača, ožujak, travanj, ...")
                    {
                        Id = 5,
                        DateTimeOfSending = new DateTime(2022, 7, 11, 7, 0, 0, DateTimeKind.Utc),
                        SenderId = 4,

                    },

                });

            builder.Entity<EventMail>()
                .HasData(new List<EventMail>()
                {
                    new EventMail("Utakmica")
                    {
                        Id = 6,
                        DateTimeOfSending = new DateTime(2022, 11, 11, 20, 0, 0, DateTimeKind.Utc),
                        SenderId = 1,
                        EventTime = new DateTime(2023, 2, 2, 16, 0, 0, DateTimeKind.Utc)
                    },
                    new EventMail("Rodendan")
                    {
                        Id = 7,
                        DateTimeOfSending = new DateTime(2022, 4, 4, 20, 0, 0, DateTimeKind.Utc),
                        SenderId = 1,
                        EventTime = new DateTime(2022, 4, 7, 16, 0, 0, DateTimeKind.Utc),
                        WasRead= true,
                    },
                    new EventMail("Predavanje")
                    {
                        Id = 8,
                        DateTimeOfSending = new DateTime(2022, 12, 1, 8, 0, 0, DateTimeKind.Utc),
                        SenderId = 2,
                        EventTime = new DateTime(2023, 2, 2, 16, 0, 0, DateTimeKind.Utc)
                    },
                    new EventMail("Radionica")
                    {
                        Id = 9,
                        DateTimeOfSending = new DateTime(2022, 11, 11, 20, 0, 0, DateTimeKind.Utc),
                        SenderId = 4,
                        EventTime = new DateTime(2023, 7, 2, 16, 0, 0, DateTimeKind.Utc)
                    },

                });

            builder.Entity<UsersSpams>()
                .HasData(new List<UsersSpams>()
                {
                    new UsersSpams()
                    {
                        UserId = 1,
                        SpamId = 4
                    },
                    new UsersSpams()
                    {
                        UserId = 2,
                        SpamId = 1
                    },
                    new UsersSpams()
                    {
                        UserId = 2,
                        SpamId = 3
                    }
                });

            builder.Entity<ReceiversMails>()
                .HasData(new List<ReceiversMails>
                {
                    new ReceiversMails()
                    {
                        ReceiverId = 2,
                        MailId= 1
                    },
                    new ReceiversMails()
                    {
                        ReceiverId = 3,
                        MailId= 1
                    },
                    new ReceiversMails()
                    {
                        ReceiverId = 1,
                        MailId= 2
                    },
                    new ReceiversMails()
                    {
                        ReceiverId = 1,
                        MailId= 3
                    },
                    new ReceiversMails()
                    {
                        ReceiverId = 2,
                        MailId= 4
                    },
                    new ReceiversMails()
                    {
                        ReceiverId = 3,
                        MailId= 5
                    }
                });
            builder.Entity<ReceiversEventMails>()
                .HasData(new List<ReceiversEventMails>
                {
                    new ReceiversEventMails()
                    {
                        ReceiverId = 2,
                        MailId= 6,
                        Status = StautsEnum.NoAnswer
                    },
                    new ReceiversEventMails()
                    {
                        ReceiverId = 5,
                        MailId= 7,
                        Status = StautsEnum.NoAnswer

                    },
                    new ReceiversEventMails()
                    {
                        ReceiverId = 4,
                        MailId= 8,
                        Status = StautsEnum.Accepted
                    },
                    new ReceiversEventMails()
                    {
                        ReceiverId = 1,
                        MailId= 9,
                        Status = StautsEnum.Declined
                    },
                    new ReceiversEventMails()
                    {
                        ReceiverId = 2,
                        MailId= 9,
                        Status = StautsEnum.NoAnswer
                    },
                    new ReceiversEventMails()
                    {
                        ReceiverId = 5,
                        MailId= 9,
                        Status = StautsEnum.NoAnswer
                    }
                });
        
        }
    }
}
