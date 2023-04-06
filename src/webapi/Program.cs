using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Configuration;
using Microsoft.Extensions.Configuration;

namespace webapi;

public class Program
{
    public static void Main(string[] args)
    {

        var builder = WebApplication.CreateBuilder(args);

        var tokenKey = builder.Configuration.GetValue<string>("TokenKey");
        var key = Encoding.ASCII.GetBytes(tokenKey);

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddCors();
        builder.Services.AddSwaggerGen();
        builder.Services.AddDbContext<KitchHubDbContext> (options =>
        {
            options.UseSqlite(@"Data Source=KitchHub.db;Cache=Shared;");

        });

        builder.Services.AddDbContext<UsersKitchHubDbContext>(options =>
        {
            options.UseSqlite(@"Data Source=UsersKitchHub.db;Cache=Shared;");
        });

        builder.Services.AddAuthentication(conf =>
        {
            conf.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            conf.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(x =>
        {
            x.RequireHttpsMetadata = false;
            x.SaveToken = true;
            x.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false
            };
        });
        builder.Services.AddSingleton<IJWTAuthenticationManager>(new JWTAuthenticationManager(tokenKey));

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        else
        {
            app.UseDefaultFiles();
            app.UseStaticFiles();
        }

        app.UseCors(builder => builder.AllowAnyOrigin()
                                      .AllowAnyMethod());

        app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}