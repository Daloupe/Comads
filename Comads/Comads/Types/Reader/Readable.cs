using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace Comads
{

    ///// <summary>
    ///// Static Readers
    ///// </summary>
    ///// <typeparam name="T"></typeparam>
    //public partial class Readable<T>
    //{
        
    //}

    /// <summary>
    /// Readable
    /// </summary>
    public partial class Readable<T> : IEnumerable<T>
    {

        public static readonly ReaderCollection<T> Readers = ReaderFactory<T>.CreateCollection();

        public readonly List<T> Values;

        public Readable(IEnumerable<T> values)
        {
            Values = values.ToList();
        }

        public IEnumerable<ValueObject> this[string prop]
        {
            get => ReadProps(prop.Lift());
        }

        public IEnumerable<ValueObject> this[string[] prop]
        {
            get => ReadProps(prop);
        }

        public IEnumerable<ValueObject> ReadProps(IEnumerable<string> prop)
        {
            var readers = Readers.Where(n => prop.Contains(n.Type.Name)).Select(n => n.Reader);

            return readers.SelectMany(reader => Values.Select(n => new ValueObject(reader?.Invoke(n), n.GetHashCode(), reader.Method.Name)));
        }

        public IEnumerable<ValueObject> ReadAllProps()
        {
            return Readers.SelectMany(reader => Values.Select(n =>ValueObject.Create(reader.Reader)(n)));
        }

        public IEnumerator<T> GetEnumerator() => Values.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => Values.GetEnumerator();
    }

    /// <summary>
    /// Extensions
    /// </summary>
    public static partial class ReadableExt
    {
        public static Readable<T> AsReadable<T>(this T source) => source.Lift().AsReadable();

        public static Readable<T> AsReadable<T>(this (T, T) source) => source.Lift().AsReadable();

        public static Readable<T> AsReadable<T>(this IEnumerable<T> source)
        {
            return new Readable<T>(source);
        }

        /// <summary>
        /// We loop over every reader, and check if we get a non empty value from our sources, as soon as we do, we move to the next reader.#
        /// As the target object is first in the list of sources it will alwasy take precedence.
        /// </summary>
        public static List<ValueObject> FillBlanks<T>(this Readable<T> source, T fillSource)
        {
            source.Values.Add(fillSource);

            var list = new List<ValueObject>();

            foreach (var prop in Readable<T>.Readers)
            {
                foreach (var value in source.Values)
                {
                    var obj = prop.Reader(value);

                    if (obj.IsEmpty()) continue;

                    list.Add(new ValueObject(obj, source.GetHashCode(), prop.Type.Name));

                    break;
                }
            }



            ////var queue = new ConcurrentQueue<ValueObject>();
            ////foreach (var prop in Readable<T>.Readers)
            //Parallel.ForEach(Readable<T>.Readers, prop =>
            //{
            //    if (!prop.Type.PropertyType.IsArray)
            //    {
            //        foreach (var value in source.Values)
            //        {
            //            var obj = prop.Reader(value);

            //            if (obj.IsEmpty())
            //                continue;

            //            queue.Enqueue(new ValueObject(obj, source.GetHashCode(), prop.Type.Name));

            //            break;
            //        }
            //    }
            //    else
            //    {
            //        /// Oh god dont look at meeeeeeeeeeeeeeeeeeeeeee
            //        var t = prop.Type.PropertyType.GetElementType();
            //        var defaultmem = ((PropertyInfo)t.GetDefaultMembers().FirstOrDefault());

            //        if (defaultmem != null)
            //        {
            //            var values = new List<(object, object)>();
            //            foreach (var value in source.Values)
            //            {
            //                var arr = prop.Reader(value) as object[];

            //                if (arr.IsEmpty()) continue;

            //                var vals = arr.Select(n => (n, defaultmem.GetValue(n))).Where(n => !values.Any(m => m.Item2 == n.Item2));

            //                values.AddRange(vals);
            //            }
            //            if (!values.IsEmpty())
            //                queue.Enqueue(new ValueObject(values.Select(n => n.Item1).ToArray(), source.GetHashCode(), prop.Type.PropertyType.Name));
            //        }
            //        else
            //        {
            //            var values = new List<object>();

            //            foreach (var value in source.Values)
            //            {
            //                var arr = prop.Reader(value) as object[];

            //                if (arr.IsEmpty()) continue;

            //                values.AddRange(arr);
            //            }
            //            if (!values.IsEmpty())
            //                queue.Enqueue(new ValueObject(values.ToArray(), source.GetHashCode(), prop.Type.PropertyType.Name));
            //        }
            //    }

            //});

            return list;
        }
    }
}
