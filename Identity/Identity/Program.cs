using Identity.Authorization;
using Microsoft.AspNetCore.Authorization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddAuthentication("MyCookie").AddCookie("MyCookie",
                options => { options.Cookie.Name = "MyCookie";
                    options.AccessDeniedPath = "/AccesDenied";
                    options.ExpireTimeSpan=TimeSpan.FromSeconds(300);
                });
                

builder.Services.AddAuthorization(options => options.AddPolicy("HRdetection",
                                   policy => policy.RequireClaim("Department", "HR")
                                   .Requirements.Add(new AuthorizationRequirment(3))));

builder.Services.AddSingleton<IAuthorizationHandler, AuthorizationRequirmentHandeler>();

builder.Services.AddHttpClient("OurWebAPI", client =>
            {
                client.BaseAddress = new Uri("https://localhost:7188/");
            });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change
    // this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();


app.UseAuthentication();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
