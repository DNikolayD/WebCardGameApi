using System.ComponentModel.DataAnnotations;
using WebCardGame.Data.DataEntities.Base;
using WebCardGame.Data.DataEntities.IdentityDataEntities;

namespace WebCardGame.Data.DataEntities.CardDataEntities
{
    public class DeckDataEntity : IDeletableDataEntity<object>
    {
        [Key]
        public string Id { get; set; }

        public string Name { get; set; }

        public int TypeId { get; set; }

        public DeckTypeDataEntity Type { get; set; }

        public string CreatorId { get; set; }

        public UserDataEntity Creator { get; set; }

        public List<UserDataEntity> Users { get; set; }

        public List<CardDataEntity> Cards { get; set; }
        object IDeletableDataEntity<object>.Id { get; set; }
        public bool IsActive { get; set; }
        public bool IsModified { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? LastModifiedOn { get; set; }
        public DateTime? DeletedOn { get; set; }
        public string? ModifiedOnId { get; set; }

        public DeckDataEntity()
        {
            this.Id = Guid.NewGuid().ToString();
            this.IsActive = true;
            this.CreatedOn = DateTime.UtcNow;
        }

    }
}
