using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Data.SqlClient;

public partial class iciciGateway : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection();
    Campus oo = new Campus();
    string sql = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {

        var param = new List<SqlParameter>
        {
            new SqlParameter("@QueryFor", "UPdateRef"),
        };
        SqlParameter para = new SqlParameter("@Msg", "");
        para.Direction = ParameterDirection.Output;
        param.Add(para);
        var msg = DLL.objDll.Sp_Insert_Update_Delete_usingExecuteNonQuery("USP_EasyPayGatewaySetting", param);

        string sql = "select top(1) ReturnUrl, ReferenceNo, SubMerchantId, MerchantID, AESKey, Isactive from TblPaymentGateway where BranchCode=1 and GateWayFor='Fee' and Isactive=1 and GateWayName='EassyPay'";
        string ReferenceNop = oo.ReturnTag(sql, "ReferenceNo");
        string SubMerchantIdp = oo.ReturnTag(sql, "SubMerchantId");

        string mandatoryfields = encryptFile((ReferenceNop + "|" + SubMerchantIdp + "|" + "02"), "2203183047705006");
        string optionalfields = encryptFile("20|20|20|20", "2203183047705006");
        string returnurl = encryptFile("http://eam.redroseschool.in/eam/apiresponses.aspx ", "2203183047705006");
        string ReferenceNo = encryptFile(ReferenceNop, "2203183047705006");
        string submerchantid = encryptFile(SubMerchantIdp, "2203183047705006");
        string transactionamount = encryptFile("02", "2203183047705006");
        string paymode = encryptFile("9", "2203183047705006");
        string textToEncrypt = "https://eazypay.icicibank.com/EazyPG?merchantid=224774&mandatory fields=" + mandatoryfields + "&optional fields=&returnurl= "+ returnurl + "&Reference No="+ReferenceNo+"&submerchantid="+ submerchantid + "&transaction amount="+transactionamount+"&paymode="+paymode+"";
        //txtString.Text = textToEncrypt;
        dd.PostBackUrl = textToEncrypt;
    }
    public static string encryptFile(string textToEncrypt, string key)
    {
        RijndaelManaged rijndaelCipher = new RijndaelManaged();
        rijndaelCipher.Mode = CipherMode.ECB;
        rijndaelCipher.Padding = PaddingMode.PKCS7;
        rijndaelCipher.KeySize = 0x80;
        rijndaelCipher.BlockSize = 0x80;
        byte[] pwdBytes = Encoding.UTF8.GetBytes(key);
        byte[] keyBytes = new byte[0x10];
        int len = pwdBytes.Length;
        if (len > keyBytes.Length)
        {
            len = keyBytes.Length;
        }
        Array.Copy(pwdBytes, keyBytes, len);
        rijndaelCipher.Key = keyBytes;


        rijndaelCipher.IV = keyBytes;
        ICryptoTransform transform = rijndaelCipher.CreateEncryptor();
        byte[] plainText = Encoding.UTF8.GetBytes(textToEncrypt);
        return Convert.ToBase64String(transform.TransformFinalBlock(plainText, 0,
        plainText.Length));
    }
    
}