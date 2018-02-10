using System;
using System.Diagnostics;

namespace Comads
{
    [DebuggerDisplay("{Value}", Name = "{Name, nq}", Type = "{Value.GetType()}")]
    public partial struct ValueObject
    {
        public readonly object Value;

        public ValueObject(object value, int ownersHashCode, string name = "")
        {
            Value = value;
            OwnersHashCode = ownersHashCode;
            Name = name;
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public string Name { get; }
        //[DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public int OwnersHashCode { get; }

    }

    /// <summary>
    /// Statics
    /// </summary>
    public partial struct ValueObject
    {
        public static Func<TModel, ValueObject> Create<TModel>(Reader<TModel> getter)
        {
            return (owner) => new ValueObject(getter?.Invoke(owner), owner.GetHashCode());
        }

        public static ValueObject Create<TModel>(object value, TModel owner)
        {
            return new ValueObject(value, owner.GetHashCode());
        }
    }

    /// <summary>
    /// Bookkeeping
    /// </summary>
    public partial struct ValueObject : IEquatable<ValueObject>, IComparable<ValueObject>
    {

        public int CompareTo(ValueObject other) => ToString().CompareTo(other.ToString());

        public bool Equals(ValueObject other) => Value.Equals(other.Value);

        public override bool Equals(object obj) => Equals(obj is ValueObject vo);

        public override int GetHashCode() => Value.GetHashCode();
        public override string ToString() => Value.ToString();


        public static bool operator ==(ValueObject left, ValueObject right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(ValueObject left, ValueObject right)
        {
            return !(left == right);
        }

        public static bool operator <(ValueObject left, ValueObject right)
        {
            return left.CompareTo(right) < 0;
        }

        public static bool operator <=(ValueObject left, ValueObject right)
        {
            return left.CompareTo(right) <= 0;
        }

        public static bool operator >(ValueObject left, ValueObject right)
        {
            return left.CompareTo(right) > 0;
        }

        public static bool operator >=(ValueObject left, ValueObject right)
        {
            return left.CompareTo(right) >= 0;
        }
    }
}
