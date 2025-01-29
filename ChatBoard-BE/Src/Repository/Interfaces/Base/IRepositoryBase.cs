using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces.Base
{
    public interface IRepositoryBase<TEntity>
    {
        IQueryable<TEntity> FindAll();
        IQueryable<TEntity> FindByCondition(Expression<Func<TEntity, bool>> expression);
        IQueryable<TEntity> FindByConditionWithTracking(Expression<Func<TEntity, bool>> expression);

        void Create(TEntity entity);
        void CreateMultiple(List<TEntity> entities);
        void Update(TEntity entity);
        void AddRange(List<TEntity> entities);
        void UpdateRange(List<TEntity> entities);
        void DeleteRange(List<TEntity> entities);
        void Delete(TEntity entity);

    }
}
