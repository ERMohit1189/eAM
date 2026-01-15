using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

public partial class room_master : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection();
    string sql = "";
    Campus oo = new Campus();
    protected void Page_Load(object sender, EventArgs e)
    {
        con = oo.dbGet_connection();
        Campus camp = new Campus(); camp.LoadLoader(loader);

        if (!IsPostBack)
        {
            sql = "Select Id, CategoryName from HostelCategoryMaster where BranchCode=" + Session["BranchCode"].ToString() + "";
            oo.FillDropDown_withValue(sql, DrpCategory, "CategoryName", "Id");
            oo.FillDropDown_withValue(sql, DrpCategory0, "CategoryName", "Id");

            sql = "Select Id, RoomType from HostelRoomTypeMaster where BranchCode=" + Session["BranchCode"].ToString() + "";
            oo.FillDropDown_withValue(sql, DrpRoomType, "RoomType", "Id");
            oo.FillDropDown_withValue(sql, DrpRoomType0, "RoomType", "Id");

            sql = "Select Id, BuildingLocation from HostelLocationMaster where BranchCode=" + Session["BranchCode"].ToString() + "";
            oo.FillDropDown_withValue(sql, DrpbuildingLocation, "BuildingLocation", "Id");
            oo.FillDropDown_withValue(sql, DrpbuildingLocation0, "BuildingLocation", "Id");

            Displaygrid();
        }
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        bool sts = false;
        if (DrpRoomnoType.SelectedValue == "Alphanumeric")
        {
            sql = "Select RoomNo from RoomMaster where RoomNo='" + txtroomnoAlpha.Text.ToString().Trim() + "' and BranchCode=" + Session["BranchCode"].ToString() + " and CategoryId=" + DrpCategory.SelectedValue + " and BuildingLocationId=" + DrpbuildingLocation.SelectedValue + "";
            sts = oo.Duplicate(sql);
        }
        else
        {
            sql = "Select RoomNo from RoomMaster where RoomNo='" + txtroomnoNum.Text.ToString().Trim() + "' and BranchCode=" + Session["BranchCode"].ToString() + "  and CategoryId=" + DrpCategory.SelectedValue + " and BuildingLocationId=" + DrpbuildingLocation.SelectedValue + "";
            sts = oo.Duplicate(sql);
        }

        if (sts)
        {
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Room No. already exists!", "A");
            return;
        }
        else
        {
            if (DrpRoomnoType.SelectedValue == "Alphanumeric")
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "RoomMasterProc";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@CategoryId", DrpCategory.SelectedValue);
                cmd.Parameters.AddWithValue("@RoomTypeId", DrpRoomType.SelectedValue);
                cmd.Parameters.AddWithValue("@BuildingLocationId", DrpbuildingLocation.SelectedValue);
                cmd.Parameters.AddWithValue("@RoomNoType", DrpRoomnoType.SelectedValue);
                cmd.Parameters.AddWithValue("@RoomNo", txtroomnoAlpha.Text.ToString().Trim());
                cmd.Parameters.AddWithValue("@Remark", txtremark.Text.ToString().Trim());
                cmd.Parameters.AddWithValue("@Status", DrpRoomStatus.Text.ToString());
                cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"].ToString());
                cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
                cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());
                cmd.Parameters.AddWithValue("@action", "Insert");

                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Submitted successfully.", "S");
                    Displaygrid();
                }
                catch (SqlException ex) { }
            }
            else
            {
                int length = 0; int start = 0;
                sql = "select count(id) counts from RoomMaster where RoomNoType='" + DrpRoomnoType.SelectedValue + "' and BranchCode=" + Session["BranchCode"].ToString() + " and CategoryId=" + DrpCategory.SelectedValue + " and BuildingLocationId=" + DrpbuildingLocation.SelectedValue + "";
                int counts = int.Parse(oo.ReturnTag(sql, "counts"));
                if (counts > 0)
                {
                    start = counts+1;
                    length = int.Parse(txtroomnoNum.Text.ToString().Trim()) + counts;
                }
                else
                {
                    start = 1;
                    length = int.Parse(txtroomnoNum.Text.ToString().Trim());
                }
                for (int i = start; i < length + 1; i++)
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = "RoomMasterProc";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@CategoryId", DrpCategory.SelectedValue);
                    cmd.Parameters.AddWithValue("@RoomTypeId", DrpRoomType.SelectedValue);
                    cmd.Parameters.AddWithValue("@BuildingLocationId", DrpbuildingLocation.SelectedValue);
                    cmd.Parameters.AddWithValue("@RoomNoType", DrpRoomnoType.SelectedValue);
                    cmd.Parameters.AddWithValue("@RoomNo",i.ToString());
                    cmd.Parameters.AddWithValue("@Remark", txtremark.Text.ToString().Trim());
                    cmd.Parameters.AddWithValue("@Status", DrpRoomStatus.Text.ToString());
                    cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"].ToString());
                    cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
                    cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());
                    cmd.Parameters.AddWithValue("@action", "Insert");

                    try
                    {
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                    catch (SqlException ex) { }
                }
                Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Submitted successfully.", "S");
                Displaygrid();
            }
        }
    }
    public void Displaygrid()
    {
        sql = "Select  Row_Number() over (order by rm.Id Asc) as SrNo,rm.Id,CategoryId, hcm.CategoryName,RoomTypeId, ";
        sql = sql + "hrtm.RoomType,BuildingLocationId, hl.BuildingLocation, RoomNoType,RoomNo,rm.Remark,rm.SessionName, ";
        sql = sql + "rm.BranchCode,rm.LoginName,case when rm.Status=0 then 'Inactive' else 'Active' end as Status  from RoomMaster rm ";
        sql = sql + "inner join HostelCategoryMaster hcm on rm.CategoryId = hcm.Id ";
        sql = sql + "inner join  HostelLocationMaster hl on rm.BuildingLocationId = hl.Id ";
        sql = sql + "inner join HostelRoomTypeMaster hrtm on rm.RoomTypeId = hrtm.Id where hrtm.BranchCode=" + Session["BranchCode"].ToString() + " and hl.BranchCode=" + Session["BranchCode"].ToString() + " and hcm.BranchCode=" + Session["BranchCode"].ToString() + " and rm.BranchCode=" + Session["BranchCode"].ToString() + "";
        GridView1.DataSource = oo.GridFill(sql);
        GridView1.DataBind();
    }


    protected void LinkButton4_Click(object sender, EventArgs e)
    {

        bool Duplicate = false;
        if (DrpRoomnoType0.SelectedValue == "Alphanumeric")
        {
            sql = "Select RoomNo from RoomMaster where RoomNo='" + txtroomnoAlpha0.Text.ToString().Trim() + "' and CategoryId=" + DrpCategory0.SelectedValue + "";
            sql = sql + "and RoomTypeId=" + DrpRoomType0.SelectedValue + "  and BranchCode=" + Session["BranchCode"].ToString() + " and BuildingLocationId=" + DrpbuildingLocation0.SelectedValue + "  and RoomNoType='" + DrpRoomnoType0.SelectedValue + "' ";
            Duplicate = oo.Duplicate(sql);
        }
        if (checkRoomName.Value != txtroomnoAlpha0.Text.ToString().Trim() && Duplicate && DrpRoomnoType0.SelectedValue == "Alphanumeric")
        {
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "This room no already exists.", "A");
        }
        else
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "RoomMasterProc";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@Id", lblid.Text.Trim());
            cmd.Parameters.AddWithValue("@CategoryId", DrpCategory0.SelectedValue);
            cmd.Parameters.AddWithValue("@RoomTypeId", DrpRoomType0.SelectedValue);
            cmd.Parameters.AddWithValue("@BuildingLocationId", DrpbuildingLocation0.SelectedValue);
            cmd.Parameters.AddWithValue("@RoomNoType", DrpRoomnoType0.SelectedValue);
            if (DrpRoomnoType0.SelectedValue == "Alphanumeric")
            {
                cmd.Parameters.AddWithValue("@RoomNo", txtroomnoAlpha0.Text.ToString().Trim());
            }
            else
            {
                cmd.Parameters.AddWithValue("@RoomNo", txtroomnoNum0.Text.ToString().Trim());
            }
            cmd.Parameters.AddWithValue("@Remark", txtremark0.Text.ToString().Trim());
            cmd.Parameters.AddWithValue("@Status", DrpRoomStatus0.Text.ToString());
            cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"].ToString());
            cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
            cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());
            cmd.Parameters.AddWithValue("@action", "Update");
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Updated successfully.", "S");
                Displaygrid();
            }
            catch (SqlException ex) { }
        }
    }
    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        checkRoomName.Value = "";

         LinkButton chk = (LinkButton)sender;
        Label lblId = (Label)chk.NamingContainer.FindControl("Label36");
        string ss = lblId.Text;
        lblid.Text = ss;

        sql = "Select  Row_Number() over (order by rm.Id Asc) as SrNo,rm.Id,CategoryId, hcm.CategoryName,RoomTypeId, ";
        sql = sql + "hrtm.RoomType,BuildingLocationId, hl.BuildingLocation, RoomNoType,RoomNo,rm.Remark,rm.SessionName, ";
        sql = sql + "rm.BranchCode,rm.LoginName, rm.Status  from RoomMaster rm ";
        sql = sql + "inner join HostelCategoryMaster hcm on rm.CategoryId = hcm.Id ";
        sql = sql + "inner join  HostelLocationMaster hl on rm.BuildingLocationId = hl.Id ";
        sql = sql + "inner join HostelRoomTypeMaster hrtm on rm.RoomTypeId = hrtm.Id";
        sql = sql + " where rm.Id=" + ss+ " and hl.BranchCode=" + Session["BranchCode"].ToString() + " and hrtm.BranchCode=" + Session["BranchCode"].ToString() + " and hcm.BranchCode=" + Session["BranchCode"].ToString() + " and rm.BranchCode=" + Session["BranchCode"].ToString() + "";
        DrpCategory0.SelectedValue = oo.ReturnTag(sql, "CategoryId");
        DrpRoomType0.SelectedValue = oo.ReturnTag(sql, "RoomTypeId").Trim();
        DrpbuildingLocation0.SelectedValue = oo.ReturnTag(sql, "BuildingLocationId");
        DrpRoomnoType0.SelectedValue = oo.ReturnTag(sql, "RoomNoType");
        
        if (oo.ReturnTag(sql, "RoomNoType") == "Alphanumeric")
        {
            txtroomnoAlpha0.Text = oo.ReturnTag(sql, "RoomNo").Trim();
            txtroomnoAlpha0.Visible = true;
            txtroomnoNum0.Visible = false;
            checkRoomName.Value = oo.ReturnTag(sql, "RoomNo").Trim();

            DrpCategory0.Enabled = false;
            DrpRoomType0.Enabled = false;
            DrpbuildingLocation0.Enabled = false;
            DrpRoomnoType0.Enabled = false;
            txtroomnoNum0.Enabled = true;
        }
        else
        {
            txtroomnoNum0.Text = oo.ReturnTag(sql, "RoomNo").Trim();
            txtroomnoAlpha0.Visible = false;
            txtroomnoNum0.Visible = true;
            
            checkRoomName.Value = oo.ReturnTag(sql, "RoomNo").Trim();
            DrpCategory0.Enabled = false;
            DrpRoomType0.Enabled = false;
            DrpbuildingLocation0.Enabled = false;
            DrpRoomnoType0.Enabled = false;
            txtroomnoNum0.Enabled = false;
        }
        DrpRoomStatus0.SelectedValue = (oo.ReturnTag(sql, "Status").Trim().ToLower()=="false"?"0":"1");
        txtremark0.Text = oo.ReturnTag(sql, "Remark").Trim();
        
        Panel1_ModalPopupExtender.Show();
    }
    protected void LinkButton3_Click(object sender, EventArgs e)
    {
        hdnBuildingName.Value = "";
        Button8.Focus();
        LinkButton chk = (LinkButton)sender;
        Label Label37 = (Label)chk.NamingContainer.FindControl("Label37");
        Label BuildingLocation = (Label)chk.NamingContainer.FindControl("Label5");
        string ss = Label37.Text;
        lblvalue.Text = ss.ToString();

        hdnBuildingName.Value = BuildingLocation.Text;
        Panel2_ModalPopupExtender.Show();
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        sql = "select count(id) as cont from HostelBedMaster where roomId=" + lblvalue.Text+ " and BranchCode=" + Session["BranchCode"].ToString() + "";
        if (int.Parse(oo.ReturnTag(sql, "cont"))>0)
        {
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Firstly delete all beds from this room!", "A");
            return;
        }

        sql = "select RoomNo, RoomNoType, CategoryId, BuildingLocationId from RoomMaster where id=" + lblvalue.Text+ " and BranchCode=" + Session["BranchCode"].ToString() + "";
        string RoomNo = oo.ReturnTag(sql, "RoomNo");
        string RoomNoType = oo.ReturnTag(sql, "RoomNoType");
        string CategoryId = oo.ReturnTag(sql, "CategoryId");
        string BuildingLocationId = oo.ReturnTag(sql, "BuildingLocationId");

        sql = "select top(1) RoomNo from RoomMaster where RoomNoType='"+ RoomNoType + "' and BranchCode=" + Session["BranchCode"].ToString() + " and CategoryId=" + CategoryId + " and BuildingLocationId="+ BuildingLocationId + " order by id desc";
        string RoomNo2 = oo.ReturnTag(sql, "RoomNo");
        if (RoomNoType == "Alphanumeric")
        {
            sql = "Delete from RoomMaster where Id=" + lblvalue.Text+ " and BranchCode=" + Session["BranchCode"].ToString() + "";

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = sql;
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                //oo.MessageBox("Successfully Deleted", this.Page);
                Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Deleted successfully.", "S");
                Displaygrid();
            }
            catch (SqlException) { }
        }
        if (RoomNoType == "Numeric")
        {
            if (RoomNo == RoomNo2)
            {
                sql = "Delete from RoomMaster where Id=" + lblvalue.Text+ " and BranchCode=" + Session["BranchCode"].ToString() + "";

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = sql;
                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;
                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    //oo.MessageBox("Successfully Deleted", this.Page);
                    Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Deleted successfully.", "S");
                    Displaygrid();
                }
                catch (SqlException) { }
            }
            else
            {
                Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Firstly delete last room no ("+ RoomNo2 + ") of " + hdnBuildingName.Value + "!", "A");
            }
        }
    }
    protected void Button8_Click(object sender, EventArgs e)
    {

    }


    protected void DrpRoomnoType_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtroomnoAlpha.Text = "";
        txtroomnoNum.Text = "";
        if (DrpRoomnoType.SelectedValue == "Alphanumeric")
        {
            txtroomnoAlpha.Visible = true;
            txtroomnoNum.Visible = false;
            lblRoomNoType.InnerHtml="Room No. <span class='vd_red'>*</span>";
        }
        else
        {
            txtroomnoAlpha.Visible = false;
            txtroomnoNum.Visible = true;
            lblRoomNoType.InnerHtml = "No. of Rooms <span class='vd_red'>*</span>";
        }
    }
    
}