using System.ComponentModel.DataAnnotations;

namespace WebCardGame.Data.DataEntities.Base
{
    public class ImageDataEntity : IDeletableDataEntity<string>
    {
        public string Id { get; set; }

        public bool IsActive { get; set; }

        public bool IsModified { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? LastModifiedOn { get; set; }

        public DateTime? DeletedOn { get; set; }

        public string? ModifiedOnId { get; set; }

        [Required]
        public string ImagePath { get; set; }

        public ImageDataEntity()
        {
            this.Id = Guid.NewGuid().ToString();
            this.ImagePath = String.Empty;
            this.IsActive = true;
            this.CreatedOn = DateTime.UtcNow;
        }
    }
}
