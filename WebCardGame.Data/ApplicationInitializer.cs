using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebCardGame.Data.DataEntities.CardDataEntities;
using WebCardGame.Data.DataEntities.IdentityDataEntities;
using WebCardGame.Data.Repositories;
using WebCardGame.Data.Requests;

namespace WebCardGame.Data
{
    public class ApplicationInitializer
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<UserDataEntity> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IDeletableRepository<DeckTypeDataEntity> _deckTypeRepository;

        public ApplicationInitializer(ApplicationDbContext context, UserManager<UserDataEntity> userManager, RoleManager<IdentityRole> roleManager, IDeletableRepository<DeckTypeDataEntity> deckTypeRepository)
        {
            _context = context;
            _roleManager = roleManager;
            _userManager = userManager;
            _deckTypeRepository = deckTypeRepository;
        }

        public async Task InitializeAsync()
        {
            await ApplyMigrationsAsync();
            await SeedRolesAsync();
            await SeedUsersAsync();
            await SeedDeckTypesAsync();
        }

        private async Task ApplyMigrationsAsync()
        {
            var pendingMigrations = await _context.Database.GetPendingMigrationsAsync();

            if (pendingMigrations.Any())
            {
                await _context.Database.MigrateAsync();
            }
        }

        private async Task SeedRolesAsync()
        {
            if (await _context.Roles.AnyAsync())
            {
                return;
            }

            await _roleManager.CreateAsync(new IdentityRole("Admin"));
            await _roleManager.CreateAsync(new IdentityRole("Owner"));
            await _roleManager.CreateAsync(new IdentityRole("Artist"));
            await _roleManager.CreateAsync(new IdentityRole("Dev"));
        }

        private async Task SeedUsersAsync()
        {
            if (await _context.Users.AnyAsync())
            {
                return;
            }

            UserDataEntity adminUser = new()
            {
                Email = "admin@gmail.com",
                UserName = "admin@gmail.com",
            };

            await _userManager.CreateAsync(adminUser, "Admin123!");
            await _userManager.AddToRoleAsync(adminUser, "Admin");

            UserDataEntity regularUser = new()
            {
                Email = "user@gmail.com",
                UserName = "user@gmail.com",
            };

            await _userManager.CreateAsync(regularUser, "User123!");
        }

        private async Task SeedDeckTypesAsync()
        {
            if (await _context.DeckTypes.AnyAsync())
            {
                return;
            }

            DeckTypeDataEntity deckTypeData = new()
            {
                CreatedOn = DateTime.UtcNow,
                Description = "Deck type that makes a deck useable by all users",
                Name = "Public"
            };
            var request = new BaseDataRequest
            {
                Type = "Insert",
                Origin = nameof(ApplicationInitializer) + "SeedDeckTypesAsync",
                Payload = deckTypeData
            };

            await _deckTypeRepository.InsertAsync(request);
            deckTypeData = new DeckTypeDataEntity
            {
                CreatedOn = DateTime.UtcNow,
                Description = "Deck type that makes a deck useable only by its creator",
                Name = "Private"
            };
            request.Payload = deckTypeData;
            await _deckTypeRepository.InsertAsync(request);
            await _deckTypeRepository.SaveAsync();
        }
    }
}
