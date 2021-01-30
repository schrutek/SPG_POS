using Microsoft.EntityFrameworkCore;
using Spg.MvcTestsAdmin.Service.Infrastructure;
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

        public async Task<IEnumerable<Lesson>> GetAllBySchoolclassAsync(string schoolclassId)
        {
            return await _context.Lesson
                .Where(l=>l.L_ClassNavigation.C_ID == schoolclassId)
                .Include(l => l.L_TeacherNavigation)
                .ToListAsync();
        }

        public async Task<Lesson> GetSingleOrDefaultAsync(long id)
        {
            return await _context.Lesson
                .Include(l => l.L_TeacherNavigation)
                .FirstOrDefaultAsync(l => l.L_ID == id);
        }

        public async Task<Lesson> CreateAsync(Lesson newModel)
        {
            try
            {
                _context.Add(newModel);
                await _context.SaveChangesAsync();
            }
            catch (InvalidOperationException ex)
            {
                throw new ServiceLayerException("Methode CreateAsync(Lesson) ist fehlgeschlagen!", ex);
            }
            return newModel;
        }

        public async Task<Lesson> EditAsync(long id, Lesson newModel)
        {
            if (id != newModel.L_ID)
            {
                throw new KeyNotFoundException("Datensatz wurde nicht gefunden!");
            }

            try
            {
                Lesson existingLesson = _context.Lesson.Find(id);
                if (existingLesson == null)
                {
                    throw new KeyNotFoundException("Datensatz wurde nicht gefunden!");
                }
                else
                {
                    existingLesson.L_Class = newModel.L_Class;
                    existingLesson.L_Day = newModel.L_Day;
                    existingLesson.L_Hour = newModel.L_Hour;
                    existingLesson.L_Room = newModel.L_Room;
                    existingLesson.L_Subject = newModel.L_Subject;
                    existingLesson.L_Teacher = newModel.L_Teacher;
                    existingLesson.L_Untis_ID = newModel.L_Untis_ID;

                    _context.Update(existingLesson);
                    await _context.SaveChangesAsync();
                }
            }
            catch (InvalidOperationException ex)
            {
                throw new ServiceLayerException("Methode EditAsync(long, Lesson) ist fehlgeschlagen!", ex);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EntityExists(newModel.L_ID))
                {
                    throw new ServiceLayerException("Datensatz wurde nicht gefunden!");
                }
                throw;
            }
            return newModel;
        }

        public async Task DeleteAsync(long id)
        {
            try
            {
                Lesson lesson = await _context.Lesson.FindAsync(id);
                _context.Remove(lesson);
                await _context.SaveChangesAsync();
            }
            catch (InvalidOperationException ex)
            {
                throw new ServiceLayerException("Methode DeleteAsync(long) ist fehlgeschlagen!", ex);
            }
        }

        private bool EntityExists(long id)
        {
            return _context.Lesson.Any(l => l.L_ID == id);
        }
    }
}
