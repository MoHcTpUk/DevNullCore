using DevNullCore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevNullCore.EF.Infrastructure.Repository
{
    public abstract class AbstractRepository<TEntity> : IRepository<TEntity> where TEntity : class, IBaseEntity
    {
        private readonly DbContext _context;

        protected AbstractRepository(DbContext context)
        {
            _context = context;
        }

        public Task<IEnumerable<TEntity>> Find(Func<TEntity, bool> predicate)
        {
            return new Task<IEnumerable<TEntity>>(() => _context.Set<TEntity>().AsNoTracking().AsEnumerable().Where(predicate));
        }

        public async Task<TEntity> Create(TEntity entity)
        {
            await _context.Set<TEntity>().AddAsync(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<TEntity> Update(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task Delete(TEntity entity)
        {
            var entityToDelete = _context.Set<TEntity>().FirstOrDefault(_ => _ == entity);

            if (entityToDelete is null)
                throw new Exception("Entity not found");

            _context.Set<TEntity>().Remove(entityToDelete);
            await _context.SaveChangesAsync();
        }
    }
}