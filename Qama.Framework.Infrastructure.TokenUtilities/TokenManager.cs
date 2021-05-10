using System;
using System.Globalization;

namespace Qama.Framework.Infrastructure.TokenUtilities
{
    public class TokenManager : ITokenManager
    {
        private readonly SSWPKE _sSWPKE;
        public TokenManager(SSWPKE sSwpke)
        {
            _sSWPKE = sSwpke;
        }

        public string GetSsnFromSignerCertificate(string signerCert)
        {
            string status = "";
            int ovRetVal = 0;

            string subject = "";
            string pubkey = "";
            string keyID = "";

            if (!_sSWPKE.PK_GetCertData(signerCert, 1, out subject, out pubkey, out keyID, out status, out ovRetVal))
            {
                throw new Exception(status);
            }

            string UNIQ_ID = "";
            int pos = -1;

            pos = subject.IndexOf("SN");
            if (pos < 0)
                pos = subject.IndexOf("Serial");

            if (pos < 0)
            {
                throw new Exception("There is not the required unique id in certificate.");
            }

            subject = subject.Substring(pos);
            subject = subject.Substring(subject.IndexOf("=") + 1);
            pos = subject.IndexOf("/");

            UNIQ_ID = subject;

            if (pos < 0)
                pos = subject.IndexOf("\n");

            if (pos >= 0)
                UNIQ_ID = subject.Substring(0, pos);
            return UNIQ_ID;
        }

        public void InitPKE()
        {
            string status = "";
            int ovRetVal = 0;

            if (!_sSWPKE.PK_InitPKE(out status, out ovRetVal))
            {
                throw new Exception(status);
            }
        }

        public void VerifyCertificate(string signerCert)
        {
            string status = "";
            int ovRetVal = 0;

            String certUsage = "KeyUsage::C=T,DIGITAL_SIGNATURE=T,NON_REPUDIATION=T,KEY_ENCIPHERMENT=F,DATA_ENCIPHERMENT=F,KEY_AGREEMENT=F,KEY_CERT_SIGN=F,CRL_SIGN=F,ENCIPHER_ONLY=F,DECIPHER_ONLY=F;ExtendedKeyUsage::C=F,SERVER_AUTH=F,CLIENT_AUTH=F,CODE_SIGN=F,EMAIL_PROTECTION=F,TIME_STAMPING=F,OCSP_SIGN=F,SMART_CARD_LOGIN=F";
            String chain_path = "trustchain.pem";

            if (!_sSWPKE.PK_VerifyCert(signerCert, chain_path, certUsage, out status, out ovRetVal))
            {
                throw new Exception(status);
            }
        }

        public string GetSignerCertificate(string signature, string tbs)
        {
            string status = "";
            int ovRetVal = 0;

            string origin_tbs = "";
            string digest = "";
            string certs = "";
            string signingTime = "";
            string signerCert = "";

            if (!_sSWPKE.PK_GetPkcs7SignInfo(signature, out origin_tbs, out digest, out certs, out signingTime, out signerCert, out status, out ovRetVal))
            {
                throw new Exception(status);
            }

            DateTime signedDateTime = DateTime.ParseExact(signingTime,
                "MMM d HH:mm:ss yyyy 'GMT'", CultureInfo.InvariantCulture,
                DateTimeStyles.AllowWhiteSpaces | DateTimeStyles.AssumeUniversal);

            if (DateTime.Now - signedDateTime > TimeSpan.FromMinutes(10))
            {
                throw new Exception("Timeout Exception");
            }

            if (!tbs.Equals(origin_tbs) && !tbs.Equals(digest))
            {
                throw new Exception("Tbs Has been changed.");
            }
            return signerCert;
        }

        public void SetSignMechanism()
        {
            string status = "";
            int ovRetVal = 0;

            string certUsage = "KeyUsage::C=F,DIGITAL_SIGNATURE=T,NON_REPUDIATION=T,KEY_ENCIPHERMENT=F,DATA_ENCIPHERMENT=F,KEY_AGREEMENT=F,KEY_CERT_SIGN=F,CRL_SIGN=F,ENCIPHER_ONLY=F,DECIPHER_ONLY=F;ExtendedKeyUsage::C=F,SERVER_AUTH=F,CLIENT_AUTH=T,CODE_SIGN=F,EMAIL_PROTECTION=F,TIME_STAMPING=F,OCSP_SIGN=F,SMART_CARD_LOGIN=F";
            int[] signSetting = new int[] { 0, -1, -1, -1, -1, -1 };
            SSWPKE.Config pk_config = new SSWPKE.Config(signSetting, certUsage, null, null);

            if (!_sSWPKE.PK_SetSignMech(pk_config.signFormat, pk_config.signHashAlg, pk_config.signMechanism, pk_config.mgf, pk_config.saltLen, out status, out ovRetVal))
            {
                throw new Exception(status);
            }
        }

        public void VerifySignature(string signature)
        {
            string status = "";
            int ovRetVal = 0;
            if (!_sSWPKE.PK_Verify("", 0, 0, "", 0, signature, out status, out ovRetVal))
            {
                throw new Exception(status);
            }
        }
    }
}
