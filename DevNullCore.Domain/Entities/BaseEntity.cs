using System;
using System.ComponentModel.DataAnnotations;

namespace DevNullCore.Domain.Entities
{
    public abstract class BaseEntity<T> : IBaseEntity<T>
    {
        [Key]
        public virtual T Id { get; set; }

        object IBaseEntity.Id
        {
            get => Id;
            set => Id = (T)value;
        }

        [DataType(DataType.DateTime)]
        public DateTime CreatedDate { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? ModifiedDate { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? DeletedDate { get; set; }
    }
}