using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SP24ClassLibraryDonham;
using SP24MVCDonham.Data;
using SP24MVCDonham.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString,
    sqlOptionsBuilder => sqlOptionsBuilder.EnableRetryOnFailure()));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<AppUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();

builder.Services.AddTransient<IAirlineRepo, AirlineRepo>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    //3 services: 1. Access database, 2. Create Roles, 3. Create Users
    var services = scope.ServiceProvider;
    try
    {
        InitialDatabase.InitializeDatabase(services);
    }
    catch (Exception serviceException)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(serviceException, "Error occured while populating database");
    }
}//end scope

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
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
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
