using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.MvcTicketShop.Services.Helper
{
    public class PaginatedList<T> : List<T>
    {
        public int PageIndex { get; private set; }

        public int TotalPages { get; private set; }

        public bool HasMore { get; set; }

        public PaginatedList(List<T> items, int count, int pageIndex, int pageSize, bool hasMore)
        {
            PageIndex = pageIndex;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            HasMore = hasMore;

            this.AddRange(items);
        }

        public bool HasPreviousPage
        {
            get
            {
                return (PageIndex > 1);
            }
        }

        public bool HasNextPage
        {
            get
            {
                return (PageIndex < TotalPages);
            }
        }

        public static async Task<PaginatedList<T>> CreateAsync(IQueryable<T> source, int pageIndex, int pageSize, bool hasMore = false)
        {
            var count = await source.CountAsync();

            var items = await source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();

            return new PaginatedList<T>(items, count, pageIndex, pageSize, hasMore);
        }
    }
}
