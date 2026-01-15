using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SubjectwiseCumlativeIXtoX : Page
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
        if (Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
        {
            Response.Redirect("~/default.aspx");
        }
        con = oo.dbGet_connection();
        Campus camp = new Campus(); camp.LoadLoader(loader);
        if (!IsPostBack)
        {
            loadclass();
            drpsection.Items.Insert(0, new ListItem("<--Select-->", ""));
            ddlMedium.Items.Insert(0, new ListItem("<--Select-->", ""));
            drpSubject.Items.Insert(0, new ListItem("<--Select-->", ""));
            drpPaper.Items.Insert(0, new ListItem("<--Select-->", ""));

        }
    }
    
    public void loadclass()
    {
        if (Session["Logintype"].ToString() == "Admin")
        {
            sql = "Select Distinct ClassName,cm.Id as Id,CIDOrder from ClassMaster cm ";
            sql +=  " inner join dt_ClassGroupMaster t1 on t1.ClassId=cm.Id";
            sql +=  " where cm.SessionName='" + Session["SessionName"] + "' and t1.BranchCode=" + Session["BranchCode"] + " and cm.BranchCode=" + Session["BranchCode"] + " and t1.SessionName='" + Session["SessionName"] + "' and GroupId='G5' Order by CIDOrder";
            oo.FillDropDown_withValue(sql, drpclass, "ClassName", "Id");
        }
        else
        {
            sql = "Select Distinct ClassName,sctm.ClassId as Id,CIDOrder from ICSESubjectTeacherAllotment sctm";
            sql +=  " inner join ClassMaster cm on cm.Id=sctm.ClassId";
            sql +=  " inner join dt_ClassGroupMaster t1 on t1.ClassId=sctm.ClassId";
            sql +=  " where GroupId='G5'  and cm.SessionName='" + Session["SessionName"].ToString() + "' and cm.BranchCode=" + Session["BranchCode"] + " and t1.BranchCode=" + Session["BranchCode"] + " and sctm.BranchCode=" + Session["BranchCode"] + " and t1.SessionName='" + Session["SessionName"] + "' and sctm.SessionName='" + Session["SessionName"] + "' and Ecode='" + Session["LoginName"].ToString() + "' Order by CIDOrder";
            oo.FillDropDown_withValue(sql, drpclass, "ClassName", "Id");
        }
        drpclass.Items.Insert(0, new ListItem("<--Select-->", "0"));
    }
    public void loadsection()
    {
        drpsection.Items.Clear();
        if (Session["Logintype"].ToString() == "Admin")
        {
            sql = "Select SectionName,id from SectionMaster where ClassNameId='" + drpclass.SelectedValue.ToString() + "' and BranchCode=" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"].ToString() + "'";
            oo.FillDropDown_withValue(sql, drpsection, "SectionName", "Id");
            drpsection.Items.Insert(0, new ListItem("<--Select-->", "0"));

        }
        else
        {
            sql = "Select Distinct sm.SectionName,sm.id from ICSESubjectTeacherAllotment sctm";
            sql +=  " inner join SectionMaster sm on sm.ClassNameId=sctm.ClassId and sm.id=sctm.SectionId ";
            sql +=  " and sm.SectionName=sctm.SectionName";
            sql +=  " where sctm.BranchCode=" + Session["BranchCode"].ToString() + " and sm.BranchCode=" + Session["BranchCode"] + " and sctm.BranchCode=" + Session["BranchCode"] + " and sctm.SessionName='" + Session["SessionName"].ToString() + "' and sctm.ClassId='" + drpclass.SelectedValue.ToString() + "' and Ecode='" + Session["LoginName"].ToString() + "'";
            oo.FillDropDown_withValue(sql, drpsection, "SectionName", "Id");
            drpsection.Items.Insert(0, new ListItem("<--Select-->", "0"));
        }
    }
    protected void loadMedium()
    {
        ddlMedium.Items.Clear();
        sql = "Select Medium,Id from MediumMaster where BranchCode=" + Session["BranchCode"].ToString() + " and SessionName='" + Session["SessionName"].ToString() + "'";
        oo.FillDropDown_withValue(sql, ddlMedium, "Medium", "Id");
        ddlMedium.Items.Insert(0, new ListItem("<--Select-->", ""));
    }
    private void loadSubject()
    {
        drpSubject.Items.Clear();
        if (Session["Logintype"].ToString() == "Admin")
        {
            sql = "Select SubjectName,Id from ttSubjectMaster where ClassId='" + drpclass.SelectedValue.ToString() + "' and BranchCode=" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"].ToString() + "' and ApplicableFor<>'TimeTable' and medium='" + ddlMedium.SelectedItem.Text + "'";

            oo.FillDropDown_withValue(sql, drpSubject, "SubjectName", "Id");
            drpSubject.Items.Insert(0, "<--Select-->");
        }
        else
        {
            sql = "Select  Distinct SubjectName,sm.Id from ICSESubjectTeacherAllotment sctm";
            sql +=  " inner join TTSubjectMaster sm on sm.Id=sctm.Subjectid and sm.ClassId=sctm.ClassId and sm.BranchId=sctm.BranchId and sm.SessionName=sctm.SessionName  and sm.BranchCode=sctm.BranchCode and sctm.medium=sm.medium";
            sql +=  " where Ecode='" + Session["LoginName"].ToString() + "'  and ApplicableFor<>'TimeTable' and sctm.ClassId=" + drpclass.SelectedValue.ToString() + " ";
            sql +=  " and  sctm.BranchCode = " + Session["BranchCode"] + " and sctm.SessionName='" + Session["SessionName"].ToString() + "' and sm.medium='" + ddlMedium.SelectedItem.Text + "'";

            oo.FillDropDown_withValue(sql, drpSubject, "SubjectName", "Id");
            drpSubject.Items.Insert(0, "<--Select-->");
        }
    }
    public void loadPaper()
    {
        drpPaper.Items.Clear();
        if (Session["Logintype"].ToString() == "Admin")
        {
            sql = "Select PaperName,Id from ttpapermaster where SubjectId='" + drpSubject.SelectedValue.ToString() + "' and Classid=" + drpclass.SelectedValue.ToString() + " and BranchCode=" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"] + "' and medium='"+ddlMedium.SelectedItem.Text+"' order by id";

            oo.FillDropDown_withValue(sql, drpPaper, "PaperName", "Id");
            drpPaper.Items.Insert(0, "<--Select-->");
        }
        else
        {
            sql = "Select  Distinct pm.PaperName,pm.Id from ICSESubjectTeacherAllotment sctm";
            sql +=  " inner join TTSubjectMaster sm on sm.Id=sctm.Subjectid and sm.ClassId=sctm.ClassId and sm.BranchId=sctm.BranchId and sm.SessionName=sctm.SessionName  and sm.BranchCode=sctm.BranchCode  and sctm.medium=sm.medium";
            sql +=  " inner join ttpapermaster pm on pm.Id=sm.Subjectid and pm.ClassId=sctm.ClassId and pm.BranchId=sctm.BranchId and pm.SessionName=sctm.SessionName  and pm.BranchCode=sctm.BranchCode and sctm.medium=Pm.medium";
            sql +=  " where Ecode='" + Session["LoginName"].ToString() + "'  and ApplicableFor<>'TimeTable' and sctm.ClassId=" + drpclass.SelectedValue.ToString() + " and pm.SubjectId='" + drpSubject.SelectedValue.ToString() + " ";
            sql +=  " and  sctm.BranchCode = " + Session["BranchCode"] + " and sctm.SessionName='" + Session["SessionName"].ToString() + "' and sctm.medium='" + ddlMedium.SelectedItem.Text + "'";

            oo.FillDropDown_withValue(sql, drpSubject, "PaperName", "Id");
            drpSubject.Items.Insert(0, "<--Select-->");
        }
    }
    protected void drpclass_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadsection();
        loadMedium();
    }
    protected void drpEval_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadSubject();
    }
    protected void drpSubject_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadPaper();
    }
    protected void lnkView_Click(object sender, EventArgs e)
    {
        loadgrid();
    }
    public void loadgrid()
    {
        cmd.Connection = con;
        cmd.CommandText = "SubjectWiseCumlativeIXtoX_UP";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@ClassId", drpclass.SelectedValue);
        cmd.Parameters.AddWithValue("@SectionId", drpsection.SelectedValue);
        cmd.Parameters.AddWithValue("@SubjectId", drpSubject.SelectedValue);
        cmd.Parameters.AddWithValue("@PaperId", drpPaper.SelectedValue);
        cmd.Parameters.AddWithValue("@TermName", drpEval.SelectedValue);
        cmd.Parameters.AddWithValue("@Medium", ddlMedium.SelectedItem.Text);
        cmd.Parameters.AddWithValue("@ClassName", drpclass.SelectedItem.Text);
        cmd.Parameters.AddWithValue("@sessionName", Session["SessionName"]);
        cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"]);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        cmd.Parameters.Clear();
        if (ds != null && ds.Tables.Count > 1)
        {
            divExport.Visible = true;
            GridView1.DataSource = ds.Tables[1];
            GridView1.DataBind();
            GridView1.Columns[4].Visible = false;
            var MonthlyTesth = (Label)GridView1.HeaderRow.FindControl("MonthlyTesth");
            var ut1h = (Label)GridView1.HeaderRow.FindControl("ut1");
            var ut2 = (Label)GridView1.HeaderRow.FindControl("ut2");
            var hy = (Label)GridView1.HeaderRow.FindControl("hy");
            var HYt = (Label)GridView1.HeaderRow.FindControl("HYt");
            if (drpEval.SelectedIndex == 1)
            {
                if (drpclass.SelectedItem.Text=="IX")
                {
                    ut1h.Text = "UT1 (" + ds.Tables[0].Rows[0]["UT1"].ToString()+")";
                    ut2.Text = "UT2 (" + ds.Tables[0].Rows[0]["UT2"].ToString() + ")";
                    hy.Text = "HY (" + ds.Tables[0].Rows[0]["HY"].ToString() + ")";
                    HYt.Text = "Total (" + ds.Tables[0].Rows[0]["HYTotal"].ToString() + ")";
                }
                if (drpclass.SelectedItem.Text == "X")
                {
                    GridView1.Columns[4].Visible = true;
                    MonthlyTesth.Text = "Monthly Test (" + ds.Tables[0].Rows[0]["MonthlyTest"].ToString() + ")";
                    ut1h.Text = "UT1 (" + ds.Tables[0].Rows[0]["UT1"].ToString() + ")";
                    ut2.Text = "UT2 (" + ds.Tables[0].Rows[0]["UT2"].ToString() + ")";
                    hy.Text = "HY (" + ds.Tables[0].Rows[0]["HY"].ToString() + ")";
                    HYt.Text = "Total (" + ds.Tables[0].Rows[0]["HYTotal"].ToString() + ")";
                }
            }
            if (drpEval.SelectedIndex == 2)
            {
                if (drpclass.SelectedItem.Text == "IX")
                {
                    ut1h.Text = "UT3 (" + ds.Tables[0].Rows[0]["UT1"].ToString() + ")";
                    ut2.Text = "UT4 (" + ds.Tables[0].Rows[0]["UT2"].ToString() + ")";
                    hy.Text = "AE (" + ds.Tables[0].Rows[0]["HY"].ToString() + ")";
                    HYt.Text = "Total (" + ds.Tables[0].Rows[0]["HYTotal"].ToString() + ")";
                }
                if (drpclass.SelectedItem.Text == "X")
                {
                    ut1h.Text = "Pre-1 (" + ds.Tables[0].Rows[0]["UT1"].ToString() + ")";
                    ut2.Text = "Pre-2 (" + ds.Tables[0].Rows[0]["UT2"].ToString() + ")";
                    hy.Text = "Pre-3 (" + ds.Tables[0].Rows[0]["HY"].ToString() + ")";
                    HYt.Text = "Total (" + ds.Tables[0].Rows[0]["HYTotal"].ToString() + ")";
                }
            }
            
        }
    }
    protected void ImageButton3_Click(object sender, EventArgs e)
    {
        oo.ExporttolandscapePdf(Response, "SubjectwiseCumlativeIXtoX", table1);
    }
    protected void ImageButton1_Click(object sender, EventArgs e)
    {
        oo.ExportTolandscapeWord(Response, "SubjectwiseCumlativeIXtoX", table1);
    }
    protected void ImageButton2_Click(object sender, EventArgs e)
    {
        oo.ExportDivToExcelWithFormatting(Response, "SubjectwiseCumlativeIXtoX.xls", table1, Server.MapPath("~/Admin/css/style.css"));
    }
    protected void ImageButton4_Click(object sender, EventArgs e)
    {
        PrintHelper_New.ctrl = table1;
        ScriptManager.RegisterClientScriptBlock(Page, GetType(), "onclick", "var winpop=window.open('../Print_New.aspx','_blank'); if(!winpop || winpop.closed){alert('Please allow pop-up blocker from browser settings!')}", true);
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }


    protected void drpsection_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadSubject();
        loadPaper();
    }

    protected void ddlMedium_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadSubject();
        loadPaper();
    }


}