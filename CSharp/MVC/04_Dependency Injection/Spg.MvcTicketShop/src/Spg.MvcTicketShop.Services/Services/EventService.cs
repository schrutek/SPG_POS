using Microsoft.EntityFrameworkCore;
using Spg.MvcTicketShop.Services.Dtos;
using Spg.MvcTicketShop.Services.Helper;
using Spg.MvcTicketShop.Services.Interfaces;
using Spg.MvcTicketShop.Services.Models;
using Spg.MvcTicketShop.Services.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Spg.MvcTicketShop.Services.Services
{
    public class EventService : IEventService
    {
        private readonly EFRepository<Events> _repository;
        private readonly TicketShopContext _context;

        public EventService(EFRepository<Events> repository, TicketShopContext context)
        {
            _context = context;
            _repository = repository;
        }

        public async Task<IEnumerable<Events>> GetAllAsync()
        {
            var result = await _context.Events
                .Include(c => c.Shows)
                .ToListAsync();
            return result;
        }

        public async Task<IEnumerable<Events>> GetAllAsync(string filter, string currentFilter, Guid? state, string sortOrder, int? pageNumber)
        {
            if (!String.IsNullOrEmpty(filter))
            {
                result = result.Where(c => c.Name.Contains(filter) && c.Description.Contains(filter));
            }

            if (state.HasValue)
            {
                result = result.Where(c => c.CatEventStateId == state.Value);
            }

            if (!String.IsNullOrEmpty(sortOrder))
            {
                switch (sortOrder)
                {
                    case "onlinefrom":
                        result = result.OrderBy(c => c.OnlineFrom);
                        break;
                    case "onlinefrom_desc":
                        result = result.OrderByDescending(c => c.OnlineFrom);
                        break;
                    case "onlineto":
                        result = result.OrderBy(s => s.OnlineTo);
                        break;
                    case "onlineto_desc":
                        result = result.OrderByDescending(s => s.OnlineTo);
                        break;
                    case "description":
                        result = result.OrderBy(s => s.Description);
                        break;
                    case "description_desc":
                        result = result.OrderByDescending(s => s.Description);
                        break;
                    case "name_desc":
                        result = result.OrderByDescending(s => s.Name);
                        break;
                    default:
                        result = result.OrderBy(s => s.Name);
                        break;
                }
            }

            return null;
        }

        public IQueryable<Events> GetQueriable(
            Expression<Func<Events, bool>> filter, 
            Func<IQueryable<Events>, IOrderedQueryable<Events>> sortOrder,
            string includeNavigationProperty = null,
            int? skip = null, 
            int? take = null)
        {
            IQueryable<Events> result = _context.Events;

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

            if (skip.HasValue)
            {
                result = result.Skip(skip.Value);
            }

            if (take.HasValue)
            {
                result = result.Take(take.Value);
            }

            return result;
        }

        public async Task<IEnumerable<Events>> GetAllOldAsync(string filter, string currentFilter, Guid? state, string sortOrder, int? pageNumber)
        {
            if (filter != null)
            {
                pageNumber = 1;
            }
            else
            {
                filter = currentFilter;
            }

            Func<IQueryable<Events>, IOrderedQueryable<Events>> orderBy = null;
            if (!String.IsNullOrEmpty(sortOrder))
            {
                switch (sortOrder)
                {
                    case "onlinefrom":
                        orderBy = c => c.OrderBy(s => s.OnlineFrom);
                        break;
                    case "onlinefrom_desc":
                        orderBy = c => c.OrderByDescending(s => s.OnlineFrom);
                        break;
                    case "onlineto":
                        orderBy = c => c.OrderBy(s => s.OnlineTo);
                        break;
                    case "onlineto_desc":
                        orderBy = c => c.OrderByDescending(s => s.OnlineTo);
                        break;
                    case "description":
                        orderBy = c => c.OrderBy(s => s.Description);
                        break;
                    case "description_desc":
                        orderBy = c => c.OrderByDescending(s => s.Description);
                        break;
                    case "name_desc":
                        orderBy = c => c.OrderByDescending(s => s.Name);
                        break;
                    default:
                        orderBy = c => c.OrderBy(s => s.Name);
                        break;
                }
            }

            Expression<Func<Events, bool>> filterQuery = null;
            if (!String.IsNullOrEmpty(filter))
            {
                filterQuery = c => c.Name.Contains(filter) || c.Description.Contains(filter);
            }
            
            bool hasMore = false;
            IQueryable<Events> result = _repository.GetQueryable(filterQuery, currentFilter, orderBy, out hasMore, 0, 100);

            if (state.HasValue)
            {
                result = result.Where(c => c.CatEventStateId == state);
            }

            int pageSize = 4;
            return await PaginatedList<Events>.CreateAsync(result.AsNoTracking(), pageNumber ?? 1, pageSize, hasMore);
        }

        public async Task<Events> GetSingleOrDefaultAsync(Guid id)
        {
            var result = await _context.Events
                .Include(c => c.Shows)
                .FirstOrDefaultAsync(m => m.EventId == id);

            //.Select(c => new EventDto()
            // {
            //     CatEventStateId = c.CatEventStateId,
            //     Description = c.Description,
            //     EventId = c.EventId,
            //     LaseChangeUserId = c.LaseChangeUserId,
            //     LastChangeDate = c.LastChangeDate,
            //     Name = c.Name,
            //     OnlineFrom = c.OnlineFrom,
            //     OnlineTo = c.OnlineTo,
            //     CatEventState = c.CatEventState,
            //     Shows = c.Shows.Select(s => new ShowDto()
            //     {
            //         CheckIn = s.CheckIn,
            //         ShowId = s.ShowId,
            //         End = s.End,
            //         EventId = s.EventId,
            //         Start = s.Start,
            //         LaseChangeUserId = s.LaseChangeUserId,
            //         LastChangeDate = s.LastChangeDate,
            //     }).ToList()
            // })

            return result;
        }

        public async Task<Events> CreateAsync(Events newModel)
        {
            newModel.EventId = Guid.NewGuid();
            _context.Add(newModel);
            await _context.SaveChangesAsync();

            return newModel;
        }

        public async Task<Events> DeleteAsync(Guid? id)
        {
            var result = await _context.Events.FindAsync(id);
            _context.Events.Remove(result);
            await _context.SaveChangesAsync();

            return result;
        }

        public async Task<Events> EditAsync(Guid id, Events newModel)
        {
            try
            {
                newModel.LastChangeDate = DateTime.Now;

                _context.Update(newModel);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EventsExists(newModel.EventId))
                {
                    throw new KeyNotFoundException("Datensatz wurde nicht gefunden!");
                }
                else
                {
                    throw;
                }
            }

            return newModel;
        }

        private bool EventsExists(Guid id)
        {
            return _context.Events.Any(e => e.EventId == id);
        }
    }
}
