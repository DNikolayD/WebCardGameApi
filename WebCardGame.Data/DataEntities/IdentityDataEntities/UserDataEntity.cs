﻿using Microsoft.AspNetCore.Identity;
using WebCardGame.Data.DataEntities.Base;
using WebCardGame.Data.DataEntities.CardDataEntities;

namespace WebCardGame.Data.DataEntities.IdentityDataEntities
{
    public class UserDataEntity : IdentityUser, IDeletableDataEntity<string>
    {
        public bool IsActive { get; set; }

        public bool IsModified { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? LastModifiedOn { get; set; }

        public DateTime? DeletedOn { get; set; }

        public string? ModifiedOnId { get; set; }

        public string ImageId { get; set; }

        public virtual ImageDataEntity ImageDataEntity { get; set; }

        public virtual ICollection<CardDataEntity> CardDataEntities { get; set; }

        public UserDataEntity()
        {
            ImageDataEntity = new ImageDataEntity();
            CardDataEntities = new List<CardDataEntity>();
        }
    }
}
