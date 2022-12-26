using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DmailApp.Data.Migrations
{
    public partial class AddingInitialSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Mail_User_SenderId",
                table: "Mail");

            migrationBuilder.DropForeignKey(
                name: "FK_ReceiversMails_Mail_ReceiverId",
                table: "ReceiversMails");

            migrationBuilder.DropForeignKey(
                name: "FK_ReceiversMails_User_MailId",
                table: "ReceiversMails");

            migrationBuilder.DropForeignKey(
                name: "FK_UsersSpams_User_SpamId",
                table: "UsersSpams");

            migrationBuilder.DropForeignKey(
                name: "FK_UsersSpams_User_UserId",
                table: "UsersSpams");

            migrationBuilder.DropPrimaryKey(
                name: "PK_User",
                table: "User");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Mail",
                table: "Mail");

            migrationBuilder.RenameTable(
                name: "User",
                newName: "Users");

            migrationBuilder.RenameTable(
                name: "Mail",
                newName: "Mails");

            migrationBuilder.RenameIndex(
                name: "IX_Mail_SenderId",
                table: "Mails",
                newName: "IX_Mails_SenderId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Mails",
                table: "Mails",
                column: "Id");

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
                columns: new[] { "MailId", "ReceiverId" },
                values: new object[,]
                {
                    { 2, 1 },
                    { 3, 1 },
                    { 9, 1 },
                    { 1, 2 },
                    { 4, 2 },
                    { 6, 2 },
                    { 9, 2 },
                    { 1, 3 },
                    { 5, 3 },
                    { 8, 4 },
                    { 7, 5 },
                    { 9, 5 }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Mails_Users_SenderId",
                table: "Mails",
                column: "SenderId",
                principalTable: "Users",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ReceiversMails_Mails_MailId",
                table: "ReceiversMails",
                column: "MailId",
                principalTable: "Mails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ReceiversMails_Users_ReceiverId",
                table: "ReceiversMails",
                column: "ReceiverId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UsersSpams_Users_SpamId",
                table: "UsersSpams",
                column: "SpamId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UsersSpams_Users_UserId",
                table: "UsersSpams",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Mails_Users_SenderId",
                table: "Mails");

            migrationBuilder.DropForeignKey(
                name: "FK_ReceiversMails_Mails_MailId",
                table: "ReceiversMails");

            migrationBuilder.DropForeignKey(
                name: "FK_ReceiversMails_Users_ReceiverId",
                table: "ReceiversMails");

            migrationBuilder.DropForeignKey(
                name: "FK_UsersSpams_Users_SpamId",
                table: "UsersSpams");

            migrationBuilder.DropForeignKey(
                name: "FK_UsersSpams_Users_UserId",
                table: "UsersSpams");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Mails",
                table: "Mails");

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
                keyValues: new object[] { 9, 1 });

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
                keyValues: new object[] { 6, 2 });

            migrationBuilder.DeleteData(
                table: "ReceiversMails",
                keyColumns: new[] { "MailId", "ReceiverId" },
                keyValues: new object[] { 9, 2 });

            migrationBuilder.DeleteData(
                table: "ReceiversMails",
                keyColumns: new[] { "MailId", "ReceiverId" },
                keyValues: new object[] { 1, 3 });

            migrationBuilder.DeleteData(
                table: "ReceiversMails",
                keyColumns: new[] { "MailId", "ReceiverId" },
                keyValues: new object[] { 5, 3 });

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

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "User");

            migrationBuilder.RenameTable(
                name: "Mails",
                newName: "Mail");

            migrationBuilder.RenameIndex(
                name: "IX_Mails_SenderId",
                table: "Mail",
                newName: "IX_Mail_SenderId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_User",
                table: "User",
                column: "UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Mail",
                table: "Mail",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Mail_User_SenderId",
                table: "Mail",
                column: "SenderId",
                principalTable: "User",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ReceiversMails_Mail_ReceiverId",
                table: "ReceiversMails",
                column: "ReceiverId",
                principalTable: "Mail",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ReceiversMails_User_MailId",
                table: "ReceiversMails",
                column: "MailId",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UsersSpams_User_SpamId",
                table: "UsersSpams",
                column: "SpamId",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UsersSpams_User_UserId",
                table: "UsersSpams",
                column: "UserId",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
