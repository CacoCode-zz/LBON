using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using LBON.EntityFrameworkCore.Entities;

namespace LBON.EntityFrameworkCore.Repositories
{
    public interface IEfRepository<TDbContext, TEntity, TPrimaryKey> where TEntity : class, IEntity<TPrimaryKey>
    {
        IQueryable<TEntity> GetAll(params Expression<Func<TEntity, object>>[] propertySelectors);

        IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> expression,
            params Expression<Func<TEntity, object>>[] propertySelectors);

        TEntity Find(TPrimaryKey id);

        Task<TEntity> FindAsync(TPrimaryKey id);

        TEntity Get(Expression<Func<TEntity, bool>> expression,
            params Expression<Func<TEntity, object>>[] propertySelectors);

        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> expression,
            params Expression<Func<TEntity, object>>[] propertySelectors);

        void Insert(TEntity entity, bool autoSave = true);

        Task InsertAsync(TEntity entity, bool autoSave = true);

        Task<TPrimaryKey> InsertAndGetIdAsync(TEntity entity, bool autoSave = true);

        void InsertList(List<TEntity> entities, bool autoSave = true);

        Task InsertListAsync(List<TEntity> entities, bool autoSave = true);

        void Update(TEntity entity, bool autoSave = true);

        Task UpdateAsync(TEntity entity, bool autoSave = true);

        void UpdateList(IEnumerable<TEntity> entities);

        Task UpdateListAsync(IEnumerable<TEntity> entities);

        void Delete(TPrimaryKey id, bool autoSave = true);

        Task DeleteAsync(TPrimaryKey id, bool autoSave = true);

        void Delete(TEntity entity, bool autoSave = true);

        Task DeleteAsync(TEntity entity, bool autoSave = true);

        void HardDelete(TPrimaryKey id, bool autoSave = true);

        Task HardDeleteAsync(TPrimaryKey id, bool autoSave = true);

        void HardDelete(TEntity entity, bool autoSave = true);

        Task HardDeleteAsync(TEntity entity, bool autoSave = true);
    }
}
