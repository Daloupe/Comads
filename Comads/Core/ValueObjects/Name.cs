using System;
using System.Collections.Generic;
using Core.SharedKernel;

namespace Core.ValueObjects
{
    public class Name : ValueObject
    {
        public string Actual { get; set; }
        public string Former { get; set; }
        public string Preffered { get; set; }
    }
}
