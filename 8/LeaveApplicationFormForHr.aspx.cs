using System;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class LeaveApplicationFormForHr : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection();
    SqlCommand cmd;
    Campus oo = new Campus();
    string sql = "";

    protected void Page_Load(object sender, EventArgs e)
    {
         if (Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
        {
            Response.Redirect("default.aspx");
        }
        con = oo.dbGet_connection();
        Campus camp = new Campus(); camp.LoadLoader(loader);  //in cs file
        if (!IsPostBack)
        {
           
        }
    }

    
    protected void txtEnter_TextChanged(object sender, EventArgs e)
    {
        displayEmpInfo();
    }
    protected void lnkShow_Click(object sender, EventArgs e)
    {
        displayEmpInfo();
    }
    public void displayEmpInfo()
    {
        var empId = Request.Form[hfEmployeeId.UniqueID];
        if (empId == string.Empty)
        {
            empId = txtEnter.Text.Trim();
        }
        sql = "Select eod.EmpId EmpId,eod.Ecode Ecode,egd.EFirstName+egd.EMiddleName+egd.ELastName as EmpName,egd.EFatherName FatherName,eod.Designation Designation, egd.EPreAddress, egd.EMobileNo from EmpployeeOfficialDetails eod ";
        sql = sql + " inner join EmpGeneralDetail egd on eod.Ecode=egd.Ecode and eod.EmpId=egd.EmpId where eod.Withdrwal is null ";
        sql = sql + " and eod.ecode='" + empId.Trim() + "' and eod.BranchCode=" + Session["BranchCode"].ToString() + " and egd.BranchCode=" + Session["BranchCode"].ToString() + "";
        Grd.DataSource = oo.GridFill(sql);
        Grd.DataBind();
        if (Grd.Rows.Count <= 0)
        {
            Campus camp = new Campus(); camp.msgbox(Page, Div1, "Sorry, No record found!", "A");
            divempDetails.Visible = false;
            divLeaveList.Visible = false;
        }
        else
        {
            divempDetails.Visible = true;
            loadGrid();
            
        }
    }
    protected void loadGrid()
    {
        var empId = Request.Form[hfEmployeeId.UniqueID];
        if (empId == string.Empty)
        {
            empId = txtEnter.Text.Trim();
        }
        sql = "Select id, ApplicationDate, Reason, case when Abbribation='L' then 'Leave' else case when Abbribation='HD' then 'Half Day' else case when Abbribation='SL' then 'Short Leave' end end end Abbribation, ";
        sql = sql + " case when Laevetype='F' then 'Full Day' else case when Laevetype='FH' then 'First Half' else case when Laevetype='SH' then 'Second Half' end end end Laevetype ";
        sql = sql + " from LeaveAppliction where BranchCode=" + Session["BranchCode"].ToString() + " and SessionName='" + Session["SessionName"].ToString() + "' and Empcode='" + empId + "' and Status='Pending' order by ApplicationDate desc";
        var dt = oo.Fetchdata(sql);
        GridView1.DataSource = dt;
        GridView1.DataBind();
        if (GridView1.Rows.Count > 0)
        {
            divLeaveList.Visible = true;
        }
        else
        {
            divLeaveList.Visible = false;
            Campus camp = new Campus(); camp.msgbox(Page, Div1, "Sorry, No record found!", "A");
            
        }

    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Save();
    }

    protected void Save()
    {
        var empId = Request.Form[hfEmployeeId.UniqueID];
        if (empId == string.Empty)
        {
            empId = txtEnter.Text.Trim();
        }
        else
        {
            int sts = 0;
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                DropDownList ddlStatus = (DropDownList)GridView1.Rows[i].FindControl("ddlStatus");
                if (ddlStatus.SelectedValue != "")
                {
                    Label idLbl = (Label)GridView1.Rows[i].FindControl("idLbl");
                    TextBox txtHrReason = (TextBox)GridView1.Rows[i].FindControl("txtHrReason");
                    cmd = new SqlCommand();
                    cmd.CommandText = "LeaveApplictionProc";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@EmpCode", empId.Trim());
                    cmd.Parameters.AddWithValue("@Id", idLbl.Text.Trim());
                    cmd.Parameters.AddWithValue("@status", ddlStatus.SelectedValue);
                    cmd.Parameters.AddWithValue("@Reason", txtHrReason.Text.Trim());
                    cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"].ToString());
                    cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());
                    cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
                    cmd.Parameters.AddWithValue("@Action", "Approved_Or_Cancellled");
                    con.Open();
                    cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                    con.Close();
                    sts = sts + 1;
                }
            }
            if (sts > 0)
            {
                loadGrid();
                Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Submitted successfully.", "S");
            }
            else
            {
                Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Please select Approve or Cancel at least one!", "A");
            }
        }
    }
    
}