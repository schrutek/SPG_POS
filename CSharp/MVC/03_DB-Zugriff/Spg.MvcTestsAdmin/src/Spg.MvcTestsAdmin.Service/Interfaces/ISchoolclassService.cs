using Spg.MvcTestsAdmin.Service.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Spg.MvcTestsAdmin.Service.Interfaces
{
    public interface ISchoolclassService
    {
        Task<IEnumerable<Schoolclass>> GetAllAsync();

        Task<Schoolclass> GetSingleOrDefaultAsync(string id);

        Task<Schoolclass> CreateAsync(Schoolclass newModel);

        Task<Schoolclass> EditAsync(Guid id, Schoolclass newModel);

        Task DeleteAsync(Guid? id);
    }
}
