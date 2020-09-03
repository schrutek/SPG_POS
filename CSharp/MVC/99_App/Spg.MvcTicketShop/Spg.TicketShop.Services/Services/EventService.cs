using Microsoft.EntityFrameworkCore;
using Spg.TicketShop.Services.Helper;
using Spg.TicketShop.Services.Interfaces;
using Spg.TicketShop.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.Data.Entity;

namespace Spg.TicketShop.Services.Services
{
    public class EventService : IEventService
    {
        private readonly TicketShopContext _context;

        public EventService(TicketShopContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Events>> GetAllAsync()
        {
            var result = await _context.Events
                .Include(c => c.Shows)
                .ToListAsync();

            return result;
        }

        public async Task<PaginatedList<Events>> GetAllAsync(string filter, string currentFilter, Guid? state, string sortOrder, int? pageNumber)
        {
            if (filter != null)
            {
                pageNumber = 1;
            }
            else
            {
                filter = currentFilter;
            }

            // Hier zeigen wie man von IFs zu IQeriable kommt
            IQueryable<Events> result = GetQueryable(filter, currentFilter, state, sortOrder)
                .Include(c => c.Shows);

            int pageSize = 4;
            return await PaginatedList<Events>.CreateAsync(result.AsNoTracking(), pageNumber ?? 1, pageSize);
        }

        private IQueryable<Events> GetQueryable(string filter, string currentFilter, Guid? state, string sortOrder, int? skip = null, int? take = null)
        {
            IQueryable<Events> result = _context.Events;
            if (!String.IsNullOrEmpty(filter))
            {
                result = result.Where(c => c.Name.Contains(filter));
            }
            if (state.HasValue)
            {
                //if (state.Value != new Guid())
                //{
                    result = result.Where(c => c.CatEventStateId == state);
                //}
            }
            if (!String.IsNullOrEmpty(sortOrder))
            {
                switch (sortOrder)
                {
                    case "onlinefrom":
                        result = result.OrderBy(s => s.OnlineFrom);
                        break;
                    case "onlinefrom_desc":
                        result = result.OrderByDescending(s => s.OnlineFrom);
                        break;
                    case "onlineto":
                        result = result.OrderBy(s => s.OnlineFrom);
                        break;
                    case "onlineto_desc":
                        result = result.OrderByDescending(s => s.OnlineFrom);
                        break;
                    case "description":
                        result = result.OrderBy(s => s.OnlineFrom);
                        break;
                    case "description_desc":
                        result = result.OrderByDescending(s => s.OnlineFrom);
                        break;
                    case "name_desc":
                        result = result.OrderByDescending(s => s.Name);
                        break;
                    default:
                        result = result.OrderBy(s => s.Name);
                        break;
                }
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

        public async Task<Events> GetSingleOrDefaultAsync(Guid id)
        {
            var result = await _context.Events
                .Include(c => c.Shows)
                .FirstOrDefaultAsync(m => m.EventId == id);

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
                if (! EventsExists(newModel.EventId))
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
