using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Risk_n_Reward.Web.Models;

public class Player
{
    [Key]
    public Guid PlayerId { get; set; } = Guid.NewGuid();
    
    [Required]
    [MaxLength(50)]
    public string Username { get; set; }

    [Required] //To be hashed
    public string Password { get; set; } = string.Empty;
    
    [Range(0, 10000000)]
    public decimal WalletBalance { get; set; }
    
    public int? FavouriteGameId { get; set; }
    
    public DateTime DateJoined { get; set; } = DateTime.UtcNow;
    
    public DateTime DateLastActive { get; set; } = DateTime.UtcNow;
    
}