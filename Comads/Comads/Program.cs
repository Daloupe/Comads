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


            var people = PersonGenerator.GenerateFamilies(7, 4);
            var a = people.ElementAt(0);
            a.Address = null;
            var b = people.ElementAt(1);

            var matcher = a.AsReadable().FillBlanks(b);

            var peoples = people.AsReadable()[new string[] { "FirstName", "MiddleName", "LastName" }];


        }
    }
}
