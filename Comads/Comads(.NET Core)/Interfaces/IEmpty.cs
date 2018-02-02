namespace Monads.Extra.Types
{
    /// <summary>
    /// EmptyValues represent the ommision of a value.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IEmpty<out T>
    {
        T EmptyValue { get; }
        bool IsEmpty { get; }
    }
}