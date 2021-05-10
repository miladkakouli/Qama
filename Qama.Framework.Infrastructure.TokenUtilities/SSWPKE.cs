using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Qama.Framework.Infrastructure.TokenUtilities
{
    public class SSWPKE
    {
        #region Define Public Configuration

        public class Config
        {
            public string auth_cert_usage = "KeyUsage::C=F,DIGITAL_SIGNATURE=T,NON_REPUDIATION=T,KEY_ENCIPHERMENT=F,DATA_ENCIPHERMENT=F,KEY_AGREEMENT=F,KEY_CERT_SIGN=F,CRL_SIGN=F,ENCIPHER_ONLY=F,DECIPHER_ONLY=F;ExtendedKeyUsage::C=F,SERVER_AUTH=F,CLIENT_AUTH=T,CODE_SIGN=F,EMAIL_PROTECTION=F,TIME_STAMPING=F,OCSP_SIGN=F,SMART_CARD_LOGIN=F";
            public string sign_cert_usage = "KeyUsage::C=F,DIGITAL_SIGNATURE=T,NON_REPUDIATION=T,KEY_ENCIPHERMENT=F,DATA_ENCIPHERMENT=F,KEY_AGREEMENT=F,KEY_CERT_SIGN=F,CRL_SIGN=F,ENCIPHER_ONLY=F,DECIPHER_ONLY=F;ExtendedKeyUsage::C=F,SERVER_AUTH=F,CLIENT_AUTH=F,CODE_SIGN=F,EMAIL_PROTECTION=F,TIME_STAMPING=F,OCSP_SIGN=F,SMART_CARD_LOGIN=F";
            public string no_cert_usage = "";

            // --- Define Variables For Sign Setting ---
            public int signType;
            public int signFormat;
            public int signMechanism;
            public int signHashAlg;
            public int mgf;
            public int saltLen;
            // --- End Of Define Variables For Sign Setting ---

            // --- Define Variables For Cert Usage ---
            public string certUsage;
            // --- End Of Define Variables For Cert Usage ---

            // --- Define Variables For TBS Setting ---
            public string tbs;
            public int tbsType;
            public int tbsHashAlg;
            // --- End Of Define Variables For TBS Setting ---

            // --- Define Variables For Crypt Setting ---
            public int cryptOpType;
            public int cryptType;
            public int cryptFormat;
            public int cryptMech;
            public int cipherAlg;
            public int cryptHashParam;
            public int cryptMgfParam;
            public int cryptSaltLenParam;
            public string cryptMechParam2;
            // --- End Of Define Variables For Crypt Setting ---

            // --- Define Variables For data Setting ---

            public string data;
            public int dataType;
            // --- End Of Define Variables For data Setting ---

            public string serverPageName;

            public Config()
            {
                // --- Set Value For Sign Setting ---
                this.signType = 0; // Graphic = 1
                this.signFormat = 2;// PK_PKCS7_ATTACHED
                this.signMechanism = 1;// PK_RSA_PKCS
                this.signHashAlg = 2;// PK_SHA1
                this.mgf = 0;// PK_NOHASH
                this.saltLen = -1;// 
                                  // --- End Of Set Value For Sign Setting ---

                // --- Set Value For Cert Usage ---
                this.certUsage = "";//NO_USAGE; 
                                    // --- End Of Set Value For Cert Usage ---

                // --- Set Value For TBS Setting ---
                this.tbs = "";
                this.tbsType = 0;// PK_RAW
                this.tbsHashAlg = 0;// PK_NOHASH
                                    // --- End Of Set Value For TBS Setting ---


                // --- Set Value For Crypt Setting ---
                this.cryptOpType = 0;
                this.cryptType = 1;
                this.cryptFormat = 2;
                this.cryptMech = 1;
                this.cipherAlg = 7;
                this.cryptHashParam = 0;
                this.cryptMgfParam = 0;
                this.cryptSaltLenParam = 0;
                this.cryptMechParam2 = "";
                // --- End Of Set Value For Crypt Setting ---

                // --- Set Value For data Setting ---
                // this.data = ""; //set from client
                this.dataType = 0;
                // --- End Of Set Value For data Setting ---


            }

            public Config(string ivCertUsage)
            {
                // --- Set Value For Sign Setting ---
                this.signType = 0;
                this.signFormat = 2;// PK_PKCS7_ATTACHED
                this.signMechanism = 1;// PK_RSA_PKCS
                this.signHashAlg = 2;// PK_SHA1
                this.mgf = 0;// PK_NOHASH
                this.saltLen = -1;// 
                                  // --- End Of Set Value For Sign Setting ---

                // --- Set Value For Cert Usage ---
                this.certUsage = ivCertUsage;
                // --- End Of Set Value For Cert Usage ---

                // --- Set Value For TBS Sestting ---
                this.tbs = "";
                this.tbsType = 0;// PK_RAW
                this.tbsHashAlg = 0;// PK_NOHASH
                                    // --- End Of Set Value For TBS Setting ---

                // --- Set Value For Crypt Setting ---
                this.cryptOpType = 0; //Encryption
                this.cryptType = 1; //ASYM
                this.cryptFormat = 2; // PK_CRYPT_PKCS7
                this.cryptMech = 1; //PK_CRYPT_RSA_PKCS
                this.cipherAlg = 7; //PK_AES_CBC
                this.cryptHashParam = 0; //PK_SHA1
                this.cryptMgfParam = 0; //PK_MGF_SHA1
                this.cryptSaltLenParam = 0;
                this.cryptMechParam2 = "";
                // --- End Of Set Value For Crypt Setting ---

                // --- Set Value For data Setting ---
                this.dataType = 0;
                // --- End Of Set Value For data Setting ---


            }

            public Config(int[] ivSignCfg, string ivCertUsage, string[] ivTBSCfg, string ivServerPage)
            {
                // --- Set Value For Sign Setting ---
                if (ivSignCfg != null)
                {
                    if (ivSignCfg[0] != -1)
                        this.signType = ivSignCfg[0];
                    else
                        this.signType = 0;

                    if (ivSignCfg[1] != -1)
                        this.signFormat = ivSignCfg[1];
                    else
                        this.signFormat = 2;

                    if (ivSignCfg[2] != -1)
                        this.signMechanism = ivSignCfg[2];
                    else
                        this.signMechanism = 1;

                    if (ivSignCfg[4] != -1)
                        this.signHashAlg = ivSignCfg[3];
                    else
                        this.signHashAlg = 2;// PK_SHA1

                    if (ivSignCfg[4] != -1)
                        this.mgf = ivSignCfg[4];
                    else
                        this.mgf = 0;

                    if (ivSignCfg[5] != -1)
                        this.saltLen = ivSignCfg[5];
                    else
                        this.saltLen = -1;
                }
                // --- End Of Set Value For Sign Setting ---

                // --- Set Value For Cert Usage ---
                if (ivCertUsage != null)
                    this.certUsage = ivCertUsage;
                else
                    this.certUsage = "";
                // --- End Of Set Value For Cert Usage ---

                // --- Set Value For TBS Setting ---
                if (ivTBSCfg != null)
                {
                    if (ivTBSCfg[0] != "")
                        this.tbs = ivTBSCfg[0];
                    else
                        this.tbs = "";

                    if (ivTBSCfg[1] != "")
                        this.tbsType = Convert.ToInt32(ivTBSCfg[1]);
                    else
                        this.tbsType = 0;// PK_RAW

                    if (ivTBSCfg[2] != "")
                        this.tbsHashAlg = Convert.ToInt32(ivTBSCfg[2]);
                    else
                        this.tbsHashAlg = 0;// PK_NOHASH
                }
                // --- End Of Set Value For TBS Setting ---

                this.serverPageName = ivServerPage;
            }

            public Config(string[] ivCryptCfg, int ivdataCfg)
            {


                // --- Set Value For Crypt Setting ---
                if (ivCryptCfg != null)
                {
                    if (ivCryptCfg[0] != "")
                        this.cryptOpType = Convert.ToInt32(ivCryptCfg[0]);
                    else
                        this.cryptOpType = 0;

                    if (ivCryptCfg[1] != "")
                        this.cryptType = Convert.ToInt32(ivCryptCfg[1]);
                    else
                        this.cryptType = 1;

                    if (ivCryptCfg[2] != "")
                        this.cryptFormat = Convert.ToInt32(ivCryptCfg[2]);
                    else
                        this.cryptFormat = 2;

                    if (ivCryptCfg[3] != "")
                        this.cryptMech = Convert.ToInt32(ivCryptCfg[3]);
                    else
                        this.cryptMech = 1;

                    if (ivCryptCfg[4] != "")
                        this.cipherAlg = Convert.ToInt32(ivCryptCfg[4]);
                    else
                        this.cipherAlg = 7;

                    if (ivCryptCfg[5] != "")
                        this.cryptHashParam = Convert.ToInt32(ivCryptCfg[5]);
                    else
                        this.cryptHashParam = 0;

                    if (ivCryptCfg[6] != "")
                        this.cryptMgfParam = Convert.ToInt32(ivCryptCfg[6]);
                    else
                        this.cryptMgfParam = 0;

                    if (ivCryptCfg[7] != "")
                        this.cryptSaltLenParam = Convert.ToInt32(ivCryptCfg[7]);
                    else
                        this.cryptSaltLenParam = 0;

                    if (ivCryptCfg[8] != "")
                        this.cryptMechParam2 = ivCryptCfg[8];
                    else
                        this.cryptMechParam2 = "";
                }
                // --- End Of Set Value For Sign Setting ---

                // --- Set Value For data Setting ---
                //if (ivdataCfg != null)
                //{

                    if (ivdataCfg != -1)
                        this.dataType = ivdataCfg;
                    else
                        this.dataType = 0;
                //}

                // --- End Of Set Value For data Setting ---

            }
        }

        #endregion

        #region Define Constant

        int MAX_BUF = 1000000;
        int BUF_SIZE1 = 1024;
        int BUF_SIZE2 = 10 * 4000;
        //int BUF_SIZE3 = 100;

        #endregion

        #region SSWPKE Function Definition

        [DllImport("sswpke_fs.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int initPKE(out int ovRetVal);

        [DllImport("sswpke_fs.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int setCryptoki(String ivCryptokiPath, String ivConf, out int ovRetVal);

        [DllImport("sswpke_fs.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int getSlotCount(out int ovSlotCount, out int ovRetVal);

        [DllImport("sswpke_fs.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int getSlotInfo(int ivSlotIdx, StringBuilder ovLabel, StringBuilder ovManufacturerID, StringBuilder ovModel, StringBuilder ovSerialNumber, out int ovRetVal);

        [DllImport("sswpke_fs.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int checkToken(int ivSlotIdx, out int ovPresent, out int ovRetVal);

        [DllImport("sswpke_fs.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int getPubKeys(int ivSlotIdx, StringBuilder ovSubjects, StringBuilder ovPubKeys, out int ovRetVal);

        [DllImport("sswpke_fs.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int getCertById(String ivPubKey, StringBuilder ovCert, out int ovRetVal);

        [DllImport("sswpke_fs.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int changePIN(String ivOldPIN, String ivNewPIN, out int ovRetVal);

        [DllImport("sswpke_fs.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int getCN(String ivCert, StringBuilder ovCN, out int ovRetVal);

        [DllImport("sswpke_fs.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int getRandom(StringBuilder ovBuf, int ivSize, out int ovRetVal);

        [DllImport("sswpke_fs.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int getKeyPairs(StringBuilder ovKeyIds, out int ovRetVal);

        [DllImport("sswpke_fs.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int injectCert(String ivCert, StringBuilder ovCertInfo, out int ovRetVal);

        [DllImport("sswpke_fs.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int getTokenCertsInfo(int ivSlotIdx, StringBuilder ovKeyIds, StringBuilder ovPubKeys, StringBuilder ovSubjects, out int ovRetVal);

        [DllImport("sswpke_fs.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int getKeysId(int ivObjCheck, StringBuilder ovKeyIds, out int ovRetVal);

        [DllImport("sswpke_fs.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int initENG(String ivEngPath, out int ovRetVal);

        [DllImport("sswpke_fs.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int getCertExtension(int ivSel, String ivCert, StringBuilder ovCertEXTInfo, out int ovRetVal);

        [DllImport("sswpke_fs.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int genRandom(int ivSlotIdx, int ivSize, StringBuilder ovBuf, out int ovRetVal);

        [DllImport("sswpke_fs.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int genSymmetricKey(int ivKeyGenMech, String ivKeyLabel, int ivKeyLen, int ivExtractable, int ivSensitive, out ulong ovHKey, out int ovRetVal);

        [DllImport("sswpke_fs.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int getInfoCertificate(String ivCert, int ivSel, StringBuilder ovCertInfo, out int ovRetVal);

        [DllImport("sswpke_fs.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int getExtByName(String ivCertExtension, String ivExtName, StringBuilder ovExtInfo, out int ovRetVal);

        [DllImport("sswpke_fs.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int deleteKeyPair(int ivSlotIdx, int ivPrvHandle, int ivPubHandle, out int ovRetVal);

        [DllImport("sswpke_fs.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int getCertData(String ivCert, int ivSel, StringBuilder ovSubject, StringBuilder ovPubKey, StringBuilder ovKeyId, out int ovRetVal);

        [DllImport("sswpke_fs.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int getTokenPubKeys(int ivSlotIdx, StringBuilder ovPubKeys, out int ovRetVal);

        [DllImport("sswpke_fs.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int genCSR(String ivKeyId, String[] ivProfile, String[] ivSubjAltName, int ivKeyLen, int ivX931GenKey, int ivExtractable, int ivRestSignMechCheck, StringBuilder ovCSR, out int ovRetVal);

        [DllImport("sswpke_fs.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int genCertIssueReq(int ivSignFlag, String[] ivAddInfo, String ivCSR, String ivUsage, int ivDuration, int ivSlotIdx, String ivRACert, String iv_ovCertIssueReqTbs, int[] iv_ovCertIssueReqTbsLen, String iv_ovCertIssueReq, int[] iv_ovCertIssueReqLen, out int ovRetVal);

        [DllImport("sswpke_fs.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int genCertReReq(int ivSignFlag, String[] ivParam, int ivType, String ivRevokeReason, int ivDuration, String ivPass, int ivSlotIdx, String ivRACert, String iv_ovCertReReqTbs, int[] iv_ovCertReReqTbsLen, String iv_ovCertReReq, int[] iv_ovCertReReqLen, out int ovRetVal);

        [DllImport("sswpke_fs.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int checkCertUsage(String ivCertificate, String ivUsage, out int ovRetVal);

        [DllImport("sswpke_fs.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int normalize(String ivData, StringBuilder ovNormaizedData, out int ovRetVal);

        [DllImport("sswpke_fs.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int sign(bool ivRestSignMechCheck, int ivSlotIdx, String ivPincode, String ivSignerKeyID, int ivTbsType, int ivTbsHashAlg, String ivTbs, int ivTbsLen, StringBuilder ovTBSDigest, StringBuilder ovSignature, out int ovRetVal);

        [DllImport("sswpke_fs.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int genHashByToken(int ivSlotId, int ivHashAlg, String ivData, int ivDataLen, StringBuilder ovDigest, StringBuilder ovHexDigest, out int ovRetVal);

        [DllImport("sswpke_fs.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int genHash(int ivHashAlg, int ivDataType, String ivData, int ivDataLen, StringBuilder ovDigest, StringBuilder ovHexDigest, out int ovRetVal);

        [DllImport("sswpke_fs.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int login(int ivUserType, int ivSlotIdx, String ivPincode, out int ovRetVal);

        [DllImport("sswpke_fs.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int logout(int ivSlotIdx, out int ovRetVal);

        [DllImport("sswpke_fs.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int setSignMech(int ivSignatureFormat, int ivSignHashAlg, int ivSignMech, int[] ivSignMechParam, out int ovRetVal);

        [DllImport("sswpke_fs.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int genKeyPair(String ivKeyLabel, int ivKeyLen, int ivX931GenKey, int ivExtractable, int ivSensitive, out int ovPrvHKey, out int ovPubHKey, StringBuilder ovKeyId, out int ovRetVal);

        [DllImport("sswpke_fs.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int getSecKeyHandle(StringBuilder ovLabels, ulong[] ovKeysHnadle, out int ovRetVal);

        [DllImport("sswpke_fs.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int deleteObj(int ivObjType, String ivKeyId, int objHandle, out int ovRetVal);

        [DllImport("sswpke_fs.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int import(int ivSlotId, int ivObjLifeSycleType, int ivObjType, String ivObj, String ivP12Pass, StringBuilder ovKeyId, out int ovKeyIdLen, out int ovRetVal);

        [DllImport("sswpke_fs.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int setCryptMech(int ivOpType, int ivCryptType, int ivCryptFormat, int ivCryptMech, int ivCipherAlg, int[] ivCryptMechParam1, String ivCryptMechParam2, out int ovRetVal);

        [DllImport("sswpke_fs.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int doCrypt(int ivOpType, int ivHKey, int ivSlotIdx, String ivPincode, String ivKeyId, String ivRecipsCerts, int ivInputDataType, String ivInputData, int ivInputDataLen, StringBuilder ovOutputData, out int ovOutputDataLen, StringBuilder ovDecDataBase64, out int ovDecDataBase64Len, out int ovRetVal);

        [DllImport("sswpke_fs.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int initializeToken(int ivSlotIdx, String ivNewTokenLabel, String ivSOPIN, out int ovRetVal);

        [DllImport("sswpke_fs.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int initializeUserPIN(int ivSlotIdx, String ivNewUserPIN, String ivSOPIN, out int ovRetVal);

        [DllImport("sswpke_fs.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int getPublicObject(int ivSlotIdx, String ivKeyID, StringBuilder ovCert, StringBuilder ovPubKey, out int ovRetVal);

        [DllImport("sswpke_fs.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int verify(String ivSignerPublicID, int ivTbsType, int ivTbsHashAlg, String ivTbs, int ivTbsLen, String ivSignature, out int ovRetVal);

        [DllImport("sswpke_fs.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int getIssuerCert(String ivChainPath, String ivChildCert, StringBuilder ovParentCert, out int ovRetVal);

        [DllImport("sswpke_fs.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int validateChain(String ivCert, String ivChainPath, out int ovRetVal);

        [DllImport("sswpke_fs.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int validateCert(String ivCert, String ivIssuerCert, String ivCRL, out int ovRetVal);

        [DllImport("sswpke_fs.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int verifyCertIssueReq(String ivChainFile, String ivCertIssueReqFile, String ivCSRFile, out int ovRetVal);

        [DllImport("sswpke_fs.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int issueCert(String ivPIN, String ivOrderFile, String ivTempPath, String ivCertFile, out int ovRetVal);

        [DllImport("sswpke_fs.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int ldapReq(String ivURL, String ivBaseDN, String ivFilter, String ivPath, String ivCertName, String ivCRLName, out int ovRetVal);

        [DllImport("sswpke_fs.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int getPkcs7SignInfo(String ivPkcs7SignedData, StringBuilder ovOriginData, StringBuilder ovDigest, StringBuilder ovDigestAlg, StringBuilder ovSigningTime, StringBuilder ovSignerCerts, out int ovRetVal);

        [DllImport("sswpke_fs.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int getPkcs7EncInfo(String ivPkcs7EncData, StringBuilder ovRecipCertsInfo, StringBuilder ovEncKeys, StringBuilder ovEncKeysAlg, StringBuilder ovCipher, StringBuilder ovEncData, out int ovRetVal);

        [DllImport("sswpke_fs.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int scep_revokeCert(String ivSN, int ivRevockReason, StringBuilder ovCRL, out int ovRetVal);

        [DllImport("sswpke_fs.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int scep_renewCert(String ivSN, StringBuilder ovRenwCert, out int ovRetVal);

        [DllImport("sswpke_fs.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern String scep_getErrorString(int ivErrorNum);

        [DllImport("sswpke_fs.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int scep_init(String ivScepURL, out int ovRetVal);

        [DllImport("sswpke_fs.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int setIssueCertConf(String ivSCEPUrl, String ivSCEPCertPath, String ivSignerCertPath, String ivSignerKeyPath, String ivTrustchainPath, int ivDuration, int ivInterval, out int ovRetVal);

        [DllImport("sswpke_fs.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int genChainFile(String ivCertFileList, String ivCrlFileList, String ivPath, String ivOutputFileName, out int ovRetVal);

        [DllImport("sswpke_fs.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int verifyCert(String ivCert, String ivChainPath, String ivUsage, out int ovRetVal);

        [DllImport("sswpke_fs.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int getCRLInfo(int ivSel, String ivCRL, StringBuilder ovVersion, StringBuilder ovIssuer, StringBuilder ovEDate, StringBuilder ovNextUpdate, StringBuilder ovSerialNumbersInfo, out int ovRetVal);

        [DllImport("sswpke_fs.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int ocspReq(String ivURL, String ivCert, String ivIssuerCert, int ivSlotIdx, String ivPincode, String ivCertSign, String ivKeySignId, String ivOCSPCertUsage, String ivCAFile, int ivSel, int ivNonce, out int ovRetVal);

        [DllImport("sswpke_fs.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int base64Decode(String ivBase64Data, StringBuilder ovData, out int ovRetVal);

        [DllImport("sswpke_fs.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int getCerts(int ivSlotIdx, StringBuilder ovCerts, StringBuilder ovKeyIds, StringBuilder ovPubKeys, StringBuilder ovSubjects, out int ovRetVal);

        [DllImport("sswpke_fs.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int signFullProcess(String ivEngPath, int ivSignatureFormat, int ivSignHashAlg, int ivSignMech, int[] ivSignMechParam, int ivSlotIdx, String ivPincode, String ivSignerKeyID, int ivTbsType, int ivTbsHashAlg, String ivTbs, int ivTbsLen, StringBuilder ovTBSDigest, StringBuilder ovSignature, out int ovRetVal);

        [DllImport("sswpke_fs.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int tspReq(String ivData, String ivServerAddr, String ivHashAlg, int ivNonce, int ivGetTSACert, String ivChainFile, String ivTSASignerUsage, StringBuilder ovDigest, StringBuilder ovSerial, StringBuilder ovTimeStamp, StringBuilder ovAccuracy, StringBuilder ovTSACert, StringBuilder ovStatus, out int ovRetVal);

        [DllImport("sswpke_fs.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int checkSignMechanism(int ivTbsType, int ivTbsHashAlg, String ivTbs, int ivTbsLen, int ivSignatureFormat, int ivSignHashAlg, int ivSignMech, StringBuilder ovTbsHexDigest, StringBuilder ovHashOID, out int ovRetVal);

        [DllImport("sswpke_fs.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int getEventLog(int ivEventNumber, StringBuilder ovEventLog, out int ovRetVal);

        [DllImport("sswpke_fs.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int getTokenSessionInfo(int ivSlotIdx, out int ovSessionInfo, out int ovRetVal);

        [DllImport("sswpke_fs.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int loginCountDown(int ivSlotID, int ivUserType, out int ovLoginAttemptRemaining, out int ovRetVal);

        //[DllImport("sswpke_fs.dll", CallingConvention = CallingConvention.Cdecl)]
        //public static extern int waitForSlotEvent(int ivSlotId, String ivTokenSerialNumber, String ivTokenAuthID, WatcherCallback ivCallback, void* ivParameter, StringBuilder ovTokenAuthID);

        [DllImport("sswpke_fs.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int checkOrder(String ivOrder, StringBuilder ovOrderInfo, out int ovRetVal);

        #endregion sswpke_fs function define

        #region PK_InitPKE
        public bool PK_InitPKE(out String ovStatus, out int ovRetVal)
        {
            /// --- Set Initial Value to Wrapper's Outputs ---
            ovStatus = "";
            ovRetVal = -1;

            /// --- Define & Initialize LibraryMethod's Outputs ---
            int rv1 = 0;

            try
            {
                initPKE(out rv1);
            }
            catch
            {
                ovStatus = "Unable to access sswpke_fs library";
                ovRetVal = 4000;
                return false;
            }

            /// --- Get Event Log ---
            int rv2 = 0;
            StringBuilder EventLog = new StringBuilder(BUF_SIZE1);
            getEventLog(rv1, EventLog, out rv2);

            /// --- Finalize Outputs ---
            ovRetVal = rv1;
            ovStatus = EventLog.ToString();

            return (rv1 == 0) ? true : false;
        }
        #endregion PK_InitPKE

        #region PK_GetCN
        public bool PK_GetCN(String ivCert, out String ovCN, out String ovStatus, out int ovRetVal)
        {
            /// --- Set Initial Value to Wrapper's Outputs ---
            ovCN = "";
            ovStatus = "";
            ovRetVal = -1;

            /// --- Define & Initialize LibraryMethod's Outputs ---
            int rv1 = 0;
            var CommonName = new StringBuilder(BUF_SIZE1);

            try
            {
                getCN(ivCert, CommonName, out rv1);
            }
            catch
            {
                ovStatus = "Unable to access sswpke_fs library";
                ovRetVal = 4000;
                return false;
            }

            /// --- Get Event Log ---
            int rv2 = 0;
            StringBuilder EventLog = new StringBuilder(BUF_SIZE1);
            getEventLog(rv1, EventLog, out rv2);

            /// --- Finalize Outputs ---
            ovCN = CommonName.ToString();
            ovStatus = EventLog.ToString();
            ovRetVal = rv1;

            return (rv1 == 0) ? true : false;
        }
        #endregion PK_GetCN

        #region PK_GetRandom
        public bool PK_GetRandom(int ivRandomStringSize, out String ovRandomString, out String ovStatus, out int ovRetVal)
        {
            /// --- Set Initial Value to Wrapper's Outputs ---
            ovRandomString = "";
            ovStatus = "";
            ovRetVal = -1;

            #region initPKE
            int nRetVal = 0;
            try
            {
                initPKE(out nRetVal);
            }
            catch
            {
                ovStatus = "Unable to access sswpke_fs library";
                ovRetVal = 4000;
                return false;
            }
            if (nRetVal != 0) return false;
            #endregion

            /// --- Define & Initialize LibraryMethod's Outputs ---
            int rv1;
            var buff = new StringBuilder(ivRandomStringSize * 2);

            try
            {
                getRandom(buff, ivRandomStringSize, out rv1);
            }
            catch
            {
                ovStatus = "Unable to access sswpke_fs library";
                ovRetVal = 4000;
                return false;
            }

            /// --- Get Event Log ---
            int rv2 = 0;
            StringBuilder EventLog = new StringBuilder(BUF_SIZE1);
            getEventLog(rv1, EventLog, out rv2);

            /// --- Finalize Outputs ---
            ovRandomString = buff.ToString();
            ovStatus = EventLog.ToString();
            ovRetVal = rv1;

            return (rv1 == 0) ? true : false;
        }
        #endregion PK_GetRandom

        #region PK_GenHash
        public bool PK_GenHash(String ivCert, int ivHashAlg, out String ovCertHash, out String ovStatus, out int ovRetVal)
        {
            /// --- Set Initial Value to Wrapper's Outputs ---
            ovCertHash = "";
            ovStatus = "";
            ovRetVal = -1;

            #region initPKE
            int nRetVal = 0;
            try
            {
                initPKE(out nRetVal);
            }
            catch
            {
                ovStatus = "Unable to access sswpke_fs library";
                ovRetVal = 4000;
                return false;
            }
            if (nRetVal != 0) return false;
            #endregion

            /// --- Define & Initialize LibraryMethod's Outputs ---
            int rv1 = 0;
            int ivDataType = 2; // certificate
            StringBuilder ovDigest = new StringBuilder(19);// 0 ~19 :20 byte for sha1
            StringBuilder ovHexDigest = new StringBuilder(39);// 0~39 :40 byte for sha1 as Hex

            try
            {
                genHash(ivHashAlg, ivDataType, ivCert, ivCert.Length, ovDigest, ovHexDigest, out rv1);
            }
            catch
            {
                ovStatus = "Unable to access sswpke_fs library";
                ovRetVal = 4000;
                return false;
            }

            /// --- Get Event Log ---
            int rv2 = 0;
            StringBuilder EventLog = new StringBuilder(BUF_SIZE1);
            getEventLog(rv1, EventLog, out rv2);

            /// --- Finalize Outputs ---
            ovCertHash = ovHexDigest.ToString();
            ovStatus = EventLog.ToString();
            ovRetVal = rv1;

            return (rv1 == 0) ? true : false;
        }
        #endregion PK_GenHash

        #region PK_GetPkcs7SignInfo
        public bool PK_GetPkcs7SignInfo(String ivSignature, out String ovOriginData, out String ovDigist, out String ovCerts, out String ovSigningTime, out String ovSignerCertificate, out String ovStatus, out int ovRetVal)
        {
            /// --- Set Initial Value to Wrapper's Outputs ---
            ovStatus = "";
            ovOriginData = "";
            ovDigist = "";
            ovCerts = "";
            ovSigningTime = "";
            ovSignerCertificate = "";
            ovRetVal = -1;

            /// --- Define & Initialize LibraryMethod's Outputs ---
            int rv1 = 0;
            var originData = new StringBuilder(MAX_BUF);
            var digist = new StringBuilder(BUF_SIZE1);
            var certs = new StringBuilder(MAX_BUF);
            var signingTime = new StringBuilder(BUF_SIZE1);
            var signerCertificate = new StringBuilder(BUF_SIZE2);

            try
            {
                getPkcs7SignInfo(ivSignature, originData, digist, certs, signingTime, signerCertificate, out rv1);
            }
            catch (Exception)
            {
                ovStatus = "Unable to access sswpke_fs library";
                ovRetVal = 4000;
                return false;
            }

            /// --- Get Event Log ---
            int rv2 = 0;
            StringBuilder EventLog = new StringBuilder(BUF_SIZE1);
            getEventLog(rv1, EventLog, out rv2);

            string s = originData.ToString();
            /// --- Finalize Outputs ---
            var bytes = Encoding.GetEncoding(1256).GetBytes(s);
            ovOriginData = Encoding.UTF8.GetString(bytes);
            ovDigist = digist.ToString();
            ovCerts = certs.ToString();
            ovSigningTime = signingTime.ToString();
            ovSignerCertificate = signerCertificate.ToString();
            ovStatus = EventLog.ToString();
            ovRetVal = rv1;

            return (rv1 == 0) ? true : false;
        }
        #endregion PK_GetPkcs7SignInfo

        #region PK_SetSignMech
        public bool PK_SetSignMech(int ivSignatureFormat, int ivSignHashAlg, int ivSignMech, int ivMGF, int ivSaltLength, out String ovStatus, out int ovRetVal)
        {
            /// --- Set Initial Value to Wrapper's Outputs ---
            ovStatus = "";
            ovRetVal = -1;

            /// --- Define & Initialize LibraryMethod's Outputs ---
            int rv1 = 0;
            int[] signParam = new int[3];

            signParam[0] = ivMGF;
            signParam[1] = ivSaltLength;
            signParam[2] = 0;

            try
            {
                setSignMech(ivSignatureFormat, ivSignHashAlg, ivSignMech, signParam, out rv1);
            }
            catch (Exception)
            {
                ovStatus = "Unable to access sswpke_fs library";
                ovRetVal = 4000;
                return false;
            }

            /// --- Get Event Log ---
            int rv2 = 0;
            StringBuilder EventLog = new StringBuilder(BUF_SIZE1);
            getEventLog(rv1, EventLog, out rv2);

            /// --- Finalize Outputs ---
            ovStatus = EventLog.ToString();
            ovRetVal = rv1;

            return (rv1 == 0) ? true : false;
        }
        #endregion PK_SetSignMech

        #region PK_VerifyCert
        public bool PK_VerifyCert(String ivCertificate, String ivChainPath, String ivCertUsage, out String ovStatus, out int ovRetVal)
        {
            /// --- Set Initial Value to Wrapper's Outputs ---
            ovStatus = "";
            ovRetVal = -1;

            #region initPKE
            int nRetVal = 0;
            try
            {
                initPKE(out nRetVal);
            }
            catch
            {
                ovStatus = "Unable to access sswpke_fs library";
                ovRetVal = 4000;
                return false;
            }
            if (nRetVal != 0) return false;
            #endregion

            /// --- Define & Initialize LibraryMethod's Outputs ---
            int rv1 = 0;
            try
            {
                verifyCert(ivCertificate, ivChainPath, ivCertUsage, out rv1);
            }
            catch (Exception)
            {
                ovStatus = "Unable to access sswpke_fs library";
                ovRetVal = 4000;
                return false;
            }

            /// --- Get Event Log ---
            int rv2 = 0;
            StringBuilder EventLog = new StringBuilder(BUF_SIZE1);
            getEventLog(rv1, EventLog, out rv2);

            /// --- Finalize Outputs ---
            ovStatus = EventLog.ToString();
            ovRetVal = rv1;

            return (rv1 == 0) ? true : false;
        }
        #endregion PK_VerifyCert

        #region PK_Verify
        public bool PK_Verify(String ivSignerCertificate, int ivTbsType, int ivTbsHashAlg, String ivTbs, int ivTbsLen, String ivSignature, out String ovStatus, out int ovRetVal)
        {
            /// --- Set Initial Value to Wrapper's Outputs ---
            ovStatus = "";
            ovRetVal = -1;

            /// --- Define & Initialize LibraryMethod's Outputs ---
            int rv = 0;

            try
            {
                verify(ivSignerCertificate, ivTbsType, ivTbsHashAlg, ivTbs, ivTbsLen, ivSignature, out rv);
            }
            catch (Exception)
            {
                ovStatus = "Unable to access sswpke_fs library";
                ovRetVal = 4000;
                return false;
            }

            /// --- Get Event Log ---
            int rv_0 = 0;
            StringBuilder EventLog = new StringBuilder(BUF_SIZE1);
            getEventLog(rv, EventLog, out rv_0);

            /// --- Finalize Outputs ---
            ovStatus = EventLog.ToString();
            ovRetVal = rv;

            return (rv == 0) ? true : false;
        }
        #endregion PK_Verify

        #region PK_SetCryptoki
        public bool PK_SetCryptoki(String ivCryptokiPath, String ivConf, out String ovStatus, out int ovRetVal)
        {
            /// --- Set Initial Value to Wrapper's Outputs ---
            ovStatus = "";
            ovRetVal = -1;

            /// --- Define & Initialize LibraryMethod's Outputs ---
            int rv1 = 0;

            try
            {
                setCryptoki(ivCryptokiPath, ivConf, out rv1);
            }
            catch
            {
                ovStatus = "Unable to access sswpke_fs library";
                ovRetVal = 4000;
                return false;
            }

            /// --- Get Event Log ---
            int rv2 = 0;
            StringBuilder EventLog = new StringBuilder(BUF_SIZE1);
            getEventLog(rv1, EventLog, out rv2);

            /// --- Finalize Outputs ---
            ovStatus = EventLog.ToString();
            ovRetVal = rv1;

            return (rv1 == 0) ? true : false;
        }
        #endregion PK_SetCryptoki

        #region PK_GetSlotCount
        public bool PK_GetSlotCount(out int ovSlotCount, out String ovStatus, out int ovRetVal)
        {
            /// --- Set Initial Value to Wrapper's Outputs ---
            ovSlotCount = -1;
            ovStatus = "";
            ovRetVal = -1;

            /// --- Define & Initialize LibraryMethod's Outputs ---
            int rv1 = 0;
            int SlotCount = -1;

            try
            {
                getSlotCount(out SlotCount, out rv1);
            }
            catch
            {
                ovStatus = "Unable to access sswpke_fs library";
                ovRetVal = 4000;
                return false;
            }

            /// --- Get Event Log ---
            int rv2 = 0;
            StringBuilder EventLog = new StringBuilder(BUF_SIZE1);
            getEventLog(rv1, EventLog, out rv2);

            /// --- Finalize Outputs ---
            ovSlotCount = SlotCount;
            ovStatus = EventLog.ToString();
            ovRetVal = rv1;

            return (rv1 == 0) ? true : false;
        }
        #endregion PK_GetSlotCount

        #region PK_GetSlotInfo
        public bool PK_GetSlotInfo(int ivSlotIdx, out String ovLabel, out String ovManufacturerID, out String ovModel, out String ovSerialNumber, out String ovStatus, out int ovRetVal)
        {
            /// --- Set Initial Value to Wrapper's Outputs ---
            ovLabel = "";
            ovManufacturerID = "";
            ovModel = "";
            ovSerialNumber = "";
            ovStatus = "";
            ovRetVal = -1;

            /// --- Define & Initialize LibraryMethod's Outputs ---
            int rv1 = 0;
            StringBuilder Label = new StringBuilder(BUF_SIZE1);
            StringBuilder ManufacturerID = new StringBuilder(BUF_SIZE1);
            StringBuilder Model = new StringBuilder(BUF_SIZE1);
            StringBuilder SerialNumber = new StringBuilder(BUF_SIZE1);

            try
            {
                getSlotInfo(ivSlotIdx, Label, ManufacturerID, Model, SerialNumber, out rv1);
            }
            catch
            {
                ovStatus = "Unable to access sswpke_fs library";
                ovRetVal = 4000;
                return false;
            }

            /// --- Get Event Log ---
            int rv2 = 0;
            StringBuilder EventLog = new StringBuilder(BUF_SIZE1);
            getEventLog(rv1, EventLog, out rv2);

            /// --- Finalize Outputs ---
            ovLabel = Label.ToString();
            ovManufacturerID = ManufacturerID.ToString();
            ovModel = Model.ToString();
            ovSerialNumber = SerialNumber.ToString();
            ovStatus = EventLog.ToString();
            ovRetVal = rv1;

            return (rv1 == 0) ? true : false;
        }
        #endregion PK_GetSlotInfo

        #region PK_CheckToken
        public bool PK_CheckToken(int ivSlotIdx, out int ovPresent, out String ovStatus, out int ovRetVal)
        {
            /// --- Set Initial Value to Wrapper's Outputs ---
            ovPresent = -1;
            ovStatus = "";
            ovRetVal = -1;

            /// --- Define & Initialize LibraryMethod's Outputs ---
            int rv1 = 0;
            int Present = -1;

            try
            {
                checkToken(ivSlotIdx, out Present, out rv1);
            }
            catch
            {
                ovStatus = "Unable to access sswpke_fs library";
                ovRetVal = 4000;
                return false;
            }

            /// --- Get Event Log ---
            int rv2 = 0;
            StringBuilder EventLog = new StringBuilder(BUF_SIZE1);
            getEventLog(rv1, EventLog, out rv2);

            /// --- Finalize Outputs ---
            ovPresent = Present;
            ovStatus = EventLog.ToString();
            ovRetVal = rv1;

            return (rv1 == 0) ? true : false;
        }
        #endregion PK_CheckToken

        #region PK_GetPubKeys
        public bool PK_GetPubKeys(int ivSlotIdx, out String ovSubjects, out String ovPubKeys, out String ovStatus, out int ovRetVal)
        {
            /// --- Set Initial Value to Wrapper's Outputs ---
            ovSubjects = "";
            ovPubKeys = "";
            ovStatus = "";
            ovRetVal = -1;

            /// --- Define & Initialize LibraryMethod's Outputs ---
            int rv1 = 0;
            StringBuilder Subjects = new StringBuilder(MAX_BUF);
            StringBuilder PubKeys = new StringBuilder(MAX_BUF);

            try
            {
                getPubKeys(ivSlotIdx, Subjects, PubKeys, out rv1);
            }
            catch
            {
                ovStatus = "Unable to access sswpke_fs library";
                ovRetVal = 4000;
                return false;
            }

            /// --- Get Event Log ---
            int rv2 = 0;
            StringBuilder EventLog = new StringBuilder(BUF_SIZE1);
            getEventLog(rv1, EventLog, out rv2);

            /// --- Finalize Outputs ---
            ovSubjects = Subjects.ToString();
            ovPubKeys = PubKeys.ToString();
            ovStatus = EventLog.ToString();
            ovRetVal = rv1;

            return (rv1 == 0) ? true : false;
        }
        #endregion PK_GetPubKeys

        #region PK_GetCertById
        public bool PK_GetCertById(String ivPubKey, out String ovCert, out String ovStatus, out int ovRetVal)
        {
            /// --- Set Initial Value to Wrapper's Outputs ---
            ovCert = "";
            ovStatus = "";
            ovRetVal = -1;

            /// --- Define & Initialize LibraryMethod's Outputs ---
            int rv1 = 0;
            StringBuilder Cert = new StringBuilder(BUF_SIZE2);
            try
            {
                getCertById(ivPubKey, Cert, out rv1);
            }
            catch
            {
                ovStatus = "Unable to access sswpke_fs library";
                ovRetVal = 4000;
                return false;
            }

            /// --- Get Event Log ---
            int rv2 = 0;
            StringBuilder EventLog = new StringBuilder(BUF_SIZE1);
            getEventLog(rv1, EventLog, out rv2);

            /// --- Finalize Outputs ---
            ovCert = Cert.ToString();
            ovStatus = EventLog.ToString();
            ovRetVal = rv1;

            return (rv1 == 0) ? true : false;
        }
        #endregion PK_GetCertById

        #region PK_ChangePIN
        public bool PK_ChangePIN(String ivOldPIN, String ivNewPIN, out String ovStatus, out int ovRetVal)
        {
            /// --- Set Initial Value to Wrapper's Outputs ---
            ovStatus = "";
            ovRetVal = -1;

            /// --- Define & Initialize LibraryMethod's Outputs ---
            int rv1 = 0;

            try
            {
                changePIN(ivOldPIN, ivNewPIN, out rv1);
            }
            catch
            {
                ovStatus = "Unable to access sswpke_fs library";
                ovRetVal = 4000;
                return false;
            }

            /// --- Get Event Log ---
            int rv2 = 0;
            StringBuilder EventLog = new StringBuilder(BUF_SIZE1);
            getEventLog(rv1, EventLog, out rv2);

            /// --- Finalize Outputs ---
            ovStatus = EventLog.ToString();
            ovRetVal = rv1;

            return (rv1 == 0) ? true : false;
        }
        #endregion PK_ChangePIN

        #region PK_GetKeyPairs
        public bool PK_GetKeyPairs(out String ovKeyIds, out String ovStatus, out int ovRetVal)
        {
            /// --- Set Initial Value to Wrapper's Outputs ---
            ovKeyIds = "";
            ovStatus = "";
            ovRetVal = -1;

            /// --- Define & Initialize LibraryMethod's Outputs ---
            int rv1 = 0;
            StringBuilder KeyIds = new StringBuilder(MAX_BUF);

            try
            {
                getKeyPairs(KeyIds, out rv1);
            }
            catch
            {
                ovStatus = "Unable to access sswpke_fs library";
                ovRetVal = 4000;
                return false;
            }

            /// --- Get Event Log ---
            int rv2 = 0;
            StringBuilder EventLog = new StringBuilder(BUF_SIZE1);
            getEventLog(rv1, EventLog, out rv2);

            /// --- Finalize Outputs ---
            ovKeyIds = KeyIds.ToString();
            ovStatus = EventLog.ToString();
            ovRetVal = rv1;

            return (rv1 == 0) ? true : false;
        }
        #endregion PK_GetKeyPairs

        #region PK_InjectCert
        public bool PK_InjectCert(String ivCert, out String ovCertInfo, out String ovStatus, out int ovRetVal)
        {
            /// --- Set Initial Value to Wrapper's Outputs ---
            ovCertInfo = "";
            ovStatus = "";
            ovRetVal = -1;

            /// --- Define & Initialize LibraryMethod's Outputs ---
            int rv1 = 0;
            StringBuilder CertInfo = new StringBuilder(BUF_SIZE2);

            try
            {
                injectCert(ivCert, CertInfo, out rv1);
            }
            catch
            {
                ovStatus = "Unable to access sswpke_fs library";
                ovRetVal = 4000;
                return false;
            }

            /// --- Get Event Log ---
            int rv2 = 0;
            StringBuilder EventLog = new StringBuilder(BUF_SIZE1);
            getEventLog(rv1, EventLog, out rv2);

            /// --- Finalize Outputs ---
            ovCertInfo = CertInfo.ToString();
            ovStatus = EventLog.ToString();
            ovRetVal = rv1;

            return (rv1 == 0) ? true : false;
        }
        #endregion PK_InjectCert

        #region PK_GetTokenCertsInfo
        public bool PK_GetTokenCertsInfo(int ivSlotIdx, out String ovKeyIds, out String ovPubKeys, out String ovSubjects, out String ovStatus, out int ovRetVal)
        {
            /// --- Set Initial Value to Wrapper's Outputs ---
            ovKeyIds = "";
            ovPubKeys = "";
            ovSubjects = "";
            ovStatus = "";
            ovRetVal = -1;

            /// --- Define & Initialize LibraryMethod's Outputs ---
            int rv1 = 0;
            StringBuilder KeyIds = new StringBuilder(MAX_BUF);
            StringBuilder PubKeys = new StringBuilder(MAX_BUF);
            StringBuilder Subjects = new StringBuilder(MAX_BUF);

            try
            {
                getTokenCertsInfo(ivSlotIdx, KeyIds, PubKeys, Subjects, out rv1);
            }
            catch
            {
                ovStatus = "Unable to access sswpke_fs library";
                ovRetVal = 4000;
                return false;
            }

            /// --- Get Event Log ---
            int rv2 = 0;
            StringBuilder EventLog = new StringBuilder(BUF_SIZE1);
            getEventLog(rv1, EventLog, out rv2);

            /// --- Finalize Outputs ---
            ovKeyIds = KeyIds.ToString();
            ovPubKeys = PubKeys.ToString();
            ovSubjects = Subjects.ToString();
            ovStatus = EventLog.ToString();
            ovRetVal = rv1;

            return (rv1 == 0) ? true : false;
        }
        #endregion PK_GetTokenCertsInfo

        #region PK_GetKeysId
        public bool PK_GetKeysId(int ivObjCheck, out String ovKeyIds, out String ovStatus, out int ovRetVal)
        {
            /// --- Set Initial Value to Wrapper's Outputs ---
            ovKeyIds = "";
            ovStatus = "";
            ovRetVal = -1;

            /// --- Define & Initialize LibraryMethod's Outputs ---
            int rv1 = 0;
            StringBuilder KeyIds = new StringBuilder(MAX_BUF);

            try
            {
                getKeysId(ivObjCheck, KeyIds, out rv1);
            }
            catch
            {
                ovStatus = "Unable to access sswpke_fs library";
                ovRetVal = 4000;
                return false;
            }

            /// --- Get Event Log ---
            int rv2 = 0;
            StringBuilder EventLog = new StringBuilder(BUF_SIZE1);
            getEventLog(rv1, EventLog, out rv2);

            /// --- Finalize Outputs ---
            ovKeyIds = KeyIds.ToString();
            ovStatus = EventLog.ToString();
            ovRetVal = rv1;

            return (rv1 == 0) ? true : false;
        }
        #endregion PK_GetKeysId

        #region PK_InitENG
        public bool PK_InitENG(String ivEngPath, out String ovStatus, out int ovRetVal)
        {
            /// --- Set Initial Value to Wrapper's Outputs ---
            ovStatus = "";
            ovRetVal = -1;

            /// --- Define & Initialize LibraryMethod's Outputs ---
            int rv1 = 0;

            try
            {
                initENG(ivEngPath, out rv1);
            }
            catch
            {
                ovStatus = "Unable to access sswpke_fs library";
                ovRetVal = 4000;
                return false;
            }

            /// --- Get Event Log ---
            int rv2 = 0;
            StringBuilder EventLog = new StringBuilder(BUF_SIZE1);
            getEventLog(rv1, EventLog, out rv2);

            /// --- Finalize Outputs ---
            ovStatus = EventLog.ToString();
            ovRetVal = rv1;

            return (rv1 == 0) ? true : false;
        }
        #endregion PK_InitENG

        #region PK_GetCertExtension
        public bool PK_GetCertExtension(int ivSel, String ivCert, out String ovCertExtInfo, out String ovStatus, out int ovRetVal)
        {
            /// --- Set Initial Value to Wrapper's Outputs ---
            ovCertExtInfo = "";
            ovStatus = "";
            ovRetVal = -1;

            /// --- Define & Initialize LibraryMethod's Outputs ---
            int rv1 = 0;
            StringBuilder CertExtInfo = new StringBuilder(BUF_SIZE2);

            try
            {
                getCertExtension(ivSel, ivCert, CertExtInfo, out rv1);
            }
            catch
            {
                ovStatus = "Unable to access sswpke_fs library";
                ovRetVal = 4000;
                return false;
            }

            /// --- Get Event Log ---
            int rv2 = 0;
            StringBuilder EventLog = new StringBuilder(BUF_SIZE1);
            getEventLog(rv1, EventLog, out rv2);

            /// --- Finalize Outputs ---
            ovCertExtInfo = CertExtInfo.ToString();
            ovStatus = EventLog.ToString();
            ovRetVal = rv1;

            return (rv1 == 0) ? true : false;
        }
        #endregion PK_GetCertExtension

        #region PK_GenRandom
        public bool PK_GenRandom(int ivSlotIdx, int ivSize, out String ovBuf, out String ovStatus, out int ovRetVal)
        {
            /// --- Set Initial Value to Wrapper's Outputs ---
            ovBuf = "";
            ovStatus = "";
            ovRetVal = -1;

            /// --- Define & Initialize LibraryMethod's Outputs ---
            int rv1 = 0;
            StringBuilder Buf = new StringBuilder(BUF_SIZE2);

            try
            {
                genRandom(ivSlotIdx, ivSize, Buf, out rv1);
            }
            catch
            {
                ovStatus = "Unable to access sswpke_fs library";
                ovRetVal = 4000;
                return false;
            }

            /// --- Get Event Log ---
            int rv2 = 0;
            StringBuilder EventLog = new StringBuilder(BUF_SIZE1);
            getEventLog(rv1, EventLog, out rv2);

            /// --- Finalize Outputs ---
            ovBuf = Buf.ToString();
            ovStatus = EventLog.ToString();
            ovRetVal = rv1;

            return (rv1 == 0) ? true : false;
        }
        #endregion PK_GenRandom

        #region PK_GenSymmetricKey
        public bool PK_GenSymmetricKey(int ivKeyGenMech, String ivKeyLabel, int ivKeyLen, int ivExtractable, int ivSensitive, out ulong ovHKey, out String ovStatus, out int ovRetVal)
        {
            /// --- Set Initial Value to Wrapper's Outputs ---
            ovHKey = 0;
            ovStatus = "";
            ovRetVal = -1;

            /// --- Define & Initialize LibraryMethod's Outputs ---
            int rv1 = 0;
            ulong HKey = 1;

            try
            {
                genSymmetricKey(ivKeyGenMech, ivKeyLabel, ivKeyLen, ivExtractable, ivSensitive, out HKey, out rv1);
            }
            catch
            {
                ovStatus = "Unable to access sswpke_fs library";
                ovRetVal = 4000;
                return false;
            }

            /// --- Get Event Log ---
            int rv2 = 0;
            StringBuilder EventLog = new StringBuilder(BUF_SIZE1);
            getEventLog(rv1, EventLog, out rv2);

            /// --- Finalize Outputs ---
            ovHKey = HKey;
            ovStatus = EventLog.ToString();
            ovRetVal = rv1;

            return (rv1 == 0) ? true : false;
        }
        #endregion PK_GenSymmetricKey

        #region PK_GetInfoCertificate
        public bool PK_GetInfoCertificate(String ivCert, int ivSel, out String ovCertInfo, out String ovStatus, out int ovRetVal)
        {
            /// --- Set Initial Value to Wrapper's Outputs ---
            ovCertInfo = "";
            ovStatus = "";
            ovRetVal = -1;

            /// --- Define & Initialize LibraryMethod's Outputs ---
            int rv1 = 0;
            StringBuilder CertInfo = new StringBuilder(BUF_SIZE2);

            try
            {
                getInfoCertificate(ivCert, ivSel, CertInfo, out rv1);
            }
            catch
            {
                ovStatus = "Unable to access sswpke_fs library";
                ovRetVal = 4000;
                return false;
            }

            /// --- Get Event Log ---
            int rv2 = 0;
            StringBuilder EventLog = new StringBuilder(BUF_SIZE1);
            getEventLog(rv1, EventLog, out rv2);

            /// --- Finalize Outputs ---
            ovCertInfo = CertInfo.ToString();
            ovStatus = EventLog.ToString();
            ovRetVal = rv1;

            return (rv1 == 0) ? true : false;
        }
        #endregion PK_GetInfoCertificate

        #region PK_GetExtByName
        public bool PK_GetExtByName(String ivCertExtension, String ivExtName, out String ovExtInfo, out String ovStatus, out int ovRetVal)
        {
            /// --- Set Initial Value to Wrapper's Outputs ---
            ovExtInfo = "";
            ovStatus = "";
            ovRetVal = -1;

            /// --- Define & Initialize LibraryMethod's Outputs ---
            int rv1 = 0;
            StringBuilder ExtInfo = new StringBuilder(BUF_SIZE2);

            try
            {
                getExtByName(ivCertExtension, ivExtName, ExtInfo, out rv1);
            }
            catch
            {
                ovStatus = "Unable to access sswpke_fs library";
                ovRetVal = 4000;
                return false;
            }

            /// --- Get Event Log ---
            int rv2 = 0;
            StringBuilder EventLog = new StringBuilder(BUF_SIZE1);
            getEventLog(rv1, EventLog, out rv2);

            /// --- Finalize Outputs ---
            ovExtInfo = ExtInfo.ToString();
            ovStatus = EventLog.ToString();
            ovRetVal = rv1;

            return (rv1 == 0) ? true : false;
        }
        #endregion PK_GetExtByName

        #region PK_DeleteKeyPair
        public bool PK_DeleteKeyPair(int ivSlotIdx, int ivPrvHandle, int ivPubHandle, out String ovStatus, out int ovRetVal)
        {
            /// --- Set Initial Value to Wrapper's Outputs ---
            ovStatus = "";
            ovRetVal = -1;

            /// --- Define & Initialize LibraryMethod's Outputs ---
            int rv1 = 0;

            try
            {
                deleteKeyPair(ivSlotIdx, ivPrvHandle, ivPubHandle, out rv1);
            }
            catch
            {
                ovStatus = "Unable to access sswpke_fs library";
                ovRetVal = 4000;
                return false;
            }

            /// --- Get Event Log ---
            int rv2 = 0;
            StringBuilder EventLog = new StringBuilder(BUF_SIZE1);
            getEventLog(rv1, EventLog, out rv2);

            /// --- Finalize Outputs ---
            ovStatus = EventLog.ToString();
            ovRetVal = rv1;

            return (rv1 == 0) ? true : false;
        }
        #endregion PK_DeleteKeyPair

        #region PK_GetCertData
        public bool PK_GetCertData(String ivCert, int ivSel, out String ovSubject, out String ovPubKey, out String ovKeyId, out String ovStatus, out int ovRetVal)
        {
            /// --- Set Initial Value to Wrapper's Outputs ---
            ovSubject = "";
            ovPubKey = "";
            ovKeyId = "";
            ovStatus = "";
            ovRetVal = -1;

            /// --- Define & Initialize LibraryMethod's Outputs ---
            int rv1 = 0;
            StringBuilder Subject = new StringBuilder(BUF_SIZE2);
            StringBuilder PubKey = new StringBuilder(BUF_SIZE2);
            StringBuilder KeyId = new StringBuilder(BUF_SIZE2);

            try
            {
                getCertData(ivCert, ivSel, Subject, PubKey, KeyId, out rv1);
            }
            catch
            {
                ovStatus = "Unable to access sswpke_fs library";
                ovRetVal = 4000;
                return false;
            }

            /// --- Get Event Log ---
            int rv2 = 0;
            StringBuilder EventLog = new StringBuilder(BUF_SIZE1);
            getEventLog(rv1, EventLog, out rv2);

            /// --- Finalize Outputs ---
            ovSubject = Subject.ToString();
            ovPubKey = PubKey.ToString();
            ovKeyId = KeyId.ToString();
            ovStatus = EventLog.ToString();
            ovRetVal = rv1;

            return (rv1 == 0) ? true : false;
        }
        #endregion PK_GetCertData

        #region PK_GetTokenPubKeys
        public bool PK_GetTokenPubKeys(int ivSlotIdx, out String ovPubKeys, out String ovStatus, out int ovRetVal)
        {
            /// --- Set Initial Value to Wrapper's Outputs ---
            ovPubKeys = "";
            ovStatus = "";
            ovRetVal = -1;

            /// --- Define & Initialize LibraryMethod's Outputs ---
            int rv1 = 0;
            StringBuilder PubKeys = new StringBuilder(MAX_BUF);

            try
            {
                getTokenPubKeys(ivSlotIdx, PubKeys, out rv1);
            }
            catch
            {
                ovStatus = "Unable to access sswpke_fs library";
                ovRetVal = 4000;
                return false;
            }

            /// --- Get Event Log ---
            int rv2 = 0;
            StringBuilder EventLog = new StringBuilder(BUF_SIZE1);
            getEventLog(rv1, EventLog, out rv2);

            /// --- Finalize Outputs ---
            ovPubKeys = PubKeys.ToString();
            ovStatus = EventLog.ToString();
            ovRetVal = rv1;

            return (rv1 == 0) ? true : false;
        }
        #endregion PK_GetTokenPubKeys

        #region PK_CheckCertUsage
        public bool PK_CheckCertUsage(String ivCertificate, String ivUsage, out String ovStatus, out int ovRetVal)
        {
            /// --- Set Initial Value to Wrapper's Outputs ---
            ovStatus = "";
            ovRetVal = -1;

            /// --- Define & Initialize LibraryMethod's Outputs ---
            int rv1 = 0;

            try
            {
                checkCertUsage(ivCertificate, ivUsage, out rv1);
            }
            catch
            {
                ovStatus = "Unable to access sswpke_fs library";
                ovRetVal = 4000;
                return false;
            }

            /// --- Get Event Log ---
            int rv2 = 0;
            StringBuilder EventLog = new StringBuilder(BUF_SIZE1);
            getEventLog(rv1, EventLog, out rv2);

            /// --- Finalize Outputs ---
            ovStatus = EventLog.ToString();
            ovRetVal = rv1;

            return (rv1 == 0) ? true : false;
        }
        #endregion PK_CheckCertUsage

        #region PK_Normalize
        public bool PK_Normalize(String ivData, out String ovNormaizedData, out String ovStatus, out int ovRetVal)
        {
            /// --- Set Initial Value to Wrapper's Outputs ---
            ovNormaizedData = "";
            ovStatus = "";
            ovRetVal = -1;

            /// --- Define & Initialize LibraryMethod's Outputs ---
            int rv1 = 0;
            StringBuilder NormaizedData = new StringBuilder(MAX_BUF);

            try
            {
                normalize(ivData, NormaizedData, out rv1);
            }
            catch
            {
                ovStatus = "Unable to access sswpke_fs library";
                ovRetVal = 4000;
                return false;
            }

            /// --- Get Event Log ---
            int rv2 = 0;
            StringBuilder EventLog = new StringBuilder(BUF_SIZE1);
            getEventLog(rv1, EventLog, out rv2);

            /// --- Finalize Outputs ---
            ovNormaizedData = NormaizedData.ToString();
            ovStatus = EventLog.ToString();
            ovRetVal = rv1;

            return (rv1 == 0) ? true : false;
        }
        #endregion PK_Normalize

        #region PK_Sign
        public bool PK_Sign(bool ivRestSignMechCheck, int ivSlotIdx, String ivPincode, String ivSignerKeyID, int ivTbsType, int ivTbsHashAlg, String ivTbs, int ivTbsLen, out String ovTBSDigest, out String ovSignature, out String ovStatus, out int ovRetVal)
        {
            /// --- Set Initial Value to Wrapper's Outputs ---
            ovTBSDigest = "";
            ovSignature = "";
            ovStatus = "";
            ovRetVal = -1;

            /// --- Define & Initialize LibraryMethod's Outputs ---
            int rv1 = 0;
            StringBuilder TBSDigest = new StringBuilder(BUF_SIZE2);
            StringBuilder Signature = new StringBuilder(BUF_SIZE2);

            try
            {
                sign(ivRestSignMechCheck, ivSlotIdx, ivPincode, ivSignerKeyID, ivTbsType, ivTbsHashAlg, ivTbs, ivTbsLen, TBSDigest, Signature, out rv1);
            }
            catch
            {
                ovStatus = "Unable to access sswpke_fs library";
                ovRetVal = 4000;
                return false;
            }

            /// --- Get Event Log ---
            int rv2 = 0;
            StringBuilder EventLog = new StringBuilder(BUF_SIZE1);
            getEventLog(rv1, EventLog, out rv2);

            /// --- Finalize Outputs ---
            ovTBSDigest = TBSDigest.ToString();
            ovSignature = Signature.ToString();
            ovStatus = EventLog.ToString();
            ovRetVal = rv1;

            return (rv1 == 0) ? true : false;
        }
        #endregion PK_Sign

        #region PK_GenHashByToken
        public bool PK_GenHashByToken(int ivSlotId, int ivHashAlg, String ivData, int ivDataLen, out String ovDigest, out String ovHexDigest, out String ovStatus, out int ovRetVal)
        {
            /// --- Set Initial Value to Wrapper's Outputs ---
            ovDigest = "";
            ovHexDigest = "";
            ovStatus = "";
            ovRetVal = -1;

            /// --- Define & Initialize LibraryMethod's Outputs ---
            int rv1 = 0;
            StringBuilder Digest = new StringBuilder(BUF_SIZE2);
            StringBuilder HexDigest = new StringBuilder(BUF_SIZE2);

            try
            {
                genHashByToken(ivSlotId, ivHashAlg, ivData, ivDataLen, Digest, HexDigest, out rv1);
            }
            catch
            {
                ovStatus = "Unable to access sswpke_fs library";
                ovRetVal = 4000;
                return false;
            }

            /// --- Get Event Log ---
            int rv2 = 0;
            StringBuilder EventLog = new StringBuilder(BUF_SIZE1);
            getEventLog(rv1, EventLog, out rv2);

            /// --- Finalize Outputs ---
            ovDigest = Digest.ToString();
            ovHexDigest = HexDigest.ToString();
            ovStatus = EventLog.ToString();
            ovRetVal = rv1;

            return (rv1 == 0) ? true : false;
        }
        #endregion PK_GenHashByToken

        #region PK_Login
        public bool PK_Login(int ivUserType, int ivSlotIdx, String ivPincode, out String ovStatus, out int ovRetVal)
        {
            /// --- Set Initial Value to Wrapper's Outputs ---
            ovStatus = "";
            ovRetVal = -1;

            /// --- Define & Initialize LibraryMethod's Outputs ---
            int rv1 = 0;

            try
            {
                login(ivUserType, ivSlotIdx, ivPincode, out rv1);
            }
            catch
            {
                ovStatus = "Unable to access sswpke_fs library";
                ovRetVal = 4000;
                return false;
            }

            /// --- Get Event Log ---
            int rv2 = 0;
            StringBuilder EventLog = new StringBuilder(BUF_SIZE1);
            getEventLog(rv1, EventLog, out rv2);

            /// --- Finalize Outputs ---
            ovStatus = EventLog.ToString();
            ovRetVal = rv1;

            return (rv1 == 0) ? true : false;
        }
        #endregion PK_Login

        #region PK_Logout
        public bool PK_Logout(int ivSlotIdx, out String ovStatus, out int ovRetVal)
        {
            /// --- Set Initial Value to Wrapper's Outputs ---
            ovStatus = "";
            ovRetVal = -1;

            /// --- Define & Initialize LibraryMethod's Outputs ---
            int rv1 = 0;

            try
            {
                logout(ivSlotIdx, out rv1);
            }
            catch
            {
                ovStatus = "Unable to access sswpke_fs library";
                ovRetVal = 4000;
                return false;
            }

            /// --- Get Event Log ---
            int rv2 = 0;
            StringBuilder EventLog = new StringBuilder(BUF_SIZE1);
            getEventLog(rv1, EventLog, out rv2);

            /// --- Finalize Outputs ---
            ovStatus = EventLog.ToString();
            ovRetVal = rv1;

            return (rv1 == 0) ? true : false;
        }
        #endregion PK_Logout

        #region PK_SetSignMech
        public bool PK_SetSignMech(int ivSignatureFormat, int ivSignHashAlg, int ivSignMech, int[] ivSignMechParam, out String ovStatus, out int ovRetVal)
        {
            /// --- Set Initial Value to Wrapper's Outputs ---
            ovStatus = "";
            ovRetVal = -1;

            /// --- Define & Initialize LibraryMethod's Outputs ---
            int rv1 = 0;

            try
            {
                setSignMech(ivSignatureFormat, ivSignHashAlg, ivSignMech, ivSignMechParam, out rv1);
            }
            catch
            {
                ovStatus = "Unable to access sswpke_fs library";
                ovRetVal = 4000;
                return false;
            }

            /// --- Get Event Log ---
            int rv2 = 0;
            StringBuilder EventLog = new StringBuilder(BUF_SIZE1);
            getEventLog(rv1, EventLog, out rv2);

            /// --- Finalize Outputs ---
            ovStatus = EventLog.ToString();
            ovRetVal = rv1;

            return (rv1 == 0) ? true : false;
        }
        #endregion PK_SetSignMech

        #region PK_GenKeyPair
        public bool PK_GenKeyPair(String ivKeyLabel, int ivKeyLen, int ivX931GenKey, int ivExtractable, int ivSensitive, out int ovPrvHKey, out int ovPubHKey, out String ovKeyId, out String ovStatus, out int ovRetVal)
        {
            /// --- Set Initial Value to Wrapper's Outputs ---
            ovPrvHKey = -1;
            ovPubHKey = -1;
            ovKeyId = "";
            ovStatus = "";
            ovRetVal = -1;

            /// --- Define & Initialize LibraryMethod's Outputs ---
            int rv1 = 0;
            int PrvHKey = -1;
            int PubHKey = -1;
            StringBuilder KeyId = new StringBuilder(BUF_SIZE1);

            try
            {
                genKeyPair(ivKeyLabel, ivKeyLen, ivX931GenKey, ivExtractable, ivSensitive, out PrvHKey, out PubHKey, KeyId, out rv1);
            }
            catch
            {
                ovStatus = "Unable to access sswpke_fs library";
                ovRetVal = 4000;
                return false;
            }

            /// --- Get Event Log ---
            int rv2 = 0;
            StringBuilder EventLog = new StringBuilder(BUF_SIZE1);
            getEventLog(rv1, EventLog, out rv2);

            /// --- Finalize Outputs ---
            ovPrvHKey = PrvHKey;
            ovPubHKey = PubHKey;
            ovKeyId = KeyId.ToString();
            ovStatus = EventLog.ToString();
            ovRetVal = rv1;

            return (rv1 == 0) ? true : false;
        }
        #endregion PK_GenKeyPair

        #region PK_GetSecKeyHandle
        public bool PK_GetSecKeyHandle(out String ovLabels, out ulong[] ovKeysHandle, out String ovStatus, out int ovRetVal)
        {
            /// --- Set Initial Value to Wrapper's Outputs ---
            ovLabels = "";
            ovKeysHandle = Enumerable.Repeat((ulong)0, MAX_BUF).ToArray();
            ovStatus = "";
            ovRetVal = -1;

            /// --- Define & Initialize LibraryMethod's Outputs ---
            int rv1 = 0;
            StringBuilder Labels = new StringBuilder(BUF_SIZE2);

            try
            {
                getSecKeyHandle(Labels, ovKeysHandle, out rv1);
            }
            catch
            {
                ovStatus = "Unable to access sswpke_fs library";
                ovRetVal = 4000;
                return false;
            }

            /// --- Get Event Log ---
            int rv2 = 0;
            StringBuilder EventLog = new StringBuilder(BUF_SIZE1);
            getEventLog(rv1, EventLog, out rv2);

            /// --- Finalize Outputs ---
            ovLabels = Labels.ToString();
            ovStatus = EventLog.ToString();
            ovRetVal = rv1;

            return (rv1 == 0) ? true : false;
        }
        #endregion PK_GetSecKeyHandle

        #region PK_DeleteObj
        public bool PK_DeleteObj(int ivObjType, String ivKeyId, int ivObjHandle, out String ovStatus, out int ovRetVal)
        {
            /// --- Set Initial Value to Wrapper's Outputs ---
            ovStatus = "";
            ovRetVal = -1;

            /// --- Define & Initialize LibraryMethod's Outputs ---
            int rv1 = 0;

            try
            {
                deleteObj(ivObjType, ivKeyId, ivObjHandle, out rv1);
            }
            catch
            {
                ovStatus = "Unable to access sswpke_fs library";
                ovRetVal = 4000;
                return false;
            }

            /// --- Get Event Log ---
            int rv2 = 0;
            StringBuilder EventLog = new StringBuilder(BUF_SIZE1);
            getEventLog(rv1, EventLog, out rv2);

            /// --- Finalize Outputs ---
            ovStatus = EventLog.ToString();
            ovRetVal = rv1;

            return (rv1 == 0) ? true : false;
        }
        #endregion PK_DeleteObj

        #region PK_Import
        public bool PK_Import(int ivSlotId, int ivObjLifeSycleType, int ivObjType, String ivObj, String ivP12Pass, out String ovKeyId, out int ovKeyIdLen, out String ovStatus, out int ovRetVal)
        {
            /// --- Set Initial Value to Wrapper's Outputs ---
            ovKeyId = "";
            ovKeyIdLen = -1;
            ovStatus = "";
            ovRetVal = -1;

            /// --- Define & Initialize LibraryMethod's Outputs ---
            int rv1 = 0;
            StringBuilder KeyId = new StringBuilder(BUF_SIZE2);
            int KeyIdLen = -1;

            try
            {
                import(ivSlotId, ivObjLifeSycleType, ivObjType, ivObj, ivP12Pass, KeyId, out KeyIdLen, out rv1);
            }
            catch
            {
                ovStatus = "Unable to access sswpke_fs library";
                ovRetVal = 4000;
                return false;
            }

            /// --- Get Event Log ---
            int rv2 = 0;
            StringBuilder EventLog = new StringBuilder(BUF_SIZE1);
            getEventLog(rv1, EventLog, out rv2);

            /// --- Finalize Outputs ---
            ovKeyId = ovKeyId.ToString();
            ovKeyIdLen = KeyIdLen;
            ovStatus = EventLog.ToString();
            ovRetVal = rv1;

            return (rv1 == 0) ? true : false;
        }
        #endregion PK_Import

        #region PK_SetCryptMech
        public bool PK_SetCryptMech(int ivOpType, int ivCryptType, int ivCryptFormat, int ivCryptMech, int ivCipherAlg, int[] ivCryptMechParam1, String ivCryptMechParam2, out String ovStatus, out int ovRetVal)
        {
            /// --- Set Initial Value to Wrapper's Outputs ---
            ovStatus = "";
            ovRetVal = -1;

            /// --- Define & Initialize LibraryMethod's Outputs ---
            int rv1 = 0;

            try
            {
                setCryptMech(ivOpType, ivCryptType, ivCryptFormat, ivCryptMech, ivCipherAlg, ivCryptMechParam1, ivCryptMechParam2, out rv1);
            }
            catch
            {
                ovStatus = "Unable to access sswpke_fs library";
                ovRetVal = 4000;
                return false;
            }

            /// --- Get Event Log ---
            int rv2 = 0;
            StringBuilder EventLog = new StringBuilder(BUF_SIZE1);
            getEventLog(rv1, EventLog, out rv2);

            /// --- Finalize Outputs ---
            ovStatus = EventLog.ToString();
            ovRetVal = rv1;

            return (rv1 == 0) ? true : false;
        }
        #endregion PK_SetCryptMech

        #region PK_DoCrypt
        public bool PK_DoCrypt(int ivOpType, int ivHKey, int ivSlotIdx, String ivPincode, String ivKeyId, String ivRecipsCerts, int ivInputDataType, String ivInputData, int ivInputDataLen, out String ovOutputData, out int ovOutputDataLen, out String ovDecDataBase64, out int ovDecDataBase64Len, out String ovStatus, out int ovRetVal)
        {
            /// --- Set Initial Value to Wrapper's Outputs ---
            ovOutputData = "";
            ovOutputDataLen = -1;
            ovDecDataBase64 = "";
            ovDecDataBase64Len = -1;
            ovStatus = "";
            ovRetVal = -1;

            /// --- Define & Initialize LibraryMethod's Outputs ---
            int rv1 = 0;
            StringBuilder OutputData = new StringBuilder(MAX_BUF);
            int OutputDataLen = -1;
            StringBuilder DecDataBase64 = new StringBuilder(MAX_BUF);
            int DecDataBase64Len = -1;

            try
            {
                doCrypt(ivOpType, ivHKey, ivSlotIdx, ivPincode, ivKeyId, ivRecipsCerts, ivInputDataType, ivInputData, ivInputDataLen, OutputData, out OutputDataLen, DecDataBase64, out DecDataBase64Len, out rv1);
            }
            catch
            {
                ovStatus = "Unable to access sswpke_fs library";
                ovRetVal = 4000;
                return false;
            }

            /// --- Get Event Log ---
            int rv2 = 0;
            StringBuilder EventLog = new StringBuilder(BUF_SIZE1);
            getEventLog(rv1, EventLog, out rv2);

            /// --- Finalize Outputs ---
            ovOutputData = OutputData.ToString();
            ovOutputDataLen = OutputDataLen;
            ovDecDataBase64 = DecDataBase64.ToString();
            ovDecDataBase64Len = DecDataBase64Len;
            ovStatus = EventLog.ToString();
            ovRetVal = rv1;

            return (rv1 == 0) ? true : false;
        }
        #endregion PK_DoCrypt

        #region PK_InitializeToken
        public bool PK_InitializeToken(int ivSlotIdx, String ivNewTokenLabel, String ivSOPIN, out String ovStatus, out int ovRetVal)
        {
            /// --- Set Initial Value to Wrapper's Outputs ---
            ovStatus = "";
            ovRetVal = -1;

            /// --- Define & Initialize LibraryMethod's Outputs ---
            int rv1 = 0;

            try
            {
                initializeToken(ivSlotIdx, ivNewTokenLabel, ivSOPIN, out rv1);
            }
            catch
            {
                ovStatus = "Unable to access sswpke_fs library";
                ovRetVal = 4000;
                return false;
            }

            /// --- Get Event Log ---
            int rv2 = 0;
            StringBuilder EventLog = new StringBuilder(BUF_SIZE1);
            getEventLog(rv1, EventLog, out rv2);

            /// --- Finalize Outputs ---
            ovStatus = EventLog.ToString();
            ovRetVal = rv1;

            return (rv1 == 0) ? true : false;
        }
        #endregion PK_InitializeToken

        #region PK_InitializeUserPIN
        public bool PK_InitializeUserPIN(int ivSlotIdx, String ivNewUserPIN, String ivSOPIN, out String ovStatus, out int ovRetVal)
        {
            /// --- Set Initial Value to Wrapper's Outputs ---
            ovStatus = "";
            ovRetVal = -1;

            /// --- Define & Initialize LibraryMethod's Outputs ---
            int rv1 = 0;

            try
            {
                initializeUserPIN(ivSlotIdx, ivNewUserPIN, ivSOPIN, out rv1);
            }
            catch
            {
                ovStatus = "Unable to access sswpke_fs library";
                ovRetVal = 4000;
                return false;
            }

            /// --- Get Event Log ---
            int rv2 = 0;
            StringBuilder EventLog = new StringBuilder(BUF_SIZE1);
            getEventLog(rv1, EventLog, out rv2);

            /// --- Finalize Outputs ---
            ovStatus = EventLog.ToString();
            ovRetVal = rv1;

            return (rv1 == 0) ? true : false;
        }
        #endregion PK_InitializeUserPIN

        #region PK_GetPublicObject
        public bool PK_GetPublicObject(int ivSlotIdx, String ivKeyID, out String ovCert, out String ovPubKey, out String ovStatus, out int ovRetVal)
        {
            /// --- Set Initial Value to Wrapper's Outputs ---
            ovCert = "";
            ovPubKey = "";
            ovStatus = "";
            ovRetVal = -1;

            /// --- Define & Initialize LibraryMethod's Outputs ---
            int rv1 = 0;
            StringBuilder Cert = new StringBuilder(BUF_SIZE2);
            StringBuilder PubKey = new StringBuilder(BUF_SIZE2);

            try
            {
                getPublicObject(ivSlotIdx, ivKeyID, Cert, PubKey, out rv1);
            }
            catch
            {
                ovStatus = "Unable to access sswpke_fs library";
                ovRetVal = 4000;
                return false;
            }

            /// --- Get Event Log ---
            int rv2 = 0;
            StringBuilder EventLog = new StringBuilder(BUF_SIZE1);
            getEventLog(rv1, EventLog, out rv2);

            /// --- Finalize Outputs ---
            ovCert = Cert.ToString();
            ovPubKey = PubKey.ToString();
            ovStatus = EventLog.ToString();
            ovRetVal = rv1;

            return (rv1 == 0) ? true : false;
        }
        #endregion PK_GetPublicObject

        #region PK_GetIssuerCert
        public bool PK_GetIssuerCert(String ivChainPath, String ivChildCert, out String ovParentCert, out String ovStatus, out int ovRetVal)
        {
            /// --- Set Initial Value to Wrapper's Outputs ---
            ovParentCert = "";
            ovStatus = "";
            ovRetVal = -1;

            /// --- Define & Initialize LibraryMethod's Outputs ---
            int rv1 = 0;
            StringBuilder ParentCert = new StringBuilder(BUF_SIZE2);

            try
            {
                getIssuerCert(ivChainPath, ivChildCert, ParentCert, out rv1);
            }
            catch
            {
                ovStatus = "Unable to access sswpke_fs library";
                ovRetVal = 4000;
                return false;
            }

            /// --- Get Event Log ---
            int rv2 = 0;
            StringBuilder EventLog = new StringBuilder(BUF_SIZE1);
            getEventLog(rv1, EventLog, out rv2);

            /// --- Finalize Outputs ---
            ovParentCert = ParentCert.ToString();
            ovStatus = EventLog.ToString();
            ovRetVal = rv1;

            return (rv1 == 0) ? true : false;
        }
        #endregion PK_GetIssuerCert

        #region PK_ValidateChain
        public bool PK_ValidateChain(String ivCert, String ivChainPath, out String ovStatus, out int ovRetVal)
        {
            /// --- Set Initial Value to Wrapper's Outputs ---
            ovStatus = "";
            ovRetVal = -1;

            /// --- Define & Initialize LibraryMethod's Outputs ---
            int rv1 = 0;

            try
            {
                validateChain(ivCert, ivChainPath, out rv1);
            }
            catch
            {
                ovStatus = "Unable to access sswpke_fs library";
                ovRetVal = 4000;
                return false;
            }

            /// --- Get Event Log ---
            int rv2 = 0;
            StringBuilder EventLog = new StringBuilder(BUF_SIZE1);
            getEventLog(rv1, EventLog, out rv2);

            /// --- Finalize Outputs ---
            ovStatus = EventLog.ToString();
            ovRetVal = rv1;

            return (rv1 == 0) ? true : false;
        }
        #endregion PK_ValidateChain

        #region PK_ValidateCert
        public bool PK_ValidateCert(String ivCert, String ivIssuerCert, String ivCRL, out String ovStatus, out int ovRetVal)
        {
            /// --- Set Initial Value to Wrapper's Outputs ---
            ovStatus = "";
            ovRetVal = -1;

            /// --- Define & Initialize LibraryMethod's Outputs ---
            int rv1 = 0;

            try
            {
                validateCert(ivCert, ivIssuerCert, ivCRL, out rv1);
            }
            catch
            {
                ovStatus = "Unable to access sswpke_fs library";
                ovRetVal = 4000;
                return false;
            }

            /// --- Get Event Log ---
            int rv2 = 0;
            StringBuilder EventLog = new StringBuilder(BUF_SIZE1);
            getEventLog(rv1, EventLog, out rv2);

            /// --- Finalize Outputs ---
            ovStatus = EventLog.ToString();
            ovRetVal = rv1;

            return (rv1 == 0) ? true : false;
        }
        #endregion PK_ValidateCert

        #region PK_VerifyCertIssueReq
        public bool PK_VerifyCertIssueReq(String ivChainFile, String ivCertIssueReqFile, String ivCSRFile, out String ovStatus, out int ovRetVal)
        {
            /// --- Set Initial Value to Wrapper's Outputs ---
            ovStatus = "";
            ovRetVal = -1;

            /// --- Define & Initialize LibraryMethod's Outputs ---
            int rv1 = 0;

            try
            {
                verifyCertIssueReq(ivChainFile, ivCertIssueReqFile, ivCSRFile, out rv1);
            }
            catch
            {
                ovStatus = "Unable to access sswpke_fs library";
                ovRetVal = 4000;
                return false;
            }

            /// --- Get Event Log ---
            int rv2 = 0;
            StringBuilder EventLog = new StringBuilder(BUF_SIZE1);
            getEventLog(rv1, EventLog, out rv2);

            /// --- Finalize Outputs ---
            ovStatus = EventLog.ToString();
            ovRetVal = rv1;

            return (rv1 == 0) ? true : false;
        }
        #endregion PK_VerifyCertIssueReq

        #region PK_IssueCert
        public bool PK_IssueCert(String ivPIN, String ivOrderFile, String ivTempPath, String ivCertFile, out String ovStatus, out int ovRetVal)
        {
            /// --- Set Initial Value to Wrapper's Outputs ---
            ovStatus = "";
            ovRetVal = -1;

            /// --- Define & Initialize LibraryMethod's Outputs ---
            int rv1 = 0;

            try
            {
                issueCert(ivPIN, ivOrderFile, ivTempPath, ivCertFile, out rv1);
            }
            catch
            {
                ovStatus = "Unable to access sswpke_fs library";
                ovRetVal = 4000;
                return false;
            }

            /// --- Get Event Log ---
            int rv2 = 0;
            StringBuilder EventLog = new StringBuilder(BUF_SIZE1);
            getEventLog(rv1, EventLog, out rv2);

            /// --- Finalize Outputs ---
            ovStatus = EventLog.ToString();
            ovRetVal = rv1;

            return (rv1 == 0) ? true : false;
        }
        #endregion PK_IssueCert

        #region PK_LdapReq
        public bool PK_LdapReq(String ivURL, String ivBaseDN, String ivFilter, String ivPath, String ivCertName, String ivCRLName, out String ovStatus, out int ovRetVal)
        {
            /// --- Set Initial Value to Wrapper's Outputs ---
            ovStatus = "";
            ovRetVal = -1;

            /// --- Define & Initialize LibraryMethod's Outputs ---
            int rv1 = 0;

            try
            {
                ldapReq(ivURL, ivBaseDN, ivFilter, ivPath, ivCertName, ivCRLName, out rv1);
            }
            catch
            {
                ovStatus = "Unable to access sswpke_fs library";
                ovRetVal = 4000;
                return false;
            }

            /// --- Get Event Log ---
            int rv2 = 0;
            StringBuilder EventLog = new StringBuilder(BUF_SIZE1);
            getEventLog(rv1, EventLog, out rv2);

            /// --- Finalize Outputs ---
            ovStatus = EventLog.ToString();
            ovRetVal = rv1;

            return (rv1 == 0) ? true : false;
        }
        #endregion PK_LdapReq

        #region PK_GetPkcs7EncInfo
        public bool PK_GetPkcs7EncInfo(String ivPkcs7EncData, out String ovRecipCertsInfo, out String ovEncKeys, out String ovEncKeysAlg, out String ovCipher, out String ovEncData, out String ovStatus, out int ovRetVal)
        {
            /// --- Set Initial Value to Wrapper's Outputs ---
            ovRecipCertsInfo = "";
            ovEncKeys = "";
            ovEncKeysAlg = "";
            ovCipher = "";
            ovEncData = "";
            ovStatus = "";
            ovRetVal = -1;

            /// --- Define & Initialize LibraryMethod's Outputs ---
            int rv1 = 0;
            StringBuilder RecipCertsInfo = new StringBuilder(BUF_SIZE2);
            StringBuilder EncKeys = new StringBuilder(BUF_SIZE2);
            StringBuilder EncKeysAlg = new StringBuilder(BUF_SIZE2);
            StringBuilder Cipher = new StringBuilder(BUF_SIZE2);
            StringBuilder EncData = new StringBuilder(BUF_SIZE2);

            try
            {
                getPkcs7EncInfo(ivPkcs7EncData, RecipCertsInfo, EncKeys, EncKeysAlg, Cipher, EncData, out rv1);
            }
            catch
            {
                ovStatus = "Unable to access sswpke_fs library";
                ovRetVal = 4000;
                return false;
            }

            /// --- Get Event Log ---
            int rv2 = 0;
            StringBuilder EventLog = new StringBuilder(BUF_SIZE1);
            getEventLog(rv1, EventLog, out rv2);

            /// --- Finalize Outputs ---
            ovRecipCertsInfo = RecipCertsInfo.ToString();
            ovEncKeys = EncKeys.ToString();
            ovEncKeysAlg = EncKeysAlg.ToString();
            ovCipher = Cipher.ToString();
            ovEncData = EncData.ToString();
            ovStatus = EventLog.ToString();
            ovRetVal = rv1;

            return (rv1 == 0) ? true : false;
        }
        #endregion PK_GetPkcs7EncInfo

        #region PK_SetIssueCertConf
        public bool PK_SetIssueCertConf(String ivSCEPUrl, String ivSCEPCertPath, String ivSignerCertPath, String ivSignerKeyPath, String ivTrustchainPath, int ivDuration, int ivInterval, out String ovStatus, out int ovRetVal)
        {
            /// --- Set Initial Value to Wrapper's Outputs ---
            ovStatus = "";
            ovRetVal = -1;

            /// --- Define & Initialize LibraryMethod's Outputs ---
            int rv1 = 0;

            try
            {
                setIssueCertConf(ivSCEPUrl, ivSCEPCertPath, ivSignerCertPath, ivSignerKeyPath, ivTrustchainPath, ivDuration, ivInterval, out rv1);
            }
            catch
            {
                ovStatus = "Unable to access sswpke_fs library";
                ovRetVal = 4000;
                return false;
            }

            /// --- Get Event Log ---
            int rv2 = 0;
            StringBuilder EventLog = new StringBuilder(BUF_SIZE1);
            getEventLog(rv1, EventLog, out rv2);

            /// --- Finalize Outputs ---
            ovStatus = EventLog.ToString();
            ovRetVal = rv1;

            return (rv1 == 0) ? true : false;
        }
        #endregion PK_SetIssueCertConf

        #region PK_GenChainFile
        public bool PK_GenChainFile(String ivCertFileList, String ivCrlFileList, String ivPath, String ivOutputFileName, out String ovStatus, out int ovRetVal)
        {
            /// --- Set Initial Value to Wrapper's Outputs ---
            ovStatus = "";
            ovRetVal = -1;

            /// --- Define & Initialize LibraryMethod's Outputs ---
            int rv1 = 0;

            try
            {
                genChainFile(ivCertFileList, ivCrlFileList, ivPath, ivOutputFileName, out rv1);
            }
            catch
            {
                ovStatus = "Unable to access sswpke_fs library";
                ovRetVal = 4000;
                return false;
            }

            /// --- Get Event Log ---
            int rv2 = 0;
            StringBuilder EventLog = new StringBuilder(BUF_SIZE1);
            getEventLog(rv1, EventLog, out rv2);

            /// --- Finalize Outputs ---
            ovStatus = EventLog.ToString();
            ovRetVal = rv1;

            return (rv1 == 0) ? true : false;
        }
        #endregion PK_GenChainFile

        #region PK_GetCRLInfo
        public bool PK_GetCRLInfo(int ivSel, String ivCRL, out String ovVersion, out String ovIssuer, out String ovDate, out String ovNextUpdate, out String ovSerialNumbersInfo, out String ovStatus, out int ovRetVal)
        {
            /// --- Set Initial Value to Wrapper's Outputs ---
            ovVersion = "";
            ovIssuer = "";
            ovDate = "";
            ovNextUpdate = "";
            ovSerialNumbersInfo = "";
            ovStatus = "";
            ovRetVal = -1;

            /// --- Define & Initialize LibraryMethod's Outputs ---
            int rv1 = 0;
            StringBuilder Version = new StringBuilder(BUF_SIZE1);
            StringBuilder Issuer = new StringBuilder(BUF_SIZE2);
            StringBuilder Date = new StringBuilder(BUF_SIZE1);
            StringBuilder NextUpdate = new StringBuilder(BUF_SIZE1);
            StringBuilder SerialNumbersInfo = new StringBuilder(BUF_SIZE1);

            try
            {
                getCRLInfo(ivSel, ivCRL, Version, Issuer, Date, NextUpdate, SerialNumbersInfo, out rv1);
            }
            catch
            {
                ovStatus = "Unable to access sswpke_fs library";
                ovRetVal = 4000;
                return false;
            }

            /// --- Get Event Log ---
            int rv2 = 0;
            StringBuilder EventLog = new StringBuilder(BUF_SIZE1);
            getEventLog(rv1, EventLog, out rv2);

            /// --- Finalize Outputs ---
            ovVersion = Version.ToString();
            ovIssuer = Issuer.ToString();
            ovDate = Date.ToString();
            ovNextUpdate = NextUpdate.ToString();
            ovSerialNumbersInfo = SerialNumbersInfo.ToString();
            ovStatus = EventLog.ToString();
            ovRetVal = rv1;

            return (rv1 == 0) ? true : false;
        }
        #endregion PK_GetCRLInfo

        #region PK_OcspReq
        public bool PK_OcspReq(String ivURL, String ivCert, String ivIssuerCert, int ivSlotIdx, String ivPincode, String ivCertSign, String ivKeySignId, String ivOCSPCertUsage, String ivCAFile, int ivSel, int ivNonce, out String ovStatus, out int ovRetVal)
        {
            /// --- Set Initial Value to Wrapper's Outputs ---
            ovStatus = "";
            ovRetVal = -1;

            /// --- Define & Initialize LibraryMethod's Outputs ---
            int rv1 = 0;

            try
            {
                ocspReq(ivURL, ivCert, ivIssuerCert, ivSlotIdx, ivPincode, ivCertSign, ivKeySignId, ivOCSPCertUsage, ivCAFile, ivSel, ivNonce, out rv1);
            }
            catch
            {
                ovStatus = "Unable to access sswpke_fs library";
                ovRetVal = 4000;
                return false;
            }

            /// --- Get Event Log ---
            int rv2 = 0;
            StringBuilder EventLog = new StringBuilder(BUF_SIZE1);
            getEventLog(rv1, EventLog, out rv2);

            /// --- Finalize Outputs ---
            ovStatus = EventLog.ToString();
            ovRetVal = rv1;

            return (rv1 == 0) ? true : false;
        }
        #endregion PK_OcspReq

        #region PK_Base64Decode
        public bool PK_Base64Decode(String ivBase64Data, out String ovData, out String ovStatus, out int ovRetVal)
        {
            /// --- Set Initial Value to Wrapper's Outputs ---
            ovData = "";
            ovStatus = "";
            ovRetVal = -1;

            /// --- Define & Initialize LibraryMethod's Outputs ---
            int rv1 = 0;
            StringBuilder Data = new StringBuilder(MAX_BUF);

            try
            {
                base64Decode(ivBase64Data, Data, out rv1);
            }
            catch
            {
                ovStatus = "Unable to access sswpke_fs library";
                ovRetVal = 4000;
                return false;
            }

            /// --- Get Event Log ---
            int rv2 = 0;
            StringBuilder EventLog = new StringBuilder(BUF_SIZE1);
            getEventLog(rv1, EventLog, out rv2);

            /// --- Finalize Outputs ---
            ovData = Data.ToString();
            ovStatus = EventLog.ToString();
            ovRetVal = rv1;

            return (rv1 == 0) ? true : false;
        }
        #endregion PK_Base64Decode

        #region PK_GetCerts
        public bool PK_GetCerts(int ivSlotIdx, out String ovCerts, out String ovKeyIds, out String ovPubKeys, out String ovSubjects, out String ovStatus, out int ovRetVal)
        {
            /// --- Set Initial Value to Wrapper's Outputs ---
            ovCerts = "";
            ovKeyIds = "";
            ovPubKeys = "";
            ovSubjects = "";
            ovStatus = "";
            ovRetVal = -1;

            /// --- Define & Initialize LibraryMethod's Outputs ---
            int rv1 = 0;
            StringBuilder Certs = new StringBuilder(MAX_BUF);
            StringBuilder KeyIds = new StringBuilder(MAX_BUF);
            StringBuilder PubKeys = new StringBuilder(MAX_BUF);
            StringBuilder Subjects = new StringBuilder(MAX_BUF);

            try
            {
                getCerts(ivSlotIdx, Certs, KeyIds, PubKeys, Subjects, out rv1);
            }
            catch
            {
                ovStatus = "Unable to access sswpke_fs library";
                ovRetVal = 4000;
                return false;
            }

            /// --- Get Event Log ---
            int rv2 = 0;
            StringBuilder EventLog = new StringBuilder(BUF_SIZE1);
            getEventLog(rv1, EventLog, out rv2);

            /// --- Finalize Outputs ---
            ovCerts = Certs.ToString();
            ovKeyIds = KeyIds.ToString();
            ovPubKeys = PubKeys.ToString();
            ovSubjects = Subjects.ToString();
            ovStatus = EventLog.ToString();
            ovRetVal = rv1;

            return (rv1 == 0) ? true : false;
        }
        #endregion PK_GetCerts

        #region PK_SignFullProcess
        public bool PK_SignFullProcess(String ivEngPath, int ivSignatureFormat, int ivSignHashAlg, int ivSignMech, int[] ivSignMechParam, int ivSlotIdx, String ivPincode, String ivSignerKeyID, int ivTbsType, int ivTbsHashAlg, String ivTbs, int ivTbsLen, out String ovTBSDigest, out String ovSignature, out String ovStatus, out int ovRetVal)
        {
            /// --- Set Initial Value to Wrapper's Outputs ---
            ovTBSDigest = "";
            ovSignature = "";
            ovStatus = "";
            ovRetVal = -1;

            /// --- Define & Initialize LibraryMethod's Outputs ---
            int rv1 = 0;
            StringBuilder TBSDigest = new StringBuilder(BUF_SIZE2);
            StringBuilder Signature = new StringBuilder(BUF_SIZE2);

            try
            {
                signFullProcess(ivEngPath, ivSignatureFormat, ivSignHashAlg, ivSignMech, ivSignMechParam, ivSlotIdx, ivPincode, ivSignerKeyID, ivTbsType, ivTbsHashAlg, ivTbs, ivTbsLen, TBSDigest, Signature, out rv1);
            }
            catch
            {
                ovStatus = "Unable to access sswpke_fs library";
                ovRetVal = 4000;
                return false;
            }

            /// --- Get Event Log ---
            int rv2 = 0;
            StringBuilder EventLog = new StringBuilder(BUF_SIZE1);
            getEventLog(rv1, EventLog, out rv2);

            /// --- Finalize Outputs ---
            ovStatus = EventLog.ToString();
            ovRetVal = rv1;

            return (rv1 == 0) ? true : false;
        }
        #endregion PK_SignFullProcess

        #region PK_TspReq
        public bool PK_TspReq(String ivData, String ivServerAddr, String ivHashAlg, int ivNonce, int ivGetTSACert, String ivChainFile, String ivTSASignerUsage, out String ovDigest, out String ovSerial, out String ovTimeStamp, out String ovAccuracy, out String ovTSACert, out String ovTSA_Status, out String ovStatus, out int ovRetVal)
        {
            /// --- Set Initial Value to Wrapper's Outputs ---
            ovDigest = "";
            ovSerial = "";
            ovTimeStamp = "";
            ovAccuracy = "";
            ovTSACert = "";
            ovTSA_Status = "";
            ovStatus = "";
            ovRetVal = -1;

            /// --- Define & Initialize LibraryMethod's Outputs ---
            int rv1 = 0;
            StringBuilder Digest = new StringBuilder(BUF_SIZE2);
            StringBuilder Serial = new StringBuilder(BUF_SIZE1);
            StringBuilder TimeStamp = new StringBuilder(BUF_SIZE1);
            StringBuilder Accuracy = new StringBuilder(BUF_SIZE1);
            StringBuilder TSACert = new StringBuilder(BUF_SIZE2);
            StringBuilder TSA_Status = new StringBuilder(BUF_SIZE1);

            try
            {
                tspReq(ivData, ivServerAddr, ivHashAlg, ivNonce, ivGetTSACert, ivChainFile, ivTSASignerUsage, Digest, Serial, TimeStamp, Accuracy, TSACert, TSA_Status, out rv1);
            }
            catch
            {
                ovStatus = "Unable to access sswpke_fs library";
                ovRetVal = 4000;
                return false;
            }

            /// --- Get Event Log ---
            int rv2 = 0;
            StringBuilder EventLog = new StringBuilder(BUF_SIZE1);
            getEventLog(rv1, EventLog, out rv2);

            /// --- Finalize Outputs ---
            ovDigest = Digest.ToString();
            ovSerial = Serial.ToString();
            ovTimeStamp = TimeStamp.ToString();
            ovAccuracy = Accuracy.ToString();
            ovTSACert = TSACert.ToString();
            ovTSA_Status = TSA_Status.ToString();
            ovStatus = EventLog.ToString();
            ovRetVal = rv1;

            return (rv1 == 0) ? true : false;
        }
        #endregion PK_TspReq

        #region PK_CheckSignMechanism
        public bool PK_CheckSignMechanism(int ivTbsType, int ivTbsHashAlg, String ivTbs, int ivTbsLen, int ivSignatureFormat, int ivSignHashAlg, int ivSignMech, out String ovTbsHexDigest, out String ovHashOID, out String ovStatus, out int ovRetVal)
        {
            /// --- Set Initial Value to Wrapper's Outputs ---
            ovTbsHexDigest = "";
            ovHashOID = "";
            ovStatus = "";
            ovRetVal = -1;

            /// --- Define & Initialize LibraryMethod's Outputs ---
            int rv1 = 0;
            StringBuilder TbsHexDigest = new StringBuilder(BUF_SIZE2);
            StringBuilder HashOID = new StringBuilder(BUF_SIZE1);

            try
            {
                checkSignMechanism(ivTbsType, ivTbsHashAlg, ivTbs, ivTbsLen, ivSignatureFormat, ivSignHashAlg, ivSignMech, TbsHexDigest, HashOID, out rv1);
            }
            catch
            {
                ovStatus = "Unable to access sswpke_fs library";
                ovRetVal = 4000;
                return false;
            }

            /// --- Get Event Log ---
            int rv2 = 0;
            StringBuilder EventLog = new StringBuilder(BUF_SIZE1);
            getEventLog(rv1, EventLog, out rv2);

            /// --- Finalize Outputs ---
            ovTbsHexDigest = TbsHexDigest.ToString();
            ovHashOID = HashOID.ToString();
            ovStatus = EventLog.ToString();
            ovRetVal = rv1;

            return (rv1 == 0) ? true : false;
        }
        #endregion PK_CheckSignMechanism

        #region PK_GetTokenSessionInfo
        public bool PK_GetTokenSessionInfo(int ivSlotIdx, out int ovSessionInfo, out String ovStatus, out int ovRetVal)
        {
            /// --- Set Initial Value to Wrapper's Outputs ---
            ovSessionInfo = -1;
            ovStatus = "";
            ovRetVal = -1;

            /// --- Define & Initialize LibraryMethod's Outputs ---
            int rv1 = 0;
            int SessionInfo = -1;

            try
            {
                getTokenSessionInfo(ivSlotIdx, out SessionInfo, out rv1);
            }
            catch
            {
                ovStatus = "Unable to access sswpke_fs library";
                ovRetVal = 4000;
                return false;
            }

            /// --- Get Event Log ---
            int rv2 = 0;
            StringBuilder EventLog = new StringBuilder(BUF_SIZE1);
            getEventLog(rv1, EventLog, out rv2);

            /// --- Finalize Outputs ---
            ovSessionInfo = SessionInfo;
            ovStatus = EventLog.ToString();
            ovRetVal = rv1;

            return (rv1 == 0) ? true : false;
        }
        #endregion PK_GetTokenSessionInfo

        #region PK_LoginCountDown
        public bool PK_LoginCountDown(int ivSlotID, int ivUserType, out int ovLoginAttemptRemaining, out String ovStatus, out int ovRetVal)
        {
            /// --- Set Initial Value to Wrapper's Outputs ---
            ovLoginAttemptRemaining = -1;
            ovStatus = "";
            ovRetVal = -1;

            /// --- Define & Initialize LibraryMethod's Outputs ---
            int rv1 = 0;
            int LoginAttemptRemaining = -1;

            try
            {
                loginCountDown(ivSlotID, ivUserType, out LoginAttemptRemaining, out rv1);
            }
            catch
            {
                ovStatus = "Unable to access sswpke_fs library";
                ovRetVal = 4000;
                return false;
            }

            /// --- Get Event Log ---
            int rv2 = 0;
            StringBuilder EventLog = new StringBuilder(BUF_SIZE1);
            getEventLog(rv1, EventLog, out rv2);

            /// --- Finalize Outputs ---
            ovLoginAttemptRemaining = LoginAttemptRemaining;
            ovStatus = EventLog.ToString();
            ovRetVal = rv1;

            return (rv1 == 0) ? true : false;
        }
        #endregion PK_LoginCountDown

        #region PK_CheckOrder
        public bool PK_CheckOrder(String ivOrder, out String ovOrderInfo, out String ovStatus, out int ovRetVal)
        {
            /// --- Set Initial Value to Wrapper's Outputs ---
            ovOrderInfo = "";
            ovStatus = "";
            ovRetVal = -1;

            /// --- Define & Initialize LibraryMethod's Outputs ---
            int rv1 = 0;
            StringBuilder OrderInfo = new StringBuilder(BUF_SIZE2);

            try
            {
                checkOrder(ivOrder, OrderInfo, out rv1);
            }
            catch
            {
                ovStatus = "Unable to access sswpke_fs library";
                ovRetVal = 4000;
                return false;
            }

            /// --- Get Event Log ---
            int rv2 = 0;
            StringBuilder EventLog = new StringBuilder(BUF_SIZE1);
            getEventLog(rv1, EventLog, out rv2);

            /// --- Finalize Outputs ---
            ovOrderInfo = OrderInfo.ToString();
            ovStatus = EventLog.ToString();
            ovRetVal = rv1;

            return (rv1 == 0) ? true : false;
        }
        #endregion PK_CheckOrder

        #region PK_GenCSR
        public bool PK_GenCSR(String ivKeyId, String[] ivProfile, String[] ivSubjAltName, int ivKeyLen, int ivX931GenKey, int ivExtractable, int ivRestSignMechCheck, out String ovCSR, out String ovStatus, out int ovRetVal)
        {
            /// --- Set Initial Value to Wrapper's Outputs ---
            ovCSR = "";
            ovStatus = "";
            ovRetVal = -1;

            /// --- Define & Initialize LibraryMethod's Outputs ---
            int rv1 = 0;
            StringBuilder CSR = new StringBuilder(MAX_BUF);

            try
            {
                genCSR(ivKeyId, ivProfile, ivSubjAltName, ivKeyLen, ivX931GenKey, ivExtractable, ivRestSignMechCheck, CSR, out rv1);
            }
            catch
            {
                ovStatus = "Unable to access sswpke_fs library";
                ovRetVal = 4000;
                return false;
            }

            /// --- Get Event Log ---
            int rv2 = 0;
            StringBuilder EventLog = new StringBuilder(BUF_SIZE1);
            getEventLog(rv1, EventLog, out rv2);

            /// --- Finalize Outputs ---
            ovCSR = CSR.ToString();
            ovStatus = EventLog.ToString();
            ovRetVal = rv1;

            return (rv1 == 0) ? true : false;
        }
        #endregion PK_GenCSR
    }
}
