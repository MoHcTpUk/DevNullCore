using DevNullCore.Domain.Entitity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DevNullCore.EF.Infrastructure.Repository
{
    public interface IRepository<TEntity> where TEntity : class, IBaseEntity
    {
        public IEnumerable<TEntity> Find(Func<TEntity, bool> predicate);
        public Task<TEntity> Create(TEntity item);
        public Task<TEntity> Update(TEntity item);
        public Task Delete(TEntity item);
    }
}