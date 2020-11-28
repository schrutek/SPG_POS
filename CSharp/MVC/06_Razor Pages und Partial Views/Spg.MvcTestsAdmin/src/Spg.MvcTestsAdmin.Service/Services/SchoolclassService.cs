using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Spg.MvcTestsAdmin.Service.Interfaces;
using Spg.MvcTestsAdmin.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.MvcTestsAdmin.Service.Services
{
    public class SchoolclassService : ISchoolclassService
    {
        private readonly TestsAdministratorContext _context;
        private readonly ILogger<SchoolclassService> _logger;

        public SchoolclassService(TestsAdministratorContext context, ILogger<SchoolclassService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<Schoolclass>> GetAllAsync()
        {
            return await _context.Schoolclass
                .Include(s => s.C_ClassTeacherNavigation)
                .Include(s => s.Lesson)
                .Include(s => s.Pupil)
                .ToListAsync();
        }

        public async Task<Schoolclass> GetSingleOrDefaultAsync(string id)
        {
            return await _context.Schoolclass
                .Include(s => s.C_ClassTeacherNavigation)
                .Include(s => s.Lesson)
                .FirstOrDefaultAsync(c => c.C_ID == id);
        }

        public async Task<Schoolclass> CreateAsync(Schoolclass newModel)
        {
            try
            {
                _context.Add(newModel);
                await _context.SaveChangesAsync();
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError(ex, "Methode CreateAsync(Schoolclass) ist fehlgeschlagen!");
                throw new ServiceLayerException("Methode CreateAsync(Schoolclass) ist fehlgeschlagen!", ex);
            }
            return newModel;
        }

        public async Task<Schoolclass> EditAsync(string id, Schoolclass newModel)
        {
            if (id != newModel.C_ID)
            {
                throw new KeyNotFoundException("Datensatz wurde nicht gefunden!");
            }

            try
            {
                Schoolclass existingSchoolclass = _context.Schoolclass.Find(id);
                if (existingSchoolclass == null)
                {
                    throw new KeyNotFoundException("Datensatz wurde nicht gefunden!");
                }
                else
                {
                    existingSchoolclass.C_ClassTeacher = newModel.C_ClassTeacher;
                    existingSchoolclass.C_Department = newModel.C_Department;

                    _context.Update(existingSchoolclass);
                    await _context.SaveChangesAsync();
                }
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError(ex, "Methode EditAsync(long, Schoolclass) ist fehlgeschlagen!");
                throw new ServiceLayerException("Methode EditAsync(long, Schoolclass) ist fehlgeschlagen!", ex);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EntityExists(newModel.C_ID))
                {
                    throw new ServiceLayerException("Datensatz wurde nicht gefunden!");
                }
                throw;
            }
            return newModel;
        }

        public async Task DeleteAsync(string id)
        {
            try
            {
                var schoolclass = await _context.Schoolclass.FindAsync(id);
                _context.Schoolclass.Remove(schoolclass);
                await _context.SaveChangesAsync();
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError(ex, "Methode DeleteAsync(long) ist fehlgeschlagen!");
                throw new ServiceLayerException("Methode DeleteAsync(long) ist fehlgeschlagen!", ex);
            }
        }

        private bool EntityExists(string id)
        {
            return _context.Schoolclass.Any(e => e.C_ID == id);
        }
    }
}
