using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DmailApp.Data.Migrations
{
    public partial class AddingInitialSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Adress", "Password" },
                values: new object[,]
                {
                    { 1, "mirko@gmail.com", "12345" },
                    { 2, "domagoj@gmail.com", "abcde" },
                    { 3, "marko12@dump.com", "sifra" },
                    { 4, "dragana.dump@gmail.com", "dragana1234" },
                    { 5, "dunja@gmail.com", "dumpdump" }
                });

            migrationBuilder.InsertData(
                table: "Mails",
                columns: new[] { "Id", "DateTimeOfSending", "EventTime", "SenderId", "Title", "WasRead", "mail_type" },
                values: new object[,]
                {
                    { 6, new DateTime(2022, 11, 11, 20, 0, 0, 0, DateTimeKind.Utc), new DateTime(2023, 2, 2, 16, 0, 0, 0, DateTimeKind.Utc), 1, "Utakmica", false, "eventmail" },
                    { 7, new DateTime(2022, 4, 4, 20, 0, 0, 0, DateTimeKind.Utc), new DateTime(2022, 4, 7, 16, 0, 0, 0, DateTimeKind.Utc), 1, "Rodendan", true, "eventmail" },
                    { 8, new DateTime(2022, 12, 1, 8, 0, 0, 0, DateTimeKind.Utc), new DateTime(2023, 2, 2, 16, 0, 0, 0, DateTimeKind.Utc), 2, "Predavanje", false, "eventmail" },
                    { 9, new DateTime(2022, 11, 11, 20, 0, 0, 0, DateTimeKind.Utc), new DateTime(2023, 7, 2, 16, 0, 0, 0, DateTimeKind.Utc), 4, "Radionica", false, "eventmail" }
                });

            migrationBuilder.InsertData(
                table: "Mails",
                columns: new[] { "Id", "Content", "DateTimeOfSending", "SenderId", "Title", "WasRead", "mail_type" },
                values: new object[,]
                {
                    { 1, "Dobar dan.", new DateTime(2022, 7, 11, 7, 0, 0, 0, DateTimeKind.Utc), 1, "Pozdrav", false, "textmail" },
                    { 2, "Poštovani, šaljem vam izvještaj.", new DateTime(2022, 8, 5, 9, 0, 0, 0, DateTimeKind.Utc), 2, "Izvještaj", false, "textmail" },
                    { 3, "U dokumentu se nalazi slika zgrade fakulteta", new DateTime(2022, 7, 11, 7, 0, 0, 0, DateTimeKind.Utc), 3, "Sika", false, "textmail" },
                    { 4, "Hej, pogledaj ovaj video.", new DateTime(2022, 7, 11, 7, 0, 0, 0, DateTimeKind.Utc), 4, "YouTube video", false, "textmail" },
                    { 5, "Kalendar: Siječanj, veljača, ožujak, travanj, ...", new DateTime(2022, 7, 11, 7, 0, 0, 0, DateTimeKind.Utc), 4, "Kalendar", false, "textmail" }
                });

            migrationBuilder.InsertData(
                table: "UsersSpams",
                columns: new[] { "SpamId", "UserId" },
                values: new object[,]
                {
                    { 4, 1 },
                    { 1, 2 },
                    { 3, 2 }
                });

            migrationBuilder.InsertData(
                table: "ReceiversMails",
                columns: new[] { "MailId", "ReceiverId", "Status", "mail_type" },
                values: new object[,]
                {
                    { 9, 1, 1, "eventmail" },
                    { 6, 2, 2, "eventmail" },
                    { 9, 2, 2, "eventmail" },
                    { 8, 4, 0, "eventmail" },
                    { 7, 5, 2, "eventmail" },
                    { 9, 5, 2, "eventmail" }
                });

            migrationBuilder.InsertData(
                table: "ReceiversMails",
                columns: new[] { "MailId", "ReceiverId", "mail_type" },
                values: new object[,]
                {
                    { 2, 1, "textmail" },
                    { 3, 1, "textmail" },
                    { 1, 2, "textmail" },
                    { 4, 2, "textmail" },
                    { 1, 3, "textmail" },
                    { 5, 3, "textmail" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ReceiversMails",
                keyColumns: new[] { "MailId", "ReceiverId" },
                keyValues: new object[] { 9, 1 });

            migrationBuilder.DeleteData(
                table: "ReceiversMails",
                keyColumns: new[] { "MailId", "ReceiverId" },
                keyValues: new object[] { 6, 2 });

            migrationBuilder.DeleteData(
                table: "ReceiversMails",
                keyColumns: new[] { "MailId", "ReceiverId" },
                keyValues: new object[] { 9, 2 });

            migrationBuilder.DeleteData(
                table: "ReceiversMails",
                keyColumns: new[] { "MailId", "ReceiverId" },
                keyValues: new object[] { 8, 4 });

            migrationBuilder.DeleteData(
                table: "ReceiversMails",
                keyColumns: new[] { "MailId", "ReceiverId" },
                keyValues: new object[] { 7, 5 });

            migrationBuilder.DeleteData(
                table: "ReceiversMails",
                keyColumns: new[] { "MailId", "ReceiverId" },
                keyValues: new object[] { 9, 5 });

            migrationBuilder.DeleteData(
                table: "ReceiversMails",
                keyColumns: new[] { "MailId", "ReceiverId" },
                keyValues: new object[] { 2, 1 });

            migrationBuilder.DeleteData(
                table: "ReceiversMails",
                keyColumns: new[] { "MailId", "ReceiverId" },
                keyValues: new object[] { 3, 1 });

            migrationBuilder.DeleteData(
                table: "ReceiversMails",
                keyColumns: new[] { "MailId", "ReceiverId" },
                keyValues: new object[] { 1, 2 });

            migrationBuilder.DeleteData(
                table: "ReceiversMails",
                keyColumns: new[] { "MailId", "ReceiverId" },
                keyValues: new object[] { 4, 2 });

            migrationBuilder.DeleteData(
                table: "ReceiversMails",
                keyColumns: new[] { "MailId", "ReceiverId" },
                keyValues: new object[] { 1, 3 });

            migrationBuilder.DeleteData(
                table: "ReceiversMails",
                keyColumns: new[] { "MailId", "ReceiverId" },
                keyValues: new object[] { 5, 3 });

            migrationBuilder.DeleteData(
                table: "UsersSpams",
                keyColumns: new[] { "SpamId", "UserId" },
                keyValues: new object[] { 4, 1 });

            migrationBuilder.DeleteData(
                table: "UsersSpams",
                keyColumns: new[] { "SpamId", "UserId" },
                keyValues: new object[] { 1, 2 });

            migrationBuilder.DeleteData(
                table: "UsersSpams",
                keyColumns: new[] { "SpamId", "UserId" },
                keyValues: new object[] { 3, 2 });

            migrationBuilder.DeleteData(
                table: "Mails",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Mails",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Mails",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Mails",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Mails",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Mails",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Mails",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Mails",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Mails",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 4);
        }
    }
}
