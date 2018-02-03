using System;
using System.Collections.Generic;
using System.Text;

namespace Comads.Types
{
    public class Monad<T>
    {
        public readonly T value;

        public Monad(T someValue)
        {
                value = someValue;
        }

        //public static Maybe<T> None() => new Maybe<T>();

        //Appl
        //Lift

    }

    public static class MaybeExtensions
    {
        public static Monad<B> Bind<A, B>(this A value, Func<A, Monad<B>> func)
        {
            return func(value);
        }
        
        public static Monad<T> Unit<T>(this T value)
        {
            return new Monad<T>(value);
        }

        //public static Monad<B> SelectMany<A, B>(this Monad<A> first, Func<A, Monad<B>> selector)
        //{
        //    return 
        //}
        public static Func<T, Monad<V>> Compose<T, U, V>(this Func<U, Monad<V>> f, Func<T, Monad<U>> g)
        {
            return x => Bind(g(x), v => f(v.value));
        }

        public static Func<T, Monad<V>> Compose<T, U, V>(this Func<U, V> f, Func<T, Monad<U>> g)
        {
            return x => Bind(g(x), v=> Unit(f(v.value)));
        }
    }
}
