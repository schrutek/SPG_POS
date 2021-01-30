using System;

namespace Spg.Delegates.Tests
{
    class Program
    {
        static void Main(string[] args)
        {
            WithDelegate delegateTests = new WithDelegate();
            delegateTests.DoSomeWork();

            GenericDelegate genericDelegate = new GenericDelegate();
            genericDelegate.DoSomeWork();
        }
    }
}
