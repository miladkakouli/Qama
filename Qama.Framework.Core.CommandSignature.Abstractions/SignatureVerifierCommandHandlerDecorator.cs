using System.Threading.Tasks;
using Qama.Framework.Core.Abstractions.Commands;
using Qama.Framework.Core.Abstractions.Context;
using Qama.Framework.Core.Abstractions.Exceptions;
using Qama.Framework.Infrastructure.TokenUtilities;

namespace Qama.Framework.Core.CommandSignature.Abstractions
{
    public class SignatureVerifierCommandHandlerDecorator<T, TSignType> : ICommandHandler<T> where T : SignCommand<TSignType>
    {
        private readonly ICommandHandler<T> _handler;
        private readonly ITokenManager _tokenManager;
        private readonly ICurrentContext _context;

        public SignatureVerifierCommandHandlerDecorator(ICommandHandler<T> handler, ITokenManager tokenManager, ICurrentContext context)
        {
            _handler = handler;
            _tokenManager = tokenManager;
            _context = context;
        }

        public async Task Handle(T command)
        {
            _tokenManager.InitPKE();
            command.IvhSignature = $"-----BEGIN PKCS7-----\n{command.IvhSignature}\n-----END PKCS7-----\n";
            var signerCert = _tokenManager.GetSignerCertificate(command.IvhSignature, command.GetSignData());
            var ssn = _tokenManager.GetSsnFromSignerCertificate(signerCert);
            if (ssn != _context.GetSsn())
                throw new ValidationException(ValidationExceptionDomain.SignValidationException);
            _tokenManager.VerifyCertificate(signerCert);
            _tokenManager.SetSignMechanism();
            _tokenManager.VerifySignature(command.IvhSignature);
            await _handler.Handle(command);
        }
    }
}
