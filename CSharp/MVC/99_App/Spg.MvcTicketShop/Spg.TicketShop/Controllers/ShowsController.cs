using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Spg.TicketShop.Services.Interfaces;
using Spg.TicketShop.Services.Models;

namespace Spg.TicketShop.Controllers
{
    [Authorize(Roles = "Administrator")]
    [Route("[controller]/[action]")]
    public class ShowsController : Controller
    {
        private readonly IShowService _showService;
        private readonly IEventService _eventService;

        public ShowsController(IShowService showService, IEventService eventService)
        {
            _showService = showService;
            _eventService = eventService;
        }

        // GET: Shows
        [HttpGet()]
        public async Task<IActionResult> Index()
        {
            var model = await _showService.GetAllAsync();
            return View(model);
        }

        [HttpGet("eventid")]
        public async Task<IActionResult> FilteredById(Guid? eventId)
        {
            if (!eventId.HasValue)
            {
                return NotFound();
            }

            var model = await _showService.GetAllByEventAsync(eventId.Value);

            if (model == null)
            {
                return NotFound();
            }

            return View("Index", model);
        }

        // GET: Shows/Details/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Details(Guid? id)
        {
            if (!id.HasValue )
            {
                return NotFound();
            }

            var result = await _showService.GetSingleOrDefaultAsync(id.Value);

            if (result == null)
            {
                return NotFound();
            }

            return View(result);
        }

        // GET: Shows/Create
        [HttpGet("Create")]
        public IActionResult Create()
        {
            //ViewData["EventId"] = new SelectList(_context.Events, "EventId", "Name");
            //ViewData["LaseChangeUserId"] = new SelectList(_context.Users, "UserId", "FirstName");
            return View();
        }

        // POST: Shows/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ShowId,LastChangeDate,LaseChangeUserId,CheckIn,Start,End,EventId")] Shows shows)
        {
            if (ModelState.IsValid)
            {
                shows.ShowId = Guid.NewGuid();
                
                await _showService.CreateAsync(shows);

                return RedirectToAction(nameof(Index));
            }
            //ViewData["EventId"] = new SelectList(_context.Events, "EventId", "Name", shows.EventId);
            //ViewData["LaseChangeUserId"] = new SelectList(_context.Users, "UserId", "FirstName", shows.LaseChangeUserId);
            return View(shows);
        }

        // GET: Shows/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (!id.HasValue)
            {
                return NotFound();
            }

            var shows = await _showService.GetSingleOrDefaultAsync(id.Value);

            if (shows == null)
            {
                return NotFound();
            }
            ViewData["EventId"] = new SelectList(_eventService.GetAllAsync().Result.ToList(), "EventId", "Name", shows.EventId);
            //ViewData["LaseChangeUserId"] = new SelectList(_context.Users, "UserId", "FirstName", shows.LaseChangeUserId);
            return View(shows);
        }

        // POST: Shows/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ShowId,LastChangeDate,LaseChangeUserId,CheckIn,Start,End,EventId")] Shows shows)
        {
            if (id != shows.ShowId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Shows result = await _showService.EditAsync(id, shows);
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
            //ViewData["EventId"] = new SelectList(_context.Events, "EventId", "Name", shows.EventId);
            //ViewData["LaseChangeUserId"] = new SelectList(_context.Users, "UserId", "FirstName", shows.LaseChangeUserId);
            return View(shows);
        }

        // GET: Shows/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (!id.HasValue)
            {
                return NotFound();
            }

            var result = await _showService.GetSingleOrDefaultAsync(id.Value);

            if (result == null)
            {
                return NotFound();
            }

            return View(result);
        }

        // POST: Shows/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _showService.DeleteAsync(id);

            return RedirectToAction(nameof(Index));
        }
    }
}
