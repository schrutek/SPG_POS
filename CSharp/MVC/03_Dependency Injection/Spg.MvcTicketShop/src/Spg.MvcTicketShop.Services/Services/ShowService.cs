using Microsoft.EntityFrameworkCore;
using Spg.MvcTicketShop.Services.Dtos;
using Spg.MvcTicketShop.Services.Interfaces;
using Spg.MvcTicketShop.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.MvcTicketShop.Services.Services
{
    public class ShowService : IShowService
    {
        private readonly TicketShopContext _context;

        public ShowService(TicketShopContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Shows>> GetAllAsync()
        {
            var result = await _context.Shows
                .Include(c => c.Event)
                .ToListAsync();

            return result;
        }

        public async Task<IEnumerable<Shows>> GetAllByEventAsync(Guid eventId)
        {
            var result = await _context.Shows
                .Where(c => c.EventId == eventId)
                .Include(c => c.Event)
                .ToListAsync();

            return result;
        }

        public async Task<Shows> GetSingleOrDefaultAsync(Guid id)
        {
            var result = await _context.Shows
                .Include(c => c.Event)
                .FirstOrDefaultAsync(m => m.ShowId == id);

            return result;
        }

        public async Task<Shows> CreateAsync(Shows newModel)
        {
            newModel.ShowId = Guid.NewGuid();
            _context.Add(newModel);
            await _context.SaveChangesAsync();

            return newModel;
        }

        public async Task<Shows> DeleteAsync(Guid? id)
        {
            var result = await _context.Shows.FindAsync(id);
            _context.Shows.Remove(result);
            await _context.SaveChangesAsync();

            return result;
        }

        public async Task<Shows> EditAsync(Guid id, Shows newModel)
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
            return _context.Shows.Any(e => e.EventId == id);
        }
    }
}
