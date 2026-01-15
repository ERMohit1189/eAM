using System.Web.UI;
using System.Data.SqlClient;
using System;
public partial class admin_NewStudentReportSessionWise : Page
{
    SqlConnection con = new SqlConnection();
    Campus oo = new Campus();
    string sql = "";
    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
        {
            Response.Redirect("default.aspx");
        }
        //con.ConnectionString = ConfigurationManager.ConnectionStrings["Menu"].ConnectionString;
        con = oo.dbGet_connection();
        if (!IsPostBack)
        {
            sql = "select SessionName from SessionMaster";
            oo.FillDropDown(sql, DropDownList1, "SessionName");
            ImageButton1.Visible = false;
            ImageButton2.Visible = false;
            ImageButton3.Visible = false;
            ImageButton4.Visible = false;



        }
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        ImageButton1.Visible = false;
        ImageButton2.Visible = false;
        ImageButton3.Visible = false;
        ImageButton4.Visible = false;
        if (DropDownList1.SelectedItem.Text == "<--Select-->")
        {
            oo.MessageBox("Please <--Select-->", this.Page);
            GridView1.Visible = false;
        }

        else
        {
            sql = "Select Row_Number() over (order by SG.Id Asc) as SNo ,SG.Id, SC.SectionName,CM.ClassName,convert(nvarchar,So.DateOfAdmiission,106) as DateOfAdmiission , ";
            sql = sql + " SO.SectionId,SO.Card,SO.Medium,Sf.FatherName,SF.MotherName,SG.FirstName,SG.MiddleName,convert(nvarchar,SG.DOB,106) as DOB,SG.LastName,sg.StEnRCode as StEnRCode,sg.srno as srno,sg.SessionName from StudentGenaralDetail SG ";
            sql = sql + " left join StudentFamilyDetails SF on SG.StEnRCode=SF.StEnRCode";
            sql = sql + " left join StudentOfficialDetails SO on SG.StEnRCode=SO.StEnRCode";
            sql = sql + " left join ClassMaster CM on SO.AdmissionForClassId=CM.Id";
            sql = sql + " left join SectionMaster SC on SO.SectionId=SC.Id";
            sql = sql + " where SO.SessionName='" + DropDownList1.SelectedItem.ToString() + "'";
            sql = sql + " and Sf.SessionName='" + DropDownList1.SelectedItem.ToString() + "'";
            sql = sql + " and Sg.SessionName='" + DropDownList1.SelectedItem.ToString() + "'";
            sql = sql + " and cm.SessionName='" + DropDownList1.SelectedItem.ToString() + "'";
            sql = sql + " and Sc.SessionName='" + DropDownList1.SelectedItem.ToString() + "'";
            sql = sql + " and sg.BranchCode=" + Session["BranchCode"].ToString() + "";
            sql = sql + "   and SO.Withdrwal is null";
            sql = sql + "  and so.typeOfAdmision='" + DropDownList2.SelectedItem.ToString() + "'";

            GridView1.DataSource = oo.GridFill(sql);
            GridView1.DataBind();
            ImageButton1.Visible = true;
            ImageButton2.Visible = true;
            ImageButton3.Visible = true;
            ImageButton4.Visible = true;
            GridView1.Visible = true;
            if (GridView1.Rows.Count == 0)
            {
                oo.MessageBox("Sorry, No Record(s) found!", this.Page);
                ImageButton1.Visible = false;
                ImageButton2.Visible = false;
                ImageButton3.Visible = false;
                ImageButton4.Visible = false;
            }
        }
    }
    protected void Print_Click(object sender, EventArgs e)
    {
        //PrintHelper_New.ctrl = abc;
        //ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>var winpop=window.open('Print_New.aspx','_blank'); if(!winpop || winpop.closed){alert('Please allow pop-up blocker from browser settings!')}</script>");
    }
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        ImageButton1.Visible = false;
        ImageButton2.Visible = false;
        ImageButton3.Visible = false;
        ImageButton4.Visible = false;
    }
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        oo.ExportToWord(Response, "LisOfAllStudent.doc", gdv);
    }
    protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
    {
        oo.ExportToExcel("LisOfAllStudent.xls", GridView1);
    }
    protected void ImageButton3_Click(object sender, ImageClickEventArgs e)
    {

    }
    protected void ImageButton4_Click(object sender, ImageClickEventArgs e)
    {
        PrintHelper_New.ctrl = abc;
        ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>var winpop=window.open('Print_New.aspx','_blank'); if(!winpop || winpop.closed){alert('Please allow pop-up blocker from browser settings!')}</script>");
    }
}