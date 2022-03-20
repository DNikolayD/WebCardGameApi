using WebCardGame.Data;

namespace WebCardGame.Api.Extantions
{
    public static class HostExtensions
    {
        public static async Task InitializeApplication(this IHost host)
        {
            using var scope = host.Services.CreateScope();
            var databaseInitializer = scope.ServiceProvider.GetRequiredService<ApplicationInitializer>();

            await databaseInitializer.InitializeAsync();
        }
    }
}
