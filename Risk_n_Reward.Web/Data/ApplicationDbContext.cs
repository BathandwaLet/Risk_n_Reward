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
        //Player
        modelBuilder.Entity<Player>()
            .HasOne(p => p.FavouriteGame) //Each player has one favourite game
            .WithMany(g => g.FavouritedBy) //Each game can be favourited by many players
            .HasForeignKey(p =>
                p.FavouriteGameId) //Player has a Foreign Key of FavouriteGameId which references the Games id 
            .OnDelete(DeleteBehavior.SetNull); //On delete the player but set all references to null
        
        //
        
    }
}