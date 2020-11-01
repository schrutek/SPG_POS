using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Spg.MvcTestsAdmin.Service.Interfaces;
using Spg.MvcTestsAdmin.Service.Models;

namespace Spg.MvcTestsAdmin.WebApp.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class LessonsController : Controller
    {
        private readonly ILessonService _lessonService;
        private readonly ISchoolclassService _schoolclassService;
        private readonly ITeacherService _teacherService;
        private readonly IPeriodService _periodService;

        public LessonsController(
            ILessonService lessonService,
            ISchoolclassService schoolclassService,
            ITeacherService teacherService,
            IPeriodService periodService)
        {
            _lessonService = lessonService;
            _schoolclassService = schoolclassService;
            _teacherService = teacherService;
            _periodService = periodService;
        }

        // GET: Lessons
        [HttpGet()]
        public async Task<IActionResult> Index()
        {
            //var testsAdministratorContext = _context.Lesson.Include(l => l.L_ClassNavigation).Include(l => l.L_HourNavigation).Include(l => l.L_TeacherNavigation);
            var testsAdministratorContext = await _lessonService.GetAllAsync();
            return View(testsAdministratorContext);
        }

        [HttpGet("{schoolclassId}")]
        public async Task<IActionResult> IndexByLessonId(string schoolclassId)
        {
            ViewData["schoolclassId"] = schoolclassId;

            var testsAdministratorContext = await _lessonService.GetAllBySchoolclassAsync(schoolclassId);
            return View("Index", testsAdministratorContext);
        }

        // GET: Lessons/Details/5
        [HttpGet("{id}/{schoolclassId}")]
        public async Task<IActionResult> Details(long? id, string schoolclassId)
        {
            ViewData["schoolclassId"] = schoolclassId;

            if (id == null)
            {
                return NotFound();
            }

            var lesson = await _lessonService.GetSingleOrDefaultAsync(id.Value);

            if (lesson == null)
            {
                return NotFound();
            }

            return View(lesson);
        }

        // GET: Lessons/Create
        [HttpGet()]
        public async Task<IActionResult> Create()
        {
            ViewData["L_Class"] = new SelectList(await _schoolclassService.GetAllAsync(), "C_ID", "C_ID");
            ViewData["L_Hour"] = new SelectList(await _periodService.GetAllAsync(), "P_Nr", "P_Nr");
            ViewData["L_Teacher"] = new SelectList(await _teacherService.GetAllAsync(), "T_ID", "T_ID");
            return View();
        }

        // POST: Lessons/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost()]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] Lesson lesson)
        {
            if (ModelState.IsValid)
            {
                await _lessonService.CreateAsync(lesson);
                return RedirectToAction(nameof(Index));
            }
            ViewData["L_Class"] = new SelectList(await _schoolclassService.GetAllAsync(), "C_ID", "C_ID", lesson.L_Class);
            ViewData["L_Hour"] = new SelectList(await _periodService.GetAllAsync(), "P_Nr", "P_Nr", lesson.L_Hour);
            ViewData["L_Teacher"] = new SelectList(await _teacherService.GetAllAsync(), "T_ID", "T_ID", lesson.L_Teacher);
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

            var lesson = await _lessonService.GetSingleOrDefaultAsync(id.Value);

            if (lesson == null)
            {
                return NotFound();
            }
            ViewData["L_Class"] = new SelectList(await _schoolclassService.GetAllAsync(), "C_ID", "C_ID", lesson.L_Class);
            ViewData["L_Hour"] = new SelectList(await _periodService.GetAllAsync(), "P_Nr", "P_Nr", lesson.L_Hour);
            ViewData["L_Teacher"] = new SelectList(await _teacherService.GetAllAsync(), "T_ID", "T_ID", lesson.L_Teacher);
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
                    await _lessonService.EditAsync(id, lesson);
                }
                catch (KeyNotFoundException)
                {
                    return NotFound();
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }

                //return RedirectToAction(nameof(Index));
                return RedirectToAction(nameof(IndexByLessonId), new RouteValueDictionary(new { schoolclassId = lesson.L_Class }));
            }
            ViewData["L_Class"] = new SelectList(await _schoolclassService.GetAllAsync(), "C_ID", "C_ID", lesson.L_Class);
            ViewData["L_Hour"] = new SelectList(await _periodService.GetAllAsync(), "P_Nr", "P_Nr", lesson.L_Hour);
            ViewData["L_Teacher"] = new SelectList(await _teacherService.GetAllAsync(), "T_ID", "T_ID", lesson.L_Teacher);
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

            var lesson = await _lessonService.GetSingleOrDefaultAsync(id.Value);

            if (lesson == null)
            {
                return NotFound();
            }

            return View(lesson);
        }

        // POST: Lessons/Delete/5
        [HttpPost("{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            await _lessonService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
