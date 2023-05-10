

using Parking.Repository.Context;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Parking.Business.Business.Interface;
using Parking.Business.Business;
using Parking.Repository.Repository.Interface;
using Parking.Repository.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using System.Configuration;
using ParkingApp;
using Microsoft.Identity.Web;

//using Microsoft.AspNetCore.SpaServices.AngularCli;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ParkingContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Con"), b => b.MigrationsAssembly(typeof(ParkingContext).Assembly.FullName));
    //options.usesq\(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddTransient<IAccountBusiness, AccountBusiness>();
builder.Services.AddTransient<IAccountRepository, AccountRepository>();
builder.Services.AddTransient<IParkingZoneBusiness, ParkingZoneBusiness>();
builder.Services.AddTransient<IParkingZoneRepository, ParkingZoneRepository>();

builder.Services.AddHttpClient<ITokenServce, TokenService>();
builder.Services.AddMicrosoftIdentityWebAppAuthentication(builder.Configuration).EnableTokenAcquisitionToCallDownstreamApi(new string[] { builder.Configuration["APIConfig:APIScope"] }).AddInMemoryTokenCaches();
builder.Services.AddControllersWithViews(options =>
{
    var policy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();
    options.Filters.Add(new AuthorizeFilter(policy));
});
builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
builder.Services.AddSingleton<IFileProvider>(
new PhysicalFileProvider(
Path.Combine(Directory.GetCurrentDirectory(),"wwwroot")));

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

app.UseAuthentication();
app.UseAuthorization();
app.UseEndpoints(endpoints => {
    app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Dashboard}/{action=Index}/{id?}");
    });


app.Run();
