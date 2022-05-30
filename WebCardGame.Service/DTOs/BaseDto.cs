using WebCardGame.Common;

namespace WebCardGame.Service.DTOs
{
    public class BaseDto : IBaseEntity
    {
        public bool IsActive { get; set; }
        public bool IsModified { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? LastModifiedOn { get; set; }
        public DateTime? DeletedOn { get; set; }

        public BaseDto()
        {
            IsActive = true;
            IsModified = false;
            CreatedOn = DateTime.UtcNow;
            LastModifiedOn = null;
            DeletedOn = null;
        }
    }
}
