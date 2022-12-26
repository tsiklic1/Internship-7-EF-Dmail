using DmailApp.Data.Entities.Models;
using DmailApp.Data.Entities.Models.Mails;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace DmailApp.Data
{
    public class DmailAppDbContext : DbContext
    {
        public DmailAppDbContext(DbContextOptions options) : base(options) { }
        public DbSet<User> User => Set<User>();
        public DbSet<Mail> Mail => Set<Mail>();

        public DbSet<TextMail> TextMail => Set<TextMail>();

        public DbSet<EventMail> EventMail => Set<EventMail>();

        public DbSet<ReceiversMails> ReceiversMails => Set<ReceiversMails>();
        public DbSet<UsersSpams> UsersSpams => Set<UsersSpams>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ReceiversMails>()
                .HasKey(rm => new { rm.ReceiverId, rm.MailId });

            modelBuilder.Entity<ReceiversMails>()
                .HasOne(r => r.SentMail)
                .WithMany(m => m.ReceiversMails)
                .HasForeignKey(rm => rm.ReceiverId);

            modelBuilder.Entity<ReceiversMails>()
                .HasOne(m => m.Receiver)
                .WithMany(m => m.ReceiversMails)
                .HasForeignKey(rm => rm.MailId);

            modelBuilder.Entity<UsersSpams>()
                .HasKey(us => new { us.UserId, us.SpamId });

            modelBuilder.Entity<UsersSpams>()
                .HasOne(u => u.User)
                .WithMany(s => s.UsersSpamsSpam)
                .HasForeignKey(us => us.SpamId);

            modelBuilder.Entity<UsersSpams>()
                .HasOne(s => s.Spam)
                .WithMany(s => s.UsersSpamsUser)
                .HasForeignKey(us => us.UserId);

            modelBuilder.Entity<Mail>()
                .HasOne(m => m.Sender)
                .WithMany(s => s.SentMails)
                .OnDelete(DeleteBehavior.NoAction);

            base.OnModelCreating(modelBuilder);

        }
    }

    public class DmailAppDbContextFactory : IDesignTimeDbContextFactory<DmailAppDbContext>
    {
        public DmailAppDbContext CreateDbContext(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddXmlFile("App.config")
                .Build();

            config.Providers
                .First()
                .TryGet("connectionStrings:add:DmailApp:connectionString", out var connectionString);

            var options = new DbContextOptionsBuilder<DmailAppDbContext>()
                .UseNpgsql(connectionString)
                .Options;

            return new DmailAppDbContext(options);
        }
    }
}