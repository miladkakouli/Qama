using System.Runtime.InteropServices;
using System.Text;

namespace Qama.Framework.Infrastructure.TokenUtilities
{
    class TokenUtilities
    {

        [DllImport("sswpke_fs.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void initPKE(out int retVal);

        [DllImport("sswpke_fs.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int getRandom(StringBuilder buff, int size, out int retVal);

        [DllImport("sswpke_fs.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int genHash(int ivHashAlg, int ivDataType, string ivData, int ivDataLen, StringBuilder ovDigest, StringBuilder ovHexDigest, out int retVal);

        [DllImport("sswpke_fs.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int getPkcs7SignInfo(string strClientTextSign, StringBuilder originData, StringBuilder digist, StringBuilder digistAlg, StringBuilder signingTime, StringBuilder signerCertificate, out int retVal);

        [DllImport("sswpke_fs.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int getPkcs7SignInfo(StringBuilder strClientTextSign, StringBuilder originData, StringBuilder digist, StringBuilder digistAlg, StringBuilder signingTime, StringBuilder signerCertificate, out int retVal);

        [DllImport("sswpke_fs.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int verifyCert(string cert, string trustchainPath, string usage, out int retVal);

        [DllImport("sswpke_fs.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int getVCertErrMsg(StringBuilder errInfo, StringBuilder certInfo, out int retVal);

        [DllImport("sswpke_fs.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int getIssuerCert(string ivChainPath, string ivChildCert, StringBuilder ovParentCert, out int retVal);

        [DllImport("sswpke_fs.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int getCertExtension(int ivSel, string ivCert, StringBuilder ovCertEXTInfo, out int retVal);

        [DllImport("sswpke_fs.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int getExtByName(string ivCertExtension, string ivExtName, StringBuilder ovExtInfo, out int retVal);

        [DllImport("sswpke_fs.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int ocspReq(string ivURL, string ivCert, string ivIssuerCert, int ivSlotIdx, string ivPincode, string ivCertSign, string ivKeySignId, string ivOCSPCertUsage, string ivCAFile, int ivSel, int ivNonce, out int retVal);

        [DllImport("sswpke_fs.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int normalize(string msg, StringBuilder normalizedString, out int retVal);

        [DllImport("sswpke_fs.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int setSignMech(int ivSignatureFormat, int ivSignHashAlg, int ivSignMech, int[] ivSignMechParam, out int retVal);

        [DllImport("sswpke_fs.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int verify(string ivSignerCert, int ivTbsType, int ivTbsHashAlg, string ivTbs, string signature, out int retVal);

        [DllImport("sswpke_fs.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int getCertData(StringBuilder ivCert, int ivSel, StringBuilder ovSubject, StringBuilder ovPubKey, StringBuilder ovKeyId, out int ovRetVal);

        [DllImport("sswpke_fs.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int getInfoCertificate(StringBuilder ivCert, int ivSel, StringBuilder ovCertInfo, out int ovRetVal);
        
    }
}
