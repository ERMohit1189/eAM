using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class RoomAllotedReport : System.Web.UI.Page
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
            Loadclass();
        }
    }
    public override void VerifyRenderingInServerForm(Control control)
    {

    }
    public void Loadclass()
    {
        sql = "Select id, ClassName from ClassMaster";
        sql = sql + "  where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
        sql = sql + "  order by Id";
        oo.FillDropDown_withValue(sql, ddlClass, "ClassName", "id");
        ddlClass.Items.Insert(0, new ListItem("<--Select-->", ""));
    }
    protected void txtSearchStudent_TextChanged(object sender, EventArgs e)
    {
        var studentId = Request.Form[hfStudentId.UniqueID];
        if (string.IsNullOrEmpty(studentId))
        {
            studentId = txtSearchStudent.Text.Trim();
        }
        FeeBind(studentId);
    }
    protected void txtSearchStaff_TextChanged(object sender, EventArgs e)
    {
        var StaffId = Request.Form[hfStaffId.UniqueID];
        if (string.IsNullOrEmpty(StaffId))
        {
            StaffId = txtSearchStaff.Text.Trim();
        }
        FeeBind(StaffId);
    }
    protected void lnkView_Click(object sender, EventArgs e)
    {
        if (rdoType.SelectedIndex == 1)
        {
            var studentId = Request.Form[hfStudentId.UniqueID];
            if (string.IsNullOrEmpty(studentId))
            {
                studentId = txtSearchStudent.Text.Trim();
            }
            FeeBind(studentId);
        }
        if (rdoType.SelectedIndex == 2)
        {
            var StaffId = Request.Form[hfStaffId.UniqueID];
            if (string.IsNullOrEmpty(StaffId))
            {
                StaffId = txtSearchStaff.Text.Trim();
            }
            FeeBind(StaffId);
        }
        if (rdoType.SelectedIndex == 0)
        {
            FeeBind("");

        }

    }
    protected void rdoType_SelectedIndexChanged(object sender, EventArgs e)
    {
        hfStaffId.Value = "";
        hfStudentId.Value = "";
        txtSearchStaff.Text = "";
        txtSearchStudent.Text = "";
        if (rdoType.SelectedIndex == 1)
        {
            divStaff.Visible = false;
            divStudent.Visible = true;
            GridView1.Visible = false;
        }
        if (rdoType.SelectedIndex == 2)
        {
            divStaff.Visible = true;
            divStudent.Visible = false;
            GridView1.Visible = false;

        }
        if (rdoType.SelectedIndex == 0)
        {
            divStaff.Visible = false;
            divStudent.Visible = false;
            GridView1.Visible = false;
        }
    }
    protected void FeeBind(string SrNoOrEmpId)
    {
        GridView1.Visible = true;
        DataSet ds = new DataSet();
        sql = "select ra.SrNoOrEmpId, (hcm.CategoryName+' Building '+hlm.BuildingLocation+' room no '+rm.RoomNo+'('+hrm.RoomType+') and bed no '+hbm.BedNo) allotedRoom, hbm.PaymentType,";
        sql = sql + " case when ra.SrType = 'Student' then(select Name from AllStudentRecord_UDF('" + Session["SessionName"] + "', "+Session["BranchCode"] +") where SrNo = ra.SrNoOrEmpId) else (select(EFirstName + ' ' + EMiddleName + ' ' + ELastName) from EmpGeneralDetail where EmpId = ra.SrNoOrEmpId) end name,";
        sql = sql + " case when ra.SrType = 'Student' then(select CombineClassName from AllStudentRecord_UDF('"+Session["SessionName"]+ "', " + Session["BranchCode"] + ") where SrNo = ra.SrNoOrEmpId) else 'Staff' end Class, * ";
        sql = sql + " from RoomAllotment ra";
        sql = sql + " inner join HostelCategoryMaster hcm on hcm.Id = ra.HostelCategoryId inner join HostelLocationMaster hlm on hlm.Id = ra.BuildingLocationId";
        sql = sql + " inner join RoomMaster rm on rm.Id = ra.RoomId inner join HostelRoomTypeMaster hrm on hrm.Id = ra.RoomTypeId inner join HostelBedMaster hbm on hbm.Id = ra.BedId";
        sql = sql + " inner join AllStudentRecord_UDF('" + Session["SessionName"] + "', " + Session["BranchCode"] + ") asr on asr.srno= ra.SrNoOrEmpId ";
        sql = sql + " where ra.sessionname='" + Session["SessionName"].ToString() + "' and hlm.BranchCode=" + Session["BranchCode"].ToString() + " and hcm.BranchCode=" + Session["BranchCode"].ToString() + " and ra.BranchCode=" + Session["BranchCode"].ToString() + " and rm.BranchCode=" + Session["BranchCode"].ToString() + " and ra.LivingStatus = 1 and ra.SrNoOrEmpId= case when '" + SrNoOrEmpId + "'='' then ra.SrNoOrEmpId else '" + SrNoOrEmpId + "' end";
        sql = sql + " and asr.classid= case when '"+ddlClass.SelectedValue + "'='' then asr.classid else '"+ ddlClass.SelectedValue + "' end ";
        ds = oo.GridFill(sql);
        GridView1.DataSource = ds;
        GridView1.DataBind();
        if (GridView1.Rows.Count > 0)
        {
            divExport.Visible = true;
            heading.Text = "List of Allotted Students";
            if (ddlClass.SelectedIndex != 0)
            {
                heading.Text = heading.Text + "("+ddlClass.SelectedItem.Text+")";
            }
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                Label txtBedStatusDamaged = (Label)GridView1.Rows[i].FindControl("txtBedStatusDamaged");
                Label lblRoomAllotmentId = (Label)GridView1.Rows[i].FindControl("lblRoomAllotmentId");
                Label txtBedStatusOk = (Label)GridView1.Rows[i].FindControl("txtBedStatusOk");
                Label txtBookingStatus = (Label)GridView1.Rows[i].FindControl("txtBookingStatus");
                Label txtBookingUnavailable = (Label)GridView1.Rows[i].FindControl("txtBookingUnavailable");
                LinkButton LinkButton3 = (LinkButton)GridView1.Rows[i].FindControl("LinkButton3");
                Label txtMode = (Label)GridView1.Rows[i].FindControl("txtMode");

                if (ds.Tables[0].Rows[i]["LivingStatus"].ToString().ToLower() == "true")
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
    }

    protected void ImageButton1_Click(object sender, EventArgs e)
    {
        oo.ExportTolandscapeWord(Response, "ListofAllottedStudents", gdv1);
    }
    protected void ImageButton2_Click(object sender, EventArgs e)
    {
        oo.ExportDivToExcelWithFormatting(Response, "ListofAllottedStudents.xls", gdv1, Server.MapPath("~/Admin/css/style.css"));
    }
    protected void ImageButton3_Click(object sender, EventArgs e)
    {
        oo.ExporttolandscapePdf(Response, "ListofAllottedStudents", abc);
    }
    protected void ImageButton4_Click(object sender, EventArgs e)
    {
        PrintHelper_New.ctrl = abc;
        ClientScript.RegisterStartupScript(GetType(), "onclick", "<script language=javascript>var winpop=window.open('Print_New.aspx','_blank'); if(!winpop || winpop.closed){alert('Please allow pop-up blocker from browser settings!')}</script>");
    }
}