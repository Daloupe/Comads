using System;

namespace Comads
{
    public static class FuncExt
    {
        public static Func<A, bool> And<A>(this Predicate<A> first, Predicate<A> second)
        {
            return (value) => !(first is null) && first(value) && !(second is null) && second(value);
        }


        /// <summary>
        /// Then aka Bind.
        /// </summary>
        /// 
        public static Func<TIn, TOut> Then<TIn, a, TOut>(this Func<TIn, Maybe<a>> source, Func<Maybe<a>, TOut> next)
            => (value)
            => next(source(value));

        public static Func<Maybe<TIn>, Maybe<TOut>> Then<TIn, a, TOut>(this Func<Maybe<TIn>, a> source, Func<a, Maybe<TOut>> next)
            => (value)
            => next(source(value));

        public static Func<TIn, TOut> Then<TIn, a, TOut>(this Func<TIn, a> source, Func<a, TOut> next)
            => (value)
            => next(source(value));

        public static Func<TIn, a, TOut> Then<TIn, a, b, TOut>(this Func<TIn, a, b> source, Func<b, a, TOut> next)
            => (value, v2)
            => next(source(value, v2), v2);

        public static Func<TIn, a, TOut>Then<TIn, a, TOut>(this Func<TIn, a, a> source, Func<TIn, a, TOut> next)
            => (value, v2)
            => next(value, source(value, v2));
    }
}
