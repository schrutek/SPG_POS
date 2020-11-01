using Spg.MvcTestsAdmin.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Spg.MvcTestsAdmin.Service.Interfaces
{
    public interface IReadOnlyRepositoryBase<TEntity>
        where TEntity : class, new()
    {
        TestsAdministratorContext Context { get; }

        IQueryable<TEntity> GetQueryable(
            Expression<Func<TEntity, bool>> filter,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> sortOrder,
            out bool hasMore,
            string includeNavigationProperty = null,
            int? skip = null,
            int? take = null);

        IQueryable<TEntity> GetAll(
                    Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                    string includeNavigationProperty = "",
                    int? skip = null,
                    int? take = null);

        IQueryable<TEntity> Get(
                    Expression<Func<TEntity, bool>> filter = null,
                    Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                    string includeNavigationProperty = "",
                    int? skip = null,
                    int? take = null);

        TEntity GetSingle(
                    Expression<Func<TEntity, bool>> filter = null,
                    string includeNavigationProperty = "");

        Task<TEntity> GetSingleAsync(
                Expression<Func<TEntity, bool>> filter = null,
                string includeNavigationProperty = "");
    }
}
