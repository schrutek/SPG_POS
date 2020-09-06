using System;
using System.Collections.Generic;
using System.Text;

namespace Spg.Basics.App.Classes
{
    public struct Point
    {
        /// <summary>
        /// Feld x
        /// </summary>
        public int x;

        /// <summary>
        /// Feld y
        /// </summary>
        public int y;

        /// <summary>
        /// Structs können keine Default-Konstruktor enthalten.
        /// </summary>
        ////public Point()
        ////{ }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="x">Parameter x</param>
        /// <param name="y">Parameter y</param>
        public Point(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        /// <summary>
        /// Methode
        /// </summary>
        /// <param name="a">Parameter a</param>
        /// <param name="b">Parameter b</param>
        public void MoveTo(int a, int b)
        {
            x = a;
            y = b;
        }
    }
}
