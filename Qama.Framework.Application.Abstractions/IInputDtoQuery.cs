namespace Qama.Framework.Application.Abstractions
{
    public interface IInputDtoQuery<out T>
    {
        T ToQuery();
    }
}
