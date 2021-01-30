using System;
using System.Collections.Generic;
using System.Text;

namespace Spg.Delegates.Tests
{
    public delegate TResult MyFunction<in T1, out TResult>(T1 arg1);
    public delegate TResult MyFunction<in T1, in T2, out TResult>(T1 arg1, T2 arg2);
    public delegate TResult MyFunction<in T1, in T2, in T3, out TResult>(T1 arg1, T2 arg2, T3 arg3);
    public delegate TResult MyFunction<in T1, in T2, in T3, in T4, out TResult>(T1 arg1, T2 arg2, T3 arg3, T4 arg4);
    public delegate TResult MyFunction<in T1, in T2, in T3, in T4, in T5, out TResult>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5);
    // usw. bis (Hausnummer) 20 Parameter
}
