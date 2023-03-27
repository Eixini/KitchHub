using Microsoft.EntityFrameworkCore;

namespace webapi;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddCors();
        builder.Services.AddSwaggerGen();
        builder.Services.AddDbContext<KitchHubDbContext>(options =>
        {
            options.UseSqlite(@"Data Source=KitchHub.db;Cache=Shared;");
        });

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

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}