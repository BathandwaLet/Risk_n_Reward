using Microsoft.EntityFrameworkCore;
using Risk_n_Reward.Web.Models;

namespace Risk_n_Reward.Web.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options){}
    
    //Game
    public DbSet<Game> Games { get; set; }
    //GameSession
    public DbSet<GameSession> GameSessions { get; set; }
    //WalletTransaction
    public DbSet<WalletTransaction> WalletTransactions { get; set; }
    //Player
    public DbSet<Player> Players { get; set; }
    //Winstreak
    public DbSet<Models.WinStreak> WinStreaks { get; set; }
    //AdminUser
    public DbSet<AdminUser>  AdminUsers { get; set; }
    //SystemConfig
    public DbSet<SystemConfig> SystemConfigs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        
        //Enums (convert to string)
        modelBuilder.Entity<AdminUser>()
            .Property(a => a.Role)
            .HasConversion<string>();
        
        modelBuilder.Entity<GameSession>()
            .Property(gs => gs.Outcome)
            .HasConversion<string>();
        
        modelBuilder.Entity<WalletTransaction>()
            .Property(wt => wt.Type)
            .HasConversion<string>();
        
        //Indexes
        //Whom the gamesession belongs to?  
        modelBuilder.Entity<GameSession>().HasIndex(gs => gs.PlayerId);
        
        //Which game was played in the gamesession?
        modelBuilder.Entity<GameSession>().HasIndex(gs => gs.GameId);
        
        //When was the gamesession started at
        modelBuilder.Entity<GameSession>().HasIndex(gs => gs.PlayedAt);
        
        //Which player made a wallet transaction
        modelBuilder.Entity<WalletTransaction>().HasIndex(wt => wt.PlayerId);
        
        //Whom does the winstreak belong to
        modelBuilder.Entity<Models.WinStreak>().HasIndex(ws => ws.PlayerId);
        
        //Database Relationships
        //Player (Player --> Game)
        modelBuilder.Entity<Player>()
            .HasOne(p => p.FavouriteGame) //Each player has one favourite game
            .WithMany(g => g.FavouritedBy) //Each game can be favourited by many players
            .HasForeignKey(p =>
                p.FavouriteGameId) //Player has a Foreign Key of FavouriteGameId which references the Games id 
            .OnDelete(DeleteBehavior.SetNull); //On delete the player but set all references to null
        
        //GameSession (GameSession --> Player)
        modelBuilder.Entity<GameSession>()
            .HasOne(gs => gs.Player) //Each gamesession belongs to one player
            .WithMany(p => p.GameSessions) //Each player can have many sessions
            .HasForeignKey(g => g.PlayerId) //Gamesession has a foreign key of player id
            .OnDelete(DeleteBehavior.Cascade); //On deletion of a GameSession delete the all the references in player as well.
        
        //GameSession (GameSession --> Game)
        modelBuilder.Entity<GameSession>()
            .HasOne(g => g.Game) //Each GameSession belongs to one game
            .WithMany(g => g.GameSessions) //Each game has many GameSessions
            .HasForeignKey(g => g.GameId) //GameSession has a foreign key of game id
            .OnDelete(DeleteBehavior.Restrict); //On deletion Delete the gamesession do not delete the references in games table
        
        //WinStreak (WinStreak --> Game)
        modelBuilder.Entity<Models.WinStreak>()
            .HasOne(ws => ws.Game) //Each WinStreak belongs to one Game
            .WithMany(g => g.WinStreak) //Each Game has many WinStreaks
            .HasForeignKey(g => g.GameId) //WinStreak has a foreign key of Game id
            .OnDelete(DeleteBehavior.Restrict); //On deletion Delete the winstreak but keep the references in games table
        
        //WinStreak (WinStreak --> Player)
        modelBuilder.Entity<Models.WinStreak>()
            .HasOne(ws => ws.Player) //Each WinStreak belongs to one Player
            .WithMany(p => p.WinStreaks) //Each player has many winstreaks
            .HasForeignKey(p => p.PlayerId) // Winstreak has a player id foreign key
            .OnDelete(DeleteBehavior.Cascade); //On deletion (e.g player is deleted winstreaks associated should be deleted as well

        //Seed the games in games table
        modelBuilder.Entity<Game>().HasData(
            new Game {Id = 1, Name = "Baccarat",Description = "Baccarat is a simple card game. You have two option to bet on Player or Banker. The aim is to get as close to nine (9) as possible.", IsEnabled = true},
            new Game {Id = 2, Name = "BlackJack",Description = "An iconic game, get closer to twenty one (21) than the dealer without going over. Everytime you hit or stand matters. Easy enough right?", IsEnabled = true},
            new Game {Id = 3, Name = "CoinToss",Description = "Simple is an understatement. Coin Toss requires the player to call head or tails watch the coin fly and find out if you made the right call. No complex strategy needed it just 50-50 chance.", IsEnabled = true},
            new Game {Id = 4, Name = "Crash",Description = "A multiplier grows from one and you aim to cash out before you crash. Wait longer and the reward grows but wait to long and lose it all. A game all about having courage but also knowing when to walk away.", IsEnabled = true},
            new Game {Id = 5, Name = "HighLow",Description = "You are shown a card and you guess whether the next card is higher or lower. Simple right?", IsEnabled = true},
            new Game {Id = 6, Name = "LuckyDice",Description = "In LuckyDice you wager on the chancee of getting a double from a dice roll(e.g two sixes). Fast, unpredictable and oddly satisfying. Roll the the dice and see.", IsEnabled = true},
            new Game {Id = 7, Name = "PickFive",Description = "Select five (5) number or use the quick pick and find out if they match the draw. A lottery styled game played with patience and hope. The odds are low but the pay off is worth it. Pick your number and see if lady luck is on your side.", IsEnabled = true},
            new Game {Id = 8, Name = "Roulette",Description = "A ball is spun around a numbered wheel and you a to bet on the square it will land in. Keep it simple with black, red or chase the greater rewards with specific numbers. No two spins are the same and every round is another chance.", IsEnabled = true},
            new Game {Id = 9, Name = "Slots",Description = "Place your bet, spin the reel and see what lines up. No complex strategy needed, just set the wager and see what lines up. Each line up of symbols hold different payouts.", IsEnabled = true},
            new Game {Id = 10, Name = "TexasHoldem",Description = "A poker game against the house. You and the dealer each get dealt the cards and the best hand wins. All the excitement of poker without having to read the room.", IsEnabled = true}
            );
        
        //Define the system config rules and actions (Reloading wallets, and winstreaks)
        modelBuilder.Entity<SystemConfig>().HasData(
            new SystemConfig{Id = 1, Key = "ReloadAmount", Value = "1000", Description = "Fixed amount of VMali added to the player's wallet on reload.", LastUpdated = new DateTime(2026,1,1,0,0,0,DateTimeKind.Utc), UpdatedBy = null},
            new SystemConfig{Id = 2, Key = "ReloadThreshold", Value = "100", Description = "Player can only reload once at or below this amount", LastUpdated = new DateTime(2026,1,1,0,0,0,DateTimeKind.Utc), UpdatedBy = null},
            new SystemConfig{Id = 3, Key = "StreakBonus_Threshold_1", Value = "3", Description = "Minimum WinStreak to qualify for the first bonus tier", LastUpdated = new DateTime(2026,1,1,0,0,0,DateTimeKind.Utc), UpdatedBy = null},
            new SystemConfig{Id = 4, Key = "StreakBonus_Threshold_2", Value = "5", Description = "Minimum WinStreak to qualify for the second bonus tier", LastUpdated = new DateTime(2026,1,1,0,0,0,DateTimeKind.Utc), UpdatedBy = null},
            new SystemConfig{Id = 5, Key = "StreakBonus_Threshold_3", Value = "10", Description = "Minimum WinStreak to qualify for the third bonus tier", LastUpdated = new DateTime(2026,1,1,0,0,0,DateTimeKind.Utc), UpdatedBy = null},
            new SystemConfig{Id = 6, Key = "StreakBonus_Multiplier_1", Value = "1.25", Description = "Payout multiplier applied at streak tier 1", LastUpdated = new DateTime(2026,1,1,0,0,0,DateTimeKind.Utc), UpdatedBy = null},
            new SystemConfig{Id = 7, Key = "StreakBonus_Multiplier_2", Value = "1.50", Description = "Payout multiplier applied at streak tier 2", LastUpdated = new DateTime(2026,1,1,0,0,0,DateTimeKind.Utc), UpdatedBy = null},
            new SystemConfig{Id = 8, Key = "StreakBonus_Multiplier_3", Value = "2.00", Description = "Payout multiplier applied at streak tier 3", LastUpdated = new DateTime(2026,1,1,0,0,0,DateTimeKind.Utc), UpdatedBy = null}
            );
        
        // Seed the test accounts
        // Fixed IDs and PublicIds keep migrations stable.
        // PasswordHash is a placeholder for "Dev@1234".
        // Replace with Identity-managed hashes before deploy.
        // TODO: Remove or gate behind IsDevelopment() before
        // deploying to production.
        
        // Fixed password hash
        const string devPasswordHash = "$2a$11$DevelopmentHashPlaceholderXXXXXXXXXXXXXXXXXXXXXXXXXXXX";

        //Seeded player test account
        modelBuilder.Entity<Player>().HasData(
            new Player
            {
                Id = 1,
                PublicId = new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaa"),
                Username = "DevPlayer",
                PasswordHash = devPasswordHash,
                WalletBalance = 50000.0m,
                FavouriteGameId = null,
                JoinDate = new DateTime(2026,1,1,0,0,0,DateTimeKind.Utc),
                LastActiveDate = new DateTime(2026,1,1,0,0,0,DateTimeKind.Utc)
            }
            );
        
        //Seeded admin test account
        modelBuilder.Entity<AdminUser>().HasData(
            new AdminUser
            {
                Id = 1,
                PublicId = new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbb"),
                UserName = "DevAdmin",
                PasswordHash = devPasswordHash,
                Role = AdminRole.SuperAdmin,
                CreationDate = new DateTime(2026,1,1,0,0,0,DateTimeKind.Utc),
                LastActiveDate = new DateTime(2026,1,1,0,0,0,DateTimeKind.Utc)
            }
        );
    }
}