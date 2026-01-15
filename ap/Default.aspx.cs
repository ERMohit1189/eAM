using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using c4SmsNew;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class ap_Default : System.Web.UI.Page
{
    private SqlConnection _con;
    private readonly Campus _oo;
#pragma warning disable 169
    private string _sql, _mess = string.Empty;
#pragma warning restore 169
    private DataTable _dt;
    public ap_Default()
    {
        _con = new SqlConnection();
        _oo = new Campus();
        _dt = new DataTable();
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        _con = _oo.dbGet_connection();
        Page.ClientScript.RegisterStartupScript(GetType(), "myScript", "ClearTextboxes();", true);
        if (!IsPostBack)
        {
            _sql = "select BranchId, BranchName from BranchTab";
            _oo.FillDropDown_withValue(_sql, DrpBranchName, "BranchName", "BranchId");
            DrpBranchName.Items.Insert(0, new ListItem("<--Select Branch-->", ""));
            Head1.DataBind();
            Page.ClientScript.RegisterStartupScript(GetType(), "myScript", "ClearTextboxes();", true);
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter("@Queryfor", "S"));
            DataSet ds = new DataSet();
            ds = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("Get_GenralInfo", param);
            try
            {
                if (ds.Tables[3].Rows[0][0].ToString() != "")
                {
                    lblCompanyName.Visible = true;
                    Label2.Text = ds.Tables[3].Rows[0][0].ToString();
                }
            }
            catch (Exception)
            {
            }
        }
    }
    protected void linkLogin_Click(object sender, EventArgs e)
    {
        Session["BranchCode"] = DrpBranchName.SelectedValue;
        Response.Redirect("login.aspx");
    }
    protected void linkRegister_Click(object sender, EventArgs e)
    {
        Session["BranchCode"] = DrpBranchName.SelectedValue;
        Response.Redirect("register.aspx");
    }
    public override void Dispose()
    {
        _con.Dispose();
        _oo.Dispose();
        _dt.Dispose();
    }



    protected void DrpBranchName_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DrpBranchName.SelectedIndex != 0)
        {
            _sql = "select BranchCode from admissionDatePermission where BranchCode=" + DrpBranchName.SelectedValue + "";
            if (_oo.Duplicate(_sql))
            {
                _sql = "select BranchCode from admissionDatePermission where BranchCode=" + DrpBranchName.SelectedValue + " and convert(date, getdate()) between convert(date, fromdate) and convert(date, todate)";
                if (_oo.Duplicate(_sql))
                {
                    divmsg.Visible = false;
                    divRegister.Visible = true;
                    divLogin.Visible = true;
                }
                else
                {
                    divmsg.Visible = true;
                    msgbox.InnerText = "Admission process in " + DrpBranchName.SelectedItem.Text+" closed now.";
                    divRegister.Visible = false;
                    divLogin.Visible = false;
                }
            }
            else
            {
                divmsg.Visible = true;
                msgbox.InnerText = "Admission date not declared in " + DrpBranchName.SelectedItem.Text;
            }
        }
        else
        {
            divmsg.Visible = false;
            divRegister.Visible = false;
            divLogin.Visible = false;
        }
    }
}