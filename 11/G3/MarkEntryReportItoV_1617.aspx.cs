using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

public partial class staff_MarkEntryReportItoV : Page
{
    SqlConnection con = new SqlConnection();
    SqlCommand cmd = new SqlCommand();
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
        if (Session["SessionName"].ToString() == "2017-2018")
        {
            Response.Redirect("MarksEntryReportItoV_1718.aspx?check=MarkEntryReport_ItoV");
        }
        con = oo.dbGet_connection();
        Campus camp = new Campus(); camp.LoadLoader(loader); 
        if (!IsPostBack)
        {
            loadclass();
            loadsection();
            loadSubjectGroup();

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
            sql = "Select Count(*) Count from ClassTeacherMaster ctm";
            sql +=  " inner join dt_ClassGroupMaster t1 on t1.ClassId=ctm.ClassId";
            sql +=  " where EmpCode='" + Session["LoginName"].ToString() + "' and IsClassTeacher=1";
            sql +=  " and ctm.SessionName='" + Session["SessionName"].ToString() + "' and t1.GroupId='G3'";

            Session["count"] = BAL.objBal.ReturnTag(sql, "Count");

            if (Convert.ToInt16(Session["count"].ToString()) > 0)
            {
                sql = "Select Distinct ClassName,cm.Id,CIDOrder from ClassTeacherMaster ctm";
                sql +=  " inner join ClassMaster cm on cm.Id=ctm.ClassId and cm.SessionName=ctm.SessionName";
                sql +=  " inner join dt_ClassGroupMaster t1 on t1.ClassId=cm.Id and cm.SessionName=ctm.SessionName";
                sql +=  " where EmpCode='" + Session["LoginName"].ToString() + "' and IsClassTeacher=1";
                sql +=  " and ctm.SessionName='" + Session["SessionName"].ToString() + "' and t1.GroupId='G3'";
                sql +=  " UNION ";
                sql +=  " Select Distinct ClassName,sctm.ClassId as Id,CIDOrder from SubjectClassTeacherMaster sctm";
                sql +=  " inner join ClassMaster cm on cm.Id=sctm.ClassId";
                sql +=  " inner join dt_ClassGroupMaster t1 on t1.ClassId=sctm.ClassId";
                sql +=  " where GroupId='G3'  and cm.SessionName='" + Session["SessionName"].ToString() + "' and sctm.SessionName='" + Session["SessionName"] + "' and Ecode='" + Session["LoginName"].ToString() + "' Order by CIDOrder";
                oo.FillDropDown_withValue(sql, drpclass, "ClassName", "Id");

            }
            else
            {
                sql = "Select Distinct ClassName,sctm.ClassId as Id,CIDOrder from SubjectClassTeacherMaster sctm";
                sql +=  " inner join ClassMaster cm on cm.Id=sctm.ClassId";
                sql +=  " inner join dt_ClassGroupMaster t1 on t1.ClassId=sctm.ClassId";
                sql +=  " where GroupId='G3'  and cm.SessionName='" + Session["SessionName"].ToString() + "' and sctm.SessionName='" + Session["SessionName"] + "' and Ecode='" + Session["LoginName"].ToString() + "' Order by CIDOrder";
                oo.FillDropDown_withValue(sql, drpclass, "ClassName", "Id");
            }
        }
    }

    public void loadsection()
    {
        sql = "Select Count(*) Count from ClassTeacherMaster ctm";
        sql +=  " inner join dt_ClassGroupMaster t1 on t1.ClassId=ctm.ClassId";
        sql +=  " where EmpCode='" + Session["LoginName"].ToString() + "' and IsClassTeacher=1";
        sql +=  " and t1.GroupId='G3' and ctm.ClassId='" + drpclass.SelectedValue.ToString() + "'";

        Session["count"] = BAL.objBal.ReturnTag(sql, "Count");


        if (Session["Logintype"].ToString() == "Admin")
        {
            sql = "Select SectionName,id from SectionMaster where ClassNameId='" + drpclass.SelectedValue.ToString() + "' and SessionName='" + Session["SessionName"].ToString() + "'";
            oo.FillDropDown_withValue(sql, drpsection, "SectionName", "Id");
            drpsection.Items.Insert(0, new ListItem("<--Select-->", "0"));

        }
        else
        {
            if (Convert.ToInt16(Session["count"].ToString()) > 0)
            {
                sql = "Select Distinct SectionName,sm.Id from ClassTeacherMaster ctm";
                sql +=  " inner join dt_ClassGroupMaster t1 on t1.ClassId=ctm.ClassId and t1.SessionName=ctm.SessionName";
                sql +=  " inner join SectionMaster sm on sm.Id=ctm.SectionId and sm.SessionName=ctm.SessionName";
                sql +=  " where EmpCode='" + Session["LoginName"].ToString() + "' and IsClassTeacher=1";
                sql +=  " and ctm.SessionName='" + Session["SessionName"].ToString() + "' and t1.GroupId='G3' and ctm.ClassId='" + drpclass.SelectedValue.ToString() + "'";
                sql +=  " UNION";
                sql +=  " Select Distinct sm.SectionName,sm.id from SubjectClassTeacherMaster sctm";
                sql +=  " inner join SectionMaster sm on sm.ClassNameId=sctm.ClassId and sm.SectionName=sctm.SectionName and sm.SessionName=sctm.SessionName ";
                sql +=  " where sctm.BranchCode=" + Session["BranchCode"].ToString() + " and sctm.SessionName='" + Session["SessionName"].ToString() + "' and ClassId='" + drpclass.SelectedValue.ToString() + "' and Ecode='" + Session["LoginName"].ToString() + "'";

                oo.FillDropDown_withValue(sql, drpsection, "SectionName", "Id");

                drpsection.Items.Insert(0, new ListItem("<--Select-->", "0"));

                //oo.FillDropDown(sql, drpsection, "SectionName");

            }
            else
            {
                sql = "Select Distinct sm.SectionName,sm.id from SubjectClassTeacherMaster sctm";
                sql +=  " inner join SectionMaster sm on sm.ClassNameId=sctm.ClassId and sm.SessionName=sctm.SessionName ";
                sql +=  " and sm.SectionName=sctm.SectionName";
                sql +=  " where sctm.BranchCode=" + Session["BranchCode"].ToString() + " and sctm.SessionName='" + Session["SessionName"].ToString() + "' and ClassId='" + drpclass.SelectedValue.ToString() + "' and Ecode='" + Session["LoginName"].ToString() + "'";
                oo.FillDropDown_withValue(sql, drpsection, "SectionName", "Id");
                drpsection.Items.Insert(0, new ListItem("<--Select-->", "0"));
            }
        }
    }

    private void loadSubjectGroup()
    {

        sql = "Select Count(*) Count from ClassTeacherMaster ctm";
        sql +=  " inner join dt_ClassGroupMaster t1 on t1.ClassId=ctm.ClassId";
        sql +=  " where EmpCode='" + Session["LoginName"].ToString() + "' and IsClassTeacher=1";
        sql +=  " and ctm.SessionName='" + Session["SessionName"].ToString() + "' ";
        sql +=  " and Sectionid='" + drpsection.SelectedValue.ToString() + "' and t1.GroupId='G3'";

        Session["count"] = BAL.objBal.ReturnTag(sql, "Count");


        if (Session["Logintype"].ToString() == "Admin" || Convert.ToInt16(Session["count"].ToString()) > 0)
        {
            sql = "Select SubjectGroup,Id from SubjectGroupMaster where ClassId='" + drpclass.SelectedValue.ToString() + "' and SectionName='" + drpsection.SelectedItem.ToString() + "' and SessionName='" + Session["SessionName"].ToString() + "' and IsForOnlyExam=1";

            oo.FillDropDown_withValue(sql, drpSubjectGroup, "SubjectGroup", "Id");
            drpSubjectGroup.Items.Insert(0, "<--Select-->");
        }
        else
        {
            sql = "Select  Distinct sgm.SubjectGroup,sgm.Id from SubjectClassTeacherMaster sctm";
            sql +=  " inner join SubjectMaster sm on sm.Id=sctm.Subjectid and sm.SessionName=sctm.SessionName";
            sql +=  " Left join S02_SubjectPaperMaster spm on spm.S02ID=sm.PaperID and spm.SessionName=sctm.SessionName";
            sql +=  " Inner join SubjectGroupMaster sgm on sgm.Id=spm.SubjectGroupID and sgm.SessionName=sctm.SessionName";
            sql +=  " where Ecode='" + Session["LoginName"].ToString() + "' and sctm.ClassId='" + drpclass.SelectedValue.ToString() + "'  ";
            sql +=  " and sctm.SectionName='" + drpsection.SelectedItem.Text.ToString() + "' and sctm.SessionName='" + Session["SessionName"].ToString() + "'";
            sql +=  " and (IsForExam is null or IsForExam ='1')";

            oo.FillDropDown_withValue(sql, drpSubjectGroup, "SubjectGroup", "Id");
            drpSubjectGroup.Items.Insert(0, "<--Select-->");
        }
    }


    public void loadSubject()
    {
        if (Session["Logintype"].ToString() == "Admin" || Convert.ToInt16(Session["count"].ToString()) > 0)
        {
            //sql = "Select SubjectName,Id from SubjectMaster where GroupId='" + drpSubjectGroup.SelectedValue.ToString() + "' and SessionName='" + Session["SessionName"].ToString() + "'";
            sql = "Select SubjectName,sm.Id from SubjectMaster sm";
            sql +=  " inner join S02_SubjectPaperMaster spm on spm.S02ID=sm.PaperID and spm.SessionName=sm.SessionName";
            sql +=  " inner join SubjectGroupMaster sgm on sgm.Id=spm.SubjectGroupID and sgm.SessionName=sm.SessionName";
            sql +=  " where sgm.Id='" + drpSubjectGroup.SelectedValue.ToString() + "' and sm.SessionName='" + Session["SessionName"].ToString() + "' and (IsForExam=1 or IsForExam is null)  order by sm.id";

            oo.FillDropDown_withValue(sql, drpSubject, "SubjectName", "Id");
            drpSubject.Items.Insert(0, "<--Select-->");
        }
        else
        {
            if (Session["SessionName"].ToString() == "2015-2016")
            {
                sql = "Select sm.SubjectName,sctm.Subjectid as Id from SubjectClassTeacherMaster sctm";
                sql +=  " inner join SubjectMaster sm on sm.Id=sctm.Subjectid";
                sql +=  " where Ecode='" + Session["LoginName"].ToString() + "' and sctm.ClassId='" + drpclass.SelectedValue.ToString() + "'  and sctm.SectionName='" + drpsection.SelectedItem.ToString() + "'";
            }
            else
            {
                sql = "Select sm.SubjectName,sctm.Subjectid as Id from SubjectClassTeacherMaster sctm";
                sql +=  " inner join SubjectMaster sm on sm.Id=sctm.Subjectid";
                sql +=  " Left join S02_SubjectPaperMaster spm on spm.S02ID=sm.PaperID";
                sql +=  " where Ecode='" + Session["LoginName"].ToString() + "' and sctm.ClassId='" + drpclass.SelectedValue.ToString() + "'  ";
                sql +=  " and sctm.SectionName='" + drpsection.SelectedItem.ToString() + "' and sctm.SessionName='" + Session["SessionName"].ToString() + "'";
                sql +=  " and (IsForExam is null or IsForExam ='1') and (spm.SubjectGroupID='" + drpSubjectGroup.SelectedValue.ToString() + "' or spm.SubjectGroupID is null)";
            }
            oo.FillDropDown_withValue(sql, drpSubject, "SubjectName", "Id");
            drpSubject.Items.Insert(0, "<--Select-->");
        }
    }
    
    protected void drpclass_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadsection();
        loadSubjectGroup();
        loadSubject();
        loadgrid();
    }
    protected void drpsection_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadSubjectGroup();
        loadSubject();
        loadgrid();
    }

    public void loadgrid()
    {
        //    sql = "Select Distinct CCEItoV.SrNo,(SG.FirstName+' '+SG.MiddleName+' '+SG.LastName) as Name from CCEItoV";
        //    sql +=  " inner join StudentGenaralDetail SG on SG.SrNo=CCEItoV.SrNo";
        //    sql +=  " inner join ClassMaster cm on cm.Id=CCEItoV.ClassId";
        //    sql +=  " inner join SubjectMaster sm on sm.Id=CCEItoV.SubjectId";
        //    sql +=  " inner join SectionMaster scm on scm.SectionName=CCEItoV.SectionName";
        //    sql +=  " where CCEItoV.ClassId='" + drpclass.SelectedValue.ToString() + "' and CCEItoV.SectionName='" + drpsection.SelectedValue.ToString() + "' and scm.ClassNameId='" + drpclass.SelectedValue.ToString() + "' and CCEItoV.SubjectId='" + drpSubject.SelectedValue.ToString() + "'";
        //    sql +=  " and scm.SessionName='" + Session["SessionName"].ToString() + "' and CCEItoV.SessionName='" + Session["SessionName"].ToString() + "' and cm.SessionName='" + Session["SessionName"].ToString() + "'";
        //    sql +=  " and sm.SessionName='" + Session["SessionName"].ToString() + "' and SG.SessionName='" + Session["SessionName"].ToString() + "' order by Name Asc";
        sql = "Select SG.SrNo,(SG.FirstName+' '+SG.MiddleName+' '+SG.LastName) as Name from StudentGenaralDetail SG";
        sql +=  " inner join StudentOfficialDetails SO on SG.SrNo=SO.SrNo";
        sql +=  " inner join ClassMaster cm on cm.Id=SO.AdmissionForClassId";
        sql +=  " inner join SectionMaster scm on scm.Id=SO.SectionId";
        sql +=  " where SO.AdmissionForClassId='" + drpclass.SelectedValue.ToString() + "' and Scm.SectionName='" + drpsection.SelectedItem.Text.ToString() + "'";
        sql +=  " and scm.ClassNameId='" + drpclass.SelectedValue.ToString() + "'";
        sql +=  " and scm.SessionName='" + Session["SessionName"].ToString() + "' and SO.SessionName='" + Session["SessionName"].ToString() + "'";
        sql +=  " and cm.SessionName='" + Session["SessionName"].ToString() + "'";
        sql +=  " and SG.SessionName='" + Session["SessionName"].ToString() + "'";
        sql +=  " and SO.Withdrwal is null  order by Name Asc";
        if (drpEval.SelectedIndex == 0)
        {
            GridView2.DataSource = null;
            GridView2.DataBind();
            GridView1.DataSource = oo.GridFill(sql);
            GridView1.DataBind();
        }
        else
        {
            GridView1.DataSource = null;
            GridView1.DataBind();
            GridView2.DataSource = oo.GridFill(sql);
            GridView2.DataBind();
        }

        if (GridView1.Rows.Count > 0)
        {

            for (int i = 0; i < GridView1.Rows.Count; i++)
            {

                loadFA1DataE1(i, "EVALUATION1", "Label2", "Label6", "Label9", "Label10", "Label19", "Label20", "Label21");
                loadFA1Data(i, "EVALUATION2", "Label2", "Label25", "Label28", "Label38", "Label39", "Label40");
                loadFA1Data(i, "EVALUATION3", "Label2", "Label46", "Label49", "Label59", "Label60", "Label61");
            }

        }
        if (GridView2.Rows.Count > 0)
        {
            hideColumn();
            for (int i = 0; i < GridView2.Rows.Count; i++)
            {
                loadFA1Data1(i, drpEval.SelectedItem.ToString().ToUpper(), "Label2", "Label6", "Label9", "Label10", "Label19", "Label20", "Label21");
            }

        }      
    }

    private void hideColumn()
    {
        Label lbl1 = (Label)GridView2.HeaderRow.FindControl("Label4");
        Label lbl2 = (Label)GridView2.HeaderRow.FindControl("Label7");
        Label lbl3 = (Label)GridView2.HeaderRow.FindControl("Label8");

        if (drpEval.SelectedIndex == 1)
        {
            GridView2.Columns[5].Visible = true;
            lbl1.Text = "MAY";
            lbl2.Text = "JULY";
            lbl3.Text = "HOLIDAYHW";
        }
        else
        {
            if (drpEval.SelectedIndex == 2)
            {
                lbl1.Text = "AUG";
                lbl2.Text = "SEP";
                lbl3.Text = "";
            }
            else if (drpEval.SelectedIndex == 3)
            {
                lbl1.Text = "DEC";
                lbl2.Text = "FEB";
                lbl3.Text = "";
            }
            GridView2.Columns[5].Visible = false;
        }


    }

    public void loadFA1DataE1(int i, string Eval, string Srno, string text1, string text2, string text3, string total, string percent, string grade)
    {
        Label srno = (Label)GridView1.Rows[i].FindControl(Srno);
        Label TEST1 = (Label)GridView1.Rows[i].FindControl(text1);
        Label TEST2 = (Label)GridView1.Rows[i].FindControl(text2);
        Label TEST3 = (Label)GridView1.Rows[i].FindControl(text3);
        Label Total = (Label)GridView1.Rows[i].FindControl(total);
        Label Percent = (Label)GridView1.Rows[i].FindControl(percent);
        Label Grade = (Label)GridView1.Rows[i].FindControl(grade);

        sql = "Select Test1,Test2,Test3,Total,Per,Grade from CCEItoV where SrNo='" + srno.Text + "' and Evaluation='" + Eval + "' and ClassId='" + drpclass.SelectedValue.ToString() + "' and SessionName='" + Session["SessionName"].ToString() + "' and SubjectId='" + drpSubject.SelectedValue.ToString() + "'";



        TEST1.Text = oo.ReturnTag(sql, "Test1").ToString();
        TEST2.Text = oo.ReturnTag(sql, "Test2").ToString();
        TEST3.Text = oo.ReturnTag(sql, "Test3").ToString();
        Total.Text = oo.ReturnTag(sql, "Total").ToString();
        Percent.Text = oo.ReturnTag(sql, "Per").ToString();
        Grade.Text = oo.ReturnTag(sql, "Grade").ToString();

    }

    public void loadFA1Data(int i, string Eval, string Srno, string text1, string text2, string total, string percent, string grade)
    {
        Label srno = (Label)GridView1.Rows[i].FindControl(Srno);
        Label TEST1 = (Label)GridView1.Rows[i].FindControl(text1);
        Label TEST2 = (Label)GridView1.Rows[i].FindControl(text2);

        Label Total = (Label)GridView1.Rows[i].FindControl(total);
        Label Percent = (Label)GridView1.Rows[i].FindControl(percent);
        Label Grade = (Label)GridView1.Rows[i].FindControl(grade);

        sql = "Select Test1,Test2,Total,Per,Grade from CCEItoV where SrNo='" + srno.Text + "' and Evaluation='" + Eval + "' and ClassId='" + drpclass.SelectedValue.ToString() + "' and SessionName='" + Session["SessionName"].ToString() + "' and SubjectId='" + drpSubject.SelectedValue.ToString() + "'";



        TEST1.Text = oo.ReturnTag(sql, "Test1").ToString();
        TEST2.Text = oo.ReturnTag(sql, "Test2").ToString();

        Total.Text = oo.ReturnTag(sql, "Total").ToString();
        Percent.Text = oo.ReturnTag(sql, "Per").ToString();
        Grade.Text = oo.ReturnTag(sql, "Grade").ToString();

    }

    public void loadFA1Data1(int i, string Eval, string Srno, string text1, string text2,string text3, string total, string percent, string grade)
    {
        Label srno = (Label)GridView2.Rows[i].FindControl(Srno);
        Label TEST1 = (Label)GridView2.Rows[i].FindControl(text1);
        Label TEST2 = (Label)GridView2.Rows[i].FindControl(text2);
        Label TEST3 = (Label)GridView2.Rows[i].FindControl(text3);
        Label Total = (Label)GridView2.Rows[i].FindControl(total);
        Label Percent = (Label)GridView2.Rows[i].FindControl(percent);
        Label Grade = (Label)GridView2.Rows[i].FindControl(grade);

        sql = "Select Test1,Test2,Test3,Total,Per,Grade from CCEItoV where SrNo='" + srno.Text + "' and Evaluation='" + Eval + "' and ClassId='" + drpclass.SelectedValue.ToString() + "' and SessionName='" + Session["SessionName"].ToString() + "' and SubjectId='" + drpSubject.SelectedValue.ToString() + "'";



        TEST1.Text = oo.ReturnTag(sql, "Test1").ToString();
        TEST2.Text = oo.ReturnTag(sql, "Test2").ToString();
        TEST3.Text = oo.ReturnTag(sql, "Test3").ToString();
        Total.Text = oo.ReturnTag(sql, "Total").ToString();
        Percent.Text = oo.ReturnTag(sql, "Per").ToString();
        Grade.Text = oo.ReturnTag(sql, "Grade").ToString();

    }

    protected void drpSubject_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadgrid();
    }

    protected void drpEval_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadgrid();
    }


    protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            //Creating a gridview object            
            GridView objGridView = (GridView)sender;

            //Creating a gridview row object
            GridViewRow objgridviewrow = new GridViewRow(1, 5, DataControlRowType.Header, DataControlRowState.Insert);

            //Creating a table cell object
            TableCell objtablecell = new TableCell();

            #region Merge cells

            AddMergedCells(objgridviewrow, objtablecell, 1, "#", System.Drawing.Color.LightGray.Name);
            AddMergedCells(objgridviewrow, objtablecell, 1, "S.R.No.", System.Drawing.Color.LightGray.Name);
            AddMergedCells(objgridviewrow, objtablecell, 1, "Student's Name", System.Drawing.Color.LightGray.Name);
            AddMergedCells(objgridviewrow, objtablecell, 6, "EVALUATION1", System.Drawing.Color.LightGray.Name);
            //AddMergedCells(objgridviewrow, objtablecell, 1, "", System.Drawing.Color.LightGray.Name);
            AddMergedCells(objgridviewrow, objtablecell, 5, "EVALUATION2", System.Drawing.Color.LightGray.Name);
            //AddMergedCells(objgridviewrow, objtablecell, 1, "", System.Drawing.Color.LightGray.Name);
            AddMergedCells(objgridviewrow, objtablecell, 5, "EVALUATION3", System.Drawing.Color.LightGray.Name);
            //AddMergedCells(objgridviewrow, objtablecell, 1, "G.TOTAL", System.Drawing.Color.LightGray.Name);

            objGridView.Controls[0].Controls.AddAt(0, objgridviewrow);

            #endregion
        }
    }

    protected void GridView2_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            //Creating a gridview object            
            GridView objGridView = (GridView)sender;

            //Creating a gridview row object
            GridViewRow objgridviewrow = new GridViewRow(1, 5, DataControlRowType.Header, DataControlRowState.Insert);

            //Creating a table cell object
            TableCell objtablecell = new TableCell();

            #region Merge cells

            AddMergedCells(objgridviewrow, objtablecell, 1, "#", System.Drawing.Color.LightGray.Name);
            AddMergedCells(objgridviewrow, objtablecell, 1, "S.R.No.", System.Drawing.Color.LightGray.Name);
            AddMergedCells(objgridviewrow, objtablecell, 1, "Student's Name", System.Drawing.Color.LightGray.Name);
            AddMergedCells(objgridviewrow, objtablecell, 6, drpEval.SelectedItem.ToString().ToUpper(), System.Drawing.Color.LightGray.Name);
            objGridView.Controls[0].Controls.AddAt(0, objgridviewrow);

            #endregion
        }
    }

    protected void AddMergedCells(GridViewRow objgridviewrow, TableCell objtablecell, int colspan, string celltext, string backcolor)
    {
        objtablecell = new TableCell();
        objtablecell.Text = celltext;
        objtablecell.ColumnSpan = colspan;
        objtablecell.CssClass = "vd_bg-blue vd_white text-center";
        //objtablecell.Style.Add("background-color", backcolor);
        objtablecell.HorizontalAlign = HorizontalAlign.Center;
        objgridviewrow.Cells.Add(objtablecell);
    }


    protected void ImageButton1_Click(object sender, EventArgs e)
    {
        if (GridView1.Rows.Count > 0)
        {
            oo.ExportToWord(Response, "AdmisionFormCollection.doc", divExport);
        }
    }
    protected void ImageButton2_Click(object sender, EventArgs e)
    {
        if (GridView1.Rows.Count > 0)
        {
            oo.ExportToExcel("AdmisionFormCollection.xls", GridView1);
        }
    }
    protected void ImageButton3_Click(object sender, EventArgs e)
    {

    }
    protected void ImageButton4_Click(object sender, EventArgs e)
    {
        PrintHelper_New.ctrl = abc;
        ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>var winpop=window.open('../Print_New.aspx','_blank'); if(!winpop || winpop.closed){alert('Please allow pop-up blocker from browser settings!')}</script>");
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }
    protected void drpSubjectGroup_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadSubject();
    }
}