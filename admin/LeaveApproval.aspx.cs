using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class admin_LeaveApproval : Page
{
    public string MSG = "", SQL = "";
    public static int H01ID = 0;
    public DataTable dt;
    Campus oo = new Campus();

    protected void Page_Load(object sender, EventArgs e)
    {
        Campus camp = new Campus(); camp.LoadLoader(loader);

        if (!IsPostBack)
        {
            ddlStatus.Enabled = false;
            GetEmp();
            txtFromDate.Text = Convert.ToDateTime(DAL.DALInstance.GetDateTime()).ToString("dd-MMM-yyyy");
            txtToDate.Text = Convert.ToDateTime(DAL.DALInstance.GetDateTime()).ToString("dd-MMM-yyyy");
            GetLeaveApplication();
        }
    }

    public void GetEmp()
    {
        dt = new DataTable();
        dt = DAL.DALInstance.GetValueInTable("SELECT Z1.EmpId,Z1.EmpName,Z1.EmpName+' ('+Z1.EmpId+')' EmpNameID FROM (SELECT DISTINCT G.EmpId,G.EFirstName+ISNULL(' '+G.ELastName,'') EmpName FROM EmpGeneralDetail G JOIN EmpployeeOfficialDetails O ON O.EmpId=G.EmpId WHERE O.Withdrwal IS NULL OR O.Withdrwal='') Z1 ORDER BY CONVERT(smallint,Z1.EmpId)");
        if (dt != null && dt.Rows.Count > 0)
        {
            BLL.FillDropDown(ddlEmpID, dt, "EmpNameID", "EmpID", 'A');
        }
    }

    private void GetLeaveApplication()
    {
        BAL.clsEmpLeave obj = new BAL.clsEmpLeave();
        obj.FromDate = Convert.ToDateTime(txtFromDate.Text.Trim() == "" ? "1 Jan 1900" : txtFromDate.Text.Trim());
        obj.ToDate = Convert.ToDateTime(txtToDate.Text.Trim() == "" ? "1 Jan 1900" : txtToDate.Text.Trim());
        obj.Status = Convert.ToInt32(ddlStatus.SelectedValue);
        obj.EmpID = ddlEmpID.SelectedValue;
        obj.A03ID = -1;

        dt = new DataTable();
        dt = DAL.DALInstance.GetEmpLeave(obj);

        if (dt != null && dt.Rows.Count > 0)
        {
            dt = BLL.BLLInstance.GetSerialNo(ref dt, "SrNo");
            gvLeaveApp.DataSource = dt;
        }
        else
        {
            gvLeaveApp.DataSource = null;
            ShowMSG("No Record Found !", "A");
        }
        gvLeaveApp.DataBind();
    }

    protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetLeaveApplication();
    }

    protected void txtFromDate_TextChanged(object sender, EventArgs e)
    {
        GetLeaveApplication();
    }

    protected void txtToDate_TextChanged(object sender, EventArgs e)
    {
        GetLeaveApplication();
    }

    protected void ddlEmpID_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetLeaveApplication();
    }

    private void ShowMSG(string MSG, string Type)
    {
        Campus camp = new Campus(); camp.msgbox(this.Page, dvMSG , BLL.BLLInstance.FetchMSG(MSG), Type);
    }

    protected void lbltnApproved_Click(object sender, EventArgs e)
    {
        GridViewRow Row = (GridViewRow)(sender as LinkButton).Parent.Parent;
        Int32 i = Row.RowIndex;
        LinkButton btn = (LinkButton)(gvLeaveApp.Rows[i].FindControl("lbltnApproved"));

        BAL.clsEmpLeave obj = new BAL.clsEmpLeave();

        obj.SQL = "A";
        obj.A03ID = Convert.ToInt32(((Label)(gvLeaveApp.Rows[i].FindControl("lblA03ID"))).Text.Trim());
        obj.Status = 1;
        obj.ApproveEmpID = Session["LoginName"].ToString();
        obj.AppDate = Convert.ToDateTime("1 Jan 1900");
        obj.ToDate = Convert.ToDateTime("1 Jan 1900");
        obj.FromDate= Convert.ToDateTime("1 Jan 1900");

        string MSG = DAL.DALInstance.SetEmpLeave(obj);

        if (string.IsNullOrEmpty(MSG))
        {
            ShowMSG("Approved successfully.", "S");
            GetLeaveApplication();
        }
        else
            ShowMSG(MSG, "A");
    }

    protected void lbtnCencelled_Click(object sender, EventArgs e)
    {
        GridViewRow Row = (GridViewRow)(sender as LinkButton).Parent.Parent;
        Int32 i = Row.RowIndex;
        LinkButton btn = (LinkButton)(gvLeaveApp.Rows[i].FindControl("lbltnApproved"));

        BAL.clsEmpLeave obj = new BAL.clsEmpLeave();

        obj.SQL = "A";
        obj.A03ID = Convert.ToInt32(((Label)(gvLeaveApp.Rows[i].FindControl("lblA03ID"))).Text.Trim());
        obj.Status = 0;
        obj.ApproveEmpID = Session["LoginName"].ToString();
        obj.AppDate = Convert.ToDateTime("1 Jan 1900");
        obj.ToDate = Convert.ToDateTime("1 Jan 1900");
        obj.FromDate = Convert.ToDateTime("1 Jan 1900");

        string MSG = DAL.DALInstance.SetEmpLeave(obj);

        if (string.IsNullOrEmpty(MSG))
        {
            ShowMSG("Cencalled successfully.", "S");
            GetLeaveApplication();
        }
        else
            ShowMSG(MSG, "A");
    }

    protected void gvLeaveApp_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if(e.Row.RowType==DataControlRowType.DataRow)
        {
            Int32 Status=Convert.ToInt32((e.Row.FindControl("lblStatus") as Label).Text.Trim());

            if (Status == 2)
            {
                (e.Row.FindControl("lbltnApproved") as LinkButton).Visible = true;
                (e.Row.FindControl("lbtnCencelled") as LinkButton).Visible = true;
            }
            else
            {
                (e.Row.FindControl("lbltnApproved") as LinkButton).Visible = false;
                (e.Row.FindControl("lbtnCencelled") as LinkButton).Visible = false;
            }
        }
    }
}