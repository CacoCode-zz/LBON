using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using LBON.EntityFrameworkCore.Entities;
using LBON.Extensions;
using Microsoft.EntityFrameworkCore;

namespace LBON.EntityFrameworkCore.Repositories
{
    public class EfRepository<TDbContext, TEntity, TPrimaryKey> : IEfRepository<TDbContext, TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
        where TDbContext : DbContext
    {
        public virtual DbContext DbContext { private set; get; }

        public virtual DbSet<TEntity> Table => DbContext.Set<TEntity>();

        public EfRepository(DbContext dbContext)
        {
            DbContext = dbContext;
        }

        public virtual IQueryable<TEntity> GetAll(params Expression<Func<TEntity, object>>[] propertySelectors)
        {
            var query = Table.AsNoTracking();
            if (!propertySelectors.IsNullOrEmpty())
            {
                foreach (var propertySelector in propertySelectors)
                {
                    query = query.Include(propertySelector);
                }
            }
            return query;
        }

        public virtual IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> expression, params Expression<Func<TEntity, object>>[] propertySelectors)
        {
            var query = Table.Where(expression);
            if (!propertySelectors.IsNullOrEmpty())
            {
                foreach (var propertySelector in propertySelectors)
                {
                    query = query.Include(propertySelector);
                }
            }
            return query;
        }

        public virtual TEntity Find(TPrimaryKey id)
        {
            return Table.Find(id);
        }

        public virtual async Task<TEntity> FindAsync(TPrimaryKey id)
        {
            return await Table.FindAsync(id);
        }

        public virtual TEntity Get(Expression<Func<TEntity, bool>> expression, params Expression<Func<TEntity, object>>[] propertySelectors)
        {
            var query = GetAll();
            if (!propertySelectors.IsNullOrEmpty())
            {
                foreach (var propertySelector in propertySelectors)
                {
                    query = query.Include(propertySelector);
                }
                return query.FirstOrDefault(expression);
            }
            return Table.FirstOrDefault(expression);
        }

