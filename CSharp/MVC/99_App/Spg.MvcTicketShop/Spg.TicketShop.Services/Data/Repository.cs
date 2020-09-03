using Microsoft.EntityFrameworkCore;
using Spg.TicketShop.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Spg.TicketShop.Services.Data
{
    public class Repository<TEntity>
        where TEntity : class, new()
    {
        private readonly TicketShopContext _context;

        public Repository(TicketShopContext context)
        {
            _context = context;
        }

        //public IQueryable<TEntity> GetQueryable(
        //    Expression<Func<TEntity, bool>> filter = null, 
        //    string includeProperties = null,
        //    Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
        //    int? skip = null,
        //    int? take = null)
        //{
        //    IQueryable<TEntity> query = _context.Set<TEntity>();
            
        //    if (!String.IsNullOrEmpty(filter))
        //    {
        //        query = query.Where(c => c.Name.Contains(filter));
        //    }
        //    if (state.HasValue)
        //    {
        //        query = query.Where(c => c.CatEventStateId == state);
        //    }
        //    if (String.IsNullOrEmpty(sortCol))
        //    {

        //    }
        //    query = query.Include()
        //    return query;
        //}
    }
}
