using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Reflection;
using System.Linq.Expressions;

namespace Comads
{
    /// <summary>
    /// Type Setup
    /// </summary>
    static partial class ReaderFactory<TModel>
    {
        static readonly Predicate<PropertyInfo> IgnoreAttributes;
        static readonly Predicate<PropertyInfo> IgnoreArguments;

        static ReaderFactory()
        {
            var AttributesToIgnore = new HashSet<Type>
            {
                //typeof(MongoDB.Bson.Serialization.Attributes.BsonIgnoreAttribute)
            };

            IgnoreAttributes =
                (prop) =>
                !prop.CustomAttributes
                .Any(attr => AttributesToIgnore.Contains(attr.AttributeType));


            var AttributeValuesToIgnore = new HashSet<string>
            {
                "_version"
            };

            IgnoreArguments =
                (prop) =>
                !prop.CustomAttributes
                .SelectMany(attr => attr.ConstructorArguments)
                .Any(arg => arg.Value != null && AttributeValuesToIgnore.Contains(arg.Value));
        }

        /// <summary>
        /// Create base public non static property collection, ignoring certain attributes.
        /// </summary>
        static IEnumerable<PropertyInfo> CreateBase()
        {
            return typeof(TModel)
                .GetTypeInfo()
                .GetProperties(BindingFlags.Public | ~BindingFlags.Static)
                .Where(IgnoreAttributes.And(IgnoreArguments));
        }
    }

    /// <summary>
    /// Public Factories. Collection allows finding by name/string, Lookup allows finding by Type.
    /// </summary>
    public static partial class ReaderFactory<TModel>
    {
        public static ReaderCollection<TModel> CreateCollection()
        {
            return new ReaderCollection<TModel>(CreateBase()
            .Select(n =>
            (
                Name: n,
                Reader: CreateDelegate(n))
            ));
        }

        public static ILookup<Type, Reader<TModel>> CreateTypeLookup()
        {
            return CreateBase()
            .Select(n =>
            (
                Name: n.PropertyType,
                Reader: CreateDelegate(n))
            )
            .ToLookup(n => n.Name, n => n.Reader);
        }
    }

    /// <summary>
    /// aka Func(TModel,object)
    /// </summary>
    public delegate object Reader<in TModel>(TModel source);

    /// <summary>
    /// Delegate Factories
    /// </summary>
    static partial class ReaderFactory<TModel>
    {
        static Reader<TModel> CreateDelegate(PropertyInfo info)
        {
            return (info.PropertyType.IsValueType)
                ? MakeValueDelegate(info.Name)
                : MakeDelegate(info.Name);
        }

        static Reader<TModel> MakeDelegate(string name)
        {
            var delegateType = typeof(Reader<TModel>);
            var getMethod = typeof(TModel).GetProperty(name).GetMethod;
            return (Reader<TModel>)Delegate.CreateDelegate(delegateType, getMethod);
        }

        static Reader<TModel> MakeValueDelegate(string name)
        {
            var param = Expression.Parameter(typeof(TModel));
            var expr = Expression.Convert(Expression.Property(param, name), typeof(object));
            return Expression.Lambda<Reader<TModel>>(expr, param).Compile();
        }
    }
}
