using Qama.Framework.Core.Abstractions.Commands;

namespace Qama.Framework.Core.CommandSignature.Abstractions
{
    public abstract class SignCommand<TSignType> : CommandBase, ISignCommand<TSignType>
    {
        public abstract string GetSignData();
        public abstract TSignType GetSignType();
        public virtual int GetVersion() => 1;
        public string IvhSignature { get; set; }
    }
}
