using System;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class ClassTeacherEvaluation : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection();
    SqlCommand cmd = new SqlCommand();
    Campus oo = new Campus();
    string sql = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((string)Session["LoginName"] == "" || (string)Session["BranchCode"] == "" || (string)Session["SessionName"] == "" || Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
        {
            Response.Redirect("default.aspx");
        }
        con = oo.dbGet_connection();
        Campus camp = new Campus(); camp.LoadLoader(loader);
        if (!IsPostBack)
        {
            loadclass();

            oo.fillSelectvalue(drpBranch, "<--Select-->");
            oo.fillSelectvalue(drpSection, "<--Select-->");
            oo.fillSelectvalue(drpmedium, "<--Select-->");
            ddlEvaluationHead.Items.Insert(0, "<--Select-->");
            loadmedium();

        }
    }
    private void loadBranch()
    {
        sql = "Select BranchName,Id from BranchMaster Where ClassId='" + drpclass.SelectedValue + "' and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
        oo.FillDropDown_withValue(sql, drpBranch, "BranchName", "Id");
        oo.fillSelectvalue(drpBranch, "<--Select-->");
    }
    public void loadclass()
    {
        sql = "Select Id,ClassName,CidOrder from ClassMaster Where SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
        sql = sql + " and id in(select classid from ICSEClassGroupMaster where GroupName='G2' and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + ")  Order by CIDOrder";
        oo.FillDropDown_withValue(sql, drpclass, "ClassName", "Id");
        oo.fillSelectvalue(drpclass, "<--Select-->");
        
    }

    public void loadsection()
    {
        sql = "Select id, SectionName from SectionMaster";
        sql = sql + " where ClassNameId=" + drpclass.SelectedValue + " and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " ";
        oo.FillDropDown_withValue(sql, drpSection, "SectionName", "id");
        drpSection.Items.Insert(0, "<--Select-->");
    }

    public void loadmedium()
    {
        sql = "Select Medium from MediumMaster";
        sql = sql + " Where SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
        oo.FillDropDown(sql, drpmedium, "Medium");
        drpmedium.Items.Insert(0, "<--Select-->");
    }
    public void EvaluationHead()
    {
        sql = "Select EvaluationHead, Id from ICSEClassTeacherEvaluationHead";
        sql = sql + " Where SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
        oo.FillDropDown_withValue(sql, ddlEvaluationHead, "EvaluationHead", "id");
        ddlEvaluationHead.Items.Insert(0, "<--Select-->");
    }
    protected void drpclass_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadBranch();
        loadsection();
    }
    protected void drpBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadmedium();
        loadsection();
    }
    protected void drpmedium_SelectedIndexChanged(object sender, EventArgs e)
    {
        EvaluationHead();
    }
    protected void ddlEvaluationHead_SelectedIndexChanged(object sender, EventArgs e)
    {
        Grd.DataSource = null;
        Grd.DataBind();
        btnSubmit.Visible = false;
    }
    protected void LinkView_Click(object sender, EventArgs e)
    {
        displayEmpInfo();
    }

    public void displayEmpInfo()
    {
        string ss = "select asr.SrNo, asr.Name, FatherName, CombineClassName from VW_AllStudentRecord asr ";
        ss = ss + " where asr.ClassId=" + drpclass.SelectedValue + " and  asr.BranchId=" + drpBranch.SelectedValue + " and  asr.SectionId=case when '" + drpSection.SelectedValue + "'='<--Select-->' then asr.SectionId else '" + drpSection.SelectedValue + "' end and asr.BranchCode=" + Session["BranchCode"] + " ";
        ss = ss + " and asr.medium='" + drpmedium.SelectedValue + "' and asr.SessionName='" + Session["SessionName"] + "' and isnull(Withdrwal, '')='' order by name asc";
        var dt = oo.Fetchdata(ss);
        Grd.DataSource = dt;
        Grd.DataBind();

        if (Grd.Rows.Count > 0)
        {
            btnSubmit.Visible = true;
            for (int i = 0; i < Grd.Rows.Count; i++)
            {
                Label lblSrNo = (Label)Grd.Rows[i].FindControl("lblSrNo");
                TextBox txtEvaluation = (TextBox)Grd.Rows[i].FindControl("txtEvaluation");
                string qq2 = "select Evaluation from ICSEClassTeacherEvaluation where Srno='" + lblSrNo.Text + "' and Term='" + ddlTerm.Text + "' ";
                qq2 = qq2 + " and BranchCode=" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"] + "' and evaluationheadid=" + ddlEvaluationHead.SelectedValue + "";
                txtEvaluation.Text = oo.ReturnTag(qq2, "Evaluation");
            }
        }
        else
        {
            Grd.DataSource = null;
            Grd.DataBind();
            btnSubmit.Visible = false;
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        int sts = 0;
        for (int i = 0; i < Grd.Rows.Count; i++)
        {
            Label lblSrNo = (Label)Grd.Rows[i].FindControl("lblSrNo");
            TextBox txtEvaluation = (TextBox)Grd.Rows[i].FindControl("txtEvaluation");
            if (txtEvaluation.Text.Trim() != "")
            {
                try
                {
                    cmd = new SqlCommand();
                    cmd.CommandText = "ICSEClassTeacherEvaluationProc";
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@EvaluationHeadid", ddlEvaluationHead.SelectedValue);
                    cmd.Parameters.AddWithValue("@SrNo", lblSrNo.Text);
                    cmd.Parameters.AddWithValue("@Term", ddlTerm.SelectedValue);
                    cmd.Parameters.AddWithValue("@Evaluation", txtEvaluation.Text.Trim());
                    cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"].ToString());
                    cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
                    cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());
                    cmd.Parameters.AddWithValue("@Action", "insert");
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    cmd.Parameters.Clear();
                    
                    sts = sts + 1;
                }
                catch (Exception ex)
                {
                }
            }
        }
        if (sts > 0)
        {
            Grd.DataSource = null;
            Grd.DataBind();
            btnSubmit.Visible = false;
            ddlEvaluationHead.SelectedIndex = 0;
            Campus camp = new Campus(); camp.msgbox(this.Page, divMsg, "Submitted successfully", "S");
        }
    }


}
