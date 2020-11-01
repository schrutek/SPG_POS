using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Spg.MvcTestsAdmin.Service.Models;

namespace Spg.MvcTestsAdmin.WebApp.Controllers
{
    public class SchoolclassesController : Controller
    {
        private readonly TestsAdministratorContext _context;

        public SchoolclassesController(TestsAdministratorContext context)
        {
            _context = context;
        }

        // GET: Schoolclasses
        public async Task<IActionResult> Index()
        {
            var testsAdministratorContext = _context.Schoolclass.Include(s => s.C_ClassTeacherNavigation);
            return View(await testsAdministratorContext.ToListAsync());
        }

        // GET: Schoolclasses/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var schoolclass = await _context.Schoolclass
                .Include(s => s.C_ClassTeacherNavigation)
                .FirstOrDefaultAsync(m => m.C_ID == id);
            if (schoolclass == null)
            {
                return NotFound();
            }

            return View(schoolclass);
        }

        // GET: Schoolclasses/Create
        public IActionResult Create()
        {
            ViewData["C_ClassTeacher"] = new SelectList(_context.Teacher, "T_ID", "T_ID");
            return View();
        }

        // POST: Schoolclasses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("C_ID,C_Department,C_ClassTeacher")] Schoolclass schoolclass)
        {
            if (ModelState.IsValid)
            {
                _context.Add(schoolclass);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["C_ClassTeacher"] = new SelectList(_context.Teacher, "T_ID", "T_ID", schoolclass.C_ClassTeacher);
            return View(schoolclass);
        }

        // GET: Schoolclasses/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var schoolclass = await _context.Schoolclass.FindAsync(id);
            if (schoolclass == null)
            {
                return NotFound();
            }
            ViewData["C_ClassTeacher"] = new SelectList(_context.Teacher, "T_ID", "T_ID", schoolclass.C_ClassTeacher);
            return View(schoolclass);
        }

        // POST: Schoolclasses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("C_ID,C_Department,C_ClassTeacher")] Schoolclass schoolclass)
        {
            if (id != schoolclass.C_ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(schoolclass);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SchoolclassExists(schoolclass.C_ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["C_ClassTeacher"] = new SelectList(_context.Teacher, "T_ID", "T_ID", schoolclass.C_ClassTeacher);
            return View(schoolclass);
        }

        // GET: Schoolclasses/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var schoolclass = await _context.Schoolclass
                .Include(s => s.C_ClassTeacherNavigation)
                .FirstOrDefaultAsync(m => m.C_ID == id);
            if (schoolclass == null)
            {
                return NotFound();
            }

            return View(schoolclass);
        }

        // POST: Schoolclasses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var schoolclass = await _context.Schoolclass.FindAsync(id);
            _context.Schoolclass.Remove(schoolclass);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SchoolclassExists(string id)
        {
            return _context.Schoolclass.Any(e => e.C_ID == id);
        }
    }
}
