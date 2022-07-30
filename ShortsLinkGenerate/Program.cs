using ContextDatabase;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ShortsLink.Core.Interface;
using ShortsLink.Core.Repository;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpContextAccessor();
builder.Services.AddDbContext<ShortsLinkContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("ShortsLinkContext") ?? throw new InvalidOperationException("Connection string 'ShortsLinkContext' not found."), o => o.UseNetTopologySuite()
        )
    );

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IGenerateRepository,GenerateRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Generate}/{action=GenerateForm}/{id?}");

app.MapControllerRoute(
    name: "special",
    pattern: "{shortUrl}",
    defaults: new { controller = "Generate", action = "GoToURL"});

app.Run();

