using System;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class staff_LeaveApplicationForm : System.Web.UI.Page
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
            
            oo.AddDateMonthYearDropDown(FromYY, FromMM, FromDD);
            oo.FindCurrentDateandSetinDropDown(FromYY, FromMM, FromDD);

            oo.AddDateMonthYearDropDown(ToYY, ToMM, ToDD);
            oo.FindCurrentDateandSetinDropDown(ToYY, ToMM, ToDD);
            
        }
    }

    protected void loadGrid()
    {
        var empId = Request.Form[hfEmployeeId.UniqueID];
        if (empId == string.Empty)
        {
            empId = txtEnter.Text.Trim();
        }
        string sqls = "select * from ( ";
        sqls = sqls + "Select egd.EFirstName+egd.EMiddleName+egd.ELastName as EmpName, Ecode, LeavGroup, Contact1 from EmpGeneralDetail egd ";
        sqls = sqls + "inner join LeaveAppliction l on l.Empcode=egd.Ecode and l.BranchCode=egd.BranchCode  and l.SessionName=egd.SessionName ";
        sqls = sqls + "where egd.Withdrwal is null and l.BranchCode=" + Session["BranchCode"].ToString() + " and l.SessionName='" + Session["SessionName"].ToString() + "'";
        sqls = sqls + "and l.Empcode='" + empId + "' group by  egd.EFirstName,egd.EMiddleName,egd.ELastName, Ecode, LeavGroup, Contact1";
        sqls = sqls + ")T1 order by T1.LeavGroup desc";
        var dt = oo.Fetchdata(sqls);
        rpt.DataSource = dt;
        rpt.DataBind();
        if (rpt.Items.Count>0)
        {
            for (int i = 0; i < rpt.Items.Count; i++)
            {
                Label LeavGroup = (Label)rpt.Items[i].FindControl("LeavGroup");
                GridView GridView1 = (GridView)rpt.Items[i].FindControl("GridView1");
                sql = "Select ApplicationDate, Reason, case when Abbribation='L' then 'Leave' else case when Abbribation='HD' then 'Half Day' else case when Abbribation='SL' then 'Short Leave' end end end Abbribation, ";
                sql = sql + " case when Laevetype='F' then 'Full Day' else case when Laevetype='FH' then 'First Half' else case when Laevetype='SH' then 'Second Half' end end end Laevetype, ";
                sql = sql + " case when Status='Pending' then 'Pending' else case when Status='Approved' then 'Granted' else case when Status='Cancelled' then 'Cancelled' end end end Status, case when isnull(HrReason, '')='' then '' else '('+HrReason+')' end HrReason ";
                sql = sql + " from LeaveAppliction where BranchCode=" + Session["BranchCode"].ToString() + " and SessionName='" + Session["SessionName"].ToString() + "' and Empcode='" + empId + "' and LeavGroup="+ LeavGroup.Text + " order by id asc";
                GridView1.DataSource = oo.GridFill(sql);
                GridView1.DataBind();
            }
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
        sql = "select ShiftName from A02_EmpShiftMaster where EmpDesId=(select top(10) EmpDesId from  EmpDesMaster where EmpDesName=(select top(10) Designation from EmpployeeOfficialDetails where Ecode='" + empId.Trim() + "' and BranchCode=" + Session["BranchCode"].ToString() + ") and BranchCode=" + Session["BranchCode"].ToString() + ") and BranchCode=" + Session["BranchCode"].ToString() + "";
        string sql2s = "select count(*)cnt from LeaveAppliction where Empcode='" + empId.Trim() + "' and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + " and status='Pending'";
        if (!oo.Duplicate(sql))
        {
            divempDetails.Visible = false;
            divControls.Visible = false;
            divLeaveList.Visible = false;
            Campus camp = new Campus(); camp.msgbox(Page, Div1, "Please create shift for the designation of staff!", "A");
            return;
        }
        if(oo.ReturnTag(sql2s, "cnt")!="0")
        {
            divempDetails.Visible = false;
            divControls.Visible = false;
            loadGrid();
            Campus camp = new Campus(); camp.msgbox(Page, Div1, "New leave can not be enter because your old leave application is pending!", "A");
            return;
        }
        divempDetails.Visible = true;
        divControls.Visible = true;
        divLeaveList.Visible = true;
        sql = "Select eod.EmpId EmpId,eod.Ecode Ecode,egd.EFirstName+egd.EMiddleName+egd.ELastName as EmpName,egd.EFatherName FatherName,eod.Designation Designation, egd.EPreAddress, egd.EMobileNo from EmpployeeOfficialDetails eod ";
        sql = sql + " inner join EmpGeneralDetail egd on eod.Ecode=egd.Ecode and eod.EmpId=egd.EmpId where eod.Withdrwal is null ";
        sql = sql + " and eod.ecode='" + empId.Trim() + "' and eod.BranchCode=" + Session["BranchCode"].ToString() + " and egd.BranchCode=" + Session["BranchCode"].ToString() + "";
        Grd.DataSource = oo.GridFill(sql);
        Grd.DataBind();
        if (Grd.Rows.Count <= 0)
        {
            Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Sorry, No Record found!", "A");
        }
        else
        {
            txtContactno1.Text = oo.ReturnTag(sql, "EMobileNo");
            txtAddress.Text = oo.ReturnTag(sql, "EPreAddress");
            GenrateGrid();
            loadGrid();
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
        if (GridGenrate.Rows.Count == 0)
        {
            Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Invalid date range selected!", "A");
            return;
        }
        else
        {
            sql = "select (isnull(MAX(LeavGroup), 0)+1) LeavGroup from LeaveAppliction";
            string LeavGroup = oo.ReturnTag(sql, "LeavGroup");
            for (int i = 0; i < GridGenrate.Rows.Count; i++)
            {
                Label date = (Label)GridGenrate.Rows[i].FindControl("date");
                DropDownList ddlAbbribation = (DropDownList)GridGenrate.Rows[i].FindControl("ddlAbbribation");
                DropDownList ddlLaevetype = (DropDownList)GridGenrate.Rows[i].FindControl("ddlLaevetype");

                cmd = new SqlCommand();
                cmd.CommandText = "LeaveApplictionProc";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@EmpCode", empId);
                cmd.Parameters.AddWithValue("@LeavGroup", LeavGroup);
                cmd.Parameters.AddWithValue("@ApplicationDate", date.Text);
                cmd.Parameters.AddWithValue("@Abbribation", ddlAbbribation.SelectedValue);
                cmd.Parameters.AddWithValue("@Laevetype", ddlLaevetype.SelectedValue);
                cmd.Parameters.AddWithValue("@Reason", txtReason.Text.Trim());
                cmd.Parameters.AddWithValue("@Contact1", txtContactno1.Text.Trim());
                cmd.Parameters.AddWithValue("@Contact2", txtContactno2.Text.Trim());
                cmd.Parameters.AddWithValue("@Address", txtAddress.Text.Trim());
                cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"].ToString());
                cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());
                cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
                cmd.Parameters.AddWithValue("@Action", "insert");
                con.Open();
                cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                con.Close();
            }
            loadGrid();
            GridGenrate.DataSource = null;
            GridGenrate.DataBind();
            Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Submitted successfully.", "S");
            divControls.Visible = false;
            divLeaveList.Visible = false;
            loadGrid();
        }
    }

    
    protected void FromYY_SelectedIndexChanged(object sender, EventArgs e)
    {
        oo.YearDropDown(FromYY, FromMM, FromDD);
        GenrateGrid();
    }
    protected void FromMM_SelectedIndexChanged(object sender, EventArgs e)
    {
        oo.MonthDropDown(FromYY, FromMM, FromDD);
        GenrateGrid();
    }
    protected void ToYY_SelectedIndexChanged(object sender, EventArgs e)
    {
        oo.YearDropDown(ToYY, ToMM, ToDD);
        GenrateGrid();
    }
    protected void ToMM_SelectedIndexChanged(object sender, EventArgs e)
    {
        oo.MonthDropDown(ToYY, ToMM, ToDD);
        GenrateGrid();
    }


    protected void FromDD_SelectedIndexChanged(object sender, EventArgs e)
    {
        GenrateGrid();
    }

    protected void ToDD_SelectedIndexChanged(object sender, EventArgs e)
    {
        GenrateGrid();
    }

    protected void GenrateGrid()
    {
        GridGenrate.DataSource = null;
        GridGenrate.DataBind();
        string FromDate = FromDD.Text + "-" + FromMM.Text + "-" + FromYY.Text;
        string ToDate = ToDD.Text + "-" + ToMM.Text + "-" + ToYY.Text;
        if (DateTime.Parse(FromDate) <= DateTime.Parse(ToDate))
        {
            sql = "select DATEDIFF(DAY, '" + FromDate + "', '" + ToDate + "')+1 as days";
            int dayslength = int.Parse(oo.ReturnTag(sql, "days"));
            DataTable table = new DataTable();
            table.Columns.Add("date", typeof(string));
            for (int i = 0; i < dayslength; i++)
            {
                sql = "select format(dateadd(DAY, " + i + ", '" + FromDate + "'), 'dd-MMM-yyyy') as dateIn";
                string dateIn = oo.ReturnTag(sql, "dateIn");
                DataRow dr;
                dr = table.NewRow();
                dr["date"] = dateIn;
                table.Rows.Add(dr);
            }
            GridGenrate.DataSource = table;
            GridGenrate.DataBind();
        }
        else
        {
            Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Invalid date range selected!", "A");
        }
    }

    protected void ddlAbbribation_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList chk = (DropDownList)sender;
        DropDownList ddlAbbribation = (DropDownList)chk.NamingContainer.FindControl("ddlAbbribation");
        DropDownList ddlLaevetype = (DropDownList)chk.NamingContainer.FindControl("ddlLaevetype");
        ddlLaevetype.Items.Clear();
        if (ddlAbbribation.SelectedValue=="L")
        {
            ddlLaevetype.Items.Insert(0, new ListItem("Full Day", "F"));
            ddlLaevetype.SelectedValue = "F";
        }
        if (ddlAbbribation.SelectedValue == "HD")
        {
            ddlLaevetype.Items.Insert(0, new ListItem("First Half", "FH"));
            ddlLaevetype.Items.Insert(1, new ListItem("Second Half", "SH"));
            ddlLaevetype.SelectedValue = "FH";
        }
        if (ddlAbbribation.SelectedValue == "SL")
        {
            ddlLaevetype.Items.Insert(0, new ListItem("First Half", "FH"));
            ddlLaevetype.Items.Insert(1, new ListItem("Second Half", "SH"));
            ddlLaevetype.SelectedValue = "FH";
        }
        
    }
    
}