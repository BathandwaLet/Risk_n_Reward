using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Risk_n_Reward.Web.Models;

public class WinStreak
{
    [Key] public int Id { get; set; }

    [Required] public int PlayerId { get; set; }

    [Required] public int GameId { get; set; }

    public int CurrentStreak { get; set; } = 0;

    public int BestStreak { get; set; } = 0;

    public DateTime LastUpdated { get; set; } = DateTime.UtcNow;

    [ForeignKey(nameof(PlayerId))] public Player Player { get; set; } = null!;

    [ForeignKey(nameof(GameId))] public Game Game { get; set; } = null!;

}