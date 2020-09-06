using Microsoft.EntityFrameworkCore;
using Spg.MvcTicketShop.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Spg.MvcTicketShop.Services.Repositories
{
    public class EFRepository<TEntity>
        where TEntity : class, new()
    {
        public TicketShopContext Context { get; }

        public EFRepository(TicketShopContext context)
        {
            Context = context;
        }

        public IQueryable<TEntity> GetQueryable(
            Expression<Func<TEntity, bool>> filter, 
            string currentFilter,
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

            if (!string.IsNullOrEmpty(includeNavigationProperty))
            {
                result = result.Include(includeNavigationProperty);
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
    }
}
