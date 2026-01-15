using System;
using System.Web.UI;

public partial class admin_Staff_Resignation_Relieve_Report : Page
{
    Campus oo = new Campus();
    string sql = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        Campus camp = new Campus(); camp.LoadLoader(loader); 

        if (Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
        {
            Response.Redirect("default.aspx");
        }
        BLL.BLLInstance.LoadHeader("Report", header);
        if (!IsPostBack)
        {
            loadGrid();
        }
    }
    public void loadGrid()
    {
        string order = "Desc";
        if (rbOrder.SelectedIndex == 0)
        {
            order = "Asc";
        }
        string shortby = "order by Convert(int,Right(EmpCode,3)) " + order + "";
        if (rbShort.SelectedIndex == 1)
        {
            shortby = "order by Convert(Date,Dateofreleving) " + order + "";
        }

        sql = "Select Id,Empid,EmpCode,Name,FatherName,MotherName,Convert(varchar(11),DateofJoining,106) as DateofJoining,Convert(varchar(11),";
        sql = sql + " Dateofreleving,106) as Dateofreleving,Remark,Contactno,Designation from EmpWithdrawlRecord  where BranchCode=" + Session["BranchCode"].ToString() + "" + shortby + "";
        Grd.DataSource = oo.GridFill(sql);
        Grd.DataBind();
        if (Grd.Rows.Count > 0)
        {
            divExport.Visible = true;
        }
        else
        {
            divExport.Visible = false;
        }
    }
    protected void ImageButton1_Click(object sender, EventArgs e)
    {
        if (Grd.Rows.Count > 0)
        {
            oo.ExportTolandscapeWord(Response, "StaffResignationRelieveReport", divExport);
        }
    }
    protected void ImageButton2_Click(object sender, EventArgs e)
    {
        if (Grd.Rows.Count > 0)
        {
            oo.ExportDivToExcelWithFormatting(Response, "StaffResignationRelieveReport", divExport);
        }
    }
    protected void ImageButton3_Click(object sender, EventArgs e)
    {
        if (Grd.Rows.Count > 0)
        {
            oo.ExporttolandscapePdf(Response, "StaffResignationRelieveReport", divExport);
        }
    }
    protected void ImageButton4_Click(object sender, EventArgs e)
    {
        PrintHelper_New.ctrl = divExport;
        ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>var winpop=window.open('Print_New.aspx','_blank'); if(!winpop || winpop.closed){alert('Please allow pop-up blocker from browser settings!')}</script>");
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }

    protected void rbShort_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadGrid();
    }
    protected void rbOrder_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadGrid();
    }
}