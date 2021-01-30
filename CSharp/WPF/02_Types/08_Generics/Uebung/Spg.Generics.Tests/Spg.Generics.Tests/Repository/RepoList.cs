using Spg.Generics.Tests.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Spg.Generics.Tests.Repository
{
    public class RepoList<T> : List<T>
        where T: EntityBase, new()
    {

    }
}
