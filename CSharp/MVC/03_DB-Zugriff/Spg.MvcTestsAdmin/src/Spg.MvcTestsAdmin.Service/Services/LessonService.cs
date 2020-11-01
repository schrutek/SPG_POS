using Microsoft.EntityFrameworkCore;
using Spg.MvcTestsAdmin.Service.Interfaces;
using Spg.MvcTestsAdmin.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.MvcTestsAdmin.Service.Services
{
    public class LessonService : ILessonService
    {
        private TestsAdministratorContext _context;

        public LessonService(TestsAdministratorContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Lesson>> GetAllAsync()
        {
            return await _context.Lesson
                .Include(l=>l.L_TeacherNavigation)
                .ToListAsync();
        }

        public async Task<Lesson> GetSingleOrDefaultAsync(long id)
        {
            return await _context.Lesson
                .Include(l => l.L_TeacherNavigation)
                .FirstOrDefaultAsync();
        }

        public async Task<Lesson> CreateAsync(Lesson newModel)
        {
            _context.Add(newModel);
            await _context.SaveChangesAsync();
            return newModel;
        }

        public async Task<Lesson> EditAsync(Guid id, Lesson newModel)
        {
            try
            {
                _context.Update(newModel);
                await _context.SaveChangesAsync();
                return newModel;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!EntityExists(newModel.L_ID))
                {
                    throw new KeyNotFoundException("Datensatz wurde nicht gefunden!");
                }
                throw;
            }
        }

        public async Task DeleteAsync(Guid? id)
        {
            Lesson lesson = await _context.Lesson.FindAsync(id);
            _context.Remove(lesson);
            await _context.SaveChangesAsync();
        }

        private bool EntityExists(long id)
        {
            return _context.Lesson.Any(l => l.L_ID == id);
        }
    }
}
