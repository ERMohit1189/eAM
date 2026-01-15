using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class admin_LeaveApplication : Page
{
    public string MSG , SQL = string.Empty;
    public static int V04ID = 0;
    public DataTable dt = new DataTable();
    Campus cmps = new Campus();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                txtFromDate.Text = Convert.ToDateTime(DAL.DALInstance.GetDateTime()).ToString("dd-MMM-yyyy");
                dvNoOfDays.Visible = false;
                dvHalfType.Visible = false;
                dvToDate.Visible = false;
                
                GetDays();
            }
            catch (Exception) { }
        }
    }

    private void Reset()
    {
        txtAddress.Text = "";
        txtContact1.Text = "";
        txtContact2.Text = "";
        txtReason.Text = "";
        txtToDate.Text = "";
        ddlHaltType.SelectedIndex = 0;
        ddlNoOfDays.SelectedIndex = 0;
        rblIsHalfDay.ClearSelection();

        dvNoOfDays.Visible = false;
        dvHalfType.Visible = false;
        dvToDate.Visible = false;
    }

    public void GetDays()
    {
        for (int i = 1; i <= 30; i++)
        {
            ddlNoOfDays.Items.Add(new ListItem(i.ToString(),i.ToString()));
        }
    }

    private void Validation(string RefNo, Control cntrl)
    {
        MSG = "";

        if(gvEmp.Rows.Count==0 && MSG=="")
        {
            MSG = "Enter Valid Emp ID !";
            txtEmpID.Focus();
        }
        if (rblIsHalfDay.SelectedIndex < 0 && MSG == "")
        {
            MSG = "Is Half Day !";
            rblIsHalfDay.Focus();
        }
        if (txtFromDate.Text.Trim()=="" && MSG == "")
        {
            MSG = "Enter Date !";
            txtFromDate.Focus();
        }
        if (txtReason.Text.Trim() == "" && MSG == "")
        {
            MSG = "Enter Reason !";
            txtReason.Focus();
        }
        if (txtAddress.Text.Trim() == "" && MSG == "")
        {
            MSG = "Enter Address!";
            txtAddress.Focus();
        }
        if (txtContact1.Text.Trim() == "" && MSG == "")
        {
            MSG = "Enter Contact No.!";
            txtContact1.Focus();
        }
        if (txtContact2.Text.Trim() == "" && MSG == "")
        {
            MSG = "Enter Alternate Contact No.!";
            txtContact2.Focus();
        }

        if (MSG != string.Empty)
        {
            ShowMSG(MSG, "A");
        }
        else
        {
            SetLeaveApplication();
        }
    }

    protected void btnInsert_Click(object sender, EventArgs e)
    {
        SQL = "I";
        Validation("", btnInsert);
    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        Reset();
    }

    private void SetLeaveApplication()
    {
        BAL.clsEmpLeave obj = new BAL.clsEmpLeave();

        obj.SQL = "I";
        obj.A03ID = 0;
        obj.EmpID = gvEmp.Rows[0].Cells[0].Text.Trim();
        obj.AppSubject = "";
        obj.AppReason = txtReason.Text.Trim();
        obj.LeaveDays = (Convert.ToDecimal(rblIsHalfDay.SelectedValue) == 0) ? Convert.ToDecimal(ddlNoOfDays.SelectedValue) : Convert.ToDecimal(.5);
        obj.Address = txtAddress.Text.Trim();
        obj.ContactNo1 = txtContact1.Text.Trim();
        obj.ContactNo2 = txtContact2.Text.Trim();
        obj.AppDate = Convert.ToDateTime(DAL.DALInstance.GetDateTime());
        obj.FromDate = Convert.ToDateTime(txtFromDate.Text.Trim());
        obj.ToDate = Convert.ToDateTime((Convert.ToDecimal(rblIsHalfDay.SelectedValue) == 0) ? (txtToDate.Text.Trim()) :"1 Jan 1900");
        obj.Status = 2;
        obj.IsHalfDay = Convert.ToInt32(rblIsHalfDay.SelectedValue);
        obj.HalfDayType = (Convert.ToDecimal(rblIsHalfDay.SelectedValue) == 1) ? Convert.ToInt32(ddlHaltType.SelectedValue) : 0;
        obj.ApproveEmpID = "";

        MSG = "";
        MSG = DAL.DALInstance.SetEmpLeave(obj);

        if (string.IsNullOrEmpty(MSG))
        {
            MSG = SQL;
            Reset();
            ShowMSG(MSG, "S");
        }
        else
        {
            ShowMSG(MSG,"A");
        }
    }

    protected void txtEmpID_TextChanged(object sender, EventArgs e)
    {
        gvEmp.DataSource = null;
        gvEmp.DataBind();

        if (!string.IsNullOrEmpty(txtEmpID.Text.Trim()))
        {
            dt = null;

            txtEmpID.Text = txtEmpID.Text.Trim().ToString();

            BAL.clsDayBook obj = new BAL.clsDayBook();
            obj.EmpID = txtEmpID.Text.Trim();
            dt = DAL.DALInstance.GetStaff(obj);

            if (dt != null && dt.Rows.Count > 0)
            {
                gvEmp.Visible = true;
                gvEmp.DataSource = dt;
            }
            else
            {
                gvEmp.DataSource = null;
                ShowMSG("Invalid Emp ID !", "A");
            }

            gvEmp.DataBind();
        }
        txtEmpID.Focus();
    }

    protected void rblIsHalfDay_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rblIsHalfDay.SelectedValue == "1")
        {
            dvNoOfDays.Visible = false;
            dvHalfType.Visible = true;
            dvToDate.Visible = false;
            lblDate.Text = "Date";
        }
        else if (rblIsHalfDay.SelectedValue == "0")
        {
            dvHalfType.Visible = false;
            dvNoOfDays.Visible = true;
            dvToDate.Visible = true;
            lblDate.Text = "From Date";
        }
        SetToDate();
        rblIsHalfDay.Focus();
    }

    public void SetToDate()
    {
        txtToDate.Text = "";
        if (rblIsHalfDay.SelectedValue == "0")
        {
            if (!string.IsNullOrEmpty(txtFromDate.Text.Trim()))
            {
                txtToDate.Text = Convert.ToDateTime(Campus.CampusInstance.ReturnTag("SELECT CONVERT(datetime,'" + txtFromDate.Text.Trim() + "')+" + ddlNoOfDays.SelectedValue + "-1 ToDate", "ToDate")).ToString("dd-MMM-yyyy");
            }
        }
        txtFromDate.Focus();
    }

    protected void ddlHaltType_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlHaltType.Focus();
    }

    protected void ddlNoOfDays_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlNoOfDays.Focus();
        SetToDate();
    }

    protected void txtFromDate_TextChanged(object sender, EventArgs e)
    {
        txtFromDate.Focus();
        SetToDate();
    }

    private void ShowMSG(string MSG, string Type)
    {
        Campus camp = new Campus(); camp.msgbox(this.Page, dvMSG , BLL.BLLInstance.FetchMSG(MSG), Type);
    }
}