using System.Data;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web.UI;

public partial class HostelFeePayment : Page
{
    SqlConnection con = new SqlConnection();
    Campus oo = new Campus();
    string sql = ""; string studentIdm = "";
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
           oo.AddDateMonthYearDropDown(DDYear, DDMonth, DDDate);
           oo.FindCurrentDateandSetinDropDown(DDYear, DDMonth, DDDate);

           oo.AddDateMonthYearDropDown(DDChkYear, DDChkMonth, DDChkDate);
           oo.FindCurrentDateandSetinDropDown(DDChkYear, DDChkMonth, DDChkDate);

            var ds1 = DLL.objDll.SelectRecord_usingExecuteDataset("USP_GetGatewayList");
            if (ds1.Tables != null)
            {
                if (ds1.Tables[1].Rows.Count > 0)
                {
                    data_key.Text = ds1.Tables[1].Rows[0]["LivePublicKey"].ToString();
                    data_email.Text = ds1.Tables[1].Rows[0]["Email"].ToString();
                    data_PseudoUniqueReference.Text = ds1.Tables[1].Rows[0]["PseudoUniqueReference"].ToString();
                }


            }
            studentIdm = Session["Srno"].ToString().Trim();
            hfStudentId.Value = studentIdm;
            if (string.IsNullOrEmpty(studentIdm))
            {
                studentIdm = txtSearchStudent.Text.Trim();
            }
            var ds = BLL.BLLInstance.GetStudentDetails(studentIdm, Session["SessionName"].ToString(), Session["BranchCode"].ToString());
            if (ds == null || ds.Tables[0].Rows.Count == 0)
            {
                div1.Visible = false;
                div2.Visible = false;
                div3.Visible = false;
                GridView1.Visible = false;

                 camp.msgbox(this.Page, msgbox0, "Sorry, No record found!", "A");
            }
            else
            {
                string sql2 = "";
                sql2 = "select (hcm.CategoryName+' Building '+hlm.BuildingLocation+' Room No. '+rm.RoomNo+'('+hrm.RoomType+') and Bed No. '+hbm.BedNo) allotedRoom from RoomAllotment ra inner join HostelCategoryMaster hcm on hcm.Id = ra.HostelCategoryId inner join HostelLocationMaster hlm on hlm.Id = ra.BuildingLocationId";
                sql2 = sql2 + " inner join RoomMaster rm on rm.Id = ra.RoomId inner join HostelRoomTypeMaster hrm on hrm.Id = ra.RoomTypeId inner join HostelBedMaster hbm on hbm.Id = ra.BedId where ra.SrNoOrEmpId = '" + studentIdm + "' and hlm.BranchCode=" + Session["BranchCode"] + " and hcm.BranchCode=" + Session["BranchCode"] + " and rm.BranchCode=" + Session["BranchCode"] + " and ra.BranchCode=" + Session["BranchCode"] + " and ra.LivingStatus = 1";
                var dss = oo.GridFill(sql2);
                if (dss != null && dss.Tables[0].Rows.Count > 0)
                {
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
                FeeBind(studentIdm);
                if (ds != null)
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
                            cmd.Parameters.AddWithValue("@SrNo", studentIdm.ToString().Trim());
                            cmd.Parameters.AddWithValue("@action", "details");
                            SqlDataAdapter das = new SqlDataAdapter(cmd);
                            DataSet dsPhoto = new DataSet();
                            das.Fill(dsPhoto);
                            cmd.Parameters.Clear();


                            if (dsPhoto.Tables[0].Rows.Count > 0)
                            {
                                img.ImageUrl = dsPhoto.Tables[0].Rows[0]["PhotoPath"].ToString() != string.Empty ? dsPhoto.Tables[0].Rows[0]["PhotoPath"] + "?value=" + DateTime.Now.ToString(CultureInfo.InvariantCulture) : "../img/user-pic/user-pic.jpg" + "?value=" + DateTime.Now.ToString(CultureInfo.InvariantCulture);
                                studentImg.NavigateUrl = dsPhoto.Tables[0].Rows[0]["PhotoPath"].ToString() != string.Empty ? dsPhoto.Tables[0].Rows[0]["PhotoPath"] + "?value=" + DateTime.Now.ToString(CultureInfo.InvariantCulture) : "../img/user-pic/user-pic.jpg" + "?value=" + DateTime.Now.ToString(CultureInfo.InvariantCulture);
                                hylinkmoredetails.NavigateUrl = "../11/StudentRegView.aspx?print=1&id=" + ds.Tables[0].Rows[0]["StEnRCode"];
                            }
                        }
                    }
                }
            }
        }
        
    }
    protected void DDYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        oo.YearDropDown(DDYear, DDMonth, DDDate);
    }

    protected void DDMonth_SelectedIndexChanged(object sender, EventArgs e)
    {
        oo.MonthDropDown(DDYear, DDMonth, DDDate);
    }

    protected void DDChkYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        oo.YearDropDown(DDChkYear, DDChkMonth, DDChkDate);
    }

    protected void DDChkMonth_SelectedIndexChanged(object sender, EventArgs e)
    {
        oo.YearDropDown(DDChkYear, DDChkMonth, DDChkDate);
    }

    protected void DropDownMOD_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DropDownMOD.SelectedIndex != 0 && DropDownMOD.SelectedIndex != 3)
        {
            txtChequeNo.CssClass = "form-control-blue validatetxt";
            Label54.Text = DropDownMOD.SelectedItem.Text + " Date";
            lblBankName.Text = "Bank Name";

            switch (DropDownMOD.SelectedIndex)
            {
                case 5:
                    Label42.Text = "Ref. No.";
                    break;
                case 6:
                    Label42.Text = "UTR No.";
                    break;
                case 7:
                    Label42.Text = "Ref. No.";
                    Label54.Text = "Ref. Date";
                    lblBankName.Text = "Remark";
                    break;
                default:
                    if (DropDownMOD.SelectedIndex == 6)
                    {
                        Label42.Text = "Ref. No.";
                        Label54.Text = "Ref. Date";
                        lblBankName.Text = "Remark";
                    }
                    else
                    {
                        Label42.Text = DropDownMOD.SelectedItem.Text + " No.";
                    }
                    break;
            }

            drpStatus.SelectedIndex = 1;
            div5.Visible = true;

            switch (DropDownMOD.SelectedIndex)
            {
                case 4:
                    div8.Visible = false;
                    txtChequeNo.CssClass = "form-control-blue";
                    drpStatus.SelectedIndex = 0;
                    break;
                case 5:
                    drpStatus.SelectedIndex = 0;
                    break;
                case 6:
                    drpStatus.SelectedIndex = 0;
                    break;
            }

            div8.Visible = true;
            div9.Visible = true;
        }
        else
        {
            div9.Visible = false;
            div5.Visible = false;
            txtChequeNo.CssClass = "form-control-blue";
            drpStatus.SelectedIndex = 0;
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
            sql2 = "select (hcm.CategoryName+' Building '+hlm.BuildingLocation+' Room No. '+rm.RoomNo+'('+hrm.RoomType+') and Bed No. '+hbm.BedNo) allotedRoom from RoomAllotment ra inner join HostelCategoryMaster hcm on hcm.Id = ra.HostelCategoryId inner join HostelLocationMaster hlm on hlm.Id = ra.BuildingLocationId";
            sql2 = sql2 + " inner join RoomMaster rm on rm.Id = ra.RoomId inner join HostelRoomTypeMaster hrm on hrm.Id = ra.RoomTypeId inner join HostelBedMaster hbm on hbm.Id = ra.BedId where ra.SrNoOrEmpId = '" + studentId + "' and hlm.BranchCode=" + Session["BranchCode"] + " and hcm.BranchCode=" + Session["BranchCode"] + " and hlm.BranchCode=" + Session["BranchCode"] + " and rm.BranchCode=" + Session["BranchCode"] + " and ra.BranchCode=" + Session["BranchCode"] + " and ra.LivingStatus = 1";
            var dss = oo.GridFill(sql2);
            if (dss != null && dss.Tables[0].Rows.Count > 0)
            {
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
            FeeBind(studentId);
            if (ds!=null)
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
                            img.ImageUrl = dsPhoto.Tables[0].Rows[0]["PhotoPath"].ToString() != string.Empty ? dsPhoto.Tables[0].Rows[0]["PhotoPath"] + "?value=" + DateTime.Now.ToString(CultureInfo.InvariantCulture) : "../img/user-pic/user-pic.jpg" + "?value=" + DateTime.Now.ToString(CultureInfo.InvariantCulture);
                            studentImg.NavigateUrl = dsPhoto.Tables[0].Rows[0]["PhotoPath"].ToString() != string.Empty ? dsPhoto.Tables[0].Rows[0]["PhotoPath"] + "?value=" + DateTime.Now.ToString(CultureInfo.InvariantCulture) : "../img/user-pic/user-pic.jpg" + "?value=" + DateTime.Now.ToString(CultureInfo.InvariantCulture);
                            hylinkmoredetails.NavigateUrl = "../11/StudentRegView.aspx?print=1&id=" + ds.Tables[0].Rows[0]["StEnRCode"];
                        }
                    }
                }
            }
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
        sql = sql + " and (eod.Ecode='" + StaffId.Trim() + "' or eod.EmpId='" + StaffId.Trim() + "') and egd.BranchCode=" + Session["BranchCode"] + " and eod.BranchCode=" + Session["BranchCode"] + "";
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
            sql2 = "select (hcm.CategoryName+' Building '+hlm.BuildingLocation+' Room No. '+rm.RoomNo+'('+hrm.RoomType+') and Bed No. '+hbm.BedNo) allotedRoom from RoomAllotment ra inner join HostelCategoryMaster hcm on hcm.Id = ra.HostelCategoryId inner join HostelLocationMaster hlm on hlm.Id = ra.BuildingLocationId";
            sql2 = sql2 + " inner join RoomMaster rm on rm.Id = ra.RoomId inner join HostelRoomTypeMaster hrm on hrm.Id = ra.RoomTypeId inner join HostelBedMaster hbm on hbm.Id = ra.BedId where ra.SrNoOrEmpId = '" + StaffId + "' and hcm.BranchCode=" + Session["BranchCode"] + " and rm.BranchCode=" + Session["BranchCode"] + " and ra.BranchCode=" + Session["BranchCode"] + " and hlm.BranchCode=" + Session["BranchCode"] + " and ra.LivingStatus = 1";
            var dss = oo.GridFill(sql2);
            if (dss != null && dss.Tables[0].Rows.Count > 0)
            {
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
            FeeBind(StaffId);
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
                sql2 = "select (hcm.CategoryName+' Building '+hlm.BuildingLocation+' Room No. '+rm.RoomNo+'('+hrm.RoomType+') and Bed No. '+hbm.BedNo) allotedRoom from RoomAllotment ra inner join HostelCategoryMaster hcm on hcm.Id = ra.HostelCategoryId inner join HostelLocationMaster hlm on hlm.Id = ra.BuildingLocationId";
                sql2 = sql2 + " inner join RoomMaster rm on rm.Id = ra.RoomId inner join HostelRoomTypeMaster hrm on hrm.Id = ra.RoomTypeId inner join HostelBedMaster hbm on hbm.Id = ra.BedId where ra.SrNoOrEmpId = '" + studentId + "' and ra.LivingStatus = 1";
                var dss = oo.GridFill(sql2);
                if (dss != null && dss.Tables[0].Rows.Count > 0)
                {
                    div1.Visible = true;
                    div2.Visible = false;
                    div3.Visible = true;
                    GridView1.Visible = true;
                    if (ds != null)
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
                                    img.ImageUrl = dsPhoto.Tables[0].Rows[0]["PhotoPath"].ToString() != string.Empty ? dsPhoto.Tables[0].Rows[0]["PhotoPath"] + "?value=" + DateTime.Now.ToString(CultureInfo.InvariantCulture) : "../img/user-pic/user-pic.jpg" + "?value=" + DateTime.Now.ToString(CultureInfo.InvariantCulture);
                                    studentImg.NavigateUrl = dsPhoto.Tables[0].Rows[0]["PhotoPath"].ToString() != string.Empty ? dsPhoto.Tables[0].Rows[0]["PhotoPath"] + "?value=" + DateTime.Now.ToString(CultureInfo.InvariantCulture) : "../img/user-pic/user-pic.jpg" + "?value=" + DateTime.Now.ToString(CultureInfo.InvariantCulture);
                                    hylinkmoredetails.NavigateUrl = "../11/StudentRegView.aspx?print=1&id=" + ds.Tables[0].Rows[0]["StEnRCode"];
                                }
                            }
                        }
                    }
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
                FeeBind(studentId);
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
            sql = sql + " and (eod.Ecode='" + StaffId.Trim() + "' or eod.EmpId='" + StaffId.Trim() + "') and egd.BranchCode = " + Session["BranchCode"] + " and eod.BranchCode = " + Session["BranchCode"] + "";
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
                sql2 = "select (hcm.CategoryName+' Building '+hlm.BuildingLocation+' Room No. '+rm.RoomNo+'('+hrm.RoomType+') and Bed No. '+hbm.BedNo) allotedRoom from RoomAllotment ra inner join HostelCategoryMaster hcm on hcm.Id = ra.HostelCategoryId inner join HostelLocationMaster hlm on hlm.Id = ra.BuildingLocationId";
                sql2 = sql2 + " inner join RoomMaster rm on rm.Id = ra.RoomId inner join HostelRoomTypeMaster hrm on hrm.Id = ra.RoomTypeId inner join HostelBedMaster hbm on hbm.Id = ra.BedId where ra.SrNoOrEmpId = '" + StaffId + "' and rm.BranchCode = " + Session["BranchCode"] + " and ra.BranchCode = " + Session["BranchCode"] + " and hcm.BranchCode = " + Session["BranchCode"] + " and hlm.BranchCode = " + Session["BranchCode"] + " and ra.LivingStatus = 1";
                var dss = oo.GridFill(sql2);
                if (dss != null && dss.Tables[0].Rows.Count > 0)
                {
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
                FeeBind(StaffId);
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
    protected void FeeBind(string SrNoOrEmpId)
    {
        DataSet ds = new DataSet();
        sql = "select top(1) (hcm.CategoryName+' Building '+hlm.BuildingLocation+' room no '+rm.RoomNo+'('+hrm.RoomType+') and bed no '+hbm.BedNo) allotedRoom, ra.FrequencyofPayment, ra.LivingStatus, ra.RoomAllotmentId, hbm.BedCharge, hbm.Status, hbm.BookedStatus, hbm.id as bedid, rm.Remark, ra.DateFrom, ra.DateTo, ra.TotalMonths, ra.TotalAmount from RoomAllotment ra inner join HostelCategoryMaster hcm on hcm.Id = ra.HostelCategoryId inner join HostelLocationMaster hlm on hlm.Id = ra.BuildingLocationId";
        sql = sql + " inner join RoomMaster rm on rm.Id = ra.RoomId inner join HostelRoomTypeMaster hrm on hrm.Id = ra.RoomTypeId inner join HostelBedMaster hbm on hbm.Id = ra.BedId where ra.SrNoOrEmpId = '" + SrNoOrEmpId + "' and rm.BranchCode = " + Session["BranchCode"] + " and ra.BranchCode = " + Session["BranchCode"] + " and hcm.BranchCode = " + Session["BranchCode"] + " and hlm.BranchCode = " + Session["BranchCode"] + " and LivingStatus=1 order by ra.RoomAllotmentId desc";
        ds = oo.GridFill(sql);
        GridView1.DataSource = ds;
        GridView1.DataBind();
        if (ds != null)
        {

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
                if (ds.Tables[0].Rows[i]["BookedStatus"].ToString().ToLower() == "true")
                {
                    if (txtMode.Text.ToLower() == "onetime")
                    {
                        OntimeGridBind(SrNoOrEmpId, lblRoomAllotmentId.Text);
                        GridMonthly.DataSource = null;
                        GridMonthly.DataBind();
                    }
                    else
                    {
                        MonthlyGridBind();
                        GridOneTime.DataSource = null;
                        GridOneTime.DataBind();
                    }
                }
            }
        }
    }
    public static IEnumerable<Tuple<string, int>> MonthsBetween(DateTime startDate, DateTime endDate)
    {
        DateTime iterator;
        DateTime limit;

        if (endDate > startDate)
        {
            iterator = new DateTime(startDate.Year, startDate.Month, 1);
            limit = endDate;
        }
        else
        {
            iterator = new DateTime(endDate.Year, endDate.Month, 1);
            limit = startDate;
        }

        var dateTimeFormat = CultureInfo.CurrentCulture.DateTimeFormat;
        while (iterator <= limit)
        {
            yield return Tuple.Create(
                dateTimeFormat.GetMonthName(iterator.Month),
                iterator.Year);
            iterator = iterator.AddMonths(1);
        }
    }
    protected void OntimeGridBind(string SrNoOrEmpId, string RoomAllotmentId)
    {
        lbltotal.Text = "0";
        txtFineO.Text = "0";
        txtExemptionO.Text = "0";
        txtPayble.Text = "0";
        hdnPayble.Value = "0";
        lblNextDue.Text = "0";
        double Fine = 0; double Exemption = 0; double Paid = 0; double Due = 0;
        DataSet ds = new DataSet(); DataTable dt = new DataTable();
        sql = "select * from HostelFeeDeposit where RoomAllotmentId = " + RoomAllotmentId + " and BranchCode = " + Session["BranchCode"] + " and SrNoOrEmpid = '" + SrNoOrEmpId + "'";
        ds= oo.GridFill(sql);

        if (ds.Tables[0].Rows.Count > 0)
        {
            GridOneTime.DataSource = ds;
            GridOneTime.DataBind();

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                Fine = Fine + double.Parse(ds.Tables[0].Rows[i]["Fine"].ToString());
                Exemption = Exemption + double.Parse(ds.Tables[0].Rows[i]["Exemption"].ToString());
                Paid = Paid + double.Parse(ds.Tables[0].Rows[i]["Paid"].ToString());
            }
            dt = ds.Tables[0];
            GridOneTime.DataSource = dt;
            GridOneTime.DataBind();
            for (int i = 1; i <= GridOneTime.Rows.Count; i++)
            {
                Label lblDueO = (Label)GridOneTime.Rows[i - 1].FindControl("lblDueO");
                if (i == GridOneTime.Rows.Count && double.Parse(lblDueO.Text) > 0)
                {
                    div4.Visible = true;
                    oneTimediv.Visible = true;

                    lbltotal.Text = double.Parse(lblDueO.Text).ToString("0");
                    txtFineO.Text = "0";
                    txtExemptionO.Text = "0";
                    txtPayble.Text = double.Parse(lblDueO.Text).ToString("0");
                    hdnPayble.Value = double.Parse(lblDueO.Text).ToString("0");
                    lblNextDue.Text = "0";
                }
                else
                {
                    div4.Visible = false;
                    oneTimediv.Visible = false;
                    div5.Visible = false;
                    Campus camp = new Campus(); camp.msgbox(this.Page, msgbox0, "Complete fee submitted for this room.", "S");
                }
            }
        }
        else
        {
            Label txtTotal = (Label)GridView1.Rows[0].FindControl("txtTotal");
            lbltotal.Text = double.Parse(txtTotal.Text).ToString("0");
            txtFineO.Text = "0";
            txtExemptionO.Text = "0";
            txtPayble.Text = double.Parse(txtTotal.Text).ToString("0");
            hdnPayble.Value = double.Parse(txtTotal.Text).ToString("0");
            div4.Visible = true;
            oneTimediv.Visible = true;
        }
        
    }

    protected void lblFineO_TextChanged(object sender, EventArgs e)
    {
        
        if (txtFineO.Text!="" && txtFineO.Text!="0")
        {
            double Payble = (double.Parse(lbltotal.Text) + double.Parse(txtFineO.Text == "" ? "0" : txtFineO.Text))- double.Parse(txtExemptionO.Text == "" ? "0" : txtExemptionO.Text);
            txtPayble.Text = Payble.ToString("0");
            hdnPayble.Value = Payble.ToString("0");
            lblNextDue.Text = (double.Parse(hdnPayble.Value) - double.Parse(txtPayble.Text)).ToString();
        }
        else
        {
            txtFineO.Text = "0";
            txtExemptionO.Text = "0";
            txtPayble.Text = double.Parse(lbltotal.Text).ToString();
            hdnPayble.Value = double.Parse(lbltotal.Text).ToString();
            lblNextDue.Text = "0";
        }
        if (double.Parse(txtPayble.Text)<=0)
        {
            txtFineO.Text = "0";
            txtExemptionO.Text = "0";
            txtPayble.Text = double.Parse(lbltotal.Text).ToString();
            hdnPayble.Value = double.Parse(lbltotal.Text).ToString();
            lblNextDue.Text = "0";
        }
    }

    protected void txtExemptionO_TextChanged(object sender, EventArgs e)
    {
        if (txtExemptionO.Text != "" && txtExemptionO.Text != "0")
        {
            double Payble = (double.Parse(lbltotal.Text) + double.Parse(txtFineO.Text == "" ? "0" : txtFineO.Text)) - double.Parse(txtExemptionO.Text == "" ? "0" : txtExemptionO.Text);
            txtPayble.Text = Payble.ToString("0");
            hdnPayble.Value = Payble.ToString("0");
            lblNextDue.Text = (double.Parse(hdnPayble.Value) - double.Parse(txtPayble.Text)).ToString();
        }
        else
        {
            txtFineO.Text = "0";
            txtExemptionO.Text = "0";
            txtPayble.Text = double.Parse(lbltotal.Text).ToString();
            hdnPayble.Value = double.Parse(lbltotal.Text).ToString();
            lblNextDue.Text = "0";
        }
        if (double.Parse(txtPayble.Text) <= 0)
        {
            txtFineO.Text = "0";
            txtExemptionO.Text = "0";
            txtPayble.Text = double.Parse(lbltotal.Text).ToString();
            hdnPayble.Value = double.Parse(lbltotal.Text).ToString();
            lblNextDue.Text = "0";
        }
    }
    protected void txtPayble_TextChanged(object sender, EventArgs e)
    {
        if (txtPayble.Text != "" && txtPayble.Text != "0")
        {
            lblNextDue.Text = (double.Parse(hdnPayble.Value) - double.Parse(txtPayble.Text)).ToString();
        }
        else if (double.Parse(txtPayble.Text)> double.Parse(hdnPayble.Value))
        {
            txtFineO.Text = "0";
            txtExemptionO.Text = "0";
            txtPayble.Text = double.Parse(lbltotal.Text).ToString();
            hdnPayble.Value = double.Parse(lbltotal.Text).ToString();
            lblNextDue.Text = "0";
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox0, "Sorry, Invalid amount!", "A");
            return;
        }
        else if (double.Parse(txtPayble.Text) <0)
        {
            txtFineO.Text = "0";
            txtExemptionO.Text = "0";
            txtPayble.Text = double.Parse(lbltotal.Text).ToString();
            hdnPayble.Value = double.Parse(lbltotal.Text).ToString();
            lblNextDue.Text = "0";
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox0, "Sorry, Invalid amount!", "A");
            return;
        }
        else
        {
            txtFineO.Text = "0";
            txtExemptionO.Text = "0";
            txtPayble.Text = double.Parse(lbltotal.Text).ToString();
            hdnPayble.Value = double.Parse(lbltotal.Text).ToString();
            lblNextDue.Text = "0";
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox0, "Sorry, Invalid amount!", "A");
            return;
        }
    }
    public string Invoice_no_generate()
    {
        string Invoice_no = "";
        sql = "Select count(*) as Counter from HostelFeeDeposit";
        if (oo.ReturnTag(sql, "Counter") == "0")
        {
            Invoice_no = Invoice_no + 1;
        }
        else
        {
            sql = "Select Top 1 ReceiptNo From HostelFeeDeposit Order By Id Desc";
            string[] words = oo.ReturnTag(sql, "ReceiptNo").Split('/');
            Double Value = Convert.ToDouble(words[2].ToString()) + 1;
            Invoice_no = Invoice_no + Value.ToString(CultureInfo.InvariantCulture);
        }
        return "HF/" + Session["SessionName"].ToString() + "/00000" + Invoice_no;
    }
    
    protected void btnPayStack_Click(object sender, EventArgs e)
    {
        Label lblRoomAllotmentId = (Label)GridView1.Rows[0].FindControl("lblRoomAllotmentId");
        Label lblModeO = (Label)GridView1.Rows[0].FindControl("txtMode");
        
        string SrNoOrEmpid = "";
        if (rdoType.SelectedValue == "Student")
        {
            SrNoOrEmpid= hfStudentId.Value;
        }
        else
        {
            SrNoOrEmpid= hfStaffId.Value;
        }
        string HostelReceiptNo = Invoice_no_generate();
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "HostelFeeDepositProc";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Connection = con;
        cmd.Parameters.AddWithValue("@RoomAllotmentId", lblRoomAllotmentId.Text);
        cmd.Parameters.AddWithValue("@SrNoOrEmpid", SrNoOrEmpid);
        cmd.Parameters.AddWithValue("@ReceiptNo", HostelReceiptNo);
        cmd.Parameters.AddWithValue("@MonthName", lblModeO.Text);
        cmd.Parameters.AddWithValue("@FrequencyType", lblModeO.Text);
        cmd.Parameters.AddWithValue("@Amount", lbltotal.Text);
        cmd.Parameters.AddWithValue("@Fine", txtFineO.Text.Trim());
        cmd.Parameters.AddWithValue("@Exemption", txtExemptionO.Text.Trim());
        cmd.Parameters.AddWithValue("@Total", hdnPayble.Value);
        cmd.Parameters.AddWithValue("@Paid", txtPayble.Text);
        cmd.Parameters.AddWithValue("@NextDue", lblNextDue.Text);
        cmd.Parameters.AddWithValue("@DepositDate", (DDDate.SelectedValue+" "+ DDMonth.SelectedValue + " " + DDYear.SelectedValue).ToString());
        cmd.Parameters.AddWithValue("@PaymentMode", DropDownMOD.SelectedValue);
        if (DropDownMOD.SelectedValue == "Cash")
        {
            cmd.Parameters.AddWithValue("@Status", "Paid");
        }
        else if (DropDownMOD.SelectedValue == "Online")
        {
            cmd.Parameters.AddWithValue("@Status", "Paid");
        }
        else
        {
            cmd.Parameters.AddWithValue("@Status", drpStatus.SelectedValue);
            cmd.Parameters.AddWithValue("@ChequeDate", (DDChkDate.SelectedValue + " " + DDChkMonth.SelectedValue + " " + DDChkYear.SelectedValue).ToString());
            cmd.Parameters.AddWithValue("@ChequeNo", txtChequeNo.Text.Trim());
            cmd.Parameters.AddWithValue("@BankName", txtbankName.Text.Trim());
        }
        cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"].ToString());
        cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
        cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());
        cmd.Parameters.AddWithValue("@action", "Insert");

        try
        {
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            OntimeGridBind(SrNoOrEmpid, lblRoomAllotmentId.Text);
            
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox0, "Submitted successfully.", "S");
        }
        catch (SqlException ex) { }
        Session["HostelReceiptNo"] = HostelReceiptNo;
        Response.Redirect("HostelFeeReciept.aspx?print=1");
    }
    
    protected void MonthlyGridBind()
    {

    }


    protected void btnView_Click(object sender, EventArgs e)
    {
        LinkButton link = (LinkButton)sender;
        LinkButton HostelReceiptNo = (LinkButton)link.NamingContainer.FindControl("btnView");
        Session["HostelReceiptNo"] = HostelReceiptNo.Text;
        Response.Redirect("HostelFeeReciept_duplicate.aspx?print=1");
    }
}