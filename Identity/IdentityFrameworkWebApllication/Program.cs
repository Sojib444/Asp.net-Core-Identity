using IdentityFrameworkWebApllication.DbContext;
using IdentityFrameworkWebApllication.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionstring = builder.Configuration.GetConnectionString("DefaultConnection");
Console.WriteLine(connectionstring);

// Add services to the container.
builder.Services.AddRazorPages();

//confihgure DbContext

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(connectionstring);
});

//set Identity
builder.Services.AddIdentity<IdentityUser, IdentityRole>(option =>
{
    option.Password.RequiredLength = 8;
    option.Password.RequireUppercase = true;
    option.Password.RequireLowercase = true;

    option.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);
    option.Lockout.MaxFailedAccessAttempts = 10;

    option.User.RequireUniqueEmail = true;
}).AddEntityFrameworkStores<ApplicationDbContext>()
  .AddDefaultTokenProviders();


//set cookie
builder.Services.ConfigureApplicationCookie(option =>
{
    option.LoginPath = "/Account/Login";
    option.AccessDeniedPath = "/Account/AccesDenied";
});

builder.Services.Configure<EmailService>(builder.Configuration.GetSection("STPM"));
builder.Services.AddSingleton<ISendEmail,SendEmail>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();
