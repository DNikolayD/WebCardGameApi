﻿using System.ComponentModel.DataAnnotations;

namespace WebCardGame.Data.DataEntities.Base
{
    public abstract class DataEntityWithStringKey : IDeletableDataEntity<string>
    {
        [Key]
        public string Id { get; set; }
        public bool IsActive { get; set; }
        public bool IsModified { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? LastModifiedOn { get; set; }
        public DateTime? DeletedOn { get; set; }
        public string? ModifiedOnId { get; set; }

        public DataEntityWithStringKey()
        {
            this.Id = Guid.NewGuid().ToString();
            IsActive = true;
            CreatedOn = DateTime.UtcNow;
        }
    }
}