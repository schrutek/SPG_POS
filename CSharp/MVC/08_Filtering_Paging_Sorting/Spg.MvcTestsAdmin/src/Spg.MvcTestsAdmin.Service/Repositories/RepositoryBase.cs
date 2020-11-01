using Microsoft.EntityFrameworkCore;
using Spg.MvcTestsAdmin.Service.Interfaces;
using Spg.MvcTestsAdmin.Service.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Spg.MvcTestsAdmin.Service.Repositories
{
    public class RepositoryBase<TEntity, TId> : ReadOnlyRepositoryBase<TEntity>, IRepositoryBase<TEntity, TId>
        where TEntity : class, new()
    {
        public RepositoryBase(TestsAdministratorContext context)
            : base(context)
        { }

        public async Task<TEntity> CreateAsync(TEntity newModel)
        {
            try
            {
                Context.Add(newModel);
                await Context.SaveChangesAsync();
            }
            catch (InvalidOperationException ex)
            {
                throw ex;
            }
            return newModel;
        }

        public async Task<TEntity> EditAsync(TEntity newModel, 
            Func<TEntity, bool> entityExistsPredicate, 
            Func<TEntity, TEntity> setEntityData)
        {
            TEntity model = setEntityData(newModel);

            try
            {
                Context.Update(newModel);
                await Context.SaveChangesAsync();
                return newModel;
            }
            catch (InvalidOperationException ex)
            {
                if (entityExistsPredicate(newModel))
                {
                    throw new KeyNotFoundException("Datensatz wurde nicht gefunden!");
                }
                throw ex;
            }
        }

        public async Task DeleteAsync(TId id)
        {
            object lesson = await Context.Lesson.FindAsync(id);
            if (lesson != null)
            {
                try
                {
                    Context.Remove(lesson);
                    await Context.SaveChangesAsync();
                }
                catch (InvalidOperationException ex)
                {
                    throw ex;
                }
            }
            else
            {
                throw new KeyNotFoundException("Datensatz wurde nicht gefunden!");
            }
        }
    }
}
