using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

public partial class Comman_G7_RemarkForXI : System.Web.UI.Page
{
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
    public void loadRemarkText()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("ReamrkText");
        dt.Columns.Add("ReamrkValue");

        DataRow dr1 = dt.NewRow();
        dr1["ReamrkText"] = "General Remark";
        dr1["ReamrkValue"] = "generalremark";
        dt.Rows.Add(dr1);

        drpSelfAwareness.DataSource = dt;
        drpSelfAwareness.DataTextField = "ReamrkText";
        drpSelfAwareness.DataValueField = "ReamrkValue";
        drpSelfAwareness.DataBind();
    }
    public void loadEval()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("Eval");
        DataRow dr1 = dt.NewRow();
        dr1["Eval"] = "TERM1";
        dt.Rows.Add(dr1);
        DataRow dr2 = dt.NewRow();
        dr2["Eval"] = "TERM2";
        dt.Rows.Add(dr2);

        drpEval.DataSource = dt;
        drpEval.DataTextField = "Eval";
        drpEval.DataValueField = "Eval";
        drpEval.DataBind();
    }

    public void loadclass()
    {
        if (Session["Logintype"].ToString() == "Admin")
        {
            sql = "Select Distinct ClassName,cm.Id as Id,CIDOrder from ClassMaster cm ";
            sql +=  " inner join dt_ClassGroupMaster t1 on t1.ClassId=cm.Id and t1.SessionName=cm.SessionName";
            sql +=  " where cm.SessionName='" + Session["SessionName"] + "' and t1.BranchCode=" + Session["BranchCode"] + " and cm.BranchCode=" + Session["BranchCode"] + " and GroupId='G6' Order by CIDOrder";
            BAL.objBal.FillDropDown_withValue(sql, drpclass, "ClassName", "Id");
        }
        else
        {
            sql = "Select  Distinct ClassName,cm.Id,CIDOrder from ClassTeacherMaster T1";
            sql +=  " inner join ClassMaster cm on cm.Id=T1.ClassId and cm.SessionName=t1.SessionName";
            sql +=  " inner join dt_ClassGroupMaster T2 on T2.ClassId=T1.ClassId and cm.SessionName=T2.SessionName";
            sql +=  " where GroupId='G6' and EmpCode='" + Session["LoginName"].ToString() + "' and cm.BranchCode=" + Session["BranchCode"] + " and t2.BranchCode=" + Session["BranchCode"] + " and t1.BranchCode=" + Session["BranchCode"] + " and T1.SessionName='" + Session["SessionName"] + "' Order by CIDOrder";
            BAL.objBal.FillDropDown_withValue(sql, drpclass, "ClassName", "Id");
            drpclass.Items.Insert(0, new ListItem("<--Select-->", "-1"));
        }

    }

    public void loadsection()
    {
        if (Session["Logintype"].ToString() == "Admin")
        {
            sql = "Select SectionName,Id from SectionMaster where ClassNameId='" + drpclass.SelectedValue.ToString() + "' and BranchCode=" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"].ToString() + "'";
            BAL.objBal.FillDropDown_withValue(sql, drpsection, "SectionName", "Id");
            drpsection.Items.Insert(0, new ListItem("<--Select-->", ""));

        }
        else
        {
            sql = "Select SectionName,sm.Id from ClassTeacherMaster T1";
            sql +=  " inner join SectionMaster sm on sm.Id=T1.SectionId and sm.SessionName=t1.SessionName";
            sql +=  " where EmpCode='" + Session["LoginName"].ToString() + "' and sm.BranchCode=" + Session["BranchCode"] + " and t1.BranchCode=" + Session["BranchCode"] + " and T1.SessionName='" + Session["SessionName"] + "'";
            sql +=  " and t1.Classid=" + drpclass.SelectedValue.ToString() + "";
            BAL.objBal.FillDropDown_withValue(sql, drpsection, "SectionName", "Id");
            drpsection.Items.Insert(0, new ListItem("<--Select-->", ""));
        }

    }

    private void loadBranch()
    {
        if (Session["Logintype"].ToString() == "Admin")
        {
            sql = "Select BranchName,Id from BranchMaster Where ClassId='" + drpclass.SelectedValue.ToString() + "' and BranchCode=" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"].ToString() + "'";
            BAL.objBal.FillDropDown_withValue(sql, drpBranch, "BranchName", "Id");
            drpBranch.Items.Insert(0, new ListItem("<--Select-->", ""));
        }
        else
        {
            sql = "Select  Distinct BranchName, bm.Id from ClassTeacherMaster T1 ";
            sql +=  "   inner join BranchMaster bm on bm.Id=T1.BranchId and bm.SessionName=t1.SessionName ";
            sql +=  "   where EmpCode='" + Session["LoginName"].ToString() + "' and bm.BranchCode=" + Session["BranchCode"] + " and T1.SessionName='" + Session["SessionName"] + "' and T1.Classid=" + drpclass.SelectedValue.ToString() + "";
            BAL.objBal.FillDropDown_withValue(sql, drpBranch, "BranchName", "Id");
            drpBranch.Items.Insert(0, new ListItem("<--Select-->", ""));
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
            loadEval();
            drpclass.Focus();
            loadclass();
            loadBranch();
            loadsection();
            loadRemarkText();
        }
    }

    
    protected void drpclass_SelectedIndexChanged(object sender, EventArgs e)
    {

        loadsection();
        drpclass.Focus();
    }
    protected void drpBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        getSelfAwareness();
        drpBranch.Focus();
    }
    
    protected void drpsection_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadBranch();
        drpsection.Focus();
    }
    protected void drpEval_SelectedIndexChanged(object sender, EventArgs e)
    {
        getSelfAwareness();
    }
    
    protected void lblSubmit_Click(object sender, EventArgs e)
    {
        setSelfAwareness();
    }

    public void setSelfAwareness()
    {
        string msg = "";
        DataTable dt = new DataTable();

        // ReSharper disable once RedundantExplicitArraySize
        dt.Columns.AddRange(new DataColumn[7] { new DataColumn("SrNo", typeof(string)),
                new DataColumn("RemarkFor", typeof(string)),
                new DataColumn("Caption",typeof(string)),new DataColumn("Eval",typeof(string)),
                new DataColumn("SessionName",typeof(string)),new DataColumn("LoginName",typeof(string)),new DataColumn("BranchCode",typeof(int)) });

        foreach (RepeaterItem ri in rpt.Items)
        {
            string srno = (ri.FindControl("lblsrno") as Label).Text;
            string Remarkfor = drpSelfAwareness.SelectedValue.ToString();
            string caption = (ri.FindControl("txtCaption") as TextBox).Text;
            string eval = drpEval.SelectedValue.ToString();

            string sessionname = Session["SessionName"].ToString();
            string loginname = Session["LoginName"].ToString();
            int branchcode = Convert.ToInt16(Session["BranchCode"].ToString());

            dt.Rows.Add(srno, Remarkfor, caption, eval, sessionname, loginname, branchcode);
        }

        List<SqlParameter> param = new List<SqlParameter>();

        param.Add(new SqlParameter("@Exam_ItoVRemark_Type", dt));

        SqlParameter para = new SqlParameter("@msg", "");
        para.Direction = ParameterDirection.Output;
        param.Add(para);

        msg = DLL.objDll.Sp_Insert_Update_Delete_usingExecuteNonQuery("USP_Exam_XItoXIIRemark_BulkInsert", param);

        if (msg == "S")
        {
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Updated successfully.", "S");
        }
        else
        {
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, msg, "W");
        }
    }

    public void getSelfAwareness()
    {
        List<SqlParameter> param = new List<SqlParameter>();

        param.Add(new SqlParameter("@SessionName", Session["SessionName"].ToString()));
        param.Add(new SqlParameter("@BranchCode", Session["BranchCode"].ToString()));
        param.Add(new SqlParameter("@RemarkFor", drpSelfAwareness.SelectedValue.ToString()));

        param.Add(new SqlParameter("@ClassId", drpclass.SelectedValue.ToString()));
        param.Add(new SqlParameter("@SectionId", drpsection.SelectedValue.ToString()));
        param.Add(new SqlParameter("@BranchId", drpBranch.SelectedValue.ToString()));
        
        param.Add(new SqlParameter("@Eval", drpEval.SelectedValue.ToString()));

        rpt.DataSource = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("USP_Get_XItoXIIRemark", param);
        rpt.DataBind();
    }

    protected void drpSelfAwareness_SelectedIndexChanged(object sender, EventArgs e)
    {
        getSelfAwareness();
    }
}