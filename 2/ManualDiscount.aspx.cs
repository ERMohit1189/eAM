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

public partial class ManualDiscount : Page
{
    public SqlConnection _con = new SqlConnection();
    private readonly Campus _oo = new Campus();
    public SqlCommand cmd = new SqlCommand();
    private string _sql = "";
    public string surl, furl, service_provider, productinfo, key, salt, PayuBaseURL, HashSeq;
    public static bool isactive = false;
    bool Alldate = false, TuitionDiscount = false, TransportDiscount = false, HostelDiscount = false, OtherDiscount = false,
    TuitionPaid = false, TransportPaid = false, HostelPaid = false, OtherPaid = false, StrdentStatus = true;
    DataTable dtComplositFee = new DataTable();
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
        
        string _sql1 = "Select AdmissionForClassId, TypeOFAdmision, Branch From StudentOfficialDetails where SrNo='" + studentId + "' and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
        string Classid = _oo.ReturnTag(_sql1, "AdmissionForClassId"), AdmissionType = _oo.ReturnTag(_sql1, "TypeOFAdmision"), Branchid = _oo.ReturnTag(_sql1, "Branch");
        string sqls = "select Classid from FeeAllotedForClassWise where Classid=" + Classid + " and AdmissionType='" + AdmissionType + "' and Branchid=" + Branchid + " and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
        if (!_oo.Duplicate(sqls))
        {
            lnkSubmit.Visible = false;
            _oo.msgbox(Page, msgs, "Fee not allotted for this class!", "A");
        }

    }
    
    protected void lnkView_Click(object sender, EventArgs e)
    {

        var studentId = Request.Form[hfStudentId.UniqueID];
        if (string.IsNullOrEmpty(studentId))
        {
            studentId = txtSearch.Text.Trim();
        }
        BindStudentDetails();

        string _sql1 = "Select AdmissionForClassId, TypeOFAdmision, Branch From StudentOfficialDetails where SrNo='" + studentId + "' and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
        string Classid = _oo.ReturnTag(_sql1, "AdmissionForClassId"), AdmissionType = _oo.ReturnTag(_sql1, "TypeOFAdmision"), Branchid = _oo.ReturnTag(_sql1, "Branch");
        string sqls = "select Classid from FeeAllotedForClassWise where Classid=" + Classid + " and AdmissionType='" + AdmissionType + "' and Branchid=" + Branchid + " and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
        if (!_oo.Duplicate(sqls))
        {
            lnkSubmit.Visible = false;
            _oo.msgbox(Page, msgs, "Fee not allotted for this class!", "A");
        }
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
        var _sql2 = "select isnull(Withdrwal, '') Withdrwal, isnull(Promotion, '') promo from StudentOfficialDetails where srno='" + studentId + "' and SessionName='" + Session["SessionName"] + "' and branchcode=" + Session["BranchCode"] + "";
        var ds = BLL.BLLInstance.GetStudentDetails(studentId, Session["SessionName"].ToString(), Session["BranchCode"].ToString());
        hdnIspromot.Value = _oo.ReturnTag(_sql2, "promo");
        if (ds.Tables[0].Rows.Count <= 0)
        { btnPrint.Visible = false; }
        else { btnPrint.Visible = true; }

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
                        divTools.Visible = true;
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

            _sql = "Select blockedRemark from StudentOfficialDetails where srno='" + studentId + "' and SessionName='" + Session["SessionName"] + "' and branchcode=" + Session["BranchCode"] + "";
            var remark = _oo.ReturnTag(_sql, "blockedRemark");
            mess.Text = remark;   divMess.Visible = true;
            
            Campus camp = new Campus(); camp.msgbox(Page, msgs, remark, "A");
        }
        else if (_oo.ReturnTag(_sql2, "Withdrwal").Trim() != "")
        {
            grdStRecord.Rows[0].BackColor = Color.Red;
            grdStRecord.ForeColor = Color.White;

            var remark = "This Student has been Withdrawn!";
            mess.Text = remark;   divMess.Visible = true;
            
            Campus camp = new Campus(); camp.msgbox(Page, msgs, remark, "A");
        }
        else if (_oo.ReturnTag(sql1, "Promotion") == "Cancelled")
        {
            grdStRecord.Rows[0].BackColor = Color.Red;
            grdStRecord.ForeColor = Color.White;

            var remark = "Student Promotion has been cancelled, Please promote again from last session!";
            mess.Text = remark;   divMess.Visible = true;
            
            Campus camp = new Campus(); camp.msgbox(Page, msgs, remark, "A");
        }
        else
        {
            if (hdnIspromot.Value != "")
            {
                var remark = "Student promoted in the next session so you can not enter discoun in session " + Session["SessionName"].ToString() + ".";
                mess.Text = remark; divMess.Visible = true;
                divTools.Visible = false;
                lnkSubmit.Visible = false;
                grdStRecord.BackColor = Color.Red;
                grdStRecord.ForeColor = Color.Black;
            }
            else
            {
                grdStRecord.BackColor = Color.White;
                grdStRecord.ForeColor = Color.Black;
            }

        }

        if (grdStRecord.Rows.Count == 0)
        {
            Campus camp = new Campus(); camp.msgbox(Page, msgs, "Sorry, no record(s) found!", "A");
        }
        else
        {
            _sql = "Select Withdrwal From StudentOfficialDetails where SrNo='" + studentId + "' and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
            if (_oo.ReturnTag(_sql, "Withdrwal") != "")
            {
                mess.Text = "This Student has been Withdrawn!";   divMess.Visible = true;
                lnkSubmit.Visible=false;
                Campus camp = new Campus(); camp.msgbox(Page, msgs, "This Student has been Withdrawn!", "W");
            }

        }

    }

    protected void GetTutionFeeDetails(string SrNo)
    {
        rptFeeStructure.DataSource = null;
        rptFeeStructure.DataBind();
        using (SqlConnection conn = new SqlConnection())
        {
            conn.ConnectionString = BAL.objBal.dbGet_connection().ConnectionString;
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = conn;
                cmd.CommandText = "GetCompositFee";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SrNo", SrNo.ToString().Trim());
                cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"].ToString().Trim());
                cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString().Trim());
                cmd.Parameters.AddWithValue("@PMtMode", "Cash");
                cmd.Parameters.AddWithValue("@MODForFeeDeposit", "I");
                cmd.Parameters.AddWithValue("@completefeeCheck", "0");
                cmd.Parameters.AddWithValue("@Actions", "MainHead");
                SqlDataAdapter das = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                das.Fill(dt);
                cmd.Parameters.Clear();
                cmd.Connection.Close();
                if (dt.Rows.Count > 0)
                {
                    string sql = "select DiscHeadName, id  from ManualDiscountHeads where SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
                    _oo.FillDropDown_withValue(sql, ddlDiscountHead, "DiscHeadName", "id");
                    ddlDiscountHead.Items.Insert(0, new ListItem("<--Select-->", ""));
                    divTools.Visible = true;
                    divTutionFee.Visible = true;
                    rptFeeStructure.DataSource = dt;
                    rptFeeStructure.DataBind();
                    double gtotal = 0; double dueRec = 0; int stsPending = 0;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        Label lblStsMain = (Label)rptFeeStructure.Items[i].FindControl("lblStsMain");
                        CheckBox chkInstallment = (CheckBox)rptFeeStructure.Items[i].FindControl("chkInstallment");
                        Label lblhideInstallmentDue = (Label)rptFeeStructure.Items[i].FindControl("lblhideInstallmentDue");
                        Label lblrecuring = (Label)rptFeeStructure.Items[i].FindControl("lblrecuring");
                        Label lblInstallmentDiscountPaid = (Label)rptFeeStructure.Items[i].FindControl("lblInstallmentDiscountPaid");
                        Label lblInstallmentDiscount = (Label)rptFeeStructure.Items[i].FindControl("lblInstallmentDiscount");
                        TextBox txtInstallmentdiscount = (TextBox)rptFeeStructure.Items[i].FindControl("txtInstallmentdiscount");
                        TextBox txtInstallmentNarration = (TextBox)rptFeeStructure.Items[i].FindControl("txtInstallmentNarration");
                        Label HeadDiscount = (Label)rptFeeStructure.Items[i].FindControl("HeadDiscount");
                        Label lbInstallmentlPaid = (Label)rptFeeStructure.Items[i].FindControl("lbInstallmentlPaid");
                        Label lblDueDate = (Label)rptFeeStructure.Items[i].FindControl("lblDueDate");
                        Label lblhiDueHead = (Label)rptFeeStructure.Items[i].FindControl("lblhideInstallmentDue");
                        Repeater RepeaterHeadAmon = (Repeater)rptFeeStructure.Items[i].FindControl("RepeaterHeadAmon");
                        Repeater rptFee = (Repeater)rptFeeStructure.Items[i].FindControl("rptFee");
                        Repeater RepeaterHeadDiscount = (Repeater)rptFeeStructure.Items[i].FindControl("RepeaterHeadDiscount");
                        Label lblInstallmentTotal = (Label)rptFeeStructure.Items[i].FindControl("lblInstallmentTotal");
                        DateTime curdate = DateTime.Parse(_oo.ReturnTag("select getdate() curdate", "curdate"));

                        


                        dueRec = dueRec + double.Parse(dt.Rows[i]["DueAmount"].ToString());
                        lblrecuring.Text = dueRec.ToString("0.00");
                        if (double.Parse(lblInstallmentDiscount.Text) > 0)
                        {
                            HeadDiscount.Visible = true;
                        }
                        else
                        {
                            HeadDiscount.Visible = false;
                        }
                        if (double.Parse(lblInstallmentDiscount.Text == "" ? "0" : lblInstallmentDiscount.Text) > 0 || double.Parse(lblInstallmentDiscountPaid.Text == "" ? "0" : lblInstallmentDiscountPaid.Text) > 0)
                        {
                            HeadDiscount.Visible = true;
                        }
                        else
                        {
                            HeadDiscount.Visible = false;
                        }
                        
                        
                        gtotal = gtotal + double.Parse(dt.Rows[i]["DueAmount"].ToString() == "" ? "0" : dt.Rows[i]["DueAmount"].ToString());
                        rptFee.DataSource = null;
                        rptFee.DataBind();
                        using (SqlCommand cmd1 = new SqlCommand())
                        {
                            cmd1.Connection = conn;
                            cmd1.CommandText = "GetCompositFee";
                            cmd1.CommandType = CommandType.StoredProcedure;
                            cmd1.Parameters.AddWithValue("@SrNo", SrNo.ToString().Trim());
                            cmd1.Parameters.AddWithValue("@SessionName", Session["SessionName"].ToString().Trim());
                            cmd1.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString().Trim());
                            cmd1.Parameters.AddWithValue("@PMtMode", "Cash");
                            cmd1.Parameters.AddWithValue("@MODForFeeDeposit","I");
                            cmd1.Parameters.AddWithValue("@completefeeCheck", "0");
                            cmd1.Parameters.AddWithValue("@InstallmentIdSet", dt.Rows[i]["MonthId"].ToString().Trim());
                            cmd1.Parameters.AddWithValue("@Actions", "ChildHead");
                            SqlDataAdapter da = new SqlDataAdapter(cmd1);
                            DataSet ds = new DataSet();
                            da.Fill(ds);
                            cmd1.Parameters.Clear();
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                RepeaterHeadAmon.DataSource = null;
                                RepeaterHeadAmon.DataBind();
                                if (ds.Tables[2].Rows.Count > 0)
                                {
                                    RepeaterHeadAmon.DataSource = ds.Tables[2];
                                    RepeaterHeadAmon.DataBind();
                                }
                                
                                RepeaterHeadDiscount.DataSource = null;
                                RepeaterHeadDiscount.DataBind();
                                if (ds.Tables[3].Rows.Count > 0)
                                {
                                    RepeaterHeadDiscount.DataSource = ds.Tables[3];
                                    RepeaterHeadDiscount.DataBind();
                                }
                                rptFee.DataSource = ds.Tables[0];
                                rptFee.DataBind();
                                
                                for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
                                {
                                    Label lblStsChild = (Label)rptFee.Items[j].FindControl("lblStsChild");
                                    Label lblInstallmentId = (Label)rptFee.Items[j].FindControl("lblInstallmentId");
                                    Label lblFeeheadId = (Label)rptFee.Items[j].FindControl("lblFeeheadId");
                                    CheckBox chkInstallmentFee = (CheckBox)rptFee.Items[j].FindControl("chkInstallmentFee");
                                    Label lblFeeHeadBalance = (Label)rptFee.Items[j].FindControl("lblFeeHeadBalance");
                                    Label lblFeeHeadDiscountPaid = (Label)rptFee.Items[j].FindControl("lblFeeHeadDiscountPaid");
                                    Label lblFeeHeadDiscount = (Label)rptFee.Items[j].FindControl("lblFeeHeadDiscount");
                                    TextBox txtFeeHeadDiscount = (TextBox)rptFee.Items[j].FindControl("txtFeeHeadDiscount");
                                    TextBox txtFeeHeadNarration = (TextBox)rptFee.Items[j].FindControl("txtFeeHeadNarration");
                                    HtmlTableRow feeheadrow = (HtmlTableRow)rptFee.Items[j].FindControl("feeheadrow");
                                    Label ChildDiscount = (Label)rptFee.Items[j].FindControl("ChildDiscount");
                                    Label lblFeeHeadTotal = (Label)rptFee.Items[j].FindControl("lblFeeHeadTotal");
                                    Label lblFeeHeadPaid = (Label)rptFee.Items[j].FindControl("lblFeeHeadPaid");
                                    Label lblisFineFeeApply = (Label)rptFee.Items[j].FindControl("lblisFineFeeApply");
                                    LinkButton btnResetDiscount = (LinkButton)rptFee.Items[j].FindControl("btnResetDiscount");

                                    
                                    if (lblFeeHeadBalance.Text=="")
                                    {
                                        lblFeeHeadBalance.Text= "0.00";
                                    }
                                    if (double.Parse(lblFeeHeadDiscount.Text == "" ? "0" : lblFeeHeadDiscount.Text) > 0 || double.Parse(lblFeeHeadDiscountPaid.Text == "" ? "0" : lblFeeHeadDiscountPaid.Text) > 0)
                                    {
                                        ChildDiscount.Visible = true;
                                    }
                                    else
                                    {
                                        ChildDiscount.Visible = false;
                                    }
                                    
                                    if ((double.Parse(lblFeeHeadBalance.Text == "" ? "0" : lblFeeHeadBalance.Text) == 0 && double.Parse(lblFeeHeadDiscount.Text) == double.Parse(lblFeeHeadDiscountPaid.Text)))
                                    {
                                        txtFeeHeadNarration.Text = "";
                                    }
                                    string ss1 = "select receiptStatus from CompositFeeDeposit  where FeeHeadId=" + lblFeeheadId.Text + " and InstallmentId=" + lblInstallmentId.Text + " and SrNo='" + SrNo + "' and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
                                    if (_oo.ReturnTag(ss1, "receiptStatus") == "Pending" && lblFeeHeadBalance.Text == "0.00")
                                    {
                                        lblStsMain.Text = "Pending";
                                        lblStsChild.Text = "Pending";
                                        stsPending = stsPending + 1;
                                    }
                                    else if (_oo.ReturnTag(ss1, "receiptStatus") != "Pending" && lblFeeHeadBalance.Text == "0.00")
                                    {
                                        lblStsMain.Text = "Paid";
                                        lblStsChild.Text = "Paid";

                                    }
                                    Label lblFeeheadId1 = (Label)rptFee.Items[j].FindControl("lblFeeheadId");
                                    Repeater RepeaterChildDiscount = (Repeater)rptFee.Items[j].FindControl("RepeaterChildDiscount");
                                    RepeaterChildDiscount.DataSource = null;
                                    RepeaterChildDiscount.DataBind();
                                    using (SqlCommand cmd2 = new SqlCommand())
                                    {
                                        cmd2.Connection = conn;
                                        cmd2.CommandText = "GetCompositFee";
                                        cmd2.CommandType = CommandType.StoredProcedure;
                                        cmd2.Parameters.AddWithValue("@SrNo", SrNo.ToString().Trim());
                                        cmd2.Parameters.AddWithValue("@SessionName", Session["SessionName"].ToString().Trim());
                                        cmd2.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString().Trim());
                                        cmd2.Parameters.AddWithValue("@PMtMode", "Cash");
                                        cmd2.Parameters.AddWithValue("@MODForFeeDeposit", "I");
                                        cmd2.Parameters.AddWithValue("@completefeeCheck",  "0");
                                        cmd2.Parameters.AddWithValue("@InstallmentIdSet", dt.Rows[i]["MonthId"].ToString().Trim());
                                        cmd2.Parameters.AddWithValue("@Feeheadid", lblFeeheadId1.Text.Trim());
                                        cmd2.Parameters.AddWithValue("@Actions", "ChildHeadDiscount");
                                        SqlDataAdapter da1s = new SqlDataAdapter(cmd2);
                                        DataTable dt1 = new DataTable();
                                        da1s.Fill(dt1);
                                        cmd2.Parameters.Clear();
                                        if (ds.Tables[0].Rows.Count > 0)
                                        {
                                            RepeaterChildDiscount.DataSource = dt1;
                                            RepeaterChildDiscount.DataBind();
                                            for (int p = 0; p < dt1.Rows.Count; p++)
                                            {
                                                if (dt1.Rows[p]["DiscountName"].ToString() == "Manual Discount" || dt1.Rows[p]["DiscountName"].ToString() == "On Page Discount")
                                                {
                                                    string chk = "select id from CompositFeeDeposit where SrNo='"+ SrNo.ToString().Trim() + "' and SessionName='"+ Session["SessionName"] + "' and BranchCode="+ Session["BranchCode"] + " and InstallmentId="+ dt.Rows[i]["MonthId"].ToString() + " and FeeHeadId="+ lblFeeheadId1.Text.Trim() + " and receiptStatus<>'Cancelled'";
                                                    if (_oo.Duplicate(chk))
                                                    {
                                                        btnResetDiscount.Visible = false;
                                                    }
                                                    else
                                                    {
                                                        btnResetDiscount.Visible = true;
                                                    }
                                                }
                                            }
                                            if (stsPending > 0)
                                            {
                                                
                                                txtInstallmentNarration.Text = "";
                                                txtInstallmentdiscount.Text = "";
                                                txtInstallmentdiscount.Enabled = false;
                                                txtInstallmentNarration.Enabled = false;
                                                chkInstallment.Checked = false;
                                                chkInstallment.Visible = true;
                                                chkInstallment.Enabled = false;

                                                txtFeeHeadDiscount.Text = "";
                                                txtFeeHeadDiscount.Enabled = false;
                                                txtFeeHeadNarration.Text = "";
                                                txtFeeHeadNarration.Enabled = false;
                                                chkInstallmentFee.Checked = false;
                                                chkInstallmentFee.Visible = true;
                                                chkInstallmentFee.Enabled = false;
                                            }
                                            string sql1 = "select id from CompositFeeDeposit where SrNo='" + SrNo + "' and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " and InstallmentId=" + dt.Rows[i]["MonthId"].ToString().Trim() + " and isnull(receiptStatus,'')<>'Cancelled'";
                                            if (_oo.Duplicate(sql1))
                                            {
                                                txtInstallmentdiscount.Enabled = false;
                                                txtFeeHeadDiscount.Enabled = false;
                                            }
                                            else
                                            {
                                                txtInstallmentdiscount.Enabled = true;
                                                txtFeeHeadDiscount.Enabled = true;
                                            }
                                        }
                                    }
                                }
                            }
                            
                        }
                    }
                    if (stsPending > 0)
                    {
                        txtTotalPaid.Text = "0.00";
                        txtTotalPaid.Enabled = false;
                        lnkSubmit.Visible = false;
                    }
                    else
                    {
                        txtTotalPaid.Text = "0.00";
                        txtTotalPaid.Enabled = true;
                        lnkSubmit.Visible = true;
                    }
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "ChangeRowColor();", true);
                }
                else
                {
                    divTutionFee.Visible = false;
                }

            }
        }
    }
    protected void SubmitFee()
    {
        var studentId = Request.Form[hfStudentId.UniqueID];
        if (string.IsNullOrEmpty(studentId))
        {
            studentId = txtSearch.Text.Trim();
        }
        double payableAmt = double.Parse(txtTotalPaid.Text == "" ? "0" : txtTotalPaid.Text);
        if (payableAmt > 0)
        {
            int roeCount = 0;
            for (int p = 0; p < rptFeeStructure.Items.Count; p++)
            {
                Repeater rptFee = (Repeater)rptFeeStructure.Items[p].FindControl("rptFee");
                for (int i = 0; i < rptFee.Items.Count; i++)
                {
                    Label lblInstallmentId = (Label)rptFee.Items[i].FindControl("lblInstallmentId");
                    Label lblFeeheadId = (Label)rptFee.Items[i].FindControl("lblFeeheadId");
                    TextBox txtFeeHeadDiscount = (TextBox)rptFee.Items[i].FindControl("txtFeeHeadDiscount");
                    TextBox txtFeeHeadNarration = (TextBox)rptFee.Items[i].FindControl("txtFeeHeadNarration");

                    CheckBox chkInstallment = (CheckBox)rptFeeStructure.Items[p].FindControl("chkInstallment");
                    CheckBox chkInstallmentFee = (CheckBox)rptFee.Items[i].FindControl("chkInstallmentFee");
                    if (chkInstallment.Checked && chkInstallmentFee.Checked)
                    {
                        using (SqlCommand cmd = new SqlCommand())
                        {
                            cmd.CommandText = "ManualDiscountProc";
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Connection = _con;
                            cmd.Parameters.AddWithValue("@SrNo", studentId);
                            cmd.Parameters.AddWithValue("@InstallmentId", lblInstallmentId.Text); 
                            cmd.Parameters.AddWithValue("@FeeHeadId", lblFeeheadId.Text);
                            cmd.Parameters.AddWithValue("@DiscountHeadId", ddlDiscountHead.SelectedValue);
                            cmd.Parameters.AddWithValue("@Amount", (txtFeeHeadDiscount.Text == "" ? "0" : txtFeeHeadDiscount.Text));
                            cmd.Parameters.AddWithValue("@Remark", txtFeeHeadNarration.Text);
                            cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"].ToString());
                            cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
                            cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());
                            try
                            {
                                _con.Open();
                                int RowsEffected = cmd.ExecuteNonQuery();
                                cmd.Parameters.Clear();
                                _con.Close();
                                if (RowsEffected > 0)
                                {
                                    roeCount++;
                                }
                            }
                            catch (Exception ex)
                            {
                                ex.Message.ToString();
                            }
                        }
                    }
                }
            }
            if (roeCount > 0)
            {
                GetTutionFeeDetails(studentId);
                Campus camp = new Campus(); camp.msgbox(Page, msgs, "Submitted successfully.", "S");
            }
            else
            {
                Campus camp = new Campus(); camp.msgbox(Page, msgs, "Some technical problems have occurred!", "A");
            }
        }
    }
    protected void lnkSubmit_Click(object sender, EventArgs e)
    {
        if (ddlDiscountHead.SelectedIndex>0)
        {
            var studentId = Request.Form[hfStudentId.UniqueID];
            if (string.IsNullOrEmpty(studentId))
            {
                studentId = txtSearch.Text.Trim();
                return;
            }
            SubmitFee();
        }
        
    }


    protected void btnResetDiscount_Click(object sender, EventArgs e)
    {
        var studentId = Request.Form[hfStudentId.UniqueID];
        if (string.IsNullOrEmpty(studentId))
        {
            studentId = txtSearch.Text.Trim();
            return;
        }
        LinkButton lnk = (LinkButton)sender;
        Label lblInstallmentId = (Label)lnk.NamingContainer.FindControl("lblInstallmentId");
        Label lblFeeheadId = (Label)lnk.NamingContainer.FindControl("lblFeeheadId");
        using (SqlCommand cmd = new SqlCommand())
        {
            cmd.CommandText = "delete from ManualDiscount where SrNo = '" + studentId + "' and SessionName = '"+ Session["SessionName"] + "' and BranchCode = "+ Session["BranchCode"] + " and InstallmentId = "+ lblInstallmentId.Text.Trim() + " and FeeHeadId = "+ lblFeeheadId.Text.Trim() + "";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = _con;
            try
            {
                _con.Open();
                int RowsEffected = cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                _con.Close();
                Campus camp = new Campus(); camp.msgbox(Page, Div1, "Submitted successfully.", "S");
                GetTutionFeeDetails(studentId);
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }
    }
}