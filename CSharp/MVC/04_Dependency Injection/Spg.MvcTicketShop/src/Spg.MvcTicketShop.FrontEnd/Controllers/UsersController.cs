using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Spg.MvcTicketShop.Services.Dtos;
using Spg.MvcTicketShop.Services.Helper;
using Spg.MvcTicketShop.Services.Interfaces;
using Spg.MvcTicketShop.Services.Models;

namespace Spg.MvcTicketShop.FrontEnd.Controllers
{
    [Route("[controller]/[action]")]
    public class UsersController : Controller
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: Users
        public async Task<IActionResult> Index(string filter, string currentFilter, string sortOrder, int? pageNumber)
        {
            ViewData["CurrentFilter"] = filter;
            if (String.IsNullOrEmpty(filter))
            {
                ViewData["CurrentFilter"] = currentFilter;
            }
            ViewData["CurrentSort"] = sortOrder;

            ViewData["FirstNameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "firstname_desc" : "";
            ViewData["LastNameSortParm"] = sortOrder == "lastname" ? "lastname_desc" : "lastname";
            ViewData["EMailSortParm"] = sortOrder == "email" ? "email_desc" : "email";

            PaginatedList<UserDto> model = (PaginatedList<UserDto>)await _userService.GetAllAsync(filter, currentFilter, sortOrder, pageNumber);
            return View(model);
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var result = await _userService.GetSingleOrDefaultAsync(id.Value);

            if (result == null)
            {
                return NotFound();
            }

            return View(result);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,LastChangeDate,LaseChangeUserId,RegisterDateTime,FirstName,LastName,EMail,PasswordHash,Salt,Role")] Users users)
        {
            if (ModelState.IsValid)
            {
                await _userService.CreateAsync(users);

                return RedirectToAction(nameof(Index));
            }
            return View(users);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var result = await _userService.GetSingleOrDefaultAsync(id.Value);

            if (result == null)
            {
                return NotFound();
            }
            return View(result);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("UserId,LastChangeDate,LaseChangeUserId,RegisterDateTime,FirstName,LastName,EMail,PasswordHash,Salt,Role")] Users users)
        {
            if (id != users.UserId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _userService.EditAsync(id, users);
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
            return View(users);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var result = await _userService.GetSingleOrDefaultAsync(id.Value);

            if (result == null)
            {
                return NotFound();
            }

            return View(result);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _userService.DeleteAsync(id);

            return RedirectToAction(nameof(Index));
        }
    }
}
