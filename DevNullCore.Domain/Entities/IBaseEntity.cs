using System;

namespace DevNullCore.Domain.Entities
{
    public interface IBaseEntity<T>: IBaseEntity
    {
        new T Id { get; set; }
    }

    public interface IBaseEntity
    {
        object Id { get; set; }
        DateTime CreatedDate { get; set; }
        DateTime? ModifiedDate { get; set; }
        DateTime? DeletedDate { get; set; }
    }
}