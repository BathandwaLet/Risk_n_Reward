using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Risk_n_Reward.Web.Models;

public class AdminUser
{
    [Key]
    public Guid AdminId { get; set; } = Guid.NewGuid();
    
    [Required]
    [MaxLength(50)]
    public string Username { get; set; }
    
    [Required] //To be hashed
    public string Password { get; set; }
    
    [Required]
    [MaxLength(20)]
    public string Role { get; set; }
    
    public DateTime DateCreated { get; set; } = DateTime.UtcNow;
    
    public DateTime DateLastActive { get; set; } = DateTime.UtcNow;
}
