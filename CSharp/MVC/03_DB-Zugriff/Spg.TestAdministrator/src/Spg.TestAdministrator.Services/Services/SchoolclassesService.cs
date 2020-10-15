using Spg.TestAdministrator.Services.Interfaces;
using Spg.TestAdministrator.Services.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Spg.TestAdministrator.Services.Services
{
    public class SchoolclassesService : ISchoolclassesService
    {
        public Task<IEnumerable<Schoolclass>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Schoolclass>> GetSingleOrDefault(long id)
        {
            throw new NotImplementedException();
        }
    }
}
