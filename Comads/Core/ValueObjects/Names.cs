using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Core.ValueObjects
{
    public partial class Names : IReadOnlyList<Name>
    {
        public Names(IEnumerable<Name> names)
        {
            _names = names.ToList();
        }

        readonly List<Name> _names;

        public Name this[int index] => _names[index];

        public int Count => _names.Count;

        public IEnumerator<Name> GetEnumerator()
        {
            return _names.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _names.GetEnumerator();
        }
    }

    public partial class Names
    {
        public class SequenceEqualityComparer : IEqualityComparer<Names>
        {
            public bool Equals(Names x, Names y)
            {
                return x.SequenceEqual(y);
            }

            public int GetHashCode(Names obj)
            {
                return obj.GetHashCode();
            }
        }

        private static readonly Lazy<SequenceEqualityComparer> lazy = new Lazy<SequenceEqualityComparer>(() => new SequenceEqualityComparer());
        public static SequenceEqualityComparer SequenceEquality => lazy.Value;
    }



}
