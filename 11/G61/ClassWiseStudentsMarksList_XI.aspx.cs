using System;
using System.Web.UI.WebControls;

public partial class comman_ClassWiseStudentsMarksList_XI : System.Web.UI.Page
{
    Campus oo = new Campus();
    string sql = "";
    protected void Page_PreInIt(object sender, EventArgs e)
    {
        if (Session["Logintype"] == null) { Response.Redirect("~/default.aspx"); }
        if (Session["Logintype"].ToString() == "Admin")
        {
            this.MasterPageFile = "~/Master/admin_root-manager.master";
        }
        else if (Session["Logintype"].ToString() == "Staff")
        {
            this.MasterPageFile = "~/Staff/staff_root-manager.master";
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
        {
            Response.Redirect("~/default.aspx");
        }
        Campus camp = new Campus(); camp.LoadLoader(loader); 
        if (!IsPostBack)
        {
            if (Session["SessionName"].ToString() == "2015-2016" || Session["SessionName"].ToString() == "2016-2017")
            {
            }
            else
            {
                Response.Redirect("Studentwisecumulative_XI.aspx?check=ClassWiseStudentsMarksList_XI");
            }
            loadclass();
            loadsection();
            loadBranch();
            loadgrid();
        }
    }


    protected void drpClass_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadsection();
        loadBranch();
        loadgrid();
    }

   

    public void loadgrid()
    {
        sql = "select SG.Id, SC.SectionName,SO.Card,SO.Medium as Medium,CM.ClassName,convert(nvarchar,So.DateOfAdmiission,106) as DateOfAdmiission ,SO.SectionId,Sf.FatherName,SF.MotherName,(SG.FirstName+' '+SG.MiddleName+' '+SG.LastName) as Name,so.StEnRCode as StEnRCode,sg.srno  as srno,case  when so.TransportRequired='Yes' then 'Yes' else 'No' end as TransportRequired,so.wayamount as wayamount from StudentGenaralDetail SG ";
        sql +=  "   left join StudentFamilyDetails SF on SG.srno=SF.srno";
        sql +=  "   left join StudentOfficialDetails SO on SG.srno=SO.srno";
        sql +=  "   left join ClassMaster CM on SO.AdmissionForClassId=CM.Id";
        sql +=  "   left join SectionMaster SC on SO.SectionId=SC.Id";
        sql +=  "   where sg.SessionName='" + Session["SessionName"].ToString() + "' and ";
        sql +=  "   so.SessionName='" + Session["SessionName"].ToString() + "' and sf.SessionName='" + Session["SessionName"].ToString() + "' and cm.SessionName='" + Session["SessionName"].ToString() + "'";
        sql +=  "   and SC.SessionName='" + Session["SessionName"].ToString() + "' and so.BranchCode=" + Session["BranchCode"] + " and sc.BranchCode=" + Session["BranchCode"] + " and sf.BranchCode=" + Session["BranchCode"] + "  and";
        sql +=  "   sg.BranchCode=" + Session["BranchCode"].ToString() + " and (so.Branch='" + drpBranch.SelectedValue.ToString() + "' or so.Branch is null) ";
        sql +=  "   and SO.Withdrwal is null and SO.AdmissionForClassId='" + drpClass.SelectedValue.ToString() + "' and SO.SectionId='" + drpsection.SelectedValue.ToString() + "' order by SG.FirstName Asc";

        GridView1.DataSource = oo.GridFill(sql);
        GridView1.DataBind();
    }

    public void loadclass()
    {
        if (Session["Logintype"].ToString() == "Admin")
        {
            sql = "Select Distinct ClassName,cm.Id as Id,CIDOrder from ClassMaster cm ";
            sql +=  " inner join dt_ClassGroupMaster t1 on t1.ClassId=cm.Id and t1.SessionName=cm.SessionName";
            sql +=  " where cm.SessionName='" + Session["SessionName"] + "' and t1.BranchCode=" + Session["BranchCode"] + " and cm.BranchCode=" + Session["BranchCode"] + " and GroupId='G6' Order by CIDOrder";
            oo.FillDropDown_withValue(sql, drpClass, "ClassName", "Id");
        }
        else
        {
            if (Session["SessionName"].ToString() == "2015-2016")
            {
                sql = "Select Distinct ClassName,sctm.ClassId as Id,CIDOrder from SubjectClassTeacherMaster sctm";
                sql +=  " inner join ClassMaster cm on cm.Id=sctm.ClassId";
                sql +=  " where cm.SessionName='" + Session["SessionName"].ToString() + "' and sctm.SessionName='" + Session["SessionName"].ToString() + "' and Ecode='" + Session["LoginName"].ToString() + "' and ClassTeacher='Yes' Order by CIDOrder";
                oo.FillDropDown_withValue(sql, drpClass, "ClassName", "Id");


            }
            else
            {
                sql = "Select  Distinct ClassName,cm.Id,CIDOrder from ClassTeacherMaster T1";
                sql +=  " inner join ClassMaster cm on cm.Id=T1.ClassId and cm.SessionName=t1.SessionName";
                sql +=  " where EmpCode='" + Session["LoginName"].ToString() + "' and cm.BranchCode=" + Session["BranchCode"] + " and t1.BranchCode=" + Session["BranchCode"] + " and IsClassTeacher=1 and T1.SessionName='" + Session["SessionName"].ToString() + "' Order by CIDOrder";
                oo.FillDropDown_withValue(sql, drpClass, "ClassName", "Id");
            }       
        }
    
       
    }

