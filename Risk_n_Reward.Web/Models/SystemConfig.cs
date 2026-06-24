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
    
    public string Description { get; set; } = string.Empty;
    
    public DateTime LastUpdated { get; set; } = DateTime.UtcNow;
    
    public string UpdatedBy { get; set; } = string.Empty;
}