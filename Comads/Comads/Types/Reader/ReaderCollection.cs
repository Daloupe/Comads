using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;

namespace Comads
{
    public class ReaderCollection<TModel> : KeyedCollection<string, (PropertyInfo Type, Reader<TModel> Reader)>
    {
        public ReaderCollection() : base(EqualityComparer<string>.Default, 0)
        {
        }

        public ReaderCollection(IEnumerable<(PropertyInfo Type, Reader<TModel> Reader)> collection) : this()
        {

            AddRange(collection);
        }

        protected override string GetKeyForItem((PropertyInfo Type, Reader<TModel> Reader) item)
        {
            return item.Type.Name;
        }

        public ReaderCollection<TModel> AddRange(IEnumerable<(PropertyInfo Type, Reader<TModel> Reader)> source)
        {
            foreach (var prop in source)
            {
                Add(prop);
            }
            return this;
        }

        public IList<(PropertyInfo Type, Reader<TModel> Reader)> Readers()
        {
            return Items;
        }
    }
}
