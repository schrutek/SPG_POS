using Spg.MvcTestsAdmin.Service.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Spg.MvcTestsAdmin.Service.Interfaces
{
    public interface ITeacherService
    {
        Task<IEnumerable<Teacher>> GetAllAsync();

        Task<Teacher> GetSingleOrDefaultAsync(string id);

        Task<Teacher> Create(Teacher newModel);

        Task<Teacher> Edit(string id, Teacher newModel);

        Task<Teacher> Delete(string id);
    }
}
