using ChalkboardChat.Data.Database;
using ChalkboardChat.Data.Database.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddRazorPages(options => options.Conventions.AuthorizeFolder("/Admin"));

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Index";
});


var authConnectionString = builder.Configuration.GetConnectionString("AuthConnection");
var appConnectionString = builder.Configuration.GetConnectionString("AppConnection");


builder.Services.AddDbContext<AuthDbContext>(options => options.UseSqlServer(authConnectionString));
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(appConnectionString));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<AuthDbContext>();

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
