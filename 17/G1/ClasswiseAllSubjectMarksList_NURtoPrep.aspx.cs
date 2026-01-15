using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class staff_ClasswiseAllSubjectMarksList : Page
{
    string sql="";
    Campus oo = new Campus();
    protected void Page_PreInIt(object sender, EventArgs e)
    {
        if (Session["Logintype"] == null) { Response.Redirect("~/default.aspx"); }
        if (Session["Logintype"].ToString() == "Admin")
        {
            this.MasterPageFile = "~/Master/admin_root-manager.master";
        }
        else if (Session["Logintype"].ToString() != "Staff")
        {
            this.MasterPageFile = "~/Staff/staff_root-manager.master";
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        Campus camp = new Campus(); camp.LoadLoader(loader);
        BLL.BLLInstance.LoadHeader("Examination", header);
        if (Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
        {
            Response.Redirect("~/default.aspx");
        }
        if (!IsPostBack)
        {
            if (Session["SessionName"].ToString() == "2016-2017" || Session["SessionName"].ToString() == "2015-2016")
            {
            }
            else
            {
                Response.Redirect("ClasswiseAllSubjectMarksListNurtoPrep_1718.aspx?check=ClasswiseAllSubjectMarksList_NURtoPrep");
            }
            //Image2.ImageUrl = ResolveClientUrl("~/DisplayImage.ashx?UserLoginID=" + 1);
            sql = "Select CollegeName from CollegeMaster where CollegeId=" + 1;
            //lblCollegeName.Text = oo.ReturnTag(sql, "CollegeName");
            Label1.Text = Label1.Text + " " + Session["SessionName"].ToString();
            loadclass();
            loadsection();
        }
    }
    public void loadclass()
    {
        if (Session["Logintype"].ToString() == "Admin")
        {
            sql = "Select Distinct ClassName,cm.Id as Id,CIDOrder from ClassMaster cm ";
            sql +=  " inner join dt_ClassGroupMaster t1 on t1.ClassId=cm.Id";
            sql +=  " where cm.SessionName='" + Session["SessionName"] + "' and t1.BranchCode=" + Session["BranchCode"] + " and cm.BranchCode=" + Session["BranchCode"] + " and GroupId='G1' Order by CIDOrder";
            oo.FillDropDown_withValue(sql, drpclass, "ClassName", "Id");

        }
        else
        {
            if (Session["SessionName"].ToString() == "2015-2016")
            {
                sql = "Select Distinct ClassName,sctm.ClassId as Id,CIDOrder from SubjectClassTeacherMaster sctm";
                sql +=  " inner join ClassMaster cm on cm.Id=sctm.ClassId";
                sql +=  " inner join dt_ClassGroupMaster t1 on t1.ClassId=sctm.ClassId";
                sql +=  " where GroupId='G1' and cm.SessionName='" + Session["SessionName"] + "' and cm.BranchCode=" + Session["BranchCode"] + " and t1.BranchCode=" + Session["BranchCode"] + " and sctm.BranchCode=" + Session["BranchCode"] + " and sctm.SessionName='" + Session["SessionName"] + "' and Ecode='" + Session["LoginName"].ToString() + "' and ClassTeacher='Yes' Order by CIDOrder";
                oo.FillDropDown_withValue(sql, drpclass, "ClassName", "Id");
            }
            else
            {
                sql = "Select ClassName,cm.Id from ClassTeacherMaster T1";
                sql +=  " inner join ClassMaster cm on cm.Id=T1.ClassId and cm.SessionName=t1.SessionName";
                sql +=  " inner join dt_ClassGroupMaster T2 on T2.ClassId=T1.ClassId and cm.SessionName=T2.SessionName";
                sql +=  " where GroupId='G1' and EmpCode='" + Session["LoginName"].ToString() + "' and cm.BranchCode=" + Session["BranchCode"] + " and t2.BranchCode=" + Session["BranchCode"] + " and t1.BranchCode=" + Session["BranchCode"] + " and IsClassTeacher=1 and T1.SessionName='" + Session["SessionName"] + "' Order by CIDOrder";
                oo.FillDropDown_withValue(sql, drpclass, "ClassName", "Id");
            }
        }
    }

    //public void loadclass()
    //{
    //    sql = "Select Distinct ClassName,cm.Id as Id,CIDOrder from ClassMaster cm ";
    //    sql +=  " inner join dt_ClassGroupMaster t1 on t1.ClassId=cm.Id";
    //    sql +=  " where cm.SessionName='" + Session["SessionName"] + "' and (GroupId='G1' or GroupId='G2') Order by CIDOrder";
    //    oo.FillDropDown_withValue(sql, drpclass, "ClassName", "Id");
    //}

    public void loadsection()
    {
        sql = "Select SectionName,Id from SectionMaster where ClassNameId='" + drpclass.SelectedValue.ToString() + "' and BranchCode=" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"].ToString() + "'";
        oo.FillDropDown_withValue(sql, drpsection, "SectionName", "Id");
        drpsection.Items.Insert(0, "<--Select-->");
    }
    protected void loadClassName()
    {
        sql = "Select ClassName from ClassMaster where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " and Id='" + drpclass.SelectedValue.ToString() + "'";
        GridView1.DataSource = oo.GridFill(sql);
        GridView1.DataBind();

        sql = "Select Left(SubjectGroup,9) as SubjectGroup,Id from SubjectGroupMaster where (IsForOnlyExam=1 or IsForOnlyExam is null) and BranchCode=" + Session["BranchCode"] + " and  Classid='" + drpclass.SelectedValue.ToString() + "' and SessionName='" + Session["SessionName"].ToString() + "'";
        DataTable dt;
        dt = oo.Fetchdata(sql);
       
        //DataList1.DataSource = dt;
        //DataList1.DataBind();
        Repeater1.DataSource = dt;
        Repeater1.DataBind();
        Repeater2.DataSource = dt;
        Repeater2.DataBind();
    }
    protected void loadSubject()
    {
        sql = "select SG.Id, SC.SectionName,SO.Card,SO.Medium as Medium,CM.ClassName,convert(nvarchar,So.DateOfAdmiission,106) as DateOfAdmiission ,SO.SectionId,Sf.FatherName,SF.MotherName,(UPPER(Left(SG.FirstName,1))+Lower(Substring((SG.FirstName+' '+SG.MiddleName+' '+SG.LastName),2,LEN(SG.FirstName+' '+SG.MiddleName+' '+SG.LastName)))) as StudentName,so.StEnRCode as StEnRCode,sg.srno  as srno,case  when so.TransportRequired='Yes' then 'Yes' else 'No' end as TransportRequired,so.wayamount as wayamount from StudentGenaralDetail SG ";
        sql +=  "    left join StudentFamilyDetails SF on SG.srno=SF.srno";
        sql +=  "   left join StudentOfficialDetails SO on SG.srno=SO.srno";
        sql +=  "   left join ClassMaster CM on SO.AdmissionForClassId=CM.Id";
        sql +=  "   left join SectionMaster SC on SO.SectionId=SC.Id";
        sql +=  "    where sc.SectionName='" + drpsection.SelectedItem.Text + "' and sg.SessionName='" + Session["SessionName"].ToString() + "' and ";
        sql +=  "     so.SessionName='" + Session["SessionName"].ToString() + "' and sf.SessionName='" + Session["SessionName"].ToString() + "' and cm.SessionName='" + Session["SessionName"].ToString() + "'";
        sql +=  "    and SC.SessionName='" + Session["SessionName"].ToString() + "'  and";
        sql +=  " sg.BranchCode=" + Session["BranchCode"].ToString() + "";
        sql +=  "   and SO.Withdrwal is null and SO.Promotion is null and sc.BranchCode=" + Session["BranchCode"] + " and cm.BranchCode=" + Session["BranchCode"] + " and sg.BranchCode=" + Session["BranchCode"] + " and sf.BranchCode=" + Session["BranchCode"] + " and so.BranchCode=" + Session["BranchCode"] + " and SO.AdmissionForClassId='" + drpclass.SelectedValue.ToString() + "' order by sg.FirstName";

        GridView GridView3 = (GridView)GridView1.Rows[0].FindControl("GridView3");
        GridView GridView2 = (GridView)GridView1.Rows[0].FindControl("GridView2");
        GridView3.DataSource = oo.GridFill(sql);
        GridView3.DataBind();
        GridView2.DataSource = oo.GridFill(sql);
        GridView2.DataBind();
    }
    public void loadStudentData()
    {
        if (drpsection.SelectedIndex != 0)
        {
            loadClassName();
            loadSubject();
            for (int k = 0; k < Repeater1.Items.Count; k++)
            {
                Label lblSubjectGroupId = (Label)Repeater1.Items[k].FindControl("Label4");
                if (drpclass.SelectedItem.Text.Contains("Play Group"))
                {
                    sql = "Select so.SrNo,Sum(case when ISNUMERIC(cv.Test1)=1 then cv.Test1 Else 0 End),sg.FirstName from CCEPG CV ";
                    sql +=  " left join StudentOfficialDetails so on cv.SrNo=so.SrNo";
                    sql +=  " left join StudentGenaralDetail sg on cv.SrNo=sg.SrNo";
                
                        sql +=  " inner join SectionMaster scm on scm.Id=so.SectionId";
                   
                    sql +=  " inner join SubjectMaster sm on sm.ClassId=cv.ClassId and sm.Id=CV.SubjectId";
                    sql +=  " inner join SubjectGroupMaster sgm on sgm.Id=sm.GroupId";
                    sql +=  " where scm.SectionName='" + drpsection.SelectedItem.Text + "' and so.SessionName='" + Session["SessionName"].ToString() + "' and sg.SessionName='" + Session["SessionName"].ToString() + "' and cv.SessionName='" + Session["SessionName"].ToString() + "' and sgm.id='" + lblSubjectGroupId.Text + "'";
                    sql +=  " and sm.SessionName='" + Session["SessionName"].ToString() + "' and sgm.SessionName='" + Session["SessionName"].ToString() + "' and cv.Evaluation='" + drpEval.SelectedItem.Text.Trim() + "'";
                   
                        sql +=  " and scm.SessionName='" + Session["SessionName"].ToString() + "' and sg.BranchCode=" + Session["BranchCode"] + " and sm.BranchCode=" + Session["BranchCode"] + " and sgm.BranchCode=" + Session["BranchCode"] + " and cv.BranchCode=" + Session["BranchCode"] + "";
                   
                    sql +=  " and so.AdmissionForClassId='" + drpclass.SelectedValue.ToString() + "' group by so.SrNo,sg.FirstName order by sg.FirstName";
                }
                else
                {
                    sql = "Select so.SrNo,Sum(case when ISNUMERIC(cv.Test1)=1 then cv.Test1 Else 0 End),sg.FirstName from CCEPGtoPrep CV ";
                    sql +=  " left join StudentOfficialDetails so on cv.SrNo=so.SrNo";
                    sql +=  " left join StudentGenaralDetail sg on cv.SrNo=sg.SrNo";
                    if (drpsection.SelectedValue == "0")
                    {
                        sql +=  " inner join SectionMaster scm on scm.Id=so.SectionId";
                    }
                    sql +=  " inner join SubjectMaster sm on sm.ClassId=cv.ClassId and sm.Id=CV.SubjectId";
                    sql +=  " inner join SubjectGroupMaster sgm on sgm.Id=sm.GroupId";
                    sql +=  " where so.SessionName='" + Session["SessionName"].ToString() + "' and scm.BranchCode=" + Session["BranchCode"] + " and sm.BranchCode=" + Session["BranchCode"] + " and cv.BranchCode=" + Session["BranchCode"] + " and sgm.BranchCode=" + Session["BranchCode"] + " and sg.SessionName='" + Session["SessionName"].ToString() + "' and cv.SessionName='" + Session["SessionName"].ToString() + "' and sgm.id='" + lblSubjectGroupId.Text + "'";
                    sql +=  " and sm.SessionName='" + Session["SessionName"].ToString() + "' and sgm.SessionName='" + Session["SessionName"].ToString() + "' and cv.Evaluation='" + drpEval.SelectedItem.Text.Trim() + "'";
                    if (drpsection.SelectedValue == "0")
                    {
                        sql +=  " and scm.SessionName='" + Session["SessionName"].ToString() + "'";
                    }
                    sql +=  " and so.AdmissionForClassId='" + drpclass.SelectedValue.ToString() + "' group by so.SrNo,sg.FirstName order by sg.FirstName";

                }
                DataTable dt;
                dt = oo.Fetchdata(sql);
                if (dt.Rows.Count > 0)
                {
                    sql = "Select so.SrNo from StudentOfficialDetails so ";
                    if (drpsection.SelectedValue != "0")
                    {
                        sql +=  " inner join SectionMaster scm on scm.Id=so.SectionId";
                    }
                    sql +=  " where so.SessionName='" + Session["SessionName"].ToString() + "'";
                    if (drpsection.SelectedValue != "0")
                    {
                        sql +=  " and scm.SessionName='" + Session["SessionName"].ToString() + "' and scm.BranchCode=" + Session["BranchCode"] + " and so.BranchCode=" + Session["BranchCode"] + "";
                    }
                    sql +=  " and so.AdmissionForClassId='" + drpclass.SelectedValue.ToString() + "'";

                    sql +=  " Except";
                    if (drpclass.SelectedItem.Text.Contains("PG"))
                    {
                        sql +=  " Select so.SrNo from CCEPG CV ";
                        sql +=  " left join StudentOfficialDetails so on cv.SrNo=so.SrNo";
                        if (drpsection.SelectedValue != "0")
                        {
                            sql +=  " inner join SectionMaster scm on scm.Id=so.SectionId";
                        }
                        sql +=  " inner join SubjectMaster sm on sm.ClassId=cv.ClassId and sm.Id=CV.SubjectId";
                        sql +=  " inner join SubjectGroupMaster sgm on sgm.Id=sm.GroupId";
                        sql +=  " where so.SessionName='" + Session["SessionName"].ToString() + "' and scm.BranchCode=" + Session["BranchCode"] + " and sgm.BranchCode=" + Session["BranchCode"] + " and sm.BranchCode=" + Session["BranchCode"] + " and cv.BranchCode=" + Session["BranchCode"] + " and cv.SessionName='" + Session["SessionName"].ToString() + "' and sgm.id='" + lblSubjectGroupId.Text + "'";
                        sql +=  " and sm.SessionName='" + Session["SessionName"].ToString() + "' and sgm.SessionName='" + Session["SessionName"].ToString() + "' and cv.Evaluation='" + drpEval.SelectedItem.Text.Trim() + "'";
                        if (drpsection.SelectedValue != "0")
                        {
                            sql +=  " and scm.SessionName='" + Session["SessionName"].ToString() + "'";
                        }
                        sql +=  " and so.AdmissionForClassId='" + drpclass.SelectedValue.ToString() + "'";
                    }
                    else
                    {
                        sql +=  " Select so.SrNo from CCEPGtoPrep CV ";
                        sql +=  " left join StudentOfficialDetails so on cv.SrNo=so.SrNo";
                        if (drpsection.SelectedValue != "0")
                        {
                            sql +=  " inner join SectionMaster scm on scm.Id=so.SectionId";
                        }
                        sql +=  " inner join SubjectMaster sm on sm.ClassId=cv.ClassId and sm.Id=CV.SubjectId";
                        sql +=  " inner join SubjectGroupMaster sgm on sgm.Id=sm.GroupId";
                        sql +=  " where so.SessionName='" + Session["SessionName"].ToString() + "' and scm.BranchCode=" + Session["BranchCode"] + " and sgm.BranchCode=" + Session["BranchCode"] + " and sm.BranchCode=" + Session["BranchCode"] + " and cv.BranchCode=" + Session["BranchCode"] + " and cv.SessionName='" + Session["SessionName"].ToString() + "' and sgm.id='" + lblSubjectGroupId.Text + "'";
                        sql +=  " and sm.SessionName='" + Session["SessionName"].ToString() + "' and sgm.SessionName='" + Session["SessionName"].ToString() + "' and cv.Evaluation='" + drpEval.SelectedItem.Text.Trim() + "'";
                        if (drpsection.SelectedValue != "0")
                        {
                            sql +=  " and scm.SessionName='" + Session["SessionName"].ToString() + "'";
                        }
                        sql +=  " and so.AdmissionForClassId='" + drpclass.SelectedValue.ToString() + "'";
                    }
                    DataTable dt1;
                    dt1 = oo.Fetchdata(sql);
                    for (int l = 0; l < dt1.Rows.Count; l++)
                    {
                        dt.Rows.Add(dt1.Rows[l][0].ToString(), 0, "");
                    }
                }
                GridView GridView2 = (GridView)GridView1.Rows[0].FindControl("GridView2");
                int changecolour = 0;
                for (int j = 0; j < GridView2.Rows.Count; j++)
                {
                    Label Label2 = (Label)GridView2.Rows[j].FindControl("Label2");
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            if (Label2.Text == dt.Rows[i][0].ToString())
                            {
                                TableCell tc = new TableCell();
                                tc.Text = dt.Rows[i][1].ToString();
                                Unit width = new Unit(30, UnitType.Pixel);
                                tc.CssClass = "p-pad-n text-center sub-m-w-85";
                                //tc.HorizontalAlign = HorizontalAlign.Center;
                                GridView2.Rows[j].Cells.Add(tc);
                                if (k % 2 != 0)
                                {
                                    tc.BackColor = System.Drawing.Color.LightGray;
                                }
                                changecolour = changecolour + 1;
                                break;
                            }
                        }
                    }
                    else
                    {
                        TableCell tc = new TableCell();
                        tc.Text = "";
                        Unit width = new Unit(30, UnitType.Pixel);
                        //tc.Width = width;
                        tc.CssClass = "p-pad-n text-center sub-m-w-85";
                        GridView2.Rows[j].Cells.Add(tc);
                        if (k % 2 != 0)
                        {
                            tc.BackColor = System.Drawing.Color.LightGray;
                        }
                        changecolour = changecolour + 1;
                    }
                }
            }
        }
    }
    protected void drpclass_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadsection();
        loadStudentData();
        Label2.Text = "Subject Wise Cumulative " + drpclass.SelectedItem.Text + "(" + drpsection.SelectedItem.Text + ")" + "/" + drpEval.SelectedItem.ToString();
    }
    protected void drpsection_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadStudentData();
        Label2.Text = "Subject Wise Cumulative " + drpclass.SelectedItem.Text + "(" + drpsection.SelectedItem.Text + ")" + "/" + drpEval.SelectedItem.ToString();
           
    }
    protected void drpEval_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (drpsection.SelectedIndex != 0)
        {
            loadStudentData();
            Label2.Text = "Subject Wise Cumulative " + drpclass.SelectedItem.Text + "(" + drpsection.SelectedItem.Text + ")" + "/" + drpEval.SelectedItem.ToString();
        }
        else
        {
            //oo.MessageBoxforUpdatePanel("Please, select section!",drpsection);
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Please, select section!", "A");       

        }
    }

 

    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }
    protected void Repeater2_ItemCreated(object sender, RepeaterItemEventArgs e)
    {
        colsp.ColSpan = Repeater2.Items.Count + 3;
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        
    }
    protected void LinkButton2_Click(object sender, EventArgs e)
    {
      

    }

    protected void ImageButton1_Click(object sender, EventArgs e)
    {
        if (GridView1.Rows.Count > 0)
        {
            loadStudentData();
            oo.ExportToWord(Response, Session["srno"] + "Report" + ".doc", divExport);
        }
    }
    protected void ImageButton2_Click(object sender, EventArgs e)
    {
        loadStudentData();
        BAL.objBal.ExportDivToExcel(Response, "SubjectWiseCumulative.xls", divExport);

    }
    protected void ImageButton4_Click(object sender, EventArgs e)
    {
        loadStudentData();
        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "colsp", "", true);
        PrintHelper_New.ctrl = abc;
        ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>var winpop=window.open('../Print_New.aspx','_blank'); if(!winpop || winpop.closed){alert('Please allow pop-up blocker from browser settings!')}</script>");
    
    }
}