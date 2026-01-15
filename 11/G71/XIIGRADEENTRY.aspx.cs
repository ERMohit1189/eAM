using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

public partial class Comman_G7_XIIGRADEENTRY : System.Web.UI.Page
{
    string sql = "";
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
        this.Page.MaintainScrollPositionOnPostBack = false;
        if (Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
        {
            Response.Redirect("~/default.aspx");
        }
        if (!IsPostBack)
        {
            loadEval();
            loadclass();
            loadsection();
            loadBranch();
            loadCaption();
        }
       
    }

    public void loadEval()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("Eval");
        if (Session["SessionName"].ToString() == "2015-2016" || Session["SessionName"].ToString() == "2016-2017")
        {
            DataRow dr1 = dt.NewRow();
            dr1["Eval"] = "SA1";
            dt.Rows.Add(dr1);
            DataRow dr2 = dt.NewRow();
            dr2["Eval"] = "SA2";
            dt.Rows.Add(dr2);
        }
        else
        {
            DataRow dr1 = dt.NewRow();
            dr1["Eval"] = "TERM1";
            dt.Rows.Add(dr1);
            DataRow dr2 = dt.NewRow();
            dr2["Eval"] = "TERM2";
            dt.Rows.Add(dr2);
        }

        drpEval.DataSource = dt;
        drpEval.DataTextField = "Eval";
        drpEval.DataValueField = "Eval";
        drpEval.DataBind();
    }

    public void loadGrid()
    {
        sql = " Select asr.SrNo,Name,FatherName from AllStudentRecord_UDF('" + Session["SessionName"].ToString() + "'," + Session["BranchCode"].ToString() + ") asr ";
        sql +=  " where asr.ClassId='" + drpclass.SelectedValue.ToString() + "'  and (asr.BranchId='" + drpBranch.SelectedValue.ToString() + "' or asr.BranchId is null)  ";
        sql +=  " and asr.SectionId='" + drpsection.SelectedValue.ToString() + "' and Withdrwal is null  and isnull(Promotion,'')<>'Cancelled' order by Name Asc";

        GridView1.DataSource = BAL.objBal.GridFill(sql);
        GridView1.DataBind();

        if (GridView1.Rows.Count > 0)
        {
            table3.Visible = true;
        }
    }

    public void loadCaption()
    {
        sql = "Select id,UPPER(txt) txt from CCEXItoXII_InternalGrades Where sessionname='" + Session["SessionName"] + "' and branchcode=" + Session["BranchCode"] + " and txtfor='" + drpGradeFor.SelectedItem.Text + "'";
        var dt = oo.Fetchdata(sql);
        drpcaption.DataSource = dt;
        drpcaption.DataTextField = "txt";
        drpcaption.DataValueField = "id";
        drpcaption.DataBind();
        drpcaption.Items.Insert(0, new ListItem("<--Select-->", "-1"));
    }

    public void loadclass()
    {
        if (Session["Logintype"].ToString() == "Admin")
        {
            sql = "Select Distinct ClassName,cm.Id as Id,CIDOrder from ClassMaster cm ";
            sql +=  " inner join dt_ClassGroupMaster t1 on t1.ClassId=cm.Id and t1.SessionName=cm.SessionName";
            sql +=  " where cm.SessionName='" + Session["SessionName"] + "' and t1.BranchCode=" + Session["BranchCode"] + " and cm.BranchCode=" + Session["BranchCode"] + " and GroupId='G7' Order by CIDOrder";
            BAL.objBal.FillDropDown_withValue(sql, drpclass, "ClassName", "Id");
            drpclass.Items.Insert(0, new ListItem("<--Select-->", "-1"));
        }
        else
        {
            sql = "Select  Distinct ClassName,cm.Id,CIDOrder from ICSEClassTeacherAllotment T1";
            sql +=  " inner join ClassMaster cm on cm.Id=T1.ClassId and cm.SessionName=t1.SessionName";
            sql +=  " inner join dt_ClassGroupMaster T2 on T2.ClassId=T1.ClassId and cm.SessionName=T2.SessionName";
            sql +=  " where GroupId='G7' and ECode='" + Session["LoginName"].ToString() + "' and cm.BranchCode=" + Session["BranchCode"] + " and t2.BranchCode=" + Session["BranchCode"] + " and t1.BranchCode=" + Session["BranchCode"] + " and T1.SessionName='" + Session["SessionName"] + "' Order by CIDOrder";
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
            sql = "Select SectionName,sm.Id from ICSEClassTeacherAllotment T1";
            sql +=  " inner join SectionMaster sm on sm.Id=T1.SectionId and sm.SessionName=t1.SessionName";
            sql +=  " where ECode='" + Session["LoginName"].ToString() + "' and sm.BranchCode=" + Session["BranchCode"] + " and t1.BranchCode=" + Session["BranchCode"] + " and T1.SessionName='" + Session["SessionName"] + "'";
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
            sql = "Select  Distinct BranchName,bm.Id from ICSEClassTeacherAllotment T1";
            sql +=  "   inner join BranchMaster bm on bm.Id=T1.BranchId and bm.SessionName=t1.SessionName";
            sql +=  "   where ECode='" + Session["LoginName"].ToString() + "' and bm.BranchCode=" + Session["BranchCode"] + " and T1.SessionName='" + Session["SessionName"] + "' and T1.Classid='" + drpclass.SelectedValue.ToString() + "'";
            BAL.objBal.FillDropDown_withValue(sql, drpBranch, "BranchName", "Id");
            drpBranch.Items.Insert(0, new ListItem("<--Select-->", ""));
        }
    }


    protected void drpclass_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadBranch();
        
    }

    protected void drpsection_SelectedIndexChanged(object sender, EventArgs e)
    {
        //loadSubjectGroup();
        //loadSubject();
        //loadGrid();
        loadStudentGrade();
    }
    protected void lnkview_Click(object sender, EventArgs e)
    {
        loadGrid();
        loadStudentGrade();
    }

    protected void drpBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadsection();
    }

    protected void drpEval_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadStudentGrade();
    }
    protected void lnkSubmit_Click(object sender, EventArgs e)
    {
        if (GridView1.Rows.Count > 0)
        {
            string msg = "";
            foreach (GridViewRow gvr in GridView1.Rows)
            {
                TextBox txtGrade = (TextBox)gvr.FindControl("txtGrade");
                Label lblSrno = (Label)gvr.FindControl("lblSrno");

                List<SqlParameter> param = new List<SqlParameter>();
                param.Add(new SqlParameter("@QueryFor", "I"));
                param.Add(new SqlParameter("@Srno", lblSrno.Text.Trim()));
                param.Add(new SqlParameter("@ClassId", drpclass.SelectedValue.ToString()));
                param.Add(new SqlParameter("@SectionId", drpsection.SelectedValue.ToString()));
                param.Add(new SqlParameter("@BranchId", drpBranch.SelectedValue.ToString()));
                param.Add(new SqlParameter("@Eval", drpEval.SelectedItem.Text.Trim()));
                param.Add(new SqlParameter("@Captionid", drpcaption.SelectedValue.ToString()));
                param.Add(new SqlParameter("@Grade", txtGrade.Text.Trim()));
                param.Add(new SqlParameter("@SessionName", Session["SessionName"].ToString()));
                param.Add(new SqlParameter("@LoginName", Session["LoginName"].ToString()));
                param.Add(new SqlParameter("@BranchCode", Session["BranchCode"].ToString()));

                SqlParameter para = new SqlParameter("@Msg", "");
                para.Direction = ParameterDirection.Output;
                param.Add(para);

                msg = DLL.objDll.Sp_Insert_Update_Delete_usingExecuteNonQuery("CCEXI_XIIINTERNALGERDEENTRYPROC", param);
            }

            if (msg == "S")
            {
                //BAL.objBal.MessageBoxwithfocuscontrol("Submitted successfully.", lnkSubmit, drpcaption.ClientID);
                Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Submitted successfully.", "S");       

                loadGrid();
                drpcaption.SelectedIndex = 0;
                BAL.objBal.ClearControls(table3);
            }
            else
            {
                //BAL.objBal.MessageBoxforUpdatePanel("Sorry, Record not updated!", lnkSubmit);
                Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Sorry, Record not updated!", "A");       

            }
        }
    }

    public void loadStudentGrade()
    {
        if (GridView1.Rows.Count > 0)
        {
            foreach (GridViewRow gvr in GridView1.Rows)
            {              
                TextBox txtGrade = (TextBox)gvr.FindControl("txtGrade");
                Label lblSrno = (Label)gvr.FindControl("lblSrno");

                List<SqlParameter> param = new List<SqlParameter>();
                param.Add(new SqlParameter("@QueryFor", "S"));
                param.Add(new SqlParameter("@Srno", lblSrno.Text.Trim()));
                param.Add(new SqlParameter("@ClassId", drpclass.SelectedValue.ToString()));
                param.Add(new SqlParameter("@SectionId", drpsection.SelectedValue.ToString()));
                param.Add(new SqlParameter("@BranchId", drpBranch.SelectedValue.ToString()));
                param.Add(new SqlParameter("@Eval", drpEval.SelectedItem.Text.Trim()));
                param.Add(new SqlParameter("@Captionid", drpcaption.SelectedValue.ToString()));
                param.Add(new SqlParameter("@SessionName", Session["SessionName"].ToString()));

                var obj = DLL.objDll.Sp_SelectRecord_usingExecuteScalar("CCEXI_XIIINTERNALGERDEENTRYPROC", param);
                if (obj != null)
                {
                    txtGrade.Text = obj.ToString();
                }
                else
                {
                    txtGrade.Text = "";
                }
            }
        }
    }

    protected void drpGradeFor_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadCaption();
        loadStudentGrade();
    }
    protected void drpcaption_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadStudentGrade();
    }
}