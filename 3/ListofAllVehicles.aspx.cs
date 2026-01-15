using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _3_ListofAllVehiclesNew : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection();
    Campus oo = new Campus();
#pragma warning disable 169
    string sql = "",_sql="", _sql1 = "", _sql11 = "";
#pragma warning restore 169
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
        {
            Response.Redirect("default.aspx");
        }

        con = oo.dbGet_connection();
        Campus camp = new Campus(); 
       //camp.LoadLoader(loader);  //in cs file
        BLL.BLLInstance.LoadHeader("Report", header);
        //con.ConnectionString = ConfigurationManager.ConnectionStrings["Menu"].ConnectionString;
        if (!IsPostBack)
        {
            _sql = "Select Distinct Id,FuelType from FuelMaster cm ";
            _sql += " where  cm.BranchCode=" + Session["BranchCode"] + "  Order by Id";
            oo.FillDropDown_withValue(_sql, drpsection, "FuelType", "Id");
            drpsection.Items.Insert(0, new ListItem("All", "-1"));

            _sql1 = "Select Distinct Id,VehicleType from VehicleMaster cm ";
            _sql1 += " where  cm.BranchCode=" + Session["BranchCode"] + "  Order by Id";
            oo.FillDropDown_withValue(_sql1, drpclass, "VehicleType", "Id");
            drpclass.Items.Insert(0, new ListItem("All", "-1"));

           
        }
    }

    protected void lnkView_Click(object sender, EventArgs e)
    {
        _sql11 = "Select  ROW_NUMBER() OVER (ORDER BY id ASC) AS SrNo,Id,VehicleType, VehicleNo, VehicleModel, BodyType, SeatCapacity, RegistrationNo, RegistrationExpiry, ";
        _sql11 = _sql11 + " driverContact,SeatCapacity,EngineNo,MFG,InsuranceNo,PollutionReceiptNo,RegistrationExpiry,InsuranceExpiry,PollutionExpiry, VehicleMade, FuelType, EngineNo, ChasisNo, VehicleCC, MFG, VehicleRemark, OwnerName, OwnerContactNo, PermitNo, RegExpiryNotifyBefore, InsuranceNotifyBefore, PollutionNotifyBefore, ";
        _sql11 = _sql11 + "  ValidUpto, OwnerAddress, OwnerRemark, SessionName, BranchCode, LoginName,RecordDate,Driver, driverContact, InsuranceNo, InsuranceExpiry, PollutionReceiptNo, PollutionExpiry from VehicleDetails vt";
        _sql11 = _sql11 + " where   BranchCode=" + Session["BranchCode"].ToString() + "";
        if(drpclass.SelectedValue!="-1")
        {
            _sql11 = _sql11 + " and  vt.VehicleType='" + drpclass.SelectedItem.Text.Trim() + "' ";
        }
        if (drpsection.SelectedValue != "-1")
        {
            _sql11 = _sql11 + " and  vt.FuelType='" + drpsection.SelectedItem.Text.Trim() + "' ";
        }
        if (drpStatus.SelectedValue != "-1")
        {
            _sql11 = _sql11 + " and  vt.Status=" + drpStatus.SelectedValue + " ";
        }
        DataSet ds = new DataSet();
        ds= oo.GridFill(_sql11);
        if (ds.Tables.Count > 0)
        {
            heading.Text = "List of Vehicles ";
            vehicleTyps.Text = "Vehicle Type: " + drpclass.SelectedItem.Text.Trim();
            fuelTyps.Text = "Fuel Type: " + drpsection.SelectedItem.Text.Trim();
            statussS.Text = "Status: " + drpStatus.SelectedItem.Text.Trim();
            pnlcontrols.Visible = true;
            icondivs.Visible = true;
            abc.Visible = true;
            GridView1.DataSource = ds;
            GridView1.DataBind();
        }
        else
        {
            icondivs.Visible = false;
            heading.Text = "";
            vehicleTyps.Text = "";
            fuelTyps.Text = "";
            statussS.Text = "";
            pnlcontrols.Visible = false;
            abc.Visible = false;
            GridView1.DataSource = null;
            GridView1.DataBind();
        }
           // GridView1.DataSource = oo.GridFill(_sql11);
       // GridView1.DataBind();
       // GridView1.DataBind();
    }

    protected void ImageButton1_Click(object sender, EventArgs e)
    {
        oo.ExportTolandscapeWord(Response, "VehiclesList", gdv1);
    }

    protected void ImageButton2_Click(object sender, EventArgs e)
    {
        oo.ExportDivToExcelWithFormatting(Response, "VehiclesList.xls", gdv1, Server.MapPath("~/Admin/css/style.css"));
    }

    protected void ImageButton3_Click(object sender, EventArgs e)
    {
        BAL.objBal.ExporttolandscapePdf(Response, "VehiclesList", abc);
    }

    protected void ImageButton4_Click(object sender, EventArgs e)
    {
        PrintHelper_New.ctrl = abc;
        ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>var winpop=window.open('Print_New.aspx','_blank'); if(!winpop || winpop.closed){alert('Please allow pop-up blocker from browser settings!')}</script>");
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }
}