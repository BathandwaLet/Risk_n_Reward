using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Risk_n_Reward.Web.Models;

public class Player
{
    [Key]
    public Guid PlayerId { get; set; }
    
    [Required]
    [MaxLength(50)]
    public string Username { get; set; }

    [Required] 
    public string Password { get; set; } = string.Empty;
    
    public decimal WalletBalance { get; set; }
    
    public int? FavouriteGameId { get; set; }
    
    public DateTime DateJoined { get; set; } = DateTime.UtcNow;
    
    public DateTime DateLastActive { get; set; } = DateTime.UtcNow;
    
}