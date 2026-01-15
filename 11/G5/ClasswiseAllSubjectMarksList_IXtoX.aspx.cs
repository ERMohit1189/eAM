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
        if (Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
        {
            Response.Redirect("~/default.aspx");
        }
        if (!IsPostBack)
        {
            if (Session["SessionName"].ToString() == "2016-2017" || Session["SessionName"].ToString() == "2015-2016")
            {
                Response.Redirect("ClasswiseAllSubjectMarksListIXtoX_1617.aspx?check=ClasswiseAllSubjectMarksList_IXtoX");
            }
            else
            {
                Response.Redirect("ClasswiseAllSubjectMarksListIXtoX_1718.aspx?check=ClasswiseAllSubjectMarksList_IXtoX");
            }
            Image2.ImageUrl = ResolveClientUrl("~/DisplayImage.ashx?UserLoginID=" + 1);
            sql = "Select CollegeName from CollegeMaster where CollegeId=" + 1;
            lblCollegeName.Text = oo.ReturnTag(sql, "CollegeName");
            Label1.Text = Label1.Text + " " + Session["SessionName"].ToString();
            loadclass();
            loadsection();
            Label2.Text = Label2.Text + drpclass.SelectedItem.Text + "(" + drpsection.SelectedItem.Text + ")" + "/" + drpEval.SelectedItem.ToString();
        }
    }
    public void loadclass()
    {
        if (Session["Logintype"].ToString() == "Admin")
        {
            sql = "Select Distinct ClassName,cm.Id as Id,CIDOrder from ClassMaster cm ";
            sql +=  " inner join dt_ClassGroupMaster t1 on t1.ClassId=cm.Id";
            sql +=  " where cm.SessionName='" + Session["SessionName"] + "' and GroupId='G5' Order by CIDOrder";
            oo.FillDropDown_withValue(sql, drpclass, "ClassName", "Id");

        }
        else
        {
            if (Session["SessionName"].ToString() == "2015-2016")
            {
                sql = "Select Distinct ClassName,sctm.ClassId as Id,CIDOrder from SubjectClassTeacherMaster sctm";
                sql +=  " inner join ClassMaster cm on cm.Id=sctm.ClassId";
                sql +=  " inner join dt_ClassGroupMaster t1 on t1.ClassId=sctm.ClassId";
                sql +=  " where GroupId='G5' and cm.SessionName='" + Session["SessionName"] + "' and sctm.SessionName='" + Session["SessionName"] + "' and Ecode='" + Session["LoginName"].ToString() + "' and ClassTeacher='Yes' Order by CIDOrder";
                oo.FillDropDown_withValue(sql, drpclass, "ClassName", "Id");
            }
            else
            {
                sql = "Select ClassName,cm.Id from ClassTeacherMaster T1";
                sql +=  " inner join ClassMaster cm on cm.Id=T1.ClassId and cm.SessionName=t1.SessionName";
                sql +=  " inner join dt_ClassGroupMaster T2 on T2.ClassId=T1.ClassId and cm.SessionName=T2.SessionName";
                sql +=  " where GroupId='G5' and EmpCode='" + Session["LoginName"].ToString() + "' and IsClassTeacher=1 and T1.SessionName='" + Session["SessionName"] + "' Order by CIDOrder";
                oo.FillDropDown_withValue(sql, drpclass, "ClassName", "Id");
            }
        }
    }

    public void loadsection()
    {
        sql = "Select SectionName,Id from SectionMaster where ClassNameId='" + drpclass.SelectedValue.ToString() + "' and SessionName='" + Session["SessionName"].ToString() + "'";
        oo.FillDropDown_withValue(sql, drpsection, "SectionName", "Id");
        drpsection.Items.Insert(0, "<--Select-->");
    }
    protected void loadClassName()
    {
        sql = "Select ClassName from ClassMaster where SessionName='" + Session["SessionName"] + "' and Id='" + drpclass.SelectedValue.ToString() + "'";
        GridView1.DataSource = oo.GridFill(sql);
        GridView1.DataBind();

        sql = "Select SubjectGroup,Id from SubjectGroupMaster where Classid='" + drpclass.SelectedValue.ToString() + "' and SectionName='"+drpsection.SelectedItem.ToString()+"'";
        DataTable dt;
        dt = oo.Fetchdata(sql);

        Repeater1.DataSource = dt;
        Repeater1.DataBind();
        Repeater2.DataSource = dt;
        Repeater2.DataBind();
    }
    protected void loadSubject()
    {
        sql = "select SG.Id, SC.SectionName,SO.Card,SO.Medium as Medium,CM.ClassName,convert(nvarchar,So.DateOfAdmiission,106) as DateOfAdmiission ,SO.SectionId,Sf.FatherName,SF.MotherName,(SG.FirstName+' '+SG.MiddleName+' '+SG.LastName) as StudentName,so.StEnRCode as StEnRCode,sg.srno  as srno,case  when so.TransportRequired='Yes' then 'Yes' else 'No' end as TransportRequired,so.wayamount as wayamount from StudentGenaralDetail SG ";
        sql +=  "    left join StudentFamilyDetails SF on SG.srno=SF.srno";
        sql +=  "   left join StudentOfficialDetails SO on SG.srno=SO.srno";
        sql +=  "   left join ClassMaster CM on SO.AdmissionForClassId=CM.Id";
        sql +=  "   left join SectionMaster SC on SO.SectionId=SC.Id";
        sql +=  "    where sc.SectionName='"+drpsection.SelectedItem.Text+"' and  sg.SessionName='" + Session["SessionName"].ToString() + "' and ";
        sql +=  "     so.SessionName='" + Session["SessionName"].ToString() + "' and sf.SessionName='" + Session["SessionName"].ToString() + "' and cm.SessionName='" + Session["SessionName"].ToString() + "'";
        sql +=  "    and SC.SessionName='" + Session["SessionName"].ToString() + "'  and";
        sql +=  " sg.BranchCode=" + Session["BranchCode"].ToString() + "";
        sql +=  "   and SO.Withdrwal is null and SO.AdmissionForClassId='" + drpclass.SelectedValue.ToString() + "' order by sg.FirstName";

        GridView GridView3 = (GridView)GridView1.Rows[0].FindControl("GridView3");
        GridView GridView2 = (GridView)GridView1.Rows[0].FindControl("GridView2");
        GridView3.DataSource = oo.GridFill(sql);
        GridView3.DataBind();
        GridView2.DataSource = oo.GridFill(sql);
        GridView2.DataBind();
    }
    public void loadStudentData()
    {
        loadClassName();
        loadSubject();
        if (drpEval.SelectedItem.Text == "FA1")
        {
            loadStudentDataForFA1();
        }
        else if (drpEval.SelectedItem.Text == "FA2" || drpEval.SelectedItem.Text == "FA3" || drpEval.SelectedItem.Text == "FA4")
        {
            loadStudentDataForFA2_Fa3_Fa4();
        }
        else
        {
            loadStudentDataForSA1_SA2();
        }
        
    }
    protected void loadStudentDataForFA1()
    {
        for (int k = 0; k < Repeater1.Items.Count; k++)
        {
            Label lblSubjectGroupId = (Label)Repeater1.Items[k].FindControl("Label4");

            sql = "Select so.SrNo,(Case When (cv.UT='' or cv.UT is Null) Then 'x' Else cv.UT End) as UT,sg.FirstName from CCE CV ";
            sql +=  " left join StudentOfficialDetails so on cv.SrNo=so.SrNo";
            sql +=  " left join StudentGenaralDetail sg on cv.SrNo=sg.SrNo";
           
                sql +=  " inner join SectionMaster scm on scm.Id=so.SectionId";
           
            sql +=  " inner join SubjectMaster sm on sm.ClassId=cv.ClassId and sm.Id=CV.SubjectId";
            sql +=  " inner join SubjectGroupMaster sgm on sgm.Id=sm.GroupId";
            sql +=  " where scm.SectionName='" + drpsection.SelectedItem.Text + "' and so.SessionName='" + Session["SessionName"].ToString() + "' and sg.SessionName='" + Session["SessionName"].ToString() + "' and cv.SessionName='" + Session["SessionName"].ToString() + "' and sgm.id='" + lblSubjectGroupId.Text + "'";
            sql +=  " and sm.SessionName='" + Session["SessionName"].ToString() + "' and sgm.SessionName='" + Session["SessionName"].ToString() + "' and cv.Evaluation='" + drpEval.SelectedItem.Text.Trim() + "'";
           
                sql +=  " and scm.SessionName='" + Session["SessionName"].ToString() + "'";
           
            sql +=  " and so.AdmissionForClassId='" + drpclass.SelectedValue.ToString() + "' order by sg.FirstName";

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
                    sql +=  " and scm.SessionName='" + Session["SessionName"].ToString() + "'";
                }
                sql +=  " and so.AdmissionForClassId='" + drpclass.SelectedValue.ToString() + "'";

                sql +=  " Except";

                sql +=  " Select so.SrNo from CCE CV ";
                sql +=  " left join StudentOfficialDetails so on cv.SrNo=so.SrNo";
                if (drpsection.SelectedValue != "0")
                {
                    sql +=  " inner join SectionMaster scm on scm.Id=so.SectionId";
                }
                sql +=  " inner join SubjectMaster sm on sm.ClassId=cv.ClassId and sm.Id=CV.SubjectId";
                sql +=  " inner join SubjectGroupMaster sgm on sgm.Id=sm.GroupId";
                sql +=  " where so.SessionName='" + Session["SessionName"].ToString() + "' and cv.SessionName='" + Session["SessionName"].ToString() + "' and sgm.id='" + lblSubjectGroupId.Text + "'";
                sql +=  " and sm.SessionName='" + Session["SessionName"].ToString() + "' and sgm.SessionName='" + Session["SessionName"].ToString() + "' and cv.Evaluation='" + drpEval.SelectedItem.Text.Trim() + "'";
                if (drpsection.SelectedValue != "0")
                {
                    sql +=  " and scm.SessionName='" + Session["SessionName"].ToString() + "'";
                }
                sql +=  " and so.AdmissionForClassId='" + drpclass.SelectedValue.ToString() + "'";
                DataTable dt1;
                dt1 = oo.Fetchdata(sql);
                for (int l = 0; l < dt1.Rows.Count; l++)
                {
                    dt.Rows.Add(dt1.Rows[l][0].ToString(), "", "");
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
                            tc.CssClass = "p-pad-n text-center sub-m-w-35";

                            GridView2.Rows[j].Cells.Add(tc);
                            TableCell tc1 = new TableCell();
                            tc1.Text = "x";
                            tc1.CssClass = "p-pad-n text-center sub-m-w-35";

                            GridView2.Rows[j].Cells.Add(tc1);                            
                            TableCell tc2 = new TableCell();
                            tc2.Text = "x";
                            tc2.CssClass = "p-pad-n text-center sub-m-w-35";

                            GridView2.Rows[j].Cells.Add(tc2);
                            if (k % 2 != 0)
                            {
                                tc.BackColor = System.Drawing.Color.LightGray;
                                tc1.BackColor = System.Drawing.Color.LightGray;
                                tc2.BackColor = System.Drawing.Color.LightGray;
                            }
                            changecolour = changecolour + 1;
                            break;
                        }
                    }
                }
                else
                {
                    TableCell tc = new TableCell();
                    tc.Text = "x";
                    tc.CssClass = "p-pad-n text-center sub-m-w-35";
                    GridView2.Rows[j].Cells.Add(tc);
                    TableCell tc1 = new TableCell();
                    tc1.Text = "x";
                    tc1.CssClass = "p-pad-n text-center sub-m-w-35";
                    GridView2.Rows[j].Cells.Add(tc1);
                    TableCell tc2 = new TableCell();
                    tc2.Text = "x";
                    tc2.CssClass = "p-pad-n text-center sub-m-w-35";
                    GridView2.Rows[j].Cells.Add(tc2);
                    if (k % 2 != 0)
                    {
                        tc.BackColor = System.Drawing.Color.LightGray;
                        tc1.BackColor = System.Drawing.Color.LightGray;
                        tc2.BackColor = System.Drawing.Color.LightGray;
                    }
                    changecolour = changecolour + 1;
                }
            }
        }
    }
    protected void loadStudentDataForFA2_Fa3_Fa4()
    {
        for (int k = 0; k < Repeater1.Items.Count; k++)
        {
            Label lblSubjectGroupId = (Label)Repeater1.Items[k].FindControl("Label4");

            sql = "Select so.SrNo,(Case When (cv.Test1='' or cv.Test1 is null) Then 'x' Else cv.Test1 End) as Test1,(Case When (cv.Test2='' or cv.Test2 is null) Then 'x' Else cv.Test2 End) as Test2,(Case When (cv.Test3='' or cv.Test3 is null) Then 'x' Else cv.Test3 End) as Test3,sg.FirstName from CCE_New1 CV ";
            sql +=  " left join StudentOfficialDetails so on cv.SrNo=so.SrNo";
            sql +=  " left join StudentGenaralDetail sg on cv.SrNo=sg.SrNo";
            if (drpsection.SelectedIndex != 0)
            {
                sql +=  " inner join SectionMaster scm on scm.Id=so.SectionId";
            }
            sql +=  " inner join SubjectMaster sm on sm.ClassId=cv.ClassId and sm.Id=CV.SubjectId";
            sql +=  " inner join SubjectGroupMaster sgm on sgm.Id=sm.GroupId";
            sql +=  " where so.SessionName='" + Session["SessionName"].ToString() + "' and sg.SessionName='" + Session["SessionName"].ToString() + "' and cv.SessionName='" + Session["SessionName"].ToString() + "' and sgm.id='" + lblSubjectGroupId.Text + "'";
            sql +=  " and sm.SessionName='" + Session["SessionName"].ToString() + "' and sgm.SessionName='" + Session["SessionName"].ToString() + "' and cv.Evaluation='" + drpEval.SelectedItem.Text.Trim() + "'";
            if (drpsection.SelectedIndex != 0)
            {
                sql +=  " and scm.SessionName='" + Session["SessionName"].ToString() + "'";
            }
            sql +=  " and so.AdmissionForClassId='" + drpclass.SelectedValue.ToString() + "' order by sg.FirstName";

            DataTable dt;
            dt = oo.Fetchdata(sql);
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
                            tc.CssClass = "p-pad-n text-center sub-m-w-35";
                            GridView2.Rows[j].Cells.Add(tc);
                            TableCell tc1 = new TableCell();
                            tc1.Text = dt.Rows[i][2].ToString();
                            tc1.CssClass = "p-pad-n text-center sub-m-w-35";
                            GridView2.Rows[j].Cells.Add(tc1);
                            TableCell tc2 = new TableCell();
                            tc2.Text = dt.Rows[i][3].ToString();
                            tc2.CssClass = "p-pad-n text-center sub-m-w-35";
                            GridView2.Rows[j].Cells.Add(tc2);
                            if (k % 2 != 0)
                            {
                                tc.BackColor = System.Drawing.Color.LightGray;
                                tc1.BackColor = System.Drawing.Color.LightGray;
                                tc2.BackColor = System.Drawing.Color.LightGray;
                            }
                            changecolour = changecolour + 1;
                            break;
                        }
                    }
                }
                else
                {
                    TableCell tc = new TableCell();
                    tc.Text = "x";
                    tc.CssClass = "p-pad-n text-center sub-m-w-35";
                    GridView2.Rows[j].Cells.Add(tc);
                    TableCell tc1 = new TableCell();
                    tc1.Text = "x";
                    tc1.CssClass = "p-pad-n text-center sub-m-w-35";
                    GridView2.Rows[j].Cells.Add(tc1);
                    TableCell tc2 = new TableCell();
                    tc2.Text = "x";
                    tc2.CssClass = "p-pad-n text-center sub-m-w-35";
                    GridView2.Rows[j].Cells.Add(tc2);
                    if (k % 2 != 0)
                    {
                        tc.BackColor = System.Drawing.Color.LightGray;
                        tc1.BackColor = System.Drawing.Color.LightGray;
                        tc2.BackColor = System.Drawing.Color.LightGray;
                    }
                    changecolour = changecolour + 1;
                }
            }
        }
    }
    protected void loadStudentDataForSA1_SA2()
    {
        for (int k = 0; k < Repeater1.Items.Count; k++)
        {
            Label lblSubjectGroupId = (Label)Repeater1.Items[k].FindControl("Label4");

            sql = "Select so.SrNo,(Case When (UT='' or UT is Null) Then 'x' Else UT End) as UT,sg.FirstName from CCE CV ";
            sql +=  " left join StudentOfficialDetails so on cv.SrNo=so.SrNo";
            sql +=  " left join StudentGenaralDetail sg on cv.SrNo=sg.SrNo";
            if (drpsection.SelectedIndex != 0)
            {
                sql +=  " inner join SectionMaster scm on scm.Id=so.SectionId";
            }
            sql +=  " inner join SubjectMaster sm on sm.ClassId=cv.ClassId and sm.Id=CV.SubjectId";
            sql +=  " inner join SubjectGroupMaster sgm on sgm.Id=sm.GroupId";
            sql +=  " where so.SessionName='" + Session["SessionName"].ToString() + "' and sg.SessionName='" + Session["SessionName"].ToString() + "' and cv.SessionName='" + Session["SessionName"].ToString() + "' and sgm.id='" + lblSubjectGroupId.Text + "'";
            sql +=  " and sm.SessionName='" + Session["SessionName"].ToString() + "' and sgm.SessionName='" + Session["SessionName"].ToString() + "' and cv.Evaluation='" + drpEval.SelectedItem.Text.Trim() + "'";
            if (drpsection.SelectedIndex != 0)
            {
                sql +=  " and scm.SessionName='" + Session["SessionName"].ToString() + "'";
            }
            sql +=  " and so.AdmissionForClassId='" + drpclass.SelectedValue.ToString() + "' order by sg.FirstName";

            DataTable dt;
            dt = oo.Fetchdata(sql);
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
                            tc.CssClass = "p-pad-n text-center sub-m-w-35";
                            GridView2.Rows[j].Cells.Add(tc);
                            TableCell tc1 = new TableCell();
                            tc1.Text = "x";
                            tc1.CssClass = "p-pad-n text-center sub-m-w-35";
                            GridView2.Rows[j].Cells.Add(tc1);
                            TableCell tc2 = new TableCell();
                            tc2.Text = "x";
                            tc2.CssClass = "p-pad-n text-center sub-m-w-35";
                            GridView2.Rows[j].Cells.Add(tc2);
                            if (k % 2 != 0)
                            {
                                tc.BackColor = System.Drawing.Color.LightGray;
                                tc1.BackColor = System.Drawing.Color.LightGray;
                                tc2.BackColor = System.Drawing.Color.LightGray;
                            }
                            changecolour = changecolour + 1;
                            break;
                        }
                    }
                }
                else
                {
                    TableCell tc = new TableCell();
                    tc.Text = "x";
                    tc.CssClass = "p-pad-n text-center sub-m-w-35";
                    GridView2.Rows[j].Cells.Add(tc);
                    TableCell tc1 = new TableCell();
                    tc1.Text = "x";
                    tc1.CssClass = "p-pad-n text-center sub-m-w-35";
                    GridView2.Rows[j].Cells.Add(tc1);
                    TableCell tc2 = new TableCell();
                    tc2.Text = "x";
                    tc2.CssClass = "p-pad-n text-center sub-m-w-35";
                    GridView2.Rows[j].Cells.Add(tc2);
                    if (k % 2 != 0)
                    {
                        tc.BackColor = System.Drawing.Color.LightGray;
                        tc1.BackColor = System.Drawing.Color.LightGray;
                        tc2.BackColor = System.Drawing.Color.LightGray;
                    }
                    changecolour = changecolour + 1;
                }
            }
        }
    }
    protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
    {
        //GridView1.Width = DataList1.Width;
        //GridView1.Height = DataList1.Width;        
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
            oo.MessageBoxforUpdatePanel("Please, select section!",drpsection);
        }
    }
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        if (GridView1.Rows.Count > 0)
        {
            loadStudentData();
            oo.ExportToWord(Response, "Report" + ".doc", divExport);
        }
    }
    protected void ImageButton4_Click(object sender, ImageClickEventArgs e)
    {
        loadStudentData();
        PrintHelper_New.ctrl = abc;
        ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>var winpop=window.open('../Print_New.aspx','_blank'); if(!winpop || winpop.closed){alert('Please allow pop-up blocker from browser settings!')}</script>");
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }
    protected void Repeater2_ItemCreated(object sender, RepeaterItemEventArgs e)
    {
        colsp.ColSpan = Repeater2.Items.Count * 3 + 5;
    }
}