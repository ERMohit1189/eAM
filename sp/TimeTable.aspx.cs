using System;
using System.Web;
using System.Web.UI;
using System.Data;
using System.IO;
using System.Web.UI.HtmlControls;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;


public partial class sp_TimeTable : Page
{
    public DataTable dt;
    public string MSG = "";
    Campus oo = new Campus();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetClass();

            SelectDDL();
            GetValue();
        }
    }
    string sql = "";
    private void SelectDDL()
    {
        //string SrNo = Session["Srno"].ToString();
        //string session = Session["SessionName"].ToString();
        //Int32 ClassID=0, SectionID=0, BranchID=0;

        //ClassID = Convert.ToInt32(Campus.CampusInstance.ReturnTag("SELECT S.AdmissionForClassId FROM StudentOfficialDetails S WHERE SessionName='" + session + "' AND SrNo='" + SrNo + "'", "AdmissionForClassId"));
        //BranchID = Convert.ToInt32(Campus.CampusInstance.ReturnTag("SELECT S.Branch FROM StudentOfficialDetails S WHERE SessionName='" + session + "' AND SrNo='" + SrNo + "'", "Branch"));
        //SectionID = Convert.ToInt32(Campus.CampusInstance.ReturnTag("SELECT S.SectionId FROM StudentOfficialDetails S WHERE SessionName='" + session + "' AND SrNo='" + SrNo + "'", "SectionId"));

        sql = "Select ClassId,SectionId,BranchId from";
        sql = sql + " AllStudentRecord_UDF('" + Session["SessionName"].ToString() + "'," + Session["BranchCode"].ToString() + ") where SrNo='" + Session["Srno"].ToString() + "'";

        string ClassID = BAL.objBal.ReturnTag(sql, "classId");
        string BranchID = BAL.objBal.ReturnTag(sql, "BranchId");
        string SectionID = BAL.objBal.ReturnTag(sql, "SectionId");
        
        ddlClass.Items.FindByValue(ClassID.ToString()).Selected = true;
        GetClassBranch();
        GetClassSection();
        ddlClassBranch.Items.FindByValue(BranchID.ToString()).Selected = true;
        ddlClassSection.Items.FindByValue(SectionID.ToString()).Selected = true;
    }

    protected void btnGet_Click(object sender, EventArgs e)
    {
        GetValue();
    }
    public void GetValue()
    {
        if (ddlClass.SelectedIndex < 1 && ddlClassSection.SelectedIndex < 1 && ddlClassBranch.SelectedIndex < 1)
        {
            MSG = "Select All Fields !";
            //new Campus().MessageBoxforUpdatePanel(MSG, btnGet);
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, new ShowMSG().MSG(MSG), "A");

        }
        else
        {
            gvTimeTable.DataSource = null;
            gvTimeTable.DataBind();
            dt = new DAL().GetTimeTableClassWise(Convert.ToInt32(ddlClass.SelectedValue), Convert.ToInt32(ddlClassBranch.SelectedValue), Convert.ToInt32(ddlClassSection.SelectedValue), "Gen");
            if (dt != null && dt.Rows.Count > 0)
            {
                gvTimeTable.DataSource = dt;
                gvTimeTable.DataBind();
                gvTimeTable.HeaderStyle.CssClass = "row-head-style text-center";
            }
            else
            {
                MSG = "No Record Found !";
                //new Campus().MessageBoxforUpdatePanel(MSG, btnGet);
                Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, new ShowMSG().MSG(MSG), "A");


            }
        }
    }
    protected void ImageButton1_Click(object sender, EventArgs e)
    {
        GetValue();
        oo.ExportToWord(Response, "TimeTable", gdv);
    }
    protected void ImageButton2_Click(object sender, EventArgs e)
    {
        oo.ExportToExcel("TimeTable.xls", gvTimeTable);
    }
    protected void ImageButton3_Click(object sender, EventArgs e)
    {
        Response.ContentType = "application/pdf";
        Response.AddHeader("content-disposition", "attachment;filename=Export.pdf");
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        StringWriter sw = new StringWriter();
        HtmlTextWriter hw = new HtmlTextWriter(sw);
        HtmlForm frm = new HtmlForm();
        gvTimeTable.Parent.Controls.Add(frm);
        frm.Attributes["runat"] = "server";
        frm.Controls.Add(gvTimeTable);
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

    protected void ddlClassBranch_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void ddlClass_SelectedIndexChanged(object sender, EventArgs e)
    {
        //GetClassBranch();
        //GetClassSection();
    }
    protected void ddlClassSection_SelectedIndexChanged1(object sender, EventArgs e)
    {

    }

    public void GetClass()
    {
        dt = new DAL().GetClass(-1, Session["SessionName"].ToString());
        if (dt != null && dt.Rows.Count > 0)
        {
            BLL.FillDropDown(ddlClass, dt, "Class", "ClassID", 'S');
        }
    }

    public void GetClassSection()
    {
        dt = new DAL().GetClassSection(Convert.ToInt32(ddlClass.SelectedValue), -1, Session["SessionName"].ToString());
        if (dt != null && dt.Rows.Count > 0)
        {
            BLL.FillDropDown(ddlClassSection, dt, "SectionName", "ClassSectionID", 'S');
        }
    }

    public void GetClassBranch()
    {
        dt = new DAL().GetClassBranch(Convert.ToInt32(ddlClass.SelectedValue), -1, Session["SessionName"].ToString());
        if (dt != null && dt.Rows.Count > 0)
        {
            BLL.FillDropDown(ddlClassBranch, dt, "branchName", "Id", 'S');
        }
    }

}