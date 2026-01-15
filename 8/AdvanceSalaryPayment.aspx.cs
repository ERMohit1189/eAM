using System;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class AdvanceSalaryPayment : System.Web.UI.Page
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
        sql = "Select eod.EmpId EmpId,eod.Ecode Ecode,egd.EFirstName+' '+egd.EMiddleName+' '+egd.ELastName as EmpName,egd.EFatherName FatherName,eod.Designation Designation, egd.EPreAddress, egd.EMobileNo from EmpployeeOfficialDetails eod ";
        sql = sql + " inner join EmpGeneralDetail egd on eod.Ecode=egd.Ecode and eod.EmpId=egd.EmpId where eod.Withdrwal is null ";
        sql = sql + " and eod.ecode='" + empId.Trim() + "' and eod.BranchCode=" + Session["BranchCode"].ToString() + " and egd.BranchCode=" + Session["BranchCode"].ToString() + "";
        Grd.DataSource = oo.GridFill(sql);
        Grd.DataBind();
        if (Grd.Rows.Count <= 0)
        {
            Campus camp = new Campus(); camp.msgbox(Page, Div1, "Sorry, No record found!", "A");
            divempDetails.Visible = false;
            divList.Visible = false;
            divControls.Visible = false;
        }
        else
        {
            divempDetails.Visible = true;
            divControls.Visible = true;
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
        string ss = "select EmpId from EmpployeeOfficialDetails where Ecode='" + empId.Trim() + "' and BranchCode=" + Session["BranchCode"] + "";
        string emp_Id = oo.ReturnTag(ss, "EmpId");
        sql = "select *, format(RecordedDate, 'dd-MMM-yyyy') dates, format(RecordedDate, 'dd-MMM-yyyy hh:mm:ss tt') recDate, [Status] sts from SalaryAdvance where BranchCode=" + Session["BranchCode"].ToString() + " and EmpId='" + emp_Id + "' order by RecordedDate desc";
        var dt = oo.Fetchdata(sql);
        GridView1.DataSource = dt;
        GridView1.DataBind();
        if (GridView1.Rows.Count > 0)
        {
            double totalCredit = 0;
            double totalDebit = 0;
            double totalBalance = 0;
            divList.Visible = true;
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                Label Status = (Label)GridView1.Rows[i].FindControl("Status");
                DropDownList ddlStatus = (DropDownList)GridView1.Rows[i].FindControl("ddlStatus");
                LinkButton LinkSts = (LinkButton)GridView1.Rows[i].FindControl("LinkSts");
                if (Status.Text == "Pending")
                {
                    ddlStatus.Visible = true;
                    LinkSts.Visible = true;
                    Status.Visible = false;
                }
                else
                {
                    ddlStatus.Visible = false;
                    LinkSts.Visible = false;
                    Status.Visible = true;
                }
                Label Credit = (Label)GridView1.Rows[i].FindControl("Credit");
                Label Debit = (Label)GridView1.Rows[i].FindControl("Debit");
                totalCredit = totalCredit + double.Parse(Credit.Text==""?"0": Credit.Text);
                totalDebit = totalDebit + double.Parse(Debit.Text==""?"0": Debit.Text);
            }
            totalBalance = (totalCredit - totalDebit);
            Label lbltotalCredit = (Label)GridView1.FooterRow.FindControl("totalCredit");
            lbltotalCredit.Text = totalCredit.ToString("0.00");
            Label lbltotalDebit = (Label)GridView1.FooterRow.FindControl("totalDebit");
            lbltotalDebit.Text = totalDebit.ToString("0.00");
            Label lbltotalBalance = (Label)GridView1.FooterRow.FindControl("totalBalance");
            lbltotalBalance.Text = totalBalance.ToString("0.00");
        }
        else
        {
            divList.Visible = false;
            Campus camp = new Campus(); camp.msgbox(Page, Div1, "Sorry, No advance salary record found!", "A");
            
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
            string ss = "select EmpId from EmpployeeOfficialDetails where Ecode='" + empId.Trim() + "' and BranchCode=" + Session["BranchCode"] + "";
            string emp_Id = oo.ReturnTag(ss, "EmpId");
            cmd = new SqlCommand();
            cmd.CommandText = "SalaryAdvanceProc";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@EmpID", emp_Id.Trim());
            cmd.Parameters.AddWithValue("@PaymentDate", txtDate.Text.Trim());
            cmd.Parameters.AddWithValue("@Mode", DropDownMOD.SelectedValue);
            if (DropDownMOD.SelectedValue == "Cash")
            {
                cmd.Parameters.AddWithValue("@Status", "Paid");
            }
            else
            {
                cmd.Parameters.AddWithValue("@Status", ddlStatus.SelectedValue);
            }
            if (ddlType.SelectedValue == "Credit")
            {
                cmd.Parameters.AddWithValue("@Credit", txtAmount.Text.Trim());
            }
            else
            {
                cmd.Parameters.AddWithValue("@Debit", txtAmount.Text.Trim());
            }
            cmd.Parameters.AddWithValue("@Type", ddlType.SelectedValue);
            cmd.Parameters.AddWithValue("@Narration", txtNarration.Text.Trim());
            cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"].ToString());
            cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());
            cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
            cmd.Parameters.AddWithValue("@Action", "insert");
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                con.Close();
                txtDate.Text = "";
                DropDownMOD.SelectedValue = "Cash";
                ddlStatus.SelectedValue = "Pending";
                ddlType.SelectedValue = "Credit";
                divsts.Visible = false;
                txtAmount.Text = "";
                txtNarration.Text = "";
                loadGrid();
                Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Submitted successfully.", "S");
            }
            catch (SqlException ex1)
            {
                Campus camp = new Campus(); camp.msgbox(Page, msgbox, ex1.Message, "A");
            }
            catch (Exception ex)
            {
                Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Some technical problem, please contact to admin!", "A");
            }
        }
    }


    protected void LinkSts_Click(object sender, EventArgs e)
    {
        LinkButton chk = (LinkButton)sender;
        Label idLbl = (Label)chk.NamingContainer.FindControl("idLbl");
        DropDownList ddlStatus = (DropDownList)chk.NamingContainer.FindControl("ddlStatus");

        var empId = Request.Form[hfEmployeeId.UniqueID];
        if (empId == string.Empty)
        {
            empId = txtEnter.Text.Trim();
        }
        string ss = "select EmpId from EmpployeeOfficialDetails where Ecode='" + empId.Trim() + "' and BranchCode=" + Session["BranchCode"] + "";
        string emp_Id = oo.ReturnTag(ss, "EmpId");
        cmd = new SqlCommand();
        cmd.CommandText = "SalaryAdvanceProc";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Connection = con;
        cmd.Parameters.AddWithValue("@EmpID", emp_Id.Trim());
        cmd.Parameters.AddWithValue("@Id", idLbl.Text.Trim());
        cmd.Parameters.AddWithValue("@Status", ddlStatus.SelectedValue);
        cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
        cmd.Parameters.AddWithValue("@Action", "status_update");
        try
        {
            con.Open();
            cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            con.Close();
            loadGrid();
            Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Updated successfully.", "S");
        }
        catch (Exception ex)
        {
            Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Some technical problem, please contact to admin!", "A");
        }
    }

    protected void DropDownMOD_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DropDownMOD.SelectedValue == "Cash")
        {
            divsts.Visible = false;
        }
        else
        {
            divsts.Visible = true;

        }
    }
}