    public void loadsection()
    {
        if (Session["Logintype"].ToString() == "Admin")
        {
            sql = "Select SectionName,Id from SectionMaster where ClassNameId='" + drpClass.SelectedValue.ToString() + "' and BranchCode=" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"].ToString() + "'";
            oo.FillDropDown_withValue(sql, drpsection, "SectionName", "Id");
            drpsection.Items.Insert(0, "<--Select-->");
        }
        else
        {
            if (Session["SessionName"].ToString() == "2015-2016")
            {
                sql = "Select SectionName,Id from SectionMaster where ClassNameId='" + drpClass.SelectedValue.ToString() + "' and BranchCode=" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"].ToString() + "'";
                oo.FillDropDown_withValue(sql, drpsection, "SectionName", "Id");
                drpsection.Items.Insert(0, "<--Select-->");

            }
            else
            {
                sql = "Select SectionName,sm.Id from ClassTeacherMaster T1";
                sql +=  " inner join SectionMaster sm on sm.Id=T1.SectionId and sm.SessionName=t1.SessionName";
                sql +=  " where EmpCode='" + Session["LoginName"].ToString() + "' and IsClassTeacher=1 and sm.BranchCode=" + Session["BranchCode"] + " and t1.BranchCode=" + Session["BranchCode"] + " and T1.SessionName='" + Session["SessionName"].ToString() + "'";
                sql +=  " and t1.Classid=" + drpClass.SelectedValue.ToString() + "";
                oo.FillDropDown_withValue(sql, drpsection, "SectionName", "Id");
                drpsection.Items.Insert(0, "<--Select-->");
            }
        }

    }

    private void loadBranch()
    {
        if (Session["Logintype"].ToString() == "Admin")
        {
            sql = "Select BranchName,Id from BranchMaster Where ClassId='" + drpClass.SelectedValue.ToString() + "' and BranchCode=" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"].ToString() + "'";
            oo.FillDropDown_withValue(sql, drpBranch, "BranchName", "Id");
            drpBranch.Items.Insert(0, new ListItem("<--Select-->", "0"));
        }
        else
        {
            if (Session["SessionName"].ToString() == "2015-2016")
            {
                sql = "Select  Distinct BranchName,Id from BranchMaster Where ClassId='" + drpClass.SelectedValue.ToString() + "' and BranchCode=" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"].ToString() + "'";
                oo.FillDropDown_withValue(sql, drpBranch, "BranchName", "Id");
                drpBranch.Items.Insert(0, new ListItem("<--Select-->", "0"));
            }
            else
            {
                sql = "Select  Distinct BranchName,bm.Id from ClassTeacherMaster T1";
                sql +=  "   inner join BranchMaster bm on bm.Id=T1.BranchId and bm.SessionName=t1.SessionName";
                sql +=  "   where EmpCode='" + Session["LoginName"].ToString() + "' and IsClassTeacher=1 and bm.BranchCode=" + Session["BranchCode"] + " and t1.BranchCode=" + Session["BranchCode"] + " and T1.SessionName='" + Session["SessionName"] + "' and T1.Classid='" + drpClass.SelectedValue.ToString() + "'";
                oo.FillDropDown_withValue(sql, drpBranch, "BranchName", "Id");
                drpBranch.Items.Insert(0, new ListItem("<--Select-->", "0"));
            }
        }
    }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        LinkButton lnk = (LinkButton)sender;
        GridViewRow currentrow = (GridViewRow)lnk.NamingContainer;

        Label srno = (Label)currentrow.FindControl("Label2");
        Session["srno"] = srno.Text;
        Session["ClassId"] = drpClass.SelectedValue.ToString();
        Session["BranchId"] = drpBranch.SelectedValue.ToString();
        Session["ClassName"] = drpClass.SelectedItem.ToString();
        Session["SectionName"] = drpsection.SelectedItem.ToString();
        Session["SectionId"] = drpsection.SelectedValue.ToString();
        if (RadioButtonList1.SelectedIndex == 0)
        {
            Response.Write("<script>");
            Response.Write("window.open('ReportCardofXI.aspx?print=1','_blank')");
            Response.Write("</script>");
        }
        else
        {
            Response.Write("<script>");
            Response.Write("window.open('ParticularStudentMarksXI.aspx?print=1','_blank')");
            Response.Write("</script>");
        }

    }
    protected void drpsection_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadgrid();
    }
    protected void drpBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadgrid();
    }

    protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (RadioButtonList1.SelectedIndex == 0)
        {
            Response.Redirect("PrintReportCardXI.aspx?print=1");
        }
    }
}