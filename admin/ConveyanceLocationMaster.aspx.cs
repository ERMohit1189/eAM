using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System;

public partial class admin_BusLocationMaster : Page
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
        if (!IsPostBack)
        {


            try
            {
                CheckValueADDDeleteUpdate();
            }
            catch (Exception) { }


            sql = "Select Distinct VehicleType from VehicleMaster";
            sql = sql + " where  SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
            oo.FillDropDown(sql, DrpVehicleType, "VehicleType");

            sql = "Select Distinct VehicleType from VehicleMaster";
            sql = sql + " where  SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
            oo.FillDropDown(sql, DrpVehicleType0, "VehicleType");

            sql = "Select  ROW_NUMBER() OVER (ORDER BY id ASC) AS SrNo,id,VehicleType,BusLocationName,VehicleNo,OneWayFare,TwowayFare,Remark from BusLocationMaster ";
            sql = sql + " where VehicleType='" + DrpVehicleType.SelectedItem.ToString() + "' and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
            GridView1.DataSource = oo.GridFill(sql);
            GridView1.DataBind();

        }

       
    }
protected void LinkButton1_Click(object sender, EventArgs e)
    {
       sql = "select VehicleType,BusLocationName from BusLocationMaster where BusLocationName='" + txtlocationName.Text + "' and VehicleType='" + DrpVehicleType.SelectedItem.ToString() + "'";
       sql = sql + " and VehicleNo='" + drpvehicleno.SelectedItem.ToString() + "' and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
      
       if (oo.Duplicate(sql))
       {
            oo.MessageBox("Duplicate Entry!", this.Page);
       }
       else if (DrpVehicleType.SelectedItem.ToString() == "<--Select-->")
       {
           oo.MessageBox("Please Select Condition!", this.Page);
       }
       else if (drpvehicleno.SelectedItem.ToString() == "<--Select-->"||drpvehicleno.SelectedItem.ToString() =="")
       {
           oo.MessageBox("Please Select Condition!", this.Page);
       }
        
        else
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "BusLocationMasterProce";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@BusLocationName", txtlocationName.Text.ToString());
            cmd.Parameters.AddWithValue("@OneWayFare", txtonewayfare.Text.ToString());
            cmd.Parameters.AddWithValue("@TwowayFare", txttwowayfare.Text.ToString());
            cmd.Parameters.AddWithValue("@Remark", txtremark.Text.ToString());
            cmd.Parameters.AddWithValue("@VehicleType", DrpVehicleType.SelectedItem.ToString());
            cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());
            cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
            cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"]);
            cmd.Parameters.AddWithValue("@Vechile", drpvehicleno.SelectedItem.ToString());
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                oo.MessageBox("Submitted successfully.", this.Page);
                oo.ClearControls(this.Page);
                sql = "Select  ROW_NUMBER() OVER (ORDER BY id ASC) AS SrNo,id,VehicleType, BusLocationName,OneWayFare,VehicleNo,TwowayFare,Remark from BusLocationMaster ";
                sql = sql + " where  SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
                GridView1.DataSource = oo.GridFill(sql);
                GridView1.DataBind();

            }
            catch (Exception) { }
        }
       try
       {

           GridView1.FooterRow.Visible = false;
       }
       catch (Exception) { }
    }
protected void LinkButton4_Click(object sender, EventArgs e)
    {
        sql = "select Vehicletype,BusLocationName from BusLocationMaster where BusLocationName='" + txtlocationName0.Text + "' and Vehicletype='" + DrpVehicleType0.SelectedItem.ToString() + "' and VehicleNo='" + drpvehicleno0.SelectedItem.ToString() + "' and Id<>'" + lblID.Text + "'";
        sql = sql + " and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
        if (oo.Duplicate(sql))
        {
            oo.MessageBox("Duplicate Entry!", this.Page);
        }
        else if (DrpVehicleType0.SelectedItem.ToString() == "<--Select-->")
        {
            oo.MessageBox("Please Select Condition!", this.Page);
        }
        else if (drpvehicleno0.SelectedItem.ToString() == "<--Select-->" || drpvehicleno0.SelectedItem.ToString() == "")
       {
           oo.MessageBox("Please Select Condition!", this.Page);
       }
     else
     {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "BusLocationMasterUpdateProce";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@id", lblID.Text);
            cmd.Parameters.AddWithValue("@BusLocationName", txtlocationName0.Text.ToString());
            cmd.Parameters.AddWithValue("@OneWayFare", txtonewayfare0.Text.ToString());
            cmd.Parameters.AddWithValue("@TwowayFare", txttwowayfare0.Text.ToString());
            cmd.Parameters.AddWithValue("@Remark", txtremark0.Text.ToString());
            cmd.Parameters.AddWithValue("@VehicleType", DrpVehicleType0.SelectedItem.ToString());
            cmd.Parameters.AddWithValue("@Vehicle", drpvehicleno0.SelectedItem.ToString());
            cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());//Session["Login"].ToString());
            cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
            cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"]);
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                oo.MessageBox("Updated successfully.", this.Page);
                oo.ClearControls(this.Page);
                sql = "Select  ROW_NUMBER() OVER (ORDER BY id ASC) AS SrNo,id,VehicleType,VehicleNo, BusLocationName,OneWayFare,TwowayFare,Remark from BusLocationMaster ";
                sql = sql + " where  SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
                GridView1.DataSource = oo.GridFill(sql);
                GridView1.DataBind();

            }
            catch (Exception) { }
     }
    }   
