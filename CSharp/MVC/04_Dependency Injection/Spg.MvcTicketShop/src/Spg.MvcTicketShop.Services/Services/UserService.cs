using Microsoft.EntityFrameworkCore;
using Spg.MvcTicketShop.Services.Dtos;
using Spg.MvcTicketShop.Services.Helper;
using Spg.MvcTicketShop.Services.Interfaces;
using Spg.MvcTicketShop.Services.Models;
using Spg.MvcTicketShop.Services.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Spg.MvcTicketShop.Services.Services
{
    public class UserService : IUserService
    {
        private readonly EFRepository<Users> _repository;

        public UserService(EFRepository<Users> repository)
        {
            _repository = repository;
        }

        public Task<IEnumerable<UserDto>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<UserDto>> GetAllAsync(string filter, string currentFilter, string sortOrder, int? pageNumber)
        {
            if (filter != null)
            {
                pageNumber = 1;
            }
            else
            {
                filter = currentFilter;
            }

            Func<IQueryable<Users>, IOrderedQueryable<Users>> orderBy = null;
            if (!String.IsNullOrEmpty(sortOrder))
            {
                switch (sortOrder)
                {
                    case "lastname":
                        orderBy = c => c.OrderBy(s => s.LastName);
                        break;
                    case "lastname_desc":
                        orderBy = c => c.OrderByDescending(s => s.LastName);
                        break;
                    case "email":
                        orderBy = c => c.OrderBy(s => s.EMail);
                        break;
                    case "email_desc":
                        orderBy = c => c.OrderByDescending(s => s.EMail);
                        break;
                    case "firstname_desc":
                        orderBy = c => c.OrderByDescending(s => s.FirstName);
                        break;
                    default:
                        orderBy = c => c.OrderBy(s => s.FirstName);
                        break;
                }
            }

            Expression<Func<Users, bool>> filterQuery = null;
            if (!String.IsNullOrEmpty(filter))
            {
                filterQuery = c => c.FirstName.Contains(filter) || c.LastName.Contains(filter);
            }

            bool hasMore = false;
            IQueryable<UserDto> result = _repository.GetQueryable(filterQuery, currentFilter, orderBy, out hasMore, 0, 100)
                .Select(c => new UserDto()
                {
                    EMail = c.EMail,
                    FirstName = c.FirstName,
                    LaseChangeUserId = c.LaseChangeUserId,
                    LastChangeDate = c.LastChangeDate,
                    LastName = c.LastName,
                    RegisterDateTime = c.RegisterDateTime,
                    UserId = c.UserId
                });

            int pageSize = 20;
            return await PaginatedList<UserDto>.CreateAsync(result.AsNoTracking(), pageNumber ?? 1, pageSize, hasMore);
        }

        public Task<UserDto> GetSingleOrDefaultAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Users> CreateAsync(Users newModel)
        {
            throw new NotImplementedException();
        }

        public Task<Users> DeleteAsync(Guid? id)
        {
            throw new NotImplementedException();
        }

        public Task<Users> EditAsync(Guid id, Users newModel)
        {
            throw new NotImplementedException();
        }
    }
}
