using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class DeskSlip : Page
{
    Campus oo = new Campus();
    string sql = "";
#pragma warning disable 169
    Byte[] img = null;
#pragma warning restore 169
    protected void Page_Load(object sender, EventArgs e)
    {
        Campus camp = new Campus(); camp.LoadLoader(loader);
        if (!IsPostBack)
        {
            sql = "Select CourseName,Id from CourseMaster where  BranchCode=" + Session["BranchCode"].ToString() + "";
            oo.FillDropDown_withValue(sql, drpCourse, "CourseName", "Id");
            drpCourse.Items.Insert(0, "<--Select-->");
            drpsection.Items.Insert(0, "<--Select-->");
            drpclass.Items.Insert(0, "<--Select-->");
        }
    }
    protected void drpCourse_SelectedIndexChanged(object sender, EventArgs e)
    {
        sql = "Select ClassName,Id from ClassMaster where Course='" + drpCourse.SelectedValue+"' and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
        oo.FillDropDown_withValue(sql, drpclass, "ClassName", "Id");
        drpclass.Items.Insert(0, "<--Select-->");
        rpStudentDetails.DataSource = null;
        rpStudentDetails.DataBind();
        divshow.Visible = false;
    }
    public void loadStudentData()
    {
        studentInfo(rpStudentDetails);
    }

    public void studentInfo(Repeater rpt)
    {
        rpStudentDetails.DataSource = null;
        rpStudentDetails.DataBind();
        //GET_STUDENTREORDFORREPORTCARD
        List<SqlParameter> param = new List<SqlParameter>();
        param.Add(new SqlParameter("@SessionName", Session["SessionName"].ToString()));
        param.Add(new SqlParameter("@BranchCode", Session["BranchCode"].ToString()));

        sql = "Select SrNo,Medium,HouseName,InstituteRollNo RollNo,Name as Name,StPerAddress,FamilyContactNo,ClassName,CombineClassName, ";
        sql = sql + " SectionName,FatherName,MotherName,Height,Weight,DOB,BloodGroup,VisionL,";
        sql = sql + " VisionR,DentalHygiene,SessionName,case when isnull(PhotoPath,'')='' then '../uploads/EmptyImage.jpg' else PhotoPath end PhotoPath from AllStudentRecord_UDF('" + Session["SessionName"].ToString() + "'," + Session["BranchCode"].ToString() + ")";
        sql = sql + " where CourseId='" + drpCourse.SelectedValue.ToString() + "'  and Withdrwal is null ";
        if (drpclass.SelectedIndex != 0)
        {
            sql = sql + " and ClassId='" + drpclass.SelectedValue.ToString() + "' ";
        }
        if (drpsection.SelectedIndex != 0)
        {
            sql = sql + " and sectionid='" + drpsection.SelectedValue.ToString() + "' ";
        }
        sql = sql + " order by Name Asc";

        rpt.DataSource = DLL.objDll.SelectRecord_usingExecuteDataset(sql, param);
        rpt.DataBind();
        if (rpt.Items.Count>0)
        {
            divshow.Visible = true;
            for (int i = 0; i < rpt.Items.Count; i++)
            {
                if ((i+1)%24==0)
                {
                    System.Web.UI.HtmlControls.HtmlGenericControl pagebreak = (System.Web.UI.HtmlControls.HtmlGenericControl)rpt.Items[i].FindControl("pagebreak");
                    pagebreak.Style.Add("page-break-after", "always");
                }
                
            }
        }
    }
    
    
    protected void btnView_Click(object sender, EventArgs e)
    {
        loadStudentData();
    }
    
   
    protected void drpclass_SelectedIndexChanged(object sender, EventArgs e)
    {
        BLL.BLLInstance.loadSection(drpsection, Session["SessionName"].ToString(), drpclass.SelectedValue.ToString());
        rpStudentDetails.DataSource = null;
        rpStudentDetails.DataBind();
        divshow.Visible = false;

    }

    protected void drpsection_SelectedIndexChanged(object sender, EventArgs e)
    {
        rpStudentDetails.DataSource = null;
        rpStudentDetails.DataBind();
        divshow.Visible = false;

    }
    
}