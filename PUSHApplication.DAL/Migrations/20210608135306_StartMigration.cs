using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PUSHApplication.DAL.Migrations
{
    public partial class StartMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    MessageId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Header = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MessageSendTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.MessageId);
                });

            migrationBuilder.CreateTable(
                name: "MobileApps",
                columns: table => new
                {
                    PhoneNumber = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirebaseToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AppVersion = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MobileApps", x => x.PhoneNumber);
                });

            migrationBuilder.CreateTable(
                name: "MessageToPhoneNumbers",
                columns: table => new
                {
                    MobileAppPhoneNumber = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MessageId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MessageToPhoneNumbers", x => new { x.MessageId, x.MobileAppPhoneNumber });
                    table.ForeignKey(
                        name: "FK_MessageToPhoneNumbers_Messages_MessageId",
                        column: x => x.MessageId,
                        principalTable: "Messages",
                        principalColumn: "MessageId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MessageToPhoneNumbers_MobileApps_MobileAppPhoneNumber",
                        column: x => x.MobileAppPhoneNumber,
                        principalTable: "MobileApps",
                        principalColumn: "PhoneNumber",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MessageToPhoneNumbers_MobileAppPhoneNumber",
                table: "MessageToPhoneNumbers",
                column: "MobileAppPhoneNumber");

            migrationBuilder.CreateIndex(
                name: "IX_MessageToPhoneNumbers_MessageId",
                table: "MessageToPhoneNumbers",
                column: "MessageId");

            migrationBuilder.CreateIndex(
                name: "IX_MobileApps_PhoneNumber",
                table: "MobileApps",
                column: "PhoneNumber");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MessageToPhoneNumbers");

            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropTable(
                name: "MobileApps");
        }
    }
}
