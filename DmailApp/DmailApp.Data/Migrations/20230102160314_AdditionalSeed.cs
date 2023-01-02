using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DmailApp.Data.Migrations
{
    public partial class AdditionalSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Mails",
                columns: new[] { "Id", "DateTimeOfSending", "EventTime", "SenderId", "Title", "WasRead", "mail_type" },
                values: new object[,]
                {
                    { 15, new DateTime(2022, 12, 12, 20, 0, 0, 0, DateTimeKind.Utc), new DateTime(2023, 11, 2, 16, 0, 0, 0, DateTimeKind.Utc), 3, "Kava", false, "eventmail" },
                    { 16, new DateTime(2022, 11, 11, 20, 0, 0, 0, DateTimeKind.Utc), new DateTime(2023, 6, 2, 16, 0, 0, 0, DateTimeKind.Utc), 3, "Večera kod kuma", false, "eventmail" },
                    { 17, new DateTime(2022, 8, 11, 20, 0, 0, 0, DateTimeKind.Utc), new DateTime(2023, 7, 4, 16, 0, 0, 0, DateTimeKind.Utc), 5, "Proslava", false, "eventmail" }
                });

            migrationBuilder.InsertData(
                table: "Mails",
                columns: new[] { "Id", "Content", "DateTimeOfSending", "SenderId", "Title", "WasRead", "mail_type" },
                values: new object[,]
                {
                    { 10, "Račun od banke", new DateTime(2022, 1, 3, 7, 0, 0, 0, DateTimeKind.Utc), 3, "Račun", false, "textmail" },
                    { 11, "Slika1, Slika2, Slika3", new DateTime(2022, 2, 7, 7, 0, 0, 0, DateTimeKind.Utc), 5, "Slike s putovanja", false, "textmail" },
                    { 12, "Pripremi se za ispit", new DateTime(2022, 3, 11, 7, 0, 0, 0, DateTimeKind.Utc), 5, "Podsjetnik", false, "textmail" },
                    { 13, "Michael Jackson, The Weekend, ...", new DateTime(2022, 4, 12, 7, 0, 0, 0, DateTimeKind.Utc), 2, "Spotify-most lisened", false, "textmail" },
                    { 14, "Dobili ste kaznu za prekoračenje brzine...", new DateTime(2022, 6, 11, 7, 0, 0, 0, DateTimeKind.Utc), 5, "Kazna", false, "textmail" }
                });

            migrationBuilder.InsertData(
                table: "UsersSpams",
                columns: new[] { "SpamId", "UserId" },
                values: new object[] { 4, 5 });

            migrationBuilder.InsertData(
                table: "ReceiversMails",
                columns: new[] { "MailId", "ReceiverId", "Status", "mail_type" },
                values: new object[,]
                {
                    { 17, 1, 2, "eventmail" },
                    { 15, 2, 2, "eventmail" },
                    { 16, 3, 2, "eventmail" },
                    { 17, 4, 2, "eventmail" },
                    { 17, 5, 2, "eventmail" }
                });

            migrationBuilder.InsertData(
                table: "ReceiversMails",
                columns: new[] { "MailId", "ReceiverId", "mail_type" },
                values: new object[,]
                {
                    { 14, 1, "textmail" },
                    { 10, 2, "textmail" },
                    { 11, 3, "textmail" },
                    { 12, 4, "textmail" },
                    { 13, 5, "textmail" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ReceiversMails",
                keyColumns: new[] { "MailId", "ReceiverId" },
                keyValues: new object[] { 17, 1 });

            migrationBuilder.DeleteData(
                table: "ReceiversMails",
                keyColumns: new[] { "MailId", "ReceiverId" },
                keyValues: new object[] { 15, 2 });

            migrationBuilder.DeleteData(
                table: "ReceiversMails",
                keyColumns: new[] { "MailId", "ReceiverId" },
                keyValues: new object[] { 16, 3 });

            migrationBuilder.DeleteData(
                table: "ReceiversMails",
                keyColumns: new[] { "MailId", "ReceiverId" },
                keyValues: new object[] { 17, 4 });

            migrationBuilder.DeleteData(
                table: "ReceiversMails",
                keyColumns: new[] { "MailId", "ReceiverId" },
                keyValues: new object[] { 17, 5 });

            migrationBuilder.DeleteData(
                table: "ReceiversMails",
                keyColumns: new[] { "MailId", "ReceiverId" },
                keyValues: new object[] { 14, 1 });

            migrationBuilder.DeleteData(
                table: "ReceiversMails",
                keyColumns: new[] { "MailId", "ReceiverId" },
                keyValues: new object[] { 10, 2 });

            migrationBuilder.DeleteData(
                table: "ReceiversMails",
                keyColumns: new[] { "MailId", "ReceiverId" },
                keyValues: new object[] { 11, 3 });

            migrationBuilder.DeleteData(
                table: "ReceiversMails",
                keyColumns: new[] { "MailId", "ReceiverId" },
                keyValues: new object[] { 12, 4 });

            migrationBuilder.DeleteData(
                table: "ReceiversMails",
                keyColumns: new[] { "MailId", "ReceiverId" },
                keyValues: new object[] { 13, 5 });

            migrationBuilder.DeleteData(
                table: "UsersSpams",
                keyColumns: new[] { "SpamId", "UserId" },
                keyValues: new object[] { 4, 5 });

            migrationBuilder.DeleteData(
                table: "Mails",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Mails",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Mails",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Mails",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Mails",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Mails",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Mails",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Mails",
                keyColumn: "Id",
                keyValue: 14);
        }
    }
}
