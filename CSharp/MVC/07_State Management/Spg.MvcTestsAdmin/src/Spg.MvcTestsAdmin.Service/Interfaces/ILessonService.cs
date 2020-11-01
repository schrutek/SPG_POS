using Spg.MvcTestsAdmin.Service.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Spg.MvcTestsAdmin.Service.Interfaces
{
    public interface ILessonService
    {
        Task<IEnumerable<Lesson>> GetAllAsync();

        Task<IEnumerable<Lesson>> GetAllBySchoolclassAsync(string schoolclassId);

        Task<Lesson> GetSingleOrDefaultAsync(long id);

        Task<Lesson> CreateAsync(Lesson newModel);

        Task<Lesson> EditAsync(long id, Lesson newModel);

        Task DeleteAsync(long id);
    }
}
