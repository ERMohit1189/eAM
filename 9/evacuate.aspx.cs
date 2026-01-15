using System.Data;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System;

public partial class evacuate : System.Web.UI.Page
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

        }
    }
    protected void txtSearchStudent_TextChanged(object sender, EventArgs e)
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
            GridView1.Visible = false;

            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox0, "Sorry, No record found!", "A");
        }
        else
        {
            string sql2 = "";
            sql2 = "select (hcm.CategoryName+' Building '+hlm.BuildingLocation+' Room No. '+rm.RoomNo+'('+hrm.RoomType+') and Bed No. '+hbm.BedNo) allotedRoom, ra.TotalMonths from RoomAllotment ra inner join HostelCategoryMaster hcm on hcm.Id = ra.HostelCategoryId inner join HostelLocationMaster hlm on hlm.Id = ra.BuildingLocationId";
            sql2 = sql2 + " inner join RoomMaster rm on rm.Id = ra.RoomId inner join HostelRoomTypeMaster hrm on hrm.Id = ra.RoomTypeId inner join HostelBedMaster hbm on hbm.Id = ra.BedId where ra.SrNoOrEmpId = '" + studentId + "' and hlm.BranchCode=" + Session["BranchCode"].ToString() + " and hcm.BranchCode=" + Session["BranchCode"].ToString() + " and rm.BranchCode=" + Session["BranchCode"].ToString() + " and ra.BranchCode=" + Session["BranchCode"].ToString() + " and ra.LivingStatus = 1";
            var dss = oo.GridFill(sql2);
            if (dss != null && dss.Tables[0].Rows.Count > 0)
            {
                Campus camp = new Campus(); camp.msgbox(this.Page, msgbox0, "This student already stay in " + dss.Tables[0].Rows[0]["allotedRoom"].ToString() + " !", "A");
                div1.Visible = true;
                div2.Visible = false;
                div3.Visible = true;
                GridView1.Visible = true;
            }
            else
            {
                div1.Visible = true;
                div2.Visible = true;
                div3.Visible = true;
                GridView1.Visible = true;
            }


            grdStRecord.DataSource = ds;
            grdStRecord.DataBind();
            BedBind(studentId);
        }
    }
    protected void txtSearchStaff_TextChanged(object sender, EventArgs e)
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
            GridView1.Visible = false;
            Grd.DataSource = null;
            Grd.DataBind();
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox0, "Sorry, No record found!", "A");
        }
        else
        {
            string sql2 = "";
            sql2 = "select (hcm.CategoryName+' Building '+hlm.BuildingLocation+' Room No. '+rm.RoomNo+'('+hrm.RoomType+') and Bed No. '+hbm.BedNo) allotedRoom, ra.TotalMonths from RoomAllotment ra inner join HostelCategoryMaster hcm on hcm.Id = ra.HostelCategoryId inner join HostelLocationMaster hlm on hlm.Id = ra.BuildingLocationId";
            sql2 = sql2 + " inner join RoomMaster rm on rm.Id = ra.RoomId inner join HostelRoomTypeMaster hrm on hrm.Id = ra.RoomTypeId inner join HostelBedMaster hbm on hbm.Id = ra.BedId where ra.SrNoOrEmpId = '" + StaffId + "' and rm.BranchCode=" + Session["BranchCode"].ToString() + " and hlm.BranchCode=" + Session["BranchCode"].ToString() + " and hcm.BranchCode=" + Session["BranchCode"].ToString() + " and ra.BranchCode=" + Session["BranchCode"].ToString() + " and ra.LivingStatus = 1";
            var dss = oo.GridFill(sql2);
            if (dss != null && dss.Tables[0].Rows.Count > 0)
            {
                Campus camp = new Campus(); camp.msgbox(this.Page, msgbox0, "This staff already stay in " + dss.Tables[0].Rows[0]["allotedRoom"].ToString() + " !", "A");
                div1.Visible = true;
                div2.Visible = false;
                div3.Visible = false;
                GridView1.Visible = true;
            }
            else
            {
                div1.Visible = true;
                div2.Visible = true;
                div3.Visible = false;
                GridView1.Visible = true;
            }
            Grd.DataSource = ds;
            Grd.DataBind();
            BedBind(StaffId);
        }
    }
    protected void lnkView_Click(object sender, EventArgs e)
    {
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
                GridView1.Visible = false;
                grdStRecord.DataSource = null;
                grdStRecord.DataBind();
                Campus camp = new Campus(); camp.msgbox(this.Page, msgbox0, "Sorry, No record found!", "A");
            }
            else
            {
                string sql2 = "";
                sql2 = "select (hcm.CategoryName+' Building '+hlm.BuildingLocation+' Room No. '+rm.RoomNo+'('+hrm.RoomType+') and Bed No. '+hbm.BedNo) allotedRoom, ra.TotalMonths from RoomAllotment ra inner join HostelCategoryMaster hcm on hcm.Id = ra.HostelCategoryId inner join HostelLocationMaster hlm on hlm.Id = ra.BuildingLocationId";
                sql2 = sql2 + " inner join RoomMaster rm on rm.Id = ra.RoomId inner join HostelRoomTypeMaster hrm on hrm.Id = ra.RoomTypeId inner join HostelBedMaster hbm on hbm.Id = ra.BedId where ra.SrNoOrEmpId = '" + studentId + "' and rm.BranchCode=" + Session["BranchCode"].ToString() + " and hcm.BranchCode=" + Session["BranchCode"].ToString() + " and hbm.BranchCode=" + Session["BranchCode"].ToString() + " and ra.LivingStatus = 1";
                var dss = oo.GridFill(sql2);
                if (dss != null && dss.Tables[0].Rows.Count > 0)
                {
                    Campus camp = new Campus(); camp.msgbox(this.Page, msgbox0, "This student already stay in " + dss.Tables[0].Rows[0]["allotedRoom"].ToString() + " !", "A");
                    div1.Visible = true;
                    div2.Visible = false;
                    div3.Visible = true;
                    GridView1.Visible = true;
                }
                else
                {
                    div1.Visible = true;
                    div2.Visible = true;
                    div3.Visible = true;
                    GridView1.Visible = true;
                }
                grdStRecord.DataSource = ds;
                grdStRecord.DataBind();
                BedBind(studentId);
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
                GridView1.Visible = false;
                Grd.DataSource = null;
                Grd.DataBind();
                Campus camp = new Campus(); camp.msgbox(this.Page, msgbox0, "Sorry, No record found!", "A");
            }
            else
            {
                string sql2 = "";
                sql2 = "select (hcm.CategoryName+' Building '+hlm.BuildingLocation+' Room No. '+rm.RoomNo+'('+hrm.RoomType+') and Bed No. '+hbm.BedNo) allotedRoom, ra.TotalMonths  from RoomAllotment ra inner join HostelCategoryMaster hcm on hcm.Id = ra.HostelCategoryId inner join HostelLocationMaster hlm on hlm.Id = ra.BuildingLocationId";
                sql2 = sql2 + " inner join RoomMaster rm on rm.Id = ra.RoomId inner join HostelRoomTypeMaster hrm on hrm.Id = ra.RoomTypeId inner join HostelBedMaster hbm on hbm.Id = ra.BedId where ra.SrNoOrEmpId = '" + StaffId + "' and hlm.BranchCode=" + Session["BranchCode"].ToString() + " and hcm.BranchCode=" + Session["BranchCode"].ToString() + " and rm.BranchCode=" + Session["BranchCode"].ToString() + " and ra.BranchCode=" + Session["BranchCode"].ToString() + " and ra.LivingStatus = 1";
                var dss = oo.GridFill(sql2);
                if (dss != null && dss.Tables[0].Rows.Count > 0)
                {
                    Campus camp = new Campus(); camp.msgbox(this.Page, msgbox0, "This staff already stay in " + dss.Tables[0].Rows[0]["allotedRoom"].ToString() + " !", "A");
                    div1.Visible = true;
                    div2.Visible = false;
                    div3.Visible = false;
                    GridView1.Visible = true;
                }
                else
                {
                    div1.Visible = true;
                    div2.Visible = true;
                    div3.Visible = false;
                    GridView1.Visible = true;
                }
                Grd.DataSource = ds;
                Grd.DataBind();
                BedBind(StaffId);
            }
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
            GridView1.Visible = false;
        }
        else
        {
            divStaff.Visible = true;
            divStudent.Visible = false;
            div2.Visible = true;
            div1.Visible = false;
            div3.Visible = false;
            GridView1.Visible = false;

        }
    }


    protected void BedBind(string SrNoOrEmpId)
    {
        DataSet ds = new DataSet();
        sql = "select (hcm.CategoryName+' Building '+hlm.BuildingLocation+' room no '+rm.RoomNo+'('+hrm.RoomType+') and bed no '+hbm.BedNo) allotedRoom, ra.TotalMonths, (select top(1) BalanceAmount from CompositFeeDeposit where SrNoOrEmpid='" + SrNoOrEmpId + "' and ra.RoomAllotmentId=RoomAllotmentId   order by id desc) NextDue, ra.LivingStatus, ra.RoomAllotmentId, hbm.BedCharge, hbm.Status, hbm.BookedStatus, hbm.id as bedid, rm.Remark, ra.DateFrom, ra.DateTo, ra.TotalMonths, ra.TotalAmount from RoomAllotment ra inner join HostelCategoryMaster hcm on hcm.Id = ra.HostelCategoryId inner join HostelLocationMaster hlm on hlm.Id = ra.BuildingLocationId";
        sql = sql + " inner join RoomMaster rm on rm.Id = ra.RoomId inner join HostelRoomTypeMaster hrm on hrm.Id = ra.RoomTypeId inner join HostelBedMaster hbm on hbm.Id = ra.BedId where ra.SrNoOrEmpId = '" + SrNoOrEmpId + "' and hcm.BranchCode=" + Session["BranchCode"].ToString() + " and hbm.BranchCode=" + Session["BranchCode"].ToString() + " and hlm.BranchCode=" + Session["BranchCode"].ToString() + " and rm.BranchCode=" + Session["BranchCode"].ToString() + " and ra.BranchCode=" + Session["BranchCode"].ToString() + " order by ra.RoomAllotmentId desc";
        ds = oo.GridFill(sql);
        GridView1.DataSource = ds;
        GridView1.DataBind();

        for (int i = 0; i < GridView1.Rows.Count; i++)
        {
            Label txtBedStatusDamaged = (Label)GridView1.Rows[i].FindControl("txtBedStatusDamaged");
            Label txtBedStatusOk = (Label)GridView1.Rows[i].FindControl("txtBedStatusOk");
            Label txtBookingStatus = (Label)GridView1.Rows[i].FindControl("txtBookingStatus");
            Label txtBookingUnavailable = (Label)GridView1.Rows[i].FindControl("txtBookingUnavailable");
            Label txtTotal = (Label)GridView1.Rows[i].FindControl("txtTotal");
            Label txtNextDue = (Label)GridView1.Rows[i].FindControl("txtNextDue");
            Label txtremark = (Label)GridView1.Rows[i].FindControl("txtremark");
            LinkButton LinkButton3 = (LinkButton)GridView1.Rows[i].FindControl("LinkButton3");

            string ss1 = "select count(*) cnt from CompositFeeDeposit where FeeHeadId=(select id from FeeHeadMaster where FeeType='Hostel Fee' and BranchCode=" + Session["BranchCode"] + ") and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " and SrNo='" + SrNoOrEmpId + "'";
            if (oo.ReturnTag(ss1, "cnt") != "0")
            {
                string ss2 = "select sum(ManualDiscount+OnpageDisc+SiblingDisc+CompleteFeeDisc+SpecialDisc+PaidAmount) paid from CompositFeeDeposit where FeeHeadId=(select id from FeeHeadMaster where FeeType='Hostel Fee' and BranchCode=" + Session["BranchCode"] + ") and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " and SrNo='" + SrNoOrEmpId + "'";
                txtNextDue.Text = (double.Parse(txtTotal.Text) - double.Parse(oo.ReturnTag(ss2, "paid"))).ToString("0.00");
            }
            else
            {
                txtNextDue.Text = txtTotal.Text;
            }

            if (ds.Tables[0].Rows[i]["LivingStatus"].ToString().ToLower() == "true")
            {
                txtBookingStatus.Visible = true;
                txtBookingUnavailable.Visible = false;
                LinkButton3.Visible = true;
            }
            else
            {
                txtBookingStatus.Visible = false;
                txtBookingUnavailable.Visible = true;
                LinkButton3.Visible = false;

            }
            if (txtNextDue.Text!="" && ds.Tables[0].Rows[i]["LivingStatus"].ToString().ToLower() == "true")
            {
                if (double.Parse(txtNextDue.Text)>0)
                {
                    LinkButton3.Visible = false;
                    txtremark.Text = txtremark.Text + "<br>Can't be evacuate because of due payment!";
                    txtremark.ForeColor = System.Drawing.Color.Red;
                }
                if (double.Parse(txtNextDue.Text) == 0)
                {
                    LinkButton3.Visible = true;
                    txtremark.Text = txtremark.Text + "<br>Can be evacuate because of complete paid.";
                    txtremark.ForeColor = System.Drawing.Color.Green;
                }
            }
        }
    }
    protected void LinkButton3_Click(object sender, EventArgs e)
    {
        

        Button8.Focus();
        LinkButton chk = (LinkButton)sender;
        Label lblId = (Label)chk.NamingContainer.FindControl("lblBedId");
        Label txtBookingStatus = (Label)chk.NamingContainer.FindControl("txtBookingStatus");
        Label lblBookedStatus = (Label)chk.NamingContainer.FindControl("lblBookedStatus");
        Label txtTotal = (Label)chk.NamingContainer.FindControl("txtTotal");
        Label txtNextDue = (Label)chk.NamingContainer.FindControl("txtNextDue");
        if (double.Parse(txtTotal.Text)> double.Parse(txtNextDue.Text==""?"0": txtNextDue.Text))
        {
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox0, "Please pay complete hostel fee!", "A");
            return;
        }
        string ss = lblId.Text;
        lblvalue.Text = ss.ToString();
        Panel2_ModalPopupExtender.Show();
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        


        sql = "update HostelBedMaster set BookedStatus=0 where id=" + lblvalue.Text+ " and BranchCode=" + Session["BranchCode"].ToString() + "";
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = sql;
        cmd.CommandType = CommandType.Text;
        cmd.Connection = con;
        try
        {
            int y = 0;
            con.Open();
            int x = cmd.ExecuteNonQuery();
            if (x > 0)
            {
                sql = "update RoomAllotment set LivingStatus=0, ReleaseDate=getdate() where SrNoOrEmpId = '" + (rdoType.SelectedIndex == 0 ? hfStudentId.Value : hfStaffId.Value) + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
                SqlCommand cmds = new SqlCommand();
                cmds.CommandText = sql;
                cmds.CommandType = CommandType.Text;
                cmds.Connection = con;
                y = cmds.ExecuteNonQuery();
            }
            con.Close();
            if (y > 0)
            {
                BedBind((rdoType.SelectedIndex == 0 ? hfStudentId.Value : hfStaffId.Value));
                Campus camp = new Campus(); camp.msgbox(this.Page, msgbox0, "Released successfully", "S");
            }
            else
            {
                Campus camp = new Campus(); camp.msgbox(this.Page, msgbox0, "Releasing not successfull", "A");
            }
            
        }
        catch (SqlException) { }
    }
    protected void Button8_Click(object sender, EventArgs e)
    {
    }
}