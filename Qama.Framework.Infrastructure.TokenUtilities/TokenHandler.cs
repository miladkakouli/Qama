using System;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Security;
using System.Text;

namespace Qama.Framework.Infrastructure.TokenUtilities
{
    public class TokenHandler
    {
        public static int MAX_BUF = 4000;
        public static int MAX_BUF1 = 512;
        public static int MAX_BUF2 = 1024;

        [HandleProcessCorruptedStateExceptions]
        [SecurityCritical]
        public string GenerateCertHash(string cert, out int ovRetVal)
        {
            int retVal = 0;

            #region initPKE

            int nRetVal = 0;
            try
            {
                TokenUtilities.initPKE(out nRetVal);
            }
            catch
            {
                ovRetVal = 1;
                return "";
            }
            if (retVal != 0)
            {
                ovRetVal = nRetVal;
                return "";
            }

            #endregion

            #region genHash

            StringBuilder certDigestStrB = new StringBuilder(19);// 0 ~19 :20 byte for sha1
            StringBuilder certDigestHexStrB = new StringBuilder(39);// 0~39 :40 byte for sha1 as Hex

            try
            {
                TokenUtilities.genHash(2/*sha1*/, 2/*certificate*/, cert, cert.Length, certDigestStrB, certDigestHexStrB, out nRetVal);
            }
            catch
            {
                ovRetVal = 1;
                return "";
            }
            if (retVal != 0)
            {
                ovRetVal = nRetVal;
                return "";
            }

            string crt_digest = certDigestHexStrB.ToString();
            ovRetVal = 0;
            return crt_digest;

            #endregion
        }
        [HandleProcessCorruptedStateExceptions]
        [SecurityCritical]
        public string GetRandomString()
        {
            #region initPKE

            int nRetVal = 0;

            try
            {
                TokenUtilities.initPKE(out nRetVal);
            }
            catch 
            {
                //PseudoDBResLabel.ForeColor = System.Drawing.Color.Red;
                //PseudoDBResLabel.Text = "initPKE خطا در فراخواني تابع";
                //Log(strPage, "server", "initPKECore", "server", "Error", "unsuccessful login : initPKE is failed");
                return ("");
            }
            if (nRetVal != 0)
            {
                //PseudoDBResLabel.ForeColor = System.Drawing.Color.Red;
                //PseudoDBResLabel.Text = "initPKE خطا در فراخواني تابع";
                //Log(strPage, "server", "initPKECore", "server", "Error", "initPKE خطا در فراخواني تابع");
            }

            #endregion

            #region getRandom

            var buff = new StringBuilder(32 * 2);
            int sizeByte = 32;

            TokenUtilities.getRandom(buff, sizeByte, out nRetVal);

            if (nRetVal == 0)
            {
                //Log(strPage, "server", "getRandom", "server", "successful", "Random string is made successful");
                return (buff.ToString());
            }
            else
            {
                //PseudoDBResLabel.ForeColor = System.Drawing.Color.Red;
                //PseudoDBResLabel.Text = "خطا در توليد عدد تصادفي";
                //Log(strPage, "server", "initPKE", "server", "Error", "unsuccessful login : Cannot generate challenge string");
                return ("");
            }
            #endregion

        }
        [HandleProcessCorruptedStateExceptions]
        [SecurityCritical]
        public bool Verify(string clientCertificate, string clientChallengeSigned, string clientRandomNumber, out string serverChallenge)
        {
            serverChallenge = GetRandomString();

            if (clientChallengeSigned != "" && clientCertificate != "")
            {
                // check if client is in pseudoDB or not
                #region checkuser

                //int retValue = -1;
                //GenerateCertHash(clientCertificate, out retValue);
                //if (retValue != 0)
                //{
                //    throw new Exception("پین‌کد را اشتباه وارد کرده‌اید، اگر چندبار پین‌کد را اشتباه وارد نمایین توکن قفل خواهد شد.");
                //}
                #endregion

                // call initPKE before any function call
                #region initPKE

                int nRetVal = 0;

                try
                {
                    TokenUtilities.initPKE(out nRetVal);
                }
                catch
                {
                    throw new Exception("initPKE خطا در فراخواني تابع");

                }
                if (nRetVal != 0)
                {
                    throw new Exception("initPKE خطا در فراخواني تابع");

                }

                #endregion

                // check if the certificate is valid or not
                #region VerifyCertificate

                try
                {
                    string strCertUsage =
                        "KeyUsage::C=T,DIGITAL_SIGNATURE=T,NON_REPUDIATION=T,KEY_ENCIPHERMENT=F,DATA_ENCIPHERMENT=F,KEY_AGREEMENT=F,KEY_CERT_SIGN=F,CRL_SIGN=F,ENCIPHER_ONLY=F,DECIPHER_ONLY=F;" +
                        "ExtendedKeyUsage::C=F,SERVER_AUTH=F,CLIENT_AUTH=F,CODE_SIGN=F,EMAIL_PROTECTION=F,TIME_STAMPING=F,OCSP_SIGN=F,SMART_CARD_LOGIN=F";
                    TokenUtilities.verifyCert(clientCertificate, "trustchain.pem", strCertUsage, out nRetVal);
                }
                catch 
                {
                    throw new Exception("خطاي داخلي در تصديق گواهي");
                }

                int iRetValbyte0 = 0x000000ff & nRetVal;
                int iRetValbyte1 = 0x0000ff00 & nRetVal;
                iRetValbyte1 >>= 8;

                if (nRetVal != 0)
                {
                    switch (iRetValbyte0)
                    {
                        //case 0:
                        //    {
                        //        if (iRetValbyte1 == 0)
                        //        {
                        //            throw new Exception( "گواهي تصديق شد");
                        //        }
                        //    }
                        //    break;
                        case 2:
                            {
                                throw new Exception("قالب‌بندي گواهي كاربر مناسب نيست");

                            }
                        case 4:
                            {
                                if ((iRetValbyte1) == 1)
                                {
                                    throw new Exception("Key usage of cert is not critical");

                                }
                                if ((iRetValbyte1) == 2)
                                {
                                    throw new Exception("Key usage of cert is critical in while input key usage is not critical");

                                }
                                if ((iRetValbyte1) == 3)
                                {
                                    throw new Exception("Input field (Critical) is not valid");

                                }
                                if ((iRetValbyte1) == 4)
                                {
                                    throw new Exception("Digital Signature is not available in cert");

                                }
                                if ((iRetValbyte1) == 5)
                                {
                                    throw new Exception("Input value is 'DIGITAL_SIGNATURE=T' but KU is empty");

                                }
                                if ((iRetValbyte1) == 6)
                                {
                                    throw new Exception("Digital Signature is available in cert");

                                }
                                if ((iRetValbyte1) == 7)
                                {
                                    throw new Exception("Input field (DigitalSignature) is not valid ");

                                }
                                if ((iRetValbyte1) == 8)
                                {
                                    throw new Exception("Non Repudiation is not available in cert ");

                                }
                                if ((iRetValbyte1) == 9)
                                {
                                    throw new Exception("Input value is 'NON_REPUDIATION=T' but KU is empty");

                                }
                                if ((iRetValbyte1) == 10)
                                {
                                    throw new Exception("Non Repudiation is available in cert");

                                }
                                if ((iRetValbyte1) == 11)
                                {
                                    throw new Exception("Input field (NonRepudiation) is not valid");

                                }
                                if ((iRetValbyte1) == 12)
                                {
                                    throw new Exception("Key Encipherment is not available in cert");

                                }
                                if ((iRetValbyte1) == 13)
                                {
                                    throw new Exception("Input value is 'KEY_ENCIPHERMENT=T' but KU is empty");

                                }
                                if ((iRetValbyte1) == 14)
                                {
                                    throw new Exception("Key Encipherment is available in cert");

                                }
                                if ((iRetValbyte1) == 15)
                                {
                                    throw new Exception("Input field (KeyEncipherment) is not valid");

                                }
                                if ((iRetValbyte1) == 16)
                                {
                                    throw new Exception("Data Encipherment is not available in cert");

                                }
                                if ((iRetValbyte1) == 17)
                                {
                                    throw new Exception("Input value is 'DATA_ENCIPHERMENT=T' but KU is empty");

                                }
                                if ((iRetValbyte1) == 18)
                                {
                                    throw new Exception("Data Encipherment is available in cert");

                                }
                                if ((iRetValbyte1) == 19)
                                {
                                    throw new Exception("Input field (DataEncipherment) is not valid");

                                }
                                if ((iRetValbyte1) == 20)
                                {
                                    throw new Exception("Key agreement is not available in cert");

                                }
                                if ((iRetValbyte1) == 21)
                                {
                                    throw new Exception("Input value is 'KEY_AGREEMENT=T' but KU is empty");

                                }
                                if ((iRetValbyte1) == 22)
                                {
                                    throw new Exception("Key agreeement is available in cert");

                                }
                                if ((iRetValbyte1) == 23)
                                {
                                    throw new Exception("Input field (Keyagreeement) is not valid");

                                }
                                if ((iRetValbyte1) == 24)
                                {
                                    throw new Exception("Encipher Only is not available in cert");

                                }
                                if ((iRetValbyte1) == 25)
                                {
                                    throw new Exception("Input value is 'ENCIPHER_ONLY=T' but KU is empty");

                                }
                                if ((iRetValbyte1) == 26)
                                {
                                    throw new Exception("Encipher Only is available in cert");

                                }
                                if ((iRetValbyte1) == 27)
                                {
                                    throw new Exception("Input field (EncipherOnly) is not valid");

                                }
                                if ((iRetValbyte1) == 28)
                                {
                                    throw new Exception("Decipher Only is not available in cert");

                                }
                                if ((iRetValbyte1) == 29)
                                {
                                    throw new Exception("Input value is 'ENCIPHER_ONLY=T' but KU is empty");

                                }
                                if ((iRetValbyte1) == 30)
                                {
                                    throw new Exception("Decipher Only is available in cert");

                                }
                                if ((iRetValbyte1) == 31)
                                {
                                    throw new Exception("Input field (DencipherOnly) is not valid");

                                }
                                if ((iRetValbyte1) == 32)
                                {
                                    throw new Exception("Input value is 'KEY_CERT_SIGN=T' but KU is empty");

                                }
                                if ((iRetValbyte1) == 33)
                                {
                                    throw new Exception("Input field (KeyCertSign) is not valid");

                                }
                                if ((iRetValbyte1) == 34)
                                {
                                    throw new Exception("Input value is 'CRL_SIGN=T' but KU is empty");

                                }
                                if ((iRetValbyte1) == 35)
                                {
                                    throw new Exception("Input field (CRLSign) is not valid");

                                }
                                if ((iRetValbyte1) == 36)
                                {
                                    throw new Exception("EKey usage of cert is not critical");

                                }
                                if ((iRetValbyte1) == 37)
                                {
                                    throw new Exception("EKey usage of cert is critical");

                                }
                                if ((iRetValbyte1) == 38)
                                {
                                    throw new Exception("Input field (Critical) is not valid");

                                }
                                if ((iRetValbyte1) == 39)
                                {
                                    throw new Exception("Server Authentication is not available in XKU of cert");

                                }
                                if ((iRetValbyte1) == 40)
                                {
                                    throw new Exception("Input value is SERVER_AUTH=T but EKU is empty");

                                }
                                if ((iRetValbyte1) == 41)
                                {
                                    throw new Exception("Server Authentication is available in XKU of cert");

                                }
                                if ((iRetValbyte1) == 42)
                                {
                                    throw new Exception("Input field (Server Auth) is not valid");

                                }
                                if ((iRetValbyte1) == 43)
                                {
                                    throw new Exception("Client Authentication is not available in XKU of cert");

                                }
                                if ((iRetValbyte1) == 44)
                                {
                                    throw new Exception("Input value is CLIENT_AUTH=T but EKU is empty");

                                }
                                if ((iRetValbyte1) == 45)
                                {
                                    throw new Exception("Client Authentication is available in XKU of cert");

                                }
                                if ((iRetValbyte1) == 46)
                                {
                                    throw new Exception("Input field (Client Auth) is not valid");

                                }
                                if ((iRetValbyte1) == 47)
                                {
                                    throw new Exception("Code Signing is not available in XKU of cert");

                                }
                                if ((iRetValbyte1) == 48)
                                {
                                    throw new Exception("Input value is 'CODE_SIGN=T' but EKU is empty");

                                }
                                if ((iRetValbyte1) == 49)
                                {
                                    throw new Exception("Code Signing is available in XKU of cert");

                                }
                                if ((iRetValbyte1) == 50)
                                {
                                    throw new Exception("Input field (Code Signing) is not valid");

                                }
                                if ((iRetValbyte1) == 51)
                                {
                                    throw new Exception("Email Protection is not available in XKU of cert");

                                }
                                if ((iRetValbyte1) == 52)
                                {
                                    throw new Exception("Input value is 'EMAIL_PROTECTION=T' but EKU is empty");

                                }
                                if ((iRetValbyte1) == 53)
                                {
                                    throw new Exception("Email Protection is available in XKU of cert");

                                }
                                if ((iRetValbyte1) == 54)
                                {
                                    throw new Exception("Input field (Email Protection) is not valid");

                                }
                                if ((iRetValbyte1) == 55)
                                {
                                    throw new Exception("Time Stamping is not available in XKU of cert");

                                }
                                if ((iRetValbyte1) == 56)
                                {
                                    throw new Exception("Input value is 'TIME_STAMPING=T' but EKU is empty");

                                }
                                if ((iRetValbyte1) == 57)
                                {
                                    throw new Exception("Time Stamping is available in XKU of cert");

                                }
                                if ((iRetValbyte1) == 58)
                                {
                                    throw new Exception("Input field (Time Stamping) is not valid");

                                }
                                if ((iRetValbyte1) == 59)
                                {
                                    throw new Exception("OCSP Sign is not available in XKU of cert");

                                }
                                if ((iRetValbyte1) == 60)
                                {
                                    throw new Exception("Input value is 'OCSP_SIGN=T' but EKU is empty");

                                }
                                if ((iRetValbyte1) == 61)
                                {
                                    throw new Exception("OCSP Sign is available in XKU of cert");

                                }
                                if ((iRetValbyte1) == 62)
                                {
                                    throw new Exception("Input field (OCSP Sign) is not valid");

                                }
                                if ((iRetValbyte1) == 63)
                                {
                                    throw new Exception("MS Smartcard login is not available in XKU of cert");

                                }
                                if ((iRetValbyte1) == 64)
                                {
                                    throw new Exception("MS Smartcard login is not available in XKU of cert");

                                }
                                if ((iRetValbyte1) == 65)
                                {
                                    throw new Exception("MS Smartcard login is available in XKU of cert");

                                }
                                if ((iRetValbyte1) == 66)
                                {
                                    throw new Exception("Input field (MS Smartcard login) is not valid");

                                }
                                if ((iRetValbyte1) == 67)
                                {
                                    throw new Exception("just Key Encipherment is KU but certificate is for sign");

                                }
                                if ((iRetValbyte1) == 68)
                                {
                                    throw new Exception("End entity user must not have cert Sign Key usage");

                                }
                                if ((iRetValbyte1) == 69)
                                {
                                    throw new Exception("End entity user must not have CRL Sign Key usage");

                                }
                                if ((iRetValbyte1) == 70)
                                {
                                    throw new Exception("cert Sign Key usage is available in End entity user Certificate!");

                                }
                                if ((iRetValbyte1) == 71)
                                {
                                    throw new Exception("CRL Sign Key usage is available in End entity user Certificate!");

                                }
                                if ((iRetValbyte1) == 72)
                                {
                                    throw new Exception("CA must have cert Sign Key usage");

                                }
                                if ((iRetValbyte1) == 73)
                                {
                                    throw new Exception("CA must have cert Sign Key usage");

                                }
                                if ((iRetValbyte1) == 74)
                                {
                                    throw new Exception("Cert Sign Key usage is not available in CA Certificate");

                                }

                                if ((iRetValbyte1) == 75)
                                {
                                    throw new Exception("CRL Sign Key usage is not available in CA Certificate");

                                }
                                if ((iRetValbyte1) == 76)
                                {
                                    throw new Exception("there are not any Key usage in cert");

                                }

                            }
                            break;
                        case 5:
                            {
                                StringBuilder errInfo = new StringBuilder(MAX_BUF);
                                StringBuilder certInfo = new StringBuilder(MAX_BUF);
                                int retValueErr = 0;

                                TokenUtilities.getVCertErrMsg(errInfo, certInfo, out retValueErr);

                                int iRetValErr = (int)retValueErr;
                                string strErrInfo = errInfo.ToString();
                                string strCertInfo = certInfo.ToString();

                                if (iRetValErr == 0)
                                {
                                    if ((iRetValbyte1) == 1)
                                    {
                                        throw new Exception("internal Error");

                                    }
                                    if ((iRetValbyte1) == 2)
                                    {
                                        throw new Exception("Error on loading CA cert or chain file");

                                    }
                                    else
                                    {
                                        throw new Exception(strErrInfo + " , failure certificate :" + certInfo);

                                    }
                                }
                            }
                            break;
                        case 6:
                            throw new Exception("قالب بندي زنجيره اعتماد مناسب نيست ");

                        case 7:
                            throw new Exception("قالب‌بندي گواهي كاربر مناسب نيست");

                        case 8:
                            throw new Exception("قالب گواهي زنجيره اعتماد مناسب نيست");

                        case 9:
                            throw new Exception("ليست ابطال نامعتبر است");

                        case 10:
                            throw new Exception("عمليات تصديق (بدون ليست ابطال) ناموفق ماند");

                        case 11:
                            throw new Exception("عمليات تصديق (با ليست ابطال) ناموفق ماند");

                        case 20:
                            {
                                if ((iRetValbyte1 & 0x01) == 0x01)
                                {
                                    throw new Exception("اين گواهي توسط اين مركز صدور گواهي صادر نشده‌است");

                                }
                                if ((iRetValbyte1 & 0x02) == 0x02)
                                {
                                    throw new Exception("امضای گواهی تصدیق نشد");

                                }
                                if ((iRetValbyte1 & 0x04) == 0x04)
                                {
                                    throw new Exception("گواهی کاربر خارج از دوره اعتبار است");

                                }
                                if ((iRetValbyte1 & 0x08) == 0x08)
                                {
                                    throw new Exception("اين ليست ابطال توسط اين مركز صدور گواهي صادر نشده‌است");

                                }
                                if ((iRetValbyte1 & 0x10) == 0x10)
                                {
                                    throw new Exception("امضای لیست ابطال تصدیق نشد");

                                }
                                if ((iRetValbyte1 & 0x20) == 0x20)
                                {
                                    throw new Exception("لیست‌هاي ابطال يكي از مراكز صدور گواهي خارج از دوره اعتبار است");

                                }
                                if ((iRetValbyte1 & 0x40) == 0x40)
                                {
                                    throw new Exception("گواهی باطل شده‌ است");

                                }
                            }
                            break;
                        default:
                            throw new Exception("گواهی تصدیق نشد");

                    };


                }
                #endregion


                // check if client signiture is valid or not
                #region VerifySignature

                int retVal = 0;

                string MsgChallenge = serverChallenge + clientRandomNumber;

                StringBuilder normalizedMsg = new StringBuilder(MsgChallenge.Length - 1);

                int[] signParam = new int[3];
                signParam[0] = 0;
                signParam[1] = 0;
                signParam[2] = 0;

                try
                {
                    TokenUtilities.setSignMech(2, 2, 1, signParam, out retVal);
                }
                catch 
                {
                    throw new Exception("خطا در تنظيم مكانيزم امضا");

                }
                if ((int)retVal > 0)
                {
                    throw new Exception("خطا در تنظيم مكانيزم امضا");

                }

                try
                {
                    TokenUtilities.normalize(MsgChallenge, normalizedMsg, out retVal);
                }
                catch 
                {
                    throw new Exception("خطا در normalize كردن داده ورودي");

                }
                if ((int)retVal > 0)
                {
                    throw new Exception("خطا در normalize كردن داده ورودي");

                }

                try
                {

                    MsgChallenge = normalizedMsg.ToString();
                    TokenUtilities.verify(clientCertificate, 0, 0, MsgChallenge.ToString(), clientChallengeSigned, out retVal);
                }
                catch
                {
                    throw new Exception("خطاي داخلي در تصديق امضا");

                }

                if ((int)retVal == 0)
                {
                    return true;

                }
                else
                {
                    if ((int)retVal == 15)
                    {
                        throw new Exception("تابع initPKE فراخواني نشده است");
                    }
                    if ((int)retVal == 28)
                    {
                        throw new Exception("تابع setSignMech فراخواني نشده است");
                    }
                    if ((int)retVal == 22 || (int)retVal == 24 || (int)retVal == 26 || (int)retVal == 25 || (int)retVal == 27)
                    {
                        throw new Exception("امضاء معتبر نمي‌باشد");
                    }
                    else
                    {
                        throw new Exception("امضا تصديق نشد. کد خطا  " + retVal.ToString());

                    }
                }
                #endregion
            }
            return true;
        }
        [HandleProcessCorruptedStateExceptions]
        [SecurityCritical]
        public bool VerifyV2(string clientCertificate, string clientChallengeSigned, string clientRandomNumber, out string serverChallenge)
        {
            serverChallenge = GetRandomString();

            if (clientChallengeSigned != "" && clientCertificate != "")
            {
                // check if client is in pseudoDB or not
                #region checkuser

                //int retValue = -1;
                //GenerateCertHash(clientCertificate, out retValue);
                //if (retValue != 0)
                //{
                //    throw new Exception("پین‌کد را اشتباه وارد کرده‌اید، اگر چندبار پین‌کد را اشتباه وارد نمایین توکن قفل خواهد شد.");
                //}
                #endregion

                // call initPKE before any function call
                #region initPKE

                int nRetVal = 0;

                try
                {
                    TokenUtilities.initPKE(out nRetVal);
                }
                catch
                {
                    throw new Exception("initPKE خطا در فراخواني تابع");

                }
                if (nRetVal != 0)
                {
                    throw new Exception("initPKE خطا در فراخواني تابع");

                }

                #endregion

                // check if the certificate is valid or not
                #region VerifyCertificate

                try
                {
                    string strCertUsage =
                        "KeyUsage::C=T,DIGITAL_SIGNATURE=T,NON_REPUDIATION=T,KEY_ENCIPHERMENT=F,DATA_ENCIPHERMENT=F,KEY_AGREEMENT=F,KEY_CERT_SIGN=F,CRL_SIGN=F,ENCIPHER_ONLY=F,DECIPHER_ONLY=F;" +
                        "ExtendedKeyUsage::C=F,SERVER_AUTH=F,CLIENT_AUTH=F,CODE_SIGN=F,EMAIL_PROTECTION=F,TIME_STAMPING=F,OCSP_SIGN=F,SMART_CARD_LOGIN=F";
                    TokenUtilities.verifyCert(clientCertificate, "trustchainV2.pem", strCertUsage, out nRetVal);
                }
                catch 
                {
                    throw new Exception("خطاي داخلي در تصديق گواهي");
                }

                int iRetValbyte0 = 0x000000ff & nRetVal;
                int iRetValbyte1 = 0x0000ff00 & nRetVal;
                iRetValbyte1 >>= 8;

                if (nRetVal != 0)
                {
                    switch (iRetValbyte0)
                    {
                        //case 0:
                        //    {
                        //        if (iRetValbyte1 == 0)
                        //        {
                        //            throw new Exception( "گواهي تصديق شد");
                        //        }
                        //    }
                        //    break;
                        case 2:
                            {
                                throw new Exception("قالب‌بندي گواهي كاربر مناسب نيست");

                            }
                        case 4:
                            {
                                if ((iRetValbyte1) == 1)
                                {
                                    throw new Exception("Key usage of cert is not critical");

                                }
                                if ((iRetValbyte1) == 2)
                                {
                                    throw new Exception("Key usage of cert is critical in while input key usage is not critical");

                                }
                                if ((iRetValbyte1) == 3)
                                {
                                    throw new Exception("Input field (Critical) is not valid");

                                }
                                if ((iRetValbyte1) == 4)
                                {
                                    throw new Exception("Digital Signature is not available in cert");

                                }
                                if ((iRetValbyte1) == 5)
                                {
                                    throw new Exception("Input value is 'DIGITAL_SIGNATURE=T' but KU is empty");

                                }
                                if ((iRetValbyte1) == 6)
                                {
                                    throw new Exception("Digital Signature is available in cert");

                                }
                                if ((iRetValbyte1) == 7)
                                {
                                    throw new Exception("Input field (DigitalSignature) is not valid ");

                                }
                                if ((iRetValbyte1) == 8)
                                {
                                    throw new Exception("Non Repudiation is not available in cert ");

                                }
                                if ((iRetValbyte1) == 9)
                                {
                                    throw new Exception("Input value is 'NON_REPUDIATION=T' but KU is empty");

                                }
                                if ((iRetValbyte1) == 10)
                                {
                                    throw new Exception("Non Repudiation is available in cert");

                                }
                                if ((iRetValbyte1) == 11)
                                {
                                    throw new Exception("Input field (NonRepudiation) is not valid");

                                }
                                if ((iRetValbyte1) == 12)
                                {
                                    throw new Exception("Key Encipherment is not available in cert");

                                }
                                if ((iRetValbyte1) == 13)
                                {
                                    throw new Exception("Input value is 'KEY_ENCIPHERMENT=T' but KU is empty");

                                }
                                if ((iRetValbyte1) == 14)
                                {
                                    throw new Exception("Key Encipherment is available in cert");

                                }
                                if ((iRetValbyte1) == 15)
                                {
                                    throw new Exception("Input field (KeyEncipherment) is not valid");

                                }
                                if ((iRetValbyte1) == 16)
                                {
                                    throw new Exception("Data Encipherment is not available in cert");

                                }
                                if ((iRetValbyte1) == 17)
                                {
                                    throw new Exception("Input value is 'DATA_ENCIPHERMENT=T' but KU is empty");

                                }
                                if ((iRetValbyte1) == 18)
                                {
                                    throw new Exception("Data Encipherment is available in cert");

                                }
                                if ((iRetValbyte1) == 19)
                                {
                                    throw new Exception("Input field (DataEncipherment) is not valid");

                                }
                                if ((iRetValbyte1) == 20)
                                {
                                    throw new Exception("Key agreement is not available in cert");

                                }
                                if ((iRetValbyte1) == 21)
                                {
                                    throw new Exception("Input value is 'KEY_AGREEMENT=T' but KU is empty");

                                }
                                if ((iRetValbyte1) == 22)
                                {
                                    throw new Exception("Key agreeement is available in cert");

                                }
                                if ((iRetValbyte1) == 23)
                                {
                                    throw new Exception("Input field (Keyagreeement) is not valid");

                                }
                                if ((iRetValbyte1) == 24)
                                {
                                    throw new Exception("Encipher Only is not available in cert");

                                }
                                if ((iRetValbyte1) == 25)
                                {
                                    throw new Exception("Input value is 'ENCIPHER_ONLY=T' but KU is empty");

                                }
                                if ((iRetValbyte1) == 26)
                                {
                                    throw new Exception("Encipher Only is available in cert");

                                }
                                if ((iRetValbyte1) == 27)
                                {
                                    throw new Exception("Input field (EncipherOnly) is not valid");

                                }
                                if ((iRetValbyte1) == 28)
                                {
                                    throw new Exception("Decipher Only is not available in cert");

                                }
                                if ((iRetValbyte1) == 29)
                                {
                                    throw new Exception("Input value is 'ENCIPHER_ONLY=T' but KU is empty");

                                }
                                if ((iRetValbyte1) == 30)
                                {
                                    throw new Exception("Decipher Only is available in cert");

                                }
                                if ((iRetValbyte1) == 31)
                                {
                                    throw new Exception("Input field (DencipherOnly) is not valid");

                                }
                                if ((iRetValbyte1) == 32)
                                {
                                    throw new Exception("Input value is 'KEY_CERT_SIGN=T' but KU is empty");

                                }
                                if ((iRetValbyte1) == 33)
                                {
                                    throw new Exception("Input field (KeyCertSign) is not valid");

                                }
                                if ((iRetValbyte1) == 34)
                                {
                                    throw new Exception("Input value is 'CRL_SIGN=T' but KU is empty");

                                }
                                if ((iRetValbyte1) == 35)
                                {
                                    throw new Exception("Input field (CRLSign) is not valid");

                                }
                                if ((iRetValbyte1) == 36)
                                {
                                    throw new Exception("EKey usage of cert is not critical");

                                }
                                if ((iRetValbyte1) == 37)
                                {
                                    throw new Exception("EKey usage of cert is critical");

                                }
                                if ((iRetValbyte1) == 38)
                                {
                                    throw new Exception("Input field (Critical) is not valid");

                                }
                                if ((iRetValbyte1) == 39)
                                {
                                    throw new Exception("Server Authentication is not available in XKU of cert");

                                }
                                if ((iRetValbyte1) == 40)
                                {
                                    throw new Exception("Input value is SERVER_AUTH=T but EKU is empty");

                                }
                                if ((iRetValbyte1) == 41)
                                {
                                    throw new Exception("Server Authentication is available in XKU of cert");

                                }
                                if ((iRetValbyte1) == 42)
                                {
                                    throw new Exception("Input field (Server Auth) is not valid");

                                }
                                if ((iRetValbyte1) == 43)
                                {
                                    throw new Exception("Client Authentication is not available in XKU of cert");

                                }
                                if ((iRetValbyte1) == 44)
                                {
                                    throw new Exception("Input value is CLIENT_AUTH=T but EKU is empty");

                                }
                                if ((iRetValbyte1) == 45)
                                {
                                    throw new Exception("Client Authentication is available in XKU of cert");

                                }
                                if ((iRetValbyte1) == 46)
                                {
                                    throw new Exception("Input field (Client Auth) is not valid");

                                }
                                if ((iRetValbyte1) == 47)
                                {
                                    throw new Exception("Code Signing is not available in XKU of cert");

                                }
                                if ((iRetValbyte1) == 48)
                                {
                                    throw new Exception("Input value is 'CODE_SIGN=T' but EKU is empty");

                                }
                                if ((iRetValbyte1) == 49)
                                {
                                    throw new Exception("Code Signing is available in XKU of cert");

                                }
                                if ((iRetValbyte1) == 50)
                                {
                                    throw new Exception("Input field (Code Signing) is not valid");

                                }
                                if ((iRetValbyte1) == 51)
                                {
                                    throw new Exception("Email Protection is not available in XKU of cert");

                                }
                                if ((iRetValbyte1) == 52)
                                {
                                    throw new Exception("Input value is 'EMAIL_PROTECTION=T' but EKU is empty");

                                }
                                if ((iRetValbyte1) == 53)
                                {
                                    throw new Exception("Email Protection is available in XKU of cert");

                                }
                                if ((iRetValbyte1) == 54)
                                {
                                    throw new Exception("Input field (Email Protection) is not valid");

                                }
                                if ((iRetValbyte1) == 55)
                                {
                                    throw new Exception("Time Stamping is not available in XKU of cert");

                                }
                                if ((iRetValbyte1) == 56)
                                {
                                    throw new Exception("Input value is 'TIME_STAMPING=T' but EKU is empty");

                                }
                                if ((iRetValbyte1) == 57)
                                {
                                    throw new Exception("Time Stamping is available in XKU of cert");

                                }
                                if ((iRetValbyte1) == 58)
                                {
                                    throw new Exception("Input field (Time Stamping) is not valid");

                                }
                                if ((iRetValbyte1) == 59)
                                {
                                    throw new Exception("OCSP Sign is not available in XKU of cert");

                                }
                                if ((iRetValbyte1) == 60)
                                {
                                    throw new Exception("Input value is 'OCSP_SIGN=T' but EKU is empty");

                                }
                                if ((iRetValbyte1) == 61)
                                {
                                    throw new Exception("OCSP Sign is available in XKU of cert");

                                }
                                if ((iRetValbyte1) == 62)
                                {
                                    throw new Exception("Input field (OCSP Sign) is not valid");

                                }
                                if ((iRetValbyte1) == 63)
                                {
                                    throw new Exception("MS Smartcard login is not available in XKU of cert");

                                }
                                if ((iRetValbyte1) == 64)
                                {
                                    throw new Exception("MS Smartcard login is not available in XKU of cert");

                                }
                                if ((iRetValbyte1) == 65)
                                {
                                    throw new Exception("MS Smartcard login is available in XKU of cert");

                                }
                                if ((iRetValbyte1) == 66)
                                {
                                    throw new Exception("Input field (MS Smartcard login) is not valid");

                                }
                                if ((iRetValbyte1) == 67)
                                {
                                    throw new Exception("just Key Encipherment is KU but certificate is for sign");

                                }
                                if ((iRetValbyte1) == 68)
                                {
                                    throw new Exception("End entity user must not have cert Sign Key usage");

                                }
                                if ((iRetValbyte1) == 69)
                                {
                                    throw new Exception("End entity user must not have CRL Sign Key usage");

                                }
                                if ((iRetValbyte1) == 70)
                                {
                                    throw new Exception("cert Sign Key usage is available in End entity user Certificate!");

                                }
                                if ((iRetValbyte1) == 71)
                                {
                                    throw new Exception("CRL Sign Key usage is available in End entity user Certificate!");

                                }
                                if ((iRetValbyte1) == 72)
                                {
                                    throw new Exception("CA must have cert Sign Key usage");

                                }
                                if ((iRetValbyte1) == 73)
                                {
                                    throw new Exception("CA must have cert Sign Key usage");

                                }
                                if ((iRetValbyte1) == 74)
                                {
                                    throw new Exception("Cert Sign Key usage is not available in CA Certificate");

                                }

                                if ((iRetValbyte1) == 75)
                                {
                                    throw new Exception("CRL Sign Key usage is not available in CA Certificate");

                                }
                                if ((iRetValbyte1) == 76)
                                {
                                    throw new Exception("there are not any Key usage in cert");

                                }

                            }
                            break;
                        case 5:
                            {
                                StringBuilder errInfo = new StringBuilder(MAX_BUF);
                                StringBuilder certInfo = new StringBuilder(MAX_BUF);
                                int retValueErr = 0;

                                TokenUtilities.getVCertErrMsg(errInfo, certInfo, out retValueErr);

                                int iRetValErr = (int)retValueErr;
                                string strErrInfo = errInfo.ToString();
                                string strCertInfo = certInfo.ToString();

                                if (iRetValErr == 0)
                                {
                                    if ((iRetValbyte1) == 1)
                                    {
                                        throw new Exception("internal Error");

                                    }
                                    if ((iRetValbyte1) == 2)
                                    {
                                        throw new Exception("Error on loading CA cert or chain file");

                                    }
                                    else
                                    {
                                        throw new Exception(strErrInfo + " , failure certificate :" + certInfo);

                                    }
                                }
                            }
                            break;
                        case 6:
                            throw new Exception("قالب بندي زنجيره اعتماد مناسب نيست ");

                        case 7:
                            throw new Exception("قالب‌بندي گواهي كاربر مناسب نيست");

                        case 8:
                            throw new Exception("قالب گواهي زنجيره اعتماد مناسب نيست");

                        case 9:
                            throw new Exception("ليست ابطال نامعتبر است");

                        case 10:
                            throw new Exception("عمليات تصديق (بدون ليست ابطال) ناموفق ماند");

                        case 11:
                            throw new Exception("عمليات تصديق (با ليست ابطال) ناموفق ماند");

                        case 20:
                            {
                                if ((iRetValbyte1 & 0x01) == 0x01)
                                {
                                    throw new Exception("اين گواهي توسط اين مركز صدور گواهي صادر نشده‌است");

                                }
                                if ((iRetValbyte1 & 0x02) == 0x02)
                                {
                                    throw new Exception("امضای گواهی تصدیق نشد");

                                }
                                if ((iRetValbyte1 & 0x04) == 0x04)
                                {
                                    throw new Exception("گواهی کاربر خارج از دوره اعتبار است");

                                }
                                if ((iRetValbyte1 & 0x08) == 0x08)
                                {
                                    throw new Exception("اين ليست ابطال توسط اين مركز صدور گواهي صادر نشده‌است");

                                }
                                if ((iRetValbyte1 & 0x10) == 0x10)
                                {
                                    throw new Exception("امضای لیست ابطال تصدیق نشد");

                                }
                                if ((iRetValbyte1 & 0x20) == 0x20)
                                {
                                    throw new Exception("لیست‌هاي ابطال يكي از مراكز صدور گواهي خارج از دوره اعتبار است");

                                }
                                if ((iRetValbyte1 & 0x40) == 0x40)
                                {
                                    throw new Exception("گواهی باطل شده‌ است");

                                }
                            }
                            break;
                        default:
                            throw new Exception("گواهی تصدیق نشد");

                    };


                }
                #endregion


                // check if client signiture is valid or not
                #region VerifySignature

                int retVal = 0;

                string MsgChallenge = serverChallenge + clientRandomNumber;

                StringBuilder normalizedMsg = new StringBuilder(MsgChallenge.Length - 1);

                int[] signParam = new int[3];
                signParam[0] = 0;
                signParam[1] = 0;
                signParam[2] = 0;

                try
                {
                    TokenUtilities.setSignMech(2, 2, 1, signParam, out retVal);
                }
                catch 
                {
                    throw new Exception("خطا در تنظيم مكانيزم امضا");

                }
                if ((int)retVal > 0)
                {
                    throw new Exception("خطا در تنظيم مكانيزم امضا");

                }

                try
                {
                    TokenUtilities.normalize(MsgChallenge, normalizedMsg, out retVal);
                }
                catch
                {
                    throw new Exception("خطا در normalize كردن داده ورودي");

                }
                if ((int)retVal > 0)
                {
                    throw new Exception("خطا در normalize كردن داده ورودي");

                }

                try
                {

                    MsgChallenge = normalizedMsg.ToString();
                    TokenUtilities.verify(clientCertificate, 0, 0, MsgChallenge.ToString(), clientChallengeSigned, out retVal);
                }
                catch
                {
                    throw new Exception("خطاي داخلي در تصديق امضا");

                }

                if ((int)retVal == 0)
                {
                    return true;

                    //var user = _iUserServices.GetUserByCertDigets(cert_digest);

                    //_iUserServices.SetLastLoginDate(user.Id, DateTime.Now.Date);
                    //var authTicket = new FormsAuthenticationTicket(1, // version
                    //    user.Username, // user name
                    //    DateTime.Now, // creation
                    //    DateTime.Now.AddMinutes(60), // Expiration
                    //    false, // Persistent
                    //    user.Username); // User data or Roles data
                    //// Now encrypt the ticket.
                    //string encryptedTicket = FormsAuthentication.Encrypt(authTicket);

                    //// Create a cookie and add the encrypted ticket to the cookie as data.
                    //var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);

                    //// Add the cookie to the outgoing cookies collection.
                    //HttpContext.Response.Cookies.Add(authCookie);
                    //// FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);
                    //if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                    //    && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                    //{
                    //    return Redirect(returnUrl);
                    //}
                }
                else
                {
                    if ((int)retVal == 15)
                    {
                        throw new Exception("تابع initPKE فراخواني نشده است");
                    }
                    if ((int)retVal == 28)
                    {
                        throw new Exception("تابع setSignMech فراخواني نشده است");
                    }
                    if ((int)retVal == 22 || (int)retVal == 24 || (int)retVal == 26 || (int)retVal == 25 || (int)retVal == 27)
                    {
                        throw new Exception("امضاء معتبر نمي‌باشد");
                    }
                    else
                    {
                        throw new Exception("امضا تصديق نشد. کد خطا  " + retVal.ToString());

                    }
                }
                #endregion
            }
            return true;
        }
        [HandleProcessCorruptedStateExceptions]
        [SecurityCritical]
        public bool SignatureVerification(string clientCertificate, string clientTextSign, string clientText)
        {
            #region cert validation and signature verification

            if ((clientCertificate != "" && clientTextSign != "") ||
               (clientCertificate == "" && clientTextSign != "") ||
               (clientCertificate != "" && clientTextSign == ""))
            {

                #region initPKE

                int nRetVal = 0;

                try
                {
                    TokenUtilities.initPKE(out nRetVal);
                }
                catch
                {
                    throw new Exception("initPKE خطا در فراخواني تابع");
                }
                if (nRetVal != 0)
                {
                    throw new Exception("initPKE خطا در فراخواني تابع");
                }

                #endregion

                // get pkcs7 sign info
                #region getPKCS7SignInfo

                if (clientTextSign != "")
                {

                    string originDataStr;

                    var originData = new StringBuilder(MAX_BUF);
                    var digist = new StringBuilder(MAX_BUF);
                    var digistAlg = new StringBuilder(MAX_BUF);
                    var signingTime = new StringBuilder(MAX_BUF);
                    var signerCertificate = new StringBuilder(MAX_BUF);

                    try
                    {
                        TokenUtilities.getPkcs7SignInfo(clientTextSign, originData, digist, digistAlg, signingTime, signerCertificate, out nRetVal);
                    }
                    catch 
                    {
                        throw new Exception("خطاي دادر دريافت اطلاعات امضا");
                    }

                    if ((int)nRetVal == 0)
                    {
                        originDataStr = originData.ToString();
                        clientText = originDataStr;
                        if (clientCertificate == "")
                        {
                            clientCertificate = signerCertificate.ToString();
                            String beginStr = "-----BEGIN CERTIFICATE-----";
                            String endStr = "-----END CERTIFICATE-----";
                            String enterStr = "\n";

                            int beginLength = beginStr.Length;
                            int indexBegin = clientCertificate.IndexOf(beginStr);

                            int endLength = endStr.Length;
                            int indexEnd = clientCertificate.IndexOf(endStr);

                            int enterLength = enterStr.Length;
                            int indexEnter = clientCertificate.IndexOf(enterStr);
                            while (indexBegin != -1)
                            {
                                indexBegin = clientCertificate.IndexOf(beginStr);
                                clientCertificate = clientCertificate.Replace(beginStr, "");
                            }
                            while (indexEnd != -1)
                            {
                                indexEnd = clientCertificate.IndexOf(endStr);
                                clientCertificate = clientCertificate.Replace(endStr, "");
                            }
                            while (indexEnter != -1)
                            {
                                indexEnter = clientCertificate.IndexOf(enterStr);
                                clientCertificate = clientCertificate.Replace(enterStr, "");
                            }
                        }
                    }
                    else
                    {
                        throw new Exception("خطا در دریافت داده اصلی از ساختار امضا");
                    }
                }

                #endregion

                // check if the certificate is valid or not
                #region VerifyCertificate

                try
                {
                    string strCertUsage = "KeyUsage::C=F,DIGITAL_SIGNATURE=T,NON_REPUDIATION=T,KEY_ENCIPHERMENT=F,DATA_ENCIPHERMENT=F,KEY_AGREEMENT=F,KEY_CERT_SIGN=F,CRL_SIGN=F,ENCIPHER_ONLY=F,DECIPHER_ONLY=F;ExtendedKeyUsage::C=F,SERVER_AUTH=F,CLIENT_AUTH=F,CODE_SIGN=F,EMAIL_PROTECTION=F,TIME_STAMPING=F,OCSP_SIGN=F,SMART_CARD_LOGIN=F";
                    TokenUtilities.verifyCert(clientCertificate, "trustchain.pem", strCertUsage, out nRetVal);
                }
                catch 
                {
                    throw new Exception("خطاي داخلي در تصديق گواهي");
                }

                int iRetValbyte0 = 0x000000ff & nRetVal;
                int iRetValbyte1 = 0x0000ff00 & nRetVal;
                iRetValbyte1 >>= 8;
                if (nRetVal == 0)
                {
                    //throw new Exception("گواهي تصديق شد");
                    //Log(strPage, "server", "verifyCert", userName, "success", userName + "The certificate is verifiedsuccessfully ");
                }
                else
                {
                    switch (iRetValbyte0)
                    {

                        case 2:
                            throw new Exception("قالب‌بندي گواهي كاربر مناسب نيست");

                        case 4:
                            {
                                if ((iRetValbyte1) == 1)
                                {
                                    throw new Exception("Key usage of cert is not critical");
                                }
                                if ((iRetValbyte1) == 2)
                                {
                                    throw new Exception("Key usage of cert is critical in while input key usage is not critical");
                                }
                                if ((iRetValbyte1) == 3)
                                {
                                    throw new Exception("Input field (Critical) is not valid");
                                }
                                if ((iRetValbyte1) == 4)
                                {
                                    throw new Exception("Digital Signature is not available in cert");
                                }
                                if ((iRetValbyte1) == 5)
                                {
                                    throw new Exception("Input value is 'DIGITAL_SIGNATURE=T' but KU is empty");
                                }
                                if ((iRetValbyte1) == 6)
                                {
                                    throw new Exception("Digital Signature is available in cert");
                                }
                                if ((iRetValbyte1) == 7)
                                {
                                    throw new Exception("Input field (DigitalSignature) is not valid ");
                                }
                                if ((iRetValbyte1) == 8)
                                {
                                    throw new Exception("Non Repudiation is not available in cert ");
                                }
                                if ((iRetValbyte1) == 9)
                                {
                                    throw new Exception("Input value is 'NON_REPUDIATION=T' but KU is empty");
                                }
                                if ((iRetValbyte1) == 10)
                                {
                                    throw new Exception("Non Repudiation is available in cert");
                                }
                                if ((iRetValbyte1) == 11)
                                {
                                    throw new Exception("Input field (NonRepudiation) is not valid");
                                }
                                if ((iRetValbyte1) == 12)
                                {
                                    throw new Exception("Key Encipherment is not available in cert");
                                }
                                if ((iRetValbyte1) == 13)
                                {
                                    throw new Exception("Input value is 'KEY_ENCIPHERMENT=T' but KU is empty");
                                }
                                if ((iRetValbyte1) == 14)
                                {
                                    throw new Exception("Key Encipherment is available in cert");
                                }
                                if ((iRetValbyte1) == 15)
                                {
                                    throw new Exception("Input field (KeyEncipherment) is not valid");
                                }
                                if ((iRetValbyte1) == 16)
                                {
                                    throw new Exception("Data Encipherment is not available in cert");
                                }
                                if ((iRetValbyte1) == 17)
                                {
                                    throw new Exception("Input value is 'DATA_ENCIPHERMENT=T' but KU is empty");
                                }
                                if ((iRetValbyte1) == 18)
                                {
                                    throw new Exception("Data Encipherment is available in cert");
                                }
                                if ((iRetValbyte1) == 19)
                                {
                                    throw new Exception("Input field (DataEncipherment) is not valid");
                                }
                                if ((iRetValbyte1) == 20)
                                {
                                    throw new Exception("Key agreeement is not available in cert");
                                }
                                if ((iRetValbyte1) == 21)
                                {
                                    throw new Exception("Input value is 'KEY_AGREEMENT=T' but KU is empty");
                                }
                                if ((iRetValbyte1) == 22)
                                {
                                    throw new Exception("Key agreeement is available in cert");
                                }
                                if ((iRetValbyte1) == 23)
                                {
                                    throw new Exception("Input field (Keyagreeement) is not valid");
                                }
                                if ((iRetValbyte1) == 24)
                                {
                                    throw new Exception("Encipher Only is not available in cert");
                                }
                                if ((iRetValbyte1) == 25)
                                {
                                    throw new Exception("Input value is 'ENCIPHER_ONLY=T' but KU is empty");
                                }
                                if ((iRetValbyte1) == 26)
                                {
                                    throw new Exception("Encipher Only is available in cert");
                                }
                                if ((iRetValbyte1) == 27)
                                {
                                    throw new Exception("Input field (EncipherOnly) is not valid");
                                }
                                if ((iRetValbyte1) == 28)
                                {
                                    throw new Exception("Decipher Only is not available in cert");
                                }
                                if ((iRetValbyte1) == 29)
                                {
                                    throw new Exception("Input value is 'ENCIPHER_ONLY=T' but KU is empty");
                                }
                                if ((iRetValbyte1) == 30)
                                {
                                    throw new Exception("Decipher Only is available in cert");
                                }
                                if ((iRetValbyte1) == 31)
                                {
                                    throw new Exception("Input field (DencipherOnly) is not valid");
                                }
                                if ((iRetValbyte1) == 32)
                                {
                                    throw new Exception("Input value is 'KEY_CERT_SIGN=T' but KU is empty");
                                }
                                if ((iRetValbyte1) == 33)
                                {
                                    throw new Exception("Input field (KeyCertSign) is not valid");
                                }
                                if ((iRetValbyte1) == 34)
                                {
                                    throw new Exception("Input value is 'CRL_SIGN=T' but KU is empty");
                                }
                                if ((iRetValbyte1) == 35)
                                {
                                    throw new Exception("Input field (CRLSign) is not valid");
                                }
                                if ((iRetValbyte1) == 36)
                                {
                                    throw new Exception("EKey usage of cert is not critical");
                                }
                                if ((iRetValbyte1) == 37)
                                {
                                    throw new Exception("EKey usage of cert is critical");
                                }
                                if ((iRetValbyte1) == 38)
                                {
                                    throw new Exception("Input field (Critical) is not valid");
                                }
                                if ((iRetValbyte1) == 39)
                                {
                                    throw new Exception("Server Authentication is not available in XKU of cert");
                                }
                                if ((iRetValbyte1) == 40)
                                {
                                    throw new Exception("Input value is SERVER_AUTH=T but EKU is empty");
                                }
                                if ((iRetValbyte1) == 41)
                                {
                                    throw new Exception("Server Authentication is available in XKU of cert");
                                }
                                if ((iRetValbyte1) == 42)
                                {
                                    throw new Exception("Input field (Server Auth) is not valid");
                                }
                                if ((iRetValbyte1) == 43)
                                {
                                    throw new Exception("Client Authentication is not available in XKU of cert");
                                }
                                if ((iRetValbyte1) == 44)
                                {
                                    throw new Exception("Input value is CLIENT_AUTH=T but EKU is empty");
                                }
                                if ((iRetValbyte1) == 45)
                                {
                                    throw new Exception("Client Authentication is available in XKU of cert");
                                }
                                if ((iRetValbyte1) == 46)
                                {
                                    throw new Exception("Input field (Client Auth) is not valid");
                                }
                                if ((iRetValbyte1) == 47)
                                {
                                    throw new Exception("Code Signig is not available in XKU of cert");
                                }
                                if ((iRetValbyte1) == 48)
                                {
                                    throw new Exception("Input value is 'CODE_SIGN=T' but EKU is empty");
                                }
                                if ((iRetValbyte1) == 49)
                                {
                                    throw new Exception("Code Signig is available in XKU of cert");
                                }
                                if ((iRetValbyte1) == 50)
                                {
                                    throw new Exception("Input field (Code Signing) is not valid");
                                }
                                if ((iRetValbyte1) == 51)
                                {
                                    throw new Exception("Email Protection is not available in XKU of cert");
                                }
                                if ((iRetValbyte1) == 52)
                                {
                                    throw new Exception("Input value is 'EMAIL_PROTECTION=T' but EKU is empty");
                                }
                                if ((iRetValbyte1) == 53)
                                {
                                    throw new Exception("Email Protection is available in XKU of cert");
                                }
                                if ((iRetValbyte1) == 54)
                                {
                                    throw new Exception("Input field (Email Protection) is not valid");
                                }
                                if ((iRetValbyte1) == 55)
                                {
                                    throw new Exception("Time Stamping is not available in XKU of cert");
                                }
                                if ((iRetValbyte1) == 56)
                                {
                                    throw new Exception("Input value is 'TIME_STAMPING=T' but EKU is empty");
                                }
                                if ((iRetValbyte1) == 57)
                                {
                                    throw new Exception("Time Stamping is available in XKU of cert");
                                }
                                if ((iRetValbyte1) == 58)
                                {
                                    throw new Exception("Input field (Time Stamping) is not valid");
                                }
                                if ((iRetValbyte1) == 59)
                                {
                                    throw new Exception("OCSP Sign is not available in XKU of cert");
                                }
                                if ((iRetValbyte1) == 60)
                                {
                                    throw new Exception("Input value is 'OCSP_SIGN=T' but EKU is empty");
                                }
                                if ((iRetValbyte1) == 61)
                                {
                                    throw new Exception("OCSP Sign is available in XKU of cert");
                                }
                                if ((iRetValbyte1) == 62)
                                {
                                    throw new Exception("Input field (OCSP Sign) is not valid");
                                }
                                if ((iRetValbyte1) == 63)
                                {
                                    throw new Exception("MS Smartcard login is not available in XKU of cert");
                                }
                                if ((iRetValbyte1) == 64)
                                {
                                    throw new Exception("MS Smartcard login is not available in XKU of cert");
                                }
                                if ((iRetValbyte1) == 65)
                                {
                                    throw new Exception("MS Smartcard login is available in XKU of cert");
                                }
                                if ((iRetValbyte1) == 66)
                                {
                                    throw new Exception("Input field (MS Smartcard login) is not valid");
                                }
                                if ((iRetValbyte1) == 67)
                                {
                                    throw new Exception("just Key Encipherment is KU but certificate is for sign");
                                }
                                if ((iRetValbyte1) == 68)
                                {
                                    throw new Exception("End entity user must not have cert Sign Key usage");
                                }
                                if ((iRetValbyte1) == 69)
                                {
                                    throw new Exception("End entity user must not have CRL Sign Key usage");
                                }
                                if ((iRetValbyte1) == 70)
                                {
                                    throw new Exception("cert Sign Key usage is available in End entity user Certificate!");
                                }
                                if ((iRetValbyte1) == 71)
                                {
                                    throw new Exception("CRL Sign Key usage is available in End entity user Certificate!");
                                }
                                if ((iRetValbyte1) == 72)
                                {
                                    throw new Exception("CA must have cert Sign Key usage");
                                }
                                if ((iRetValbyte1) == 73)
                                {
                                    throw new Exception("CA must have cert Sign Key usage");
                                }
                                if ((iRetValbyte1) == 74)
                                {
                                    throw new Exception("Cert Sign Key usage is not available in CA Certificate");
                                }
                                if ((iRetValbyte1) == 75)
                                {
                                    throw new Exception("CRL Sign Key usage is not available in CA Certificate");
                                }
                                if ((iRetValbyte1) == 76)
                                {
                                    throw new Exception("there are not any Key usage in cert");
                                }
                            }
                            break;
                        case 5:
                            {
                                StringBuilder errInfo = new StringBuilder(MAX_BUF);
                                StringBuilder certInfo = new StringBuilder(MAX_BUF);
                                int retValueErr = 0;

                                TokenUtilities.getVCertErrMsg(errInfo, certInfo, out retValueErr);

                                int iRetValErr = (int)retValueErr;
                                string strErrInfo = errInfo.ToString();
                                string strCertInfo = certInfo.ToString();

                                if (iRetValErr == 0)
                                {
                                    if ((iRetValbyte1) == 1)
                                    {
                                        throw new Exception("internal error");
                                    }
                                    if ((iRetValbyte1) == 2)
                                    {
                                        throw new Exception("Error on loading CA cert or chain file");
                                    }
                                    else
                                    {
                                        throw new Exception(strErrInfo + " , failure certificate :" + certInfo);
                                    }
                                }
                            }
                            break;
                        case 6:

                            throw new Exception(" قالب بندي زنجيره اعتماد مناسب نيست ");
                        case 7:
                            throw new Exception("قالب‌بندي گواهي كاربر مناسب نيست");
                        case 8:
                            throw new Exception("قالب گواهي زنجيره اعتماد مناسب نيست");
                        case 9:
                            throw new Exception("ليست ابطال نامعتبر است");
                        case 10:
                            throw new Exception("عمليات تصديق (بدون ليست ابطال) ناموفق ماند");
                        case 11:
                            throw new Exception("عمليات تصديق (با ليست ابطال) ناموفق ماند");
                        case 20:
                            {
                                if ((iRetValbyte1 & 0x01) == 0x01)
                                {
                                    throw new Exception("اين گواهي توسط اين مركز صدور گواهي صادر نشده‌است");
                                }
                                if ((iRetValbyte1 & 0x02) == 0x02)
                                {
                                    throw new Exception("امضای گواهی تصدیق نشد");
                                }
                                if ((iRetValbyte1 & 0x04) == 0x04)
                                {
                                    throw new Exception("گواهی کاربر خارج از دوره اعتبار است");
                                }
                                if ((iRetValbyte1 & 0x08) == 0x08)
                                {
                                    throw new Exception("اين ليست ابطال توسط اين مركز صدور گواهي صادر نشده‌است");
                                }
                                if ((iRetValbyte1 & 0x10) == 0x10)
                                {
                                    throw new Exception("امضای لیست ابطال تصدیق نشد");
                                }
                                if ((iRetValbyte1 & 0x20) == 0x20)
                                {
                                    throw new Exception("لیست‌هاي ابطال يكي از مراكز صدور گواهي خارج از دوره اعتبار است");
                                }
                                if ((iRetValbyte1 & 0x40) == 0x40)
                                {
                                    throw new Exception("گواهی باطل شده‌ است");
                                }
                            }
                            break;
                        default:

                            throw new Exception("گواهی تصدیق نشد  ");
                    };
                }

                #endregion

                // check if client signiture is valid or not
                #region VerifySignature
                if (clientTextSign != "")
                {
                    int retVal = 0;
                    int[] signParam = new int[3];

                    signParam[0] = 0;
                    signParam[1] = 0;
                    signParam[2] = 0;

                    StringBuilder normalizedMsg = new StringBuilder(clientText.Length - 1);
                    try
                    {
                        TokenUtilities.setSignMech(3, 2, 1, signParam, out retVal);
                    }
                    catch 
                    {
                        throw new Exception("خطا در تنظيم مكانيزم امضا");
                    }
                    if ((int)retVal > 0)
                    {
                        throw new Exception("خطا در تنظيم مكانيزم امضا");
                    }
                    try
                    {
                        TokenUtilities.normalize(clientText.ToString(), normalizedMsg, out retVal);
                    }
                    catch 
                    {
                        throw new Exception("خطا در normalize كردن داده ورودي");
                    }
                    if ((int)retVal > 0)
                    {
                        throw new Exception("خطا در normalize كردن داده ورودي");
                    }

                    try
                    {
                        TokenUtilities.verify(clientCertificate, 0, 0, normalizedMsg.ToString(), clientTextSign, out retVal);
                    }
                    catch 
                    {
                        throw new Exception("خطاي داخلي در تصديق امضا");
                    }

                    if ((int)retVal == 0)
                    {
                        //SignVeriReslabel.ForeColor = System.Drawing.Color.Green;
                        //SignVeriReslabel.Text = "امضا تصديق شد";
                    }
                    else
                    {
                        if ((int)retVal == 15)
                        {
                            throw new Exception("تابع initPKE فراخواني نشده است");
                        }
                        if ((int)retVal == 28)
                        {
                            throw new Exception("تابع setSignMech فراخواني نشده است ");
                        }
                        if ((int)retVal == 22 || (int)retVal == 24 || (int)retVal == 26 || (int)retVal == 25 || (int)retVal == 27)
                        {

                            throw new Exception("امضاء معتبر نمي‌باشد");
                        }
                        else
                        {
                            throw new Exception("امضا تصديق نشد. کد خطا  " + retVal.ToString());
                        }
                    }
                }
                #endregion

                // if these three steps passed successfully

            }

            #endregion


            return true;
        }
        [HandleProcessCorruptedStateExceptions]
        [SecurityCritical]
        public TokenSubject ParseTokenSubject(string certificate)
        {
            TokenSubject result = new TokenSubject();

            StringBuilder cert = new StringBuilder(certificate);



            int retValue;
            StringBuilder subject = new StringBuilder(4000);
            StringBuilder puKey = new StringBuilder(4000);
            StringBuilder keyid = new StringBuilder(4000);

            TokenUtilities.getCertData(cert, 1, subject, puKey, keyid, out retValue);

            byte[] bytes = Encoding.Default.GetBytes(subject.ToString());
            string s = Encoding.UTF8.GetString(bytes);

            string[] parts = s.ToString().Split('/');
            try { result.C = parts.Where(x => x.Contains("C=")).FirstOrDefault().Replace("C=", ""); } catch { }
            try { result.S = parts.Where(x => x.Contains("S=")).ToArray()[0].Replace("S=", ""); } catch { }
            try { result.L = parts.Where(x => x.Contains("L=")).FirstOrDefault().Replace("L=", ""); } catch { }
            try { result.O = parts.Where(x => x.Contains("O=")).FirstOrDefault().Replace("O=", ""); } catch { }
            try { result.OU = parts.Where(x => x.Contains("OU=")).FirstOrDefault().Replace("OU=", ""); } catch { }
            try { result.CN = parts.Where(x => x.Contains("CN=")).FirstOrDefault().Replace("CN=", ""); } catch { }
            try { result.E = parts.Where(x => x.Contains("E=")).FirstOrDefault().Replace("E=", ""); } catch { }
            try { result.SN = parts.Where(x => x.Contains("SN=")).FirstOrDefault().Replace("SN=", ""); } catch { }
            try { result.G = parts.Where(x => x.Contains("G=")).FirstOrDefault().Replace("G=", ""); } catch { }
            try { result.S2 = parts.Where(x => x.Contains("S=")).ToArray()[1].Replace("S=", ""); } catch { }
            try { result.T1 = s.Substring(s.IndexOf("/T=")).Split(',')[0]; } catch { }
            try { result.T2 = s.Substring(s.IndexOf("/T=")).Split(',')[1]; } catch { }
            return result;
        }
        [HandleProcessCorruptedStateExceptions]
        [SecurityCritical]
        public string GetThumpprint(string certificate)
        {
            string result = string.Empty;
            const string fieldName = "Thumbprint:";
            int retValue;
            StringBuilder cert = new StringBuilder(certificate);
            StringBuilder certInfo = new StringBuilder(4000);
            TokenUtilities.getInfoCertificate(cert, 1, certInfo, out retValue);
            int loc = certInfo.ToString().IndexOf(fieldName) + fieldName.Length;
            result = certInfo.ToString().Substring(loc, 59).Replace(" ", "").ToUpper();

            return result;
        }
        [HandleProcessCorruptedStateExceptions]
        [SecurityCritical]
        public string GetSerialNumber(string certificate)
        {
            StringBuilder ovCertInfo = new StringBuilder(4000);
            int retValue;
            TokenUtilities.getInfoCertificate(new StringBuilder(certificate), 1, ovCertInfo, out retValue);
            string[] parts = ovCertInfo.ToString().Split(';');
            string serialPart = parts.Where(x => x.ToLower().Contains("serial")).First();
            string result = serialPart.ToLower().Replace("serial", "").Replace(":", "").Trim();
            return result;
        }
        [HandleProcessCorruptedStateExceptions]
        [SecurityCritical]
        public string ConvertHexToString(string hexData)
        {

            string strDataTmp = "";
            string strData = "";
            string strHexData = hexData;

            /*---- first take two hex value using substring. ----*/
            /*---- then convert Hex value into ascii. ----*/
            /*---- then convert ascii value into character. ----*/
            while (strHexData.Length > 0)
            {
                strDataTmp = System.Convert.ToChar(System.Convert.ToUInt32(strHexData.Substring(0, 2), 16)).ToString();
                strData = strData + strDataTmp;
                strHexData = strHexData.Substring(2, strHexData.Length - 2);
            }
            return strData;
        }
        [HandleProcessCorruptedStateExceptions]
        [SecurityCritical]
        public SignInfo GetSignInfo(string clientTextSign)
        {
            SignInfo result = null;
            int nRetVal = 0;

            try
            {
                TokenUtilities.initPKE(out nRetVal);
            }
            catch
            {
                throw new Exception("initPKE خطا در فراخواني تابع");
            }
            if (nRetVal != 0)
            {
                throw new Exception("initPKE خطا در فراخواني تابع");
            }

            // get pkcs7 sign info
            #region getPKCS7SignInfo

            if (clientTextSign != "")
            {

                //string originDataStr;

                var originData = new StringBuilder(MAX_BUF);
                var digist = new StringBuilder(MAX_BUF);
                var digistAlg = new StringBuilder(MAX_BUF);
                var signingTime = new StringBuilder(MAX_BUF);
                var signerCertificate = new StringBuilder(MAX_BUF);

                try
                {
                    TokenUtilities.getPkcs7SignInfo(clientTextSign, originData, digist, digistAlg, signingTime, signerCertificate, out nRetVal);

                    byte[] bytes = Encoding.Default.GetBytes(originData.ToString());
                    string strOriginData = Encoding.UTF8.GetString(bytes);


                    result = new SignInfo()
                    {
                        Digest = digist.ToString(),
                        DigestAlg = digistAlg.ToString(),
                        OriginData = strOriginData.ToString(),
                        //SigningTime = DateTime.ParseExact(signingTime.ToString(),
                        //              "MMM dd hh:mm:ss yyyy Z",
                        //               CultureInfo.InvariantCulture).ToLongTimeString(),
                        SigningTime = signingTime.ToString(),
                        Subject = this.ParseTokenSubject(signerCertificate.ToString())
                    };
                }
                catch 
                {
                    throw new Exception("خطاي دادر دريافت اطلاعات امضا");
                }

                //if ((int)nRetVal == 0)
                //{
                //    originDataStr = originData.ToString();
                //    clientText = originDataStr;
                //    if (clientCertificate == "")
                //    {
                //        clientCertificate = signerCertificate.ToString();
                //        String beginStr = "-----BEGIN CERTIFICATE-----";
                //        String endStr = "-----END CERTIFICATE-----";
                //        String enterStr = "\n";

                //        int beginLength = beginStr.Length;
                //        int indexBegin = clientCertificate.IndexOf(beginStr);

                //        int endLength = endStr.Length;
                //        int indexEnd = clientCertificate.IndexOf(endStr);

                //        int enterLength = enterStr.Length;
                //        int indexEnter = clientCertificate.IndexOf(enterStr);
                //        while (indexBegin != -1)
                //        {
                //            indexBegin = clientCertificate.IndexOf(beginStr);
                //            clientCertificate = clientCertificate.Replace(beginStr, "");
                //        }
                //        while (indexEnd != -1)
                //        {
                //            indexEnd = clientCertificate.IndexOf(endStr);
                //            clientCertificate = clientCertificate.Replace(endStr, "");
                //        }
                //        while (indexEnter != -1)
                //        {
                //            indexEnter = clientCertificate.IndexOf(enterStr);
                //            clientCertificate = clientCertificate.Replace(enterStr, "");
                //        }
                //    }
                //}
                //else
                //{
                //    throw new Exception("خطا در دریافت داده اصلی از ساختار امضا");
                //}
            }

            return result;

            #endregion
        }
    }
}
