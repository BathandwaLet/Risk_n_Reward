using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Risk_n_Reward.Web.Models;

public class GameSession
{
    [Key]
    public int Id { get; set; }
    
    public Guid PublicId { get; set; } = Guid.NewGuid();
    
    [Required]
    public int PlayerId { get; set; }
    
    [Required]
    public int GameId { get; set; }
    
    [Required]
    [Column(TypeName = "decimal(18,2)")]
    public decimal BetAmount { get; set; }

    [Required]
    [Column(TypeName = "decimal(18,2)")]
    public decimal Payout { get; set; } = 0.00m;
    
    public Outcome Outcome { get; set; }
    
    public string? BetType { get; set; }

    public int WinStreakAtPlay { get; set; } = 0;
    
    public DateTime PlayedAt { get; set; } = DateTime.UtcNow;

    [ForeignKey(nameof(PlayerId))] 
    public Player Player { get; set; } = null!;

    [ForeignKey(nameof(GameId))] 
    public Game Game { get; set; } = null!;
}