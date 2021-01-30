using Spg.Generics.Tests.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Spg.Generics.Tests.Repository
{
    public class PupilRepository
    {
        private List<Pupil> _dbSet;

        public PupilRepository()
        {
            _dbSet = new MockDatabaseContext().ListPupils();
        }

        public Pupil GetById(int id)
        {
            foreach (Pupil item in _dbSet)
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
