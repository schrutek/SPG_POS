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
    [Route("[controller]/[action]")]
    [ApiController()]
    public class LessonsController : Controller
    {
        private readonly TestsAdministratorContext _context;

        public LessonsController(TestsAdministratorContext context)
        {
            _context = context;
        }

        // GET: Lessons
        [HttpGet()]
        public async Task<IActionResult> Index()
        {
            var testsAdministratorContext = _context.Lesson.Include(l => l.L_ClassNavigation).Include(l => l.L_HourNavigation).Include(l => l.L_TeacherNavigation);
            return View(await testsAdministratorContext.ToListAsync());
        }

        // GET: Lessons/Details/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lesson = await _context.Lesson
                .Include(l => l.L_ClassNavigation)
                .Include(l => l.L_HourNavigation)
                .Include(l => l.L_TeacherNavigation)
                .FirstOrDefaultAsync(m => m.L_ID == id);
            if (lesson == null)
            {
                return NotFound();
            }

            return View(lesson);
        }

        // GET: Lessons/Create
        [HttpGet()]
        public IActionResult Create()
        {
            ViewData["L_Class"] = new SelectList(_context.Schoolclass, "C_ID", "C_ID");
            ViewData["L_Hour"] = new SelectList(_context.Period, "P_Nr", "P_Nr");
            ViewData["L_Teacher"] = new SelectList(_context.Teacher, "T_ID", "T_ID");
            return View();
        }

        // POST: Lessons/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost()]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm()] Lesson lesson)
        {
            if (ModelState.IsValid)
            {
                _context.Add(lesson);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["L_Class"] = new SelectList(_context.Schoolclass, "C_ID", "C_ID", lesson.L_Class);
            ViewData["L_Hour"] = new SelectList(_context.Period, "P_Nr", "P_Nr", lesson.L_Hour);
            ViewData["L_Teacher"] = new SelectList(_context.Teacher, "T_ID", "T_ID", lesson.L_Teacher);
            return View(lesson);
        }

        // GET: Lessons/Edit/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lesson = await _context.Lesson.FindAsync(id);
            if (lesson == null)
            {
                return NotFound();
            }
            ViewData["L_Class"] = new SelectList(_context.Schoolclass, "C_ID", "C_ID", lesson.L_Class);
            ViewData["L_Hour"] = new SelectList(_context.Period, "P_Nr", "P_Nr", lesson.L_Hour);
            ViewData["L_Teacher"] = new SelectList(_context.Teacher, "T_ID", "T_ID", lesson.L_Teacher);
            return View(lesson);
        }

        // POST: Lessons/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [FromForm()] Lesson lesson)
        {
            if (id != lesson.L_ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lesson);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LessonExists(lesson.L_ID))
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
            ViewData["L_Class"] = new SelectList(_context.Schoolclass, "C_ID", "C_ID", lesson.L_Class);
            ViewData["L_Hour"] = new SelectList(_context.Period, "P_Nr", "P_Nr", lesson.L_Hour);
            ViewData["L_Teacher"] = new SelectList(_context.Teacher, "T_ID", "T_ID", lesson.L_Teacher);
            return View(lesson);
        }

        // GET: Lessons/Delete/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lesson = await _context.Lesson
                .Include(l => l.L_ClassNavigation)
                .Include(l => l.L_HourNavigation)
                .Include(l => l.L_TeacherNavigation)
                .FirstOrDefaultAsync(m => m.L_ID == id);
            if (lesson == null)
            {
                return NotFound();
            }

            return View(lesson);
        }

        // POST: Lessons/Delete/5
        [HttpPost("{id}"), ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var lesson = await _context.Lesson.FindAsync(id);
            _context.Lesson.Remove(lesson);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LessonExists(long id)
        {
            return _context.Lesson.Any(e => e.L_ID == id);
        }
    }
}
