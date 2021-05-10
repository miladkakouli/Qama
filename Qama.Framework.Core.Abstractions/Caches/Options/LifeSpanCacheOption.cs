namespace Qama.Framework.Core.Abstractions.Caches.Options
{
    public enum LifeSpanCacheOption : long
    {
        Forever = 0,
        Absolute = 1,
        RelativeFromLastAccess = 2
    }
}
