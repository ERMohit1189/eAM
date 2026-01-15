using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data.SqlClient;
using c4SmsNew;

/// <summary>
/// Summary description for WebService
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class VendorIDName : System.Web.Services.WebService
{


    public VendorIDName()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }
    [WebMethod(EnableSession = true)]
    public string[] GetStudents(string studentId)
    {
        List<string> students = new List<string>();
        using (SqlConnection conn = new SqlConnection())
        {
            conn.ConnectionString = BAL.objBal.dbGet_connection().ConnectionString;
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "SELECT OrganizationName+' '+VendorCode as NAME,ID  FROM AccVendor  where (VendorCode like '%' + @SearchText + '%' or OrganizationName like '%' + @SearchText + '%') and BranchCode=" + Session["BranchCode"].ToString() + "";
                cmd.Parameters.AddWithValue("@SearchText", studentId);
                cmd.Connection = conn;
                conn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        // students.Add(string.Format("{0}", sdr["NAME"]));
                        students.Add(string.Format("{0}@{1}", sdr["NAME"], sdr["ID"]));
                    }
                }
                conn.Close();
            }
        }
        return students.ToArray();
    }

    [WebMethod(EnableSession = true)]
    public string[] GetQtnNo(string QtnNo)
    {
        List<string> students = new List<string>();
        using (SqlConnection conn = new SqlConnection())
        {
            conn.ConnectionString = BAL.objBal.dbGet_connection().ConnectionString;
            using (SqlCommand cmd = new SqlCommand())
            {
                string str = "SELECT Title+' ('+QtnNo+')' as NAME,QtnNo  FROM InvQuotation  where (Title like '%' + @SearchText + '%' or QtnNo like '%' + @SearchText + '%') and BranchCode=" + Session["BranchCode"].ToString() + " and status='Approve' and QtnNo not in (select QtnNo from invPurchaeOrder where BranchCode=" + Session["BranchCode"] + ")";
                cmd.CommandText = str;
                cmd.Parameters.AddWithValue("@SearchText", QtnNo);
                cmd.Connection = conn;
                conn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        students.Add(string.Format("{0}@{1}", sdr["NAME"], sdr["QtnNo"]));
                    }
                }
                conn.Close();
            }
        }
        return students.ToArray();
    }
    [WebMethod(EnableSession = true)]
    public string[] GetPoNo(string PONo)
    {
        List<string> students = new List<string>();
        using (SqlConnection conn = new SqlConnection())
        {
            conn.ConnectionString = BAL.objBal.dbGet_connection().ConnectionString;
            using (SqlCommand cmd = new SqlCommand())
            {
                string str= "SELECT distinct Subject+' ('+pono+')' as NAME,pono  FROM invPurchaeOrder  where (Subject like '%' + @SearchText + '%' or pono like '%' + @SearchText + '%') and BranchCode=" + Session["BranchCode"].ToString() + " and pono not in (select isnull(pono, '') from invInvoiceEntry where BranchCode=" + Session["BranchCode"] + ")";
                cmd.CommandText = str;
                cmd.Parameters.AddWithValue("@SearchText", PONo);
                cmd.Connection = conn;
                conn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        students.Add(string.Format("{0}@{1}", sdr["NAME"], sdr["pono"]));
                    }
                }
                conn.Close();
            }
        }
        return students.ToArray();
    }
    [WebMethod(EnableSession = true)]
    public string[] GetPoNoAll(string PONo)
    {
        List<string> students = new List<string>();
        using (SqlConnection conn = new SqlConnection())
        {
            conn.ConnectionString = BAL.objBal.dbGet_connection().ConnectionString;
            using (SqlCommand cmd = new SqlCommand())
            {
                string str = "SELECT distinct Subject+' ('+PONo+')' as NAME,PONo  FROM invPurchaeOrder  where (Subject like '%' + @SearchText + '%' or pono like '%' + @SearchText + '%') and BranchCode=" + Session["BranchCode"].ToString() + "";
                cmd.CommandText = str;
                cmd.Parameters.AddWithValue("@SearchText", PONo);
                cmd.Connection = conn;
                conn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        students.Add(string.Format("{0}@{1}", sdr["NAME"], sdr["PONo"]));
                    }
                }
                conn.Close();
            }
        }
        return students.ToArray();
    }
    [WebMethod(EnableSession = true)]
    public string[] GetInvoiceNo(string InvoiceNo)
    {
        List<string> InvoiceNoList = new List<string>();
        using (SqlConnection conn = new SqlConnection())
        {
            conn.ConnectionString = BAL.objBal.dbGet_connection().ConnectionString;
            using (SqlCommand cmd = new SqlCommand())
            {
                string str = "select distinct OrganizationName+' ('+InvoiceNo+')' NAME, InvoiceNo from invInvoiceEntry inv inner join AccVendor v on v.id=inv.VendorId and v.BranchCode=inv.BranchCode where isnull(PONo,'')='' and (OrganizationName like '%' + @SearchText + '%' or InvoiceNo like '%' + @SearchText + '%') and inv.BranchCode=" + Session["BranchCode"] + " ";
                cmd.CommandText = str;
                cmd.Parameters.AddWithValue("@SearchText", InvoiceNo);
                cmd.Connection = conn;
                conn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        InvoiceNoList.Add(string.Format("{0}@{1}", sdr["NAME"], sdr["InvoiceNo"]));
                    }
                }
                conn.Close();
            }
        }
        return InvoiceNoList.ToArray();
    }
}
