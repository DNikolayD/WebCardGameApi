using Microsoft.EntityFrameworkCore;
using WebCardGame.Data.DataEntities.Base;
using WebCardGame.Data.DataEntities.CardDataEntities;

namespace WebCardGame.Data
{
    public static class ModelBuilderConfigurations
    {

        public static void SetKeys(this ModelBuilder modelBuilder, Type type)
        {
            if (type.IsEquivalentTo(typeof(IDeletableDataEntity<string>)))
            {
                var entity = modelBuilder.Entity<IDeletableDataEntity<string>>();

                entity.HasKey(e => e.Id);
            }
            else
            {
                var entity = modelBuilder.Entity<IDeletableDataEntity<int>>();

                entity.HasKey(e => e.Id);
            }
        }

        public static void CardDataEntityConfigurations(this ModelBuilder modelBuilder)
        {
            var entity = modelBuilder.Entity<CardDataEntity>();

            entity
                .HasOne(cde => cde.ImageDataEntity)
                .WithOne(ide => ide.CardDataEntity)
                .HasForeignKey(typeof(CardDataEntity), nameof(CardDataEntity.ImageId))
                .OnDelete(DeleteBehavior.Cascade);

            entity
                .HasOne(cde => cde.Type)
                .WithMany(ctde => ctde.CardDataEntities)
                .HasForeignKey(cde => cde.TypeId)
                .OnDelete(DeleteBehavior.Cascade);

            entity
                .HasMany(cde => cde.Effects)
                .WithMany(ede => ede.CardDataEntities)
                .UsingEntity<CardDataEntity>();

            entity
                .HasOne(cde => cde.Creator)
                .WithMany(ude => ude.CardDataEntities)
                .HasForeignKey(cde => cde.CreatorId)
                .OnDelete(DeleteBehavior.SetNull);

            entity
                .HasOne(cde => cde.Deck)
                .WithMany(dde => dde.Cards)
                .HasForeignKey(cde => cde.DeckId)
                .OnDelete(DeleteBehavior.SetNull);
        }

        public static void CardTypeDataEntityConfigurations(this ModelBuilder modelBuilder)
        {
            var entity = modelBuilder.Entity<CardTypeDataEntity>();

            entity
                .HasMany(ctde => ctde.CardDataEntities)
                .WithOne(cde => cde.Type)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
