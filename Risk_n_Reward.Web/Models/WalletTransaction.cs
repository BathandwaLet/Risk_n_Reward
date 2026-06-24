using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Risk_n_Reward.Web.Models;

public class WalletTransaction
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    public int PlayerId { get; set; }
    
    public TransactionType Type { get; set; }
    
    [Required]
    [Column(TypeName = "decimal(18,2)")]
    public decimal Amount { get; set; }
    
    [Required]
    [Column(TypeName = "decimal(18,2)")]
    public decimal BalanceAfter { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [ForeignKey(nameof(PlayerId))] 
    public Player Player { get; set; } = null!;
}