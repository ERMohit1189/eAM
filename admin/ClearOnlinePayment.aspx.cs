using System.Data.SqlClient;
using System;
using System.Net;
using System.Data;
using System.Collections.Generic;

public partial class ClearOnlinePayment : System.Web.UI.Page
{
    string sql = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
        {
            Response.Redirect("default.aspx");
        }
        Campus camp = new Campus(); camp.LoadLoader(loader);
    }

    [System.Web.Services.WebMethod]
    public static Details[] GetData(string fromDate, string toDate)
    {
        Campus oo = new Campus();
        SqlConnection con = new SqlConnection();
        con = oo.dbGet_connection();
        DataTable dt = new DataTable();
        List<Details> details = new List<Details>();
        con.Open();
        SqlCommand cmd = new SqlCommand("select SrNo, TxnID, RecieptSrNo, RecievedAmount, FeeDepositeDate, Status, case when isnull(Cancel, '')='Y' then 'Cancelled' else '' end Cancel from FeeDeposite where MOP='Online' and Status<>'Paid' and convert(date, FeeDepositeDate) between '" + fromDate + "' and '" + toDate + "'", con);
        cmd.CommandType = CommandType.Text;
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dt);
        con.Close();
        foreach (DataRow dtrow in dt.Rows)
        {
            Details Detail = new Details();
            Detail.SrNo = dtrow["SrNo"].ToString();
            Detail.TxnID = dtrow["TxnID"].ToString();
            Detail.RecieptSrNo = dtrow["RecieptSrNo"].ToString();
            Detail.RecievedAmount = dtrow["RecievedAmount"].ToString();
            Detail.FeeDepositeDate = DateTime.Parse(dtrow["FeeDepositeDate"].ToString()).ToString("dd-MMM-yyyy");
            Detail.CancelStatus = dtrow["Cancel"].ToString();
            Detail.Status = dtrow["Status"].ToString();
            details.Add(Detail);
        }
        return details.ToArray();
    }

    public class Details
    {
        public string SrNo { get; set; }
        public string TxnID { get; set; }
        public string RecieptSrNo { get; set; }
        public string RecievedAmount { get; set; }
        public string FeeDepositeDate { get; set; }
        public string CancelStatus { get; set; }
        public string Status { get; set; }
    }

    [System.Web.Services.WebMethod]
    public static string GetOnlineData(string merchantTransactionIds)
    {
        string result = "";
        ServicePointManager.SecurityProtocol = (SecurityProtocolType)(0xc0 | 0x300 | 0xc00);
        string URI = "https://www.payumoney.com/payment/payment/chkMerchantTxnStatus?";
        string authorizationHeader = "ANEAVGil2OCGKh9NyshZbc8RzOlcSgAtLAx4j95afig=";
        string myParameters = "merchantKey=1xFJKtXe&merchantTransactionIds=" + merchantTransactionIds + "";

        using (WebClient wc = new WebClient())
        {
            wc.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
            wc.Headers.Add("Authorization", authorizationHeader);
            var HtmlResult = wc.UploadString(URI, myParameters);
            result = HtmlResult;
            return result;
        }
    }

    [System.Web.Services.WebMethod]
    public static void UpdateData(string data)
    {
        string[] valData = data.Split(new string[] { "$" }, StringSplitOptions.None);
        for (int i = 0; i < (valData.Length-1); i++)
        {
            string[] valDataFinal = valData[i].Split(new string[] { "##" }, StringSplitOptions.None);
            Campus oo = new Campus();
            SqlConnection con = new SqlConnection();
            con = oo.dbGet_connection();
            using (SqlCommand cmd = new SqlCommand("ClearOnlinePaymentProc"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SrNo", valDataFinal[0].ToString().Trim());
                cmd.Parameters.AddWithValue("@RecieptSrNo", valDataFinal[1].ToString().Trim());
                cmd.Connection = con;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

    }

}