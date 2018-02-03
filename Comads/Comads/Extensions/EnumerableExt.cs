using System;
using System.Collections.Generic;

namespace Comads
{
    public static class EnumerableExt
    {
        /// <summary>
        /// Lifts single T into IEnumerable<T>
        /// </summary>
        public static IEnumerable<T> Lift<T>(this T source)
        {
            yield return source;
        }

        /// <summary>
        /// Lifts (T, T) into IEnumerable<T>
        /// </summary>
        public static IEnumerable<T> Lift<T>(this (T, T) source)
        {
            yield return source.Item1;
            yield return source.Item2;
        }

        /// <summary>
        /// Lifts (T, T, T) into IEnumerable<T>
        /// </summary>
        public static IEnumerable<T> Lift<T>(this (T, T, T) source)
        {
            yield return source.Item1;
            yield return source.Item2;
            yield return source.Item3;
        }

        /// <summary>
        /// Lifts (T, T, T, T) into IEnumerable<T>
        /// </summary>
        public static IEnumerable<T> Lift<T>(this (T, T, T, T) source)
        {
            yield return source.Item1;
            yield return source.Item2;
            yield return source.Item3;
            yield return source.Item4;
        }

        /// <summary>
        /// Creates Key Values Pairs
        /// </summary>
        public static IEnumerable<KeyValuePair<T, U>> ToKVP<T, U, V>(this IEnumerable<V> source, Func<V, T> keySelector, Func<V, U> valueSelector)
        {
            foreach (var item in source)
            {
                yield return new KeyValuePair<T, U>(keySelector(item), valueSelector(item));
            }
        }
    }

}
