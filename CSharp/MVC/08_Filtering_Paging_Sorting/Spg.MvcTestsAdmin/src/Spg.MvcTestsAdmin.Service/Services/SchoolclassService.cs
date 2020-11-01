using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Spg.MvcTestsAdmin.Service.Interfaces;
using Spg.MvcTestsAdmin.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Spg.MvcTestsAdmin.Service.Services
{
    public class SchoolclassService : ISchoolclassService
    {
        private readonly IRepositoryBase<Schoolclass, string> _repository;
        private readonly ILogger<SchoolclassService> _logger;

        public SchoolclassService(IRepositoryBase<Schoolclass, string> repository, ILogger<SchoolclassService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<IEnumerable<Schoolclass>> GetAllAsync()
        {
            return await _repository.GetAll(
                includeNavigationProperty: "C_ClassTeacherNavigation;Lesson"
            ).ToListAsync();
        }

        public async Task<Schoolclass> GetSingleOrDefaultAsync(string id)
        {
            return await _repository.GetSingleAsync(
                filter: c => c.C_ID == id,
                includeNavigationProperty: "C_ClassTeacherNavigation;Lesson"
            );
        }

        public async Task<Schoolclass> CreateAsync(Schoolclass newModel)
        {
            try
            {
                return await _repository.CreateAsync(newModel);
            }
            catch (KeyNotFoundException ex)
            {
                throw new ServiceLayerException("Methode EditAsync(long, Lesson) ist fehlgeschlagen!", ex);
            }
            catch (InvalidOperationException ex)
            {
                throw new ServiceLayerException("Methode EditAsync(long, Lesson) ist fehlgeschlagen!", ex);
            }
        }

        public async Task<Schoolclass> EditAsync(string id, Schoolclass newModel)
        {
            try
            {
                return await _repository.EditAsync(newModel, c => !EntityExists(newModel.C_ID), c =>
                {
                    return new Schoolclass()
                    {
                        C_ID = newModel.C_ID,
                        C_ClassTeacher = newModel.C_ClassTeacher,
                        C_Department = newModel.C_Department,
                    };
                });
            }
            catch (KeyNotFoundException ex)
            {
                throw new ServiceLayerException("Methode EditAsync(long, Lesson) ist fehlgeschlagen!", ex);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new ServiceLayerException("Methode EditAsync(long, Lesson) ist fehlgeschlagen!", ex);
            }
            catch (InvalidOperationException ex)
            {
                throw new ServiceLayerException("Methode EditAsync(long, Lesson) ist fehlgeschlagen!", ex);
            }
        }

        public async Task DeleteAsync(string id)
        {
            try
            {
                await _repository.DeleteAsync(id);
            }
            catch (KeyNotFoundException ex)
            {
                throw new ServiceLayerException("Methode EditAsync(long, Lesson) ist fehlgeschlagen!", ex);
            }
            catch (InvalidOperationException ex)
            {
                throw new ServiceLayerException("Methode EditAsync(long, Lesson) ist fehlgeschlagen!", ex);
            }
        }

        private bool EntityExists(string id)
        {
            return _repository.Context.Schoolclass.Any(e => e.C_ID == id);
        }
    }
}
