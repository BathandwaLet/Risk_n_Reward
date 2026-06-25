using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Risk_n_Reward.Web.Models;

public class SystemConfig
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(50)] 
    public string Key { get; set; } = string.Empty;
    
    [Required]
    public string Value { get; set; } = string.Empty;
    
    [MaxLength(255)] 
    public string? Description { get; set; }
    
    public DateTime LastUpdated { get; set; } = DateTime.UtcNow;
    
    [MaxLength(50)] 
    public string? UpdatedBy { get; set; }
}