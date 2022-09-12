using System.ComponentModel.DataAnnotations;

namespace WebCardGame.Data.DataEntities.Base
{
    public abstract class DataEntityWithIntKey : IBaseDataEntity<int>
    {
        [Key]
        public int Id { get; set; }
        public bool IsActive { get; set; }
        public bool IsModified { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? LastModifiedOn { get; set; }
        public DateTime? DeletedOn { get; set; }

        protected DataEntityWithIntKey()
        {
            IsActive = true;
            IsModified = false;
            CreatedOn = DateTime.UtcNow;
        }
    }
}
