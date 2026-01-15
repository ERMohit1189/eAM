using System;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Web.Services;


public partial class LocationMaster : System.Web.UI.Page
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
            BranchCode = Session["BranchCode"].ToString();
        }
        GetLocationList();
    }

    #region Method for loaddropdown
    protected void GetLocationList()
    {
        sql = "SELECT  locationId, locationName, distanceInKm, oneWayAmt, twoWayAmt, recordDate, loginUser FROM LocationMaster where SessionName='" + Session["SessionName"]+ "' and BranchCode=" + Session["BranchCode"] + "";
        gvLocation.DataSource = oo.Fetchdata(sql);
        gvLocation.DataBind();
        for (int i = 0; i < gvLocation.Rows.Count; i++)
        {
            Label lblLocationId = (Label)gvLocation.Rows[i].FindControl("lblLocationId");
            LinkButton lnkDelete = (LinkButton)gvLocation.Rows[i].FindControl("lnkDelete");
            LinkButton lnkEdit = (LinkButton)gvLocation.Rows[i].FindControl("lnkEdit");
            sql = "select count(*) cnt from LocationMapping where SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + " and locationId=" + lblLocationId.Text.Trim().ToString() + "";
            if (oo.ReturnTag(sql, "cnt") != "0")
            {
                lnkDelete.Enabled = false;
                lnkDelete.Text = "<i class='fa fa-lock'></i>";
                lnkEdit.Enabled = false;
                lnkEdit.Text = "<i class='fa fa-lock'></i>";
            }
        }
        if (gvLocation.Rows.Count>0)
        {
            LinkButton1.Enabled = false;
            LinkButton1.Visible = false;
        }
        else
        {
            LinkButton1.Enabled = true;
            LinkButton1.Visible = true;
        }
    }

    #endregion

    #region Method for (Insert,Update,delete)
    protected void Insert()
    {
        Campus campe3 = new Campus();
        double OneWayAmt = 0;
        double TwoWayAmt = 0;
        double.TryParse(txtOneWayAmt.Text, out OneWayAmt);
        double.TryParse(txtTwoWayAmt.Text, out TwoWayAmt);
        if (OneWayAmt == 0 || TwoWayAmt == 0) 
        {
            campe3.msgbox(this.Page, msgbox, "Plaese enter valid amount!", "A");
        }
        else
        {
            cmd = new SqlCommand();
            cmd.CommandText = "LocationMasterProc";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@action", "insert");
            cmd.Parameters.AddWithValue("@locationName", txtLocationName.Text);
            cmd.Parameters.AddWithValue("@distanceInKm", txtDistance.Text);
            cmd.Parameters.AddWithValue("@oneWayAmt", txtOneWayAmt.Text);
            cmd.Parameters.AddWithValue("@twoWayAmt", txtTwoWayAmt.Text);
            cmd.Parameters.AddWithValue("@loginUser", Session["LoginName"].ToString());
            cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"].ToString());
            cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
            con.Open();
            int x=cmd.ExecuteNonQuery();
            con.Close();
            clear();
            if (x>0)
            {
                Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Submitted successfully.", "S");
            }
        }
        GetLocationList();
    }

    protected void CopyDataButton()
    {
            Campus campe3 = new Campus();
            cmd = new SqlCommand();
            cmd.CommandText = "CopyLocationMasterProc";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@action", "Copy");
            cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"].ToString());
            cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
            con.Open();
            int x = cmd.ExecuteNonQuery();
            con.Close();
          
            if (x > 0)
            {
                Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Copy Submitted successfully.", "S");
            }
        
        GetLocationList();
    }
    #endregion

    #region Method for checkDuplicay and clear control

    protected void clear()
    {
        txtLocationName.Text = "";
        txtDistance.Text = "";
        txtOneWayAmt.Text = "0.00";
        txtTwoWayAmt.Text = "0.00";
    }
    #endregion

    protected void lnkSubmit_Click(object sender, EventArgs e)
    {
        Insert();
    }

    protected void lnkEdit_Click(object sender, EventArgs e)
    {
        LinkButton chk = (LinkButton)sender;
        lblIDMP.Text = ((Label)chk.NamingContainer.FindControl("lblLocationId")).Text;
        txtLocationNameMP.Text = ((Label)chk.NamingContainer.FindControl("lblLocation")).Text;
        txtDistanceMP.Text = ((Label)chk.NamingContainer.FindControl("lblDistance")).Text;
        txtOneWayAmtMP.Text = ((Label)chk.NamingContainer.FindControl("lblOneWayAmount")).Text;
        txtTwoWayAmtMP.Text = ((Label)chk.NamingContainer.FindControl("lblTwoWayAmt")).Text;
        updatePanel_ModalPopupExtender.Show();
    }

    protected void lnkDelete_Click(object sender, EventArgs e)
    {
        LinkButton chk = (LinkButton)sender;
        lblvalue.Text = ((Label)chk.NamingContainer.FindControl("lblLocationId")).Text;
        panelDelete_ModalPopupExtender.Show();
    }

    protected void lnkUpdate_Click(object sender, EventArgs e)
    {
        if (txtLocationNameMP.Text != String.Empty)
        {
            sql = "Select LocationName from LocationMaster where LocationName='" + txtLocationNameMP.Text.Trim() + "' and LocationId<>'" + lblIDMP.Text.Trim() + "' and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
            if (oo.Duplicate(sql) == false)
            {
                Campus campe3 = new Campus();
                double OneWayAmt = 0;
                double TwoWayAmt = 0;
                double.TryParse(txtOneWayAmtMP.Text, out OneWayAmt);
                double.TryParse(txtTwoWayAmtMP.Text, out TwoWayAmt);
                if (OneWayAmt == 0 && TwoWayAmt == 0)
                {
                    campe3.msgbox(this.Page, msgbox, "Plaese enter valid amount!", "A");
                }
                else
                {
                    cmd = new SqlCommand();
                    cmd.CommandText = "LocationMasterProc";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@action", "update");
                    cmd.Parameters.AddWithValue("@locationId", lblIDMP.Text.Trim());
                    cmd.Parameters.AddWithValue("@locationName", txtLocationNameMP.Text.Trim());
                    cmd.Parameters.AddWithValue("@distanceInKm", txtDistanceMP.Text.Trim());
                    cmd.Parameters.AddWithValue("@oneWayAmt", txtOneWayAmtMP.Text);
                    cmd.Parameters.AddWithValue("@twoWayAmt", txtTwoWayAmtMP.Text);
                    cmd.Parameters.AddWithValue("@loginUser", Session["LoginName"].ToString());
                    cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"].ToString());
                    cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
                    con.Open();
                    int x = cmd.ExecuteNonQuery();
                    con.Close();
                    clear();
                    if (x > 0)
                    {
                        Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Submitted successfully.", "S");
                    }
                }
                GetLocationList();
            }
            else
            {
                Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Duplicate Entry!", "A");
            }
        }
        else
        {
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Plaese enter Location Name!", "A");
        }
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "LocationMasterProc";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Connection = con;
        cmd.Parameters.AddWithValue("@action", "delete");
        cmd.Parameters.AddWithValue("@locationId", lblvalue.Text.Trim());
        cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"].ToString());
        cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();
        GetLocationList();
        Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Deleted successfully.", "S");
    }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        CopyDataButton();
    }
}