using Spg.TestAdministrator.Services.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Spg.TestAdministrator.Services.Interfaces
{
    public interface ISchoolclassesService
    {
        Task<IEnumerable<Schoolclass>> GetAllAsync();

        Task<IEnumerable<Schoolclass>> GetSingleOrDefault(long id);
    }
}
