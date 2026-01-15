using System;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class TC_UPBoard : Page
{
    SqlConnection con = new SqlConnection();
    Campus oo = new Campus();
    string sql = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
        {
            Response.Redirect("default.aspx");
        }
        BLL.BLLInstance.LoadHeader("Certificate", header);
        if (!IsPostBack)
        {
            sql = "Select AffiliationNo,SchoolNo from CollegeMaster where CollegeId=" + Session["BranchCode"] + "";

            lblaffno.Text = oo.ReturnTag(sql, "AffiliationNo");
            if (oo.ReturnTag(sql, "AffiliationNo") == "")
            {
                lblaf.Visible = false;
            }
            DisplayStudentCopy();
        }
    }

    protected void lnkPrint_Click(object sender, EventArgs e)
    {
        PrintHelper_New.ctrl = abc;
        ClientScript.RegisterStartupScript(GetType(), "onclick", "<script language=javascript>var winpop=window.open('Print_New.aspx','_blank'); if(!winpop || winpop.closed){alert('Please allow pop-up blocker from browser settings!')}</script>");
    }

    public void DisplayStudentCopy()
    {
        string className = "";
        string srno = "";
        try
        {
            sql = "Select ClassName from ClassMaster where SessionName='" + Session["SessionName"].ToString() + "' and ClassName <>'<--Select-->' order By CIDOrder ASC ";
            GridView1.DataSource = oo.GridFill(sql);
            GridView1.DataBind();

            sql = "select StudentGenaralDetail.SrNo,convert( Varchar(11),StudentGenaralDetail.DOB,105) as DOB,convert( Varchar(11),StudentGenaralDetail.DOB,106) as DOB1,ClassMaster.ClassName, StudentGenaralDetail.FirstName+' '+ StudentGenaralDetail.MiddleName+' '+StudentGenaralDetail.LastName as StudentName,StudentFamilyDetails.FatherName,StudentGenaralDetail.Religion,StudentFamilyDetails.MotherName,StudentFamilyDetails.FatherOccupation,StudentGenaralDetail.StPerAddress,StudentGenaralDetail.Pen,ApaarID from TCCollection inner join StudentGenaralDetail on StudentGenaralDetail.SrNo=TCCollection.srno inner join StudentFamilyDetails on StudentFamilyDetails.SrNo=TCCollection.srno inner join StudentOfficialDetails on StudentOfficialDetails.SrNo=TCCollection.srno inner join ClassMaster on ClassMaster.Id=StudentOfficialDetails.AdmissionForClassId  where TCCollection.RecieptNo='" + Request.QueryString["print"].ToString() + "' and TCCollection.SessionName='" + Session["SessionName"].ToString() + "' and StudentGenaralDetail.SessionName='" + Session["Top_sessionName"].ToString() + "'  and StudentFamilyDetails.SessionName='" + Session["Top_sessionName"].ToString() + "'   and ClassMaster.SessionName='" + Session["Top_sessionName"].ToString() + "' and  StudentOfficialDetails.SessionName='" + Session["Top_sessionName"].ToString() + "' ";
            Label9.Text = oo.ReturnTag(sql, "StudentName");
            Label10.Text = oo.ReturnTag(sql, "Religion");
            Label4.Text = oo.ReturnTag(sql, "FatherName");
            Label5.Text = oo.ReturnTag(sql, "MotherName");
            Label6.Text = oo.ReturnTag(sql, "FatherOccupation");
            Label7.Text = oo.ReturnTag(sql, "StPerAddress");
            Label12.Text = oo.ReturnTag(sql, "DOB");
            lblPen.Text = oo.ReturnTag(sql, "Pen");
            lblApaarID.Text = oo.ReturnTag(sql, "ApaarID");

            className = oo.ReturnTag(sql, "ClassName");
            srno = oo.ReturnTag(sql, "SrNo");
            Label13.Text = oo.Date_in_Words(Convert.ToInt32(Label12.Text.Substring(0, 2))) + " - " + oo.ReturnTag(sql, "DOB1").Substring(3, 3) + " - " + oo.Date_in_Words(Convert.ToInt32(Label12.Text.Substring(6, 4)));

            sql = "Select StudentPreviousSchool.SchoolName from StudentPreviousSchool where SrNo='" + oo.ReturnTag(sql, "SrNo") + "' ";
            Label11.Text = oo.ReturnTag(sql, "SchoolName");
        }
        catch (Exception e)
        {
            oo.MessageBox(e.ToString(), this.Page);
        }

        for (int i = 0; i < GridView1.Rows.Count; i++)
        {
            Label lbl_Class = (Label)GridView1.Rows[i].FindControl("lbl_Class");
            Label lbl_date = (Label)GridView1.Rows[i].FindControl("lbl_date");
            Label lbl_promotion = (Label)GridView1.Rows[i].FindControl("lbl_promotion");
            Label lbl_relieve = (Label)GridView1.Rows[i].FindControl("lbl_relieve");
            Label lbl_reason = (Label)GridView1.Rows[i].FindControl("lbl_reason");
            Label lbl_Year = (Label)GridView1.Rows[i].FindControl("lbl_Year");
            Label lbl_character = (Label)GridView1.Rows[i].FindControl("lbl_character");
            if (lbl_Class.Text == className)
            {
                sql = "select top 1 convert(varchar(11),StudentOfficialDetails.DateOfAdmiission,106) as DateOfAdmiission from StudentOfficialDetails where SrNo='" + srno + "'";
                lbl_date.Text = oo.ReturnTag(sql, "DateOfAdmiission");
                sql = "select convert(varchar(11),DateOfStruckOff,106) as RecordDate,Remark,ConductandWork,tcType from TCCollection where RecieptNo='" + Request.QueryString["print"].ToString() + "'";
                lbl_reason.Text = oo.ReturnTag(sql, "Remark");
                lbl_character.Text = oo.ReturnTag(sql, "ConductandWork");
                lbl_relieve.Text = oo.ReturnTag(sql, "RecordDate");
                //lbl_Year.Text = Session["SessionName"].ToString().Substring(5, 4);
                lbl_Year.Text = Session["SessionName"].ToString();
                if (oo.ReturnTag(sql, "tcType") == "Original")
                {
                    tcCopy.InnerText = "मूल प्रति";
                }
                if (oo.ReturnTag(sql, "tcType") == "Duplicate")
                {
                    tcCopy.InnerText = "दूसरी प्रति";
                }
                if (oo.ReturnTag(sql, "tcType") == "Triplicate")
                {
                    tcCopy.InnerText = "तीसरी प्रति";
                }
                if (oo.ReturnTag(sql, "tcType") == "Quadruplicate")
                {
                    tcCopy.InnerText = "चौथी प्रति";
                }
            }
        }

    }

    protected void LinkButton6_Click(object sender, EventArgs e)
    {
        // LaserPrint();
        PrintHelper_New.ctrl = abc;
        ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>var winpop=window.open('Print_New.aspx','_blank'); if(!winpop || winpop.closed){alert('Please allow pop-up blocker from browser settings!')}</script>");
    }
}