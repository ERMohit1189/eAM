using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class UniformFeeDepositReport : Page
{
    private SqlConnection _con = new SqlConnection();
    private readonly Campus _oo = new Campus();
    private string _sql = string.Empty;
    private string sql = string.Empty;

    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
        {
            Response.Redirect("default.aspx");
        }

        _con = _oo.dbGet_connection();
        Campus camp = new Campus(); camp.LoadLoader(loaders);  //in cs file

        header1.Controls.Clear();
        BLL.BLLInstance.LoadHeader("Report", header1);
        if (!IsPostBack)
        {
            ImageButton1.Visible = false;
            ImageButton2.Visible = false;
            ImageButton3.Visible = false;
            ImageButton4.Visible = false;
            Label1.Visible = false;
            _oo.AddDateMonthYearDropDown(FromYY, FromMM, FromDD);
            _oo.AddDateMonthYearDropDown(ToYY, ToMM, ToDD);
            _oo.FindCurrentDateandSetinDropDown(FromYY, FromMM, FromDD);
            _oo.FindCurrentDateandSetinDropDown(ToYY, ToMM, ToDD);

            abc.Visible = false;
            string sql = "Select BranchId, BranchName from Branchtab";
            var dt = _oo.Fetchdata(sql);
            ddlBranch.DataSource = dt;
            ddlBranch.DataTextField = "BranchName";
            ddlBranch.DataValueField = "BranchId";
            ddlBranch.DataBind();
            ddlBranch.Items.Insert(0, new ListItem("<--Select-->", ""));
            ddlBranch.SelectedValue = Session["BranchCode"].ToString();

            string sqls = "select SessionName from SessionMaster where BranchCode=" + ddlBranch.SelectedValue + "";
            var dt2 = _oo.Fetchdata(sqls);
            drpSession.DataSource = dt2;
            drpSession.DataTextField = "SessionName";
            drpSession.DataValueField = "SessionName";
            drpSession.DataBind();
            drpSession.Items.Insert(0, new ListItem("<--All-->", ""));
            if (Session["LoginType"].ToString() == "Admin")
            {
                divBranch.Visible = false;
                //divSession.Visible = true;
            }
            else
            {
                divBranch.Visible = true;
                //divSession.Visible = true;
            }

            drpSession.Items.Insert(0, new ListItem("<--Select-->", ""));
            LoadUser();
        }
    }

    protected void LoadUser()
    {
        string sql = "";
        if (Session["LoginType"].ToString() == "Admin")
        {
            sql = "Select UserId From NewAdminInformation where BranchCode=" + Session["BranchCode"] + "";
        }
        else
        {
            sql = "Select UserId From NewAdminInformation where BranchCode=" + ddlBranch.SelectedValue + "";
        }
        _oo.FillDropDownWithOutSelect(sql, drpUsers, "UserId");
        drpUsers.Items.Insert(0, new ListItem("All", "All"));
    }
    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadUser();
        if (ddlBranch.SelectedIndex == 0)
        {
            drpSession.Items.Clear();
            drpSession.Items.Insert(0, new ListItem("<--Select Session-->", ""));
            return;
        }
        string sql = "select SessionName from SessionMaster where BranchCode=" + ddlBranch.SelectedValue + "";
        var dt2 = _oo.Fetchdata(sql);
        drpSession.DataSource = dt2;
        drpSession.DataTextField = "SessionName";
        drpSession.DataValueField = "SessionName";
        drpSession.DataBind();
        drpSession.Items.Insert(0, new ListItem("<--Select Session-->", ""));
        drpSession.SelectedIndex = (drpSession.Items.Count - 1);
        if (Session["LoginType"].ToString() == "Admin")
        {
            drpSession.SelectedValue = Session["SessionName"].ToString();
        }

    }
    protected void rdoType_SelectedIndexChanged(object sender, EventArgs e)
    {
        hfStaffId.Value = "";
        hfStudentId.Value = "";
        txtSearchStaff.Text = "";
        txtSearchStudent.Text = "";
        if (rdoType.SelectedIndex == 0)
        {
            divStaff.Visible = false;
            divStudent.Visible = true;
        }
        else
        {
            divStaff.Visible = true;
            divStudent.Visible = false;
        }
    }
    protected void txtSearchStudent_TextChanged(object sender, EventArgs e)
    {
        var studentId = Request.Form[hfStudentId.UniqueID];
        if (string.IsNullOrEmpty(studentId))
        {
            studentId = txtSearchStudent.Text.Trim();
        }
        Bind(studentId);
    }
    protected void txtSearchStaff_TextChanged(object sender, EventArgs e)
    {
        var StaffId = Request.Form[hfStaffId.UniqueID];
        if (string.IsNullOrEmpty(StaffId))
        {
            StaffId = txtSearchStaff.Text.Trim();
        }
        Bind(StaffId);
    }
    protected void lnkView_Click(object sender, EventArgs e)
    {
        Bind("");
    }
    protected void Bind(string txtSearch)
    {
        if (txtSearch == "")
        {
            hfStudentId.Value = "";
            txtSearchStudent.Text = "";
            hfStaffId.Value = "";
            txtSearchStaff.Text = "";
        }

        DataSet DS = new DataSet();
        string fromDate = (FromDD.SelectedValue + " " + FromMM.SelectedValue + " " + FromYY.SelectedValue).ToString();
        string toDate = (ToDD.SelectedValue + " " + ToMM.SelectedValue + " " + ToYY.SelectedValue).ToString();
        Label1.Text = "Product Sale Report from " + DateTime.Parse(fromDate).ToString("dd MMM yyyy") + " to " + DateTime.Parse(toDate).ToString("dd MMM yyyy");
        sql = "select top(1) SessionName from SessionMaster where '" + DateTime.Parse(toDate).ToString("dd MMM yyyy") + "' between convert(date, FromDate) and convert(date, ToDate)";
        string SessionName2 = _oo.ReturnTag(sql, "SessionName");
        _sql = "select asr.SrNo, name, fatherName, CombineClassName Class, Receipt_no ReceiptNo, DepositDate, AmtForPay Amount, Discount Exemption, PaidAmt Paid, NextDeuAmt NextDue, Mode, PaymentSatus";
        _sql = _sql + " from UniformFeeDeposit fd";
        _sql = _sql + " inner join AllStudentRecord_UDF('', " + ddlBranch.SelectedValue + ") asr on asr.SrNo = fd.SrNo ";
        _sql = _sql + " where fd.SrNo = case when '" + txtSearch + "'= '' then fd.SrNo else '" + txtSearch + "' end and fd.BranchCode=" + ddlBranch.SelectedValue + " and PaymentSatus =case when '" + drpStatus.SelectedValue + "' = 'All' then PaymentSatus else '" + drpStatus.SelectedValue + "' end";
        _sql = _sql + " and Mode =case when '" + drpPaymentMode.SelectedValue + "' = 'All' then Mode else '" + drpPaymentMode.SelectedValue + "' end and LoginName =case when '" + drpUsers.SelectedValue + "' = 'All' then LoginName else '" + drpUsers.SelectedValue + "' end and CONVERT(DATE, DepositDate) between '" + fromDate + "' and '" + toDate + "'";
        if (drpSession.SelectedIndex != 0)
        {
            _sql = _sql + "  and fd.SessionName = '" + drpSession.SelectedItem.Text + "' ";
        }
        string sts = "";
        if (drpSession.SelectedIndex != 0)
        {
            sts = sts + "Session : " + drpSession.SelectedItem.Text;
        }
        if (drpPaymentMode.SelectedIndex != 0)
        {
            if (drpSession.SelectedIndex != 0)
            {
                sts = sts + " | Mode : " + drpPaymentMode.SelectedValue;
            }
            else
            {
                sts = sts + "Mode : " + drpPaymentMode.SelectedValue;
            }
        }
        if (drpStatus.SelectedIndex != 0)
        {
            sql = sql + " and Status=" + drpStatus.SelectedValue + " ";
            if (drpPaymentMode.SelectedIndex != 0)
            {
                sts = sts + " | Status : " + drpStatus.SelectedValue;
            }
            else
            {
                sts = sts + "Status : " + drpStatus.SelectedValue;
            }
        }
        if (drpUsers.SelectedIndex != 0)
        {
            sql = sql + " and LoginName=" + drpUsers.SelectedValue + " ";
            sts = sts + " by " + drpUsers.SelectedValue;
        }
        lbloptions.Text = sts;
        DS = _oo.GridFill(_sql);
        if (DS != null && DS.Tables[0].Rows.Count > 0)
        {

            ImageButton1.Visible = false;
            ImageButton2.Visible = true;
            ImageButton3.Visible = false;
            ImageButton4.Visible = true;
            abc.Visible = true;
            GridOneTime.DataSource = _oo.GridFill(_sql);
            GridOneTime.DataBind();

            double TotalPaid = 0;
            Label lblTotalPaid = (Label)GridOneTime.FooterRow.FindControl("lblTotalPaid");
            for (int i = 0; i < GridOneTime.Rows.Count; i++)
            {
                Label lblPaidO = (Label)GridOneTime.Rows[i].FindControl("lblPaidO");
                TotalPaid += double.Parse(lblPaidO.Text);
            }
            lblTotalPaid.Text = TotalPaid.ToString();
        }
        else
        {
            abc.Visible = false;
            GridOneTime.DataSource = null;
            GridOneTime.DataBind();
        }
    }
    protected void FromYY_SelectedIndexChanged(object sender, EventArgs e)
    {
        _oo.YearDropDown(FromYY, FromMM, FromDD);
    }
    protected void FromMM_SelectedIndexChanged(object sender, EventArgs e)
    {
        _oo.MonthDropDown(FromYY, FromMM, FromDD);
    }

    protected void ToYY_SelectedIndexChanged(object sender, EventArgs e)
    {
        _oo.YearDropDown(ToYY, ToMM, ToDD);
    }
    protected void ToMM_SelectedIndexChanged(object sender, EventArgs e)
    {
        _oo.MonthDropDown(ToYY, ToMM, ToDD);
    }


    protected void ImageButton1_Click(object sender, EventArgs e)
    {
        _oo.ExportTolandscapeWord(Response, "AditionalFeeReport", gdv1);
    }
    protected void ImageButton2_Click(object sender, EventArgs e)
    {
        _oo.ExportDivToExcelWithFormatting(Response, "AditionalFeeReport.xls", gdv1, Server.MapPath("~/Admin/css/style.css"));
    }
    protected void ImageButton3_Click(object sender, EventArgs e)
    {
        _oo.ExporttolandscapePdf(Response, "AditionalFeeReport", abc);
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


    protected void btnView_Click(object sender, EventArgs e)
    {
        LinkButton link = (LinkButton)sender;
        LinkButton HostelReceiptNo = (LinkButton)link.NamingContainer.FindControl("btnView");
        Session["HostelReceiptNo"] = HostelReceiptNo.Text;
        Response.Redirect("HostelFeeRecieptInstallment_duplicate.aspx?print=1");
    }
}
