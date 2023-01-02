using DmailApp.Data.Entities.Models;
using DmailApp.Data.Entities.Models.Mails;
using DmailApp.Data.Enums;
using Microsoft.EntityFrameworkCore;

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
                    new TextMail("Slika", "U dokumentu se nalazi slika zgrade fakulteta")
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
                    new TextMail("Račun", "Račun od banke")
                    {
                        Id = 10,
                        DateTimeOfSending = new DateTime(2022, 1, 3, 7, 0, 0, DateTimeKind.Utc),
                        SenderId = 3,
                    },
                    new TextMail("Slike s putovanja", "Slika1, Slika2, Slika3")
                    {
                        Id = 11,
                        DateTimeOfSending = new DateTime(2022, 2, 7, 7, 0, 0, DateTimeKind.Utc),
                        SenderId = 5,
                    },
                    new TextMail("Podsjetnik", "Pripremi se za ispit")
                    {
                        Id = 12,
                        DateTimeOfSending = new DateTime(2022, 3, 11, 7, 0, 0, DateTimeKind.Utc),
                        SenderId = 5,
                    },
                    new TextMail("Spotify-most lisened", "Michael Jackson, The Weekend, ...")
                    {
                        Id = 13,
                        DateTimeOfSending = new DateTime(2022, 4, 12, 7, 0, 0, DateTimeKind.Utc),
                        SenderId = 2,
                    },
                    new TextMail("Kazna", "Dobili ste kaznu za prekoračenje brzine...")
                    {
                        Id = 14,
                        DateTimeOfSending = new DateTime(2022, 6, 11, 7, 0, 0, DateTimeKind.Utc),
                        SenderId = 5,
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
                    new EventMail("Kava")
                    {
                        Id = 15,
                        DateTimeOfSending = new DateTime(2022, 12, 12, 20, 0, 0, DateTimeKind.Utc),
                        SenderId = 3,
                        EventTime = new DateTime(2023, 11, 2, 16, 0, 0, DateTimeKind.Utc)
                    },
                    new EventMail("Večera kod kuma")
                    {
                        Id = 16,
                        DateTimeOfSending = new DateTime(2022, 11, 11, 20, 0, 0, DateTimeKind.Utc),
                        SenderId = 3,
                        EventTime = new DateTime(2023, 6, 2, 16, 0, 0, DateTimeKind.Utc)
                    },
                    new EventMail("Proslava")
                    {
                        Id = 17,
                        DateTimeOfSending = new DateTime(2022, 8, 11, 20, 0, 0, DateTimeKind.Utc),
                        SenderId = 5,
                        EventTime = new DateTime(2023, 7, 4, 16, 0, 0, DateTimeKind.Utc)
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
                    },
                    new UsersSpams()
                    {
                        UserId = 5,
                        SpamId = 4
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
                    },
                    new ReceiversMails()
                    {
                        ReceiverId = 2,
                        MailId= 10
                    },
                    new ReceiversMails()
                    {
                        ReceiverId = 3,
                        MailId= 11
                    },
                    new ReceiversMails()
                    {
                        ReceiverId = 4,
                        MailId= 12
                    },
                    new ReceiversMails()
                    {
                        ReceiverId = 5,
                        MailId= 13
                    },
                    new ReceiversMails()
                    {
                        ReceiverId = 1,
                        MailId= 14
                    },
                    
                });
            builder.Entity<ReceiversEventMails>()
                .HasData(new List<ReceiversEventMails>
                {
                    new ReceiversEventMails()
                    {
                        ReceiverId = 2,
                        MailId= 6,
                        Status = StatusEnum.NoAnswer
                    },
                    new ReceiversEventMails()
                    {
                        ReceiverId = 5,
                        MailId= 7,
                        Status = StatusEnum.NoAnswer

                    },
                    new ReceiversEventMails()
                    {
                        ReceiverId = 4,
                        MailId= 8,
                        Status = StatusEnum.Accepted
                    },
                    new ReceiversEventMails()
                    {
                        ReceiverId = 1,
                        MailId= 9,
                        Status = StatusEnum.Declined
                    },
                    new ReceiversEventMails()
                    {
                        ReceiverId = 2,
                        MailId= 9,
                        Status = StatusEnum.NoAnswer
                    },
                    new ReceiversEventMails()
                    {
                        ReceiverId = 5,
                        MailId= 9,
                        Status = StatusEnum.NoAnswer
                    },
                    new ReceiversEventMails()
                    {
                        ReceiverId = 2,
                        MailId= 15,
                    },
                    new ReceiversEventMails()
                    {
                        ReceiverId = 3,
                        MailId= 16,
                    },
                    new ReceiversEventMails()
                    {
                        ReceiverId = 4,
                        MailId= 17,
                    },
                    new ReceiversEventMails()
                    {
                        ReceiverId = 5,
                        MailId= 17,
                    },
                    new ReceiversEventMails()
                    {
                        ReceiverId = 1,
                        MailId= 17
                    },
                });
        
        }
    }
}
