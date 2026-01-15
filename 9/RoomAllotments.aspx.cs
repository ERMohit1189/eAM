using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

public partial class admin_RoomAllotments : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection();
    Campus oo = new Campus();
    string sql = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
        {
            Response.Redirect("default.aspx");
        }
        con = oo.dbGet_connection();
        Campus camp = new Campus(); camp.LoadLoader(loader);
        if (!IsPostBack)
        {
            sql = "Select Id, CategoryName from HostelCategoryMaster where BranchCode=" + Session["BranchCode"].ToString() + "";
            oo.FillDropDown_withValue(sql, DrpCategory, "CategoryName", "Id");

            sql = "Select Id, RoomType from HostelRoomTypeMaster where BranchCode=" + Session["BranchCode"].ToString() + "";
            oo.FillDropDown_withValue(sql, DrpRoomType, "RoomType", "Id");
            DrpRoomType.Items.Insert(0, "Select");

            sql = "Select Id, BuildingLocation from HostelLocationMaster where BranchCode=" + Session["BranchCode"].ToString() + "";
            oo.FillDropDown_withValue(sql, DrpbuildingLocation, "BuildingLocation", "Id");

            oo.AddDateMonthYearDropDown(DDYearFrom, DDMonthFrom, DDDateFrom);
            oo.FindCurrentDateandSetinDropDown(DDYearFrom, DDMonthFrom, DDDateFrom);

            oo.AddDateMonthYearDropDown(DDYearTo, DDMonthTo, DDDateTo);
            oo.FindCurrentDateandSetinDropDown(DDYearTo, DDMonthTo, DDDateTo);
            div5.Visible = false;
        }
    }
    public static int GetMonthDifference(DateTime startDate, DateTime endDate)
    {
        int monthsApart = 12 * (startDate.Year - endDate.Year) + startDate.Month - endDate.Month;
        return Math.Abs(monthsApart);
    }
    protected void DDYearFrom_SelectedIndexChanged(object sender, EventArgs e)
    {
        LinkSubmit.Visible = false;
        oo.YearDropDown(DDYearFrom, DDMonthFrom, DDDateFrom);
        string fromdt = DateTime.Parse(DDDateFrom.SelectedValue + "-" + DDMonthFrom.SelectedValue + "-" + DDYearFrom.SelectedValue).ToString("dd-MMM-yyyy");
        string todt = DateTime.Parse(DDDateTo.SelectedValue + "-" + DDMonthTo.SelectedValue + "-" + DDYearTo.SelectedValue).ToString("dd-MMM-yyyy");
        string curdate = "select format(getdate(),'dd-MMM-yyyy') curdate";
        string sessiondate = "select  format(ToDate,'dd-MMM-yyyy') sessiondate from SessionMaster where SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
        if (DateTime.Parse(fromdt) >= DateTime.Parse(todt))
        {
            LblPaybleAmount.Text = "0";
            hdnPaybleAmount.Value = "0";
            lblMonths.Text = "0";
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Please select valid from date and to date!", "A");
        }
        //else if (DateTime.Parse(fromdt) < DateTime.Parse(oo.ReturnTag(curdate, "curdate")))
        //{
        //    LblPaybleAmount.Text = "0";
        //    hdnPaybleAmount.Value = "0";
        //    lblMonths.Text = "0";
        //    Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Can not select back date!", "A");
        //}
        else if (DateTime.Parse(todt) > DateTime.Parse(oo.ReturnTag(sessiondate, "sessiondate")))
        {
            LblPaybleAmount.Text = "0";
            hdnPaybleAmount.Value = "0";
            lblMonths.Text = "0";
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Please select between current session date!", "A");
        }
        else
        {
            int monthDiff = GetMonthDifference(DateTime.Parse(fromdt), DateTime.Parse(todt)) + 1;
            lblMonths.Text = monthDiff.ToString();
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                CheckBox chk = (CheckBox)GridView1.Rows[i].FindControl("chkFree");
                Label Price = (Label)GridView1.Rows[i].FindControl("txtPrice");
                Label txtPaymentType = (Label)GridView1.Rows[i].FindControl("txtPaymentType");
                if (chk.Checked)
                {
                    LblPaybleAmount.Text = (monthDiff * double.Parse(Price.Text)).ToString();
                    hdnPaybleAmount.Value = (monthDiff * double.Parse(Price.Text)).ToString();
                    LinkSubmit.Visible = true;
                }
            }

        }
        changeInstallAmt();
    }
    protected void DDMonthFrom_SelectedIndexChanged(object sender, EventArgs e)
    {
        LinkSubmit.Visible = false;
        string fromdt = DateTime.Parse(DDDateFrom.SelectedValue + "-" + DDMonthFrom.SelectedValue + "-" + DDYearFrom.SelectedValue).ToString("dd-MMM-yyyy");
        string todt = DateTime.Parse(DDDateTo.SelectedValue + "-" + DDMonthTo.SelectedValue + "-" + DDYearTo.SelectedValue).ToString("dd-MMM-yyyy");
        string curdate = "select format(getdate(),'dd-MMM-yyyy') curdate";
        string sessiondate = "select  format(ToDate,'dd-MMM-yyyy') sessiondate from SessionMaster where SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
        if (DateTime.Parse(fromdt) >= DateTime.Parse(todt))
        {
            LblPaybleAmount.Text = "0";
            hdnPaybleAmount.Value = "0";
            lblMonths.Text = "0";
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Please select valid from date and to date!", "A");
        }
        //else if (DateTime.Parse(fromdt) < DateTime.Parse(oo.ReturnTag(curdate, "curdate")))
        //{
        //    LblPaybleAmount.Text = "0";
        //    hdnPaybleAmount.Value = "0";
        //    lblMonths.Text = "0";
        //    Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Can not select back date!", "A");
        //}
        else if (DateTime.Parse(todt) > DateTime.Parse(oo.ReturnTag(sessiondate, "sessiondate")))
        {
            LblPaybleAmount.Text = "0";
            hdnPaybleAmount.Value = "0";
            lblMonths.Text = "0";
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Please select between current session date!", "A");
        }
        else
        {
            int monthDiff = GetMonthDifference(DateTime.Parse(fromdt), DateTime.Parse(todt)) + 1;
            lblMonths.Text = monthDiff.ToString();
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                CheckBox chk = (CheckBox)GridView1.Rows[i].FindControl("chkFree");
                Label Price = (Label)GridView1.Rows[i].FindControl("txtPrice");
                Label txtPaymentType = (Label)GridView1.Rows[i].FindControl("txtPaymentType");
                if (chk.Checked)
                {
                    LblPaybleAmount.Text = (monthDiff * double.Parse(Price.Text)).ToString();
                    hdnPaybleAmount.Value = (monthDiff * double.Parse(Price.Text)).ToString();
                    LinkSubmit.Visible = true;
                }
            }

        }
        changeInstallAmt();
    }
    protected void DDDateFrom_SelectedIndexChanged(object sender, EventArgs e)
    {
        LinkSubmit.Visible = false;
        string fromdt = DateTime.Parse(DDDateFrom.SelectedValue + "-" + DDMonthFrom.SelectedValue + "-" + DDYearFrom.SelectedValue).ToString("dd-MMM-yyyy");
        string todt = DateTime.Parse(DDDateTo.SelectedValue + "-" + DDMonthTo.SelectedValue + "-" + DDYearTo.SelectedValue).ToString("dd-MMM-yyyy");
        string curdate = "select format(getdate(),'dd-MMM-yyyy') curdate";
        string sessiondate = "select  format(ToDate,'dd-MMM-yyyy') sessiondate from SessionMaster where SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
        if (DateTime.Parse(fromdt) >= DateTime.Parse(todt))
        {
            LblPaybleAmount.Text = "0";
            hdnPaybleAmount.Value = "0";
            lblMonths.Text = "0";
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Please select valid from date and to date!", "A");
        }
        //else if (DateTime.Parse(fromdt) < DateTime.Parse(oo.ReturnTag(curdate, "curdate")))
        //{
        //    LblPaybleAmount.Text = "0";
        //    hdnPaybleAmount.Value = "0";
        //    lblMonths.Text = "0";
        //    Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Can not select back date!", "A");
        //}
        else if (DateTime.Parse(todt) > DateTime.Parse(oo.ReturnTag(sessiondate, "sessiondate")))
        {
            LblPaybleAmount.Text = "0";
            hdnPaybleAmount.Value = "0";
            lblMonths.Text = "0";
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Please select between current session date!", "A");
        }
        else
        {
            int monthDiff = GetMonthDifference(DateTime.Parse(fromdt), DateTime.Parse(todt)) + 1;
            lblMonths.Text = monthDiff.ToString();
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                CheckBox chk = (CheckBox)GridView1.Rows[i].FindControl("chkFree");
                Label Price = (Label)GridView1.Rows[i].FindControl("txtPrice");
                Label txtPaymentType = (Label)GridView1.Rows[i].FindControl("txtPaymentType");
                if (chk.Checked)
                {
                    LblPaybleAmount.Text = (monthDiff * double.Parse(Price.Text)).ToString();
                    hdnPaybleAmount.Value = (monthDiff * double.Parse(Price.Text)).ToString();
                    LinkSubmit.Visible = true;
                }
            }

        }
        changeInstallAmt();
    }
    protected void DDYearTo_SelectedIndexChanged(object sender, EventArgs e)
    {
        LinkSubmit.Visible = false;
        oo.YearDropDown(DDYearTo, DDMonthTo, DDDateTo);
        string fromdt = DateTime.Parse(DDDateFrom.SelectedValue + "-" + DDMonthFrom.SelectedValue + "-" + DDYearFrom.SelectedValue).ToString("dd-MMM-yyyy");
        string todt = DateTime.Parse(DDDateTo.SelectedValue + "-" + DDMonthTo.SelectedValue + "-" + DDYearTo.SelectedValue).ToString("dd-MMM-yyyy");
        string curdate = "select format(getdate(),'dd-MMM-yyyy') curdate";
        string sessiondate = "select  format(ToDate,'dd-MMM-yyyy') sessiondate from SessionMaster where SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
        if (DateTime.Parse(fromdt) >= DateTime.Parse(todt))
        {
            LblPaybleAmount.Text = "0";
            hdnPaybleAmount.Value = "0";
            lblMonths.Text = "0";
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Please select valid from date and to date!", "A");
        }
        //else if (DateTime.Parse(fromdt) < DateTime.Parse(oo.ReturnTag(curdate, "curdate")))
        //{
        //    LblPaybleAmount.Text = "0";
        //    hdnPaybleAmount.Value = "0";
        //    lblMonths.Text = "0";
        //    Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Can not select back date!", "A");
        //}
        else if (DateTime.Parse(todt) > DateTime.Parse(oo.ReturnTag(sessiondate, "sessiondate")))
        {
            LblPaybleAmount.Text = "0";
            hdnPaybleAmount.Value = "0";
            lblMonths.Text = "0";
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Please select between current session date!", "A");
        }
        else
        {
            int monthDiff = GetMonthDifference(DateTime.Parse(fromdt), DateTime.Parse(todt)) + 1;
            lblMonths.Text = monthDiff.ToString();
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                CheckBox chk = (CheckBox)GridView1.Rows[i].FindControl("chkFree");
                Label Price = (Label)GridView1.Rows[i].FindControl("txtPrice");
                Label txtPaymentType = (Label)GridView1.Rows[i].FindControl("txtPaymentType");
                if (chk.Checked)
                {
                    LblPaybleAmount.Text = (monthDiff * double.Parse(Price.Text)).ToString();
                    hdnPaybleAmount.Value = (monthDiff * double.Parse(Price.Text)).ToString();
                    LinkSubmit.Visible = true;
                }
            }


        }
        changeInstallAmt();
    }
    protected void DDMonthTo_SelectedIndexChanged(object sender, EventArgs e)
    {
        LinkSubmit.Visible = false;
        oo.MonthDropDown(DDYearTo, DDMonthTo, DDDateTo);
        string fromdt = DateTime.Parse(DDDateFrom.SelectedValue + "-" + DDMonthFrom.SelectedValue + "-" + DDYearFrom.SelectedValue).ToString("dd-MMM-yyyy");
        string todt = DateTime.Parse(DDDateTo.SelectedValue + "-" + DDMonthTo.SelectedValue + "-" + DDYearTo.SelectedValue).ToString("dd-MMM-yyyy");
        string curdate = "select format(getdate(),'dd-MMM-yyyy') curdate";
        string sessiondate = "select  format(ToDate,'dd-MMM-yyyy') sessiondate from SessionMaster where SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
        if (DateTime.Parse(fromdt) >= DateTime.Parse(todt))
        {
            LblPaybleAmount.Text = "0";
            hdnPaybleAmount.Value = "0";
            lblMonths.Text = "0";
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Please select valid from date and to date!", "A");
        }
        //else if (DateTime.Parse(fromdt) < DateTime.Parse(oo.ReturnTag(curdate, "curdate")))
        //{
        //    LblPaybleAmount.Text = "0";
        //    hdnPaybleAmount.Value = "0";
        //    lblMonths.Text = "0";
        //    Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Can not select back date!", "A");
        //}
        else if (DateTime.Parse(todt) > DateTime.Parse(oo.ReturnTag(sessiondate, "sessiondate")))
        {
            LblPaybleAmount.Text = "0";
            hdnPaybleAmount.Value = "0";
            lblMonths.Text = "0";
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Please select between current session date!", "A");
        }
        else
        {
            int monthDiff = GetMonthDifference(DateTime.Parse(fromdt), DateTime.Parse(todt)) + 1;
            lblMonths.Text = monthDiff.ToString();
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                CheckBox chk = (CheckBox)GridView1.Rows[i].FindControl("chkFree");
                Label Price = (Label)GridView1.Rows[i].FindControl("txtPrice");
                Label txtPaymentType = (Label)GridView1.Rows[i].FindControl("txtPaymentType");
                if (chk.Checked)
                {
                    LblPaybleAmount.Text = (monthDiff * double.Parse(Price.Text)).ToString();
                    hdnPaybleAmount.Value = (monthDiff * double.Parse(Price.Text)).ToString();
                    LinkSubmit.Visible = true;
                }
            }

        }
        changeInstallAmt();
    }
    protected void DDDateTo_SelectedIndexChanged(object sender, EventArgs e)
    {
        LinkSubmit.Visible = false;
        string fromdt = DateTime.Parse(DDDateFrom.SelectedValue + "-" + DDMonthFrom.SelectedValue + "-" + DDYearFrom.SelectedValue).ToString("dd-MMM-yyyy");
        string todt = DateTime.Parse(DDDateTo.SelectedValue + "-" + DDMonthTo.SelectedValue + "-" + DDYearTo.SelectedValue).ToString("dd-MMM-yyyy");
        string curdate = "select format(getdate(),'dd-MMM-yyyy') curdate";
        string sessiondate = "select  format(ToDate,'dd-MMM-yyyy') sessiondate from SessionMaster where SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
        if (DateTime.Parse(fromdt) >= DateTime.Parse(todt))
        {
            LblPaybleAmount.Text = "0";
            hdnPaybleAmount.Value = "0";
            lblMonths.Text = "0";
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Please select valid from date and to date!", "A");
        }
        //else if (DateTime.Parse(fromdt) < DateTime.Parse(oo.ReturnTag(curdate, "curdate")))
        //{
        //    LblPaybleAmount.Text = "0";
        //    hdnPaybleAmount.Value = "0";
        //    lblMonths.Text = "0";
        //    Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Can not select back date!", "A");
        //}
        else if (DateTime.Parse(todt) > DateTime.Parse(oo.ReturnTag(sessiondate, "sessiondate")))
        {
            LblPaybleAmount.Text = "0";
            hdnPaybleAmount.Value = "0";
            lblMonths.Text = "0";
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Please select between current session date!", "A");
        }
        else
        {
            int monthDiff = GetMonthDifference(DateTime.Parse(fromdt), DateTime.Parse(todt)) + 1;
            lblMonths.Text = monthDiff.ToString();
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                CheckBox chk = (CheckBox)GridView1.Rows[i].FindControl("chkFree");
                Label Price = (Label)GridView1.Rows[i].FindControl("txtPrice");
                Label txtPaymentType = (Label)GridView1.Rows[i].FindControl("txtPaymentType");
                if (chk.Checked)
                {
                    LblPaybleAmount.Text = (monthDiff * double.Parse(Price.Text)).ToString();
                    hdnPaybleAmount.Value = (monthDiff * double.Parse(Price.Text)).ToString();
                    LinkSubmit.Visible = true;
                }
            }

        }
        changeInstallAmt();
    }
    protected void changeInstallAmt()
    {

        double amt = (rptFeeStructure.Items.Count > 0 ? double.Parse(LblPaybleAmount.Text) / rptFeeStructure.Items.Count : 0);
        for (int i = 0; i < rptFeeStructure.Items.Count; i++)
        {
            TextBox txtAmount = (TextBox)rptFeeStructure.Items[i].FindControl("txtAmount");
            txtAmount.Text = amt.ToString("0.00");
        }
        txtAmountTotal.Text = double.Parse(LblPaybleAmount.Text).ToString("0.00");
    }
    protected void txtSearchStudent_TextChanged(object sender, EventArgs e)
    {
        div1.Visible = false;
        div2.Visible = false;
        div3.Visible = false;
        div4.Visible = false;
        div5.Visible = false;
        DrpRoomType.SelectedIndex = 0;
        //DrpRoomNo.SelectedIndex = 0;

        divAmountTotal.Visible = false;
        txtAmountTotal.Visible = false;
        rptFeeStructure.DataSource = null;
        rptFeeStructure.DataBind();
        GridView2.DataSource = null;
        GridView2.DataBind();
        var studentId = Request.Form[hfStudentId.UniqueID];
        if (string.IsNullOrEmpty(studentId))
        {
            studentId = txtSearchStudent.Text.Trim();
        }
        var ds = BLL.BLLInstance.GetStudentDetails(studentId, Session["SessionName"].ToString(), Session["BranchCode"].ToString());
        if (ds == null || ds.Tables[0].Rows.Count == 0)
        {
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox0, "No record found!", "A");
        }
        else
        {
            string sql2 = "";
            sql2 = "select (hcm.CategoryName+' Building '+hlm.BuildingLocation+' Room No. '+rm.RoomNo+'('+hrm.RoomType+') and Bed No. '+hbm.BedNo) allotedRoom, hbm.BookedStatus, ra.TotalMonths, hbm.BedNo, ra.PaymentType, ra.TotalAmount, ra.DateFrom, ra. DateTo, ra.TotalMonths from RoomAllotment ra inner join HostelCategoryMaster hcm on hcm.Id = ra.HostelCategoryId inner join HostelLocationMaster hlm on hlm.Id = ra.BuildingLocationId";
            sql2 = sql2 + " inner join RoomMaster rm on rm.Id = ra.RoomId inner join HostelRoomTypeMaster hrm on hrm.Id = ra.RoomTypeId inner join HostelBedMaster hbm on hbm.Id = ra.BedId where ra.SrNoOrEmpId = '" + studentId + "' and hbm.BranchCode=" + Session["BranchCode"].ToString() + " and hlm.BranchCode=" + Session["BranchCode"].ToString() + " and hcm.BranchCode=" + Session["BranchCode"].ToString() + " and rm.BranchCode=" + Session["BranchCode"].ToString() + " and ra.BranchCode=" + Session["BranchCode"].ToString() + " and ra.LivingStatus = 1 and ra.SessionName='" + Session["SessionName"].ToString() + "'";
            var dss = oo.GridFill(sql2);
            string PaymentType = oo.ReturnTag(sql2, "PaymentType");
            if (dss != null && dss.Tables[0].Rows.Count > 0)
            {
                GridView2.DataSource = dss;
                GridView2.DataBind();
                LinkSubmit.Visible = true;
                sql = "select SrNo from CompositFeeDeposit where SessionName='" + Session["SessionName"].ToString() + "' and SrNo = '" + studentId + "' and BranchCode=" + Session["BranchCode"].ToString() + " and FeeHeadId in (select id from FeeHeadMaster where BranchCode=" + Session["BranchCode"].ToString() + " and FeeType='Hostel Fee')";
                var dss2 = oo.GridFill(sql);
                if (dss2.Tables[0].Rows.Count > 0)
                {
                    LinkSubmit.Visible = false;
                    rptFeeStructure.DataSource = null;
                    rptFeeStructure.DataBind();
                    divAmountTotal.Visible = false;
                    txtAmountTotal.Visible = false;
                    for (int i = 0; i < rptFeeStructure.Items.Count; i++)
                    {
                        TextBox textbox = (TextBox)rptFeeStructure.Items[i].FindControl("txtAmount");
                        textbox.Enabled = false;
                    }
                    Campus camp = new Campus(); camp.msgbox(this.Page, msgbox0, "This student already occupied a room and submitted the fee.", "A");
                }
                else
                {
                    if (PaymentType.ToString().ToLower() != "monthly")
                    {
                        loadInstallMent(studentId);
                        LinkSubmit.Visible = true;
                        divAmountTotal.Visible = true;
                        txtAmountTotal.Visible = true;
                        for (int i = 0; i < rptFeeStructure.Items.Count; i++)
                        {
                            TextBox textbox = (TextBox)rptFeeStructure.Items[i].FindControl("txtAmount");
                            textbox.Enabled = true;
                        }
                    }
                    Campus camp = new Campus(); camp.msgbox(this.Page, msgbox0, "This student already occupied room.", "A");
                    LinkSubmit.Visible = false;
                }

                div1.Visible = true;
                div2.Visible = false;
                div3.Visible = false;
                div4.Visible = false;
                div5.Visible = false;

            }
            else
            {
                div1.Visible = true;
                div2.Visible = true;
                div3.Visible = true;
                div4.Visible = true;
                LinkSubmit.Visible = false;
            }


            grdStRecord.DataSource = ds;
            grdStRecord.DataBind();
            //DrpRoomNo.SelectedIndex = 0;
        }
        DrpRoomNo.Items.Clear();
        if (DrpRoomType.SelectedIndex != 0)
        {
            sql = "Select distinct rm.Id, RoomNo from RoomMaster rm inner join HostelBedMaster bm on bm.roomId=rm.Id where CategoryId = " + DrpCategory.SelectedValue + " and rm.BranchCode=" + Session["BranchCode"].ToString() + " and bm.BranchCode=" + Session["BranchCode"].ToString() + " and RoomTypeId=" + DrpRoomType.SelectedValue + " and BuildingLocationId=" + DrpbuildingLocation.SelectedValue + "  and bm.Status=1 and bm.BookedStatus=0";
            oo.FillDropDown_withValue(sql, DrpRoomNo, "RoomNo", "Id");
            DrpRoomNo.Items.Insert(0, "Select");
        }
        else
        {
            DrpRoomNo.Items.Insert(0, "Select");
        }
    }
    protected void txtSearchStaff_TextChanged(object sender, EventArgs e)
    {
        div1.Visible = false;
        div2.Visible = false;
        div3.Visible = false;
        div4.Visible = false;
        div5.Visible = false;
        DrpRoomType.SelectedIndex = 0;
        DrpRoomNo.SelectedIndex = 0;

        GridView2.DataSource = null;
        GridView2.DataBind();
        var StaffId = Request.Form[hfStaffId.UniqueID];
        if (string.IsNullOrEmpty(StaffId))
        {
            StaffId = txtSearchStaff.Text.Trim();
        }
        sql = "Select eod.EmpId EmpId,eod.Ecode Ecode,egd.EFirstName+egd.EMiddleName+egd.ELastName as EmpName,egd.EFatherName FatherName,eod.Designation Designation from EmpployeeOfficialDetails eod ";
        sql = sql + " inner join EmpGeneralDetail egd on eod.Ecode=egd.Ecode and eod.EmpId=egd.EmpId where eod.Withdrwal is null ";
        sql = sql + " and (eod.Ecode='" + StaffId.Trim() + "' or eod.EmpId='" + StaffId.Trim() + "') and egd.BranchCode=" + Session["BranchCode"].ToString() + " and eod.BranchCode=" + Session["BranchCode"].ToString() + "";
        var ds = BAL.objBal.GridFill(sql);
        if (ds == null || ds.Tables[0].Rows.Count == 0)
        {
            div1.Visible = false;
            div2.Visible = false;
            div3.Visible = false;
            div4.Visible = false;
            div5.Visible = false;
            Grd.DataSource = null;
            Grd.DataBind();
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox0, "No record found!", "A");
        }
        else
        {
            string sql2 = "";

            sql2 = "select (hcm.CategoryName+' Building '+hlm.BuildingLocation+' Room No. '+rm.RoomNo+'('+hrm.RoomType+') and Bed No. '+hbm.BedNo) allotedRoom,  hbm.BookedStatus, hbm.BedNo, ra.PaymentType, ra.TotalAmount, ra.DateFrom, ra. DateTo, ra.TotalMonths from RoomAllotment ra inner join HostelCategoryMaster hcm on hcm.Id = ra.HostelCategoryId inner join HostelLocationMaster hlm on hlm.Id = ra.BuildingLocationId";
            sql2 = sql2 + " inner join RoomMaster rm on rm.Id = ra.RoomId inner join HostelRoomTypeMaster hrm on hrm.Id = ra.RoomTypeId inner join HostelBedMaster hbm on hbm.Id = ra.BedId where ra.SrNoOrEmpId = '" + StaffId + "' and hlm.BranchCode=" + Session["BranchCode"].ToString() + " and hrm.BranchCode=" + Session["BranchCode"].ToString() + " and hcm.BranchCode=" + Session["BranchCode"].ToString() + " and rm.BranchCode=" + Session["BranchCode"].ToString() + " and ra.BranchCode=" + Session["BranchCode"].ToString() + " and ra.LivingStatus = 1 and ra.SessionName='" + Session["SessionName"].ToString() + "'";
            var dss = oo.GridFill(sql2);
            if (dss != null && dss.Tables[0].Rows.Count > 0)
            {
                GridView2.DataSource = dss;
                GridView2.DataBind();
                Campus camp = new Campus(); camp.msgbox(this.Page, msgbox0, "This staff already occupied room!", "A");
                div1.Visible = true;
                div2.Visible = false;
                div3.Visible = false;
                div4.Visible = false;
                div5.Visible = false;
                LinkSubmit.Visible = false;
            }
            else
            {
                div1.Visible = true;
                div2.Visible = true;
                div3.Visible = true;
                div4.Visible = true;
                LinkSubmit.Visible = true;
            }
            Grd.DataSource = ds;
            Grd.DataBind();
        }
    }
    protected void lnkView_Click(object sender, EventArgs e)
    {
        div1.Visible = false;
        div2.Visible = false;
        div3.Visible = false;
        div4.Visible = false;
        div5.Visible = false;
        DrpRoomType.SelectedIndex = 0;
        DrpRoomNo.SelectedIndex = 0;

        divAmountTotal.Visible = false;
        txtAmountTotal.Visible = false;

        rptFeeStructure.DataSource = null;
        rptFeeStructure.DataBind();
        GridView2.DataSource = null;
        GridView2.DataBind();
        grdStRecord.DataSource = null;
        grdStRecord.DataBind();
        Grd.DataSource = null;
        Grd.DataBind();
        if (rdoType.SelectedIndex == 0)
        {
            var studentId = Request.Form[hfStudentId.UniqueID];
            if (string.IsNullOrEmpty(studentId))
            {
                studentId = txtSearchStudent.Text.Trim();
            }
            var ds = BLL.BLLInstance.GetStudentDetails(studentId, Session["SessionName"].ToString(), Session["BranchCode"].ToString());
            if (ds == null || ds.Tables[0].Rows.Count == 0)
            {
                div1.Visible = false;
                div2.Visible = false;
                div3.Visible = false;
                div4.Visible = false;
                div5.Visible = false;
                grdStRecord.DataSource = null;
                grdStRecord.DataBind();
                Campus camp = new Campus(); camp.msgbox(this.Page, msgbox0, "No record found!", "A");
            }
            else
            {
                string sql2 = "";
                sql2 = "select (hcm.CategoryName+' Building '+hlm.BuildingLocation+' Room No. '+rm.RoomNo+'('+hrm.RoomType+') and Bed No. '+hbm.BedNo) allotedRoom,  hbm.BookedStatus, ra.TotalMonths, hbm.BedNo, ra.PaymentType, ra.TotalAmount, ra.DateFrom, ra. DateTo, ra.TotalMonths from RoomAllotment ra inner join HostelCategoryMaster hcm on hcm.Id = ra.HostelCategoryId inner join HostelLocationMaster hlm on hlm.Id = ra.BuildingLocationId";
                sql2 = sql2 + " inner join RoomMaster rm on rm.Id = ra.RoomId inner join HostelRoomTypeMaster hrm on hrm.Id = ra.RoomTypeId inner join HostelBedMaster hbm on hbm.Id = ra.BedId where ra.SrNoOrEmpId = '" + studentId + "' and hlm.BranchCode=" + Session["BranchCode"].ToString() + " and hrm.BranchCode=" + Session["BranchCode"].ToString() + " and hcm.BranchCode=" + Session["BranchCode"].ToString() + " and rm.BranchCode=" + Session["BranchCode"].ToString() + " and ra.BranchCode=" + Session["BranchCode"].ToString() + " and ra.LivingStatus = 1 and ra.SessionName='" + Session["SessionName"].ToString() + "'";
                var dss = oo.GridFill(sql2);
                string PaymentType = oo.ReturnTag(sql2, "PaymentType");
                if (dss != null && dss.Tables[0].Rows.Count > 0)
                {
                    GridView2.DataSource = dss;
                    GridView2.DataBind();
                    LinkSubmit.Visible = true;
                    sql = "select SrNo from CompositFeeDeposit where SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + " and SrNo = '" + studentId + "' and feeheadid in (select TOP(1) id from FeeHeadMaster where BranchCode=" + Session["BranchCode"].ToString() + " and FeeType='Hostel Fee')";
                    var dss2 = oo.Fetchdata(sql);
                    if (dss2.Rows.Count > 0)
                    {
                        rptFeeStructure.DataSource = null;
                        rptFeeStructure.DataBind();
                        LinkSubmit.Visible = false;
                        divAmountTotal.Visible = false;
                        txtAmountTotal.Visible = false;
                        CheckBox ff = (CheckBox)rptFeeStructure.Controls[0].Controls[0].FindControl("chkFill");
                        ff.Checked = true;
                        ff.Enabled = false;
                        for (int i = 0; i < rptFeeStructure.Items.Count; i++)
                        {
                            TextBox textbox = (TextBox)rptFeeStructure.Items[i].FindControl("txtAmount");
                            textbox.Enabled = false;
                        }
                        Campus camp = new Campus(); camp.msgbox(this.Page, msgbox0, "This student already occupied room and submitted fee for some installment.", "A");
                        LinkSubmit.Visible = false;
                    }
                    else
                    {
                        if (PaymentType.ToString().ToLower() != "monthly")
                        {
                            loadInstallMent(studentId);
                            LinkSubmit.Visible = true;
                            divAmountTotal.Visible = true;
                            txtAmountTotal.Visible = true;
                            for (int i = 0; i < rptFeeStructure.Items.Count; i++)
                            {
                                TextBox textbox = (TextBox)rptFeeStructure.Items[i].FindControl("txtAmount");
                                textbox.Enabled = true;
                            }
                        }
                        Campus camp = new Campus(); camp.msgbox(this.Page, msgbox0, "This student has already occupied a room!", "A");
                        LinkSubmit.Visible = false;
                    }
                    div1.Visible = true;
                    div2.Visible = false;
                    div3.Visible = false;
                    div4.Visible = false;
                    div5.Visible = false;

                }
                else
                {
                    div1.Visible = true;
                    div2.Visible = true;
                    div3.Visible = true;
                    div4.Visible = true;
                    LinkSubmit.Visible = false;

                }
                grdStRecord.DataSource = ds;
                grdStRecord.DataBind();
                //DrpRoomNo.SelectedIndex = 0;
            }
        }
        else
        {
            var StaffId = Request.Form[hfStaffId.UniqueID];
            if (string.IsNullOrEmpty(StaffId))
            {
                StaffId = txtSearchStaff.Text.Trim();
            }
            sql = "Select eod.EmpId EmpId,eod.Ecode Ecode,egd.EFirstName+egd.EMiddleName+egd.ELastName as EmpName,egd.EFatherName FatherName,eod.Designation Designation from EmpployeeOfficialDetails eod ";
            sql = sql + " inner join EmpGeneralDetail egd on eod.Ecode=egd.Ecode and eod.EmpId=egd.EmpId where eod.Withdrwal is null ";
            sql = sql + " and (eod.Ecode='" + StaffId.Trim() + "' or eod.EmpId='" + StaffId.Trim() + "') and egd.BranchCode=" + Session["BranchCode"].ToString() + " and eod.BranchCode=" + Session["BranchCode"].ToString() + "";
            var ds = BAL.objBal.GridFill(sql);
            if (ds == null || ds.Tables[0].Rows.Count == 0)
            {
                div1.Visible = false;
                div2.Visible = false;
                div3.Visible = false;
                div4.Visible = false;
                Grd.DataSource = null;
                Grd.DataBind();
                Campus camp = new Campus(); camp.msgbox(this.Page, msgbox0, "No record found!", "A");
            }
            else
            {
                string sql2 = "";
                sql2 = "select (hcm.CategoryName+' Building '+hlm.BuildingLocation+' Room No. '+rm.RoomNo+'('+hrm.RoomType+') and Bed No. '+hbm.BedNo) allotedRoom from RoomAllotment ra inner join HostelCategoryMaster hcm on hcm.Id = ra.HostelCategoryId inner join HostelLocationMaster hlm on hlm.Id = ra.BuildingLocationId";
                sql2 = sql2 + " inner join RoomMaster rm on rm.Id = ra.RoomId inner join HostelRoomTypeMaster hrm on hrm.Id = ra.RoomTypeId inner join HostelBedMaster hbm on hbm.Id = ra.BedId where ra.SrNoOrEmpId = '" + StaffId + "' and hlm.BranchCode=" + Session["BranchCode"].ToString() + " and hcm.BranchCode=" + Session["BranchCode"].ToString() + " and hbm.BranchCode=" + Session["BranchCode"].ToString() + " and rm.BranchCode=" + Session["BranchCode"].ToString() + " and ra.BranchCode=" + Session["BranchCode"].ToString() + " and ra.LivingStatus = 1";
                var dss = oo.GridFill(sql2);
                if (dss != null && dss.Tables[0].Rows.Count > 0)
                {
                    Campus camp = new Campus(); camp.msgbox(this.Page, msgbox0, "This staff already stay in " + dss.Tables[0].Rows[0]["allotedRoom"].ToString() + " !", "A");
                    div1.Visible = true;
                    div2.Visible = false;
                    div3.Visible = false;
                    div4.Visible = false;
                }
                else
                {
                    div1.Visible = true;
                    div2.Visible = true;
                    div3.Visible = true;
                    div4.Visible = true;
                }
                Grd.DataSource = ds;
                Grd.DataBind();
            }
        }
        DrpRoomNo.Items.Clear();
        if (DrpRoomType.SelectedIndex != 0)
        {
            sql = "Select distinct rm.Id, RoomNo from RoomMaster rm inner join HostelBedMaster bm on bm.roomId=rm.Id where CategoryId=" + DrpCategory.SelectedValue + " and RoomTypeId=" + DrpRoomType.SelectedValue + " and BranchCode=" + Session["BranchCode"].ToString() + " and BuildingLocationId=" + DrpbuildingLocation.SelectedValue + "  and bm.BookedStatus=0 and bm.Status=1";
            oo.FillDropDown_withValue(sql, DrpRoomNo, "RoomNo", "Id");
            DrpRoomNo.Items.Insert(0, "Select");
        }
        else
        {
            DrpRoomNo.Items.Insert(0, "Select");
        }
    }
    protected void rdoType_SelectedIndexChanged(object sender, EventArgs e)
    {
        hfStaffId.Value = "";
        hfStudentId.Value = "";
        txtSearchStaff.Text = "";
        txtSearchStudent.Text = "";
        grdStRecord.DataSource = null;
        grdStRecord.DataBind();
        Grd.DataSource = null;
        Grd.DataBind();
        if (rdoType.SelectedIndex == 0)
        {
            divStaff.Visible = false;
            divStudent.Visible = true;
            div2.Visible = false;
            div1.Visible = true;
            div3.Visible = false;
        }
        else
        {
            divStaff.Visible = true;
            divStudent.Visible = false;
            div2.Visible = true;
            div1.Visible = false;
            div3.Visible = false;
        }
    }
    protected void DrpbuildingLocation_SelectedIndexChanged(object sender, EventArgs e)
    {
        DrpRoomType.SelectedIndex = 0;
    }
    protected void DrpRoomType_SelectedIndexChanged(object sender, EventArgs e)
    {
        rptFeeStructure.DataSource = null;
        rptFeeStructure.DataBind();
        div5.Visible = false;
        DrpRoomNo.Items.Clear();
        if (DrpRoomType.SelectedIndex != 0)
        {
            sql = "Select distinct rm.Id, RoomNo from RoomMaster rm inner join HostelBedMaster bm on bm.roomId=rm.Id where CategoryId=" + DrpCategory.SelectedValue + " and rm.BranchCode=" + Session["BranchCode"].ToString() + " and bm.BranchCode=" + Session["BranchCode"].ToString() + " and RoomTypeId=" + DrpRoomType.SelectedValue + " and BuildingLocationId=" + DrpbuildingLocation.SelectedValue + "  and bm.BookedStatus=0 and bm.Status=1";
            oo.FillDropDown_withValue(sql, DrpRoomNo, "RoomNo", "Id");
            DrpRoomNo.Items.Insert(0, "Select");
        }
        else
        {
            DrpRoomNo.Items.Insert(0, "Select");
        }
    }
    protected void DrpRoomNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        BedBind();

        if (rptFeeStructure.Items.Count > 0)
        {
            for (int i = 0; i < rptFeeStructure.Items.Count; i++)
            {
                TextBox txtAmount = (TextBox)rptFeeStructure.Items[i].FindControl("txtAmount");
                txtAmount.Text = "0.00";

            }
        }
        txtAmountTotal.Text = "0.00";
    }
    protected void BedBind()
    {
        divAmountTotal.Visible = false;
        txtAmountTotal.Visible = false;
        lblMonths.Text = "0";
        LblPaybleAmount.Text = "0";
        hdnPaybleAmount.Value = "0";
        GridView1.DataSource = null;
        GridView1.DataBind();
        oo.FindCurrentDateandSetinDropDown(DDYearFrom, DDMonthFrom, DDDateFrom);
        oo.FindCurrentDateandSetinDropDown(DDYearTo, DDMonthTo, DDDateTo);
        if (DrpRoomNo.SelectedIndex != 0)
        {
            int bookedCount = 0; int bedChech = 0;
            DataSet ds = new DataSet();
            sql = "select hbm.id, BedNo, BedCharge, PaymentType, hbm.remark, hbm.status, hbm.BookedStatus from HostelBedMaster hbm inner join RoomMaster rm on rm.Id = hbm.roomId where hbm.roomId=" + DrpRoomNo.SelectedValue + " and rm.BranchCode=" + Session["BranchCode"].ToString() + " and hbm.BranchCode=" + Session["BranchCode"].ToString() + "";
            ds = oo.GridFill(sql);

            if (ds == null || ds.Tables[0].Rows.Count == 0)
            {
                GridView1.DataSource = null;
                GridView1.DataBind();
                rptFeeStructure.DataSource = null;
                rptFeeStructure.DataBind();
                div5.Visible = false;
                LinkSubmit.Visible = false;
                Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Beds not created in this room!", "A");
            }
            else
            {
                LinkSubmit.Visible = true;
                div5.Visible = true;
                GridView1.DataSource = oo.GridFill(sql);
                GridView1.DataBind();
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    Label txtBedStatusDamaged = (Label)GridView1.Rows[i].FindControl("txtBedStatusDamaged");
                    Label txtBedStatusOk = (Label)GridView1.Rows[i].FindControl("txtBedStatusOk");
                    Label txtBookingStatus = (Label)GridView1.Rows[i].FindControl("txtBookingStatus");
                    Label txtBookingUnavailable = (Label)GridView1.Rows[i].FindControl("txtBookingUnavailable");
                    CheckBox chkFree = (CheckBox)GridView1.Rows[i].FindControl("chkFree");
                    if (ds.Tables[0].Rows[i]["status"].ToString().ToLower() == "true")
                    {
                        txtBedStatusDamaged.Visible = false;
                        txtBedStatusOk.Visible = true;
                    }
                    else
                    {
                        txtBedStatusDamaged.Visible = true;
                        txtBedStatusOk.Visible = false;
                    }
                    if (ds.Tables[0].Rows[i]["BookedStatus"].ToString().ToLower() == "true")
                    {
                        bookedCount = bookedCount + 1;
                        chkFree.Visible = false;
                        txtBookingStatus.Visible = true;

                    }
                    else
                    {
                        if (ds.Tables[0].Rows[i]["status"].ToString().ToLower() == "true")
                        {
                            chkFree.Visible = true;
                            txtBookingStatus.Visible = false;
                            txtBookingUnavailable.Visible = false;
                            if (bedChech == 0)
                            {
                                chkFree.Checked = true;
                                bedChech = bedChech + 1;
                            }
                        }
                        else
                        {
                            bookedCount = bookedCount + 1;
                            chkFree.Visible = false;
                            txtBookingStatus.Visible = false;
                            txtBookingUnavailable.Visible = true;
                        }

                    }
                }
                if (bookedCount == ds.Tables[0].Rows.Count)
                {

                    divdateFrom.Visible = false;
                    divdateTo.Visible = false;
                    divFrequencyofPayment.Visible = false;
                }
                else
                {

                    divdateFrom.Visible = true;
                    divdateTo.Visible = true;
                    divFrequencyofPayment.Visible = true;


                }
                for (int i = 0; i < GridView1.Rows.Count; i++)
                {
                    CheckBox chk = (CheckBox)GridView1.Rows[i].FindControl("chkFree");
                    Label PaymentType = (Label)GridView1.Rows[i].FindControl("txtPaymentType");
                    Label Price = (Label)GridView1.Rows[i].FindControl("txtPrice");
                    Label lblSrno = (Label)grdStRecord.Rows[0].FindControl("lblSrno");
                    if (chk.Checked && PaymentType.Text.ToLower().Trim() == "installment")
                    {
                        LblPaybleAmount.Text = "0.00";
                        hdnPaybleAmount.Value = "0.00";
                        lblPaymentType.Text = PaymentType.Text;
                        string studentIds = "";
                        if (rdoType.SelectedValue == "Student")
                        {
                            studentIds = hfStudentId.Value;
                            loadInstallMent(lblSrno.Text);
                        }
                        divAmountTotal.Visible = true;
                        txtAmountTotal.Visible = true;
                    }
                }
            }
        }
        else
        {
            GridView1.DataSource = null;
            GridView1.DataBind();
            div5.Visible = false;
        }
    }
    protected void chkFree_CheckedChanged(object sender, EventArgs e)
    {
        for (int i = 0; i < GridView1.Rows.Count; i++)
        {
            CheckBox chkFree = (CheckBox)GridView1.Rows[i].FindControl("chkFree");
            chkFree.Checked = false;
        }
        CheckBox chk = (CheckBox)sender;
        CheckBox CheckBoxForCheck = (CheckBox)chk.NamingContainer.FindControl("chkFree");
        CheckBoxForCheck.Checked = true;
        Label txtPrice = (Label)chk.NamingContainer.FindControl("txtPrice");
        Label txtPaymentType = (Label)chk.NamingContainer.FindControl("txtPaymentType");
        lblPaymentType.Text = txtPaymentType.Text;
        if (txtPaymentType.Text.ToLower() == "installment")
        {
            string studentIds = "";
            if (rdoType.SelectedValue == "Student")
            {
                studentIds = hfStudentId.Value;
                loadInstallMent(studentIds);
            }

            LblPaybleAmount.Text = (double.Parse(txtPrice.Text) * int.Parse(lblMonths.Text == "" ? "0" : lblMonths.Text)).ToString();
            hdnPaybleAmount.Value = (double.Parse(txtPrice.Text) * int.Parse(lblMonths.Text == "" ? "0" : lblMonths.Text)).ToString();
            divAmountTotal.Visible = true;
            txtAmountTotal.Visible = true;
            changeInstallAmt();
        }
        else
        {
            txtAmountTotal.Visible = false;
            divAmountTotal.Visible = false;
            rptFeeStructure.DataSource = null;
            rptFeeStructure.DataBind();
            LblPaybleAmount.Text = "0";
            hdnPaybleAmount.Value = "0";
        }
    }

    protected void loadInstallMent(string studentId)
    {
        rptFeeStructure.DataSource = null;
        rptFeeStructure.DataBind();
        rptFeeStructure.DataSource = BLL.BLLInstance.GetHostelInstallmentFee(studentId, Session["SessionName"].ToString(), Session["BranchCode"].ToString());
        rptFeeStructure.DataBind();
        int sts = 0;
        for (int i = rptFeeStructure.Items.Count - 1; i >= 0; i--)
        {
            if (sts == 0)
            {
                Label lblid = (Label)rptFeeStructure.Items[i].FindControl("lblids");
                Label lblInstallmentId = (Label)rptFeeStructure.Items[i].FindControl("lblInstallmentId");
                TextBox txtAmount = (TextBox)rptFeeStructure.Items[i].FindControl("txtAmount");
                LinkButton LinkButton3 = (LinkButton)rptFeeStructure.Items[i].FindControl("LinkButton3");
                string sh = "select id from CompositFeeDeposit where SrNo='" + studentId + "' and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " and InstallmentId=" + lblInstallmentId + " and receiptStatus='Paid'";
                if (oo.Duplicate(sh))
                {
                    txtAmount.Enabled = false;
                    LinkButton3.Visible = false;
                }
                else if (lblid.Text == "0")
                {
                    LinkButton3.Visible = false;
                }
                else
                {
                    sts = sts + 1;
                    LinkButton3.Visible = true;
                }
            }
        }




        if (GridView1.Rows.Count > 0 && GridView2.Rows.Count == 0)
        {
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                CheckBox chkFree = (CheckBox)GridView1.Rows[i].FindControl("chkFree");
                Label txtPrice = (Label)GridView1.Rows[i].FindControl("txtPrice");
                Label txtPaymentType = (Label)GridView1.Rows[i].FindControl("txtPaymentType");
                if (chkFree.Checked)
                {
                    LblPaybleAmount.Text = "0.00";
                    hdnPaybleAmount.Value = "0.00";
                }
            }
        }
        else
        {
            Label txtPrices = (Label)GridView2.Rows[0].FindControl("txtPrices");
            hdnPaybleAmount.Value = (double.Parse(txtPrices.Text) * rptFeeStructure.Items.Count).ToString();
            for (int i = 0; i < rptFeeStructure.Items.Count; i++)
            {
                TextBox txtAmount = (TextBox)rptFeeStructure.Items[i].FindControl("txtAmount");
                txtAmount.Enabled = false;

            }
        }
        double total = 0;
        for (int i = 0; i < rptFeeStructure.Items.Count; i++)
        {
            TextBox txtAmount = (TextBox)rptFeeStructure.Items[i].FindControl("txtAmount");
            total = total + double.Parse(txtAmount.Text == "" ? "0" : txtAmount.Text);

        }
        txtAmountTotal.Text = "Total : " + total.ToString();
        divAmountTotal.Visible = true;
        txtAmountTotal.Visible = true;



    }
    protected void LinkButton3_Click(object sender, EventArgs e)
    {
        Button8.Focus();
        LinkButton chk = (LinkButton)sender;
        Label lblId = (Label)chk.NamingContainer.FindControl("lblids");
        string ss = lblId.Text;
        lblvalue.Text = ss.ToString();
        Panel2_ModalPopupExtender.Show();
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        string studentIds = hfStudentId.Value;
        sql = "Delete from HostelFeeAllotmentForCondidate where SrNoOrEmpId='" + studentIds + "' and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " and id=" + lblvalue.Text;

        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = sql;
        cmd.CommandType = CommandType.Text;
        cmd.Connection = con;
        try
        {
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            //oo.MessageBox("Deleted successfully.", this.Page);
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Deleted successfully", "S");
            string sql2 = "";
            sql2 = "select (hcm.CategoryName+' Building '+hlm.BuildingLocation+' Room No. '+rm.RoomNo+'('+hrm.RoomType+') and Bed No. '+hbm.BedNo) allotedRoom, hbm.BookedStatus, hbm.BedNo, ra.PaymentType, ra.TotalAmount, hbm.BookedStatus, ra.DateFrom, ra. DateTo, ra.TotalMonths from RoomAllotment ra inner join HostelCategoryMaster hcm on hcm.Id = ra.HostelCategoryId inner join HostelLocationMaster hlm on hlm.Id = ra.BuildingLocationId";
            sql2 = sql2 + " inner join RoomMaster rm on rm.Id = ra.RoomId inner join HostelRoomTypeMaster hrm on hrm.Id = ra.RoomTypeId inner join HostelBedMaster hbm on hbm.Id = ra.BedId where ra.SrNoOrEmpId = '" + studentIds + "' and hbm.BranchCode=" + Session["BranchCode"].ToString() + " and hlm.BranchCode=" + Session["BranchCode"].ToString() + " and hcm.BranchCode=" + Session["BranchCode"].ToString() + " and hbm.BranchCode=" + Session["BranchCode"].ToString() + " and rm.BranchCode=" + Session["BranchCode"].ToString() + " and ra.BranchCode=" + Session["BranchCode"].ToString() + " and ra.LivingStatus = 1";
            var dss = oo.GridFill(sql2);
            if (dss != null && dss.Tables[0].Rows.Count > 0)
            {
                GridView2.DataSource = dss;
                GridView2.DataBind();
                loadInstallMent(studentIds);
            }
        }
        catch (SqlException ex) { }
    }
    protected void Button8_Click(object sender, EventArgs e)
    {

    }
    protected void LinkSubmit_Click(object sender, EventArgs e)
    {
        Label lblSrno = (Label)grdStRecord.Rows[0].FindControl("lblSrno");
        string PaymentType = ""; double totalAmount = 0;
        for (int i = 0; i < rptFeeStructure.Items.Count; i++)
        {
            TextBox txtAmount = (TextBox)rptFeeStructure.Items[i].FindControl("txtAmount");
            totalAmount = totalAmount + double.Parse(txtAmount.Text == "" ? "0" : txtAmount.Text);
        }
        if (GridView1.Rows.Count > 0 && GridView2.Rows.Count == 0)
        {
            if (double.Parse(totalAmount.ToString("0.0")) != double.Parse(LblPaybleAmount.Text))
            {
                Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Please enter total instalments amount equal to payble amount", "A");
                return;
            }
            else
            {
                hdnPaybleAmount.Value = LblPaybleAmount.Text.Trim();
            }
        }
        else
        {
            if (double.Parse(totalAmount.ToString("0.0")) != double.Parse(LblPaybleAmount.Text))
            {
                Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Please enter total instalments amount equal to payble amount", "A");
                return;
            }
            else
            {
                hdnPaybleAmount.Value = LblPaybleAmount.Text.Trim();
            }
        }

        if (GridView1.Rows.Count > 0 && GridView2.Rows.Count == 0)
        {

            string fromdt = DateTime.Parse(DDDateFrom.SelectedValue + "-" + DDMonthFrom.SelectedValue + "-" + DDYearFrom.SelectedValue).ToString("dd-MMM-yyyy");
            string todt = DateTime.Parse(DDDateTo.SelectedValue + "-" + DDMonthTo.SelectedValue + "-" + DDYearTo.SelectedValue).ToString("dd-MMM-yyyy");
            if (DateTime.Parse(fromdt.ToString()) >= DateTime.Parse(todt.ToString()))
            {
                Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Please select valid from date and to date!", "A");
                return;
            }
            if (lblMonths.Text.Trim() == "0" || hdnPaybleAmount.Value.Trim() == "0")
            {
                Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Invalid Amount!", "A");
                return;
            }
            string RoomCostPerMonth = "";
            string BedId = "";
            string remark = "";
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                CheckBox chk = (CheckBox)GridView1.Rows[i].FindControl("chkFree");
                Label Price = (Label)GridView1.Rows[i].FindControl("txtPrice");
                Label lblBedId = (Label)GridView1.Rows[i].FindControl("lblBedId");
                TextBox txtremark = (TextBox)GridView1.Rows[i].FindControl("txtremark");
                if (chk.Checked)
                {
                    RoomCostPerMonth = Price.Text;
                    BedId = lblBedId.Text;
                    remark = txtremark.Text;
                }
            }

            string studentIds = "";

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "RoomAllotmentProc";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            if (rdoType.SelectedValue == "Student")
            {
                studentIds = lblSrno.Text.Trim();
                cmd.Parameters.AddWithValue("@SrNoOrEmpId", lblSrno.Text.Trim());
            }
            else
            {
                studentIds = hfStaffId.Value;
                cmd.Parameters.AddWithValue("@SrNoOrEmpId", hfStaffId.Value);
            }
            cmd.Parameters.AddWithValue("@SrType", rdoType.SelectedValue);
            cmd.Parameters.AddWithValue("@HostelCategoryId", DrpCategory.SelectedValue);
            cmd.Parameters.AddWithValue("@BuildingLocationId", DrpbuildingLocation.SelectedValue);
            cmd.Parameters.AddWithValue("@RoomId", DrpRoomNo.SelectedValue);
            cmd.Parameters.AddWithValue("@RoomTypeId", DrpRoomType.SelectedValue);
            cmd.Parameters.AddWithValue("@RoomCostPerMonth", RoomCostPerMonth.Trim());
            cmd.Parameters.AddWithValue("@BedId", BedId.Trim());
            cmd.Parameters.AddWithValue("@DateFrom", fromdt.ToString());
            cmd.Parameters.AddWithValue("@DateTo", todt.ToString());
            cmd.Parameters.AddWithValue("@PaymentType", PaymentType);
            cmd.Parameters.AddWithValue("@FrequencyofPayment", drpFrequencyofPayment.SelectedValue);
            cmd.Parameters.AddWithValue("@TotalMonths", lblMonths.Text.Trim());
            cmd.Parameters.AddWithValue("@TotalAmount", hdnPaybleAmount.Value.Trim());
            cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"].ToString());
            cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
            cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());
            cmd.Parameters.AddWithValue("@Remark", remark);

            try
            {
                string PaybleAmount = LblPaybleAmount.Text;
                int x = 0;
                con.Open();
                x = cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                con.Close();
                lblMonths.Text = "0";
                LblPaybleAmount.Text = "0";

                if (x > 0)
                {
                    sql = "select ClassId, CardId, TypeofEducation, * from AllStudentRecord_UDF('" + Session["SessionName"].ToString() + "', " + Session["BranchCode"].ToString() + ") where SrNo = '" + lblSrno.Text.Trim() + "'";
                    string CardId = oo.ReturnTag(sql, "CardId");
                    string TypeofEducation = (oo.ReturnTag(sql, "TypeofEducation").ToString().ToUpper() == "R" ? "Regular" : "Private");
                    string ClassId = oo.ReturnTag(sql, "ClassId");
                    for (int i = 0; i < rptFeeStructure.Items.Count; i++)
                    {

                        TextBox txtAmounts = (TextBox)rptFeeStructure.Items[i].FindControl("txtAmount");
                        Label lblInstallmentId = (Label)rptFeeStructure.Items[i].FindControl("lblInstallmentId");
                        if (double.Parse(txtAmounts.Text == "" ? "0" : txtAmounts.Text) > 0)
                        {
                            cmd.CommandText = "HostelFeeAllotmentForCondidateProc";
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Connection = con;
                            if (rdoType.SelectedValue == "Student")
                            {
                                cmd.Parameters.AddWithValue("@SrNoOrEmpId", lblSrno.Text.Trim());
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@SrNoOrEmpId", hfStaffId.Value);
                            }
                            cmd.Parameters.AddWithValue("@ClassId", ClassId);
                            cmd.Parameters.AddWithValue("@InstallmentId", lblInstallmentId.Text.ToString());
                            cmd.Parameters.AddWithValue("@Amount", double.Parse(txtAmounts.Text).ToString("0.0"));
                            cmd.Parameters.AddWithValue("@CardId", CardId);
                            cmd.Parameters.AddWithValue("@TypeofEducation", TypeofEducation);
                            cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"].ToString());
                            cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
                            cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());
                            cmd.Parameters.AddWithValue("@Action", "Insert");
                            try
                            {
                                con.Open();
                                cmd.ExecuteNonQuery();
                                cmd.Parameters.Clear();
                                con.Close();
                            }
                            catch (SqlException ex)
                            {
                                Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, ex.Message, "A");
                            }
                        }
                    }
                    GridView1.DataSource = null;
                    GridView1.DataBind();
                    divAmountTotal.Visible = true;
                    txtAmountTotal.Visible = true;

                    GridView2.DataSource = null;
                    GridView2.DataBind();

                    string sql2 = "";
                    sql2 = "select (hcm.CategoryName+' Building '+hlm.BuildingLocation+' Room No. '+rm.RoomNo+'('+hrm.RoomType+') and Bed No. '+hbm.BedNo) allotedRoom, hbm.BookedStatus, hbm.BedNo, ra.PaymentType, ra.TotalAmount, hbm.BookedStatus, ra.DateFrom, ra. DateTo, ra.TotalMonths from RoomAllotment ra inner join HostelCategoryMaster hcm on hcm.Id = ra.HostelCategoryId inner join HostelLocationMaster hlm on hlm.Id = ra.BuildingLocationId";
                    sql2 = sql2 + " inner join RoomMaster rm on rm.Id = ra.RoomId inner join HostelRoomTypeMaster hrm on hrm.Id = ra.RoomTypeId inner join HostelBedMaster hbm on hbm.Id = ra.BedId where ra.SrNoOrEmpId = '" + studentIds + "' and hbm.BranchCode=" + Session["BranchCode"].ToString() + " and hlm.BranchCode=" + Session["BranchCode"].ToString() + " and hcm.BranchCode=" + Session["BranchCode"].ToString() + " and hbm.BranchCode=" + Session["BranchCode"].ToString() + " and rm.BranchCode=" + Session["BranchCode"].ToString() + " and ra.BranchCode=" + Session["BranchCode"].ToString() + " and ra.LivingStatus = 1";
                    var dss = oo.GridFill(sql2);
                    if (dss != null && dss.Tables[0].Rows.Count > 0)
                    {
                        GridView2.DataSource = dss;
                        GridView2.DataBind();
                        loadInstallMent(studentIds);
                    }

                }

                div2.Visible = false;
                div4.Visible = false;
                div5.Visible = false;

                Campus camp1 = new Campus(); camp1.msgbox(this.Page, msgbox, "Submitted successfully.", "S");
            }
            catch (SqlException ex)
            {
                Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, ex.Message, "A");
            }
        }
        else
        {


            sql = "select ClassId, CardId, TypeofEducation, * from AllStudentRecord_UDF('" + Session["SessionName"].ToString() + "', " + Session["BranchCode"].ToString() + ") where SrNo = '" + lblSrno.Text.Trim() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
            string CardId = oo.ReturnTag(sql, "CardId");
            string TypeofEducation = (oo.ReturnTag(sql, "TypeofEducation").ToString().ToUpper() == "R" ? "Regular" : "Private");
            string ClassId = oo.ReturnTag(sql, "ClassId");
            for (int i = 0; i < rptFeeStructure.Items.Count; i++)
            {
                TextBox txtAmount = (TextBox)rptFeeStructure.Items[i].FindControl("txtAmount");
                Label lblInstallmentId = (Label)rptFeeStructure.Items[i].FindControl("lblInstallmentId");

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "HostelFeeAllotmentForCondidateProc";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;
                if (rdoType.SelectedValue == "Student")
                {
                    cmd.Parameters.AddWithValue("@SrNoOrEmpId", lblSrno.Text.Trim());
                }
                else
                {
                    cmd.Parameters.AddWithValue("@SrNoOrEmpId", hfStaffId.Value);
                }
                cmd.Parameters.AddWithValue("@ClassId", ClassId);
                cmd.Parameters.AddWithValue("@InstallmentId", lblInstallmentId.Text.ToString());
                cmd.Parameters.AddWithValue("@Amount", double.Parse(txtAmount.Text).ToString("0.0"));
                cmd.Parameters.AddWithValue("@CardId", CardId);
                cmd.Parameters.AddWithValue("@TypeofEducation", TypeofEducation);
                cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"].ToString());
                cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
                cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());
                cmd.Parameters.AddWithValue("@Action", "Insert");
                try
                {
                    con.Open();
                    int x = cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                    Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Submitted successfully.", "S");
                    con.Close();
                }
                catch (SqlException ex)
                {
                    Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, ex.Message, "A");
                }
            }
            loadInstallMent(lblSrno.Text.Trim());
        }
    }

    protected void chkFill_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chk = (CheckBox)sender;
        CheckBox chkFill = (CheckBox)chk.NamingContainer.FindControl("chkFill");
        if (chkFill.Checked)
        {
            string fillAmt = "0.00";
            for (int i = 0; i < rptFeeStructure.Items.Count; i++)
            {
                TextBox txtAmount = (TextBox)rptFeeStructure.Items[i].FindControl("txtAmount");
                if (i == 0)
                {
                    fillAmt = txtAmount.Text;
                }
                else
                {
                    txtAmount.Text = fillAmt;
                }
            }
            double total = 0;
            for (int i = 0; i < rptFeeStructure.Items.Count; i++)
            {
                TextBox txtAmount = (TextBox)rptFeeStructure.Items[i].FindControl("txtAmount");
                total = total + double.Parse(txtAmount.Text == "" ? "0" : txtAmount.Text);
            }
            txtAmountTotal.Text = "Total : " + total.ToString();
        }
    }


}