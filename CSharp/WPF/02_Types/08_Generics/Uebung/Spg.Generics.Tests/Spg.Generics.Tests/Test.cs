using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Spg.Generics.Tests
{
    class Test : IEquatable<Test>
    {
        public bool Equals([AllowNull] Test other)
        {
            throw new NotImplementedException();
        }

        public void MyTest()
        {
        }
    }

    class TestConsumer
    {
        public void DoSomthingWithTest(IEquatable<Test> t)
        {
            int x = t.GetHashCode();
        }
    }

}
