using Spg.TicketShop.Services.Helper;
using Spg.TicketShop.Services.Interfaces;
using Spg.TicketShop.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.TicketShop.Services.MockServices
{
    public class EventServiceMock : IEventService
    {
        private IEnumerable<Events> _eventList;

        public EventServiceMock()
        {
            _eventList = new List<Events>()
            {
                new Events()
                {
                    EventId = new Guid("659F67CE-F39C-4F81-B222-015113794849"),
                    Description = "Mock-Description",
                    Name = "Mock-Name",
                    OnlineFrom = DateTime.Now,
                    OnlineTo = DateTime.Now
                }
            };        
        }

        public async Task<IEnumerable<Events>> GetAllAsync()
        {
            return await Task.FromResult(_eventList);
        }

        public async Task<Events> GetSingleOrDefaultAsync(Guid id)
        {
            return await Task.FromResult(_eventList
                .Where(c => c.EventId == id)
                .FirstOrDefault());
        }

        public async Task<Events> CreateAsync(Events newModel)
        {
            newModel.EventId = Guid.NewGuid();

            _eventList.ToList().Add(newModel);

            return await Task.FromResult(newModel);
        }

        public async Task<Events> DeleteAsync(Guid? id)
        {
            var result = _eventList.Where(c => c.EventId == id).FirstOrDefault();
            _eventList.ToList().Remove(result);

            return await Task.FromResult(result);
        }

        public async Task<Events> EditAsync(Guid id, Events newModel)
        {
            List<Events> tmpList = _eventList.ToList();

            var result = tmpList.Where(c => c.EventId == id).FirstOrDefault();
            tmpList.Remove(result);
            tmpList.Add(newModel);
            _eventList = tmpList;

            return await Task.FromResult(result);
        }

        public Task<PaginatedList<Events>> GetAllAsync(string filter, string currentFilter, Guid? state, string sortOrder, int? pageNumber)
        {
            throw new NotImplementedException();
        }
    }
}
