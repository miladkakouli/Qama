namespace Qama.Framework.Core.Abstractions.DAL
{
    public abstract class Id<T> : ValueObject
    {
        public T DbId { get; private set; }
        protected Id() { }
        protected Id(T dbId)
        {
            DbId = dbId;
        }
    }
}
