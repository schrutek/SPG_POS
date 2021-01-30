using Spg.Generics.Tests.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Spg.Generics.Tests.Repository
{
    public class SchoolClassRepository
    {
        private List<SchoolClass> _dbSet;

        public SchoolClassRepository()
        {
            _dbSet = new MockDatabaseContext().ListSchoolClasses();
        }

        public SchoolClass GetById(int id)
        {
            foreach (SchoolClass item in _dbSet)
            {
                if (item.Id == id)
                {
                    return item;
                }
            }
            throw new KeyNotFoundException("Entität nicht gefunden!");
        }
    }
}
