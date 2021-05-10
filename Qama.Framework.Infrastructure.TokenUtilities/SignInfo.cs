namespace Qama.Framework.Infrastructure.TokenUtilities
{
    public class SignInfo
    {
        public string OriginData { get; set; }
        public string Digest { get; set; }
        public string DigestAlg { get; set; }
        public string SigningTime { get; set; }
        public TokenSubject Subject { get; set; }
    }
}