using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Risk_n_Reward.Web.Models;

public class Player
{
    [Key]
    public int Id { get; set; }
    
    public Guid PublicId { get; set; } = Guid.NewGuid();
    
    [Required]
    [MaxLength(50)]
    public string Username { get; set; } = string.Empty;
    
    [Required]
    public string PasswordHash { get; set; } = string.Empty;

    [Column(TypeName = "decimal(18,2)")] 
    public decimal WalletBalance { get; set; } = 1000.00m;
    
    public int? FavouriteGameId { get; set; }
    
    public DateTime JoinDate { get; set; } = DateTime.UtcNow;
    
    public DateTime LastActiveDate { get; set; } = DateTime.UtcNow;
    
    [ForeignKey(nameof(FavouriteGameId))]
    public Game? FavouriteGame { get; set; }

    public ICollection<GameSession> GameSessions { get; set; } = new ICollection<GameSession>();

    public ICollection<WinStreak> WinStreaks { get; set; } = new List<WinStreak>();

    public ICollection<WalletTransaction> WalletTransactions { get; set; } = new List<WalletTransaction>();
}