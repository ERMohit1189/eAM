using System;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class OptionalSubjectAllotment : System.Web.UI.Page
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
        sql = "Select Id,ClassName,CidOrder from ClassMaster Where SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + " Order by CIDOrder";
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
        sql = sql + " Where BranchCode=" + Session["BranchCode"].ToString() + "";
        oo.FillDropDown(sql, drpmedium, "Medium");
        drpmedium.Items.Insert(0, "<--Select-->");
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
    protected void LinkView_Click(object sender, EventArgs e)
    {
        displayEmpInfo();
    }
    protected void drpStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        displayEmpInfo();
    }
    public void displayEmpInfo()
    {
        Grd.DataSource = null;
        Grd.DataBind();
        string qq = "select id, SubjectName from TTSubjectMaster where SubjectType<>'Compulsory' and Medium='"+drpmedium.SelectedValue+"' ";
        qq = qq + " and ClassId=" + drpclass.SelectedValue + " and  BranchId=" + drpBranch.SelectedValue + " and BranchCode=" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"] + "'";
        var dts = oo.Fetchdata(qq);
        if (dts.Rows.Count==0)
        {
            Campus camp = new Campus(); camp.msgbox(this.Page, divMsg, "Option subject(s) not found in selected class!", "A");
            btnSubmit.Visible = false;
            return;
        }
        btnSubmit.Visible = true;
        string ss = "select asr.SrNo, asr.Name, FatherName, CombineClassName from VW_AllStudentRecord asr ";
        ss = ss + " where asr.ClassId=" + drpclass.SelectedValue + " and  asr.BranchId=" + drpBranch.SelectedValue + " and  asr.SectionId=case when '" + drpSection.SelectedValue + "'='<--Select-->' then asr.SectionId else '" + drpSection.SelectedValue + "' end and asr.BranchCode=" + Session["BranchCode"] + " ";
        ss = ss + " and asr.medium='" + drpmedium.SelectedValue + "' and asr.SessionName='" + Session["SessionName"] + "'  ";
        if (drpStatus.SelectedValue != "0")
        {
            ss = ss + "  and isnull(Withdrwal,'') = case when isnull('" + drpStatus.SelectedValue + "','')='B' or isnull('" + drpStatus.SelectedValue + "','')='' then isnull(Withdrwal,'') else case when isnull('" + drpStatus.SelectedValue + "','')='A' then '' else 'W' end end and isnull(blocked,'') = case when isnull('" + drpStatus.SelectedValue + "','')= 'W' or isnull('" + drpStatus.SelectedValue + "','')= '' then isnull(blocked,'') else case when isnull('" + drpStatus.SelectedValue + "','')= 'A' then '' else 'yes' end end ";
        }
        ss = ss + " order by name asc";
        var dt = oo.Fetchdata(ss);
        Grd.DataSource = dt;
        Grd.DataBind();
        for (int i = 0; i < Grd.Rows.Count; i++)
        {
            Label lblSrNo = (Label)Grd.Rows[i].FindControl("lblSrNo");
            CheckBoxList chkSubjects = (CheckBoxList)Grd.Rows[i].FindControl("chkSubjects");
            chkSubjects.DataSource = dts;
            chkSubjects.DataTextField = "SubjectName";
            chkSubjects.DataValueField = "id";
            chkSubjects.DataBind();
            string qq2 = "select OptSubjectId from ICSEOptionalSubjectAllotment where Srno='" + lblSrNo.Text + "' ";
            qq2 = qq2 + " and BranchCode=" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"] + "'";
            var dts2 = oo.Fetchdata(qq2);
            if (dts2.Rows.Count>0)
            {
                for (int j = 0; j < dts2.Rows.Count; j++)
                {
                    for (int k = 0; k < chkSubjects.Items.Count; k++)
                    {
                        if (chkSubjects.Items[k].Value == dts2.Rows[j]["OptSubjectId"].ToString())
                        {
                            chkSubjects.Items[k].Selected = true;
                        }
                    }
                }
            }
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        int sts = 0;
        for (int i = 0; i < Grd.Rows.Count; i++)
        {
            Label lblSrNo = (Label)Grd.Rows[i].FindControl("lblSrNo");
            string qq2 = "delete from ICSEOptionalSubjectAllotment where Srno='" + lblSrNo.Text + "' ";
            qq2 = qq2 + " and BranchCode=" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"] + "'";
            cmd = new SqlCommand();
            cmd.CommandText = qq2;
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            cmd.Parameters.Clear();
            CheckBoxList chkSubjects = (CheckBoxList)Grd.Rows[i].FindControl("chkSubjects");
            for (int j = 0; j < chkSubjects.Items.Count; j++)
            {
                if (chkSubjects.Items[j].Selected)
                {
                    cmd = new SqlCommand();
                    cmd.CommandText = "ICSEOptionalSubjectAllotmentProc";
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Srno", lblSrNo.Text);
                    cmd.Parameters.AddWithValue("@OptSubjectId", chkSubjects.Items[j].Value);
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
            }
        }
        if (sts>0)
        {
            Grd.DataSource = null;
            Grd.DataBind();
            btnSubmit.Visible = false;
            Campus camp = new Campus(); camp.msgbox(this.Page, divMsg, "Submitted successfully", "S");
        }
    }
}
