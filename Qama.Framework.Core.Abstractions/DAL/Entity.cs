namespace Qama.Framework.Core.Abstractions.DAL
{
    public abstract class Entity<TKey>
    {
        public virtual TKey Id { get; protected set; }
        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (this.GetType() != obj.GetType()) return false;
            var entity = obj as Entity<TKey>;
            return this.Id.Equals(entity.Id);
        }
        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }
    }
}
