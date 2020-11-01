using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Spg.MvcTestsAdmin.Service.Interfaces;
using Spg.MvcTestsAdmin.Service.Models;
using Spg.MvcTestsAdmin.Service.Services;

namespace Spg.MvcTestsAdmin.WebApp.Controllers
{
    public class SchoolclassesController : Controller
    {
        private readonly ISchoolclassService _schoolclassService;
        private readonly ITeacherService _techerService;

        public SchoolclassesController(
            ISchoolclassService schoolclassService,
            ITeacherService teacherService)
        {
            _schoolclassService = schoolclassService;
            _techerService = teacherService;
        }

        // GET: Schoolclasses
        public async Task<IActionResult> Index()
        {
            var model = await _schoolclassService.GetAllAsync();

            return View(model);
        }

        // GET: Schoolclasses/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var model = await _schoolclassService.GetSingleOrDefaultAsync(id);

            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        // GET: Schoolclasses/Create
        public async Task<IActionResult> Create()
        {
            ViewData["C_ClassTeacher"] = new SelectList(await _techerService.GetAllAsync(), "T_ID", "T_ID");
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
                try
                {
                    await _schoolclassService.CreateAsync(schoolclass);
                    return RedirectToAction(nameof(Index));
                }
                catch (ServiceLayerException)
                {
                    return StatusCode(500);
                }
            }
            ViewData["C_ClassTeacher"] = new SelectList(await _techerService.GetAllAsync(), "T_ID", "T_ID", schoolclass.C_ClassTeacher);
            return View(schoolclass);
        }

        // GET: Schoolclasses/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var schoolclass = await _schoolclassService.GetSingleOrDefaultAsync(id);

            if (schoolclass == null)
            {
                return NotFound();
            }
            ViewData["C_ClassTeacher"] = new SelectList(await _techerService.GetAllAsync(), "T_ID", "T_ID", schoolclass.C_ClassTeacher);
            return View(schoolclass);
        }

        // POST: Schoolclasses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Post(string id, [FromBody] Schoolclass schoolclass)
        {
            if (id != schoolclass.C_ID)
            {
                return Conflict();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _schoolclassService.EditAsync(id, schoolclass);
                }
                catch (KeyNotFoundException)
                {
                    return NotFound();
                }
                catch (ServiceLayerException)
                {
                    return StatusCode(500);
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["C_ClassTeacher"] = new SelectList(await _techerService.GetAllAsync(), "T_ID", "T_ID", schoolclass.C_ClassTeacher);
            return View(schoolclass);
        }

        // GET: Schoolclasses/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var model = await _schoolclassService.GetSingleOrDefaultAsync(id);
            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        // POST: Schoolclasses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            try
            {
                await _schoolclassService.DeleteAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch (ServiceLayerException)
            {
                return StatusCode(500);
            }
        }
    }
}
