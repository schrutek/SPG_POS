using Spg.TicketShop.Services.Helper;
using Spg.TicketShop.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.TicketShop.Services.Interfaces
{
    public interface IEventService
    {
        Task<IEnumerable<Events>> GetAllAsync();

        Task<PaginatedList<Events>> GetAllAsync(string filter, string currentFilter, Guid? state, string sortOrder, int? pageNumber);

        Task<Events> GetSingleOrDefaultAsync(Guid id);

        Task<Events> CreateAsync(Events newModel);

        Task<Events> EditAsync(Guid id, Events newModel);

        Task<Events> DeleteAsync(Guid? id);
    }
}
