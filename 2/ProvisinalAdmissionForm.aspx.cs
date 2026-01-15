using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;
using c4SmsNew;
using Layered_TimeTable;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Security.Cryptography;


public partial class ProvisinalAdmissionForm : Page
{
    public string surl, furl, service_provider, productinfo, key, salt, PayuBaseURL, HashSeq;
    public static bool isactive = false;
    private SqlConnection _con = new SqlConnection();
    private readonly Campus _oo = new Campus();
    private string _sql = string.Empty;
    protected void Page_PreInIt(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(Request.QueryString["check"]))
        {
            if (Session["Logintype"] == null) { Response.Redirect("~/default.aspx"); }
            switch (Session["Logintype"].ToString())
            {
                case "Admin":
                    MasterPageFile = "~/Master/admin_root-manager.master";
                    break;
                case "Guardian":
                    MasterPageFile = "~/sp/sp_root-manager.master";
                    break;
            }
        }
        else
        {
            if (Session["mobilenochk"] == null) { Response.Redirect("../ap/default.aspx"); }
            MasterPageFile = "~/ap/main-ap.master";
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        Campus camp = new Campus(); camp.LoadLoader(loader);
        BLL.BLLInstance.LoadHeader("Report", header);
        txtSearch.Focus();
        _con = _oo.dbGet_connection();
        if (Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
        {
            Response.Redirect("~/default.aspx");
        }
        if (!IsPostBack)
        {
            txtFromDate.Text = DateTime.Today.ToString("dd-MMM-yyyy");
            txtToDate.Text = DateTime.Today.ToString("dd-MMM-yyyy");
            GetSessionClass();
        }
    }
    public override void VerifyRenderingInServerForm(Control control)
    {

    }
    protected void txtSearch_TextChanged(object sender, EventArgs e)
    {
        var studentId = Request.Form[hfStudentId.UniqueID];
        if (string.IsNullOrEmpty(studentId))
        {
            studentId = txtSearch.Text.Trim();
        }
        BindStudentDetails();
    }

    protected void lnkView_Click(object sender, EventArgs e)
    {
        var studentId = Request.Form[hfStudentId.UniqueID];
        if (string.IsNullOrEmpty(studentId))
        {
            studentId = txtSearch.Text.Trim();
        }
        BindStudentDetails();
    }
    public void BindStudentDetails()
    {

        mess.Text = ""; divMess.Visible = false;
        var studentId = Request.Form[hfStudentId.UniqueID];
        if (string.IsNullOrEmpty(studentId))
        {
            studentId = txtSearch.Text.Trim();
        }
        var ds = BLL.BLLInstance.GetStudentDetails(studentId, Session["SessionName"].ToString(), Session["BranchCode"].ToString());
        grdStRecord.DataSource = ds;
        grdStRecord.DataBind();

        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = BAL.objBal.dbGet_connection().ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "USP_StudentsPhotoReport";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@sessionName", Session["SessionName"].ToString().Trim());
                    cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString().Trim());
                    cmd.Parameters.AddWithValue("@SrNo", studentId.ToString().Trim());

                    cmd.Parameters.AddWithValue("@action", "details");
                    SqlDataAdapter das = new SqlDataAdapter(cmd);
                    DataSet dsPhoto = new DataSet();
                    das.Fill(dsPhoto);
                    cmd.Parameters.Clear();

                    if (dsPhoto.Tables[0].Rows.Count > 0)
                    {
                        divStudent.Visible = true;
                        img.ImageUrl = dsPhoto.Tables[0].Rows[0]["PhotoPath"].ToString() != string.Empty ? dsPhoto.Tables[0].Rows[0]["PhotoPath"] + "?value=" + DateTime.Now.ToString(CultureInfo.InvariantCulture) : "../img/user-pic/user-pic.jpg" + "?value=" + DateTime.Now.ToString(CultureInfo.InvariantCulture);
                        studentImg.NavigateUrl = dsPhoto.Tables[0].Rows[0]["PhotoPath"].ToString() != string.Empty ? dsPhoto.Tables[0].Rows[0]["PhotoPath"] + "?value=" + DateTime.Now.ToString(CultureInfo.InvariantCulture) : "../img/user-pic/user-pic.jpg" + "?value=" + DateTime.Now.ToString(CultureInfo.InvariantCulture);
                        hylinkmoredetails.NavigateUrl = "../11/StudentRegView.aspx?print=1&id=" + ds.Tables[0].Rows[0]["stenrcode"];
                    }
                }
            }
        }
    }
    private void GetSessionClass()
    {
        try
        {
            _sql = "select SessionName from SessionMaster where BranchCode=" + Session["BranchCode"].ToString() + "";
            _oo.FillDropDownWithOutSelect(_sql, drpSession, "SessionName");
            _sql = "select top(1) SessionName from SessionMaster where BranchCode=" + Session["BranchCode"] + " and DATEDIFF(day, FromDate, GETDATE())>200 order by SessionId desc";
            if (_oo.Duplicate(_sql))
            {
                drpSession.SelectedIndex = (drpSession.Items.Count - 1);
            }
            else
            {
                try
                {
                    _sql = "select top(1) SessionName from SessionMaster where BranchCode=" + Session["BranchCode"] + " and SessionId <>";
                    _sql = _sql + "(select top(1) SessionId from SessionMaster where BranchCode=" + Session["BranchCode"] + " order by SessionId desc) order by SessionId desc";
                    drpSession.SelectedValue = _oo.ReturnTag(_sql, "SessionName");
                }
                catch (Exception ex)
                {

                    drpSession.SelectedIndex = (drpSession.Items.Count - 1);
                }

            }
            drpSession.Items.Insert(0, new ListItem("<-- Select -->", "<-- Select -->"));
        }
        catch (Exception)
        {
            // ignored
        }
    }


    protected void Submit_Click(object sender, EventArgs e)
    {
        var studentId = Request.Form[hfStudentId.UniqueID];
        if (string.IsNullOrEmpty(studentId))
        {
            studentId = txtSearch.Text.Trim();
        }
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "ProvisionalFormProc";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Srno", studentId);
        cmd.Parameters.AddWithValue("@AdmissionClass", txtAdmissionClass.Text.Trim());
        cmd.Parameters.AddWithValue("@WhichSessionName", drpSession.SelectedValue);
        cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"]);
        cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"]);
        cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"]);
        cmd.Parameters.AddWithValue("@Action", "insert");
        cmd.Connection = _con;
        try
        {
            _con.Open();
            cmd.ExecuteNonQuery();
            _con.Close();
            Display(studentId);
            _oo.MessageBox("Submited successfully", Page);
            ScriptManager.RegisterClientScriptBlock(Page, GetType(), "redirect", "window.open('../2/ProvisionalFormPrint.aspx?print=" + studentId + "$" + Session["SessionName"] + "','_blank')", true);
        }
        catch (Exception)
        {
            // ignored
        }
    }


    protected void LinkView_Click(object sender, EventArgs e)
    {
        Display("");
    }
    public void Display(string srno="")
    {
        using (SqlConnection conn = new SqlConnection())
        {
            conn.ConnectionString = BAL.objBal.dbGet_connection().ConnectionString;
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = conn;
                cmd.CommandText = "ProvisionalFormProc";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@sessionName", Session["SessionName"].ToString().Trim());
                cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString().Trim());
                if (srno != "")
                {
                    cmd.Parameters.AddWithValue("@SrNo", srno);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@FromDate", txtFromDate.Text);
                    cmd.Parameters.AddWithValue("@ToDate", txtToDate.Text);
                }
                cmd.Parameters.AddWithValue("@Action", "select");
                SqlDataAdapter das = new SqlDataAdapter(cmd);
                DataTable dtlist = new DataTable();
                das.Fill(dtlist);
                cmd.Parameters.Clear();

                if (dtlist.Rows.Count > 0)
                {
                    string fromdate = txtFromDate.Text;
                    string todate = txtToDate.Text;
                    heading.Text = "Provisional Form Report from " + fromdate + " to " + todate + "";
                    divdatabind.Visible = true;
                    Grd.DataSource = dtlist;
                    Grd.DataBind();
                }
            }
        }
    }

    protected void lnkPrintAF_Click(object sender, EventArgs e)
    {
        LinkButton chk = (LinkButton)sender;
        Label curSession = (Label)chk.NamingContainer.FindControl("curSession");
        Label Labelid = (Label)chk.NamingContainer.FindControl("Srno");
        string ss = Labelid.Text;
        ScriptManager.RegisterClientScriptBlock(Page, GetType(), "redirect", "window.open('../2/ProvisionalFormPrint.aspx?print=" + ss + "$"+ curSession.Text + "','_blank')", true);
    }

    protected void ImageButton1_Click(object sender, EventArgs e)
    {
        _oo.ExportTolandscapeWord(Response, "ProvisinalAdmissionForm", gdv1);
    }
    protected void ImageButton2_Click(object sender, EventArgs e)
    {
        _oo.ExportDivToExcelWithFormatting(Response, "ProvisinalAdmissionForm.xls", gdv1, Server.MapPath("~/Admin/css/style.css"));
    }
    protected void ImageButton3_Click(object sender, EventArgs e)
    {
        _oo.ExporttolandscapePdf(Response, "ProvisinalAdmissionForm", abc);
    }
    protected void ImageButton4_Click(object sender, EventArgs e)
    {
        PrintHelper_New.ctrl = abc;
        ClientScript.RegisterStartupScript(GetType(), "onclick", "<script language=javascript>var winpop=window.open('Print_New.aspx','_blank'); if(!winpop || winpop.closed){alert('Please allow pop-up blocker from browser settings!')}</script>");
    }
    public override void Dispose()
    {
        _con.Dispose();
        _oo.Dispose();
    }

}