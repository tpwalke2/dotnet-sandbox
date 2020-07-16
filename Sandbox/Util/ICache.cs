namespace Sandbox.Util
{
    public interface ICache<in TKey, out TValue>
    {
        TValue Get(TKey key);
    }
}
