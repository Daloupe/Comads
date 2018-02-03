using System;
using System.Collections.Generic;

namespace Comads
{
    public static partial class GenericExtentions
    {
        public static bool IsEmpty<T>(this T source)
        {
            if (ReferenceEquals(source, null)) return true;

            switch (source)
            {
                case object o when o == null:
                    return true;
                case Array a when a.Length == 0:
                    return true;
                case List<object> a when a.Count == 0:
                    return true;
                case String s when string.IsNullOrEmpty(s):
                    return true;
            }

            return false;
        }
    }
}
