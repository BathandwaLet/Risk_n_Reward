using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Risk_n_Reward.Web.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AdminUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PublicId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: false),
                    Role = table.Column<string>(type: "text", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastActiveDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdminUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Games",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    IsEnabled = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SystemConfigs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Key = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Value = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    LastUpdated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedBy = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemConfigs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PublicId = table.Column<Guid>(type: "uuid", nullable: false),
                    Username = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: false),
                    WalletBalance = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    FavouriteGameId = table.Column<int>(type: "integer", nullable: true),
                    JoinDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastActiveDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Players_Games_FavouriteGameId",
                        column: x => x.FavouriteGameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "GameSessions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PublicId = table.Column<Guid>(type: "uuid", nullable: false),
                    PlayerId = table.Column<int>(type: "integer", nullable: false),
                    GameId = table.Column<int>(type: "integer", nullable: false),
                    BetAmount = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    Payout = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    Outcome = table.Column<string>(type: "text", nullable: false),
                    BetType = table.Column<string>(type: "text", nullable: true),
                    WinStreakAtPlay = table.Column<int>(type: "integer", nullable: false),
                    PlayedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameSessions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GameSessions_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GameSessions_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WalletTransactions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PlayerId = table.Column<int>(type: "integer", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: false),
                    Amount = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    BalanceAfter = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WalletTransactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WalletTransactions_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WinStreaks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PlayerId = table.Column<int>(type: "integer", nullable: false),
                    GameId = table.Column<int>(type: "integer", nullable: false),
                    CurrentStreak = table.Column<int>(type: "integer", nullable: false),
                    BestStreak = table.Column<int>(type: "integer", nullable: false),
                    LastUpdated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WinStreaks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WinStreaks_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WinStreaks_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AdminUsers",
                columns: new[] { "Id", "CreationDate", "LastActiveDate", "PasswordHash", "PublicId", "Role", "UserName" },
                values: new object[] { 1, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "$2a$11$DevelopmentHashPlaceholderXXXXXXXXXXXXXXXXXXXXXXXXXXXX", new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), "SuperAdmin", "DevAdmin" });

            migrationBuilder.InsertData(
                table: "Games",
                columns: new[] { "Id", "Description", "IsEnabled", "Name" },
                values: new object[,]
                {
                    { 1, "Baccarat is a simple card game. You have two option to bet on Player or Banker. The aim is to get as close to nine (9) as possible.", true, "Baccarat" },
                    { 2, "An iconic game, get closer to twenty one (21) than the dealer without going over. Everytime you hit or stand matters. Easy enough right?", true, "BlackJack" },
                    { 3, "Simple is an understatement. Coin Toss requires the player to call head or tails watch the coin fly and find out if you made the right call. No complex strategy needed it just 50-50 chance.", true, "CoinToss" },
                    { 4, "A multiplier grows from one and you aim to cash out before you crash. Wait longer and the reward grows but wait to long and lose it all. A game all about having courage but also knowing when to walk away.", true, "Crash" },
                    { 5, "You are shown a card and you guess whether the next card is higher or lower. Simple right?", true, "HighLow" },
                    { 6, "In LuckyDice you wager on the chancee of getting a double from a dice roll(e.g two sixes). Fast, unpredictable and oddly satisfying. Roll the the dice and see.", true, "LuckyDice" },
                    { 7, "Select five (5) number or use the quick pick and find out if they match the draw. A lottery styled game played with patience and hope. The odds are low but the pay off is worth it. Pick your number and see if lady luck is on your side.", true, "PickFive" },
                    { 8, "A ball is spun around a numbered wheel and you a to bet on the square it will land in. Keep it simple with black, red or chase the greater rewards with specific numbers. No two spins are the same and every round is another chance.", true, "Roulette" },
                    { 9, "Place your bet, spin the reel and see what lines up. No complex strategy needed, just set the wager and see what lines up. Each line up of symbols hold different payouts.", true, "Slots" },
                    { 10, "A poker game against the house. You and the dealer each get dealt the cards and the best hand wins. All the excitement of poker without having to read the room.", true, "TexasHoldem" }
                });

            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "Id", "FavouriteGameId", "JoinDate", "LastActiveDate", "PasswordHash", "PublicId", "Username", "WalletBalance" },
                values: new object[] { 1, null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "$2a$11$DevelopmentHashPlaceholderXXXXXXXXXXXXXXXXXXXXXXXXXXXX", new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), "DevPlayer", 50000.0m });

            migrationBuilder.InsertData(
                table: "SystemConfigs",
                columns: new[] { "Id", "Description", "Key", "LastUpdated", "UpdatedBy", "Value" },
                values: new object[,]
                {
                    { 1, "Fixed amount of VMali added to the player's wallet on reload.", "ReloadAmount", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "1000" },
                    { 2, "Player can only reload once at or below this amount", "ReloadThreshold", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "100" },
                    { 3, "Minimum WinStreak to qualify for the first bonus tier", "StreakBonus_Threshold_1", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "3" },
                    { 4, "Minimum WinStreak to qualify for the second bonus tier", "StreakBonus_Threshold_2", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "5" },
                    { 5, "Minimum WinStreak to qualify for the third bonus tier", "StreakBonus_Threshold_3", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "10" },
                    { 6, "Payout multiplier applied at streak tier 1", "StreakBonus_Multiplier_1", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "1.25" },
                    { 7, "Payout multiplier applied at streak tier 2", "StreakBonus_Multiplier_2", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "1.50" },
                    { 8, "Payout multiplier applied at streak tier 3", "StreakBonus_Multiplier_3", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "2.00" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_GameSessions_GameId",
                table: "GameSessions",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_GameSessions_PlayedAt",
                table: "GameSessions",
                column: "PlayedAt");

            migrationBuilder.CreateIndex(
                name: "IX_GameSessions_PlayerId",
                table: "GameSessions",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_Players_FavouriteGameId",
                table: "Players",
                column: "FavouriteGameId");

            migrationBuilder.CreateIndex(
                name: "IX_WalletTransactions_PlayerId",
                table: "WalletTransactions",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_WinStreaks_GameId",
                table: "WinStreaks",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_WinStreaks_PlayerId",
                table: "WinStreaks",
                column: "PlayerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdminUsers");

            migrationBuilder.DropTable(
                name: "GameSessions");

            migrationBuilder.DropTable(
                name: "SystemConfigs");

            migrationBuilder.DropTable(
                name: "WalletTransactions");

            migrationBuilder.DropTable(
                name: "WinStreaks");

            migrationBuilder.DropTable(
                name: "Players");

            migrationBuilder.DropTable(
                name: "Games");
        }
    }
}
