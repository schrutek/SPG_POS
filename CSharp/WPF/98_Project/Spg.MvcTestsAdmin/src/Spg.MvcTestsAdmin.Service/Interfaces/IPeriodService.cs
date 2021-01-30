using Spg.MvcTestsAdmin.Service.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Spg.MvcTestsAdmin.Service.Interfaces
{
    public interface IPeriodService
    {
        Task<IEnumerable<Period>> GetAllAsync();

        Task<Period> GetSingleOrDefaultAsync(long id);

        Task<Period> Create(Period newModel);

        Task<Period> Edit(long id, Period newModel);

        Task<Period> Delete(long id);
    }
}
