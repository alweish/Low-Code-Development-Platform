namespace RO.Common3
{
	using System;
	using System.Text;
	using System.Configuration;
	using System.Security.Cryptography;
    using System.Security.Cryptography.X509Certificates;
    using System.Net;
    using System.IO;

    public class Encryption: Key
    {
		private string pExpiryDt = "9999.12.01";
        private byte[] SignerCert = { 0x30, 0x82, 0x02, 0x32, 0x30, 0x82, 0x01, 0x9B, 0xA0, 0x03, 0x02, 0x01, 0x02, 0x02, 0x10, 0x9E, 0xDD, 0x12, 0x73, 0x76, 0xA4, 0xEC, 0x96, 0x4A, 0x6B, 0xDA, 0x87, 0xFB, 0x03, 0x15, 0x15, 0x30, 0x0D, 0x06, 0x09, 0x2A, 0x86, 0x48, 0x86, 0xF7, 0x0D, 0x01, 0x01, 0x04, 0x05, 0x00, 0x30, 0x28, 0x31, 0x26, 0x30, 0x24, 0x06, 0x03, 0x55, 0x04, 0x03, 0x13, 0x1D, 0x52, 0x69, 0x6E, 0x74, 0x61, 0x67, 0x69, 0x20, 0x52, 0x6F, 0x62, 0x6F, 0x63, 0x6F, 0x64, 0x65, 0x72, 0x20, 0x43, 0x6F, 0x72, 0x70, 0x6F, 0x72, 0x61, 0x74, 0x69, 0x6F, 0x6E, 0x30, 0x1E, 0x17, 0x0D, 0x31, 0x36, 0x30, 0x39, 0x31, 0x33, 0x31, 0x39, 0x33, 0x31, 0x35, 0x38, 0x5A, 0x17, 0x0D, 0x33, 0x39, 0x31, 0x32, 0x33, 0x31, 0x32, 0x33, 0x35, 0x39, 0x35, 0x39, 0x5A, 0x30, 0x28, 0x31, 0x26, 0x30, 0x24, 0x06, 0x03, 0x55, 0x04, 0x03, 0x13, 0x1D, 0x52, 0x69, 0x6E, 0x74, 0x61, 0x67, 0x69, 0x20, 0x52, 0x6F, 0x62, 0x6F, 0x63, 0x6F, 0x64, 0x65, 0x72, 0x20, 0x43, 0x6F, 0x72, 0x70, 0x6F, 0x72, 0x61, 0x74, 0x69, 0x6F, 0x6E, 0x30, 0x81, 0x9F, 0x30, 0x0D, 0x06, 0x09, 0x2A, 0x86, 0x48, 0x86, 0xF7, 0x0D, 0x01, 0x01, 0x01, 0x05, 0x00, 0x03, 0x81, 0x8D, 0x00, 0x30, 0x81, 0x89, 0x02, 0x81, 0x81, 0x00, 0xB7, 0xBD, 0x07, 0x3D, 0x06, 0xDD, 0x2D, 0xC8, 0x30, 0x22, 0xDA, 0x9C, 0x20, 0x10, 0x1D, 0x1F, 0x4B, 0x7B, 0x04, 0xC3, 0xCE, 0x7B, 0x76, 0x1B, 0xBF, 0x7F, 0x3E, 0xBD, 0xBD, 0x69, 0x14, 0xFE, 0x1E, 0xCE, 0x95, 0x66, 0x2D, 0x01, 0xB5, 0xA0, 0x95, 0x74, 0x3C, 0xDD, 0x09, 0x54, 0x29, 0x5F, 0x59, 0xC6, 0x5B, 0x2E, 0x9A, 0x5F, 0x26, 0xB6, 0x27, 0xC4, 0xE2, 0x15, 0x7E, 0x6A, 0x53, 0x72, 0x03, 0x83, 0xF8, 0x7D, 0xE5, 0xF1, 0xCF, 0x55, 0x22, 0xB7, 0x18, 0x9E, 0x65, 0x66, 0x26, 0x46, 0xFC, 0xFE, 0x21, 0x71, 0xE6, 0x52, 0x7B, 0x02, 0x54, 0x9F, 0x71, 0x2E, 0x53, 0xFC, 0x1D, 0xE5, 0x18, 0x53, 0x06, 0x2C, 0xA9, 0x03, 0xCC, 0x53, 0x47, 0xC5, 0x0E, 0xFA, 0xFE, 0x6C, 0xD1, 0x93, 0x56, 0xB9, 0x62, 0x43, 0x41, 0x0E, 0x84, 0xC1, 0x59, 0xFB, 0xFF, 0x14, 0x70, 0x10, 0xAD, 0x07, 0x02, 0x03, 0x01, 0x00, 0x01, 0xA3, 0x5D, 0x30, 0x5B, 0x30, 0x59, 0x06, 0x03, 0x55, 0x1D, 0x01, 0x04, 0x52, 0x30, 0x50, 0x80, 0x10, 0xBE, 0x0F, 0x20, 0xC5, 0xED, 0xBC, 0x77, 0x9B, 0x71, 0x66, 0xF5, 0x38, 0xFD, 0x52, 0x87, 0x98, 0xA1, 0x2A, 0x30, 0x28, 0x31, 0x26, 0x30, 0x24, 0x06, 0x03, 0x55, 0x04, 0x03, 0x13, 0x1D, 0x52, 0x69, 0x6E, 0x74, 0x61, 0x67, 0x69, 0x20, 0x52, 0x6F, 0x62, 0x6F, 0x63, 0x6F, 0x64, 0x65, 0x72, 0x20, 0x43, 0x6F, 0x72, 0x70, 0x6F, 0x72, 0x61, 0x74, 0x69, 0x6F, 0x6E, 0x82, 0x10, 0x9E, 0xDD, 0x12, 0x73, 0x76, 0xA4, 0xEC, 0x96, 0x4A, 0x6B, 0xDA, 0x87, 0xFB, 0x03, 0x15, 0x15, 0x30, 0x0D, 0x06, 0x09, 0x2A, 0x86, 0x48, 0x86, 0xF7, 0x0D, 0x01, 0x01, 0x04, 0x05, 0x00, 0x03, 0x81, 0x81, 0x00, 0xB3, 0xA4, 0xCE, 0xA5, 0x2C, 0x80, 0x1E, 0x3A, 0x2F, 0x07, 0x4A, 0x5A, 0x22, 0x03, 0x29, 0x5D, 0x94, 0x25, 0x67, 0x5D, 0x76, 0x56, 0x42, 0x60, 0x9E, 0xF4, 0xEE, 0xE6, 0x13, 0x46, 0x4C, 0x77, 0x66, 0x76, 0xD5, 0x37, 0x55, 0xBB, 0xF8, 0x03, 0x9E, 0x46, 0x65, 0x7B, 0x94, 0x17, 0x75, 0xD9, 0xA0, 0xE7, 0x1A, 0xDD, 0xEF, 0xE6, 0x58, 0x3A, 0x01, 0xEA, 0x43, 0x2F, 0x66, 0x16, 0xC4, 0x3D, 0x7B, 0x5D, 0x3A, 0x12, 0xD1, 0xA0, 0x44, 0xF4, 0x38, 0x6F, 0x4D, 0xFA, 0x77, 0xAA, 0x0C, 0xF9, 0x2B, 0xEA, 0xE4, 0x8A, 0x48, 0xA8, 0x69, 0xCF, 0x99, 0x58, 0xEC, 0x37, 0x72, 0xDF, 0x32, 0x19, 0xB0, 0xB6, 0xB9, 0x9C, 0xEC, 0x70, 0xBF, 0x6F, 0x75, 0x9C, 0x31, 0x0F, 0x3A, 0x82, 0x8A, 0x18, 0x21, 0x9E, 0xCB, 0x92, 0x13, 0x22, 0x06, 0x47, 0xD6, 0x4B, 0x0F, 0x7C, 0xCC, 0xB0, 0xA8, 0x9B };
		// RCEncryption uses TripleDES algorithm to encrypt and/or decrypt an input string.
		// By default a key is used to do the decryption, this key should be the same for decryption and encryption.
		
		private string PrevKey
		{
			get{return pPrevKey;}
			set{pPrevKey = value;}
		}
		
		private string CurrKey
		{
			get{return pCurrKey;}
			set{pCurrKey = value;}
		}
		
		public Encryption()
		{
			if (DateTime.Now >= DateTime.Parse(pExpiryDt)) { throw new Exception("License has expired, please procure another license and try again."); }
		}

		public string EncryptString(string inStr)
		{
			return EncryptString(inStr, CurrKey);
		}

		public string EncryptString(string inStr, string inKey)
		{
			string outStr = string.Empty;
			MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
			TripleDESCryptoServiceProvider des = new TripleDESCryptoServiceProvider();
			des.Mode = CipherMode.ECB;
			try
			{
				des.Key = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(inKey));
				outStr = Convert.ToBase64String(des.CreateEncryptor().TransformFinalBlock(UTF8Encoding.UTF8.GetBytes(inStr), 0, UTF8Encoding.UTF8.GetBytes(inStr).Length));
			}
			catch
			{
				outStr = null;
			}
			hashmd5 = null;
			des = null;
			return outStr;
		}

        public string GetInstallID()
        {
            byte[] data = new System.Security.Cryptography.MD5CryptoServiceProvider().ComputeHash(UTF8Encoding.UTF8.GetBytes(GetMachineKey()));
            string installID = Convert.ToBase64String(data).Left(10).ToUpper();
            return installID;
        }

        private System.Collections.Generic.Dictionary<string, string> GetLicense(string installID, string appID, string moduleName)
        {
            X509Certificate2 cert = new X509Certificate2(SignerCert);
            RSACryptoServiceProvider csp = (RSACryptoServiceProvider)cert.PublicKey.Key;
            SHA1Managed sha1 = new SHA1Managed();
            System.Collections.Generic.Dictionary<string, string> signedLicense = System.Web.HttpRuntime.Cache["InstallationLicense"] as System.Collections.Generic.Dictionary<string, string>;
            try
            {
                string licenseJSON = signedLicense["License"];
                string licenseSig = signedLicense["LicenseSig"];
                if (csp.VerifyHash(sha1.ComputeHash(UTF8Encoding.UTF8.GetBytes(licenseJSON)), CryptoConfig.MapNameToOID("SHA1"), Convert.FromBase64String((licenseSig))))
                    return signedLicense;
                else throw new Exception("no cached license");
            }
            catch 
            {
                string licenseServerUrl = (System.Configuration.ConfigurationManager.AppSettings["LicenseServer"] ?? "https://www.rintagi.com") + "/AdminWs.asmx/GetLicenseDetail";
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(licenseServerUrl);
                httpWebRequest.ContentType = "application/json; charset=utf-8";
                httpWebRequest.Accept = "application/json; charset=utf-8";
                httpWebRequest.Method = "POST";

                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    string json = "{ installID: '" + installID + "',appID:'" + appID + "',moduleName:'" + moduleName + "'}";

                    streamWriter.Write(json);
                }
                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var responseText = streamReader.ReadToEnd();
                    System.Web.Script.Serialization.JavaScriptSerializer jss = new System.Web.Script.Serialization.JavaScriptSerializer();
                    System.Collections.Generic.Dictionary<string, System.Collections.Generic.Dictionary<string, string>> result = jss.Deserialize<System.Collections.Generic.Dictionary<string, System.Collections.Generic.Dictionary<string, string>>>(responseText);
                    string licenseJSON = result["d"]["License"];
                    string licenseSig = result["d"]["LicenseSig"];

                    if (csp.VerifyHash(sha1.ComputeHash(UTF8Encoding.UTF8.GetBytes(licenseJSON)), CryptoConfig.MapNameToOID("SHA1"), Convert.FromBase64String((licenseSig))))
                    {
                        System.Collections.Generic.List<System.Collections.Generic.Dictionary<string, string>> licenseDetail = jss.Deserialize<System.Collections.Generic.List<System.Collections.Generic.Dictionary<string, string>>>(licenseJSON);
                        foreach (var l in licenseDetail)
                        {
                            if (l["InstallID"] == installID
                                && l["AppID"] == appID
                                && DateTime.Parse(l["Expiry"]) >= DateTime.Today.ToUniversalTime())
                            {
                                try
                                {
                                    DateTime cacheUntil = DateTime.Now.ToUniversalTime().AddDays((DateTime.Parse(l["Expiry"]).Date - DateTime.Now.ToUniversalTime().Date).TotalDays);
                                    System.Web.HttpRuntime.Cache.Add("InstallationLicense", result["d"], null, cacheUntil, System.Web.Caching.Cache.NoSlidingExpiration, System.Web.Caching.CacheItemPriority.Normal, null);
                                }
                                catch { }
                            }
                            return result["d"];
                        }
                        return null;
                    }
                    else return null;
                }
            }

        }
        public System.Collections.Generic.List<System.Collections.Generic.Dictionary<string, string>> GetLicenseDetail(string installID, string appID, string moduleName)
        {
            System.Collections.Generic.Dictionary<string, string> signedLicense = GetLicense(GetInstallID(), "RO", "RO");
            System.Web.Script.Serialization.JavaScriptSerializer jss = new System.Web.Script.Serialization.JavaScriptSerializer();
            string licenseJSON = signedLicense["License"];
            string licenseSig = signedLicense["LicenseSig"];
            X509Certificate2 cert = new X509Certificate2(SignerCert);
            RSACryptoServiceProvider csp = (RSACryptoServiceProvider)cert.PublicKey.Key;
            SHA1Managed sha1 = new SHA1Managed();
            byte[] hash = sha1.ComputeHash(UTF8Encoding.UTF8.GetBytes(GetInstallID()));
            if (csp.VerifyHash(sha1.ComputeHash(UTF8Encoding.UTF8.GetBytes(licenseJSON)), CryptoConfig.MapNameToOID("SHA1"), Convert.FromBase64String((licenseSig))))
            {
                System.Collections.Generic.List<System.Collections.Generic.Dictionary<string, string>> licenseDetail = jss.Deserialize<System.Collections.Generic.List<System.Collections.Generic.Dictionary<string, string>>>(licenseJSON);

                return licenseDetail;
            }
            else return new System.Collections.Generic.List<System.Collections.Generic.Dictionary<string,string>>();

        }
        public bool IsValidateLicense()
        {
            // how to get cert public key
            // X509Certificate2 cert = new X509Certificate2(@"c:\inetpub\wwwroot\ro\web\modules\rintagi_signer.cer");
            // string xx = cert.GetRawCertDataString();
            // string zz = "0x" + BitConverter.ToString(cert.Export(X509ContentType.Cert)).Replace("-", ",0x");
            // paste as raw hex value for raw_cert
            X509Certificate2 cert = new X509Certificate2(SignerCert);
            RSACryptoServiceProvider csp = (RSACryptoServiceProvider)cert.PublicKey.Key;
            SHA1Managed sha1 = new SHA1Managed();
            byte[] hash = sha1.ComputeHash(UTF8Encoding.UTF8.GetBytes(GetInstallID()));
            string license = Config.RintagiLicense;
            try
            {
                byte[] signature = Convert.FromBase64String(license);

                //how to sign
                //string sig = RO.Common3.Utils.SignData(UTF8Encoding.UTF8.GetBytes(GetInstallID()), @"c:\inetpub\wwwroot\ro\web\modules\rintagi_signer.pfx");
                //bool x = csp.VerifyHash(hash, CryptoConfig.MapNameToOID("SHA1"), Convert.FromBase64String(sig));

                // Verify the signature with the hash
                bool hasPermLicense = csp.VerifyHash(hash, CryptoConfig.MapNameToOID("SHA1"), signature);
                if (!hasPermLicense) throw new Exception("no perm license");
                return hasPermLicense;
            }
            catch {
                try
                {
                    System.Collections.Generic.Dictionary<string, string> signedLicense = GetLicense(GetInstallID(), "RO", "RO");
                    System.Web.Script.Serialization.JavaScriptSerializer jss = new System.Web.Script.Serialization.JavaScriptSerializer();
                    string licenseJSON = signedLicense["License"];
                    string licenseSig = signedLicense["LicenseSig"];
                    if (csp.VerifyHash(sha1.ComputeHash(UTF8Encoding.UTF8.GetBytes(licenseJSON)), CryptoConfig.MapNameToOID("SHA1"), Convert.FromBase64String((licenseSig))))
                    {
                        System.Collections.Generic.List<System.Collections.Generic.Dictionary<string, string>> licenseDetail = jss.Deserialize<System.Collections.Generic.List<System.Collections.Generic.Dictionary<string, string>>>(licenseJSON);
                        foreach (var l in licenseDetail)
                        {
                            if (l["InstallID"] == GetInstallID()
                                && l["AppID"] == "RO"
                                && DateTime.Parse(l["Expiry"]) >= DateTime.Today.ToUniversalTime())
                            {
                                return true;
                            }
                        }
                    }
                    return false;
                }
                catch 
                {
                    return false;
                }
            }
        }

        public string GetMachineKey()
        {
            return EncryptString(RO.Common3.Utils.GetComputerSid().ToString());
        }

        public bool CheckValidLicense()
        {
            return true;
            /*
            if (EncryptString("Rintagi".ToUpper()) == "tToRcgonOIY=") return true; // internal projects
            return IsValidateLicense();
            */
            //if (EncryptString("Rintagi".ToUpper()) == "tToRcgonOIY=") return; // internal projects
            //if (EncryptString("Rintagi".ToUpper()) == "blciyNL5Rc4=") return; // external projects

            //string myHost = System.Net.Dns.GetHostName();
            //bool isValidLicense = false;

            //string license = DecryptString(Config.RintagiLicense);
            //if (!string.IsNullOrEmpty(license))
            //{
            //    string[] licenseType = license.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
            //    if (licenseType[0] == "IP")
            //    {
            //        foreach (System.Net.IPAddress ip in System.Net.Dns.GetHostEntry(myHost).AddressList)
            //        {
            //            if (EncryptString((Config.AppNameSpace + ip.ToString() + Config.AppNameSpace).ToUpper()) == licenseType[licenseType.Length - 1].Substring(1)) { isValidLicense = true; break; };
            //        }
            //        // for EC2 specific
            //        try
            //        {
            //            System.Net.WebRequest wr = System.Net.HttpWebRequest.Create("http://169.254.169.254/latest/meta-data/public-ipv4");
            //            System.IO.StreamReader sr = new System.IO.StreamReader(wr.GetResponse().GetResponseStream());
            //            string ec2IP = sr.ReadToEnd();
            //            sr.Close();
            //            if (!string.IsNullOrEmpty(ec2IP) && EncryptString((Config.AppNameSpace + ec2IP.ToString() + Config.AppNameSpace).ToUpper()) == licenseType[licenseType.Length - 1].Substring(1)) { isValidLicense = true; };
            //        }
            //        catch { }
            //    }
            //    else
            //    {
            //        if (EncryptString((Config.AppNameSpace + "Rintagi".ToUpper() + Config.AppNameSpace).ToUpper()) == licenseType[licenseType.Length - 1].Substring(1)) { isValidLicense = true; };
            //    }
            //}

            //if (!isValidLicense)
            //{
            //    throw new Exception("Please acquire proper Rintagi license for this instance to run");
            //}
        }
        public void UpdateLicense(string license,string hash)
        {
            if (Convert.ToBase64String((new HMACMD5(UTF8Encoding.UTF8.GetBytes((new RO.Common3.Encryption()).GetType().ToString() + Config.AppNameSpace.ToUpper()))).ComputeHash(UTF8Encoding.UTF8.GetBytes(license))) == hash)
            {
                Config.RintagiLicense = license;
            }
        }

        protected string DecryptString(string inStr, string inKey)
        {
            if (string.IsNullOrEmpty(inStr)) return null;

            string outStr = "";
            MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
            TripleDESCryptoServiceProvider des = new TripleDESCryptoServiceProvider();
            des.Mode = CipherMode.ECB;
            try
            {
                des.Key = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(inKey));
                outStr = UTF8Encoding.UTF8.GetString(des.CreateDecryptor().TransformFinalBlock(Convert.FromBase64String(inStr), 0, Convert.FromBase64String(inStr).Length));
            }
            catch
            {
                outStr = null;
            }
            hashmd5 = null;
            des = null;
            return outStr;
        }

		protected string DecryptString(string inStr)
		{
            if (string.IsNullOrEmpty(inStr)) return null;

			string outStr="";
			MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
			TripleDESCryptoServiceProvider des = new TripleDESCryptoServiceProvider();
			des.Mode = CipherMode.ECB;
			try
			{
				des.Key = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(CurrKey));
				outStr = UTF8Encoding.UTF8.GetString(des.CreateDecryptor().TransformFinalBlock(Convert.FromBase64String(inStr),0,Convert.FromBase64String(inStr).Length));
			}
			catch
			{
				try
				{
					des.Key = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(PrevKey));
					outStr = UTF8Encoding.UTF8.GetString(des.CreateDecryptor().TransformFinalBlock(Convert.FromBase64String(inStr),0,Convert.FromBase64String(inStr).Length));
				}
				catch
				{
					outStr=null;
				}
			}
			hashmd5 = null;
			des = null;
			return outStr;
		}
		protected string GetDesConnStr()
		{
			return Config.GetConnStr(Config.DesProvider, Config.DesServer, Config.DesDatabase, "", Config.DesUserId) + DecryptString(Config.DesPassword);
		}
	}
}