using System;
using System.Web.UI.WebControls;

public partial class admin_ClassWiseStudentsMarksList_ItoV : System.Web.UI.Page
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
                Response.Redirect("Studentwisecumulative_NurtoPrep.aspx?check=ClassWiseStudentsMarksList_NUR1toPrep");
            }
            loadclass();
            loadsection();
            loadgrid();
        }
    }


    protected void drpClass_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadsection();
        loadgrid();
    }

    public void loadgrid()
    {
        sql = "select SG.Id, SC.SectionName,SO.Card,SO.Medium as Medium,CM.ClassName,convert(nvarchar,So.DateOfAdmiission,106) as DateOfAdmiission ,SO.SectionId,Sf.FatherName,SF.MotherName,(SG.FirstName+' '+SG.MiddleName+' '+SG.LastName) as Name,so.StEnRCode as StEnRCode,sg.srno  as srno,case  when so.TransportRequired='Yes' then 'Yes' else 'No' end as TransportRequired,so.wayamount as wayamount from StudentGenaralDetail SG ";
        sql +=  "    left join StudentFamilyDetails SF on SG.srno=SF.srno";
        sql +=  "   left join StudentOfficialDetails SO on SG.srno=SO.srno";
        sql +=  "   left join ClassMaster CM on SO.AdmissionForClassId=CM.Id";
        sql +=  "   left join SectionMaster SC on SO.SectionId=SC.Id";
        sql +=  "    where sg.SessionName='" + Session["SessionName"].ToString() + "' and ";
        sql +=  "     so.SessionName='" + Session["SessionName"].ToString() + "' and cm.BranchCode=" + Session["BranchCode"] + " and sc.BranchCode=" + Session["BranchCode"] + " and sg.BranchCode=" + Session["BranchCode"] + " and so.BranchCode=" + Session["BranchCode"] + " and sf.BranchCode=" + Session["BranchCode"] + " and sf.SessionName='" + Session["SessionName"].ToString() + "' and cm.SessionName='" + Session["SessionName"].ToString() + "'";
        sql +=  "    and SC.SessionName='" + Session["SessionName"].ToString() + "'  and";
        sql +=  " sg.BranchCode=" + Session["BranchCode"].ToString() + "";
        sql +=  "   and SO.Withdrwal is null and SO.AdmissionForClassId='" + drpClass.SelectedValue.ToString() + "' and SO.SectionId='" + drpsection.SelectedValue.ToString() + "' order by Name Asc";

        GridView1.DataSource = oo.GridFill(sql);
        GridView1.DataBind();
    }



    //For Staff--------------------------------------------------------------------------------------------------------------
    public void loadclass()
    {
        if (Session["Logintype"].ToString() == "Admin")
        {
            sql = "Select Distinct ClassName,cm.Id as Id,CIDOrder from ClassMaster cm ";
            sql +=  " inner join dt_ClassGroupMaster t1 on t1.ClassId=cm.Id";
            sql +=  " where cm.SessionName='" + Session["SessionName"] + "' and t1.BranchCode=" + Session["BranchCode"] + " and cm.BranchCode=" + Session["BranchCode"] + " and GroupId='G2' Order by CIDOrder";
            oo.FillDropDown_withValue(sql, drpClass, "ClassName", "Id");
        }
        else
        {
            if (Session["SessionName"].ToString() == "2015-2016")
            {
                sql = "Select Distinct ClassName,sctm.ClassId as Id,CIDOrder from SubjectClassTeacherMaster sctm";
                sql +=  " inner join ClassMaster cm on cm.Id=sctm.ClassId";
                sql +=  " where cm.SessionName='" + Session["SessionName"] + "' and cm.BranchCode=" + Session["BranchCode"] + " and sctm.BranchCode=" + Session["BranchCode"] + " and sctm.SessionName='" + Session["SessionName"].ToString() + "' and Ecode='" + Session["LoginName"].ToString() + "' and ClassTeacher='Yes' Order by CIDOrder";
                oo.FillDropDown_withValue(sql, drpClass, "ClassName", "Id");
            }
            else
            {
                sql = "Select cm.ClassName,cm.Id from ClassTeacherMaster ctm ";
                sql +=  " inner join ClassMaster cm on cm.Id=ctm.ClassId and cm.SessionName=ctm.SessionName";
                sql +=  " where EmpCode='" + Session["LoginName"].ToString() + "' and cm.BranchCode=" + Session["BranchCode"] + " and ctm.BranchCode=" + Session["BranchCode"] + " and ctm.SessionName='" + Session["SessionName"].ToString() + "' and ctm.BranchCode=" + Session["BranchCode"] + " and IsClassTeacher=1";
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
                sql +=  " where EmpCode='" + Session["LoginName"].ToString() + "' and IsClassTeacher=1 and T1.SessionName='" + Session["SessionName"].ToString() + "'";
                sql +=  " and t1.Classid=" + drpClass.SelectedValue.ToString() + " and sm.BranchCode=" + Session["BranchCode"] + " and t1.BranchCode=" + Session["BranchCode"] + "";
                oo.FillDropDown_withValue(sql, drpsection, "SectionName", "Id");
                drpsection.Items.Insert(0, "<--Select-->");
            }
        }
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        LinkButton lnk = (LinkButton)sender;
        Label srno = (Label)lnk.NamingContainer.FindControl("Label2");
        
        Session["srno"] = srno.Text;
        Session["ClassId"] = drpClass.SelectedValue.ToString();
        Session["ClassName"] = drpClass.SelectedItem.ToString();
        Session["SectionName"] = drpsection.SelectedItem.ToString();

        if (RadioButtonList1.SelectedIndex == 0)
        {
            Response.Write("<script>");
            Response.Write("var winpop=window.open('ReportCardofNURtoPREP.aspx?print=1','_blank'); if(!winpop || winpop.closed){alert('Please allow pop-up blocker from browser settings!')}");
            
            Response.Write("</script>");
        }
        else
        {
            Response.Write("<script>");
            Response.Write("var winpop=window.open('ParticularStudentMarksNUR1toPrep.aspx?print=1','_blank'); if(!winpop || winpop.closed){alert('Please allow pop-up blocker from browser settings!')}");
            Response.Write("</script>");
        }
    }
    protected void drpsection_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadgrid();
    }
}