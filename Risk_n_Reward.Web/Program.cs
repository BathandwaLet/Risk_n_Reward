using Risk_n_Reward.Web.Data;
using Microsoft.EntityFrameworkCore;
using Risk_n_Reward.Web.Hub;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();//.AddSessionStateTempDataProvider();
builder.Services.AddDbContext<ApplicationDbContext>(options => 
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnectionString")));
builder.Services.AddSignalR();
//builder.Services.AddSession();
//builder.Services.AddDistributedMemoryCache();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
//app.UseSession();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();
app.MapHub<MultiplierHub>("/multiplierHub");
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();