        public virtual async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> expression, params Expression<Func<TEntity, object>>[] propertySelectors)
        {
            var query = GetAll();
            if (!propertySelectors.IsNullOrEmpty())
            {
                foreach (var propertySelector in propertySelectors)
                {
                    query = query.Include(propertySelector);
                }
                return await query.FirstOrDefaultAsync(expression);
            }
            return await Table.FirstOrDefaultAsync(expression);
        }

        public virtual void Insert(TEntity entity, bool autoSave = true)
        {
            var hasCreationTime = typeof(IHasCreationTime).IsAssignableFrom(typeof(TEntity));
            if (hasCreationTime)
            {
                ((IHasCreationTime)entity).CreationTime = DateTime.Now;
            }
            ((IHasCreationTime)entity).CreationTime = DateTime.Now;
            Table.Add(entity);
            if (autoSave)
            {
                DbContext.SaveChanges();
            }
        }

        public virtual async Task InsertAsync(TEntity entity, bool autoSave = true)
        {
            var hasCreationTime = typeof(IHasCreationTime).IsAssignableFrom(typeof(TEntity));
            if (hasCreationTime)
            {
                ((IHasCreationTime)entity).CreationTime = DateTime.Now;
            }
            await Table.AddAsync(entity);
            if (autoSave)
            {
                await DbContext.SaveChangesAsync();
            }
        }

        public virtual async Task<TPrimaryKey> InsertAndGetIdAsync(TEntity entity, bool autoSave = true)
        {
            var hasCreationTime = typeof(IHasCreationTime).IsAssignableFrom(typeof(TEntity));
            if (hasCreationTime)
            {
                ((IHasCreationTime)entity).CreationTime = DateTime.Now;
            }
            await Table.AddAsync(entity);
            if (autoSave)
            {
                await DbContext.SaveChangesAsync();
            }
            return entity.Id;
        }

        public virtual void InsertList(List<TEntity> entities, bool autoSave = true)
        {
            var hasCreationTime = typeof(IHasCreationTime).IsAssignableFrom(typeof(TEntity));
            if (hasCreationTime)
            {
                foreach (var entity in entities)
                {
                    ((IHasCreationTime)entity).CreationTime = DateTime.Now;
                }
            }
            Table.AddRange(entities);
            if (autoSave)
            {
                DbContext.SaveChanges();
            }
        }

        public virtual async Task InsertListAsync(List<TEntity> entities, bool autoSave = true)
        {
            var hasCreationTime = typeof(IHasCreationTime).IsAssignableFrom(typeof(TEntity));
            if (hasCreationTime)
            {
                foreach (var entity in entities)
                {
                    ((IHasCreationTime)entity).CreationTime = DateTime.Now;
                }
            }
            await Table.AddRangeAsync(entities);
            if (autoSave)
            {
                await DbContext.SaveChangesAsync();
            }
        }

        public virtual void Update(TEntity entity, bool autoSave = true)
        {
            var hasLastModificationTime = typeof(IHasLastModificationTime).IsAssignableFrom(typeof(TEntity));
            if (hasLastModificationTime)
            {
                ((IHasLastModificationTime)entity).LastModificationTime = DateTime.Now;
            }
            Table.Update(entity);
            if (autoSave)
            {
                DbContext.SaveChanges();
            }
        }

        public virtual async Task UpdateAsync(TEntity entity, bool autoSave = true)
        {
            var hasLastModificationTime = typeof(IHasLastModificationTime).IsAssignableFrom(typeof(TEntity));
            if (hasLastModificationTime)
            {
                ((IHasLastModificationTime)entity).LastModificationTime = DateTime.Now;
            }
            Table.Update(entity);
            if (autoSave)
            {
                await DbContext.SaveChangesAsync();
            }
        }

        public virtual void UpdateList(IEnumerable<TEntity> entities)
        {
            Table.UpdateRange(entities);
            DbContext.SaveChanges();
        }

        public virtual async Task UpdateListAsync(IEnumerable<TEntity> entities)
        {
            Table.UpdateRange(entities);
            await DbContext.SaveChangesAsync();
        }

        public virtual void Delete(TPrimaryKey id, bool autoSave = true)
        {
            var entity = Find(id);
            if (entity == null)
            {
                throw new ArgumentNullException();
            }
            Delete(entity, autoSave);
        }

        public virtual async Task DeleteAsync(TPrimaryKey id, bool autoSave = true)
        {
            var entity = await FindAsync(id);
            if (entity == null)
            {
                throw new ArgumentNullException();
            }
            await DeleteAsync(entity, autoSave);
        }

        public virtual void Delete(TEntity entity, bool autoSave = true)
        {
            var hasDeletionTime = typeof(IHasDeletionTime).IsAssignableFrom(typeof(TEntity));
            if (hasDeletionTime)
            {
                ((IHasDeletionTime)entity).DeletionTime = DateTime.Now;
            }
            var isDeleteEntity = typeof(ISoftDelete).IsAssignableFrom(typeof(TEntity));
            if (isDeleteEntity)
            {
                ((ISoftDelete)entity).IsDeleted = true;
            }
            Update(entity, autoSave);
        }

        public virtual async Task DeleteAsync(TEntity entity, bool autoSave = true)
        {
            var hasDeletionTime = typeof(IHasDeletionTime).IsAssignableFrom(typeof(TEntity));
            if (hasDeletionTime)
            {
                ((IHasDeletionTime)entity).DeletionTime = DateTime.Now;
            }
            var isDeleteEntity = typeof(ISoftDelete).IsAssignableFrom(typeof(TEntity));
            if (isDeleteEntity)
            {
                ((ISoftDelete)entity).IsDeleted = true;
            }
            await UpdateAsync(entity, autoSave);
        }

        public virtual void HardDelete(TPrimaryKey id, bool autoSave = true)
        {
            var entity = Find(id);
            if (entity == null)
            {
                throw new ArgumentNullException();
            }
            HardDelete(entity, autoSave);
        }

        public async Task HardDeleteAsync(TPrimaryKey id, bool autoSave = true)
        {
            var entity = await FindAsync(id);
            if (entity == null)
            {
                throw new ArgumentNullException();
            }
            await HardDeleteAsync(entity, autoSave);
        }

        public virtual void HardDelete(TEntity entity, bool autoSave = true)
        {
            Table.Remove(entity);
            if (autoSave)
            {
                DbContext.SaveChanges();
            }
        }

        public virtual async Task HardDeleteAsync(TEntity entity, bool autoSave = true)
        {
            Table.Remove(entity);
            if (autoSave)
            {
                await DbContext.SaveChangesAsync();
            }
        }
    }
}
