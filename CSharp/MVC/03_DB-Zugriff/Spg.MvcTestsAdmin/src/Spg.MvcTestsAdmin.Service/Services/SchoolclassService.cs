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
    public class SchoolclassService : ISchoolclassService
    {
        private readonly TestsAdministratorContext _context;

        public SchoolclassService(TestsAdministratorContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Schoolclass>> GetAllAsync()
        {
            return await _context.Schoolclass
                .Include(s => s.C_ClassTeacherNavigation)
                .ToListAsync();
        }

        public async Task<Schoolclass> GetSingleOrDefaultAsync(string id)
        {
            return await _context.Schoolclass
                .Include(s => s.C_ClassTeacherNavigation)
                .FirstOrDefaultAsync(c => c.C_ID == id);
        }

        public async Task<Schoolclass> CreateAsync(Schoolclass newModel)
        {
            _context.Add(newModel);
            await _context.SaveChangesAsync();

            return newModel;
        }

        public async Task<Schoolclass> EditAsync(Guid id, Schoolclass newModel)
        {
            try
            {
                _context.Update(newModel);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EntityExists(newModel.C_ID))
                {
                    throw new KeyNotFoundException("Datensatz wurde nicht gefunden!");
                }
                throw;
            }
            return newModel;
        }

        public async Task DeleteAsync(Guid? id)
        {
            var schoolclass = await _context.Schoolclass.FindAsync(id);
            _context.Schoolclass.Remove(schoolclass);
            await _context.SaveChangesAsync();
        }

        private bool EntityExists(string id)
        {
            return _context.Schoolclass.Any(e => e.C_ID == id);
        }
    }
}
