using System;
using System.Linq;
using System.Collections;

namespace Comads
{
    class Program
    {
        static void Main(string[] args)
        {
            var smth = new[] { 43, 335, 34 }.Select(n => n.Return().Bind(y => y.ToString().Return()));
        }
    }
}
