using System;
using System.Collections.Generic;
using System.Text;

namespace SPG.LambdaTutorial
{
    public delegate TResult MyFunc<T, TResult>(T arg1);
}
