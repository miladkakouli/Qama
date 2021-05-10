namespace Qama.Framework.Infrastructure.TokenUtilities
{
    public interface ITokenManager
    {
        string GetSsnFromSignerCertificate(string signerCert);
        void InitPKE();
        void VerifyCertificate(string signerCert);
        string GetSignerCertificate(string signature, string tbs);
        void SetSignMechanism();
        void VerifySignature(string signature);
    }
}