protected void btnDelete_Click(object sender, EventArgs e)
    {
        sql = "Delete from BusLocationMaster where id=" + lblvalue.Text;

        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = sql;
        cmd.CommandType = CommandType.Text;
        cmd.Connection = con;
        try
        {
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            oo.MessageBox("Deleted successfully.", this.Page);
            sql = "Select  ROW_NUMBER() OVER (ORDER BY id ASC) AS SrNo,id,VehicleType, BusLocationName,VehicleNo,OneWayFare,TwowayFare,Remark from BusLocationMaster ";
            sql = sql + " where  SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
            GridView1.DataSource = oo.GridFill(sql);
            GridView1.DataBind();
        }
        catch (SqlException) { }
    }   
protected void LinkButton6_Click(object sender, EventArgs e)
{
        LinkButton chk = (LinkButton)sender;
        string ss = chk.Text;
        lblID.Text = ss;
        sql = "Select VehicleNo from VehicleDetails where VehicleType='" + DrpVehicleType.SelectedItem.ToString() + "'";
        sql = sql + " and  SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
        oo.FillDropDown(sql, drpvehicleno0, "VehicleNo");
        sql = "Select  ROW_NUMBER() OVER (ORDER BY id ASC) AS SrNo,id,VehicleType,VehicleNo, BusLocationName,OneWayFare,TwowayFare,Remark from BusLocationMaster ";
        sql = sql + " where SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + " and Id=" + ss;
        DrpVehicleType0.Text = oo.ReturnTag(sql, "VehicleType");
        Session["VehicleName"] = oo.ReturnTag(sql, "VehicleType");
        txtlocationName0.Text = oo.ReturnTag(sql, "BusLocationName");
        txtonewayfare0.Text = oo.ReturnTag(sql, "OneWayFare");
        txttwowayfare0.Text = oo.ReturnTag(sql, "TwowayFare");
        txtremark0.Text = oo.ReturnTag(sql, "Remark");
        try
        {
            drpvehicleno0.Text = oo.ReturnTag(sql, "VehicleNo");
        }
        catch
        {
        }
        Panel1_ModalPopupExtender.Show();
}
protected void LinkButton7_Click(object sender, EventArgs e)
{
    LinkButton chk = (LinkButton)sender;
    string ss = chk.Text;
    lblvalue.Text = ss.ToString();
    Panel2_ModalPopupExtender.Show();
}
protected void DrpVehicleType_SelectedIndexChanged(object sender, EventArgs e)
{
    sql = "Select VehicleNo from VehicleDetails where VehicleType='" + DrpVehicleType.Text + "' and SessionName='" + Session["SessionName"].ToString() + "'";
    oo.FillDropDown(sql, drpvehicleno, "VehicleNo");
    sql = "Select  ROW_NUMBER() OVER (ORDER BY id ASC) AS SrNo,id,VehicleType,BusLocationName,VehicleNo,OneWayFare,TwowayFare,Remark from BusLocationMaster where VehicleType='"+DrpVehicleType.SelectedItem.ToString()+"'";
    sql = sql + " and  SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
    GridView1.DataSource = oo.GridFill(sql);
    GridView1.DataBind();
    try
    {
        GridView1.FooterRow.Visible = false;
    }
    catch (Exception) { }
}
public void PermissionGrant(int add1, int delete1, int update1, LinkButton Ladd, Button Ldelete, LinkButton LUpdate)
{


    if (add1 == 1)
    {
        Ladd.Enabled = true;
    }
    else
    {
        Ladd.Enabled = false;
    }


    if (delete1 == 1)
    {
        Ldelete.Enabled = true;
    }
    else
    {
        Ldelete.Enabled = false;
    }

    if (update1 == 1)
    {
        LUpdate.Enabled = true;
    }
    else
    {
        LUpdate.Enabled = false;
    }


}
public void CheckValueADDDeleteUpdate()
{
    sql = " select LoginId,LoginName,Pass,SessionId,BranchId,LT.LoginTypeName,ltb.add1 as add1,ltb.delete1 as delete1,ltb.update1 as update1 from LoginTab LTb";
    sql = sql + " left join LoginType Lt on LTb.LoginTypeId=Lt.LoginTypeId where LT.LoginTypeName='" + Session["Logintype"] + "' and LTb.LoginName='" + Session["LoginName"] + "'";
    int a, u, d;
    a = Convert.ToInt32(oo.ReturnTag(sql, "add1"));
    u = Convert.ToInt32(oo.ReturnTag(sql, "update1"));
    d = Convert.ToInt32(oo.ReturnTag(sql, "delete1"));

    PermissionGrant(a, d, u, (LinkButton)LinkButton1, btnDelete, LinkButton4);
}
protected void DrpVehicleType0_SelectedIndexChanged(object sender, EventArgs e)
{
    sql = "Select VehicleNo from VehicleDetails where VehicleType='" + DrpVehicleType0.SelectedItem.ToString() + "' and SessionName='" + Session["SessionName"].ToString() + "'";
    oo.FillDropDown(sql, drpvehicleno0, "VehicleNo");
    Panel1_ModalPopupExtender.Show();
}
}