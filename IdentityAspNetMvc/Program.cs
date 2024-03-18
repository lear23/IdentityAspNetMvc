using Infrastructure.Contexts;
using Infrastructure.Entities;
using Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AppDbContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("LocalDatabase")));

//-------------------------------------------------

builder.Services.AddDefaultIdentity<UserEntity>(x =>
{
    x.User.RequireUniqueEmail = true;
    x.SignIn.RequireConfirmedAccount = false;
    x.Password.RequiredLength = 8;
}).AddEntityFrameworkStores<AppDbContext>();

//builder.Services.AddIdentity<UserEntity, IdentityRole>(options =>
//{
//    options.User.RequireUniqueEmail = true;
//    options.SignIn.RequireConfirmedEmail = false;
//    options.Password.RequiredLength = 8;
//})
//.AddEntityFrameworkStores<AppDbContext>();
//-------------------------------------------



builder.Services.ConfigureApplicationCookie(x =>
{
    x.LoginPath = "/signin";
    x.LogoutPath = "/signout";
    x.AccessDeniedPath = "/denied";

    x.Cookie.HttpOnly = true;
    x.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    x.ExpireTimeSpan = TimeSpan.FromMinutes(60);
    x.SlidingExpiration = true;
});


builder.Services.AddAuthentication()
    .AddFacebook(x =>
    {
        x.AppId = "296787203435897";
        x.AppSecret = "244f04473c4dd6487454ad7c8e5c46d9";
        x.Fields.Add("first_name");
        x.Fields.Add("last_name");
    })
    .AddGoogle(options =>
    {
        options.ClientId = "377115093634-c3cj5pfqgjf9r25hn4k1afoq5e60vjut.apps.googleusercontent.com";
        options.ClientSecret = "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";
    });



//builder.Services.AddAuthentication().AddFacebook(x =>
//{
//    x.AppId = "296787203435897";
//    x.AppSecret = "244f04473c4dd6487454ad7c8e5c46d9";
//    x.Fields.Add("first_name");
//    x.Fields.Add("last_name");


//});




builder.Services.AddScoped<AddressServices>();

var app = builder.Build();
app.UseHsts();
app.UseStatusCodePagesWithReExecute("/Home/Error404", "?satusCode={0}");
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Home}/{id?}");

app.Run();



