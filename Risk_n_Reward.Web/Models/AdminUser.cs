using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Risk_n_Reward.Web.Models;

public class AdminUser
{
    [Key]
    public int Id { get; set; }
    
    public Guid PublicId { get; set; } = Guid.NewGuid();
    
    [Required]
    [MaxLength(50)]
    public string UserName { get; set; } = string.Empty;
    
    [Required]
    public string PasswordHash { get; set; } = string.Empty;
    
    public AdminRole Role { get; set; }
    
    public DateTime CreationDate { get; init; } = DateTime.UtcNow;
    
    public DateTime LastActiveDate { get; set; } = DateTime.UtcNow;
}