using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using AjaxControlToolkit;

namespace _1
{
    public partial class StudentInformation : Page
    {
        private SqlConnection _con;
        readonly Campus _oo;
        private string _sql = "";
        private static bool _isfullpaid = true;
        private static int _count;
        private static decimal _nextDue;
#pragma warning disable 649
        private static bool _isAnualDeposit;
#pragma warning restore 649
        // ReSharper disable once RedundantDefaultMemberInitializer
        private static int _isConvAlloted = 0;
        public StudentInformation()
        {
            _con = new SqlConnection();
            _oo = new Campus();
        }
        protected void Page_PreInIt(object sender, EventArgs e)
        {
            if (Session["Logintype"] == null) { Response.Redirect("~/default.aspx"); }
            // ReSharper disable once PossibleNullReferenceException
            if (Session["Logintype"].ToString() == "Admin")
            {
                MasterPageFile = "~/Master/admin_root-manager.master";
            }
            else if (Session["Logintype"].ToString() == "Staff")
            {
                MasterPageFile = "~/Staff/staff_root-manager.master";
            }
            else if (Session["Logintype"].ToString() == "Guardian")
            {
                MasterPageFile = "~/sp/sp_root-manager.master";
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
            {
                Response.Redirect("default.aspx");
            }
            _con = _oo.dbGet_connection();
            Campus camp = new Campus(); camp.LoadLoader(loader);
        }

        private void GetData(string studentId)
        {
            _sql = "Select id,SrNo,StEnRCode,Name,FatherName,ClassName,SectionName,CombineClassName,Medium,Card,Convert(varchar(11),DateOfAdmiission) as DateOfAdmiission,CourseName,BranchName,FamilyContactNo,PhotoPath";
            _sql = _sql + " from AllStudentRecord_UDF('" + Session["SessionName"] + "'," + Session["BranchCode"] + ") asr where srno='" + studentId + "' and Withdrwal is null";
            grdStRecord.DataSource = _oo.GridFill(_sql);
            grdStRecord.DataBind();
            DataSet ds;
            ds = _oo.GridFill(_sql);

            // ReSharper disable once UseNullPropagation
            if (ds != null && grdStRecord.Rows.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    div1.Visible = true;
                    div2.Visible = true;
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

                            GetTutionFeeDetails(studentId.ToString().Trim());
                            if (dsPhoto.Tables[0].Rows.Count > 0)
                            {
                                img.ImageUrl = dsPhoto.Tables[0].Rows[0]["PhotoPath"].ToString() != string.Empty ? dsPhoto.Tables[0].Rows[0]["PhotoPath"] + "?value=" + DateTime.Now.ToString(CultureInfo.InvariantCulture) : "../img/user-pic/user-pic.jpg" + "?value=" + DateTime.Now.ToString(CultureInfo.InvariantCulture);
                                studentImg.NavigateUrl = dsPhoto.Tables[0].Rows[0]["PhotoPath"].ToString() != string.Empty ? dsPhoto.Tables[0].Rows[0]["PhotoPath"] + "?value=" + DateTime.Now.ToString(CultureInfo.InvariantCulture) : "../img/user-pic/user-pic.jpg" + "?value=" + DateTime.Now.ToString(CultureInfo.InvariantCulture);
                                hylinkmoredetails.NavigateUrl = "../11/StudentRegView.aspx?print=1&id=" + ds.Tables[0].Rows[0]["stenrcode"];
                            }
                        }
                    }
                }
            }
        }
        protected void GetTutionFeeDetails(string SrNo)
        {
            try
            {
                string sqlsession = "select top(5) SessionName from StudentOfficialDetails where SrNo='" + SrNo + "' and BranchCode=" + Session["BranchCode"] + " order by id desc";
                DataTable dtsession;
                dtsession = _oo.Fetchdata(sqlsession);
                Accordion2.DataSource = dtsession.DefaultView;
                Accordion2.DataBind();
                if (Accordion2.Panes.Count > 0)
                {
                    for (int s = 0; s < dtsession.Rows.Count; s++)
                    {
                        Repeater rptFeeStructure = (Repeater)Accordion2.Panes[s].FindControl("rptFeeStructure");


                        Session["isDoubleClick"] = "0";
                        int isMinus = 0;
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
                                cmd.Parameters.AddWithValue("@SessionName", dtsession.Rows[s]["SessionName"].ToString().Trim());
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
                                        gtotal = gtotal + double.Parse(txtInstallmentPayable.Text == "" ? "0" : txtInstallmentPayable.Text);
                                        rptFee.DataSource = null;
                                        rptFee.DataBind();
                                        using (SqlCommand cmd1 = new SqlCommand())
                                        {
                                            cmd1.Connection = conn;
                                            cmd1.CommandText = "GetCompositFee";
                                            cmd1.CommandType = CommandType.StoredProcedure;
                                            cmd1.Parameters.AddWithValue("@SrNo", SrNo.ToString().Trim());
                                            cmd1.Parameters.AddWithValue("@SessionName", dtsession.Rows[s]["SessionName"].ToString().Trim());
                                            cmd1.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString().Trim());
                                            cmd1.Parameters.AddWithValue("@PMtMode", "Cash");
                                            cmd1.Parameters.AddWithValue("@MODForFeeDeposit", "I");
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
                                                    string ss1 = "select receiptStatus from CompositFeeDeposit  where FeeHeadId=" + lblFeeheadId.Text + " and InstallmentId=" + lblInstallmentId.Text + " and SrNo='" + SrNo + "' and SessionName='" + dtsession.Rows[s]["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
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
                                                        cmd2.Parameters.AddWithValue("@SessionName", dtsession.Rows[s]["SessionName"].ToString().Trim());
                                                        cmd2.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString().Trim());
                                                        cmd2.Parameters.AddWithValue("@PMtMode", "Cash");
                                                        cmd2.Parameters.AddWithValue("@MODForFeeDeposit", "I");
                                                        cmd2.Parameters.AddWithValue("@completefeeCheck", "0");
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
                                                    string rec1 = "select Template from FeereceiptTemplate where SessionName='" + dtsession.Rows[s]["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
                                                    if (_oo.Duplicate(rec1))
                                                    {
                                                        if (_oo.ReturnTag(rec1, "Template").ToString().ToLower() == "template 1")
                                                        {
                                                            HyperLink HylnkReceiptNo = (HyperLink)rptHistory.Items[k].FindControl("HylnkReceiptNo");
                                                            HylnkReceiptNo.Visible = true;
                                                            HylnkReceiptNo.NavigateUrl = "../2/FeeReceiptAllDuplicate.aspx?IsIfo=1&&RecieptSrNo=" + ds.Tables[1].Rows[k]["ReceiptNo"].ToString().Replace("/", "__") + "$" + ds.Tables[1].Rows[k]["SessionName"].ToString() + "$" + ds.Tables[1].Rows[k]["BranchCode"].ToString();
                                                        }
                                                        else
                                                        {
                                                            HyperLink HylnkReceiptNoT2 = (HyperLink)rptHistory.Items[k].FindControl("HylnkReceiptNoT2");
                                                            HylnkReceiptNoT2.Visible = true;
                                                            HylnkReceiptNoT2.NavigateUrl = "../2/FeeReceiptAllT2.aspx?IsIfo=1&&RecieptSrNo=" + ds.Tables[1].Rows[k]["ReceiptNo"].ToString().Replace("/", "__") + "$" + ds.Tables[1].Rows[k]["SessionName"].ToString() + "$" + ds.Tables[1].Rows[k]["BranchCode"].ToString();
                                                        }
                                                    }
                                                    else
                                                    {
                                                        HyperLink HylnkReceiptNo = (HyperLink)rptHistory.Items[k].FindControl("HylnkReceiptNo");
                                                        HylnkReceiptNo.Visible = true;
                                                        HylnkReceiptNo.NavigateUrl = "../2/FeeReceiptAllDuplicate.aspx?IsIfo=1&&RecieptSrNo=" + ds.Tables[1].Rows[k]["ReceiptNo"].ToString().Replace("/", "__") + "$" + ds.Tables[1].Rows[k]["SessionName"].ToString() + "$" + ds.Tables[1].Rows[k]["BranchCode"].ToString();
                                                    }

                                                }
                                            }

                                        }
                                    }
                                    if (gtotal == 0)
                                    {
                                        //lnkSubmit.Visible = false;
                                    }
                                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "ChangeRowColor();", true);
                                }

                                if (isMinus > 0)
                                {
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
                        Label lblInstallmentFee_total = (Label)Accordion2.Panes[s].FindControl("lblInstallmentFee_total");
                        Label lblDisvount_total = (Label)Accordion2.Panes[s].FindControl("lblDisvount_total");
                        Label lblTotal_total = (Label)Accordion2.Panes[s].FindControl("lblTotal_total");
                        Label lblPaid_total = (Label)Accordion2.Panes[s].FindControl("lblPaid_total");
                        Label lblBalance_total = (Label)Accordion2.Panes[s].FindControl("lblBalance_total");
                        Label lblConsolidated_total = (Label)Accordion2.Panes[s].FindControl("lblConsolidated_total");
                        lblInstallmentFee_total.Text = InstallmentFee_total.ToString("0.00");
                        lblDisvount_total.Text = Disvount_total.ToString("0.00");
                        lblTotal_total.Text = Total_total.ToString("0.00");
                        lblPaid_total.Text = Paid_total.ToString("0.00");
                        lblBalance_total.Text = Balance_total.ToString("0.00");
                        lblConsolidated_total.Text = Consolidated_total.ToString("0.00");
                    }
                }
            }
            catch (SqlException ex)
            {
            }
        }
        
        protected void grdDownloadedPunch_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
                e.Row.TableSection = TableRowSection.TableHeader;
        }
        private void GetStudentRemarkReport(string studentId)
        {
            try
            {
                _sql = "Select *from StudentOfficialDetails where blocked='Yes' and srno='" + studentId + "'";
                // ReSharper disable once UnusedVariable
                var sql1 = "Select Promotion,MODForFeeDeposit from StudentOfficialDetails where srno='" + studentId + "' and SessionName='" + Session["SessionName"] + "' and BranchCode="+Session["BranchCode"] +"";
                var param = new List<SqlParameter>
                {
                    (new SqlParameter("@QueryFor", "R")),
                    (new SqlParameter("@SrNo", studentId)),
                    (new SqlParameter("@SessionName", Session["SessionName"].ToString())),
                    (new SqlParameter("@BranchCode", Session["BranchCode"].ToString()))
                };
                grdDownloadedPunch.DataSource = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("USP_StudentRemarkDetails", param);
                grdDownloadedPunch.DataBind();
                LoadAttWd(w1, studentId);
                if (grdDownloadedPunch.Rows.Count > 0)
                {
                    divreport.Visible = true;
                }
                else
                {
                    divreport.Visible = false;
                    //Campus camp = new Campus(); camp.msgbox(Page, divmsgrecord, "Sorry, No Remark found!", "A");
                }
            }
            catch
            {
                // ignored
            }
        }
        public string LoadAttWd(Control parentId, String srniid)
        {
            var msg = "";
            try
            {
                Session["id"] = srniid;
                // ReSharper disable once StringLiteralTypo
                const string path = "~/admin/usercontrol/widgets/StudentRemark.ascx";
                var uc = LoadControl(path);
                parentId.Controls.Add(uc);
                if (grdDownloadedPunch.Rows.Count > 0)
                {
                    divreport.Visible = true;
                }
                else
                {
                    //Campus camp = new Campus(); camp.msgbox(Page, divmsgrecord, "Sorry, No Remark found!", "A");
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            Session["id"] = string.Empty;
            return msg;
        }
        private void LoadGrid(string studentId)
        {
            string sql = "SELECT bir.accessionno, lie.Title, CONVERT(NVARCHAR(11), bir.issuedate, 106) IssueDate, ";
            sql= sql+ " CONVERT(NVARCHAR(11), DATEADD(dd, (SELECT ISNULL(R1, 0)FROM RulesForLibrary WHERE ";
            sql = sql + " RulesForLibrary.BranchCode = " + Session["BranchCode"] + "), bir.issuedate),106) ReturnDate, bir.sessionname, case when isreturn=1 then 'Yes' else 'No' end status,";
            sql = sql + " format(bir.ReturnDate,'dd-MMM-yyyy hh:mm tt') returnon FROM AllStudentRecord_UDF('" + Session["SessionName"] + "', "+ Session["BranchCode"] + ") asr ";
            sql = sql + " INNER JOIN BookIssueReturn bir ON bir.srno = asr.SrNo AND bir.branchcode ="+ Session["BranchCode"] + " ";
            sql = sql + " INNER JOIN LibraryItemEntry lie ON lie.AccessionNo = bir.accessionno ";
            sql = sql + " where asr.SrNo= '"+ studentId + "'  ORDER BY bir.issuedate DESC; ";
            var dt = _oo.Fetchdata(sql);
            if (dt.Rows.Count > 0)
            {
                grdDocList.DataSource = dt;
                grdDocList.DataBind();
            }
            else
            {
                grdDocList.DataSource = null;
                grdDocList.DataBind();
            }
        }
        public void Fillmonth()
        {
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter("@QueryFor", "S"));
            param.Add(new SqlParameter("@SessionName", Session["SessionName"].ToString()));
            param.Add(new SqlParameter("@BranchCode", Session["BranchCode"].ToString()));

            DataSet ds;
            ds = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("GetMonthNamebwTwoSession_Proc", param);
            DataTable dt;
            dt = ds.Tables[0];

            Accordion1.DataSource = dt.DefaultView;
            Accordion1.DataBind();

            Session["Srno"] = ViewState["srno"];
            BLL.BLLInstance.LoadControls("~/sp/userControl/attendanceataGlance.ascx", W3);
        }
        private void Loadgrid(string studentId)
        {
            for (int i = 0; i < Accordion1.Panes.Count; i++)
            {
                GridView grd = (GridView)Accordion1.Panes[i].FindControl("GridView1");
                Label lblMonth = (Label)Accordion1.Panes[i].FindControl("lblMonth");
                Label lblYear = (Label)Accordion1.Panes[i].FindControl("lblYear");

                List<SqlParameter> param = new List<SqlParameter>();
                param.Add(new SqlParameter("@SessionName", Session["SessionName"].ToString()));
                param.Add(new SqlParameter("@BranchCode", Session["BranchCode"].ToString()));
                param.Add(new SqlParameter("@MonthName", lblMonth.Text.Trim()));
                param.Add(new SqlParameter("@Year", lblYear.Text.Trim()));
                param.Add(new SqlParameter("@Srno", studentId));

                DataSet ds;
                ds = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("GetStudentMonthlyAtt_Proc", param);

                grd.DataSource = ds;
                grd.DataBind();
            }

            Accordion1.SelectedIndex = Accordion1.Panes.Count - 1;
        }

        private void CountTotalPresentAbsent()
        {
            for (int i = 0; i < Accordion1.Panes.Count; i++)
            {
                GridView grd = (GridView)Accordion1.Panes[i].FindControl("GridView1");
                int present = 0;
                int absent = 0;
                for (int j = 0; j < grd.Rows.Count; j++)
                {
                    Label lblAttendance = (Label)grd.Rows[j].FindControl("lblAttendance");
                    if (lblAttendance.Text == "Absent")
                    {
                        absent += 1;
                    }
                    else
                    {
                        present += 1;
                    }
                    Label lblTotalPrs = (Label)grd.FooterRow.FindControl("lblTotalPrs");
                    Label lblTotalAb = (Label)grd.FooterRow.FindControl("lblTotalAb");

                    lblTotalPrs.Text = present.ToString();
                    lblTotalAb.Text = absent.ToString();
                }
            }
        }
        protected void TxtEnter_OnTextChanged(object sender, EventArgs e)
        {
            var studentId = Request.Form[hfStudentId.UniqueID];
            if (studentId != null)
            {
                if (string.IsNullOrEmpty(studentId))
                {
                    studentId = TxtEnter.Text.Trim();
                }
            }
            else
            {
                studentId = TxtEnter.Text.Trim();
            }
            ViewState["srno"] = studentId;
            GetStudentRemarkReport(studentId);
            GetData(studentId);
            LoadGrid(studentId);
            Fillmonth();
            Loadgrid(studentId);
            CountTotalPresentAbsent();
            HostelFee(studentId);
            loadGridTransportAllot(studentId);
        }
        private void loadGridTransportAllot( string studentId)
        {
            string sql = "select top(1) rm.VehicleType, locationName, vd.VehicleNo, Driver, driverContact  from StudentVehicleAllotment vi";
            sql = sql + " inner join AllStudentRecord_UDF('" + Session["SessionName"] + "', " + Session["BranchCode"] + ") asr on asr.SrNo=vi.SrNo and asr.BranchCode=vi.BranchCode and asr.SessionName=vi.SessionName ";
            sql = sql + " left join LocationMaster pp on pp.locationId=vi.LocationId and pp.BranchCode=vi.BranchCode and pp.SessionName=vi.SessionName ";
            sql = sql + " inner join VehicleMaster rm on rm.Id=vi.VehicleId  and rm.BranchCode=vi.BranchCode ";
            sql = sql + " inner join VehicleDetails vd on vd.Id=vi.VehicleId  and vd.BranchCode=vi.BranchCode ";
            sql = sql + " where asr.SrNo='" + studentId + "' and vi.BranchCode=" + Session["BranchCode"] + " and vi.SessionName='" + Session["SessionName"] + "' ";
            var dt = _oo.Fetchdata(sql);
            if (dt.Rows.Count > 0)
            {
                GridTransportAllot.DataSource = dt;
                GridTransportAllot.DataBind();
            }
            else
            {
                GridTransportAllot.DataSource = null;
                GridTransportAllot.DataBind();
            }
        }
        protected void HostelFee(string SrNoOrEmpId)
        {
            GridHostal.Visible = true;
           
            string sql = "select (hcm.CategoryName+' Building '+hlm.BuildingLocation+' room no '+rm.RoomNo+'('+hrm.RoomType+') and bed no '+hbm.BedNo) allotedRoom, hbm.PaymentType,";
            sql = sql + " case when ra.SrType = 'Student' then(select Name from AllStudentRecord_UDF('" + Session["SessionName"] + "', " + Session["BranchCode"] + ") where SrNo = ra.SrNoOrEmpId) else (select(EFirstName + ' ' + EMiddleName + ' ' + ELastName) from EmpGeneralDetail where EmpId = ra.SrNoOrEmpId) end name,";
            sql = sql + " case when ra.SrType = 'Student' then(select CombineClassName from AllStudentRecord_UDF('" + Session["SessionName"] + "', " + Session["BranchCode"] + ") where SrNo = ra.SrNoOrEmpId) else 'Staff' end Class, * ";
            sql = sql + " from RoomAllotment ra";
            sql = sql + " inner join HostelCategoryMaster hcm on hcm.Id = ra.HostelCategoryId inner join HostelLocationMaster hlm on hlm.Id = ra.BuildingLocationId";
            sql = sql + " inner join RoomMaster rm on rm.Id = ra.RoomId inner join HostelRoomTypeMaster hrm on hrm.Id = ra.RoomTypeId inner join HostelBedMaster hbm on hbm.Id = ra.BedId";
            sql = sql + " where ra.sessionname='" + Session["SessionName"] + "' and hlm.BranchCode=" + Session["BranchCode"].ToString() + " and hcm.BranchCode=" + Session["BranchCode"].ToString() + " and ra.BranchCode=" + Session["BranchCode"].ToString() + " and rm.BranchCode=" + Session["BranchCode"].ToString() + " and ra.LivingStatus = 1 and ra.SrNoOrEmpId= case when '" + SrNoOrEmpId + "'='' then ra.SrNoOrEmpId else '" + SrNoOrEmpId + "' end";
            var dt = _oo.Fetchdata(sql);
            GridHostal.DataSource = dt;
            GridHostal.DataBind();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Label txtBedStatusDamaged = (Label)GridHostal.Rows[i].FindControl("txtBedStatusDamaged");
                Label lblRoomAllotmentId = (Label)GridHostal.Rows[i].FindControl("lblRoomAllotmentId");
                Label txtBedStatusOk = (Label)GridHostal.Rows[i].FindControl("txtBedStatusOk");
                Label txtBookingStatus = (Label)GridHostal.Rows[i].FindControl("txtBookingStatus");
                Label txtBookingUnavailable = (Label)GridHostal.Rows[i].FindControl("txtBookingUnavailable");
                LinkButton LinkButton3 = (LinkButton)GridHostal.Rows[i].FindControl("LinkButton3");
                Label txtMode = (Label)GridHostal.Rows[i].FindControl("txtMode");

                if (dt.Rows[i]["LivingStatus"].ToString().ToLower() == "true")
                {
                    txtBookingStatus.Visible = true;
                    txtBookingUnavailable.Visible = false;
                }
                else
                {
                    txtBookingStatus.Visible = false;
                    txtBookingUnavailable.Visible = true;
                }
            }
        }
        protected void LinkButton1_OnClick(object sender, EventArgs e)
        {
            var studentId = Request.Form[hfStudentId.UniqueID];
            if (studentId != null)
            {
                if (string.IsNullOrEmpty(studentId))
                {
                    studentId = TxtEnter.Text.Trim();
                }
            }
            else
            {
                studentId = TxtEnter.Text.Trim();
            }
            ViewState["srno"] = studentId;
            GetStudentRemarkReport(studentId);
            GetData(studentId);
            LoadGrid(studentId);
            Fillmonth();
            Loadgrid(studentId);
            CountTotalPresentAbsent();
            HostelFee(studentId);
            loadGridTransportAllot(studentId);
        }

        protected void chkAll2_OnCheckedChanged(object sender, EventArgs e)
        {
        
        }
        public override void Dispose()
        {
            _con.Dispose();
            _oo.Dispose();
        }

        protected DataSet GetFineforArrear(string registervalue, string installment, string sessionname)
        {
            //decimal lateFine = 0;
            var param = new List<SqlParameter>
            {
                new SqlParameter("@registervalue", registervalue),
                new SqlParameter("@installment", installment),
                new SqlParameter("@sessionName", sessionname)
            };
            var ds = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("", param);
            return ds;
        }

        public bool ApplyFineRule(int index)
        {
            var flag = false;
            // ReSharper disable once RedundantAssignment
            List<SqlParameter> param = new List<SqlParameter>();
            DataSet ds;
            DataTable dt;

            param = new List<SqlParameter>();
            param.Add(new SqlParameter("@QueryFor", "S"));
            param.Add(new SqlParameter("@SessionName", Session["SessionName"].ToString()));
            ds = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("RulesForFineProc", param);
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        flag = dt.Rows[0][index].ToString() == "1";
                    }
                }
            }

            return flag;
        }

        protected DataSet GetFine(string registervalue, string installment, string sessionname)
        {
            //decimal lateFine = 0;
            var param = new List<SqlParameter>
            {
                new SqlParameter("@registervalue", registervalue),
                new SqlParameter("@installment", installment),
                new SqlParameter("@sessionName", sessionname)
            };



            var ds = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("USP_GetFine", param);

            return ds;
        }

        public bool IsFineExempted(string srno, string installment)
        {
            var param = new List<SqlParameter>
            {
                new SqlParameter("@SessionName", Session["SessionName"].ToString()),
                new SqlParameter("@BranchCode", Session["BranchCode"].ToString()),
                new SqlParameter("@Srno", srno),
                new SqlParameter("@Installment", installment)
            };

            return Convert.ToBoolean(DLL.objDll.Sp_SelectRecord_usingExecuteScalar("USP_IsFineExempted", param));
        }

        public List<decimal> Get_AutometicDiscountAmount(string registervalue, string installmentName)
        {
            var discamount = new List<decimal>();

            decimal discountamount1, discountamount2, discountamount3, discountamount4;
            var autoDisc = "0";
            var manualDisc = "0";
            var fconcDisc = "0";
            var cconcDisc = "0";

            var param = new List<SqlParameter>
            {
                new SqlParameter("@RegisterValue", registervalue),
                new SqlParameter("@InstallmentName", installmentName),
                new SqlParameter("@SessionName", Session["SessionName"].ToString()),
                new SqlParameter("@BranchCode", Session["BranchCode"].ToString()),
                new SqlParameter("@isAnnualDeposit", _isAnualDeposit ? "A" : "I")
            };



            var ds = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("USP_GetAutometicDiscountAmount", param);
            // ReSharper disable once UseNullPropagation
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    autoDisc = ds.Tables[0].Rows[0]["AutoDisc"].ToString();
                    manualDisc = ds.Tables[0].Rows[0]["ManualDisc"].ToString();
                    fconcDisc = ds.Tables[0].Rows[0]["FConc"].ToString();
                    string sql = "SELECT R1 FROM dbo.RulesForFee WHERE SessionName ='" + Session["SessionName"] + "'";
                    if (_oo.ReturnTag(sql, "R1") != "1")
                    {
                        cconcDisc = ds.Tables[0].Rows[0]["CConc"].ToString();
                    }

                }
            }
            decimal.TryParse(autoDisc, out discountamount1);
            decimal.TryParse(manualDisc, out discountamount2);
            decimal.TryParse(fconcDisc, out discountamount3);
            decimal.TryParse(cconcDisc, out discountamount4);

            var totaldiscount = discountamount1 + discountamount2 + discountamount3 + discountamount4;

            discamount.Add(discountamount1);
            discamount.Add(discountamount2);
            discamount.Add(discountamount3);
            discamount.Add(discountamount4);
            discamount.Add(totaldiscount);


            return discamount;
        }

        protected DataSet Paidamount(string registervalue, string installment, string sessionname)
        {
            var param = new List<SqlParameter>
            {
                new SqlParameter("@registervalue", registervalue),
                new SqlParameter("@installment", installment),
                new SqlParameter("@sessionName", sessionname)
            };

            var ds = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("USP_GETFeeDetils", param);

            return ds;

        }

        protected decimal GetDueAmt(string amount, string conveyance, string latefee, string exemption, string conc)
        {
            // ReSharper disable once InconsistentNaming
            decimal Amount;
            // ReSharper disable once InconsistentNaming
            decimal Conveyance;
            // ReSharper disable once InconsistentNaming
            decimal Latefee;
            // ReSharper disable once InconsistentNaming
            decimal Exemption;
            // ReSharper disable once InconsistentNaming
            decimal Conc;
            decimal.TryParse(amount, out Amount);
            decimal.TryParse(conveyance, out Conveyance);
            decimal.TryParse(latefee, out Latefee);
            decimal.TryParse(exemption, out Exemption);
            decimal.TryParse(conc, out Conc);

            if ((Math.Abs(Exemption) + Math.Abs(Math.Abs(Exemption) - Math.Abs(Conc))) == Amount + Conveyance)
            {
                return Amount + Conveyance - (Math.Abs(Exemption) + Math.Abs(Math.Abs(Exemption) - Math.Abs(Conc)));
            }
            else
            {
                return Amount + Conveyance + Latefee - (Math.Abs(Exemption) + Math.Abs(Math.Abs(Exemption) - Math.Abs(Conc)));
            }

        }

        protected decimal GetDueAmtAgain(string amount, string conveyance, string latefee, string chequebouncefee, string exemption, string conc)
        {
            // ReSharper disable once InconsistentNaming
            decimal Amount;
            // ReSharper disable once InconsistentNaming
            decimal Conveyance;
            // ReSharper disable once InconsistentNaming
            decimal Latefee;
            // ReSharper disable once InconsistentNaming
            decimal ChequeBounceFee;
            // ReSharper disable once InconsistentNaming
            decimal Exemption;
            // ReSharper disable once InconsistentNaming
            decimal Conc;
            decimal.TryParse(amount, out Amount);
            decimal.TryParse(conveyance, out Conveyance);
            decimal.TryParse(latefee, out Latefee);
            decimal.TryParse(chequebouncefee, out ChequeBounceFee);
            decimal.TryParse(exemption, out Exemption);
            decimal.TryParse(conc, out Conc);

            return Amount + Conveyance + Latefee + ChequeBounceFee - (Math.Abs(Exemption) + Math.Abs(Math.Abs(Exemption) - Math.Abs(Conc)));

        }

        // ReSharper disable once ParameterHidesMember
        protected void GetHistory(GridView grdHistory, string registervalue, string installment, string sessionname)
        {
            var param = new List<SqlParameter>
            {
                new SqlParameter("@registervalue", registervalue),
                new SqlParameter("@installment", installment),
                new SqlParameter("@sessionName", sessionname)
            };
            var ds = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("USP_FeeHistoryInstallmentWise", param);
            grdHistory.DataSource = ds;
            grdHistory.DataBind();
        }
        protected void rptFeeStructure_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            var studentId = Request.Form[hfStudentId.UniqueID];
            if (string.IsNullOrEmpty(studentId))
            {
                studentId = TxtEnter.Text.Trim();
            }
            if (e.Item.ItemType != ListItemType.Item && e.Item.ItemType != ListItemType.AlternatingItem) return;
            var lblInstallment = (Label)e.Item.FindControl("lblInstallment");
            var lblPaidLateFee = (Label)e.Item.FindControl("lblPaidLateFee");
            var lblCurrentLatefee = (Label)e.Item.FindControl("lblCurrentLatefee");
            var lblLateFee = (Label)e.Item.FindControl("lblLateFee");
            var lblPaidBouncedFee = (Label)e.Item.FindControl("lblPaidBouncedFee");
            var lblCurrentBounceFee = (Label)e.Item.FindControl("lblCurrentBounceFee");
            var lblAmountPaid = (Label)e.Item.FindControl("lblAmountPaid");
            var lblConveyancePaid = (Label)e.Item.FindControl("lblConveyancePaid");
            var lblTotalCbFine = (Label)e.Item.FindControl("lblTotalCBFine");
            var lblTotalFine = (Label)e.Item.FindControl("lblTotalFine");
            //var chk = (CheckBox)e.Item.FindControl("chk");
            decimal value1 = 0; decimal value2 = 0;
            if (lblInstallment.Text == "Arrear")
            {
                var dsforfine = GetFineforArrear(studentId, lblInstallment.Text, Session["SessionName"].ToString());
                if (dsforfine != null)
                {
                    lblPaidLateFee.Text = dsforfine.Tables[0].Rows[0][0].ToString();
                    decimal.TryParse(dsforfine.Tables[0].Rows[0][0].ToString(), out value1);

                    if (ApplyFineRule(1))
                    {
                        lblCurrentLatefee.Text = dsforfine.Tables[0].Rows[0][1].ToString();
                        decimal.TryParse(dsforfine.Tables[0].Rows[0][1].ToString(), out value2);
                    }
                    else
                    {
                        lblCurrentLatefee.Text = "0.00";
                        value2 = 0;
                    }

                    lblLateFee.Text = (value1 + value2).ToString(CultureInfo.InvariantCulture);
                }
            }
            else
            {
                var dsforfine = GetFine(studentId, lblInstallment.Text, Session["SessionName"].ToString());
                if (dsforfine != null)
                {
                    lblPaidLateFee.Text = dsforfine.Tables[0].Rows[0][0].ToString();
                    decimal.TryParse(dsforfine.Tables[0].Rows[0][0].ToString(), out value1);

                    if (IsFineExempted(studentId, lblInstallment.Text))
                    {
                        lblCurrentLatefee.Text = "0.00";
                        value2 = 0;
                    }
                    else
                    {
                        lblCurrentLatefee.Text = dsforfine.Tables[0].Rows[0][1].ToString();
                        decimal.TryParse(dsforfine.Tables[0].Rows[0][1].ToString(), out value2);
                    }

                    lblLateFee.Text = (value1 + value2).ToString(CultureInfo.InvariantCulture);
                }
            }

            // ReSharper disable once LocalVariableHidesMember
            var lblAutoDisc = (Label)e.Item.FindControl("lblAutoDisc");
            // ReSharper disable once LocalVariableHidesMember
            var lblManualDisc = (Label)e.Item.FindControl("lblManualDisc");

            var lblFConc = (Label)e.Item.FindControl("lblFConc");
            var lblCConc = (Label)e.Item.FindControl("lblCConc");

            var lblExemption = (Label)e.Item.FindControl("lblExemption");

            // ReSharper disable once LocalVariableHidesMember
            var txtExemption = (TextBox)e.Item.FindControl("txtExemption");

            // ReSharper disable once RedundantAssignment
            var discountvalues = new List<decimal>();

            discountvalues = Get_AutometicDiscountAmount(studentId, lblInstallment.Text);

            lblAutoDisc.Text = discountvalues[0].ToString(CultureInfo.InvariantCulture);
            lblManualDisc.Text = discountvalues[1].ToString(CultureInfo.InvariantCulture);

            lblFConc.Text = discountvalues[2].ToString(CultureInfo.InvariantCulture);
            lblCConc.Text = discountvalues[3].ToString(CultureInfo.InvariantCulture);

            lblExemption.Text = discountvalues[4].ToString(CultureInfo.InvariantCulture);
            txtExemption.Text = Math.Abs(discountvalues[4]).ToString(CultureInfo.InvariantCulture);

            var lblAmount = (Label)e.Item.FindControl("lblAmount");
            // ReSharper disable once LocalVariableHidesMember
            var lblConveyance = (Label)e.Item.FindControl("lblConveyance");

            var lblDueAmt = (Label)e.Item.FindControl("lblDueAmt");

            // ReSharper disable once InconsistentNaming
            decimal Amount;
            // ReSharper disable once InconsistentNaming
            decimal Conveyance;
            // ReSharper disable once InconsistentNaming
            decimal Exemption;
            // ReSharper disable once InconsistentNaming
            decimal Conc;
            decimal.TryParse(lblAmount.Text, out Amount);
            decimal.TryParse(lblConveyance.Text, out Conveyance);

            decimal.TryParse(lblExemption.Text, out Exemption);
            decimal.TryParse(txtExemption.Text, out Conc);

            var latefee = lblLateFee.Text;

            if ((Math.Abs(Exemption) + Math.Abs(Math.Abs(Exemption) - Math.Abs(Conc))) == Amount + Conveyance)
            {
                lblLateFee.Text = "0.00";
            }



            var lblPaidAmt = (Label)e.Item.FindControl("lblPaidAmt");
            var lblBalanceAmt = (Label)e.Item.FindControl("lblBalanceAmt");


            var insttalment = lblInstallment.Text;
            if (insttalment == "Arrear")
            {
                insttalment = "Arrier";
            }

            var ds = Paidamount(studentId, insttalment, Session["SessionName"].ToString());

            decimal paidBouncedFee = 0;

            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {


                    lblPaidAmt.Text = ds.Tables[0].Rows[0][0].ToString() == string.Empty ? "0.00" : ds.Tables[0].Rows[0][0].ToString();

                    lblConveyancePaid.Text = ds.Tables[0].Rows[0][1].ToString();
                    lblAmountPaid.Text = ds.Tables[0].Rows[0][2].ToString();

                    lblPaidBouncedFee.Text = ds.Tables[0].Rows[0][3].ToString();

                    decimal.TryParse(lblPaidBouncedFee.Text, out paidBouncedFee);
                }
            }
            else
            {
                lblPaidAmt.Text = "0.00";
            }



            decimal paidamounts, dueamounts;
            decimal.TryParse(lblPaidAmt.Text, out paidamounts);

            if (lblAmountPaid.Text != string.Empty)
            {
                if (!ApplyFineRule(0))
                {
                    lblCurrentLatefee.Text = "0.00";
                    value2 = 0;
                    lblLateFee.Text = (value1 + value2).ToString(CultureInfo.InvariantCulture);
                }
            }

            lblDueAmt.Text = GetDueAmt(lblAmount.Text, lblConveyance.Text, lblLateFee.Text, lblExemption.Text, txtExemption.Text).ToString(CultureInfo.InvariantCulture);

            if ((Math.Abs(Exemption) + Math.Abs(Math.Abs(Exemption) - Math.Abs(Conc))) == Amount + Conveyance)
            {
                if (paidamounts == 0)
                {
                    decimal.TryParse(lblDueAmt.Text, out dueamounts);

                    lblDueAmt.Text = (dueamounts + paidBouncedFee).ToString(CultureInfo.InvariantCulture);

                    decimal.TryParse(lblDueAmt.Text, out dueamounts);

                    lblBalanceAmt.Text = (dueamounts - paidamounts).ToString(CultureInfo.InvariantCulture);
                }
                else
                {
                    lblLateFee.Text = latefee;

                    decimal.TryParse(latefee, out dueamounts);

                    lblDueAmt.Text = (dueamounts).ToString(CultureInfo.InvariantCulture);

                    lblBalanceAmt.Text = (dueamounts - paidamounts).ToString(CultureInfo.InvariantCulture);
                }

            }
            else
            {
                decimal.TryParse(lblDueAmt.Text, out dueamounts);

                lblDueAmt.Text = (dueamounts + paidBouncedFee).ToString(CultureInfo.InvariantCulture);

                decimal.TryParse(lblDueAmt.Text, out dueamounts);

                lblBalanceAmt.Text = (dueamounts - paidamounts).ToString(CultureInfo.InvariantCulture);
            }

            string cbFineonInstallment = "";
            if (_count == 0)
            {
                DataSet chequeBounceFineAmount = BLL.BLLInstance.loadChequeBounceFineAmount(studentId);
                if (chequeBounceFineAmount != null)
                {
                    if (chequeBounceFineAmount.Tables.Count > 0)
                    {
                        if (chequeBounceFineAmount.Tables[0].Rows.Count > 0)
                        {
                            lblCurrentBounceFee.Text = chequeBounceFineAmount.Tables[0].Rows[0]["BouncedCharge"].ToString();
                            cbFineonInstallment = chequeBounceFineAmount.Tables[0].Rows[0]["Installment"].ToString();
                        }
                    }
                }
            }

            decimal currentBounceFee = 0;
            if (cbFineonInstallment == insttalment)
            {
                _count = 1;

                decimal.TryParse(lblCurrentBounceFee.Text, out currentBounceFee);

                lblTotalCbFine.Text = (currentBounceFee + paidBouncedFee).ToString(CultureInfo.InvariantCulture);
                lblDueAmt.Text = GetDueAmtAgain(lblAmount.Text, lblConveyance.Text, lblLateFee.Text, lblTotalCbFine.Text, lblExemption.Text, txtExemption.Text).ToString(CultureInfo.InvariantCulture);

                decimal.TryParse(lblPaidAmt.Text, out paidamounts);
                decimal.TryParse(lblDueAmt.Text, out dueamounts);

                lblBalanceAmt.Text = (dueamounts - paidamounts).ToString(CultureInfo.InvariantCulture);

                if (lblBalanceAmt.Text == "0.00")
                {
                    //chk.Visible = false;
                    lblExemption.Visible = true;
                
                }
                else
                {
                    _isfullpaid = false;
                    lblExemption.Visible = false;
                }

                txtExemption.Visible = false;
                // ReSharper disable once ConvertIfStatementToConditionalTernaryExpression
                //if (grdStRecord.Rows[0].BackColor == System.Drawing.Color.Red)
                //{
                //    chk.Visible = false;
                //}
                //else
                //{
                //    chk.Visible = true;
                //}
            }

            lblTotalCbFine.Text = (currentBounceFee + paidBouncedFee).ToString(CultureInfo.InvariantCulture);
            lblTotalFine.Text = (value1 + value2 + currentBounceFee + paidBouncedFee).ToString(CultureInfo.InvariantCulture);

            decimal value;
            decimal.TryParse(lblBalanceAmt.Text, out value);
            var lblNextDue = (Label)e.Item.FindControl("lblNextDue");

            _nextDue = _nextDue + value;
            lblNextDue.Text = _nextDue.ToString(CultureInfo.InvariantCulture);

            //var chkisConpaid = (CheckBox)e.Item.FindControl("chkisConpaid");
            if (lblNextDue.Text == "0.00")
            {
                //chk.Visible = false;
                lblExemption.Visible = true;

                var tr = (HtmlTableRow)e.Item.FindControl("tr1");

                tr.Style.Add("background-color", "#79c500");
                tr.Style.Add("color", "#fff");


                //chkisConpaid.Visible = false;

            }
            else
            {
                _isfullpaid = false;
                lblExemption.Visible = false;
                //if (lblConveyancePaid.Text == "T")
                //{
                //    chkisConpaid.Visible = false;
                //}
                if (lblConveyance.Text != "0.00")
                {
                    if (_isConvAlloted == 0)
                    {
                        _isConvAlloted = 1;
                    }
                }
            }
        
            var acorPreviousHistory = (AccordionPane)e.Item.FindControl("AcorPreviousHistory");
            // ReSharper disable once LocalVariableHidesMember
            var grdHistory = (GridView)acorPreviousHistory.FindControl("grdHistory");
            GetHistory(grdHistory, studentId, insttalment, Session["SessionName"].ToString());

            switch (grdHistory.Rows.Count)
            {
                case 0:
                    acorPreviousHistory.Visible = false;
                    break;
                default:
                    acorPreviousHistory.Visible = true;
                    break;
            }

            lblExemption.Visible = true;
            txtExemption.Visible = false;
        }

    }
}
