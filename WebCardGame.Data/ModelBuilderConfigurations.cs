using Microsoft.EntityFrameworkCore;
using WebCardGame.Data.DataEntities.Base;
using WebCardGame.Data.DataEntities.CardDataEntities;

namespace WebCardGame.Data
{
    public static class ModelBuilderConfigurations
    {

        public static void SetKeys(this ModelBuilder modelBuilder, Type type)
        {
            if (type.IsEquivalentTo(typeof(IBaseDataEntity<string>)))
            {
                var entity = modelBuilder.Entity<IBaseDataEntity<string>>();

                entity.HasKey(e => e.Id);
            }
            else
            {
                var entity = modelBuilder.Entity<IBaseDataEntity<int>>();

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
                .HasForeignKey(cde => cde.TypeId)
                .OnDelete(DeleteBehavior.SetNull);
        }

        public static void DeckDataEntityConfigurations(this ModelBuilder modelBuilder)
        {
            var entity = modelBuilder.Entity<DeckDataEntity>();

            entity
                .HasOne(dde => dde.Type)
                .WithMany(dtde => dtde.Decks)
                .HasForeignKey(nameof(DeckDataEntity.TypeId))
                .OnDelete(DeleteBehavior.SetNull);

            entity
                .HasOne(dde => dde.Creator)
                .WithMany(c => c.CreatedDecks)
                .HasForeignKey(dde => dde.CreatorId)
                .OnDelete(DeleteBehavior.SetNull);

            entity
                .HasMany(dde => dde.Users)
                .WithMany(u => u.OtherDecks)
                .UsingEntity<DeckDataEntity>();

            entity
                .HasMany(dde => dde.Cards)
                .WithOne(cde => cde.Deck)
                .HasForeignKey(cde => cde.DeckId)
                .OnDelete(DeleteBehavior.SetNull);
        }

        public static void DeckTypeDataEntityConfigurations(this ModelBuilder modelBuilder)
        {
            var entity = modelBuilder.Entity<DeckTypeDataEntity>();

            entity
                .HasMany(dtde => dtde.Decks)
                .WithOne(dde => dde.Type)
                .HasForeignKey(nameof(DeckDataEntity.TypeId));
        }

        public static void EffectDataEntityConfigurations(this ModelBuilder modelBuilder)
        {
            var entity = modelBuilder.Entity<EffectDataEntity>();

            entity
                .HasOne(ede => ede.Type)
                .WithMany(etde => etde.Effects)
                .HasForeignKey(nameof(EffectDataEntity.TypeId));

            entity
                .HasMany(ede => ede.CardDataEntities)
                .WithMany(cde => cde.Effects)
                .UsingEntity<CardDataEntity>();
        }

        public static void EffectTypeDataEntityConfigurations(this ModelBuilder modelBuilder)
        {
            var entity = modelBuilder.Entity<EffectTypeDataEntity>();

            entity
                .HasMany(etde => etde.Effects)
                .WithOne(ede => ede.Type)
                .HasForeignKey(nameof(EffectDataEntity.TypeId));
        }
    }
}
