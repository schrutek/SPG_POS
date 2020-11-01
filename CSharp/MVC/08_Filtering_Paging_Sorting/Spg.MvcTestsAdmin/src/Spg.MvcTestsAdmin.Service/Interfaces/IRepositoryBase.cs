using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Spg.MvcTestsAdmin.Service.Interfaces
{
    public interface IRepositoryBase<TEntity, TId> : IReadOnlyRepositoryBase<TEntity>
        where TEntity : class, new()
    {
        Task<TEntity> CreateAsync(TEntity newModel);

        Task<TEntity> EditAsync(TEntity newModel, 
            Func<TEntity, bool> existsPredicate, 
            Func<TEntity, TEntity> setEntityData);

        Task DeleteAsync(TId id);
    }
}
