using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Spg.TicketShop.Services.Helper;
using Spg.TicketShop.Services.Interfaces;
using Spg.TicketShop.Services.Models;
using Spg.TicketShop.Services.Services;

namespace Spg.TicketShop.Controllers
{
    [Authorize(Roles = "User,Administrator")]
    [Route("[controller]/[action]")]
    public class EventsController : Controller
    {
        private readonly IEventService _eventService;

        public EventsController(IEventService eventService)
        {
            _eventService = eventService;
        }

        // GET: Events
        [HttpGet()]
        public async Task<IActionResult> Index(string filter, string currentFilter, Guid? state, string sortOrder, int? pageNumber)
        {
            ViewData["CurrentFilter"] = filter;
            ViewData["CurrentState"] = state;
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["DescriptionSortParm"] = sortOrder == "description" ? "description_desc" : "description";
            ViewData["DateFromSortParm"] = sortOrder == "onlinefrom" ? "onlinefrom_desc" : "onlinefrom";
            ViewData["DateToSortParm"] = sortOrder == "onlineto" ? "onlineto_desc" : "onlineto";

            PaginatedList<Events> model = await _eventService.GetAllAsync(filter, currentFilter, state, sortOrder, pageNumber);
            return View(model);
        }

        // GET: Events/Details/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Details(Guid? id)
        {
            if (!id.HasValue)
            {
                return NotFound();
            }

            var events = await _eventService.GetSingleOrDefaultAsync(id.Value);

            if (events == null)
            {
                return NotFound();
            }

            return View(events);
        }

        // GET: Events/Create
        [HttpGet()]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Events/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EventId,LastChangeDate,LaseChangeUserIdId,Name,Description,OnlineFrom,OnlineTo")] Events events)
        {
            if (ModelState.IsValid)
            {
                await _eventService.CreateAsync(events);

                return RedirectToAction(nameof(Index));
            }
            return View(events);
        }

        // GET: Events/Edit/5
        [HttpGet()]
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var events = await _eventService.GetSingleOrDefaultAsync(id.Value);

            if (events == null)
            {
                return NotFound();
            }
            return View(events);
        }

        // POST: Events/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("EventId,LastChangeDate,LaseChangeUserIdId,Name,Description,OnlineFrom,OnlineTo")] Events events)
        {
            if (id != events.EventId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _eventService.EditAsync(id, events);
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
                catch (KeyNotFoundException)
                {
                    return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(events);
        }

        // GET: Events/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var events = await _eventService.GetSingleOrDefaultAsync(id.Value);

            if (events == null)
            {
                return NotFound();
            }

            return View(events);
        }

        // POST: Events/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _eventService.DeleteAsync(id);

            return RedirectToAction(nameof(Index));
        }
    }
}
