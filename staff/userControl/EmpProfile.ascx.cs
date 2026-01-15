using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class staff_userControl_EmpProfile : System.Web.UI.UserControl
{
    Campus oo = new Campus();
    public SqlConnection con;
    public SqlCommand cmd = new SqlCommand();
    public SqlDataAdapter ad = new SqlDataAdapter();
    string sql = "";
    public void MakeConnection()
    {
        con = new SqlConnection();
        try
        {
            cmd = new SqlCommand();
            con = oo.dbGet_connection();
            con.Open();
        }
        catch { }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetEmpProfile();
        }

    }
    private void GetEmpProfile()
    {
        sql = "Select EmpId,Ecode,Name,Designation,RegistrationDate DOJ,ContactNo,EMobileNo,Email,(Address + ', ' + City + ', ' + State) Address,PhotoPath From dbo.GetSingleStaffRecords_UDF(" + Session["BranchCode"].ToString() + ", '" + Session["LoginName"] + "')";
        var data = oo.Fetchdata(sql);
        if (data.Rows.Count > 0)
        {

            string path = data.Rows[0]["PhotoPath"].ToString() != string.Empty ? "../" + data.Rows[0]["PhotoPath"] + "?value=" + DateTime.Now.ToString(CultureInfo.InvariantCulture) : "~/img/user-pic/user-pic.jpg" + "?value=" + DateTime.Now.ToString(CultureInfo.InvariantCulture);
            //if (!File.Exists(path))
            //{
            //    path = "~/img/user-pic/user-pic.jpg";
            //}
            stImage.ImageUrl = path;
            
            lblEmpId.Text = data.Rows[0]["EmpId"].ToString();
            lblEmpCode.Text = data.Rows[0]["Ecode"].ToString();
            lblStName.Text = data.Rows[0]["Name"].ToString();
            lblDes.Text = data.Rows[0]["Designation"].ToString();
            lblDOJ.Text = data.Rows[0]["DOJ"].ToString();
            lblContactNo.Text = data.Rows[0]["ContactNo"].ToString();
            lblENo.Text = data.Rows[0]["EMobileNo"].ToString();
            lblEmail.Text = data.Rows[0]["Email"].ToString();
            lblAddress.Text = data.Rows[0]["Address"].ToString();
        }
    }
}