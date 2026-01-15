using System;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Web.Services;


public partial class LocationMapping : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection();
    SqlCommand cmd;
    Campus oo = new Campus();
    string sql = "";
    public static string BranchCode = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((string)Session["LoginName"] == "" || (string)Session["BranchCode"] == "" || (string)Session["SessionName"] == "" || Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
        {
            Response.Redirect("default.aspx");
        }
        con = oo.dbGet_connection();
        if (!IsPostBack)
        {
            GetVehicle();
        }
        
    }
    protected void GetVehicle()
    {
        sql = "SELECT Id, VehicleNo+ ' ( '+ VehicleMade+' '+FuelType+' )' VehicleNo FROM VehicleDetails where BranchCode=" + Session["BranchCode"].ToString() + " ORDER by VehicleNo";
        oo.FillDropDown_withValue(sql, ddlVehicleH, "VehicleNo", "Id");
        ddlVehicleH.Items.Insert(0, new ListItem("<--Select-->", "0"));
    }
    protected void LoadGrid()
    {
        string sqls = "SELECT lmp.id, lm.locationId,vm.id vechicleId, VehicleNo+ ' ( '+ VehicleMade+' '+FuelType+' )' VehicleNo, locationName+' ('+distanceInKm+')' locationName FROM locationMapping lmp inner join LocationMaster lm on lmp.locationId= lm.locationId inner join VehicleDetails vm on lmp.VehicleId=vm.Id where lmp.SessionName='" + Session["SessionName"] + "' and lmp.BranchCode=" + Session["BranchCode"] + " and lmp.VehicleId="+ddlVehicleH.SelectedValue+"";
        gvLocation.DataSource = oo.Fetchdata(sqls);
        gvLocation.DataBind();
        for (int i = 0; i < gvLocation.Rows.Count; i++)
        {
            Label vechicleId = (Label)gvLocation.Rows[i].FindControl("vechicleId");
            Label locationId = (Label)gvLocation.Rows[i].FindControl("locationId");
            LinkButton lnkDelete = (LinkButton)gvLocation.Rows[i].FindControl("lnkDelete");
            sql = "select count(*) cnt from LocationWiseVehicleAmount where RouteId=" + locationId.Text + " and VehicleId=" + vechicleId.Text + " and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
            if (oo.ReturnTag(sql, "cnt") != "0")
            {
                lnkDelete.Enabled = false;
                lnkDelete.Text = "<i class='fa fa-lock'></i>";
            }
        }
    }
    #region Method for loaddropdown
    protected void GetLocationList()
    {
        string sql2 = "SELECT  locationId, locationName+' ('+distanceInKm+')' locationName FROM LocationMaster where SessionName='" + Session["SessionName"]+ "' and BranchCode=" + Session["BranchCode"] + " and locationId not in (select locationId from locationMapping where VehicleId=" + ddlVehicleH.SelectedValue + " and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + ") order by locationName asc";
        oo.FillCheckBox_withValue(sql2, ddlLocationH, "locationName", "locationId");
        if (oo.Fetchdata(sql2).Rows.Count>0)
        {
            lnkSubmit.Visible = true;
            div_location.Visible = true;
        }
        else
        {
            lnkSubmit.Visible = false;
            div_location.Visible = false;
        }
    }
    protected void ddlVehicleH_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetLocationList();
        LoadGrid();
    }
    #endregion





    #region Method for (Insert,Update,delete)
    protected void Insert()
    {
        int sts = 0;
        for (int i = 0; i < ddlLocationH.Items.Count; i++)
        {
            if (ddlLocationH.Items[i].Selected)
            {
                cmd = new SqlCommand();
                cmd.CommandText = "locationMappingProc";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@action", "insert");
                cmd.Parameters.AddWithValue("@locationId", ddlLocationH.Items[i].Value);
                cmd.Parameters.AddWithValue("@VehicleId", ddlVehicleH.SelectedValue);
                cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());
                cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"].ToString());
                cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
                con.Open();
                int x = cmd.ExecuteNonQuery();
                con.Close();
                sts = sts + 1;
            }
        }
        if (sts > 0)
        {
            LoadGrid();
            GetLocationList();
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Submitted successfully.", "S");
        }
        else
        {
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Please select location(s)!", "A");
        }
    }
    
    #endregion

    #region Method for checkDuplicay and clear control

    
    #endregion

    protected void lnkSubmit_Click(object sender, EventArgs e)
    {
        if (ddlVehicleH.SelectedIndex == 0)
        {
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Please select Vehicle Name!", "A");
        }
        else
        {
            Insert();
        }
    }


    protected void lnkDelete_Click(object sender, EventArgs e)
    {
        LinkButton chk = (LinkButton)sender;
        lblvalue.Text = ((Label)chk.NamingContainer.FindControl("Label37")).Text;
        panelDelete_ModalPopupExtender.Show();
    }
    
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "locationMappingProc";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Connection = con;
        cmd.Parameters.AddWithValue("@action", "delete");
        cmd.Parameters.AddWithValue("@id", lblvalue.Text.Trim());
        cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"].ToString());
        cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();
        GetLocationList();
        LoadGrid();
        Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Deleted successfully.", "S");
    }



    protected void chkAll_CheckedChanged(object sender, EventArgs e)
    {
        if(chkAll.Checked)
        {
            foreach (ListItem item in ddlLocationH.Items)
            {
                item.Selected = true;
            }
        }
        else
        {
            foreach (ListItem item in ddlLocationH.Items)
            {
                item.Selected = false;
            }
        }
    }
}