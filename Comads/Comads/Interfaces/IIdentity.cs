namespace Monads.Extra.Types
{
    /// <summary>
    /// Application of the Identity Value returns the origianl value unchanged.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IIdentity<out T>
    {
        T IdentityValue { get; }
    }
}