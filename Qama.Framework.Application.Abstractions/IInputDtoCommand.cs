namespace Qama.Framework.Application.Abstractions
{
    public interface IInputDtoCommand<out T>
    {
        T ToCommand();
    }
}
