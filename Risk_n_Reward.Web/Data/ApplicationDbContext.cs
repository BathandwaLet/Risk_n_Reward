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

    }
}