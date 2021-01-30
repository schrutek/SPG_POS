using Spg.Generics.Tests.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Spg.Generics.Tests.Repository
{
    public class RepositoryBase<T>
        where T : EntityBase, new()
    {
        private List<T> _dbSet;

        public RepositoryBase()
        {
            _dbSet = (List<T>)new MockDatabaseContext().GetDbSet<T>();
        }

        public T GetById(int id)
        {
            foreach (T item in _dbSet)
            {
                if (item.Id == id)
                {
                    return item;
                }
            }
            throw new KeyNotFoundException("Entität nicht gefunden!");
        }

        public T Create()
        {
            return new T() { Id = 99 };
        }
    }
}
