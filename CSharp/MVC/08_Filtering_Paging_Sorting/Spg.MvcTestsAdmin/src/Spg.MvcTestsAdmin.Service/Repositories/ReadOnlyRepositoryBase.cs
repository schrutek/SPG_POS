using Microsoft.EntityFrameworkCore;
using Spg.MvcTestsAdmin.Service.Interfaces;
using Spg.MvcTestsAdmin.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Spg.MvcTestsAdmin.Service.Repositories
{
    public class ReadOnlyRepositoryBase<TEntity> : IReadOnlyRepositoryBase<TEntity>
        where TEntity : class, new()
    {
        public TestsAdministratorContext Context { get; }

        public ReadOnlyRepositoryBase(TestsAdministratorContext context)
        {
            Context = context;
        }

        public IQueryable<TEntity> GetQueryable(
            Expression<Func<TEntity, bool>> filter,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> sortOrder,
            out bool hasMore,
            string includeNavigationProperty = null,
            int? skip = null,
            int? take = null)
        {
            hasMore = false;

            IQueryable<TEntity> result = Context.Set<TEntity>();

            if (filter != null)
            {
                result = result.Where(filter);
            }
            if (sortOrder != null)
            {
                result = sortOrder(result);
            }

            includeNavigationProperty = includeNavigationProperty ?? String.Empty;
            foreach (var item in includeNavigationProperty.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries))
            {
                result = result.Include(item);
            }

            int count = result.Count();
            if (skip.HasValue)
            {
                result = result.Skip(skip.Value);
            }
            if (take.HasValue)
            {
                result = result.Take(take.Value);
            }

            if (result.Count() < count)
            {
                hasMore = true;
            }
            return result;
        }

        public IQueryable<TEntity> GetAll(
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeNavigationProperty = "", 
            int? skip = null, 
            int? take = null)
        {
            bool hasMore;
            return GetQueryable(
                null, 
                orderBy, 
                out hasMore, 
                includeNavigationProperty, 
                skip, 
                take
            );
        }

        public IQueryable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null, 
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, 
            string includeNavigationProperty = "", 
            int? skip = null, 
            int? take = null)
        {
            bool hasMore;

            return GetQueryable(
                filter, 
                orderBy, 
                out hasMore, 
                includeNavigationProperty, 
                skip, 
                take
            );
        }

        public TEntity GetSingle(
            Expression<Func<TEntity, bool>> filter = null,
            string includeNavigationProperty = "")
        {
            bool hasMore;
            return GetQueryable(
                filter,
                null,
                out hasMore,
                includeNavigationProperty: includeNavigationProperty
            ).SingleOrDefault();
        }

        public async Task<TEntity> GetSingleAsync(
        Expression<Func<TEntity, bool>> filter = null, 
        string includeNavigationProperty = "")
        {
            bool hasMore;
            return await GetQueryable(
                filter, 
                null, 
                out hasMore, 
                includeNavigationProperty: includeNavigationProperty
            ).SingleOrDefaultAsync();
        }
    }
}
