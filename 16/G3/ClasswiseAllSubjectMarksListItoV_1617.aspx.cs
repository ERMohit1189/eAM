using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI.HtmlControls;

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
        BLL.BLLInstance.LoadHeader("Examination", header);

        if (!IsPostBack)
        {
            if (Session["SessionName"].ToString() == "2017-2018" || Session["SessionName"].ToString() == "2018-2019")
            {
                Response.Redirect("ClasswiseAllSubjectMarksListItoV_1718.aspx?check=ClasswiseAllSubjectMarksList_ItoV");
            }
            sql = "Select CollegeName from CollegeMaster where CollegeId=" + 1;          
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
            sql +=  " where cm.SessionName='" + Session["SessionName"] + "' and GroupId='G3' Order by CIDOrder";
            oo.FillDropDown_withValue(sql, drpclass, "ClassName", "Id");

        }
        else
        {
            sql = "Select ClassName,cm.Id from ClassTeacherMaster T1";
            sql +=  " inner join ClassMaster cm on cm.Id=T1.ClassId and cm.SessionName=t1.SessionName";
            sql +=  " inner join dt_ClassGroupMaster T2 on T2.ClassId=T1.ClassId and cm.SessionName=T2.SessionName";
            sql +=  " where GroupId='G3' and EmpCode='" + Session["LoginName"].ToString() + "' and IsClassTeacher=1 and T1.SessionName='" + Session["SessionName"] + "' Order by CIDOrder";
            oo.FillDropDown_withValue(sql, drpclass, "ClassName", "Id");
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
        

        sql = "Select Left(SubjectGroup,9) as SubjectGroup,Id from SubjectGroupMaster where (IsForOnlyExam=1 or IsForOnlyExam is null) and Classid='" + drpclass.SelectedValue.ToString() + "' and SectionName='" + drpsection.SelectedItem.Text + "' and SessionName='"+Session["SessionName"].ToString()+"'";
        DataTable dt;
        dt = oo.Fetchdata(sql);

        Repeater1.DataSource = dt;
        Repeater1.DataBind();
        if (drpEval.SelectedIndex != 0)
        {
            for (int i = 0; i < Repeater1.Items.Count; i++)
            {
                HtmlControl td2 = (HtmlControl)Repeater1.Items[i].FindControl("td2");
                td2.Attributes.Add("colspan", "2");
            }
        }
        else
        {
            for (int i = 0; i < Repeater1.Items.Count; i++)
            {
                HtmlControl td2 = (HtmlControl)Repeater1.Items[i].FindControl("td2");
                td2.Attributes.Add("colspan", "3");
            }
        }
        Repeater2.DataSource = dt;
        Repeater2.DataBind();
        if (drpEval.SelectedIndex != 0)
        {
            for (int i = 0; i < Repeater2.Items.Count; i++)
            {
                Label Label7 = (Label)Repeater2.Items[i].FindControl("Label7");
                Label Label8 = (Label)Repeater2.Items[i].FindControl("Label8");

                if (drpEval.SelectedIndex == 1)
                {
                    Label7.Text = "AUG"; Label8.Text = "SEPT";
                }
                else
                {
                    Label7.Text = "DEC"; Label8.Text = "FEB";
                }
                
                HtmlControl td3 = (HtmlControl)Repeater2.Items[i].FindControl("td3");
                td3.Visible = false;
            }
        }
        else
        {
            for (int i = 0; i < Repeater2.Items.Count; i++)
            {
                Label Label7 = (Label)Repeater2.Items[i].FindControl("Label7");
                Label Label8 = (Label)Repeater2.Items[i].FindControl("Label8");
                Label7.Text = "MAY"; Label8.Text = "JULY";

                HtmlControl td3 = (HtmlControl)Repeater2.Items[i].FindControl("td3");
                td3.Visible = true;
            }
        }
    }
    protected void loadSubject()
    {
        sql = "select SG.Id, SC.SectionName,SO.Card,SO.Medium as Medium,CM.ClassName,convert(nvarchar,So.DateOfAdmiission,106) as DateOfAdmiission ,SO.SectionId,Sf.FatherName,SF.MotherName,(SG.FirstName+' '+SG.MiddleName+' '+SG.LastName) as StudentName,so.StEnRCode as StEnRCode,sg.srno  as srno,case  when so.TransportRequired='Yes' then 'Yes' else 'No' end as TransportRequired,so.wayamount as wayamount from StudentGenaralDetail SG ";
        sql +=  "   left join StudentFamilyDetails SF on SG.srno=SF.srno";
        sql +=  "   left join StudentOfficialDetails SO on SG.srno=SO.srno";
        sql +=  "   left join ClassMaster CM on SO.AdmissionForClassId=CM.Id";
        sql +=  "   left join SectionMaster SC on SO.SectionId=SC.Id";
        sql +=  "   where sc.SectionName='" + drpsection.SelectedItem.Text + "' and sg.SessionName='" + Session["SessionName"].ToString() + "' and ";
        sql +=  "   so.SessionName='" + Session["SessionName"].ToString() + "' and sf.SessionName='" + Session["SessionName"].ToString() + "' and cm.SessionName='" + Session["SessionName"].ToString() + "'";
        sql +=  "   and SC.SessionName='" + Session["SessionName"].ToString() + "'  and";
        sql +=  "   sg.BranchCode=" + Session["BranchCode"].ToString() + "";
        sql +=  "   and SO.Withdrwal is null and SO.AdmissionForClassId='" + drpclass.SelectedValue.ToString() + "' order by sg.FirstName";

        rpt2.DataSource = oo.GridFill(sql);
        rpt2.DataBind();
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
                int changecolour = 0;
                for (int j = 0; j < rpt2.Items.Count; j++)
                {
                    Repeater rpt3 = (Repeater)rpt2.Items[j].FindControl("rpt3");
                    Label lblsrno = (Label)rpt2.Items[j].FindControl("lblsrno");

                sql = "Select Sum(Convert(float,CASE When ISNUMERIC(cv.Test1)=1 Then cv.Test1 Else '0' End)) as Test1,Sum(Convert(float,CASE When ISNUMERIC(cv.Test2)=1 Then cv.Test2 Else '0' End)) as Test2,Sum(Convert(float,CASE When ISNUMERIC(cv.Test3)=1 Then cv.Test3 Else '0' End)) as Test3 from CCEItoV CV ";
                //sql +=  " left join StudentOfficialDetails so on cv.SrNo=so.SrNo";
                //sql +=  " left join StudentGenaralDetail sg on cv.SrNo=sg.SrNo";               
                //sql +=  " inner join SectionMaster scm on scm.Id=so.SectionId";                
                sql +=  " inner join SubjectMaster sm on sm.Id=CV.SubjectId";
                sql +=  " inner join SubjectGroupMaster sgm on sgm.Id=sm.GroupId";
                //sql +=  " where scm.SectionName='" + drpsection.SelectedItem.Text + "' and so.SessionName='" + Session["SessionName"].ToString() + "' and sg.SessionName='" + Session["SessionName"].ToString() + "' and cv.SessionName='" + Session["SessionName"].ToString() + "' and sgm.id='" + lblSubjectGroupId.Text + "'";
                //sql +=  " and sm.SessionName='" + Session["SessionName"].ToString() + "' and sgm.SessionName='" + Session["SessionName"].ToString() + "' and cv.Evaluation='" + drpEval.SelectedItem.Text.Trim() + "'";               
                //sql +=  " and scm.SessionName='" + Session["SessionName"].ToString() + "'";               
                //sql +=  " and so.AdmissionForClassId='" + drpclass.SelectedValue.ToString() + "' group by so.SrNo,sg.FirstName order by sg.FirstName";
                sql +=  " where cv.SessionName='" + Session["SessionName"].ToString() + "' and sgm.Id='" + lblSubjectGroupId.Text + "'";
                sql +=  " and cv.Evaluation='" + drpEval.SelectedItem.Text.Trim() + "' and cv.SrNo='" + lblsrno.Text + "'";

                DataTable dt;
                dt = oo.Fetchdata(sql);
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            
                                TableCell tc = new TableCell();
                                tc.Text = dt.Rows[i][0].ToString();                             
                                tc.CssClass = "p-pad-n text-center sub-m-w-45";
                                rpt3.Controls.Add(tc);


                                TableCell tc1 = new TableCell();
                                tc1.Text = dt.Rows[i][1].ToString();
                                tc1.CssClass = "p-pad-n text-center sub-m-w-45";
                                rpt3.Controls.Add(tc1);

                                if (drpEval.SelectedIndex == 0)
                                {
                                    TableCell tc2 = new TableCell();
                                    tc2.Text = dt.Rows[i][2].ToString();
                                    tc2.CssClass = "p-pad-n text-center sub-m-w-45";
                                    rpt3.Controls.Add(tc2);

                                    if (k % 2 != 0)
                                    {
                                        tc.BackColor = System.Drawing.Color.LightGray;
                                        tc1.BackColor = System.Drawing.Color.LightGray;
                                        tc2.BackColor = System.Drawing.Color.LightGray;
                                        
                                    }
                                }
                                else
                                {
                                    if (k % 2 != 0)
                                    {
                                        tc.BackColor = System.Drawing.Color.LightGray;
                                        tc1.BackColor = System.Drawing.Color.LightGray;
                                    }
                                }
                               
                                changecolour = changecolour + 1;
                               
                        }
                    }
                    else
                    {
                        TableCell tc = new TableCell();
                        tc.Text = "x";                   
                        tc.CssClass = "p-pad-n text-center sub-m-w-45";
                        rpt3.Controls.Add(tc);

                        TableCell tc1 = new TableCell();
                        tc1.Text = "x";                        
                        tc1.CssClass = "p-pad-n text-center sub-m-w-45";
                        rpt3.Controls.Add(tc1);
                        if (drpEval.SelectedIndex == 0)
                        {     
                            TableCell tc2 = new TableCell();
                            tc2.Text = "x";
                            tc2.CssClass = "p-pad-n text-center sub-m-w-45";
                            rpt3.Controls.Add(tc2);

                            if (k % 2 != 0)
                            {
                                tc.BackColor = System.Drawing.Color.LightGray;
                                tc1.BackColor = System.Drawing.Color.LightGray;
                                tc2.BackColor = System.Drawing.Color.LightGray;
                            }
                        }
                        else
                        {
                            if (k % 2 != 0)
                            {
                                tc.BackColor = System.Drawing.Color.LightGray;
                                tc1.BackColor = System.Drawing.Color.LightGray;
                            }
                        }
                        changecolour = changecolour + 1;
                    }
                }
            }
        }
    }



    protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
    {
        //GridView1.Width = DataList1.Width;
        
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
    protected void ImageButton1_Click(object sender, EventArgs e)
    {
      
    }
    protected void ImageButton4_Click(object sender, EventArgs e)
    {
   
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }
    protected void ImageButton1_Click1(object sender, EventArgs e)
    {
        if (rpt2.Items.Count > 0)
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
    protected void ImageButton4_Click1(object sender, EventArgs e)
    {
        loadStudentData();
        PrintHelper_New.ctrl = abc;
        ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>var winpop=window.open('../Print_New.aspx','_blank'); if(!winpop || winpop.closed){alert('Please allow pop-up blocker from browser settings!')}</script>");
   
    }
}