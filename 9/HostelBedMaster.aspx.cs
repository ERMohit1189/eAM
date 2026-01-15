using System.Data;
using System.Data.SqlClient;
using System;
using System.Web.UI.WebControls;

public partial class HostelBedMaster : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection();
    Campus oo = new Campus();
    string sql = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        con = oo.dbGet_connection();
        Campus camp = new Campus(); camp.LoadLoader(loader); 

        if (Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
        {
            Response.Redirect("default.aspx");
        }
    }
    
    protected void txtSearch_TextChanged(object sender, EventArgs e)
    {
        divNoOfRoom.Visible = false;
        txtNoofBeds.Text = "";
        var RoomId = Request.Form[hfRoomId.UniqueID];
        if (RoomId != "")
        {
            divNoOfRoom.Visible = true;
            bindStopageGrid();
        }
    }
    protected void txtNoofBeds_TextChanged(object sender, EventArgs e)
    {
        bool flag;
        int stopage;
        flag = int.TryParse(txtNoofBeds.Text.Trim(), out stopage);
        if (flag)
        {
            bindStopageGrid();
        }
    }
    public void bindStopageGrid()
    {
        GridView2.DataSource = null;
        GridView2.DataBind();
        DataSet ds = new DataSet();
        sql = "select hbm.id, BedNo, BedCharge, PaymentType, hbm.remark, (case when hbm.Status=1 then 'Active' else 'Damaged' end) bedStatus, (case when BookedStatus=1 then 'Occupied' else 'Vacant' end) BookedStatus from HostelBedMaster hbm inner join RoomMaster rm on rm.Id = hbm.roomId where hbm.roomId=" + hfRoomId.Value+ " and rm.BranchCode=" + Session["BranchCode"].ToString() + "  and hbm.BranchCode=" + Session["BranchCode"].ToString() + "";
        ds = oo.GridFill(sql);

        if (ds == null || ds.Tables[0].Rows.Count == 0)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("BedNo");
            dt.Columns.Add("BedCharge");
            dt.Columns.Add("PaymentType");
            dt.Columns.Add("remark");
            bool flag;
            int stopage = 0;
            flag = int.TryParse(txtNoofBeds.Text.Trim(), out stopage);
            if (flag)
            {
                for (int i = 1; i < stopage + 1; i++)
                {
                    DataRow dr = dt.NewRow();
                    dr["BedNo"] = (i).ToString();
                    dr["BedCharge"] = "";
                    dr["PaymentType"] = "";
                    dr["remark"] = "";
                    dt.Rows.Add(dr);
                }
                BtnSubmit.Visible = true;
            }
            GridView1.Visible = true;
            GridView2.Visible = false;
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }
        else
        {
            GridView1.Visible = false;
            GridView2.Visible = true;
            divNoOfRoom.Visible = false;
            BtnSubmit.Visible = false;
            GridView2.DataSource = ds;
            GridView2.DataBind();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                DropDownList drpPaymentType = (DropDownList)GridView2.Rows[i].FindControl("drpPaymentType");
                drpPaymentType.SelectedValue = ds.Tables[0].Rows[i]["PaymentType"].ToString();
            }
        }
        //for (int i = 0; i < GridView2.Rows.Count; i++)
        //{
        //    LinkButton LinkEdit = (LinkButton)GridView2.Rows[i].FindControl("LinkButton2");
        //    LinkButton btnDelete = (LinkButton)GridView2.Rows[i].FindControl("LinkButton3");
        //    Label lblBookedStatus = (Label)GridView2.Rows[i].FindControl("lblBookedStatus");
        //    if (lblBookedStatus.Text== "Booked")
        //    {
        //        btnDelete.Visible = false;
        //        LinkEdit.Visible = false;
        //    }
        //    else
        //    {
        //        btnDelete.Enabled = true;
        //        LinkEdit.Enabled = true;
        //    }
        //}
    }
    protected void BtnSubmit_Click(object sender, EventArgs e)
    {
        if (hfRoomId.Value == "")
        {
            Campus camp1 = new Campus(); camp1.msgbox(this.Page, msgbox3, "Sorry, Select Room No!", "A");
            return;
        }
        for (int i = 0; i < GridView1.Rows.Count; i++)
        {
            TextBox txtBedNo = (TextBox)GridView1.Rows[i].FindControl("txtBedNo");
            TextBox txtPrice = (TextBox)GridView1.Rows[i].FindControl("txtPrice");
            TextBox txtremark = (TextBox)GridView1.Rows[i].FindControl("txtremark");
            DropDownList drpPaymentType = (DropDownList)GridView1.Rows[i].FindControl("drpPaymentType");

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "HostelBedMasterProc";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@roomId", hfRoomId.Value.Trim());
            cmd.Parameters.AddWithValue("@BedNo", txtBedNo.Text.Trim().ToString());
            cmd.Parameters.AddWithValue("@BedCharge", txtPrice.Text.Trim().ToString());
            cmd.Parameters.AddWithValue("@PaymentType", drpPaymentType.SelectedValue.ToString());
            cmd.Parameters.AddWithValue("@remark", txtremark.Text.Trim().ToString());
            cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());
            cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
            cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"]);
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex) { }
        }
        Campus camp = new Campus(); camp.msgbox(this.Page, msgbox3, "Submitted successfully", "S");
        GridView1.Visible = false;
        GridView2.Visible = true;
        divNoOfRoom.Visible = false;
        BtnSubmit.Visible = false;
        sql = "select hbm.id, BedNo, BedCharge, PaymentType, hbm.remark, (case when hbm.Status=1 then 'Active' else 'Damaged' end) bedStatus, (case when BookedStatus=1 then 'Occupied' else 'Vacant' end) BookedStatus from HostelBedMaster hbm inner join RoomMaster rm on rm.Id = hbm.roomId where hbm.roomId=" + hfRoomId.Value + " and rm.BranchCode=" + Session["BranchCode"].ToString() + " and hbm.BranchCode=" + Session["BranchCode"].ToString() + "";
        GridView2.DataSource = oo.GridFill(sql);
        GridView2.DataBind();
        var ds = oo.GridFill(sql);
        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            DropDownList drpPaymentType = (DropDownList)GridView2.Rows[i].FindControl("drpPaymentType");
            drpPaymentType.SelectedValue = ds.Tables[0].Rows[i]["PaymentType"].ToString();
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (hfRoomId.Value == "")
        {
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox3, "Sorry, Select Room No!", "A");
            return;
        }
        TextBox txtBedNo = (TextBox)GridView2.FooterRow.FindControl("txtBedNo");
        TextBox txtPrice = (TextBox)GridView2.FooterRow.FindControl("txtBedCharge");
        DropDownList drpPaymentTypeFooter = (DropDownList)GridView2.FooterRow.FindControl("drpPaymentTypeFooter");
        TextBox txtremark = (TextBox)GridView2.FooterRow.FindControl("txtremark");
        sql = "select * from HostelBedMaster where roomId=" + hfRoomId.Value + " and BedNo='" + txtBedNo.Text.Trim() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
        if (oo.Duplicate(sql))
        {
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox3, "Sorry, Bed No " + txtBedNo.Text.Trim() + " already added in this room!", "A");
        }
        else
        {

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "HostelBedMasterProc";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@roomId", hfRoomId.Value.Trim());
            cmd.Parameters.AddWithValue("@BedNo", txtBedNo.Text.Trim().ToString());
            cmd.Parameters.AddWithValue("@BedCharge", txtPrice.Text.Trim().ToString());
            cmd.Parameters.AddWithValue("@PaymentType", drpPaymentTypeFooter.SelectedValue.ToString());
            cmd.Parameters.AddWithValue("@remark", txtremark.Text.Trim().ToString());
            cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());
            cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
            cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"]);
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                Campus camp = new Campus(); camp.msgbox(this.Page, msgbox3, "Submitted successfully", "S");
                GridView1.Visible = false;
                GridView2.Visible = true;
                divNoOfRoom.Visible = false;
                sql = "select hbm.id, BedNo, BedCharge, PaymentType, hbm.remark, (case when hbm.Status=1 then 'Active' else 'Damaged' end) bedStatus, (case when BookedStatus=1 then 'Occupied' else 'Vacant' end) BookedStatus from HostelBedMaster hbm inner join RoomMaster rm on rm.Id = hbm.roomId where hbm.roomId=" + hfRoomId.Value + " and rm.BranchCode=" + Session["BranchCode"].ToString() + " and hbm.BranchCode=" + Session["BranchCode"].ToString() + "";
                GridView2.DataSource = oo.GridFill(sql);
                GridView2.DataBind();
                var ds = oo.GridFill(sql);
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    DropDownList drpPaymentType = (DropDownList)GridView2.Rows[i].FindControl("drpPaymentType");
                    drpPaymentType.SelectedValue = ds.Tables[0].Rows[i]["PaymentType"].ToString();
                }
                txtBedNo.Text = "";
                txtPrice.Text = "";
                txtremark.Text = "";
            }
            catch (Exception) { }
        }
    }
    protected void LinkButton2_Click(object sender, EventArgs e)
    {
       
        ddlBedStatus0.Enabled = true;
        ddlBookedStatus0.Enabled = true;
        
        Button8.Focus();
        LinkButton chk = (LinkButton)sender;
        Label lblId = (Label)chk.NamingContainer.FindControl("lblBedIdEdit");
        Label lblbedStatus = (Label)chk.NamingContainer.FindControl("lblbedStatus");
        Label lblBookedStatus = (Label)chk.NamingContainer.FindControl("lblBookedStatus");
        Label lblremark = (Label)chk.NamingContainer.FindControl("lblremark");
        string ss = lblId.Text;
        lblvalueEdit.Text = ss.ToString();

        ddlBedStatus0.SelectedValue = (lblbedStatus.Text== "Active"?"1":"0");
        ddlBookedStatus0.SelectedValue = (lblBookedStatus.Text == "Booked" ? "1" : "0");
        txtremark0.Text = lblremark.Text;
        
        if (lblBookedStatus.Text == "Booked")
        {
            
            ddlBedStatus0.Enabled = false;
            ddlBookedStatus0.Enabled = false;
            Panel3_ModalPopupExtender.Show();
        }
        else
        {
            ddlBedStatus0.Enabled = true;
            ddlBookedStatus0.Enabled = true;
            Panel3_ModalPopupExtender.Show();
        }
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        sql = "update HostelBedMaster set Status=" + ddlBedStatus0.SelectedValue + ", BookedStatus=" + ddlBookedStatus0.SelectedValue + ", remark='" + txtremark0.Text + "' where id=" + lblvalueEdit.Text;
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = sql;
        cmd.CommandType = CommandType.Text;
        cmd.Connection = con;
        try
        {
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox3, "Updated successfully", "S");
            sql = "select hbm.id, BedNo, BedCharge, PaymentType, hbm.remark, (case when hbm.Status=1 then 'Active' else 'Damaged' end) bedStatus, (case when BookedStatus=1 then 'Occupied' else 'Vacant' end) BookedStatus from HostelBedMaster hbm inner join RoomMaster rm on rm.Id = hbm.roomId where hbm.roomId=" + hfRoomId.Value + " and rm.BranchCode=" + Session["BranchCode"].ToString() + " and hbm.BranchCode=" + Session["BranchCode"].ToString() + "";
            GridView2.DataSource = oo.GridFill(sql);
            GridView2.DataBind();
            var ds = oo.GridFill(sql);
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                DropDownList drpPaymentType = (DropDownList)GridView2.Rows[i].FindControl("drpPaymentType");
                drpPaymentType.SelectedValue = ds.Tables[0].Rows[i]["PaymentType"].ToString();
            }
        }
        catch (SqlException) { }
    }
    protected void LinkButton3_Click(object sender, EventArgs e)
    {
        Button8.Focus();
        LinkButton chk = (LinkButton)sender;
        Label lblId = (Label)chk.NamingContainer.FindControl("lblBedId");
        Label lblBookedStatus = (Label)chk.NamingContainer.FindControl("lblBookedStatus");
        if (lblBookedStatus.Text == "Booked")
        {
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox3, "Sorry, This bed is already booked, can not be delete details!", "A");
        }
        else
        {
            string ss = lblId.Text;
            lblvalue.Text = ss.ToString();
            Panel2_ModalPopupExtender.Show();
        }
    }
    
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        sql = "Delete from HostelBedMaster where id=" + lblvalue.Text.Trim();
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = sql;
        cmd.CommandType = CommandType.Text;
        cmd.Connection = con;
        try
        {
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox3, "Deleted successfully", "S");
            sql = "select hbm.id, BedNo, BedCharge, PaymentType, hbm.remark, (case when hbm.Status=1 then 'Active' else 'Damaged' end) bedStatus, (case when BookedStatus=1 then 'Occupied' else 'Vacant' end) BookedStatus from HostelBedMaster hbm inner join RoomMaster rm on rm.Id = hbm.roomId where hbm.roomId=" + hfRoomId.Value + " and rm.BranchCode=" + Session["BranchCode"].ToString() + " and hbm.BranchCode=" + Session["BranchCode"].ToString() + "";
            GridView2.DataSource = oo.GridFill(sql);
            GridView2.DataBind();
            var ds = oo.GridFill(sql);
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                DropDownList drpPaymentType = (DropDownList)GridView2.Rows[i].FindControl("drpPaymentType");
                drpPaymentType.SelectedValue = ds.Tables[0].Rows[i]["PaymentType"].ToString();
            }
        }
        catch (SqlException) { }
    }
    protected void Button8_Click(object sender, EventArgs e)
    {

    }

        protected void Button3_Click(object sender, EventArgs e)
    {

    }
    protected void chkAll_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chkAll = (CheckBox)GridView1.HeaderRow.FindControl("chkAll");
        if (chkAll.Checked)
        {
            TextBox txtPriceMain = (TextBox)GridView1.Rows[0].FindControl("txtPrice");
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                TextBox txtPrice = (TextBox)GridView1.Rows[i].FindControl("txtPrice");
                txtPrice.Text = txtPriceMain.Text;
            }
        }
        else
        {
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                TextBox txtPrice = (TextBox)GridView1.Rows[i].FindControl("txtPrice");
                txtPrice.Text = "";
            }
        }
    }
}
