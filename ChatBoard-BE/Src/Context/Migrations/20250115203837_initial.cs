using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Context.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:PostgresExtension:pg_buffercache", ",,")
                .Annotation("Npgsql:PostgresExtension:pg_stat_statements", ",,");

            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FirstName = table.Column<string>(type: "text", nullable: true),
                    MiddleName = table.Column<string>(type: "text", nullable: true),
                    LastName = table.Column<string>(type: "text", nullable: true),
                    Address = table.Column<string>(type: "text", nullable: true),
                    Username = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: true),
                    Password = table.Column<string>(type: "text", nullable: true),
                    EGender = table.Column<int>(type: "integer", nullable: false),
                    DOB = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsEmailVerified = table.Column<bool>(type: "boolean", nullable: false),
                    IsVerifiedAccount = table.Column<bool>(type: "boolean", nullable: false),
                    EmailVerifiedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    ProfileImage = table.Column<string>(type: "text", nullable: true),
                    ProfileBannerImage = table.Column<string>(type: "text", nullable: true),
                    Bio = table.Column<string>(type: "text", nullable: true),
                    AccountType = table.Column<int>(type: "integer", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    createdAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    updatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "ConstantSetting",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Key = table.Column<string>(type: "text", nullable: true),
                    Value = table.Column<string>(type: "text", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    createdAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    updatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConstantSetting", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SenderId = table.Column<long>(type: "bigint", nullable: false),
                    ReceiverId = table.Column<long>(type: "bigint", nullable: false),
                    Content = table.Column<string>(type: "text", nullable: true),
                    SentAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    IsRead = table.Column<bool>(type: "boolean", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    createdAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    updatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "AccountConfirmations",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Code = table.Column<string>(type: "text", nullable: true),
                    ExpireAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsUsed = table.Column<bool>(type: "boolean", nullable: false),
                    UsedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    AccountId = table.Column<long>(type: "bigint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    createdAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    updatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountConfirmations", x => x.id);
                    table.ForeignKey(
                        name: "FK_AccountConfirmations_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DeviceActivities",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AccountId = table.Column<long>(type: "bigint", nullable: false),
                    Operation = table.Column<string>(type: "text", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    createdAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    updatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceActivities", x => x.id);
                    table.ForeignKey(
                        name: "FK_DeviceActivities_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserDevices",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AccountId = table.Column<long>(type: "bigint", nullable: false),
                    DeviceName = table.Column<string>(type: "text", nullable: true),
                    DeviceType = table.Column<string>(type: "text", nullable: true),
                    IpAddress = table.Column<string>(type: "text", nullable: true),
                    PlayerID = table.Column<string>(type: "text", nullable: true),
                    VoiPPlayerId = table.Column<string>(type: "text", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    createdAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    updatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserDevices", x => x.id);
                    table.ForeignKey(
                        name: "FK_UserDevices_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccountConfirmations_AccountId",
                table: "AccountConfirmations",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_DeviceActivities_AccountId",
                table: "DeviceActivities",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_UserDevices_AccountId",
                table: "UserDevices",
                column: "AccountId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountConfirmations");

            migrationBuilder.DropTable(
                name: "ConstantSetting");

            migrationBuilder.DropTable(
                name: "DeviceActivities");

            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropTable(
                name: "UserDevices");

            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.AlterDatabase()
                .OldAnnotation("Npgsql:PostgresExtension:pg_buffercache", ",,")
                .OldAnnotation("Npgsql:PostgresExtension:pg_stat_statements", ",,");
        }
    }
}
