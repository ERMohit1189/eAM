using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;

/// <summary>
/// Summary description for webHook
/// </summary>
public class webHookrequest
{
    public class responseDetails
    {
        public string statusCode { get; set; }
        public string message { get; set; }
        public string description { get; set; }
    }
    public class payInstrument
    {
        public List<responseDetails> responseDetails { get; set; }
    }

    public class jsonObject
    {
        public payInstrument payInstrument { get; set; }
    }
    public string APIrequest(string merchId, string merchTxnDate, string password, string merchTxnIds, double amount)
    {
        try
        {
            // using System.Net;
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            // Use SecurityProtocolType.Ssl3 if needed for compatibility reasons

            string reqHashKey = "4ff45ddf51b3022d8f";
            string passphrase = "BCA2F60CCD5E4D94D0CAD7A667150022";// "BCA2F60CCD5E4D94D0CAD7A667150022";
            string salt = "BCA2F60CCD5E4D94D0CAD7A667150022";// "BCA2F60CCD5E4D94D0CAD7A667150022";
            byte[] iv = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 };
            int iterations = 65536;
            Pojo.Root rt = new Pojo.Root();
            Pojo.HeadDetails hd = new Pojo.HeadDetails();
            Pojo.MerchDetails md = new Pojo.MerchDetails();
            Pojo.PayDetails pd = new Pojo.PayDetails();
            Pojo.PayInstrument pi = new Pojo.PayInstrument();
            hd.api = "TXNVERIFICATION";
            hd.source = "source";
            md.merchId = merchId;
            md.password = password;
            md.merchTxnId = merchTxnIds;
            md.merchTxnDate = merchTxnDate;

            pd.amount = amount;
            pd.txnCurrency = "INR";

            string strsignature = md.merchId + md.password + md.merchTxnId + pd.amount + pd.txnCurrency + hd.api;
            byte[] bytes = Encoding.UTF8.GetBytes(reqHashKey);
            byte[] bt = new System.Security.Cryptography.HMACSHA512(bytes).ComputeHash(Encoding.UTF8.GetBytes(strsignature));
            string signature = byteToHexString(bt).ToLower();
            pd.signature = signature;
            pi.headDetails = hd;
            pi.merchDetails = md;
            pi.payDetails = pd;
            rt.payInstrument = pi;
            var json = new JavaScriptSerializer().Serialize(rt);
            string Encryptval = Encrypt(json, passphrase, salt, iv, iterations);
            string Link = "https://payment1.atomtech.in/ots/payment/status?merchId=" + merchId + "&encData=" + Encryptval;
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Link);

            request.ProtocolVersion = HttpVersion.Version11;
            request.Method = "POST";
            request.ContentType = "application/json";
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
            request.Proxy.Credentials = CredentialCache.DefaultCredentials;
            Encoding encoding = new UTF8Encoding();
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream resStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(resStream);
            string responseFromServer = reader.ReadToEnd();
            var uri = new Uri("http://atom.in?" + responseFromServer);


            var query = HttpUtility.ParseQueryString(uri.Query);

            string encData = query.Get("encData");

            string passphrase1 = "8384E1F34C7503507179BC818A9158F0";// "8384E1F34C7503507179BC818A9158F0";
            string salt1 = "8384E1F34C7503507179BC818A9158F0";// "8384E1F34C7503507179BC818A9158F0";
            string Decryptval = decrypt(encData, passphrase1, salt1, iv, iterations);

            JObject jObject = JObject.Parse(Decryptval);
            var statusCode = jObject["payInstrument"].First.First.Parent["responseDetails"];
            statusCode = JsonConvert.DeserializeObject<responseDetails>(statusCode.ToString()).statusCode;

            return statusCode.ToString();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message.ToString());
        }
    }
    public static string byteToHexString(byte[] byData)
    {
        StringBuilder sb = new StringBuilder((byData.Length * 2));
        for (int i = 0; (i < byData.Length); i++)
        {
            int v = (byData[i] & 255);
            if ((v < 16))
            {
                sb.Append('0');
            }

            sb.Append(v.ToString("X"));

        }

        return sb.ToString();
    }
    public string Encrypt(string plainText, string passphrase, string salt, Byte[] iv, int iterations)
    {
        var plainBytes = Encoding.UTF8.GetBytes(plainText);
        string data = ByteArrayToHexString(Encrypt(plainBytes, GetSymmetricAlgorithm(passphrase, salt, iv, iterations))).ToUpper();


        return data;
    }
    public String decrypt(String plainText, String passphrase, String salt, Byte[] iv, int iterations)
    {
        byte[] str = HexStringToByte(plainText);

        string data1 = Encoding.UTF8.GetString(decrypt(str, GetSymmetricAlgorithm(passphrase, salt, iv, iterations)));
        return data1;
    }
    public byte[] Encrypt(byte[] plainBytes, SymmetricAlgorithm sa)
    {
        return sa.CreateEncryptor().TransformFinalBlock(plainBytes, 0, plainBytes.Length);

    }
    public byte[] decrypt(byte[] plainBytes, SymmetricAlgorithm sa)
    {
        return sa.CreateDecryptor().TransformFinalBlock(plainBytes, 0, plainBytes.Length);
    }
    public SymmetricAlgorithm GetSymmetricAlgorithm(String passphrase, String salt, Byte[] iv, int iterations)
    {
        var saltBytes = new byte[16];
        var ivBytes = new byte[16];
        Rfc2898DeriveBytes rfcdb = new System.Security.Cryptography.Rfc2898DeriveBytes(passphrase, Encoding.UTF8.GetBytes(salt), iterations, HashAlgorithmName.SHA512);
        saltBytes = rfcdb.GetBytes(32);
        var tempBytes = iv;
        Array.Copy(tempBytes, ivBytes, Math.Min(ivBytes.Length, tempBytes.Length));
        var rij = new RijndaelManaged(); //SymmetricAlgorithm.Create();
        rij.Mode = CipherMode.CBC;
        rij.Padding = PaddingMode.PKCS7;
        rij.FeedbackSize = 128;
        rij.KeySize = 128;

        rij.BlockSize = 128;
        rij.Key = saltBytes;
        rij.IV = ivBytes;
        return rij;
    }
    protected static byte[] HexStringToByte(string hexString)
    {
        try
        {
            int bytesCount = (hexString.Length) / 2;
            byte[] bytes = new byte[bytesCount];
            for (int x = 0; x < bytesCount; ++x)
            {
                bytes[x] = Convert.ToByte(hexString.Substring(x * 2, 2), 16);
            }
            return bytes;
        }
        catch
        {
            throw;
        }
    }
    public static string ByteArrayToHexString(byte[] ba)
    {
        StringBuilder hex = new StringBuilder(ba.Length * 2);
        foreach (byte b in ba)
            hex.AppendFormat("{0:x2}", b);
        return hex.ToString();
    }
}