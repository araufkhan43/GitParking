using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.AzureAD.UI;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.Resource;
using Parking.Business.Business.Interface;
using Parking.Business.Business;
using Parking.Repository.Repository.Interface;
using Parking.Repository.Repository;
using Parking.Repository.Context;
using Microsoft.EntityFrameworkCore;

namespace ParkingApp.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
           
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddMicrosoftIdentityWebApi(builder.Configuration);
            builder.Services.AddTransient<IAccountBusiness, AccountBusiness>();
            builder.Services.AddTransient<IAccountRepository, AccountRepository>();
            builder.Services.AddDbContext<ParkingContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("Con"), b => b.MigrationsAssembly(typeof(ParkingContext).Assembly.FullName));
                //options.usesq\(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            builder.Services.AddTransient<IParkingZoneBusiness, ParkingZoneBusiness>();
            builder.Services.AddTransient<IParkingZoneRepository, ParkingZoneRepository>();
            //    builder.Services.AddAuthentication(AzureADDefaults.BearerAuthenticationScheme)
            //       .AddAzureADBearer(options => builder.Configuration.Bind("AzureAd", options));
            //    builder.Services.AddMicrosoftIdentityWebAppAuthentication(builder.Configuration).EnableTokenAcquisitionToCallDownstreamApi(new string[] { builder.Configuration["APIConfig:APIScope"] }).AddInMemoryTokenCaches();

            //    builder.Services.Configure<JwtBearerOptions>(AzureADDefaults.JwtBearerAuthenticationScheme, options =>
            //    {
            //        options.Authority += "/v2.0";
            //        options.Audience = "api://049db7ac-763c-4309-93db-59f6c95caa12";
            //        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
            //        {
            //            ValidateIssuer = false
            //    };
            //});

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            //var policyName = "_myAllowSpecificOrigins";
            //builder.Services.AddCors(options =>
            //{
            //    options.AddPolicy(name:policyName,
            //        builder =>
            //        {
            //            builder
            //            .AllowAnyOrigin()
            //            .AllowAnyMethod()
            //            .AllowAnyHeader();
            //        });
            //});

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            // app.UseCors(policyName);
            app.UseCors(options =>
            {
                options.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin();
            });
            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}






































