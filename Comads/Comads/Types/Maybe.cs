using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Comads
{
    public class Maybe<T>
    {
        public readonly T value;

        public Maybe(T someValue)
        {
            if (!EqualityComparer<T>.Default.Equals(someValue, default(T)))
                value = someValue;
        }

        private Maybe()
        {
        }



        public Maybe<TO> Then<TO>(Func<T, TO> func)
        {
            return new Maybe<TO>(func(value));
        }

        public static Maybe<T> None() => new Maybe<T>();
    }

    public static class MaybeExtensions
    {
        public static Maybe<T> Return<T>(this T value)
        {
            return !EqualityComparer<T>.Default.Equals(value, default(T)) ? new Maybe<T>(value) : Maybe<T>.None();
        }

        public static IEnumerable<B> Selecty<A, B>(this IEnumerable<A> first, Func<A, IEnumerable<B>> selector)
        {
            foreach (var outer in first)
            {
                foreach (var inner in selector?.Invoke(outer))
                {
                    yield return inner;
                }
            }
        }

        public static int Sum(this IEnumerable<int> seq)
        {
            return seq.Aggregate(0,(a, b) => a + b);
        }

        public static int Product(this IEnumerable<int> seq)
        {
            return seq.Aggregate(1, (a, b) => a * b);
        }
    }
}
