using Microsoft.EntityFrameworkCore;
using Spg.Services.Dom;
using Spg.Services.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Spg.Services.Services
{
    public class EventService
    {
        private TicketShopContext _dbContext = new TicketShopContext();

        /// <summary>
        /// Erstellte eine neue Instanz der Klasse <code>EventService</code>
        /// </summary>
        private static readonly Lazy<EventService> _instance = new Lazy<EventService>(() => new EventService());

        /// <summary>
        /// Der Konstruktor wird private gesetzut. Die Klasse kann nun von 
        /// außen nicht mehr instanziert werden.
        /// </summary>
        private EventService()
        { }

        /// <summary>
        /// Gib die Instanz der Klasse <code>EventService</code>zurück.
        /// </summary>
        public static EventService Instance => _instance.Value;

        public IEnumerable<EventDto> GetAllEvents()
        {
            List<Events> result = _dbContext.Events.ToList();

            using (TicketShopContext dbContext = new TicketShopContext())
            {
                //List<Events> result = dbContext.Events.ToList();

                return (IEnumerable<EventDto>)dbContext.Events.Select(
                    c => new EventDto()
                    {
                        Id = c.EventId,
                        Name = c.Name,
                        Description = c.Description
                    }).ToList();
            }
        }

        public IEnumerable<ShowDto> GetShowsByEvent(Guid eventId)
        {
            List<Shows> result = _dbContext.Shows.Where(c => c.EventId == eventId).ToList();

            using (TicketShopContext dbContext = new TicketShopContext())
            {
                //List<Shows> result = dbContext.Shows.Where(c => c.EventId == eventId).ToList();

                return (IEnumerable<ShowDto>)dbContext.Shows.Where(c => c.EventId == eventId).Select(
                    c => new ShowDto()
                    {
                        Id = c.ShowId,
                        EventId = c.EventId,
                        Start = c.Start,
                        End = c.End,
                        CheckIn = c.CheckIn
                    }).ToList();
            }
        }

        public IEnumerable<GuestDto> GetAllGuests()
        {
            using (TicketShopContext dbContext = new TicketShopContext())
            {
                return (IEnumerable<GuestDto>)dbContext.Users.OrderBy(c => c.LastName).Select(
                    c => new GuestDto()
                    {
                        EMail = c.EMail,
                        FirstName = c.FirstName,
                        LastName = c.LastName,
                        RegisterDateTime = c.RegisterDateTime
                    }).ToList();
            }
        }
    }
}
