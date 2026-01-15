using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AdmitCard : Page
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
            sql = "Select CourseName,Id from CourseMaster where BranchCode=" + Session["BranchCode"].ToString() + "";
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
        drpExamination.SelectedIndex = 0;
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
        if (drpExamination.SelectedIndex==0)
        {
            rpt.DataSource = null;
            rpt.DataBind();
            return;
        }
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
                if ((i+1)%6==0)
                {
                    System.Web.UI.HtmlControls.HtmlGenericControl pagebreak = (System.Web.UI.HtmlControls.HtmlGenericControl)rpt.Items[i].FindControl("pagebreak");
                    pagebreak.Style.Add("page-break-after", "always");
                }
                
            }
        }
    }
    
    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }

    //protected void lnkPrint_Click(object sender, EventArgs e)
    //{
    //    PrintHelper_New.ctrl = divExport;
    //    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "onclick", "var winpop=window.open('Print_New.aspx','_blank'); if(!winpop || winpop.closed){alert('Please allow pop-up blocker from browser settings!')}", true);
    //}

    //protected void lnkWord_Click(object sender, EventArgs e)
    //{
    //    BAL.objBal.ExportToWord(Response, Session["srno"] + "ReportCard", divExport);
    //}
    //protected void lnkExcel_Click(object sender, EventArgs e)
    //{
    //    BAL.objBal.ExportDivToExcelWithFormatting(Response, Session["srno"] + "ReportCard", divExport, Server.MapPath("~/css/theme.min.css"));
    //}

    protected void drpExamination_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadStudentData();
    }
    
    protected void rpStudentDetails_ItemCreated(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            string sqlss = "Select CollegeName, CologeLogoPath, ClassTeacherSign, PrincpalSign from CollegeMaster where BranchCode=" + Session["BranchCode"].ToString() + "";
            Image imgLogo = (Image)e.Item.FindControl("imgLogo");
            imgLogo.ImageUrl = oo.ReturnTag(sqlss, "CologeLogoPath");

            Image AccountantImg = (Image)e.Item.FindControl("AccountantImg");
            AccountantImg.ImageUrl = oo.ReturnTag(sqlss, "ClassTeacherSign");
            Image Principalimg = (Image)e.Item.FindControl("Principalimg");
            Principalimg.ImageUrl = oo.ReturnTag(sqlss, "PrincpalSign");

            Label collegeName = (Label)e.Item.FindControl("collegeName");
            collegeName.Text = oo.ReturnTag(sqlss, "CollegeName");


            Label lblEvaluation = (Label)e.Item.FindControl("lblEvaluation");
            lblEvaluation.Text = drpExamination.SelectedItem.ToString();

            Label lblSession = (Label)e.Item.FindControl("lblSession");
            lblSession.Text = " (" + Session["SessionName"].ToString()+")";
            
        }

       

        System.Web.UI.HtmlControls.HtmlGenericControl divnote = (System.Web.UI.HtmlControls.HtmlGenericControl)e.Item.FindControl("divnote");
        System.Web.UI.HtmlControls.HtmlGenericControl divnateanddisclamir = (System.Web.UI.HtmlControls.HtmlGenericControl)e.Item.FindControl("divnateanddisclamir");

        

    }
   
    protected void drpclass_SelectedIndexChanged(object sender, EventArgs e)
    {
        BLL.BLLInstance.loadSection(drpsection, Session["SessionName"].ToString(), drpclass.SelectedValue.ToString());
        drpExamination.SelectedIndex = 0;
        rpStudentDetails.DataSource = null;
        rpStudentDetails.DataBind();
        divshow.Visible = false;
    }

    protected void drpsection_SelectedIndexChanged(object sender, EventArgs e)
    {
        drpExamination.SelectedIndex = 0;
        rpStudentDetails.DataSource = null;
        rpStudentDetails.DataBind();
        divshow.Visible = false;
    }
}