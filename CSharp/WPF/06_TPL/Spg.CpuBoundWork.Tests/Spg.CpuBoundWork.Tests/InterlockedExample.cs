using System;
using System.Threading;
using System.Collections.Generic;
using System.Text;

namespace Spg.CpuBoundWork.Tests
{
    public class InterlockedExample
    {
        public int _value;

        public void IncreaseNumberParallel()
        {
            Thread thread1 = new Thread(new ThreadStart(AddOne));
            Thread thread2 = new Thread(new ThreadStart(AddOne));
            thread1.Start();
            thread2.Start();
            thread1.Join();
            thread2.Join();
            Console.WriteLine(_value);
        }

        private void AddOne()
        {
            // Erhöht den Inhalt in _value um 1
            Interlocked.Add(ref _value, 1);
        }
    }
}
