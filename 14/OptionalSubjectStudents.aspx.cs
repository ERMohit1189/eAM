using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class OptionalSubjectStudents : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection();
    SqlCommand cmd = new SqlCommand();
    Campus oo = new Campus();
    string sql = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((string)Session["LoginName"] == "" || (string)Session["BranchCode"] == "" || (string)Session["SessionName"] == "" || Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
        {
            Response.Redirect("default.aspx");
        }
        con = oo.dbGet_connection();
        Campus camp = new Campus(); camp.LoadLoader(loader);
        BLL.BLLInstance.LoadHeader("Report", header);
        if (!IsPostBack)
        {
            loadclass();

            drpBranch.Items.Insert(0, new ListItem("<--Select-->", ""));
            drpSection.Items.Insert(0, new ListItem("<--Select-->", ""));
            drpmedium.Items.Insert(0, new ListItem("<--Select-->", "0"));
           // drpOptSubjects.Items.Insert(0, new ListItem("<--Select-->", ""));
            drpOptSubjects.Items.Insert(0, new ListItem("<--Select-->", "0"));
            loadmedium();
        }
    }
    public override void VerifyRenderingInServerForm(Control control)
    {

    }
    private void loadBranch()
    {
        sql = "Select BranchName,Id from BranchMaster Where ClassId='" + drpclass.SelectedValue + "' and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
        oo.FillDropDown_withValue(sql, drpBranch, "BranchName", "Id");
        drpBranch.Items.Insert(0, new ListItem("<--Select-->", ""));
    }
    public void loadclass()
    {
        sql = "Select Id,ClassName,CidOrder from ClassMaster Where SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + " Order by CIDOrder";
        oo.FillDropDown_withValue(sql, drpclass, "ClassName", "Id");
        oo.fillSelectvalue(drpclass, "<--Select-->");
    }

    public void loadsection()
    {
        sql = "Select id, SectionName from SectionMaster";
        sql = sql + " where ClassNameId=" + drpclass.SelectedValue + " and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " ";
        oo.FillDropDown_withValue(sql, drpSection, "SectionName", "id");
        drpSection.Items.Insert(0, new ListItem("<--Select-->", ""));
    }

    public void loadmedium()
    {
        sql = "Select Medium from MediumMaster";
        sql = sql + " Where  BranchCode=" + Session["BranchCode"].ToString() + "";
        oo.FillDropDown(sql, drpmedium, "Medium");
        drpmedium.Items.Insert(0, new ListItem("<--Select-->", "0"));
    }
    public void loadOptSubject()
    {
        string qq = "select id, SubjectName from TTSubjectMaster where SubjectType<>'Compulsory' and Medium='" + drpmedium.SelectedValue + "' ";
        qq = qq + " and ClassId=" + drpclass.SelectedValue + " and  BranchId=" + drpBranch.SelectedValue + " and BranchCode=" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"] + "'";
        oo.FillDropDown_withValue(qq, drpOptSubjects, "SubjectName", "Id");
        drpOptSubjects.Items.Insert(0, new ListItem("<--Select-->", "0"));
      //  drpOptSubjects.Items.Insert(0, new ListItem("<--Select-->", ""));
    }

    protected void drpclass_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadBranch();
    }
    protected void drpBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadsection();
        loadmedium();
    }
    protected void drpmedium_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadOptSubject();
    }
    protected void LinkView_Click(object sender, EventArgs e)
    {
        displayEmpInfo();
    }
    public void displayEmpInfo()
    {
        if (drpOptSubjects.SelectedValue != "")
        {
            if (drpOptSubjects.SelectedValue != "0")
            {
                string subjects = drpOptSubjects.SelectedItem.Text;
                //string ss = "select asr.SrNo, asr.Name, FatherName, CombineClassName,";
                //ss = ss + ""+subjects+"" + " as optsubjects";
                //ss = ss + "  from VW_AllStudentRecord asr ";
                //ss = ss + " inner join ICSEOptionalSubjectAllotment osa on osa.SrNo=asr.SrNo and osa.SessionName=asr.SessionName and  osa.BranchCode=asr.BranchCode ";
                //ss = ss + " where asr.ClassId=" + drpclass.SelectedValue + " and  asr.BranchId=" + drpBranch.SelectedValue + " ";
                //ss = ss + " and  asr.SectionId=case when '" + drpSection.SelectedValue + "'='' then asr.SectionId else '" + drpSection.SelectedValue + "' end and asr.BranchCode=" + Session["BranchCode"] + " ";
                //ss = ss + " and asr.medium='" + drpmedium.SelectedValue + "' and osa.OptSubjectId=" + drpOptSubjects.SelectedValue + " and asr.SessionName='" + Session["SessionName"] + "' and isnull(Withdrwal, '')='' order by name asc";

                string ss = "SELECT Distinct asr.SrNo, asr.Name, asr.FatherName, asr.CombineClassName,asr.Medium, " +
            "'" + subjects + "' AS optsubjects " +  // Properly enclosing the subject text in quotes
            "FROM VW_AllStudentRecord asr " +
            "INNER JOIN ICSEOptionalSubjectAllotment osa ON osa.SrNo = asr.SrNo " +
            "AND osa.SessionName = asr.SessionName " +
            "AND osa.BranchCode = asr.BranchCode " +
            "WHERE asr.ClassId = " + drpclass.SelectedValue + " " +
            "AND asr.BranchId = " + drpBranch.SelectedValue + " " +
            "AND asr.SectionId = CASE WHEN '" + drpSection.SelectedValue + "' = '' " +
            "THEN asr.SectionId ELSE '" + drpSection.SelectedValue + "' END " +
            "AND asr.BranchCode = '" + Session["BranchCode"] + "'  "; // Ensuring BranchCode is treated as a string
                if (drpmedium.SelectedValue != "0")
                {
                    ss = ss + " AND asr.medium = '" + drpmedium.SelectedValue + "'  ";
                }
                ss = ss + "  AND osa.OptSubjectId = " + drpOptSubjects.SelectedValue + " " +
            "AND asr.SessionName = '" + Session["SessionName"] + "' " +
            "AND ISNULL(Withdrwal, '') = '' " +
            "ORDER BY asr.Name ASC;";
                var dt = oo.Fetchdata(ss);
                if (dt.Rows.Count > 0)
                {
                    divExport.Visible = true;
                    heading.Text = "Optional Subject Allotment Report of " + drpOptSubjects.SelectedItem.Text + "";
                    Grd.DataSource = dt;
                    Grd.DataBind();
                }
                else
                {
                    Grd.DataSource = null;
                    Grd.DataBind();
                    divExport.Visible = false;
                }
            }
            else
            {
              
                //string ss = "select asr.SrNo, asr.Name, FatherName, CombineClassName,";
                //ss = ss + subjects + " as optsubjects";
                //ss = ss + "  from VW_AllStudentRecord asr ";
                //ss = ss + " inner join ICSEOptionalSubjectAllotment osa on osa.SrNo=asr.SrNo and osa.SessionName=asr.SessionName and  osa.BranchCode=asr.BranchCode ";
                //ss = ss + " where asr.ClassId=" + drpclass.SelectedValue + " and  asr.BranchId=" + drpBranch.SelectedValue + " ";
                //ss = ss + " and  asr.SectionId=case when '" + drpSection.SelectedValue + "'='' then asr.SectionId else '" + drpSection.SelectedValue + "' end and asr.BranchCode=" + Session["BranchCode"] + " ";
                //ss = ss + " and asr.medium='" + drpmedium.SelectedValue + "'  and asr.SessionName='" + Session["SessionName"] + "' and isnull(Withdrwal, '')='' order by name asc";


                string ss = "SELECT Distinct asr.SrNo, asr.Name, asr.FatherName, asr.CombineClassName,asr.Medium, " +
             " ( " +
             "     SELECT STUFF( " +
             "         ( " +
             "             SELECT ', ' + SubjectName " +
             "             FROM TTSubjectMaster " +
             "             WHERE id IN ( " +
             "                 SELECT OptSubjectId " +
             "                 FROM ICSEOptionalSubjectAllotment " +
             "                 WHERE SrNo = asr.SrNo " +
             "                 AND SessionName = asr.SessionName " +
             "                 AND BranchCode = asr.BranchCode " +
             "             ) " +
             "             FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)') " +
             "     , 1, 2, '') " +
             " ) AS OptSubjects " +  // Fixed missing space before FROM
             " FROM VW_AllStudentRecord asr " +
             " INNER JOIN ICSEOptionalSubjectAllotment osa ON osa.SrNo = asr.SrNo " +
             " AND osa.SessionName = asr.SessionName " +
             " AND osa.BranchCode = asr.BranchCode " +
             " WHERE asr.ClassId = " + drpclass.SelectedValue + " " +
             " AND asr.BranchId = " + drpBranch.SelectedValue + " " +
             " AND asr.SectionId = CASE WHEN '" + drpSection.SelectedValue + "' = '' " +
             " THEN asr.SectionId ELSE '" + drpSection.SelectedValue + "' END " +
             " AND asr.BranchCode = '" + Session["BranchCode"] + "' " ;
                if(drpmedium.SelectedValue!="0")
                {
                    ss = ss + " AND asr.medium = '" + drpmedium.SelectedValue + "'  ";
                }
           
                //" AND osa.OptSubjectId = " + drpOptSubjects.SelectedValue + " " +  // Uncomment if needed
                ss = ss + " AND asr.SessionName = '" + Session["SessionName"] + "' " +
             " AND ISNULL(Withdrwal, '') = '' " +
             " ORDER BY asr.Name ASC;";


                var dt = oo.Fetchdata(ss);
                if (dt.Rows.Count > 0)
                {
                    divExport.Visible = true;
                    heading.Text = "Optional Subject Allotment Report of All Subjects";
                    Grd.DataSource = dt;
                    Grd.DataBind();
                }
                else
                {
                    Grd.DataSource = null;
                    Grd.DataBind();
                    divExport.Visible = false;
                }
            }
        }
        else
        {
            Grd.DataSource = null;
            Grd.DataBind();
            divExport.Visible = false;
        }
      
       
    }
    protected void ImageButton1_Click(object sender, EventArgs e)
    {
        oo.ExportTolandscapeWord(Response, "OptionalSubjectStudents", gdv1);
    }
    protected void ImageButton2_Click(object sender, EventArgs e)
    {
        oo.ExportDivToExcelWithFormatting(Response, "OptionalSubjectStudents.xls", gdv1, Server.MapPath("~/Admin/css/style.css"));
    }
    protected void ImageButton3_Click(object sender, EventArgs e)
    {
        oo.ExporttolandscapePdf(Response, "OptionalSubjectStudents", abc);
    }
    protected void ImageButton4_Click(object sender, EventArgs e)
    {
        PrintHelper_New.ctrl = abc;
        ClientScript.RegisterStartupScript(GetType(), "onclick", "<script language=javascript>var winpop=window.open('Print_New.aspx','_blank'); if(!winpop || winpop.closed){alert('Please allow pop-up blocker from browser settings!')}</script>");
    }
}
