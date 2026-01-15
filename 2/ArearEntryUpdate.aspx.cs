using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Threading;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using c4SmsNew;
using System.Security.Cryptography;
using System.Text;
using System.Collections;
using System.Net;

public partial class ArearEntryUpdate : Page
{
    public SqlConnection _con = new SqlConnection();
    private readonly Campus _oo = new Campus();
    public SqlCommand cmd = new SqlCommand();
    private string _sql = "";

    public void MakeConnection()
    {
        _con = new SqlConnection();
        try
        {
            cmd = new SqlCommand();
            _con = _oo.dbGet_connection();
            _con.Open();
        }
        catch { }
    }
    protected void Page_PreInIt(object sender, EventArgs e)
    {
        if (Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null || Session["Logintype"] == null)
        {
            Response.Redirect("~/default.aspx");
        }
        switch (Session["Logintype"].ToString())
        {
            case "Admin":
                MasterPageFile = "~/Master/admin_root-manager.master";
                break;
            case "Guardian":
                MasterPageFile = "~/sp/sp_root-manager.master";
                break;
            case "Student":
                MasterPageFile = "~/13/stuRootManager.master";
                break;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null || Session["Logintype"] == null)
        {
            Response.Redirect("~/default.aspx");
        }
        _con = _oo.dbGet_connection();

        Campus camp = new Campus(); camp.LoadLoader(loader);
        if (!IsPostBack)
        {
            txtSearch.Focus();
        }

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
        if (Session["Logintype"].ToString() == "Guardian" || Session["Logintype"].ToString() == "Student")
        {
            studentId = Session["Srno"].ToString();
        }
        _sql = "Select * from StudentOfficialDetails where blocked='Yes' and srno='" + studentId + "' and SessionName='" + Session["SessionName"] + "' and branchcode=" + Session["BranchCode"] + "";
        var sql1 = "Select Promotion,MODForFeeDeposit  from StudentOfficialDetails where srno='" + studentId + "' and SessionName='" + Session["SessionName"] + "' and branchcode=" + Session["BranchCode"] + "";
        var _sql2 = "select isnull(Withdrwal, '') Withdrwal from StudentOfficialDetails where srno='" + studentId + "' and SessionName='" + Session["SessionName"] + "' and branchcode=" + Session["BranchCode"] + "  and isnull(TypeOFAdmision,'')='Old' ";
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
                        if (Session["Logintype"].ToString() == "Guardian" || Session["Logintype"].ToString() == "Student")
                        {
                            studentId = Session["Srno"].ToString();
                        }
                        divStudent.Visible = true;
                        img.ImageUrl = dsPhoto.Tables[0].Rows[0]["PhotoPath"].ToString() != string.Empty ? dsPhoto.Tables[0].Rows[0]["PhotoPath"] + "?value=" + DateTime.Now.ToString(CultureInfo.InvariantCulture) : "../img/user-pic/user-pic.jpg" + "?value=" + DateTime.Now.ToString(CultureInfo.InvariantCulture);
                        studentImg.NavigateUrl = dsPhoto.Tables[0].Rows[0]["PhotoPath"].ToString() != string.Empty ? dsPhoto.Tables[0].Rows[0]["PhotoPath"] + "?value=" + DateTime.Now.ToString(CultureInfo.InvariantCulture) : "../img/user-pic/user-pic.jpg" + "?value=" + DateTime.Now.ToString(CultureInfo.InvariantCulture);
                        hylinkmoredetails.NavigateUrl = "../11/StudentRegView.aspx?print=1&id=" + ds.Tables[0].Rows[0]["stenrcode"];
                        GetTutionFeeDetails(studentId);
                    }
                }
            }
        }

        if (_oo.Duplicate(_sql))
        {
            grdStRecord.Rows[0].BackColor = Color.Red;
            grdStRecord.ForeColor = Color.White;

            _sql = "Select blockedRemark from StudentOfficialDetails where srno='" + studentId + "' and SessionName='" + Session["SessionName"] + "' and branchcode=" + Session["BranchCode"] + " and isnull(TypeOFAdmision,'')='Old' ";
            var remark = _oo.ReturnTag(_sql, "blockedRemark");
            mess.Text = remark; divMess.Visible = true;

            Campus camp = new Campus(); camp.msgbox(Page, msgbox, remark, "A");
        }
        else if (_oo.ReturnTag(_sql2, "Withdrwal").Trim() != "")
        {
            grdStRecord.Rows[0].BackColor = Color.Red;
            grdStRecord.ForeColor = Color.White;

            var remark = "This Student has been Withdrawn!";
            mess.Text = remark; divMess.Visible = true;

            Campus camp = new Campus(); camp.msgbox(Page, msgbox, remark, "A");
        }
        else if (_oo.ReturnTag(sql1, "Promotion") == "Cancelled")
        {
            grdStRecord.Rows[0].BackColor = Color.Red;
            grdStRecord.ForeColor = Color.White;

            var remark = "Student Promotion has been cancelled, Please promote again from last session!";
            mess.Text = remark; divMess.Visible = true;

            Campus camp = new Campus(); camp.msgbox(Page, msgbox, remark, "A");
        }
        else
        {
            string sql = "Select Top 1 SessionName from SessionMaster Order by SessionId Desc";
            if (_oo.ReturnTag(sql, "SessionName") == Session["SessionName"].ToString())
            {
                grdStRecord.BackColor = Color.White;
                grdStRecord.ForeColor = Color.Black;
            }
            else
            {
                sql = "Select SessionName from StudentOfficialDetails where srno='" + studentId + "' and SessionName=(Select Top 1 SessionName from SessionMaster Order by SessionId Desc) and Promotion is null  and isnull(TypeOFAdmision,'')='Old' ";

                if (_oo.Duplicate(sql))
                {
                    var remark = "Student promoted to session " + _oo.ReturnTag(sql, "SessionName") + " so you can not take fee in session " + Session["SessionName"].ToString() + "!";
                    mess.Text = remark; divMess.Visible = true;

                    Campus camp = new Campus(); camp.msgbox(Page, msgbox, remark, "A");
                }
                else
                {
                    grdStRecord.BackColor = Color.White;
                    grdStRecord.ForeColor = Color.Black;
                }
            }

        }

        if (grdStRecord.Rows.Count == 0)
        {
            Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Sorry, no record(s) found!", "A");
        }
        else
        {
            _sql = "Select Withdrwal From StudentOfficialDetails where SrNo='" + studentId + "' and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "  and isnull(TypeOFAdmision,'')='Old' ";
            if (_oo.ReturnTag(_sql, "Withdrwal") != "")
            {
                mess.Text = "This Student has been Withdrawn!"; divMess.Visible = true;

                Campus camp = new Campus(); camp.msgbox(Page, msgbox, "This Student has been Withdrawn!", "W");
            }

        }

    }

    protected void GetTutionFeeDetails(string SrNo)
    {
        string ss = "select * from CompositFeeDeposit fd inner join feeheadmaster fm on fm.id=fd.feeHeadid and fm.branchcode=fd.branchcode where fd.SessionName='" + Session["SessionName"] + "' and fd.BranchCode=" + Session["BranchCode"] + " and fd.SrNo='" + SrNo.ToString().Trim() + "' and fd.installmentid=0 and isnull(fd.receiptStatus,'') not in ('Paid', 'Pending', 'Cancelled') and isnull(ReceiptNo,'')=''";
        rptFeeStructure.DataSource = _oo.Fetchdata(ss);
        rptFeeStructure.DataBind();
        if (rptFeeStructure.Items.Count>0)
        {
            divTutionFee.Visible = true;
        }
        else
        {
            divTutionFee.Visible = false;
            Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Arrear Fee submit complete or partial!.", "A");
        }
    }
    protected void lnkSubmit_Click(object sender, EventArgs e)
    {
        var studentId = Request.Form[hfStudentId.UniqueID];
        if (string.IsNullOrEmpty(studentId))
        {
            studentId = txtSearch.Text.Trim();
        }
        int sts = 0;
        for (int i = 0; i < rptFeeStructure.Items.Count; i++)
        {
            Label FeeHeadId = (Label)rptFeeStructure.Items[i].FindControl("lblFeeHeadId");
            TextBox Amount = (TextBox)rptFeeStructure.Items[i].FindControl("txtAmount");
            if (double.Parse(Amount.Text) > 0)
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "updateArrearEntry";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = _con;
                    cmd.Parameters.AddWithValue("@SrNo", studentId);
                    cmd.Parameters.AddWithValue("@FeeHeadId", FeeHeadId.Text);
                    cmd.Parameters.AddWithValue("@HeadAmount", Amount.Text);
                    cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"]);
                    cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"]);
                    cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"]);
                    try
                    {
                        _con.Open();
                        int RowsEffected = cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();
                        sts = sts + 1;
                        _con.Close();
                    }
                    catch (Exception ex)
                    {
                        ex.Message.ToString();
                        // ignored
                    }
                }
            }
        }
        if (sts>0)
        {
            Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Submitted successfully.", "S");
        }
    }
}