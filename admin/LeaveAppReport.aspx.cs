using System;
using System.Web;
using System.Web.UI;
using System.Data;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using System.Web.UI.HtmlControls;

public partial class admin_LeaveAppReport : Page
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
            GetEmp();
            GetLeaveApplication();
            txtFromDate.Text = Convert.ToDateTime(DAL.DALInstance.GetDateTime()).ToString("dd-MMM-yyyy");
            txtToDate.Text = Convert.ToDateTime(DAL.DALInstance.GetDateTime()).ToString("dd-MMM-yyyy");
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
            ShowMSG("No Record Found !","A");
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

    protected void ImageButton1_Click(object sender, EventArgs e)
    {
        oo.ExportToWord(Response, "TimeTable", abc);
    }

    protected void ImageButton2_Click(object sender, EventArgs e)
    {
        oo.ExportToExcel("TimeTable.xls", gvLeaveApp);
    }

    protected void ImageButton3_Click(object sender, EventArgs e)
    {
        Response.ContentType = "application/pdf";
        Response.AddHeader("content-disposition", "attachment;filename=Export.pdf");
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        StringWriter sw = new StringWriter();
        HtmlTextWriter hw = new HtmlTextWriter(sw);
        HtmlForm frm = new HtmlForm();
        gvLeaveApp.Parent.Controls.Add(frm);
        frm.Attributes["runat"] = "server";
        frm.Controls.Add(gvLeaveApp);
        frm.RenderControl(hw);
        StringReader sr = new StringReader(sw.ToString());
        Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
        HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
        PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
        pdfDoc.Open();
        htmlparser.Parse(sr);
        pdfDoc.Close();
        Response.Write(pdfDoc);
        Response.End();
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

    private void ShowMSG(string MSG, string Type)
    {
        Campus camp = new Campus(); camp.msgbox(this.Page, dvMSG , BLL.BLLInstance.FetchMSG(MSG), Type);
    }
}