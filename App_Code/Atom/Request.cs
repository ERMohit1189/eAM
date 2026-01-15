using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;

/// <summary>
/// Summary description for Request
/// </summary>
public class Request
{
    public Request()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public string GetToken(string merchId, string userId, string password, string schooolEmail, string custMobile,string amount, string custEmail, string merchTxnIds)
    {
        string payInstrument = "";
        string Tok_id = "";


        try
        {

            Payrequest.RootObject rt = new Payrequest.RootObject();
            Payrequest.MsgBdy mb = new Payrequest.MsgBdy();
            Payrequest.HeadDetails hd = new Payrequest.HeadDetails();
            // Payrequest.HeadDetails hd = new Payrequest.HeadDetails();
            Payrequest.MerchDetails md = new Payrequest.MerchDetails();
            Payrequest.PayDetails pd = new Payrequest.PayDetails();
            Payrequest.CustDetails cd = new Payrequest.CustDetails();
            Payrequest.Extras ex = new Payrequest.Extras();

            Payrequest.Payrequest pr = new Payrequest.Payrequest();


            hd.version = "OTSv1.1";
            hd.api = "AUTH";
            hd.platform = "FLASH";

            md.merchId = merchId;
            md.userId = "";
            md.password = password;
            md.merchTxnDate = DateTime.Now.ToString("yyyy-MM-dd HH:MM:ss");// "2021-09-04 20:46:00";
            md.merchTxnId = merchTxnIds;// "test000123";


            pd.amount = amount;
            pd.product = "COLLEGE";
            pd.custAccNo = "213232323";
            pd.txnCurrency = "INR";

            cd.custEmail = custEmail==""?schooolEmail: custEmail;
            cd.custMobile = custMobile;

            ex.udf1 = "";
            ex.udf2 = "";
            ex.udf3 = "";
            ex.udf4 = "";
            ex.udf5 = "";


            pr.headDetails = hd;
            pr.merchDetails = md;
            pr.payDetails = pd;
            pr.custDetails = cd;
            pr.extras = ex;

            rt.payInstrument = pr;
            var json = new JavaScriptSerializer().Serialize(rt);



            string passphrase = "BCA2F60CCD5E4D94D0CAD7A667150022";
            string salt = "BCA2F60CCD5E4D94D0CAD7A667150022";
            byte[] iv = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 };
            int iterations = 65536;
            int keysize = 256;
            string hashAlgorithm = "SHA1";
            string Encryptval = Encrypt(json, passphrase, salt, iv, iterations);

            string testurleq = "https://payment1.atomtech.in/ots/aipay/auth?merchId=" + merchId + "&encData=" + Encryptval;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(testurleq);
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };

            request.Proxy.Credentials = CredentialCache.DefaultCredentials;
            Encoding encoding = new UTF8Encoding();
            byte[] data = encoding.GetBytes(json);
            request.ProtocolVersion = HttpVersion.Version11;
            request.Method = "POST";
            request.ContentType = "application/json";
            request.ContentLength = data.Length;
            //request.Timeout = 600000;
            Stream stream = request.GetRequestStream();
            stream.Write(data, 0, data.Length);
            stream.Close();
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            string jsonresponse = response.ToString();

            StreamReader reader = new StreamReader(response.GetResponseStream());
            string temp = null;
            string status = "";
            while ((temp = reader.ReadLine()) != null)
            {
                jsonresponse += temp;
            }
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            var result = jsonresponse.Replace("System.Net.HttpWebResponse", "");

            var uri = new Uri("http://atom.in?" + result);


            var query = HttpUtility.ParseQueryString(uri.Query);

            string encData = query.Get("encData");
            string passphrase1 = "8384E1F34C7503507179BC818A9158F0";
            string salt1 = "8384E1F34C7503507179BC818A9158F0";
            string Decryptval = decrypt(encData, passphrase1, salt1, iv, iterations);
            Payverify.Payverify objectres = new Payverify.Payverify();
            objectres = new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<Payverify.Payverify>(Decryptval);
            string txnMessage = objectres.responseDetails.txnMessage;
            Tok_id = objectres.atomTokenId;

            return Tok_id;
        }

        catch (Exception ex)
        {
        }
        return Tok_id;
    }

    protected void btmpay_Click(object sender, EventArgs e)
    {



    }


    public String Encrypt(String plainText, String passphrase, String salt, Byte[] iv, int iterations)
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