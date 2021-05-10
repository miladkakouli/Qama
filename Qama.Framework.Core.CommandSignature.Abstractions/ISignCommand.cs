namespace Qama.Framework.Core.CommandSignature.Abstractions
{
    interface ISignCommand<T>
    {
        string GetSignData();
        T GetSignType();
        int GetVersion();
    }
}
