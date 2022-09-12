using WebCardGame.Api.Extensions;
using WebCardGame.Common.Configuration;

var builder = WebApplication.CreateBuilder(args);
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
var services = builder.Services;
var configuration = builder.Configuration;

var authOptions = configuration.GetSection(nameof(AuthOptions)).Get<AuthOptions>();

services.PipeLine(configuration, authOptions);

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


/*
Space for global comments and TODO items outside of ReadMe.txt
Fixes: 
TODOs:
Maybe add different cost types later
Add commonalities between cards(attributes, families ets)
Add Active/Inactive status on the effects
Comments:
I use extensions instead of AutoMapper so I can control and observe the process of mapping and regulate the more complicated cases
All Card Types have soft deletion because the cards that users create are going to be owned by all users collectively as this app is created or entertainment and has no intentions to profit of any data or art what so ever
*/