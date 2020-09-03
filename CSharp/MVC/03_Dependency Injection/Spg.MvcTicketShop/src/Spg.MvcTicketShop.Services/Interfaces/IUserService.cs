using Spg.MvcTicketShop.Services.Dtos;
using Spg.MvcTicketShop.Services.Helper;
using Spg.MvcTicketShop.Services.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Spg.MvcTicketShop.Services.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetAllAsync();

        Task<IEnumerable<UserDto>> GetAllAsync(string filter, string currentFilter, string sortOrder, int? pageNumber);

        Task<UserDto> GetSingleOrDefaultAsync(Guid id);

        Task<Users> CreateAsync(Users newModel);

        Task<Users> EditAsync(Guid id, Users newModel);

        Task<Users> DeleteAsync(Guid? id);
    }
}
