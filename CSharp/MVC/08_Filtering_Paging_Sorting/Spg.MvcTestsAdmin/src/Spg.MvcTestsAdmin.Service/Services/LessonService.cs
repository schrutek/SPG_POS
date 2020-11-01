using Microsoft.EntityFrameworkCore;
using Spg.MvcTestsAdmin.Service.Interfaces;
using Spg.MvcTestsAdmin.Service.Models;
using Spg.MvcTestsAdmin.Service.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Spg.MvcTestsAdmin.Service.Services
{
    public class LessonService : ILessonService
    {
        private readonly IRepositoryBase<Lesson, long> _repository;

        public LessonService(IRepositoryBase<Lesson, long> repository)
        {
            _repository = repository;
        }

        public async Task<PagenatedList<Lesson>> GetAllAsync(int pageIndex, string filter)
        {
            Expression<Func<Lesson, bool>> filterExpression = null;
            if (!string.IsNullOrEmpty(filter))
            {
                filterExpression = l => l.L_Teacher.Contains(filter);
            }

            IQueryable<Lesson> result = _repository.Get(
                filter: filterExpression,
                includeNavigationProperty: "L_ClassNavigation;L_TeacherNavigation"
            );
            PagenatedList<Lesson> page = await PagenatedList<Lesson>.CreateAsync(result, pageIndex, 10);
            return page;
        }

        public async Task<PagenatedList<Lesson>> GetAllBySchoolclassAsync(string schoolclassId, int pageIndex, string filter)
        {
            Expression<Func<Lesson, bool>> filterExpression = null;
            if (!string.IsNullOrEmpty(filter))
            {
                filterExpression = l => l.L_ClassNavigation.C_ID == schoolclassId
                    && l.L_Teacher.Contains(filter);
            }
            else
            {
                filterExpression = l => l.L_ClassNavigation.C_ID == schoolclassId;
            }

            IQueryable<Lesson> result = _repository.Get(
                filter: filterExpression,
                includeNavigationProperty: "L_ClassNavigation;L_TeacherNavigation"
            );
            PagenatedList<Lesson> page = await PagenatedList<Lesson>.CreateAsync(result, pageIndex, 10);
            return page;
        }

        public async Task<Lesson> GetSingleOrDefaultAsync(long id)
        {
            return await _repository.GetSingleAsync(
                filter: l => l.L_ID == id,
                includeNavigationProperty: "L_ClassNavigation;L_TeacherNavigation");
        }

        public async Task<Lesson> CreateAsync(Lesson newModel)
        {
            try
            {
                return await _repository.CreateAsync(newModel);
            }
            catch (KeyNotFoundException ex)
            {
                throw new ServiceLayerException("Methode CreateAsync(Lesson) ist fehlgeschlagen!", ex);
            }
            catch (InvalidOperationException ex)
            {
                throw new ServiceLayerException("Methode CreateAsync(Lesson) ist fehlgeschlagen!", ex);
            }
        }

        public async Task<Lesson> EditAsync(long id, Lesson newModel)
        {
            try
            {
                return await _repository.EditAsync(newModel, l => !EntityExists(newModel.L_ID), l =>
                {
                    return new Lesson()
                    {
                        L_ID = newModel.L_ID,
                        L_Class = newModel.L_Class,
                        L_Day = newModel.L_Day,
                        L_Hour = newModel.L_Hour,
                        L_Room = newModel.L_Room,
                        L_Subject = newModel.L_Subject,
                        L_Teacher = newModel.L_Teacher,
                        L_Untis_ID = newModel.L_Untis_ID
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

        public async Task DeleteAsync(long id)
        {
            try
            {
                await _repository.DeleteAsync(id);
            }
            catch (KeyNotFoundException ex)
            {
                throw new ServiceLayerException("Methode DeleteAsync(long) ist fehlgeschlagen!", ex);
            }
            catch (InvalidOperationException ex)
            {
                throw new ServiceLayerException("Methode DeleteAsync(long) ist fehlgeschlagen!", ex);
            }
        }

        private bool EntityExists(long id)
        {
            return _repository.Context.Lesson.Any(l => l.L_ID == id);
        }
    }
}
