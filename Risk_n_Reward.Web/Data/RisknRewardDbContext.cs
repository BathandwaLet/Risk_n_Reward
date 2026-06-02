using Microsoft.EntityFrameworkCore;
using Risk_n_Reward.Web.Models;

namespace Risk_n_Reward.Web.Data;

public class RisknRewardDbContext : DbContext
{
    public RisknRewardDbContext(DbContextOptions<RisknRewardDbContext> options):base(options)
    { }
    
    public DbSet<RisknRewardDbContext> RisknRewardDb { get;set; }
}