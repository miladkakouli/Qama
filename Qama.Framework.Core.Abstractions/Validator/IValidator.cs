namespace Qama.Framework.Core.Abstractions.Validator
{
    public interface IValidator<T> where T : IValidatable
    {
        void Validate(T command);
    }
}
