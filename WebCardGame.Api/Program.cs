using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using WebCardGame.Api.Extensions;
using WebCardGame.Common.Configuration;
using WebCardGame.Data;
using WebCardGame.Data.DataEntities.IdentityDataEntities;
using WebCardGame.Data.Repositories;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;

var authOptions = configuration.GetSection(nameof(AuthOptions)).Get<AuthOptions>();
services.AddSingleton(authOptions);

services.AddControllers();
services.AddIdentity<UserDataEntity, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();
services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")).LogTo(message => File.AppendAllText(Path.Combine(Assembly.GetExecutingAssembly().Location, @"..\..\..\..\logs.txt"), message)));
services.AddScoped(typeof(IDeletableRepository<>), typeof(DeletableRepository<>));
services.AddServices();
services.AddScoped<ApplicationInitializer>();
services.AddEndpointsApiExplorer();
services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebCardGame.Api", Version = "v1" });
    c.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Description = "Please enter JWT with Bearer into field"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = JwtBearerDefaults.AuthenticationScheme
                            }
                        },
                        new List<string>()
                    }
                });
});
services
    .AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(configuration =>
    {
        configuration.RequireHttpsMetadata = false;
        configuration.TokenValidationParameters = new TokenValidationParameters
        {
            IssuerSigningKey = new SymmetricSecurityKey(authOptions.SecurityKeyAsBytes),
            ValidateAudience = true,
            ValidAudience = authOptions.Audience,
            ValidateIssuer = true,
            ValidIssuer = authOptions.Issuer,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ClockSkew = TimeSpan.Zero
        };
    });

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseAuthentication();

app.MapControllers();

await app.InitializeApplication();
await app.StartAsync();


/// Space for global comments and TODO items outside of ReadMe.txt
/// Fixes: 
/// Find a better way to inherit the generic entity for the generic repository in the classes Repository and Deletable Repository
/// Find how to mock the db
/// TODOs:
/// maybe add diffrent cost types later
/// add commonalities between cards(atrbutes, families ets)
/// add Active/Inactive status on the effects
/// Comments:
/// I use extentions instead of AutoMapper so I can control and observe the process of mapping and regulate the more complicated cases
/// All Card Types have soft deletion because the cards that users create are going to be owened by all users collectivlly as this app is created or entartaiment and has no intentions to profit of any data or art what so ever