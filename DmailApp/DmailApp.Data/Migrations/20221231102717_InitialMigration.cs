using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DmailApp.Data.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Adress = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Mails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: false),
                    DateTimeOfSending = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    WasRead = table.Column<bool>(type: "boolean", nullable: false),
                    SenderId = table.Column<int>(type: "integer", nullable: false),
                    mail_type = table.Column<string>(type: "text", nullable: false),
                    EventTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Content = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Mails_Users_SenderId",
                        column: x => x.SenderId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UsersSpams",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    SpamId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersSpams", x => new { x.UserId, x.SpamId });
                    table.ForeignKey(
                        name: "FK_UsersSpams_Users_SpamId",
                        column: x => x.SpamId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsersSpams_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReceiversMails",
                columns: table => new
                {
                    ReceiverId = table.Column<int>(type: "integer", nullable: false),
                    MailId = table.Column<int>(type: "integer", nullable: false),
                    mail_type = table.Column<string>(type: "text", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReceiversMails", x => new { x.ReceiverId, x.MailId });
                    table.ForeignKey(
                        name: "FK_ReceiversMails_Mails_MailId",
                        column: x => x.MailId,
                        principalTable: "Mails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReceiversMails_Users_ReceiverId",
                        column: x => x.ReceiverId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_Mails_SenderId",
                table: "Mails",
                column: "SenderId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiversMails_MailId",
                table: "ReceiversMails",
                column: "MailId");

            migrationBuilder.CreateIndex(
                name: "IX_UsersSpams_SpamId",
                table: "UsersSpams",
                column: "SpamId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReceiversMails");

            migrationBuilder.DropTable(
                name: "UsersSpams");

            migrationBuilder.DropTable(
                name: "Mails");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
