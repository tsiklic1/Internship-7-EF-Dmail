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
                name: "User",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Adress = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Mail",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: false),
                    DateTimeOfSending = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    WasRead = table.Column<bool>(type: "boolean", nullable: false),
                    SenderId = table.Column<int>(type: "integer", nullable: false),
                    Discriminator = table.Column<string>(type: "text", nullable: false),
                    EventTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Content = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Mail_User_SenderId",
                        column: x => x.SenderId,
                        principalTable: "User",
                        principalColumn: "UserId");
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
                        name: "FK_UsersSpams_User_SpamId",
                        column: x => x.SpamId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsersSpams_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReceiversMails",
                columns: table => new
                {
                    ReceiverId = table.Column<int>(type: "integer", nullable: false),
                    MailId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReceiversMails", x => new { x.ReceiverId, x.MailId });
                    table.ForeignKey(
                        name: "FK_ReceiversMails_Mail_ReceiverId",
                        column: x => x.ReceiverId,
                        principalTable: "Mail",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReceiversMails_User_MailId",
                        column: x => x.MailId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Mail_SenderId",
                table: "Mail",
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
                name: "Mail");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
