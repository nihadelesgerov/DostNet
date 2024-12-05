using AspNetCoreRateLimit;
using DostNetProject.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();



// RATE LIMITING TO AVOID DOS , BRUTE FORCE 
//builder.Services.AddInMemoryRateLimiting();
//builder.Services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
//builder.Services.Configure<IpRateLimitOptions>(options =>
//{
//    options.EnableEndpointRateLimiting = true;
//    options.StackBlockedRequests = true;
//    options.GeneralRules = new List<RateLimitRule>()
//    {
//        new RateLimitRule
//        {
//            Endpoint = "Get:/Account/*",
//            Period = "1h",
//            Limit = 300
//        }
//    };
//});



builder.Services.AddDbContext<DostNetDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DostNetDbConnection"));
});

builder.Services.AddIdentity<DostNetUser, IdentityRole>().AddEntityFrameworkStores<DostNetDbContext>().AddDefaultTokenProviders();
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/oAuth/SignIn";
    options.LogoutPath = "/oAuth/LogOut";
    options.AccessDeniedPath = "/Main/Notfound";
    options.ExpireTimeSpan = TimeSpan.FromDays(5);
    options.AccessDeniedPath = "/oAuth/AccesDenied";
    options.SlidingExpiration = true;
    options.Cookie.SameSite = SameSiteMode.Lax;
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    options.Cookie.HttpOnly = true;
});
builder.Services.AddAuthentication().AddGoogle(options =>
{
    options.ClientId = builder.Configuration["Authentication:Google:ClientId"];
    options.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"];
}).AddCookie();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseStatusCodePages( async options =>
{
    var response = options.HttpContext.Response;
    if (response.StatusCode == 405 || response.StatusCode == 404)
    {
        response.Redirect("/Main/Notfound");
    }
});
app.UseAuthorization();
app.UseAuthentication();
app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Main}/{action=HomePage}/{id?}")
    .WithStaticAssets();


app.Run();
