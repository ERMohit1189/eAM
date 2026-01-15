using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_ClassTeacherList : Page
{
#pragma warning disable 169
    string sql = "";
#pragma warning restore 169
    Campus _oo = new Campus();
    private SqlConnection _con;
    DataTable dt;
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((string)Session["LoginName"] == "" || (string)Session["BranchCode"] == "" || (string)Session["SessionName"] == "" || Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
        {
            Response.Redirect("default.aspx");
        }
        _con = new SqlConnection();
        _con = _oo.dbGet_connection();
        Campus camp = new Campus(); camp.LoadLoader(loader);
        BLL.BLLInstance.LoadHeader("Report",header);
        if (!IsPostBack)
        {
            loadClass();
            BAL.objBal.fillSelectvalue(drpSection, "<--Select-->");
            BAL.objBal.fillSelectvalue(drpBranch, "<--Select-->");
        }
    }
    protected void drpClass_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadBranch();
        loadSection();
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        loadGrid();
    }
    protected void loadClass()
    {
        sql = "Select ClassName,Id from ClassMaster";
        sql = sql + "  where SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
        sql = sql + " order by CIDOrder";
        BAL.objBal.FillDropDown_withValue(sql, drpClass, "ClassName", "Id");
        drpClass.Items.Insert(0, new ListItem("<--Select-->", "<--Select-->"));
    }

    protected void loadBranch()
    {
        sql = "Select BranchName,Id from BranchMaster where ClassId=" + drpClass.SelectedValue.ToString();
        sql = sql + " and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";

        BAL.objBal.FillDropDown_withValue(sql, drpBranch, "BranchName", "Id");

        drpBranch.Items.Insert(0, new ListItem("<--Select-->", "<--Select-->"));

    }

    protected void loadSection()
    {
        sql = "Select SectionName,id from SectionMaster where ClassNameId=" + drpClass.SelectedValue.ToString();
        sql = sql + " and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";

        BAL.objBal.FillDropDown_withValue(sql, drpSection, "SectionName", "id");

        drpSection.Items.Insert(0, new ListItem("<--Select-->", "<--Select-->"));

    }
    private void loadGrid()
    {
        //DataSet ds = new DataSet();

        //List<SqlParameter> param = new List<SqlParameter>();
        //param.Add(new SqlParameter("@QueryFor", "S"));
        //if (drpClass.SelectedIndex>0)
        //{
        //    param.Add(new SqlParameter("@ClassId", drpClass.SelectedValue));
        //}
        //if (drpBranch.SelectedIndex > 0)
        //{
        //    param.Add(new SqlParameter("@BranchId", drpBranch.SelectedValue));
        //}
        //if (drpBranch.SelectedIndex > 0)
        //{
        //    param.Add(new SqlParameter("@SectionId", drpSection.SelectedValue));
        //}
        //param.Add(new SqlParameter("@SessionName", Session["SessionName"].ToString()));
        //param.Add(new SqlParameter("@BranchCode", Session["BranchCode"].ToString()));

        //ds = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("ClassTeacherMasterProcNew", param);

        //if (ds.Tables.Count > 0)
        //{
        //    heading.Text = "List of Class Teachers";
        //    pnlcontrols.Visible = true;
        //    abc.Visible = true;
        //    Grd1.DataSource = ds.Tables[0];
        //    Grd1.DataBind();
        //}


        sql = "Select ctm.Id,Eg.Ecode,Eg.EmpId,(EG.EFirstName+case when EG.EMiddleName=' ' then ' ' else ' '+EG.EMiddleName+' ' End+EG.ELastName) EmpName, ";
       sql = sql + " SectionName,Medium,ClassName,(case when bm.IsDisplay = 1 then BranchName else '' end) BranchName ";
       sql = sql + " from ClassTeacherMaster ctm ";
        sql = sql + " inner join EmpGeneralDetail Eg on ltrim(rtrim(Eg.ECode))= ltrim(rtrim(ctm.EmpCode)) and Eg.BranchCode = ctm.BranchCode ";
        sql = sql + " inner join ClassMaster cm on cm.Id = ctm.ClassId and cm.SessionName = ctm.SessionName and cm.BranchCode = ctm.BranchCode ";
        sql = sql + " inner join SectionMaster sm on sm.Id = ctm.SectionId ";
        sql = sql + "and sm.SessionName = ctm.SessionName and sm.BranchCode = ctm.BranchCode ";
        sql = sql + " inner join MediumMaster mm on ";
        sql = sql + " mm.BranchCode = ctm.BranchCode and mm.Id=ctm.MediumId ";
        sql = sql + " inner join BranchMaster bm on bm.Id = ctm.BranchId and bm.SessionName = ctm.SessionName ";
        sql = sql + " and bm.BranchCode = ctm.BranchCode ";
        sql = sql + " where ctm.SessionName = '" + Session["SessionName"].ToString() + "' and ctm.BranchCode = " + Session["BranchCode"].ToString() + " and IsClassTeacher = 1 ";
        if (drpClass.SelectedIndex > 0)
        {
            sql = sql + " and  ctm.Classid = '" + drpClass.SelectedItem.Value + "'";
        }
        if (drpSection.SelectedIndex != 0)
        {
            sql = sql + " and ctm.SectionId=" + drpSection.SelectedValue + " ";
        }
        if (drpBranch.SelectedIndex != 0)
        {
            sql = sql + " and ctm.BranchId=" + drpBranch.SelectedValue + " ";
        }
        sql = sql + "  order by cm.CIDOrder,sm.SectionName";
        var dt = _oo.Fetchdata(sql);
        Grd1.DataSource = dt;
        if (dt.Rows.Count>0)
        {
            heading.Text = "List of Class Teachers";
            pnlcontrols.Visible = true;
            abc.Visible = true;
            Grd1.DataSource = dt;
            Grd1.DataBind();
        }
       
       
    }
    protected void ImageButton1_Click(object sender, EventArgs e)
    {
        //BAL.objBal.ExportToWord(Response, "ClassTeacherList", abc);
        _oo.ExportTolandscapeWord(Response, "ClassTeacherList", gdv1);
    }
    protected void ImageButton2_Click(object sender, EventArgs e)
    {
        //BAL.objBal.ExportDivToExcelWithFormatting(Response,"ClassTeacherList", abc);
        _oo.ExportDivToExcelWithFormatting(Response, "ClassTeacherList.xls", gdv1, Server.MapPath("~/Admin/css/style.css"));
    }
    protected void ImageButton3_Click(object sender, EventArgs e)
    {
        BAL.objBal.ExporttolandscapePdf(Response, "ClassTeacherList", abc);
        //_oo.ExporttolandscapePdf(Response, "ClassTeacherList", abc);
    }
    protected void ImageButton4_Click(object sender, EventArgs e)
    {
        PrintHelper_New.ctrl = abc;
        ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>var winpop=window.open('Print_New.aspx','_blank'); if(!winpop || winpop.closed){alert('Please allow pop-up blocker from browser settings!')}</script>");
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }

}