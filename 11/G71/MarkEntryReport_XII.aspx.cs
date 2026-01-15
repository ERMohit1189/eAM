using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

public partial class staff_MarkEntryReportForXI_XII : System.Web.UI.Page
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
        con = oo.dbGet_connection();
        if (!IsPostBack)
        {
            if (Session["SessionName"].ToString() == "2015-2016" || Session["SessionName"].ToString() == "2016-2017")
            {
                Response.Redirect("MarkEntryReportXII_1617.aspx?check=MarkEntryReport_XII");
            }
            else
            {
                Response.Redirect("MarksEntryReportXII_1718.aspx?check=MarkEntryReport_XII");
            }
            Image1.ImageUrl = ResolveClientUrl("~/DisplayImage.ashx?UserLoginID=" + 1);
            sql = "Select CollegeName from CollegeMaster where CollegeId=" + 1;
            lblCollegeName.Text = oo.ReturnTag(sql, "CollegeName");
            Label1.Text = Label1.Text + " " + Session["SessionName"].ToString();   
            loadclass();
            loadBranch();
            loadsection();
            loadSubjectGroup();

        }
    }

    private void loadSubjectGroup()
    {
        if (Session["Logintype"].ToString() == "Admin")
        {
            sql = "Select SubjectGroup,Id from SubjectGroupMaster where ClassId='" + drpclass.SelectedValue.ToString() + "' and SessionName='" + Session["SessionName"].ToString() + "' and IsForOnlyExam=1";

            oo.FillDropDown_withValue(sql, drpSubjectGroup, "SubjectGroup", "Id");
            drpSubjectGroup.Items.Insert(0, "<--Select-->");
        }
        else
        {
            sql = "Select Distinct sgm.SubjectGroup,sgm.Id from SubjectClassTeacherMaster sctm";
            sql +=  " inner join SubjectMaster sm on sm.Id=sctm.Subjectid and sm.SessionName=sctm.SessionName";
            sql +=  " Left join S02_SubjectPaperMaster spm on spm.S02ID=sm.PaperID and spm.SessionName=sctm.SessionName";
            sql +=  " Inner join SubjectGroupMaster sgm on sgm.Id=spm.SubjectGroupID and sgm.SessionName=sctm.SessionName";
            sql +=  " where Ecode='" + Session["LoginName"].ToString() + "' and sctm.ClassId='" + drpclass.SelectedValue.ToString() + "'  ";
            sql +=  " and sctm.SectionName='" + drpsection.SelectedItem.ToString() + "' and sctm.SessionName='" + Session["SessionName"].ToString() + "'";
            sql +=  " and (IsForExam is null or IsForExam ='1')";

            oo.FillDropDown_withValue(sql, drpSubjectGroup, "SubjectGroup", "Id");
            drpSubjectGroup.Items.Insert(0, "<--Select-->");
        }
    }
    public void loadclass()
    {
        if (Session["Logintype"].ToString() == "Admin")
        {
            sql = "Select Distinct ClassName,cm.Id as Id,CIDOrder from ClassMaster cm ";
            sql +=  " inner join dt_ClassGroupMaster t1 on t1.ClassId=cm.Id";
            sql +=  " where cm.SessionName='" + Session["SessionName"] + "' and GroupId='G7' Order by CIDOrder";
            oo.FillDropDown_withValue(sql, drpclass, "ClassName", "Id");
        }
        else
        {
            sql = "Select Distinct ClassName,sctm.ClassId as Id,CIDOrder from SubjectClassTeacherMaster sctm";
            sql +=  " inner join ClassMaster cm on cm.Id=sctm.ClassId";
            sql +=  " where cm.SessionName='" + Session["SessionName"] + "' and sctm.SessionName='" + Session["SessionName"] + "' and Ecode='" + Session["LoginName"].ToString() + "' Order by CIDOrder";
            oo.FillDropDown_withValue(sql, drpclass, "ClassName", "Id");

        }
    }
    //--For Admin------------------------------------------------------------------------------------------------------------
    //public void loadclass()
    //{
    //    sql = "Select Distinct ClassName,cm.Id as Id,CIDOrder from ClassMaster cm ";
    //    sql +=  " inner join dt_ClassGroupMaster t1 on t1.ClassId=cm.Id";
    //    sql +=  " where cm.SessionName='" + Session["SessionName"] + "' and GroupId='G6' Order by CIDOrder";
    //    oo.FillDropDown_withValue(sql, drpclass, "ClassName", "Id");
    //}

   
    public void loadSubject()
    {
        if (Session["Logintype"].ToString() == "Admin")
        {
            sql = "Select SubjectName,Id from SubjectMaster where GroupId='" + drpSubjectGroup.SelectedValue.ToString() + "' and SessionName='" + Session["SessionName"].ToString() + "'";

            oo.FillDropDown_withValue(sql, drpSubject, "SubjectName", "Id");
            drpSubject.Items.Insert(0, "<--Select-->");
        }
        else
        {
            sql = "Select Distinct sm.SubjectName,sctm.Subjectid as Id from SubjectClassTeacherMaster sctm";
            sql +=  " inner join SubjectMaster sm on sm.Id=sctm.Subjectid";
            sql +=  " Left join S02_SubjectPaperMaster spm on spm.S02ID=sm.PaperID";
            sql +=  " where sctm.ClassId='" + drpclass.SelectedValue.ToString() + "'  ";
            sql +=  " and sctm.SectionName='" + drpsection.SelectedItem.ToString() + "' and sctm.SessionName='" + Session["SessionName"].ToString() + "'";
            sql +=  " and (IsForExam is null or IsForExam ='1') and sctm.Ecode='" + Session["LoginName"].ToString() + "'  and spm.SubjectGroupID='" + drpSubjectGroup.SelectedValue.ToString() + "'";
            oo.FillDropDown_withValue(sql, drpSubject, "SubjectName", "Id");
            drpSubject.Items.Insert(0, "<--Select-->");
        }
    }
    public void loadsection()
    {
        if (Session["Logintype"].ToString() == "Admin")
        {
            sql = "Select SectionName from SectionMaster where ClassNameId='" + drpclass.SelectedValue.ToString() + "' and SessionName='" + Session["SessionName"].ToString() + "'";
            oo.FillDropDown(sql, drpsection, "SectionName");

        }
        else
        {
            sql = "Select sm.SectionName from SubjectClassTeacherMaster sctm";
            sql +=  " inner join SectionMaster sm on sm.ClassNameId=sctm.ClassId and sm.SessionName=sctm.SessionName ";
            sql +=  " and sm.SectionName=sctm.SectionName";
            sql +=  " where sctm.BranchCode=" + Session["BranchCode"].ToString() + " and sctm.SessionName='" + Session["SessionName"].ToString() + "' and ClassId='" + drpclass.SelectedValue.ToString() + "' and Ecode='" + Session["LoginName"].ToString() + "'";

            oo.FillDropDown(sql, drpsection, "SectionName");
        }
    }

    private void loadBranch()
    {
        if (Session["Logintype"].ToString() == "Admin")
        {
            sql = "Select BranchName,Id from BranchMaster where ClassId='" + drpclass.SelectedValue.ToString() + "'";
            sql +=  " and   BranchCode=" + Session["BranchCode"].ToString() + " and SessionName='" + Session["SessionName"].ToString() + "'";
            oo.FillDropDown_withValue(sql, drpBranch, "BranchName", "Id");
        }
        else
        {
            if (Session["SessionName"].ToString() == "2015-2016")
            {
                sql = "Select BranchName,Id from BranchMaster Where ClassId='" + drpclass.SelectedValue.ToString() + "' and SessionName='" + Session["SessionName"].ToString() + "'";
                oo.FillDropDown_withValue(sql, drpBranch, "BranchName", "Id");
                drpBranch.Items.Insert(0, new ListItem("<--Select-->", "0"));
            }
            else
            {
                sql = "Select BranchName,bm.Id from ClassTeacherMaster T1";
                sql +=  "   inner join BranchMaster bm on bm.Id=T1.BranchId and bm.SessionName=t1.SessionName";
                sql +=  "   where EmpCode='" + Session["LoginName"].ToString() + "' and IsClassTeacher=1 and T1.SessionName='" + Session["SessionName"] + "' and T1.Classid='" + drpclass.SelectedValue.ToString() + "'";
                oo.FillDropDown_withValue(sql, drpBranch, "BranchName", "Id");
                drpBranch.Items.Insert(0, new ListItem("<--Select-->", "0"));
            }
        }
    }
    protected void drpclass_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadBranch();
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
        try
        {
            sql = "Select SG.SrNo,(SG.FirstName+' '+SG.MiddleName+' '+SG.LastName) as Name from StudentGenaralDetail SG";
            sql +=  " inner join StudentOfficialDetails SO on SG.SrNo=SO.SrNo";
            sql +=  " inner join ClassMaster cm on cm.Id=SO.AdmissionForClassId";
            sql +=  " inner join SectionMaster scm on scm.Id=SO.SectionId";
            sql +=  " where SO.AdmissionForClassId='" + drpclass.SelectedValue.ToString() + "'  and (so.Branch='" + drpBranch.SelectedValue.ToString() + "' or so.Branch is null)  and SO.SectionId='" + drpsection.SelectedValue.ToString() + "' and scm.ClassNameId='" + drpclass.SelectedValue.ToString() + "'";
            sql +=  " and scm.SessionName='" + Session["SessionName"].ToString() + "' and SO.SessionName='" + Session["SessionName"].ToString() + "' and cm.SessionName='" + Session["SessionName"].ToString() + "'";
            sql +=  " and SG.SessionName='" + Session["SessionName"].ToString() + "' and SO.Withdrwal is null order by Name Asc";
            if (drpEval.SelectedValue.ToUpper() == "ALL")
            {
                GridView2.DataSource = null;
                GridView2.DataBind();
                GridView3.DataSource = null;
                GridView3.DataBind();
                GridView1.DataSource = oo.GridFill(sql);
                GridView1.DataBind();
            }
            else if (drpEval.SelectedValue == "FA1" || drpEval.SelectedValue == "FA2" || drpEval.SelectedValue == "P1" || drpEval.SelectedValue == "P2")
            {
                GridView1.DataSource = null;
                GridView1.DataBind();
                GridView3.DataSource = null;
                GridView3.DataBind();
                GridView2.DataSource = oo.GridFill(sql);
                GridView2.DataBind();

            }
            else if (drpEval.SelectedValue == "HY" || drpEval.SelectedValue == "AE")
            {
                GridView1.DataSource = null;
                GridView1.DataBind();
                GridView2.DataSource = null;
                GridView2.DataBind();
                GridView3.DataSource = oo.GridFill(sql);
                GridView3.DataBind();
            }
            else
            {
                GridView1.DataSource = null;
                GridView1.DataBind();
                GridView2.DataSource = null;
                GridView2.DataBind();
                GridView3.DataSource = null;
                GridView3.DataBind();
            }

            if (GridView1.Rows.Count > 0)
            {
                for (int i = 0; i < GridView1.Rows.Count; i++)
                {
                    loadFAData(GridView1, i, "FA1", "Label2", "Label4", "Label5", "Label6", "Label7", "Label8", "Label9", "Label11", "Label15", "Label17", "Label20","Label21", "Label22");
                    loadFAData(GridView1, i, "FA2", "Label2", "Label24", "Label25", "Label26", "Label27", "Label28", "Label29", "Label31", "Label35", "Label37", "Label40","Label41", "Label42");
                    loadFAData(GridView1, i, "P1", "Label2", "Label59", "Label60", "Label61", "Label62", "Label63", "Label64", "Label66", "Label70", "Label72", "Label75","Label76", "Label77");
                    loadFAData(GridView1, i, "P2", "Label2", "Label79", "Label80", "Label81", "Label82", "Label83", "Label84", "Label86", "Label90", "Label92", "Label95","Label96", "Label97");
                    loadSAData(GridView1, i, "HY", "Label2", "Label45", "Label47", "Label49", "Label51", "Label53", "Label55", "Label57");
                    loadSAData(GridView1, i, "AE", "Label2", "Label100", "Label102", "Label104", "Label106", "Label108", "Label110", "Label112");
                }
            }
            else if (GridView2.Rows.Count > 0)
            {
                for (int i = 0; i < GridView2.Rows.Count; i++)
                {
                    loadFAData(GridView2, i, drpEval.SelectedItem.ToString().ToUpper(), "Label2", "Label4", "Label5", "Label6", "Label7", "Label8", "Label9", "Label11", "Label15", "Label17", "Label20","Label21", "Label22");
                }
            }
            else if (GridView3.Rows.Count > 0)
            {
                for (int i = 0; i < GridView3.Rows.Count; i++)
                {
                    loadSAData(GridView3, i, drpEval.SelectedItem.ToString().ToUpper(), "Label2", "Label5", "Label7", "Label9", "Label11", "Label13", "Label15", "Label17");
                }
            }
        }
        catch
        {
        }

    }
    public void loadFAData(GridView gv,int i, string Eval, string Srno, string test1, string test2, string test3, string test4, string test5, string test6,string total, string act, string ca, string att, string grtotal, string tenpercent)
    {
        Label srno = (Label)gv.Rows[i].FindControl(Srno);
        Label Test1 = (Label)gv.Rows[i].FindControl(test1);
        Label Test2 = (Label)gv.Rows[i].FindControl(test2);
        Label Test3 = (Label)gv.Rows[i].FindControl(test3);
        Label Test4 = (Label)gv.Rows[i].FindControl(test4);
        Label Test5 = (Label)gv.Rows[i].FindControl(test5);
        Label Test6 = (Label)gv.Rows[i].FindControl(test6);
        Label TOTAL = (Label)gv.Rows[i].FindControl(total);
        Label ACT = (Label)gv.Rows[i].FindControl(act);
        Label CA = (Label)gv.Rows[i].FindControl(ca);
        Label ATT = (Label)gv.Rows[i].FindControl(att);
        Label GRTotal = (Label)gv.Rows[i].FindControl(grtotal);
        Label TenPercent = (Label)gv.Rows[i].FindControl(tenpercent);


        sql = "Select Test1,Test2,Test3,Test4,Test5,Test6,Total,ACT,CA,ATT,GrTotal,TenPercent from CCEXItoXII where SrNo='" + srno.Text + "' and Evaluation='" + Eval + "' and ClassId='" + drpclass.SelectedValue.ToString() + "' and (BranchId='" + drpBranch.SelectedValue.ToString() + "' or BranchId is null) and SessionName='" + Session["SessionName"].ToString() + "' and SubjectId='" + drpSubject.SelectedValue.ToString() + "'";



        Test1.Text = oo.ReturnTag(sql, "Test1").ToString();
        Test2.Text = oo.ReturnTag(sql, "Test2").ToString();
        Test3.Text = oo.ReturnTag(sql, "Test3").ToString();
        Test4.Text = oo.ReturnTag(sql, "Test4").ToString();
        Test5.Text = oo.ReturnTag(sql, "Test5").ToString();
        Test6.Text = oo.ReturnTag(sql, "Test6").ToString();
        TOTAL.Text = oo.ReturnTag(sql, "total").ToString();
        ACT.Text = oo.ReturnTag(sql, "ACT").ToString();
        CA.Text = oo.ReturnTag(sql, "CA").ToString();
        ATT.Text = oo.ReturnTag(sql, "ATT").ToString();
        GRTotal.Text = oo.ReturnTag(sql, "GrTotal").ToString();
        TenPercent.Text = oo.ReturnTag(sql, "TenPercent").ToString();
        

    }
    public void loadSAData(GridView gv, int i, string Eval, string Srno, string tewntyper, string prac, string thory, string pracplusthy, string eightperofpracandthy, string eightplustwenty, string hundredper)
    {
        Label srno = (Label)gv.Rows[i].FindControl(Srno);
        Label lbltewntyper = (Label)gv.Rows[i].FindControl(tewntyper);
        Label lblprac = (Label)gv.Rows[i].FindControl(prac);
        Label lblthory = (Label)gv.Rows[i].FindControl(thory);
        Label lblpracplusthy = (Label)gv.Rows[i].FindControl(pracplusthy);
        Label lbleightperofpracandthy = (Label)gv.Rows[i].FindControl(eightperofpracandthy);
        Label lbleightplustwenty = (Label)gv.Rows[i].FindControl(eightplustwenty);
        Label lblhundredper = (Label)gv.Rows[i].FindControl(hundredper);


        sql = "Select Twentyper,Prc,Test1,PrcplusTh,EightperofPrcplusTh,Eightplustwentyper,HundPer from CCEXItoXII where SrNo='" + srno.Text + "' and Evaluation='" + Eval + "' and ClassId='" + drpclass.SelectedValue.ToString() + "' and (BranchId='" + drpBranch.SelectedValue.ToString() + "' or BranchId is null) and SessionName='" + Session["SessionName"].ToString() + "' and SubjectId='" + drpSubject.SelectedValue.ToString() + "'";


        lbltewntyper.Text = oo.ReturnTag(sql, "Twentyper");
        lblprac.Text = oo.ReturnTag(sql, "Prc");
        lblthory.Text = oo.ReturnTag(sql, "Test1");
        lblpracplusthy.Text = oo.ReturnTag(sql, "PrcplusTh");
        lbleightperofpracandthy.Text = oo.ReturnTag(sql, "EightperofPrcplusTh");
        lbleightplustwenty.Text = oo.ReturnTag(sql, "Eightplustwentyper");
        lblhundredper.Text = oo.ReturnTag(sql, "HundPer");

    }        
    protected void drpSubject_SelectedIndexChanged(object sender, EventArgs e)
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
            GridViewRow objgridviewrow = new GridViewRow(1, 4, DataControlRowType.Header, DataControlRowState.Insert);

            //Creating a table cell object
            TableCell objtablecell = new TableCell();

            #region Merge cells

            //AddMergedCells(objgridviewrow, objtablecell, 1, "#", System.Drawing.Color.Snow.Name);
            //AddMergedCells(objgridviewrow, objtablecell, 1, "S.R.No.", System.Drawing.Color.Snow.Name);
            //AddMergedCells(objgridviewrow, objtablecell, 1, "Student's Name", System.Drawing.Color.Snow.Name);
            //AddMergedCells(objgridviewrow, objtablecell, 7, "FA1", System.Drawing.Color.Snow.Name);
            //AddMergedCells(objgridviewrow, objtablecell, 1, "", System.Drawing.Color.LightGray.Name);
            //AddMergedCells(objgridviewrow, objtablecell, 7, "FA2", System.Drawing.Color.Snow.Name);
            //AddMergedCells(objgridviewrow, objtablecell, 1, "", System.Drawing.Color.LightGray.Name);
            //AddMergedCells(objgridviewrow, objtablecell, 7, "HY", System.Drawing.Color.Snow.Name);
            //AddMergedCells(objgridviewrow, objtablecell, 1, "", System.Drawing.Color.LightGray.Name);
            //AddMergedCells(objgridviewrow, objtablecell, 7, "P1/FA3", System.Drawing.Color.Snow.Name);
            //AddMergedCells(objgridviewrow, objtablecell, 1, "", System.Drawing.Color.LightGray.Name);
            //AddMergedCells(objgridviewrow, objtablecell, 7, "P2/FA4", System.Drawing.Color.Snow.Name);
            //AddMergedCells(objgridviewrow, objtablecell, 1, "", System.Drawing.Color.LightGray.Name);
            //AddMergedCells(objgridviewrow, objtablecell, 7, "AE/SA2", System.Drawing.Color.Snow.Name);

            AddMergedCells1(objgridviewrow,"", objtablecell, 1, "#", System.Drawing.Color.Snow.Name);
            AddMergedCells1(objgridviewrow,"", objtablecell, 1, "S.R.No.", System.Drawing.Color.Snow.Name);
            AddMergedCells1(objgridviewrow, "tab-titel15", objtablecell, 1, "Student's Name", System.Drawing.Color.Snow.Name);
            AddMergedCells1(objgridviewrow, "tab-titel35", objtablecell, 7, "FA1", System.Drawing.Color.Snow.Name);
            AddMergedCells1(objgridviewrow, "", objtablecell, 1, "", System.Drawing.Color.LightGray.Name);
            AddMergedCells1(objgridviewrow, "tab-titel35", objtablecell, 7, "FA2", System.Drawing.Color.Snow.Name);
            AddMergedCells1(objgridviewrow, "", objtablecell, 1, "", System.Drawing.Color.LightGray.Name);
            AddMergedCells1(objgridviewrow, "tab-titel35", objtablecell, 7, "HY", System.Drawing.Color.Snow.Name);
            AddMergedCells1(objgridviewrow, "", objtablecell, 1, "", System.Drawing.Color.LightGray.Name);
            AddMergedCells1(objgridviewrow, "tab-titel35", objtablecell, 7, "P1/FA3", System.Drawing.Color.Snow.Name);
            AddMergedCells1(objgridviewrow, "", objtablecell, 1, "", System.Drawing.Color.LightGray.Name);
            AddMergedCells1(objgridviewrow, "tab-titel35", objtablecell, 7, "P2/FA4", System.Drawing.Color.Snow.Name);
            AddMergedCells1(objgridviewrow, "", objtablecell, 1, "", System.Drawing.Color.LightGray.Name);
            AddMergedCells1(objgridviewrow, "tab-titel35", objtablecell, 7, "AE/SA2", System.Drawing.Color.Snow.Name);

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
            GridViewRow objgridviewrow = new GridViewRow(1, 4, DataControlRowType.Header, DataControlRowState.Insert);

            //Creating a table cell object
            TableCell objtablecell = new TableCell();

            #region Merge cells

            AddMergedCells(objgridviewrow, objtablecell, 1, "#", System.Drawing.Color.Snow.Name);
            AddMergedCells(objgridviewrow, objtablecell, 1, "S.R.No.", System.Drawing.Color.Snow.Name);
            AddMergedCells(objgridviewrow, objtablecell, 1, "Student's Name", System.Drawing.Color.Snow.Name);
            AddMergedCells(objgridviewrow, objtablecell, 7, drpEval.SelectedItem.ToString().ToUpper(), System.Drawing.Color.Snow.Name);

            objGridView.Controls[0].Controls.AddAt(0, objgridviewrow);

            #endregion
        }
    }
    protected void GridView3_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            //Creating a gridview object            
            GridView objGridView = (GridView)sender;

            //Creating a gridview row object
            GridViewRow objgridviewrow = new GridViewRow(1, 4, DataControlRowType.Header, DataControlRowState.Insert);

            //Creating a table cell object
            TableCell objtablecell = new TableCell();

            #region Merge cells

            AddMergedCells(objgridviewrow, objtablecell, 1, "#", System.Drawing.Color.Snow.Name);
            AddMergedCells(objgridviewrow, objtablecell, 1, "S.R.No.", System.Drawing.Color.Snow.Name);
            AddMergedCells(objgridviewrow, objtablecell, 1, "Student's Name", System.Drawing.Color.Snow.Name);
            AddMergedCells(objgridviewrow, objtablecell, 7, drpEval.SelectedItem.ToString().ToUpper(), System.Drawing.Color.Snow.Name);

            objGridView.Controls[0].Controls.AddAt(0, objgridviewrow);

            #endregion
        }
    }

    protected void AddMergedCells1(GridViewRow objgridviewrow, string style, TableCell objtablecell, int colspan, string celltext, string backcolor)
    {
        objtablecell = new TableCell();
        objtablecell.Text = celltext;
        objtablecell.ColumnSpan = colspan;
        //objtablecell.Style.Add("background-color", backcolor);
        //objtablecell.Style.Add("font-weight", "bold");
        //objtablecell.Style.Add("font-size", "16px");
        //objtablecell.HorizontalAlign = HorizontalAlign.Left;
        objtablecell.CssClass = style;
        objgridviewrow.Cells.Add(objtablecell);
    }

    protected void AddMergedCells(GridViewRow objgridviewrow, TableCell objtablecell, int colspan, string celltext, string backcolor)
    {
        objtablecell = new TableCell();
        objtablecell.Text = celltext;
        objtablecell.ColumnSpan = colspan;
        objtablecell.Style.Add("background-color", backcolor);
        objtablecell.Style.Add("font-weight", "bold");
        objtablecell.Style.Add("font-size", "16px");
        objtablecell.HorizontalAlign = HorizontalAlign.Center;
        objgridviewrow.Cells.Add(objtablecell);
    }
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        if (GridView1.Rows.Count > 0)
        {
            oo.ExportToWord(Response, drpclass.SelectedItem.Text+"_"+ drpsection.SelectedItem.Text+"_"+drpEval.SelectedItem.Text+"_MarksEntryReportof"+drpSubject.SelectedItem.Text, divExport);
        }
    }
    protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
    {
        if (GridView1.Rows.Count > 0)
        {
            oo.ExportToExcel(drpclass.SelectedItem.Text + "_" + drpsection.SelectedItem.Text + "_" + drpEval.SelectedItem.Text + "_MarksEntryReportof" + drpSubject.SelectedItem.Text+".xls", GridView1);
        }
        else if (GridView1.Rows.Count > 0)
        {
            oo.ExportToExcel(drpclass.SelectedItem.Text + "_" + drpsection.SelectedItem.Text + "_" + drpEval.SelectedItem.Text + "_MarksEntryReportof" + drpSubject.SelectedItem.Text + ".xls", GridView2);
        }
        else if (GridView1.Rows.Count > 0)
        {
            oo.ExportToExcel(drpclass.SelectedItem.Text + "_" + drpsection.SelectedItem.Text + "_" + drpEval.SelectedItem.Text + "_MarksEntryReportof" + drpSubject.SelectedItem.Text + ".xls", GridView3);
        }
    }
    protected void ImageButton3_Click(object sender, ImageClickEventArgs e)
    {

    }
    protected void ImageButton4_Click(object sender, ImageClickEventArgs e)
    {
        PrintHelper_New.ctrl = abc;
        ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>var winpop=window.open('../Print_New.aspx','_blank'); if(!winpop || winpop.closed){alert('Please allow pop-up blocker from browser settings!')}</script>");
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }
    protected void drpEval_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadgrid();
    }
    protected void UpdatePanel2_Unload(object sender, EventArgs e)
    {
        oo.printGridWithinupdatepanel(sender,this.Page);
    }
    protected void UpdatePanel1_Unload(object sender, EventArgs e)
    {
        oo.printGridWithinupdatepanel(sender, this.Page);
    }
    protected void UpdatePanel3_Unload(object sender, EventArgs e)
    {
        oo.printGridWithinupdatepanel(sender, this.Page);
    }
    protected void drpBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadgrid();
    }
    protected void drpSubjectGroup_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadSubject();
    }
}