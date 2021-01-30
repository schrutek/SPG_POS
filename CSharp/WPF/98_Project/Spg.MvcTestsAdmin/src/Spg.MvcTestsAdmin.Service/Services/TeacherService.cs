using Microsoft.EntityFrameworkCore;
using Spg.MvcTestsAdmin.Service.Infrastructure;
using Spg.MvcTestsAdmin.Service.Interfaces;
using Spg.MvcTestsAdmin.Service.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Spg.MvcTestsAdmin.Service.Services
{
    public class TeacherService : ITeacherService
    {
        private TestsAdministratorContext _context;

        public TeacherService(TestsAdministratorContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Teacher>> GetAllAsync()
        {
            return await _context.Teacher
                .ToListAsync();
        }

        public async Task<Models.Teacher> GetSingleOrDefaultAsync(string id)
        {
            return await _context.Teacher
                .FirstOrDefaultAsync(t => t.T_ID == id);
        }

        public async Task<Models.Teacher> Create(Teacher newModel)
        {
            throw new NotImplementedException();
        }

        public async Task<Models.Teacher> Delete(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<Models.Teacher> Edit(string id, Teacher newModel)
        {
            throw new NotImplementedException();
        }
    }
}
