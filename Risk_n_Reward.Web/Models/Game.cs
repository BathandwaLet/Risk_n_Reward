using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Risk_n_Reward.Web.Models;

public class Game
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    [MaxLength(50)]
    public string Name { get; set; } = string.Empty;
    
    [MaxLength(255)]
    public string? Description { get; set; }

    public bool IsEnabled { get; set; } = true;
    
    public ICollection<GameSession> GameSessions { get; set; } = new List<GameSession>();

    public ICollection<WinStreak> WinStreak { get; set; } = new List<WinStreak>();
    
    public ICollection<Player> FavouritedBy { get; set; } = new List<Player>();
}