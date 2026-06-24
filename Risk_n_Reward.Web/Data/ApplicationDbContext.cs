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
    public DbSet<WinStreak> WinStreaks { get; set; }
    //AdminUser
    public DbSet<AdminUser>  AdminUsers { get; set; }
    //SystemConfig
    public DbSet<SystemConfig> SystemConfigs { get; set; }

    
}