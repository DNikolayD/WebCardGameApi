using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using WebCardGame.Data.DataEntities.Base;
using WebCardGame.Data.DataEntities.CardDataEntities;
using WebCardGame.Data.DataEntities.IdentityDataEntities;

namespace WebCardGame.Data
{
    public class ApplicationDbContext : IdentityDbContext<UserDataEntity>
    {

        public DbSet<CardDataEntity> Cards { get; set; }

        public DbSet<CardTypeDataEntity> CardTypes { get; set; }

        public DbSet<DeckDataEntity> Decks { get; set; }

        public DbSet<DeckTypeDataEntity> DeckTypes { get; set; }

        public DbSet<EffectDataEntity> Effects { get; set; }

        public DbSet<EffectTypeDataEntity> EffectTypes { get; set; }

        public DbSet<ImageDataEntity> Images { get; set; }



        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
            
        }

        public ApplicationDbContext()
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            foreach (var type in typeof(ApplicationDbContext).GetProperties().Where(p => p.Name.EndsWith("s")).Select(x => x.GetType()))
            {
                builder.SetKeys(type);
            }
            builder.CardDataEntityConfigurations();
            builder.CardTypeDataEntityConfigurations();
            base.OnModelCreating(builder);
        }
    }
}
