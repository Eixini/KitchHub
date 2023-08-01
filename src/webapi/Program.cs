using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;
using webapi;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddCors();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v0.1", new OpenApiInfo
    {
        Version = "v0.1",
        Title = "KitchHub",
        Description = "Recipe search service",
    });
    options.ResolveConflictingActions(ApiDescription => ApiDescription.First());
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[]{}
        }
    });

    var xmlFileName = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFileName));
});
builder.Services.AddDbContext<KitchHubDbContext> (options =>
{
    options.UseSqlite(builder.Configuration["ConnectionStrings:KitchHubDB"]);

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
        ValidIssuer = builder.Configuration["JwtToken:Issuer"],
        ValidAudience = builder.Configuration["JwtToken:Audience"],
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtToken:SecretKey"])),
        ValidateIssuer = true,
        ValidateAudience = true
    };
});
builder.Services.AddSingleton<IJwtAuthenticationManager>(
    new JwtAuthenticationManager(builder.Configuration.GetSection("JwtToken"))
    );
builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v0.1/swagger.json", "v0.1");
    });
}
else
{
    app.UseDefaultFiles();
    app.UseStaticFiles();
    app.UseHttpsRedirection();
}

app.UseCors(builder => builder.AllowAnyOrigin()
                              .AllowAnyMethod()
                              .AllowAnyHeader()
                              );
app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

app.Run();