using Qama.Framework.Core.Abstractions.Equality;

namespace Qama.Framework.Core.Abstractions.DAL
{
    public abstract class ValueObject : IValueObject
    {
        public override bool Equals(object obj)
        {
            return EqualsBuilder.ReflectionEquals(this, obj);
        }

        public override int GetHashCode()
        {
            return HashCodeBuilder.ReflectionHashCode(this);
        }
    }
}
