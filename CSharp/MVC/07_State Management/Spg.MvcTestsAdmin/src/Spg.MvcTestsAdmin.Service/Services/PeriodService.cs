using Microsoft.EntityFrameworkCore;
using Spg.MvcTestsAdmin.Service.Interfaces;
using Spg.MvcTestsAdmin.Service.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Spg.MvcTestsAdmin.Service.Services
{
    public class PeriodService : IPeriodService
    {
        private TestsAdministratorContext _context;

        public PeriodService(TestsAdministratorContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Period>> GetAllAsync()
        {
            return await _context.Period
                .ToListAsync();
        }

        public async Task<Models.Period> GetSingleOrDefaultAsync(long id)
        {
            return await _context.Period
                .FirstOrDefaultAsync(p => p.P_Nr == id);
        }

        public async Task<Models.Period> Create(Period newModel)
        {
            throw new NotImplementedException();
        }

        public async Task<Models.Period> Delete(long id)
        {
            throw new NotImplementedException();
        }

        public async Task<Models.Period> Edit(long id, Period newModel)
        {
            throw new NotImplementedException();
        }
    }
}
