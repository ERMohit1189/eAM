using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class admin_AttendanceReport : System.Web.UI.Page
{
    DataTable dt = new DataTable();
    Campus oo = new Campus();
    static string AttendanceStatus = "";

    protected void Page_PreInIt(object sender, EventArgs e)
    {
        if (Session["Logintype"] == null) { Response.Redirect("~/default.aspx"); }
        if (Session["Logintype"].ToString() == "Admin")
        {
            this.MasterPageFile = "~/Master/admin_root-manager.master";
        }
        else if (Session["Logintype"].ToString() == "Staff")
        {
            this.MasterPageFile = "~/Staff/staff_root-manager.master";
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        
        Campus camp = new Campus(); camp.LoadLoader(loader);
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["Sts"]))
                AttendanceStatus = Request.QueryString["Sts"].ToString();
            else
                AttendanceStatus = ddlAttendance.SelectedValue;

            GetClass();

            ddlBranch.Items.Insert(0, new ListItem("--All--", "-1"));
            ddlSection.Items.Insert(0, new ListItem("--All--", "-1"));

            txtFromDate.Text = DateTime.Now.ToString("dd-MMM-yyyy");
            GetAttendanceReport();
        }
        lblHeading.Text = "Date : " + Convert.ToDateTime(txtFromDate.Text.Trim()).ToString("dd-MMM-yyyy");
        //lblHeading.Text = "Consolidated Attendance Report ( Date : " + Convert.ToDateTime(txtFromDate.Text.Trim()).ToString("yyyy MMM dd") + " )";
    }

    public void GetClass()
    {
        divExport.Visible = true;
        string sql = "select * from classmaster where branchCode="+Session["BranchCode"]+ " and SessionName='" + Session["SessionName"] + "'";
        oo.FillDropDown_withValue(sql, ddlClass, "ClassName", "id");
        ddlClass.Items.Insert(0, new ListItem("<--Select-->", "-1"));
        if (Session["Logintype"].ToString() == "Staff")
        {
            sql = "Select ClassId from ClassTeacherMaster where Empcode='" + Session["LoginName"] + "' and branchCode=" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"] + "'";
            if (oo.Duplicate(sql))
            {
                ddlClass.SelectedValue = oo.ReturnTag(sql, "ClassId");
                GetSection();
                GetBranch();
            }
            else
            {
                ddlClass.Items.Insert(0, new ListItem("<--Select-->", "-100"));
                divExport.Visible = false;
                Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Please allot as a class teacher!", "A");
            }
            divClass.Visible = false;
        }
        else
        {
            ddlClass.SelectedIndex = 0;
            divClass.Visible = true;
        }
    }                   

    public void GetSection()
    {
        string sql = "select * from SectionMaster where branchCode=" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"] + "' and ClassNameid="+ddlClass.SelectedValue+"";
        oo.FillDropDown_withValue(sql, ddlSection, "SectionName", "Id");
        ddlSection.Items.Insert(0, new ListItem("<--Select-->", "-1"));
        if (Session["Logintype"].ToString() == "Staff")
        {
            sql = "Select SectionId from ClassTeacherMaster where Empcode='" + Session["LoginName"] + "' and branchCode=" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"] + "'";
            if (oo.Duplicate(sql))
            {
                ddlSection.SelectedValue = oo.ReturnTag(sql, "SectionId");
            }
            else
            {
                ddlSection.Items.Insert(0, new ListItem("<--Select-->", "-100"));
                divExport.Visible = false;
                Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Please allot as a class teacher!", "A");
            }
            divSection.Visible = false;
        }
        else
        {
            ddlSection.SelectedIndex = 0;
            divSection.Visible = true;
        }

    }

    public void GetBranch()
    {
        string sql = "select * from BranchMaster where branchCode=" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"] + "' and Classid=" + ddlClass.SelectedValue + "";
        oo.FillDropDown_withValue(sql, ddlBranch, "BranchName", "Id");
        ddlBranch.Items.Insert(0, new ListItem("<--Select-->", "-1"));
        if (Session["Logintype"].ToString() == "Staff")
        {
            sql = "Select BranchId from ClassTeacherMaster where Empcode='" + Session["LoginName"] + "' and branchCode=" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"] + "'";
            if (oo.Duplicate(sql))
            {
                ddlBranch.SelectedValue = oo.ReturnTag(sql, "BranchId");
            }
            else
            {
                ddlBranch.Items.Insert(0, new ListItem("<--Select-->", "-100"));
                divExport.Visible = false;
                Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Please allot as a class teacher!", "A");
            }
            divBranch.Visible = false;
        }
        else
        {
            ddlBranch.SelectedIndex = 0;
            divBranch.Visible = true;
        }
    }

    protected void ddlClass_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetSection();
        GetBranch();
        GetAttendanceReport();
    }


    protected void lbtnSearch_Click(object sender, EventArgs e)
    {
        GetAttendanceReport();
    }

    private void GetAttendanceReport()
    {
        BAL.clsSearchAttendance obj = new BAL.clsSearchAttendance();

        obj.BranchID = Convert.ToInt32(ddlBranch.SelectedValue);
        obj.ClassID = Convert.ToInt32(ddlClass.SelectedValue);
        obj.SectionID = Convert.ToInt32(ddlSection.SelectedValue);
        obj.AttendanceType = AttendanceStatus;
        obj.FromDate = Convert.ToDateTime(txtFromDate.Text.Trim() == "1 Jan 1900" ? "" : txtFromDate.Text.Trim());
        obj.ToDate = Convert.ToDateTime("1 Jan 1900");
        obj.SrNo = txtSrNo.Text.Trim();

        dt = null;

        dt = DAL.DALInstance.GetAttendanceReport(obj);

        if (dt != null && dt.Rows.Count > 0)
        {
            gvAttendance.DataSource = dt;
        }
        else
        { 
            gvAttendance.DataSource = null;
        }
        gvAttendance.DataBind();
    }
    protected void ddlAttendance_SelectedIndexChanged(object sender, EventArgs e)
    {
        AttendanceStatus = "";
        AttendanceStatus = ddlAttendance.SelectedValue;
        GetAttendanceReport();
    }
    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetAttendanceReport();
    }
    protected void ddlSection_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetAttendanceReport();
    }
    protected void txtFromDate_TextChanged(object sender, EventArgs e)
    {
        GetAttendanceReport();
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }
    protected void ImageButton1_Click(object sender, EventArgs e)
    {
        BAL.objBal.ExportTolandscapeWord(Response, "StudentAttendanceReport", divExport);
    }

    protected void ImageButton2_Click(object sender, EventArgs e)
    {
        BAL.objBal.ExportDivToExcelWithFormatting(Response, "StudentAttendanceReport.xls", divExport);
    }
    protected void ImageButton3_Click(object sender, EventArgs e)
    {
        BAL.objBal.ExporttolandscapePdf(Response, "StudentAttendanceReport", divExport);
    }
    protected void ImageButton4_Click(object sender, EventArgs e)
    {
        PrintHelper_New.ctrl = divExport;
        ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>var winpop=window.open('Print_New.aspx','_blank'); if(!winpop || winpop.closed){alert('Please allow pop-up blocker from browser settings!')}</script>");
    }

}