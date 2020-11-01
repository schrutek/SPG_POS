using Spg.MvcTestsAdmin.Service.Models;
using Spg.MvcTestsAdmin.Service.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Spg.MvcTestsAdmin.Service.Interfaces
{
    public interface ILessonService
    {
        Task<PagenatedList<Lesson>> GetAllAsync(int pageIndex, string filter);

        Task<PagenatedList<Lesson>> GetAllBySchoolclassAsync(string schoolclassId, int pageIndex, string filter);

        Task<Lesson> GetSingleOrDefaultAsync(long id);

        Task<Lesson> CreateAsync(Lesson newModel);

        Task<Lesson> EditAsync(long id, Lesson newModel);

        Task DeleteAsync(long id);
    }
}
