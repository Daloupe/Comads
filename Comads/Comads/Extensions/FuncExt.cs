using System;

namespace Comads
{
    public static class FuncExt
    {
        /// <summary>
        /// Combines 2 predicates to be called in order. Short circuiting: If the first fails, the second isn't invoked.
        /// </summary>
        public static Func<A, bool> And<A>(this Predicate<A> first, Predicate<A> second)
        {
            return (value) => first(value) && second(value);
        }
    }
}
