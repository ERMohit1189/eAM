using c4SmsNew;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class CompositFeeDeposit : Page
{
    public SqlConnection _con = new SqlConnection();
    private readonly Campus _oo = new Campus();
    public SqlCommand cmd = new SqlCommand();
    private string _sql = "";
    public string surl, furl, service_provider, productinfo, key, salt, PayuBaseURL, HashSeq;
    public static bool isactive = false;
    bool Alldate = false, TuitionDiscount = false, TransportDiscount = false, HostelDiscount = false, OtherDiscount = false,
    TuitionPaid = false, TransportPaid = false, HostelPaid = false, OtherPaid = false, StrdentStatus = true;
    string txtremarks = "1";
    DataTable dtComplositFee = new DataTable();
    static readonly DateTime lastDateTime;
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
        if (Session["Logintype"].ToString() != "Admin")
        {
            Response.Redirect("~/default.aspx");
        }
        MasterPageFile = "~/Master/admin_root-manager.master";
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
            string sql1 = "select CollegeId from CollegeMaster where BranchCode=" + Session["BranchCode"] + " and isWebHook='Manual' ";
            if (_oo.Duplicate(sql1))
            {
                reInitiate.Visible = true;
            }

            txtDepositDate.Text = _oo.ReturnTag("select format(getdate(),'dd-MMM-yyyy')dateCur", "dateCur");
            txtChequeDate.Text = _oo.ReturnTag("select format(getdate(),'dd-MMM-yyyy')dateCur", "dateCur");
            DropDownMOD.Visible = true;
            studentdivnotshow.Visible = false;
            txtSearch.Focus();


            string _sql2 = "select top(1) StartType from ReceiptNoStart where BranchCode=" + Session["BranchCode"] + " order by id desc";
            if (!_oo.Duplicate(_sql2))
            {
                studentdivnotshow.Visible = false;
                msgbox.InnerHtml = "Please initialize receipt no!";
                msgbox.Attributes.Add("class", "text-danger");
            }
            else
            {
                if (Session["Logintype"].ToString() == "Guardian" || Session["Logintype"].ToString() == "Student")
                {
                    studentdivnotshow.Visible = false;
                }
                else
                {
                    studentdivnotshow.Visible = true;
                }
                msgbox.InnerHtml = "";
                msgbox.Attributes.Add("class", "");
            }

        }

    }

    protected void txtSearch_TextChanged(object sender, EventArgs e)
    {
        DropDownMOD.SelectedValue = "Cash";
        DataTable dtp = new DataTable();
        using (SqlCommand cmd = new SqlCommand())
        {
            try
            {
                cmd.CommandText = "FeePermissionSettingProc";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = _con;
                cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"].ToString());
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dtp);
                if (dtp.Rows.Count > 0)
                {
                    txtDepositDate.Enabled = (int.Parse(dtp.Rows[0]["Alldate"].ToString()) > 0 ? true : false);
                    hdnRemark.Text = (dtp.Rows[0]["TuitionDiscount"].ToString() == "0" ? "0" : "1");
                    TuitionDiscount = (dtp.Rows[0]["TuitionDiscount"].ToString() == "0" ? false : true);
                    hdnDiscountText.Text = (int.Parse(dtp.Rows[0]["TuitionDiscount"].ToString()) > 0 ? "1" : "0");
                    hdnPaidText.Text = (dtp.Rows[0]["TuitionPaid"].ToString() == "0" ? "0" : "1");
                    TuitionPaid = (dtp.Rows[0]["TuitionPaid"].ToString() == "0" ? false : true);

                    txtTotalPaid.Enabled = (dtp.Rows[0]["TuitionPaid"].ToString() == "0" ? false : true);
                }
            }
            catch (Exception ex)
            {
            }
        }
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
            divTutionFee.Visible = false;
            divTools.Visible = false;
            lnkSubmit.Visible = false;
            _oo.msgbox(Page, msgs, "Fee not allotted for this class!", "A");
        }
    }

    protected void lnkView_Click(object sender, EventArgs e)
    {
        DropDownMOD.SelectedValue = "Cash";
        DataTable dtp = new DataTable();
        using (SqlCommand cmd = new SqlCommand())
        {
            try
            {
                cmd.CommandText = "FeePermissionSettingProc";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = _con;
                cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"].ToString());
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dtp);
                if (dtp.Rows.Count > 0)
                {
                    txtDepositDate.Enabled = (int.Parse(dtp.Rows[0]["Alldate"].ToString()) > 0 ? true : false);
                    hdnRemark.Text = (dtp.Rows[0]["TuitionDiscount"].ToString() == "0" ? "0" : "1");
                    TuitionDiscount = (dtp.Rows[0]["TuitionDiscount"].ToString() == "0" ? false : true);
                    hdnDiscountText.Text = (int.Parse(dtp.Rows[0]["TuitionDiscount"].ToString()) > 0 ? "1" : "0");
                    hdnPaidText.Text = (dtp.Rows[0]["TuitionPaid"].ToString() == "0" ? "0" : "1");
                    TuitionPaid = (dtp.Rows[0]["TuitionPaid"].ToString() == "0" ? false : true);
                    txtTotalPaid.Enabled = (dtp.Rows[0]["TuitionPaid"].ToString() == "0" ? false : true);
                }
            }
            catch (Exception ex)
            {
            }
        }
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
            divTutionFee.Visible = false;
            divTools.Visible = false;
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
                        divStudent.Visible = true;
                        divTools.Visible = true;
                        img.ImageUrl = dsPhoto.Tables[0].Rows[0]["PhotoPath"].ToString() != string.Empty ? dsPhoto.Tables[0].Rows[0]["PhotoPath"] + "?value=" + DateTime.Now.ToString(CultureInfo.InvariantCulture) : "../img/user-pic/user-pic.jpg" + "?value=" + DateTime.Now.ToString(CultureInfo.InvariantCulture);
                        studentImg.NavigateUrl = dsPhoto.Tables[0].Rows[0]["PhotoPath"].ToString() != string.Empty ? dsPhoto.Tables[0].Rows[0]["PhotoPath"] + "?value=" + DateTime.Now.ToString(CultureInfo.InvariantCulture) : "../img/user-pic/user-pic.jpg" + "?value=" + DateTime.Now.ToString(CultureInfo.InvariantCulture);
                        hylinkmoredetails.NavigateUrl = "../11/StudentRegView.aspx?print=1&id=" + ds.Tables[0].Rows[0]["stenrcode"];
                        string ss = "select count(*) cnt from CompositFeeDeposit where SrNo='" + studentId + "' and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " and receiptStatus='Paid'";
                        if (int.Parse(_oo.ReturnTag(ss, "cnt")) > 0)
                        {
                            chkCompleteFee.Visible = false;
                            chkCompleteFee.Checked = false;
                            chkCompleteFee.Attributes.Add("readonly", "readonly");
                        }
                        else
                        {
                            chkCompleteFee.Visible = true;
                            chkCompleteFee.Checked = false;
                            chkCompleteFee.Enabled = true;
                        }
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
            mess.Text = remark; divMess.Visible = true;
            //divTutionFee.Visible = false;
            divTools.Visible = false;
            lnkSubmit.Visible = false;
            Campus camp = new Campus(); camp.msgbox(Page, msgbox, remark, "A");
        }
        else if (_oo.ReturnTag(_sql2, "Withdrwal").Trim() != "")
        {
            grdStRecord.Rows[0].BackColor = Color.Red;
            grdStRecord.ForeColor = Color.White;

            var remark = "This Student has been Withdrawn!";
            mess.Text = remark; divMess.Visible = true;
            //divTutionFee.Visible = false;
            divTools.Visible = false;
            lnkSubmit.Visible = false;

            Campus camp = new Campus(); camp.msgbox(Page, msgbox, remark, "A");
        }
        else if (_oo.ReturnTag(sql1, "Promotion") == "Cancelled")
        {
            grdStRecord.Rows[0].BackColor = Color.Red;
            grdStRecord.ForeColor = Color.White;

            var remark = "Student Promotion has been cancelled, Please promote again from last session!";
            mess.Text = remark; divMess.Visible = true;
            //divTutionFee.Visible = false;
            divTools.Visible = false;
            lnkSubmit.Visible = false;
            Campus camp = new Campus(); camp.msgbox(Page, msgbox, remark, "A");
        }
        else
        {
            if (hdnIspromot.Value != "")
            {
                var remark = "Student promoted in the next session so you can not take fee in session " + Session["SessionName"].ToString() + ".";
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
            Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Sorry, no record(s) found!", "A");
            divTutionFee.Visible = false;
            divTools.Visible = false;
            lnkSubmit.Visible = false;
        }
        else
        {
            _sql = "Select Withdrwal From StudentOfficialDetails where SrNo='" + studentId + "' and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
            if (_oo.ReturnTag(_sql, "Withdrwal") != "")
            {
                mess.Text = "This Student has been Withdrawn!"; divMess.Visible = true;
                //divTutionFee.Visible = false;
                divTools.Visible = false;
                lnkSubmit.Visible = false;
                Campus camp = new Campus(); camp.msgbox(Page, msgbox, "This Student has been Withdrawn!", "W");
            }
        }
    }

    protected void GetTutionFeeDetails(string SrNo)
    {
        try
        {
            int isMinus = 0;
            rptFeeStructure.DataSource = null;
            rptFeeStructure.DataBind();
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = BAL.objBal.dbGet_connection().ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {
                    string ss = "select id from CompositFeeDeposit where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " and SrNo='" + SrNo.ToString().Trim() + "' and CompleteFeeeDiscount>0";
                    if (_oo.Duplicate(ss))
                    {
                        chkCompleteFee.Checked = true;
                    }
                    cmd.Connection = conn;
                    cmd.CommandText = "GetCompositFee";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@SrNo", SrNo.ToString().Trim());
                    cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"].ToString().Trim());
                    cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString().Trim());
                    cmd.Parameters.AddWithValue("@PMtMode", DropDownMOD.SelectedValue);
                    cmd.Parameters.AddWithValue("@MODForFeeDeposit", chkCompleteFee.Checked == true ? "A" : "I");
                    cmd.Parameters.AddWithValue("@completefeeCheck", (chkCompleteFee.Checked ? "1" : "0"));
                    cmd.Parameters.AddWithValue("@Actions", "MainHead");
                    SqlDataAdapter das = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    das.Fill(dt);
                    cmd.Parameters.Clear();
                    cmd.Connection.Close();
                    if (dt.Rows.Count > 0)
                    {
                        divTools.Visible = true;
                        divTutionFee.Visible = true;
                        rptFeeStructure.DataSource = dt;
                        rptFeeStructure.DataBind();
                        double gtotal = 0; double dueRec = 0; int stsPending = 0;
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            Label lblhideInstallmentDue = (Label)rptFeeStructure.Items[i].FindControl("lblhideInstallmentDue");
                            Label lblStsMain = (Label)rptFeeStructure.Items[i].FindControl("lblStsMain");
                            Label lblInstallmentDue = (Label)rptFeeStructure.Items[i].FindControl("lblInstallmentDue");
                            Label lblrecuring = (Label)rptFeeStructure.Items[i].FindControl("lblrecuring");
                            Label lblInstallmentDiscountPaid = (Label)rptFeeStructure.Items[i].FindControl("lblInstallmentDiscountPaid");
                            Label lblInstallmentDiscount = (Label)rptFeeStructure.Items[i].FindControl("lblInstallmentDiscount");
                            TextBox txtInstallmentPayable = (TextBox)rptFeeStructure.Items[i].FindControl("txtInstallmentPayable");
                            TextBox txtInstallmentdiscount = (TextBox)rptFeeStructure.Items[i].FindControl("txtInstallmentdiscount");
                            TextBox txtInstallmentNarration = (TextBox)rptFeeStructure.Items[i].FindControl("txtInstallmentNarration");
                            Label HeadDiscount = (Label)rptFeeStructure.Items[i].FindControl("HeadDiscount");
                            Label lbInstallmentlPaid = (Label)rptFeeStructure.Items[i].FindControl("lbInstallmentlPaid");
                            Label lblDueDate = (Label)rptFeeStructure.Items[i].FindControl("lblDueDate");
                            Label lblhiDueHead = (Label)rptFeeStructure.Items[i].FindControl("lblhideInstallmentDue");
                            Repeater RepeaterHeadAmon = (Repeater)rptFeeStructure.Items[i].FindControl("RepeaterHeadAmon");
                            Repeater rptFee = (Repeater)rptFeeStructure.Items[i].FindControl("rptFee");
                            CheckBox chkInstallment = (CheckBox)rptFeeStructure.Items[i].FindControl("chkInstallment");
                            Repeater RepeaterHeadDiscount = (Repeater)rptFeeStructure.Items[i].FindControl("RepeaterHeadDiscount");
                            Label lblInstallmentTotal = (Label)rptFeeStructure.Items[i].FindControl("lblInstallmentTotal");
                            DateTime curdate = DateTime.Parse(_oo.ReturnTag("select getdate() curdate", "curdate"));
                            Label lblInstallmentAmount = (Label)rptFeeStructure.Items[i].FindControl("lblInstallmentAmount");



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
                            if (double.Parse(lblhideInstallmentDue.Text) > 0 && DateTime.Parse(lblDueDate.Text) < DateTime.Parse(curdate.ToString("dd-MMM-yyyy")))
                            {
                                txtInstallmentPayable.Text = dt.Rows[i]["DueAmount"].ToString();
                                lblInstallmentDue.Text = "0.00";
                                chkInstallment.Checked = true;

                            }
                            if (double.Parse(lblInstallmentDiscount.Text) > double.Parse(lblInstallmentDiscountPaid.Text) && double.Parse(lblhideInstallmentDue.Text) == 0 && DateTime.Parse(lblDueDate.Text) < DateTime.Parse(curdate.ToString("dd-MMM-yyyy")))
                            {
                                txtInstallmentPayable.Text = dt.Rows[i]["DueAmount"].ToString();
                                lblInstallmentDue.Text = "0.00";
                                chkInstallment.Checked = true;
                            }
                            else if ((double.Parse(lblhideInstallmentDue.Text) > 0 || double.Parse(lblInstallmentDiscount.Text) > double.Parse(lblInstallmentDiscountPaid.Text)) && DateTime.Parse(lblDueDate.Text) > DateTime.Parse(curdate.ToString("dd-MMM-yyyy")))
                            {
                                txtInstallmentPayable.Text = "";
                                lblInstallmentDue.Text = dt.Rows[i]["DueAmount"].ToString();
                                chkInstallment.Checked = false;
                                txtInstallmentdiscount.Attributes.Add("readonly", "readonly");
                                txtInstallmentPayable.Attributes.Add("readonly", "readonly");
                                txtInstallmentNarration.Attributes.Add("readonly", "readonly");
                            }
                            else if (double.Parse(lblInstallmentTotal.Text) == double.Parse(lbInstallmentlPaid.Text) && double.Parse(lblhideInstallmentDue.Text) == 0)
                            {
                                txtInstallmentdiscount.Text = "";
                                txtInstallmentPayable.Text = "";
                                txtInstallmentNarration.Text = "";
                                lblInstallmentDue.Text = "0.00";
                                txtInstallmentdiscount.Attributes.Add("readonly", "readonly");
                                txtInstallmentPayable.Attributes.Add("readonly", "readonly");
                                txtInstallmentNarration.Attributes.Add("readonly", "readonly");
                                chkInstallment.Checked = false;
                                chkInstallment.Visible = false;
                            }
                            if ((double.Parse(lblhideInstallmentDue.Text) == 0 && double.Parse(lblInstallmentDiscount.Text) >= double.Parse(lblInstallmentDiscountPaid.Text)))
                            {
                                txtInstallmentdiscount.Attributes.Add("readonly", "readonly");
                                txtInstallmentPayable.Attributes.Add("readonly", "readonly");
                                txtInstallmentNarration.Attributes.Add("readonly", "readonly");
                            }
                            if ((double.Parse(lblhideInstallmentDue.Text) == 0 && double.Parse(lblInstallmentDiscount.Text) > double.Parse(lblInstallmentDiscountPaid.Text)))
                            {
                                chkInstallment.Checked = true;
                                chkInstallment.Visible = true;
                            }
                            if ((double.Parse(lblhideInstallmentDue.Text) == 0 && (double.Parse(lblInstallmentDiscount.Text) + double.Parse(lbInstallmentlPaid.Text)) == double.Parse(lblInstallmentAmount.Text)))
                            {
                                chkInstallment.Checked = false;
                                chkInstallment.Visible = false;
                            }
                            if ((double.Parse(lblhideInstallmentDue.Text) == 0 && (double.Parse(lblInstallmentDiscount.Text) + double.Parse(lbInstallmentlPaid.Text)) == double.Parse(lblInstallmentAmount.Text) && double.Parse(lbInstallmentlPaid.Text) == 0))
                            {
                                chkInstallment.Checked = true;
                                chkInstallment.Visible = true;
                            }
                            if (double.Parse(lblInstallmentDiscountPaid.Text) == double.Parse(lblInstallmentAmount.Text) && double.Parse(lbInstallmentlPaid.Text) == 0)
                            {
                                txtInstallmentdiscount.Text = "";
                                txtInstallmentPayable.Text = "";
                                txtInstallmentNarration.Text = "";
                                lblInstallmentDue.Text = "0.00";
                                txtInstallmentdiscount.Attributes.Add("readonly", "readonly");
                                txtInstallmentPayable.Attributes.Add("readonly", "readonly");
                                txtInstallmentNarration.Attributes.Add("readonly", "readonly");
                                chkInstallment.Checked = false;
                                chkInstallment.Visible = false;
                            }
                            if ((double.Parse(lblInstallmentDiscountPaid.Text) + double.Parse(lbInstallmentlPaid.Text)) != double.Parse(lblInstallmentAmount.Text) && DateTime.Parse(lblDueDate.Text) < DateTime.Parse(curdate.ToString("dd-MMM-yyyy")))
                            {
                                chkInstallment.Checked = true;
                                chkInstallment.Visible = true;
                            }
                            if (chkInstallment.Checked)
                            {
                                txtInstallmentPayable.Text = dt.Rows[i]["DueAmount"].ToString(); ;
                                lblInstallmentDue.Text = "0.00";
                            }
                            else
                            {
                                txtInstallmentPayable.Text = "";
                                lblInstallmentDue.Text = dt.Rows[i]["DueAmount"].ToString();
                            }
                            if (chkCompleteFee.Checked)
                            {
                                txtInstallmentPayable.Text = dt.Rows[i]["DueAmount"].ToString(); ;
                                lblInstallmentDue.Text = "0.00";
                                txtInstallmentdiscount.Attributes.Add("readonly", "readonly");
                                txtInstallmentPayable.Attributes.Add("readonly", "readonly");
                                txtInstallmentNarration.Attributes.Add("readonly", "readonly");
                                chkInstallment.Checked = true;
                                chkInstallment.Visible = true;
                                chkInstallment.Attributes.Add("readonly", "readonly");
                            }
                            gtotal = gtotal + double.Parse(txtInstallmentPayable.Text == "" ? "0" : txtInstallmentPayable.Text);
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
                                cmd1.Parameters.AddWithValue("@PMtMode", DropDownMOD.SelectedValue);
                                cmd1.Parameters.AddWithValue("@MODForFeeDeposit", chkCompleteFee.Checked == true ? "A" : "I");
                                cmd1.Parameters.AddWithValue("@completefeeCheck", (chkCompleteFee.Checked ? "1" : "0"));
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
                                        CheckBox chkInstallmentFee = (CheckBox)rptFee.Items[j].FindControl("chkInstallmentFee");
                                        Label lblStsChild = (Label)rptFee.Items[j].FindControl("lblStsChild");
                                        Label lblInstallmentId = (Label)rptFee.Items[j].FindControl("lblInstallmentId");
                                        Label lblFeeheadId = (Label)rptFee.Items[j].FindControl("lblFeeheadId");
                                        Label lblFeeHeadBalance = (Label)rptFee.Items[j].FindControl("lblFeeHeadBalance");
                                        Label lblFeeHeadDiscountPaid = (Label)rptFee.Items[j].FindControl("lblFeeHeadDiscountPaid");
                                        Label lblFeeHeadDiscount = (Label)rptFee.Items[j].FindControl("lblFeeHeadDiscount");
                                        TextBox txtFeeHeadDiscount = (TextBox)rptFee.Items[j].FindControl("txtFeeHeadDiscount");
                                        TextBox txtFeeHeadPayable = (TextBox)rptFee.Items[j].FindControl("txtFeeHeadPayable");
                                        TextBox txtFeeHeadNarration = (TextBox)rptFee.Items[j].FindControl("txtFeeHeadNarration");
                                        HtmlTableRow feeheadrow = (HtmlTableRow)rptFee.Items[j].FindControl("feeheadrow");
                                        Label ChildDiscount = (Label)rptFee.Items[j].FindControl("ChildDiscount");
                                        Label lblFeeHeadTotal = (Label)rptFee.Items[j].FindControl("lblFeeHeadTotal");
                                        Label lblFeeHeadPaid = (Label)rptFee.Items[j].FindControl("lblFeeHeadPaid");
                                        Label lblFeeHeadBalanceShow = (Label)rptFee.Items[j].FindControl("lblFeeHeadBalanceShow");
                                        Label lblisFineFeeApply = (Label)rptFee.Items[j].FindControl("lblisFineFeeApply");
                                        Label lblFeeheadAmount = (Label)rptFee.Items[j].FindControl("lblFeeheadAmount");

                                        string chkCheque = "(select id from FeeHeadMaster where BranchCode=" + Session["BranchCode"] + " and FeeType in ('Cheque Bounce Charge','Fine (Late Fee)') and id=" + lblFeeheadId.Text.Trim() + " )";
                                        if (_oo.Duplicate(chkCheque))
                                        {
                                            chkInstallmentFee.Attributes.Add("onclick", "return false;");
                                        }

                                        if (lblFeeHeadBalance.Text == "")
                                        {
                                            lblFeeHeadBalance.Text = "0.00";
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
                                        //if (double.Parse(lblFeeHeadDiscount.Text == "" ? "0" : lblFeeHeadDiscount.Text) == double.Parse(lblFeeheadAmount.Text == "" ? "0" : lblFeeheadAmount.Text))
                                        //{
                                        //}
                                        if (double.Parse(lblFeeHeadDiscount.Text == "" ? "0" : lblFeeHeadDiscount.Text) > 0 || double.Parse(lblFeeHeadDiscountPaid.Text == "" ? "0" : lblFeeHeadDiscountPaid.Text) > 0)
                                        {
                                            ChildDiscount.Visible = true;
                                        }
                                        else
                                        {
                                            ChildDiscount.Visible = false;
                                        }

                                        if ((double.Parse(lblFeeHeadBalance.Text == "" ? "0" : lblFeeHeadBalance.Text) == 0 && double.Parse(lblFeeHeadDiscount.Text) == double.Parse(lblFeeheadAmount.Text)))
                                        {
                                            txtFeeHeadPayable.Text = "0.00";
                                            lblFeeHeadBalanceShow.Text = "0.00";
                                            chkInstallmentFee.Checked = false;
                                            if (lblisFineFeeApply.Text == "ApplyFine")
                                            {

                                                txtFeeHeadPayable.Attributes.Add("readonly", "readonly");

                                                //chkInstallmentFee.Enabled = false;
                                            }
                                        }
                                        else if ((double.Parse(lblFeeHeadBalance.Text == "" ? "0" : lblFeeHeadBalance.Text) > 0 || double.Parse(lblFeeHeadDiscount.Text) > double.Parse(lblFeeHeadDiscountPaid.Text)) && chkInstallment.Checked)
                                        {
                                            txtFeeHeadPayable.Text = ds.Tables[0].Rows[j]["DueAmount"].ToString();
                                            lblFeeHeadBalanceShow.Text = "0.00";
                                            chkInstallmentFee.Checked = true;
                                            if (lblisFineFeeApply.Text == "ApplyFine")
                                            {

                                                txtFeeHeadPayable.Attributes.Add("readonly", "readonly");

                                                //chkInstallmentFee.Enabled = false;
                                            }


                                        }
                                        else if ((double.Parse(lblFeeHeadBalance.Text == "" ? "0" : lblFeeHeadBalance.Text) > 0 || double.Parse(lblFeeHeadDiscount.Text) > double.Parse(lblFeeHeadDiscountPaid.Text)) && !chkInstallment.Checked)
                                        {
                                            txtFeeHeadPayable.Text = "";
                                            lblFeeHeadBalanceShow.Text = ds.Tables[0].Rows[j]["DueAmount"].ToString();
                                            chkInstallmentFee.Checked = true;
                                            chkInstallmentFee.Checked = false;
                                            txtFeeHeadDiscount.Attributes.Add("readonly", "readonly");
                                            if (lblisFineFeeApply.Text == "ApplyFine")
                                            {

                                                txtFeeHeadPayable.Attributes.Add("readonly", "readonly");

                                                //chkInstallmentFee.Enabled = false;
                                            }

                                            txtFeeHeadNarration.Attributes.Add("readonly", "readonly");

                                        }
                                        else if ((double.Parse(lblFeeHeadBalance.Text == "" ? "0" : lblFeeHeadBalance.Text) == 0 && double.Parse(lblFeeHeadDiscount.Text) == double.Parse(lblFeeHeadDiscountPaid.Text)))
                                        {
                                            lblFeeHeadBalanceShow.Text = "0.00";
                                            txtFeeHeadDiscount.Text = "";
                                            txtFeeHeadPayable.Text = "";
                                            txtFeeHeadNarration.Text = "";
                                            txtFeeHeadDiscount.Attributes.Add("readonly", "readonly");
                                            txtFeeHeadPayable.Attributes.Add("readonly", "readonly");
                                            txtFeeHeadNarration.Attributes.Add("readonly", "readonly");
                                            chkInstallmentFee.Checked = false;
                                            chkInstallmentFee.Visible = false;
                                        }
                                        if ((double.Parse(lblFeeHeadBalance.Text == "" ? "0" : lblFeeHeadBalance.Text) == 0 && double.Parse(lblFeeHeadDiscount.Text) >= double.Parse(lblFeeHeadDiscountPaid.Text)))
                                        {
                                            txtFeeHeadDiscount.Attributes.Add("readonly", "readonly");
                                            txtFeeHeadPayable.Attributes.Add("readonly", "readonly");
                                            txtFeeHeadNarration.Attributes.Add("readonly", "readonly");
                                        }
                                        if ((double.Parse(lblFeeHeadBalance.Text == "" ? "0" : lblFeeHeadBalance.Text) == 0 && double.Parse(lblFeeHeadDiscount.Text) > double.Parse(lblFeeHeadDiscountPaid.Text)))
                                        {
                                            chkInstallmentFee.Checked = true;
                                            chkInstallmentFee.Visible = true;

                                        }
                                        if ((double.Parse(lblFeeHeadBalance.Text == "" ? "0" : lblFeeHeadBalance.Text) == 0 && double.Parse(lblFeeHeadDiscount.Text) == double.Parse(lblFeeheadAmount.Text)))
                                        {
                                            txtFeeHeadPayable.Text = "0.00";
                                            lblFeeHeadBalanceShow.Text = "0.00";
                                            chkInstallmentFee.Checked = false;
                                            chkInstallmentFee.Visible = false;
                                            if (lblisFineFeeApply.Text == "ApplyFine")
                                            {

                                                txtFeeHeadPayable.Attributes.Add("readonly", "readonly");

                                                //chkInstallmentFee.Enabled = false;
                                            }

                                        }
                                        if ((double.Parse(lblFeeHeadBalance.Text == "" ? "0" : lblFeeHeadBalance.Text) == 0 && (double.Parse(lblFeeHeadDiscount.Text) + double.Parse(lblFeeHeadPaid.Text)) == double.Parse(lblFeeheadAmount.Text) && double.Parse(lblFeeHeadPaid.Text) == 0))
                                        {
                                            chkInstallmentFee.Checked = true;
                                            chkInstallmentFee.Visible = true;
                                        }
                                        if (chkInstallmentFee.Checked)
                                        {
                                            txtFeeHeadPayable.Text = ds.Tables[0].Rows[j]["DueAmount"].ToString();
                                            lblFeeHeadBalanceShow.Text = "0.00";
                                        }
                                        else
                                        {
                                            txtFeeHeadPayable.Text = "";
                                            lblFeeHeadBalanceShow.Text = ds.Tables[0].Rows[j]["DueAmount"].ToString();
                                        }
                                        if (chkCompleteFee.Checked)
                                        {
                                            txtFeeHeadPayable.Text = ds.Tables[0].Rows[j]["DueAmount"].ToString();
                                            lblFeeHeadBalanceShow.Text = "0.00";
                                            txtFeeHeadDiscount.Attributes.Add("readonly", "readonly");
                                            txtFeeHeadPayable.Attributes.Add("readonly", "readonly");
                                            txtFeeHeadNarration.Attributes.Add("readonly", "readonly");
                                            chkInstallmentFee.Checked = true;
                                            chkInstallmentFee.Visible = true;

                                        }
                                        if (stsPending > 0)
                                        {
                                            txtInstallmentPayable.Text = "";
                                            txtInstallmentNarration.Text = "";
                                            txtInstallmentdiscount.Text = "";
                                            txtInstallmentdiscount.Attributes.Add("readonly", "readonly");
                                            txtInstallmentPayable.Attributes.Add("readonly", "readonly");
                                            txtInstallmentNarration.Attributes.Add("readonly", "readonly");
                                            chkInstallment.Checked = false;
                                            chkInstallment.Visible = true;
                                            chkInstallment.Attributes.Add("readonly", "readonly");


                                            //txtFeeHeadPayable.Text = "";
                                            txtFeeHeadDiscount.Text = "";
                                            txtFeeHeadDiscount.Attributes.Add("readonly", "readonly");
                                            txtFeeHeadPayable.Attributes.Add("readonly", "readonly");
                                            txtFeeHeadNarration.Text = "";
                                            txtFeeHeadNarration.Attributes.Add("readonly", "readonly");
                                            chkInstallmentFee.Checked = false;
                                            chkInstallmentFee.Visible = true;

                                        }
                                        if ((double.Parse(lblFeeHeadPaid.Text == "" ? "0" : lblFeeHeadPaid.Text) == 0 && double.Parse(lblFeeheadAmount.Text) == double.Parse(lblFeeHeadDiscountPaid.Text)))
                                        {
                                            lblFeeHeadBalanceShow.Text = "0.00";
                                            txtFeeHeadDiscount.Text = "";
                                            txtFeeHeadPayable.Text = "";
                                            txtFeeHeadNarration.Text = "";
                                            txtFeeHeadDiscount.Attributes.Add("readonly", "readonly");
                                            txtFeeHeadPayable.Attributes.Add("readonly", "readonly");
                                            txtFeeHeadNarration.Attributes.Add("readonly", "readonly");
                                            chkInstallmentFee.Checked = false;
                                            chkInstallmentFee.Visible = false;
                                        }
                                        if (!TuitionPaid)
                                        {
                                            txtInstallmentPayable.Attributes.Add("readonly", "readonly");
                                            txtFeeHeadPayable.Attributes.Add("readonly", "readonly");
                                        }
                                        if (!TuitionDiscount)
                                        {
                                            txtInstallmentdiscount.Attributes.Add("readonly", "readonly");
                                            txtFeeHeadDiscount.Attributes.Add("readonly", "readonly");
                                            txtFeeHeadNarration.Text = "";
                                            txtFeeHeadNarration.Attributes.Add("readonly", "readonly");
                                            txtInstallmentNarration.Text = "";
                                            txtInstallmentNarration.Attributes.Add("readonly", "readonly");

                                        }
                                        if (Session["Logintype"].ToString() == "Guardian" || Session["Logintype"].ToString() == "Student")
                                        {
                                            txtInstallmentPayable.Attributes.Add("readonly", "readonly");
                                            txtFeeHeadPayable.Attributes.Add("readonly", "readonly");
                                            txtInstallmentdiscount.Attributes.Add("readonly", "readonly");
                                            txtFeeHeadDiscount.Attributes.Add("readonly", "readonly");
                                        }
                                        if (!chkInstallment.Checked)
                                        {
                                            chkInstallmentFee.Checked = false;

                                        }
                                        if (double.Parse(lblFeeHeadTotal.Text) < 0)
                                        {
                                            isMinus = isMinus + 1;
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
                                            cmd2.Parameters.AddWithValue("@PMtMode", DropDownMOD.SelectedValue);
                                            cmd2.Parameters.AddWithValue("@MODForFeeDeposit", chkCompleteFee.Checked == true ? "A" : "I");
                                            cmd2.Parameters.AddWithValue("@completefeeCheck", (chkCompleteFee.Checked ? "1" : "0"));
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
                                            }
                                        }
                                    }
                                }
                                Label lblIcon = (Label)rptFeeStructure.Items[i].FindControl("lblIcon");

                                Repeater rptHistory = (Repeater)rptFeeStructure.Items[i].FindControl("rptHistory");
                                rptHistory.DataSource = null;
                                rptHistory.DataBind();
                                if (ds.Tables[1].Rows.Count > 0)
                                {
                                    lblIcon.BackColor = System.Drawing.ColorTranslator.FromHtml("#c4ffc6");
                                    rptHistory.DataSource = ds.Tables[1];
                                    rptHistory.DataBind();
                                    for (int k = 0; k < ds.Tables[1].Rows.Count; k++)
                                    {
                                        string rec1 = "select Template from FeereceiptTemplate where BranchCode=" + Session["BranchCode"].ToString() + "";
                                        if (_oo.Duplicate(rec1))
                                        {
                                            if (_oo.ReturnTag(rec1, "Template").ToString().ToLower() == "template 1")
                                            {
                                                HyperLink HylnkReceiptNo = (HyperLink)rptHistory.Items[k].FindControl("HylnkReceiptNo");
                                                HylnkReceiptNo.Visible = true;
                                                HylnkReceiptNo.NavigateUrl = "FeeReceiptAllDuplicate.aspx?RecieptSrNo=" + ds.Tables[1].Rows[k]["ReceiptNo"].ToString().Replace("/", "__") + "$" + ds.Tables[1].Rows[k]["SessionName"].ToString() + "$" + ds.Tables[1].Rows[k]["BranchCode"].ToString();
                                            }
                                            else if (_oo.ReturnTag(rec1, "Template").ToString().ToLower() == "template 2")
                                            {
                                                HyperLink HylnkReceiptNoT2 = (HyperLink)rptHistory.Items[k].FindControl("HylnkReceiptNoT2");
                                                HylnkReceiptNoT2.Visible = true;
                                                HylnkReceiptNoT2.NavigateUrl = "FeeReceiptAllT2.aspx?RecieptSrNo=" + ds.Tables[1].Rows[k]["ReceiptNo"].ToString().Replace("/", "__") + "$" + ds.Tables[1].Rows[k]["SessionName"].ToString() + "$" + ds.Tables[1].Rows[k]["BranchCode"].ToString();
                                            }
                                            else
                                            {
                                                HyperLink HylnkReceiptNo = (HyperLink)rptHistory.Items[k].FindControl("HylnkReceiptNo");
                                                HylnkReceiptNo.Visible = true;
                                                HylnkReceiptNo.NavigateUrl = "FeeReceiptTT3.aspx?RecieptSrNo=" + ds.Tables[1].Rows[k]["ReceiptNo"].ToString().Replace("/", "__") + "$" + ds.Tables[1].Rows[k]["SessionName"].ToString() + "$" + ds.Tables[1].Rows[k]["BranchCode"].ToString();
                                            }
                                        }
                                        else
                                        {
                                            HyperLink HylnkReceiptNo = (HyperLink)rptHistory.Items[k].FindControl("HylnkReceiptNo");
                                            HylnkReceiptNo.Visible = true;
                                            HylnkReceiptNo.NavigateUrl = "FeeReceiptAllDuplicate.aspx?RecieptSrNo=" + ds.Tables[1].Rows[k]["ReceiptNo"].ToString().Replace("/", "__") + "$" + ds.Tables[1].Rows[k]["SessionName"].ToString() + "$" + ds.Tables[1].Rows[k]["BranchCode"].ToString();
                                        }



                                    }
                                }

                            }
                        }
                        if (stsPending > 0)
                        {
                            txtTotalPaid.Text = "0.00";
                            hdnTotalPaid.Value = "0.00";
                            txtTotalPaid.Attributes.Add("readonly", "readonly");
                            lnkSubmit.Visible = false;
                        }
                        else
                        {
                            txtTotalPaid.Text = gtotal.ToString("0.00");
                            hdnTotalPaid.Value = gtotal.ToString("0.00");
                            lnkSubmit.Visible = true;
                        }
                        if (gtotal == 0)
                        {
                            //lnkSubmit.Visible = false;
                        }
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "ChangeRowColor();", true);
                    }
                    else
                    {
                        divTutionFee.Visible = false;
                    }
                    if (isMinus > 0)
                    {
                        txtTotalPaid.Attributes.Add("readonly", "readonly");
                        lnkSubmit.Visible = false;
                        Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Please check discount or fee amount seems negative!", "A");
                    }

                }
            }
            double InstallmentFee_total = 0; double Disvount_total = 0; double Total_total = 0; double Paid_total = 0; double Balance_total = 0;
            double Consolidated_total = 0;
            for (int i = 0; i < rptFeeStructure.Items.Count; i++)
            {
                Label lblInstallmentAmount = (Label)rptFeeStructure.Items[i].FindControl("lblInstallmentAmount");
                Label lblInstallmentDiscount = (Label)rptFeeStructure.Items[i].FindControl("lblInstallmentDiscount");
                Label lblInstallmentTotal = (Label)rptFeeStructure.Items[i].FindControl("lblInstallmentTotal");
                Label lbInstallmentlPaid = (Label)rptFeeStructure.Items[i].FindControl("lbInstallmentlPaid");
                Label lblInstallmentDue = (Label)rptFeeStructure.Items[i].FindControl("lblInstallmentDue");
                Label lblrecuring = (Label)rptFeeStructure.Items[i].FindControl("lblrecuring");
                InstallmentFee_total = InstallmentFee_total + double.Parse(lblInstallmentAmount.Text == "" ? "0" : lblInstallmentAmount.Text);
                Disvount_total = Disvount_total + double.Parse(lblInstallmentDiscount.Text == "" ? "0" : lblInstallmentDiscount.Text);
                Total_total = Total_total + double.Parse(lblInstallmentTotal.Text == "" ? "0" : lblInstallmentTotal.Text);
                Paid_total = Paid_total + double.Parse(lbInstallmentlPaid.Text == "" ? "0" : lbInstallmentlPaid.Text);
                Balance_total = Balance_total + double.Parse(lblInstallmentDue.Text == "" ? "0" : lblInstallmentDue.Text);
                if ((rptFeeStructure.Items.Count - 1) == i)
                {
                    Consolidated_total = Consolidated_total + double.Parse(lblrecuring.Text == "" ? "0" : lblrecuring.Text);
                }
            }
            lblInstallmentFee_total.Text = InstallmentFee_total.ToString("0.00");
            lblDisvount_total.Text = Disvount_total.ToString("0.00");
            lblTotal_total.Text = Total_total.ToString("0.00");
            lblPaid_total.Text = Paid_total.ToString("0.00");
            lblBalance_total.Text = Balance_total.ToString("0.00");
            lblConsolidated_total.Text = Consolidated_total.ToString("0.00");
        }
        catch (SqlException ex)
        {
        }
    }
    protected void SubmitFee()
    {
        string dateOfDeposit = "";
        var studentId = Request.Form[hfStudentId.UniqueID];
        if (string.IsNullOrEmpty(studentId))
        {
            studentId = txtSearch.Text.Trim();
        }
        if (Session["Logintype"].ToString() == "Guardian")
        {
            studentId = Session["Srno"].ToString();
            string ss = "select getdate() dateOfDeposit";
            dateOfDeposit = DateTime.Parse(_oo.ReturnTag(ss, "dateOfDeposit")).ToString("dd-MMM-yyyy");
        }
        string RecieptNo = _oo.FindRecieptNo();
        Session["RecieptSrNo"] = RecieptNo;

        if (RecieptNo.Trim() == "")
        {
            Campus camp = new Campus(); camp.msgbox(this.Page, msgs, "Please initialize receipt no!", "A");
            return;
        }

        double payableAmt = double.Parse(hdnTotalPaid.Value == "" ? "0" : hdnTotalPaid.Value);




        dtComplositFee.Columns.Add("SrNo", typeof(string));
        dtComplositFee.Columns.Add("ReceiptNo", typeof(string));
        dtComplositFee.Columns.Add("InstallmentId", typeof(string));
        dtComplositFee.Columns.Add("FeeHeadId", typeof(string));
        dtComplositFee.Columns.Add("HeadAmount", typeof(string));
        dtComplositFee.Columns.Add("OnpageDisc", typeof(string));
        dtComplositFee.Columns.Add("ManualDiscount", typeof(string));
        dtComplositFee.Columns.Add("SpecialDisc", typeof(string));
        dtComplositFee.Columns.Add("SiblingDisc", typeof(string));
        dtComplositFee.Columns.Add("CompleteFeeDisc", typeof(string));
        dtComplositFee.Columns.Add("PayableAmount", typeof(string));
        dtComplositFee.Columns.Add("PaidAmount", typeof(string));
        dtComplositFee.Columns.Add("BalanceAmount", typeof(string));
        dtComplositFee.Columns.Add("DepositDate", typeof(string));
        dtComplositFee.Columns.Add("ModeOfPayment", typeof(string));
        dtComplositFee.Columns.Add("InstrumentDate", typeof(string));
        dtComplositFee.Columns.Add("InstrumentNo", typeof(string));
        dtComplositFee.Columns.Add("InstrumentStatus", typeof(string));
        dtComplositFee.Columns.Add("BankName", typeof(string));
        dtComplositFee.Columns.Add("receiptStatus", typeof(string));
        dtComplositFee.Columns.Add("SessionName", typeof(string));
        dtComplositFee.Columns.Add("BranchCode", typeof(string));
        dtComplositFee.Columns.Add("LoginName", typeof(string));
        dtComplositFee.Columns.Add("GateWayName", typeof(string));
        dtComplositFee.Columns.Add("TxnID", typeof(string));
        dtComplositFee.Columns.Add("GateWayTxnId", typeof(string));
        dtComplositFee.Columns.Add("Narration", typeof(string));

        if (payableAmt >= 0)
        {
            string ChequeDate = ""; string ChequeNo = ""; string BankName = ""; string ChequeStatus = ""; string Status = "";
            if (DropDownMOD.SelectedValue != "Cash" && DropDownMOD.SelectedValue != "Online")
            {
                ChequeDate = txtChequeDate.Text; ChequeNo = txtChequeNo.Text; BankName = txtBank.Text;
            }
            if (DropDownMOD.SelectedValue == "Cheque" || DropDownMOD.SelectedValue == "Other")
            {
                ChequeStatus = drpStatus.SelectedValue;
                Status = drpStatus.SelectedValue;
            }
            else
            {
                Status = "Paid";
            }
            for (int p = 0; p < rptFeeStructure.Items.Count; p++)
            {
                Repeater rptFee = (Repeater)rptFeeStructure.Items[p].FindControl("rptFee");
                for (int i = 0; i < rptFee.Items.Count; i++)
                {
                    Label lblInstallmentName = (Label)rptFeeStructure.Items[p].FindControl("lblInstallmentName");
                    Label lblFeehead = (Label)rptFee.Items[i].FindControl("lblFeehead");
                    Label lblInstallmentId = (Label)rptFee.Items[i].FindControl("lblInstallmentId");
                    Label lblFeeheadId = (Label)rptFee.Items[i].FindControl("lblFeeheadId");
                    Label lblFeeheadAmount = (Label)rptFee.Items[i].FindControl("lblFeeheadAmount");

                    Label lblFeeHeadDiscount = (Label)rptFee.Items[i].FindControl("lblFeeHeadDiscount");
                    Label lblFeeHeadDiscountPaid = (Label)rptFee.Items[i].FindControl("lblFeeHeadDiscountPaid");
                    TextBox txtFeeHeadDiscount = (TextBox)rptFee.Items[i].FindControl("txtFeeHeadDiscount");

                    Label lblFeeHeadTotal = (Label)rptFee.Items[i].FindControl("lblFeeHeadTotal");
                    Label lblFeeHeadPaid = (Label)rptFee.Items[i].FindControl("lblFeeHeadPaid");
                    TextBox txtFeeHeadPayable = (TextBox)rptFee.Items[i].FindControl("txtFeeHeadPayable");

                    Label lblFeeHeadBalance = (Label)rptFee.Items[i].FindControl("lblFeeHeadBalance");
                    Label lblFeeHeadBalanceShow = (Label)rptFee.Items[i].FindControl("lblFeeHeadBalanceShow");

                    TextBox txtFeeHeadNarration = (TextBox)rptFee.Items[i].FindControl("txtFeeHeadNarration");
                    CheckBox chkInstallment = (CheckBox)rptFeeStructure.Items[p].FindControl("chkInstallment");
                    CheckBox chkInstallmentFee = (CheckBox)rptFee.Items[i].FindControl("chkInstallmentFee");
                    if (chkInstallment.Checked && chkInstallmentFee.Checked)
                    {
                        double OnpageDiscount = 0;
                        double ManualDiscount = 0;
                        double GenderDiscount = 0;
                        double SiblingDiscount = 0;
                        double CompleteFeeDiscount = 0;
                        Repeater RepeaterChildDiscount = (Repeater)rptFee.Items[i].FindControl("RepeaterChildDiscount");

                        for (int j = 0; j < RepeaterChildDiscount.Items.Count; j++)
                        {
                            Label lblDiscNameC = (Label)RepeaterChildDiscount.Items[j].FindControl("lblDiscNameC");
                            Label lblDiscAmtC = (Label)RepeaterChildDiscount.Items[j].FindControl("lblDiscAmtC");
                            Label lblPaidDiscAmtC = (Label)RepeaterChildDiscount.Items[j].FindControl("lblPaidDiscAmtC");

                            OnpageDiscount = double.Parse(txtFeeHeadDiscount.Text == "" ? "0.00" : txtFeeHeadDiscount.Text);
                            if (lblDiscNameC.Text == "Manual Discount")
                            {
                                ManualDiscount = double.Parse(lblDiscAmtC.Text);
                            }
                            if (lblDiscNameC.Text == "Special Discount")
                            {
                                GenderDiscount = double.Parse(lblDiscAmtC.Text);
                            }
                            if (lblDiscNameC.Text == "Sibling Discount")
                            {
                                SiblingDiscount = double.Parse(lblDiscAmtC.Text);
                            }
                            if (lblDiscNameC.Text == "Complete Fee Discount")
                            {
                                CompleteFeeDiscount = double.Parse(lblDiscAmtC.Text);
                            }
                        }

                        DataRow row = dtComplositFee.NewRow();
                        row["SrNo"] = studentId;
                        row["ReceiptNo"] = RecieptNo;
                        row["InstallmentId"] = lblInstallmentId.Text;
                        row["FeeHeadId"] = lblFeeheadId.Text;
                        string mainpa = "0";
                        double totaldisc = 0;
                        string ss1 = "select sum(ManualDiscount) manuals, sum(SpecialDisc) SpecialDisc, sum(SiblingDisc) SiblingDisc, sum(CompleteFeeDisc) CompleteFeeDisc from CompositFeeDeposit where FeeHeadId=" + lblFeeheadId.Text + " and InstallmentId=" + lblInstallmentId.Text + " and SrNo='" + studentId + "' and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " and receiptStatus<>'Cancelled'";
                        if (_oo.Duplicate(ss1))
                        {
                            double manual = double.Parse(_oo.ReturnTag(ss1, "manuals"));
                            totaldisc = (double.Parse(txtFeeHeadDiscount.Text == "" ? "0" : txtFeeHeadDiscount.Text) + (ManualDiscount - manual));
                            ManualDiscount = (ManualDiscount - manual);
                            mainpa = lblFeeHeadBalance.Text;
                            GenderDiscount = 0;
                            SiblingDiscount = 0;
                            CompleteFeeDiscount = 0;
                        }
                        else
                        {
                            row["HeadAmount"] = lblFeeheadAmount.Text;
                            mainpa = lblFeeheadAmount.Text;
                            totaldisc = (double.Parse(txtFeeHeadDiscount.Text == "" ? "0" : txtFeeHeadDiscount.Text) + ManualDiscount + GenderDiscount + SiblingDiscount + CompleteFeeDiscount);
                        }
                        double payle = (double.Parse(mainpa) - totaldisc);
                        row["HeadAmount"] = double.Parse(mainpa).ToString("0.00");
                        row["OnpageDisc"] = (txtFeeHeadDiscount.Text == "" ? "0" : txtFeeHeadDiscount.Text);
                        row["ManualDiscount"] = ManualDiscount.ToString("0.00");
                        row["SpecialDisc"] = GenderDiscount.ToString("0.00");
                        row["SiblingDisc"] = SiblingDiscount.ToString("0.00");
                        row["CompleteFeeDisc"] = CompleteFeeDiscount.ToString("0.00");
                        row["PayableAmount"] = payle.ToString("0.00");
                        row["PaidAmount"] = double.Parse(txtFeeHeadPayable.Text).ToString("0.00");
                        row["BalanceAmount"] = (payle > 0 ? (payle - double.Parse(txtFeeHeadPayable.Text)).ToString("0.00") : "0.00");
                        row["DepositDate"] = (Session["Logintype"].ToString() == "Guardian" ? dateOfDeposit : txtDepositDate.Text);
                        row["ModeOfPayment"] = DropDownMOD.SelectedValue;
                        row["InstrumentDate"] = ChequeDate;
                        row["InstrumentNo"] = ChequeNo;
                        row["InstrumentStatus"] = "Paid";
                        row["BankName"] = BankName;
                        row["InstrumentStatus"] = ChequeStatus;
                        row["receiptStatus"] = Status;
                        row["SessionName"] = Session["SessionName"].ToString();
                        row["BranchCode"] = Session["BranchCode"].ToString();
                        row["LoginName"] = Session["LoginName"].ToString();
                        row["GateWayName"] = "";
                        row["TxnID"] = "";
                        row["GateWayTxnId"] = "";
                        row["Narration"] = txtFeeHeadNarration.Text;
                        dtComplositFee.Rows.Add(row);
                    }
                }
            }
        }
    }
    protected void lnkSubmit_Click(object sender, EventArgs e)
    {


        if (txtDepositDate.Text.Trim() == "" && Session["Logintype"].ToString() != "Guardian")
        {
            Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Please select Deposit date.", "A");
            ScriptManager.RegisterClientScriptBlock(this, GetType(), "disabled", "$('.dbclick').removeAttr('disabled');", true);
            return;
        }

        if ((DropDownMOD.SelectedValue == "Cheque" || DropDownMOD.SelectedValue == "DD") && txtChequeNo.Text.Trim() == "")
        {
            Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Please enter Instrument No.", "A");
            ScriptManager.RegisterClientScriptBlock(this, GetType(), "disabled", "$('.dbclick').removeAttr('disabled');", true);
            return;
        }
        else if ((DropDownMOD.SelectedValue == "Card") && txtChequeNo.Text.Trim() == "")
        {
            Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Please enter Card No.", "A");
            ScriptManager.RegisterClientScriptBlock(this, GetType(), "disabled", "$('.dbclick').removeAttr('disabled');", true);
            return;
        }
        else if ((DropDownMOD.SelectedValue == "Online Transfer" || DropDownMOD.SelectedValue == "Other") && txtChequeNo.Text.Trim() == "")
        {
            Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Please enter Ref. No.", "A");
            ScriptManager.RegisterClientScriptBlock(this, GetType(), "disabled", "$('.dbclick').removeAttr('disabled');", true);
            return;
        }
        if (DropDownMOD.SelectedValue.Trim() == "Cash" || DropDownMOD.SelectedValue.Trim() == "Cheque" || DropDownMOD.SelectedValue.Trim() == "DD" || DropDownMOD.SelectedValue.Trim() == "Card" || DropDownMOD.SelectedValue.Trim() == "Online Transfer" || DropDownMOD.SelectedValue.Trim() == "Other")
        {

            var studentId = Request.Form[hfStudentId.UniqueID];

            if (string.IsNullOrEmpty(studentId))
            {
                studentId = txtSearch.Text.Trim();
            }
       
            SubmitFee();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SetAllCompositFeeProc";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = _con;
            List<SqlParameter> para = new List<SqlParameter>
                        {
                            new SqlParameter("@TableTypeComplositFee", dtComplositFee),
                            new SqlParameter("@ComplositFeeCount", dtComplositFee.Rows.Count),
                            new SqlParameter("@Result", "")
                        };
            para[para.Count - 1].Size = 0x100;
            para[para.Count - 1].Direction = ParameterDirection.Output;
            cmd.Parameters.AddRange(para.ToArray());
            try
            {
                _con.Open();
                int RowsEffected = cmd.ExecuteNonQuery();
                _con.Close();

                if (RowsEffected > 0)
                {
                    string recieptSrNo = cmd.Parameters["@result"].Value.ToString();
                    if (recieptSrNo == "")
                    {
                        Campus camp = new Campus(); camp.msgbox(Page, msgbox, "A technical error has occurred. Please try the fee deposit again.", "W");
                    }
                    else
                    {
                        Campus camp = new Campus();
                        camp.msgbox(Page, msgbox, "submit successfully", "S");
                        ComposeSMS(recieptSrNo);
                        string qstr = recieptSrNo.Replace("/", "__");
                        qstr = qstr + ("$" + Session["SessionName"].ToString() + "$" + Session["BranchCode"].ToString()).ToString();
                        Session["qstr"] = qstr;
                        string rec = "select Template from FeereceiptTemplate where BranchCode=" + Session["BranchCode"].ToString() + "";
                        if (_oo.Duplicate(rec))
                        {
                            if (_oo.ReturnTag(rec, "Template").ToString().ToLower() == "template 1")
                            {
                                Response.Redirect("FeeReceiptAll.aspx?RecieptSrNo=" + qstr, false);
                            }
                            else if (_oo.ReturnTag(rec, "Template").ToString().ToLower() == "template 2")
                            {
                                Response.Redirect("FeeReceiptAllT2.aspx?RecieptSrNo=" + qstr, false);
                            }
                            else
                            {
                                Response.Redirect("FeeReceiptTT3.aspx?RecieptSrNo=" + qstr, false);
                            }
                        }
                        else
                        {
                            Response.Redirect("FeeReceiptAll.aspx?RecieptSrNo=" + qstr, false);
                        }
                    }
                }
                else
                {
                    Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Some technical problems have occurred", "A");
                }

            }
            catch (Exception ex)
            {
                if (Session["isDoubleClick"] != null)
                    Session.Remove("isDoubleClick");
                if (Session["TempSrNo"] != null)
                    Session.Remove("TempSrNo");
                if (Session["qstr"] != null)
                    Session.Remove("qstr");
                ex.Message.ToString();
                // ignored
            }
        }

        ScriptManager.RegisterClientScriptBlock(this, GetType(), "disabled", "$('.dbclick').removeAttr('disabled');", true);
    }

    public void ComposeSMS(string recieptNo)
    {
        try
        {
            List<SqlParameter> param = new List<SqlParameter>()
            {
                new SqlParameter("@SessionName",Session["SessionName"]),
                new SqlParameter("@ReceiptNo",recieptNo),
                new SqlParameter("@BranchCode",Session["BranchCode"])
            };
            DataSet ds = _oo.ReturnDataSet("USP_CompositFeeTemplate", param.ToArray());
            if (ds != null && ds.Tables.Count > 0)
            {
                string msg = SendSms(ds);
            }

        }
        catch
        {

        }

    }

    public string SendSms(DataSet ds)
    {
        string msg;
        try
        {
            DataTable data = new DataTable();
            data = ds.Tables[0];

            var fatherContactNo = data.Rows[0]["FatherContactNo"].ToString();

            DataTable template = new DataTable();
            template = ds.Tables[1];

            msg = template.Rows[0][0].ToString();
            string[] param = template.Rows[0][1].ToString().Split(',');

            string[] daynamicVariables = msg.Split(new char[0]);
            foreach (var para in param)
            {
                string value = data.Rows[0][para].ToString();
                for (int i = 0; i < daynamicVariables.Count(); i++)
                {
                    if (daynamicVariables[i].ToString() == "{{{}}}")
                    {
                        daynamicVariables[i] = value;
                        break;
                    }
                }
            }

            msg = string.Join(" ", daynamicVariables);

            SendFeesSms(fatherContactNo, msg, "1");

        }
        catch
        {
            msg = "";
        }
        return msg;
    }
    public string SendFeesSms(string fmobileNo, string msg, string smsPageId)
    {
        string res = "0";
        try
        {
            _sql = "Select HitValue from SMSActivateDeactivate where BranchCode=" + Session["BranchCode"] + "";
            if (_oo.ReturnTag(_sql, "HitValue") == "") return res;
            if (_oo.ReturnTag(_sql, "HitValue") != "true") return res;
            string sql1 = "Select SmsSent From SmsEmailMaster Where Id='" + smsPageId + "' and BranchCode='" + Session["BranchCode"] + "'";
            if (_oo.ReturnTag(sql1, "SmsSent").Trim() == "true")
            {
                var sadpNew = new SMSAdapterNew();
                //_sql = "Select top(1) FirstName as StudentName  from StudentGenaralDetail  where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " and SrNo='" + registervalue + "' order by id desc";
                ////var mess = "INR " + receivedAmount + " received for " + _oo.ReturnTag(_sql, "StudentName") + " ( " + registervalue + " ). Receipt No. " + recieptNo + "";
                //string msg = "INR " + receivedAmount + " received for " + _oo.ReturnTag(_sql, "StudentName") + " (" + registervalue + "). Receipt No. " + recieptNo + " From, Mahavir Vidya Mandir Inter College.";
                //// format : INR {#var#} received for {#var#} ({#var#}). Receipt No. {#var#} From, RRSLKO
                if (fmobileNo == "") return "0";
                sadpNew.Send(msg, fmobileNo, smsPageId);
                res = "1";
            }
        }
        catch (Exception ex)
        {
            // ignored
        }

        return res;
    }

    protected void chkCompleteFee_CheckedChanged(object sender, EventArgs e)
    {
        var studentId = Request.Form[hfStudentId.UniqueID];
        if (string.IsNullOrEmpty(studentId))
        {
            studentId = txtSearch.Text.Trim();
        }
        if (Session["Logintype"].ToString() == "Guardian" || Session["Logintype"].ToString() == "Student")
        {
            studentId = Session["Srno"].ToString();
        }
        GetTutionFeeDetails(studentId);
        if (chkCompleteFee.Checked)
        {
            txtTotalPaid.Attributes.Add("readonly", "readonly");
        }

        ScriptManager.RegisterStartupScript(Page, GetType(), "script", "MODChenge();", true);
    }

    protected void reInitiate_Click(object sender, EventArgs e)
    {
        string sql1 = "select authorizationHeader, MarchentKEY,MerchantID from TblPayMentGateway where GateWayName='PayUMoney' and GateWayfor='Fee' and BranchCode=" + Session["BranchCode"] + " and Isactive=1";
        string sql2 = "select MerchantID from TblPayMentGateway where GateWayName='Eazypay' and GateWayfor='Fee' and BranchCode=" + Session["BranchCode"] + " and Isactive=1";
        string sql3 = "select MerchantID, MarchentKEY, ProductInfo from TblPayMentGateway where GateWayName='Atom' and GateWayfor='Fee' and BranchCode=" + Session["BranchCode"] + " and Isactive=1";
        if (_oo.Duplicate(sql1) || _oo.Duplicate(sql2) || _oo.Duplicate(sql3))
        {
            string authorizationHeader = "", merchantKey = "", MerchantID = "";
            authorizationHeader = _oo.ReturnTag(sql1, "authorizationHeader");
            merchantKey = _oo.ReturnTag(sql1, "MarchentKEY");
            MerchantID = _oo.ReturnTag(sql2, "MerchantID");
            string sql = "select distinct GateWayName, TxnID, FORMAT(RecordDate, 'yyyy-MM-dd') txtDate, sum(PaidAmount) amount  from CompositFeeDepositTemp where BranchCode=" + Session["BranchCode"] + " group by GateWayName, TxnID, FORMAT(RecordDate, 'yyyy-MM-dd') ";
            if (_oo.Duplicate(sql))
            {
                var dt = _oo.Fetchdata(sql);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["GateWayName"].ToString().ToLower() == "payumoney")
                    {
                        GetOnlineData(dt.Rows[i]["TxnID"].ToString(), authorizationHeader, merchantKey);
                    }
                    if (dt.Rows[i]["GateWayName"].ToString().ToLower() == "easypay")
                    {
                        GetOnlineDataEasyPay(dt.Rows[i]["TxnID"].ToString(), MerchantID);
                    }
                    if (dt.Rows[i]["GateWayName"].ToString().ToLower() == "atom")
                    {
                        double amt = 0;
                        double.TryParse(dt.Rows[i]["amount"].ToString(), out amt);
                        GetOnlineDataAtom(_oo.ReturnTag(sql3, "MarchentKEY"), dt.Rows[i]["txtDate"].ToString(), _oo.ReturnTag(sql3, "ProductInfo"), dt.Rows[i]["TxnID"].ToString(), amt);
                    }
                }
                try
                {
                    MakeConnection();
                    cmd.CommandText = "[Sp_DeleteTempFeedataPayumoney]";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = _con;
                    cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
                    cmd.ExecuteNonQuery();
                    _con.Close();
                }
                catch (Exception ex)
                {
                }

            }
        }
    }

    public string GetOnlineData(string merchantTransactionIds, string authorizationHeader, string merchantKey)
    {
        string result = "";
        try
        {
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)(0xc0 | 0x300 | 0xc00);
            string URI = "https://www.payumoney.com/payment/payment/chkMerchantTxnStatus?";
            string myParameters = "merchantKey=" + merchantKey + "&merchantTransactionIds=" + merchantTransactionIds + "";
            using (WebClient wc = new WebClient())
            {
                wc.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                wc.Headers.Add("Authorization", authorizationHeader);
                var HtmlResult = wc.UploadString(URI, myParameters);
                result = HtmlResult.ToString();
                result = result.Replace("{", "").Replace("}", "").Replace("[", "").Replace("]", "").Replace("\"", "").Replace(",\"", ",").Replace("\":\"", ":").Replace("\",\"", ",");
                string[] res = result.Split(new string[] { "," }, StringSplitOptions.None);
                string[] status = res[0].Split(new string[] { ":" }, StringSplitOptions.None);
                string sts = status[1];
                string[] pstatus = res[4].Split(new string[] { ":" }, StringSplitOptions.None);
                string psts = pstatus[1].ToLower();
                try
                {
                    if (sts == "0")
                    {
                        if (psts == "money settled" || psts == "settlement in process")
                        {
                            string MSG = "";
                            MakeConnection();
                            cmd.CommandText = "[SetAllCompositFeeOnlineProc]";
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Connection = _con;
                            cmd.Parameters.AddWithValue("@TxnID", merchantTransactionIds);
                            cmd.Parameters.AddWithValue("@SessionNames", Session["SessionName"].ToString());
                            cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
                            cmd.Parameters.AddWithValue("@MSG", MSG);
                            cmd.Parameters["@MSG"].Direction = ParameterDirection.InputOutput;
                            cmd.Parameters["@MSG"].Size = 1000;
                            cmd.ExecuteNonQuery();
                            MSG = cmd.Parameters["@MSG"].Value.ToString();
                            _con.Close();
                        }
                        else
                        {
                            string sqlsss = "SELECT top(1) DATEDIFF(MINUTE, RecordDate, GETDATE()) timediffer from CompositFeeDepositTemp where BranchCode=" + Session["BranchCode"] + " and TxnID='" + merchantTransactionIds + "'";
                            int timeMin = int.Parse(_oo.ReturnTag(sqlsss, "timediffer"));
                            if (timeMin > 20)
                            {
                                string sqlsss1 = "delete from CompositFeeDepositTemp where BranchCode=" + Session["BranchCode"] + " and TxnID='" + merchantTransactionIds + "'";
                                MakeConnection();
                                cmd.CommandText = sqlsss1;
                                cmd.CommandType = CommandType.Text;
                                cmd.Connection = _con;
                                cmd.ExecuteNonQuery();
                                _con.Close();
                            }
                        }
                    }
                }
                catch (Exception)
                {
                }
            }
        }
        catch (Exception)
        {
            //throw;
        }
        return result;
    }
    public string GetOnlineDataEasyPay(string merchantTransactionIds, string MerchantID)
    {
        string result = "";
        try
        {
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)(0xc0 | 0x300 | 0xc00);
            string URI = "https://eazypay.icicibank.com/EazyPGVerify?";
            string myParameters = "ezpaytranid=&amount=&paymentmode=&merchantid=" + MerchantID + "&trandate=&pgreferenceno=" + merchantTransactionIds + "";
            using (WebClient wc = new WebClient())
            {
                wc.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                var HtmlResult = wc.UploadString(URI, myParameters);
                result = HtmlResult.ToString();
                string[] res = result.Split(new string[] { "=" }, StringSplitOptions.None);
                string[] res1 = res[1].Split(new string[] { "&" }, StringSplitOptions.None);
                string sts1 = res1[0];
                try
                {
                    if (sts1.ToLower() == "success" || sts1.ToLower() == "rip" || sts1.ToLower() == "sip")
                    {
                        string MSG = "";
                        MakeConnection();
                        cmd.CommandText = "[SetAllCompositFeeOnlineProc]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection = _con;
                        cmd.Parameters.AddWithValue("@TxnID", merchantTransactionIds);
                        cmd.Parameters.AddWithValue("@SessionNames", Session["SessionName"].ToString());
                        cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
                        cmd.Parameters.AddWithValue("@MSG", MSG);
                        cmd.Parameters["@MSG"].Direction = ParameterDirection.InputOutput;
                        cmd.Parameters["@MSG"].Size = 1000;
                        cmd.ExecuteNonQuery();
                        MSG = cmd.Parameters["@MSG"].Value.ToString();
                        _con.Close();
                    }
                    else
                    {
                        string sqlsss = "SELECT top(1) DATEDIFF(MINUTE, RecordDate, GETDATE()) timediffer from CompositFeeDepositTemp where BranchCode=" + Session["BranchCode"] + " and TxnID='" + merchantTransactionIds + "'";
                        int timeMin = int.Parse(_oo.ReturnTag(sqlsss, "timediffer"));
                        if (timeMin > 20)
                        {
                            string sqlsss1 = "delete from CompositFeeDepositTemp where BranchCode=" + Session["BranchCode"] + " and TxnID='" + merchantTransactionIds + "'";
                            MakeConnection();
                            cmd.CommandText = sqlsss1;
                            cmd.CommandType = CommandType.Text;
                            cmd.Connection = _con;
                            cmd.ExecuteNonQuery();
                            _con.Close();
                        }
                    }
                }
                catch (Exception ex)
                {
                }
            }
        }
        catch (Exception)
        {
            //throw;
        }
        return result;
    }

    public string GetOnlineDataAtom(string merchId, string merchTxnDate, string password, string merchTxnIds, double amount)
    {
        webHookrequest obj = new webHookrequest();
        string sts1 = obj.APIrequest(merchId, merchTxnDate, password, merchTxnIds, amount);
        try
        {
            if (sts1.ToLower() == "ots0000")
            {
                string MSG = "";
                MakeConnection();
                cmd.CommandText = "[SetAllCompositFeeOnlineProc]";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = _con;
                cmd.Parameters.AddWithValue("@TxnID", merchTxnIds);
                cmd.Parameters.AddWithValue("@SessionNames", Session["SessionName"].ToString());
                cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
                cmd.Parameters.AddWithValue("@MSG", MSG);
                cmd.Parameters["@MSG"].Direction = ParameterDirection.InputOutput;
                cmd.Parameters["@MSG"].Size = 1000;
                cmd.ExecuteNonQuery();
                MSG = cmd.Parameters["@MSG"].Value.ToString();
                _con.Close();
            }
            else
            {
                string sqlsss = "SELECT top(1) DATEDIFF(MINUTE, RecordDate, GETDATE()) timediffer from CompositFeeDepositTemp where BranchCode=" + Session["BranchCode"] + " and TxnID='" + merchTxnIds + "'";
                int timeMin = int.Parse(_oo.ReturnTag(sqlsss, "timediffer"));
                if (timeMin > 20)
                {
                    string sqlsss1 = "delete from CompositFeeDepositTemp where BranchCode=" + Session["BranchCode"] + " and TxnID='" + merchTxnIds + "'";
                    MakeConnection();
                    cmd.CommandText = sqlsss1;
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = _con;
                    cmd.ExecuteNonQuery();
                    _con.Close();
                }
            }
            return sts1;
        }
        catch (Exception)
        {
            return sts1;
        }
    }
}