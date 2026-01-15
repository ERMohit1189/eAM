using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

public partial class common_VItoVIIISelfAwareness : System.Web.UI.Page
{
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
        Campus camp = new Campus(); camp.LoadLoader(loader);
        if (!IsPostBack)
        {
            drpclass.Focus();
            loadEval();
            loadRemarkText();
            loadclass();
            loadsection();
        }
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

    public void loadclass()
    {
        if (Session["Logintype"].ToString() == "Admin")
        {
            sql = "Select Distinct ClassName,cm.Id as Id,CIDOrder from ClassMaster cm ";
            sql +=  " inner join dt_ClassGroupMaster t1 on t1.ClassId=cm.Id";
            sql +=  " where cm.SessionName='" + Session["SessionName"] + "' and t1.BranchCode=" + Session["BranchCode"] + " and cm.BranchCode=" + Session["BranchCode"] + " and t1.SessionName='" + Session["SessionName"] + "' and GroupId='G4' Order by CIDOrder";
            BAL.objBal.FillDropDown_withValue(sql, drpclass, "ClassName", "Id");
        }
        else
        {
            sql = "Select  Distinct ClassName,cm.Id,CIDOrder from ClassTeacherMaster T1";
            sql +=  " inner join ClassMaster cm on cm.Id=T1.ClassId and cm.SessionName=t1.SessionName";
            sql +=  " inner join dt_ClassGroupMaster T2 on T2.ClassId=T1.ClassId and cm.SessionName=T2.SessionName";
            sql +=  " where GroupId='G4' and EmpCode='" + Session["LoginName"].ToString() + "' and cm.BranchCode=" + Session["BranchCode"] + " and t2.BranchCode=" + Session["BranchCode"] + " and t1.BranchCode=" + Session["BranchCode"] + " and T1.SessionName='" + Session["SessionName"] + "' Order by CIDOrder";
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
            drpsection.Items.Insert(0, new ListItem("<--Select-->", "-1"));

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
    protected void drpclass_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadsection();
    }
    protected void drpsection_SelectedIndexChanged(object sender, EventArgs e)
    {
        //loadSrno();
        getSelfAwareness();
    }

    public void loadSrno()
    {
        List<SqlParameter> param = new List<SqlParameter>();
        sql = @"Select Name+' - '+SrNo NAME,SrNo from AllStudentRecord_UDF(@SessionName,@BranchCode) where 
                @Classid=Classid and @Sectionid=CASE WHEN @Sectionid='' THEN @Sectionid ELSE Sectionid END  and Withdrwal is null and  isnull(Promotion,'')<>'Cancelled'  ORDER BY NAME";

        param.Add(new SqlParameter("@SessionName", Session["SessionName"].ToString()));
        param.Add(new SqlParameter("@BranchCode", Session["BranchCode"].ToString()));
        param.Add(new SqlParameter("@Classid", drpclass.SelectedValue));
        param.Add(new SqlParameter("@Sectionid", drpsection.SelectedValue));

        rpt.DataSource = DLL.objDll.SelectRecord_usingExecuteDataset(sql, param);
        rpt.DataBind();
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
        dt.Columns.AddRange(new DataColumn[6] { new DataColumn("SrNo", typeof(string)),
                new DataColumn("SelfAwarenessFor", typeof(string)),
                new DataColumn("Caption",typeof(string)),new DataColumn("SessionName",typeof(string)),new DataColumn("LoginName",typeof(string)),new DataColumn("BranchCode",typeof(int)) });

        foreach (RepeaterItem ri in rpt.Items)
        {
            string srno = (ri.FindControl("lblsrno") as Label).Text;
            string selfawarenessfor = drpSelfAwareness.SelectedValue.ToString();
            string caption = (ri.FindControl("txtCaption") as TextBox).Text;

            string sessionname = Session["SessionName"].ToString();
            string loginname = Session["LoginName"].ToString();
            int branchcode = Convert.ToInt16(Session["BranchCode"].ToString());

            dt.Rows.Add(srno, selfawarenessfor, caption, sessionname, loginname, branchcode);
        }

        List<SqlParameter> param = new List<SqlParameter>();

        param.Add(new SqlParameter("@VItoVIIISelfAwarenessTable", dt));
        param.Add(new SqlParameter("@Eval", drpEval.SelectedItem.Text.Trim()));

        SqlParameter para=new SqlParameter("@msg","");
        para.Direction=ParameterDirection.Output;
        param.Add(para);

        msg = DLL.objDll.Sp_Insert_Update_Delete_usingExecuteNonQuery("USP_BULKINSERT_VItoVIIISelfAwareness", param);

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
        param.Add(new SqlParameter("@SelfAwarenessFor", drpSelfAwareness.SelectedValue.ToString()));

        param.Add(new SqlParameter("@ClassId", drpclass.SelectedValue.ToString()));
        param.Add(new SqlParameter("@SectionId", drpsection.SelectedValue.ToString()));

        param.Add(new SqlParameter("@Eval", drpEval.SelectedItem.Text.ToString()));

        rpt.DataSource=DLL.objDll.Sp_SelectRecord_usingExecuteDataset("USP_Get_VItoVIIISelfAwareness", param);
        rpt.DataBind();
    }

    protected void drpSelfAwareness_SelectedIndexChanged(object sender, EventArgs e)
    {
        getSelfAwareness();
    }

    protected void drpEval_SelectedIndexChanged(object sender, EventArgs e)
    {
        getSelfAwareness();
    }
}