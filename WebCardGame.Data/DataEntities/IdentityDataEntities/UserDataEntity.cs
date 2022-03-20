using Microsoft.AspNetCore.Identity;
using WebCardGame.Data.DataEntities.Base;

namespace WebCardGame.Data.DataEntities
{
    public class UserDataEntity : IdentityUser, IDeletableDataEntity<object>
    {
        public bool IsActive { get; set; }

        public bool IsModified { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? LastModifiedOn { get; set; }

        public DateTime? DeletedOn { get; set; }

        public string? ModifiedOnId { get; set; }

        public string ImageId { get; set; }

        public virtual ImageDataEntity ImageDataEntity { get; set; }
        object IDeletableDataEntity<object>.Id { get; set; }
    }
}
