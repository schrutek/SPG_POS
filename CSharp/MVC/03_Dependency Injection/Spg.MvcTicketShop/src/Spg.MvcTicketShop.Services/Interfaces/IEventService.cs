using Spg.MvcTicketShop.Services.Dtos;
using Spg.MvcTicketShop.Services.Helper;
using Spg.MvcTicketShop.Services.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Spg.MvcTicketShop.Services.Interfaces
{
    public interface IEventService
    {
        Task<IEnumerable<Events>> GetAllAsync();

        Task<IEnumerable<Events>> GetAllAsync(string filter, string currentFilter, Guid? state, string sortOrder, int? pageNumber);

        Task<Events> GetSingleOrDefaultAsync(Guid id);

        Task<Events> CreateAsync(Events newModel);

        Task<Events> EditAsync(Guid id, Events newModel);

        Task<Events> DeleteAsync(Guid? id);
    }
}
