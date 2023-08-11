using LaptopShop.Data;
using LaptopShop.Models;
using LaptopShop.Models.ViewModels;
using LaptopShop.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

builder.Services.AddDbContext<LaptopDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer_Hoang"));
});
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<LaptopDbContext>()
    .AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(options => options.LoginPath = "/Identity/UserAuthentication/Login");

//add services to container
builder.Services.AddScoped<IUserAuthenticationService, UserAuthenticationService>();

builder.Services.AddSession(options =>
{
	// Set a short timeout for easy testing.
	options.IdleTimeout = TimeSpan.FromMinutes(30);
	//options.Cookie.HttpOnly = true;
	// Make the session cookie essential
	options.Cookie.IsEssential = true;
});

var app = builder.Build();

// seed data (bảng Category và Product)
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    SeedData.Initialize(services);
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();
app.UseRouting();
app.UseAuthorization();
app.UseAuthentication();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
      name: "areas",
      pattern: "{area=Customer}/{controller=Home}/{action=Index}/{id?}"
    );
});

app.Run();
