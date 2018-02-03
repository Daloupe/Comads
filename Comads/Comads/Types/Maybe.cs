using System;
using System.Collections.Generic;
using System.Text;

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

        public Maybe<TO> Bind<TO>(Func<T, Maybe<TO>> func)
        {
            return !EqualityComparer<T>.Default.Equals(value, default(T)) ? func?.Invoke(value) : Maybe<TO>.None();
        }

        public static Maybe<T> None() => new Maybe<T>();
    }

    public static class MaybeExtensions
    {
        public static Maybe<T> Return<T>(this T value)
        {
            return !EqualityComparer<T>.Default.Equals(value, default(T)) ? new Maybe<T>(value) : Maybe<T>.None();
        }

        public static IEnumerable<B> SelectMany<A, B>(
            this IEnumerable<Maybe<A>> first,
            Func<A, IEnumerable<B>> selector)
        {
            foreach (var outer in first)
            {
                foreach (var inner in selector(outer.value))
                {
                    yield return inner;
                }
            }
        }
    }
}
