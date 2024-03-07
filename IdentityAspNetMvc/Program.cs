using Infrastructure.Contexts;
using Infrastructure.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AppDbContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("LocalDatabase")));
//builder.Services.AddDefaultIdentity<UserEntity>(x =>
//{
//    x.User.RequireUniqueEmail = true;
//    x.SignIn.RequireConfirmedEmail = false;
//    x.Password.RequiredLength = 8;
//}).AddEntityFrameworkStores<AppDbContext>();
builder.Services.AddIdentity<UserEntity, IdentityRole>(options =>
{
    options.User.RequireUniqueEmail = true;
    options.SignIn.RequireConfirmedEmail = false;
    options.Password.RequiredLength = 8;
})
.AddEntityFrameworkStores<AppDbContext>();




var app = builder.Build();
app.UseHsts();
app.UseStatusCodePagesWithReExecute("/error", "?satusCode={0}");
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Home}/{id?}");

app.Run();



