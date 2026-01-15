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
        Campus camp = new Campus(); camp.LoadLoader(loader);
        BLL.BLLInstance.LoadHeader("Examination", header);

        if (!IsPostBack)
        {
            if (Session["SessionName"].ToString() == "2017-2018" || Session["SessionName"].ToString() == "2018-2019")
            {
                Response.Redirect("ClasswiseAllSubjectMarksListVItoVIII_1718.aspx?check=ClasswiseAllSubjectMarksList_VItoVIII");
            }
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
            sql +=  " where cm.SessionName='" + Session["SessionName"] + "' and GroupId='G4' Order by CIDOrder";
            oo.FillDropDown_withValue(sql, drpclass, "ClassName", "Id");

        }
        else
        {
            if (Session["SessionName"].ToString() == "2015-2016")
            {
                sql = "Select Distinct ClassName,sctm.ClassId as Id,CIDOrder from SubjectClassTeacherMaster sctm";
                sql +=  " inner join ClassMaster cm on cm.Id=sctm.ClassId";
                sql +=  " inner join dt_ClassGroupMaster t1 on t1.ClassId=sctm.ClassId";
                sql +=  " where GroupId='G4' and cm.SessionName='" + Session["SessionName"] + "' and sctm.SessionName='" + Session["SessionName"] + "' and Ecode='" + Session["LoginName"].ToString() + "' and ClassTeacher='Yes' Order by CIDOrder";
                oo.FillDropDown_withValue(sql, drpclass, "ClassName", "Id");
            }
            else
            {
                sql = "Select ClassName,cm.Id from ClassTeacherMaster T1";
                sql +=  " inner join ClassMaster cm on cm.Id=T1.ClassId and cm.SessionName=t1.SessionName";
                sql +=  " inner join dt_ClassGroupMaster T2 on T2.ClassId=T1.ClassId and cm.SessionName=T2.SessionName";
                sql +=  " where GroupId='G4' and EmpCode='" + Session["LoginName"].ToString() + "' and IsClassTeacher=1 and T1.SessionName='" + Session["SessionName"] + "' Order by CIDOrder";
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
        //sql = "Select SubjectGroup,Id from SubjectGroupMaster where IsForOnlyExam=1 and IsAdditional=0 and Classid='" + drpclass.SelectedValue.ToString() + "' and SectionName='" + drpsection.SelectedItem.ToString() + "'";

        sql = "Select SubjectName as SubjectGroup,sm.Id,sgm.DisplayOrder from SubjectMaster sm   ";
        sql +=  "   inner join S02_SubjectPaperMaster spm on spm.S02ID=sm.PaperID and spm.SessionName=sm.SessionName   ";
        sql +=  "   inner join SubjectGroupMaster sgm on sgm.Id=spm.SubjectGroupID and sgm.SessionName=sm.SessionName  ";
        sql +=  "   where sm.Classid='" + drpclass.SelectedValue.ToString() + "'";
        sql +=  "   and sm.SectionName='" + drpsection.SelectedItem.Text + "' and IsForExam=1 and IsForOnlyExam=1 and IsAdditional=0 and sm.SessionName='" + Session["SessionName"].ToString() + "' order by sgm.DisplayOrder";
        
        DataTable dt;
        dt = oo.Fetchdata(sql);

        Repeater1.DataSource = dt;
        Repeater1.DataBind();
        Repeater2.DataSource = dt;
        Repeater2.DataBind();


        if (Repeater2.Items.Count > 0)
        {
            foreach (RepeaterItem rpi1 in Repeater2.Items)
            {
                Label lbl1 = (Label)rpi1.FindControl("Label2");
                Label lbl2 = (Label)rpi1.FindControl("Label9");
                Label lbl3 = (Label)rpi1.FindControl("Label10");
                Label lbl4 = (Label)rpi1.FindControl("Label11");

                Label lblMM1 = (Label)rpi1.FindControl("lblMM1");
                Label lblMM2 = (Label)rpi1.FindControl("lblMM2");
                Label lblMM3 = (Label)rpi1.FindControl("lblMM3");
                Label lblMM4 = (Label)rpi1.FindControl("lblMM4");

                HtmlTableCell col2 = (HtmlTableCell)rpi1.FindControl("col2");
                HtmlTableCell col3 = (HtmlTableCell)rpi1.FindControl("col3");
                HtmlTableCell col4 = (HtmlTableCell)rpi1.FindControl("col4");

                if (drpEval.SelectedItem.Text == "SA1" || drpEval.SelectedItem.Text == "SA2")
                {
                    lbl1.Text = "Thy.";
                    lbl2.Text = "";
                    lbl3.Text = "";
                    lbl4.Text = "";

                    lblMM1.Text = "100";
                    lblMM2.Text = "";
                    lblMM3.Text = "";
                    lblMM4.Text = "";

                    col2.Visible = false;
                    col3.Visible = false;
                    col4.Visible = false;
                }
                else
                {
                    lbl1.Text = "T1";
                    lbl2.Text = "T2";
                    lbl3.Text = "T3";
                    lbl4.Text = "T4";

                    lblMM1.Text = "20";
                    lblMM2.Text = "20";
                    lblMM3.Text = "20";
                    lblMM4.Text = "20";

                    col2.Visible = true;
                    col3.Visible = true;
                    col4.Visible = true;
                }
            }

        }

        if (Repeater1.Items.Count > 0)
        {
            int count = 0;
            foreach (RepeaterItem rpi in Repeater1.Items)
            {
                Label lblSubjectid = (Label)rpi.FindControl("Label4");
                HtmlTableCell col1 = (HtmlTableCell)rpi.FindControl("col1");

                if (drpEval.SelectedItem.Text == "SA1" || drpEval.SelectedItem.Text == "SA2")
                {
                    col1.ColSpan = 1;

                    sql = "Select *from CCEMMarksForVItoX where Eval='" + drpEval.SelectedItem.Text.Trim() + "' and SubjectId='" + lblSubjectid.Text + "'";
                    sql +=  " and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";

                    Label lblMM1 = (Label)Repeater2.Items[count].FindControl("lblMM1");
                    Label lblMM2 = (Label)Repeater2.Items[count].FindControl("lblMM2");
                    Label lblMM3 = (Label)Repeater2.Items[count].FindControl("lblMM3");
                    Label lblMM4 = (Label)Repeater2.Items[count].FindControl("lblMM4");

                    count = count + 1;

                    lblMM1.Text = oo.ReturnTag(sql, "SAT") == string.Empty ? "?" : oo.ReturnTag(sql, "SAT");
                    lblMM2.Text = "";
                    lblMM3.Text = "";
                    lblMM4.Text = "";
                }
                else
                {
                    col1.ColSpan = 4;

                    sql = "Select P1,P2,P3,P4 from CCEMMarksForVItoX where Eval='" + drpEval.SelectedItem.Text.Trim() + "' and SubjectId='" + lblSubjectid.Text + "'";
                    sql +=  " and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";

                    Label lblMM1 = (Label)Repeater2.Items[count].FindControl("lblMM1");
                    Label lblMM2 = (Label)Repeater2.Items[count].FindControl("lblMM2");
                    Label lblMM3 = (Label)Repeater2.Items[count].FindControl("lblMM3");
                    Label lblMM4 = (Label)Repeater2.Items[count].FindControl("lblMM4");
                    count = count + 1;

                    lblMM1.Text = oo.ReturnTag(sql, "P1") == string.Empty ? "20" : oo.ReturnTag(sql, "P1");
                    lblMM2.Text = oo.ReturnTag(sql, "P2") == string.Empty ? "20" : oo.ReturnTag(sql, "P2");
                    lblMM3.Text = oo.ReturnTag(sql, "P3") == string.Empty ? "20" : oo.ReturnTag(sql, "P3");
                    lblMM4.Text = oo.ReturnTag(sql, "P4") == string.Empty ? "20" : oo.ReturnTag(sql, "P4");
                }
            }
        }

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

        rpt2.DataSource = oo.GridFill(sql);
        rpt2.DataBind();
    }
    public void loadStudentData()
    {
        loadClassName();
        loadSubject();
        if (drpEval.SelectedItem.Text == "FA1" || drpEval.SelectedItem.Text == "FA2" || drpEval.SelectedItem.Text == "FA3" || drpEval.SelectedItem.Text == "FA4")
        {
            loadStudentDataForFA1_FA2_Fa3_Fa4();
        }
        else
        {
            loadStudentDataForSA1_SA2();
        }

        lblSubject.Text = "All";
        lblClass.Text = drpclass.SelectedItem.Text.Trim();
        lblSection.Text = drpsection.SelectedItem.Text.Trim();
        lblDate.Text = DateTime.Today.ToString("dd MMM yyyy");        
    }
    
    protected void loadStudentDataForFA1_FA2_Fa3_Fa4()
    {
        for (int k = 0; k < Repeater1.Items.Count; k++)
        {
            Label lblSubjectGroupId = (Label)Repeater1.Items[k].FindControl("Label4");

            int changecolour = 0;
            for (int j = 0; j < rpt2.Items.Count; j++)
            {
                Repeater rpt3 = (Repeater)rpt2.Items[j].FindControl("rpt3");
                Label lblsrno = (Label)rpt2.Items[j].FindControl("lblsrno");


                sql = "Select cv.SrNo,(Case When (cv.T1='' or cv.T1 is null) Then 'x' Else cv.T1 End) as Test1,(Case When (cv.T2='' or cv.T2 is null) Then 'x' Else cv.T2 End) as Test2,";
                sql +=  " (Case When (cv.T3='' or cv.T3 is null) Then 'x' Else cv.T3 End) as Test3,(Case When (cv.T4='' or cv.T4 is null) Then 'x' Else cv.T4 End) as Test4,";
                sql +=  " '' FirstName from CCEVItoX CV  where cv.SessionName='" + Session["SessionName"].ToString() + "' and SubjectId='" + lblSubjectGroupId.Text + "'";
                sql +=  " and cv.Evaluation='" + drpEval.SelectedItem.Text.Trim() + "' and cv.SrNo='" + lblsrno.Text + "'";

                DataTable dt;
                dt = oo.Fetchdata(sql);

                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {

                            TableCell tc = new TableCell();
                            tc.Text = dt.Rows[i][1].ToString();
                            tc.CssClass = "p-pad-n text-center sub-m-w-35";
                            rpt3.Controls.Add(tc);

                            TableCell tc1 = new TableCell();
                            tc1.Text = dt.Rows[i][2].ToString();
                            tc1.CssClass = "p-pad-n text-center sub-m-w-35";
                            rpt3.Controls.Add(tc1);

                            TableCell tc2 = new TableCell();
                            tc2.Text = dt.Rows[i][3].ToString();
                            tc2.CssClass = "p-pad-n text-center sub-m-w-35";
                            rpt3.Controls.Add(tc2);

                            TableCell tc3 = new TableCell();
                            tc3.Text = dt.Rows[i][4].ToString();
                            tc3.CssClass = "p-pad-n text-center sub-m-w-35";
                            rpt3.Controls.Add(tc3);

                            if (k % 2 != 0)
                            {
                                tc.BackColor = System.Drawing.Color.LightGray;
                                tc1.BackColor = System.Drawing.Color.LightGray;
                                tc2.BackColor = System.Drawing.Color.LightGray;
                                tc3.BackColor = System.Drawing.Color.LightGray;
                            }
                            changecolour = changecolour + 1;
                            
                    }
                }
                else
                {
                    TableCell tc = new TableCell();
                    tc.Text = "x";
                    tc.CssClass = "p-pad-n text-center sub-m-w-35";
                    rpt3.Controls.Add(tc);

                    TableCell tc1 = new TableCell();
                    tc1.Text = "x";
                    tc1.CssClass = "p-pad-n text-center sub-m-w-35";
                    rpt3.Controls.Add(tc1);

                    TableCell tc2 = new TableCell();
                    tc2.Text = "x";
                    tc2.CssClass = "p-pad-n text-center sub-m-w-35";
                    rpt3.Controls.Add(tc2);

                    TableCell tc3 = new TableCell();
                    tc3.Text = "x";
                    tc3.CssClass = "p-pad-n text-center sub-m-w-35";
                    rpt3.Controls.Add(tc3);

                    if (k % 2 != 0)
                    {
                        tc.BackColor = System.Drawing.Color.LightGray;
                        tc1.BackColor = System.Drawing.Color.LightGray;
                        tc2.BackColor = System.Drawing.Color.LightGray;
                        tc3.BackColor = System.Drawing.Color.LightGray;
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

            int changecolour = 0;
            for (int j = 0; j < rpt2.Items.Count; j++)
            {
                Repeater rpt3 = (Repeater)rpt2.Items[j].FindControl("rpt3");
                Label lblsrno = (Label)rpt2.Items[j].FindControl("lblsrno");

                sql = "Select cv.SrNo,(Case When (SAT='' or SAT is Null) Then 'x' Else SAT End) as UT,";
                sql +=  " '' FirstName from CCEVItoX CV  where cv.SessionName='" + Session["SessionName"].ToString() + "' and SubjectId='" + lblSubjectGroupId.Text + "'";
                sql +=  " and cv.Evaluation='" + drpEval.SelectedItem.Text.Trim() + "' and cv.SrNo='" + lblsrno.Text + "'";

                DataTable dt;
                dt = oo.Fetchdata(sql);

                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {

                        TableCell tc = new TableCell();
                        tc.Text = dt.Rows[i][1].ToString();
                        tc.CssClass = "p-pad-n text-center sub-m-w-35";
                        rpt3.Controls.Add(tc);

                        if (k % 2 != 0)
                        {
                            tc.BackColor = System.Drawing.Color.LightGray;
                        }
                        changecolour = changecolour + 1;
                            
                    }
                }
                else
                {
                    TableCell tc = new TableCell();
                    tc.Text = "x";
                    tc.CssClass = "p-pad-n text-center sub-m-w-35";
                    rpt3.Controls.Add(tc);

                    if (k % 2 != 0)
                    {
                        tc.BackColor = System.Drawing.Color.LightGray;
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
       
    }
    protected void drpsection_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadStudentData();
       
    }
    protected void drpEval_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (drpsection.SelectedIndex != 0)
        {
            loadStudentData();
        }
        else
        {
            //oo.MessageBoxforUpdatePanel("Please, select section!", drpsection);
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Please, select section!", "A");       

        }
    }


    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }
    protected void Repeater2_ItemCreated(object sender, RepeaterItemEventArgs e)
    {
        //colsp.ColSpan = Repeater2.Items.Count * 3 + 5;
    }
    protected void ImageButton1_Click(object sender, EventArgs e)
    {
        if (rpt2.Items.Count > 0)
        {
            loadStudentData();
            oo.ExportToWord(Response, "Report" + ".doc", divExport);
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
        PrintHelper_New.ctrl = abc;
        ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>var winpop=window.open('../Print_New.aspx','_blank'); if(!winpop || winpop.closed){alert('Please allow pop-up blocker from browser settings!')}</script>");
    }
}