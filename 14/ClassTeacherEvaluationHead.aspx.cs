using System;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class ClassTeacherEvaluationHead : System.Web.UI.Page
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
        sql = "Select Id,ClassName,CidOrder from ClassMaster Where SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
        sql = sql + " and id in(select classid from ICSEClassGroupMaster where GroupName='G2' and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + ")  Order by CIDOrder";
        oo.FillDropDown_withValue(sql, drpclass, "ClassName", "Id");
        oo.fillSelectvalue(drpclass, "<--Select-->");
    }
    
    public void loadmedium()
    {
        sql = "Select Medium from MediumMaster";
        sql = sql + " Where SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
        oo.FillDropDown(sql, drpmedium, "Medium");
        drpmedium.Items.Insert(0, "<--Select-->");
    }

    protected void drpclass_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadBranch();
    }
    protected void drpBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadmedium();
    }
    protected void drpmedium_SelectedIndexChanged(object sender, EventArgs e)
    {
        displayEmpInfo();
    }
    public void displayEmpInfo()
    {
        Grd.DataSource = null;
        Grd.DataBind();
        btnSubmit.Visible = true;
        string ss = "select evh.id, evh.classid, evh.Branchid, cm.ClassName,  bm.BranchName, evh.EvaluationHead, evh.Medium from ICSEClassTeacherEvaluationHead evh ";
        ss = ss + " inner join ClassMaster cm on cm.id=evh.Classid and cm.SessionName=evh.SessionName  and cm.BranchCode=evh.BranchCode ";
        ss = ss + " inner join BranchMaster bm on bm.id=evh.Branchid and evh.Classid=bm.Classid  and bm.SessionName=evh.SessionName  and bm.BranchCode=evh.BranchCode ";
        ss = ss + " where evh.ClassId=" + drpclass.SelectedValue + " and  evh.BranchId=" + drpBranch.SelectedValue + " and evh.BranchCode=" + Session["BranchCode"] + " ";
        ss = ss + " and evh.medium='" + drpmedium.SelectedValue+ "' and evh.SessionName='" + Session["SessionName"] + "' order by evh.id asc";
        var dt = oo.Fetchdata(ss);
        Grd.DataSource = dt;
        Grd.DataBind();
        for (int i = 0; i < Grd.Rows.Count; i++)
        {
            Label EvaluationHeadid = (Label)Grd.Rows[i].FindControl("Label36");
            LinkButton lnkEdit = (LinkButton)Grd.Rows[i].FindControl("LinkButton2");
            LinkButton lnkDelete = (LinkButton)Grd.Rows[i].FindControl("LinkButton3");

            sql = "select Medium from ICSEClassTeacherEvaluation where EvaluationHeadid=" + EvaluationHeadid.Text + " and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
            if (oo.Duplicate(sql))
            {
                lnkEdit.Text = "<i class='fa fa-lock'></i>";
                lnkDelete.Text = "<i class='fa fa-lock'></i>";
                lnkEdit.Enabled = false;
                lnkDelete.Enabled = false;
            }
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string ss = "select evh.Medium from ICSEClassTeacherEvaluationHead evh ";
        ss = ss + " where evh.ClassId=" + drpclass.SelectedValue + " and  evh.BranchId=" + drpBranch.SelectedValue + " and evh.BranchCode=" + Session["BranchCode"] + " ";
        ss = ss + " and evh.medium='" + drpmedium.SelectedValue + "' and evh.SessionName='" + Session["SessionName"] + "' and evh.EvaluationHead='" + txtEvaluationHead.Text.Trim() + "'";
        if (oo.Duplicate(ss))
        {
            Campus camp = new Campus(); camp.msgbox(this.Page, divMsg, "Duplicate entry!", "A");
            return;
        }
        try
        {
            cmd = new SqlCommand();
            cmd.CommandText = "ICSEClassTeacherEvaluationHeadProc";
            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Classid", drpclass.SelectedValue);
            cmd.Parameters.AddWithValue("@Branchid", drpBranch.SelectedValue);
            cmd.Parameters.AddWithValue("@Medium", drpmedium.SelectedValue);
            cmd.Parameters.AddWithValue("@EvaluationHead", txtEvaluationHead.Text.Trim());
            cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"].ToString());
            cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
            cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());
            cmd.Parameters.AddWithValue("@Action", "insert");
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            displayEmpInfo();
            txtEvaluationHead.Text = "";
            Campus camp = new Campus(); camp.msgbox(this.Page, divMsg, "Submitted successfully", "S");
        }
        catch (Exception ex)
        {
        }
    }


    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        LinkButton chk = (LinkButton)sender;
        Label lblId = (Label)chk.NamingContainer.FindControl("Label36");
        Label lblClassId = (Label)chk.NamingContainer.FindControl("lblClassId");
        Label lblBranchId = (Label)chk.NamingContainer.FindControl("lblBranchId");
        Label lblMedium = (Label)chk.NamingContainer.FindControl("lblMedium");
        Label lblEvaluationHead = (Label)chk.NamingContainer.FindControl("lblEvaluationHead");
        lblID.Text = lblId.Text;
        lblClassIds.Text = lblClassId.Text;
        lblBranchIds.Text = lblBranchId.Text;
        lblMediums.Text = lblMedium.Text;
        txtEvaluationHead0.Text = lblEvaluationHead.Text;
        Panel1_ModalPopupExtender.Show();
    }
    protected void LinkButton3_Click(object sender, EventArgs e)
    {
        Button8.Focus();
        LinkButton chk = (LinkButton)sender;
        Label lblId = (Label)chk.NamingContainer.FindControl("Label36");
        Label lblClassId = (Label)chk.NamingContainer.FindControl("lblClassId");
        Label lblBranchId = (Label)chk.NamingContainer.FindControl("lblBranchId");
        Label lblMedium = (Label)chk.NamingContainer.FindControl("lblMedium");
        lblvalue.Text = lblId.Text;
        lblClassIdss.Text = lblClassId.Text;
        lblBranchIdss.Text = lblBranchId.Text;
        lblMediumss.Text = lblMedium.Text;
        Panel2_ModalPopupExtender.Show();
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {


            cmd = new SqlCommand();
            cmd.CommandText = "ICSEClassTeacherEvaluationHeadProc";
            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id", lblvalue.Text);
            cmd.Parameters.AddWithValue("@Classid", lblClassIdss.Text);
            cmd.Parameters.AddWithValue("@Branchid", lblBranchIdss.Text);
            cmd.Parameters.AddWithValue("@Medium", lblMediumss.Text);
            cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"].ToString());
            cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
            cmd.Parameters.AddWithValue("@Action", "delete");
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            displayEmpInfo();
            Campus camp = new Campus(); camp.msgbox(Page, divMsg, "Deleted successfully.", "S");
        }
        catch (Exception ex)
        {
        }
    }
    protected void Button8_Click(object sender, EventArgs e)
    {

    }
    protected void LinkButton4_Click(object sender, EventArgs e)
    {
        string ss = "select evh.Medium from ICSEClassTeacherEvaluationHead evh ";
        ss = ss + " where evh.ClassId=" + lblClassIds.Text + " and  evh.BranchId=" + lblBranchIds.Text + " and evh.BranchCode=" + Session["BranchCode"] + " ";
        ss = ss + " and evh.medium='" + lblMediums.Text + "' and evh.SessionName='" + Session["SessionName"] + "' and evh.EvaluationHead='" + txtEvaluationHead0.Text.Trim() + "' and id<>"+ lblID.Text.Trim() + "";
        if (oo.Duplicate(ss))
        {
            Campus camp = new Campus(); camp.msgbox(this.Page, divMsg, "Duplicate entry!", "A");
            return;
        }
        try
        {
            cmd = new SqlCommand();
            cmd.CommandText = "ICSEClassTeacherEvaluationHeadProc";
            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id", lblID.Text);
            cmd.Parameters.AddWithValue("@Classid", lblClassIds.Text);
            cmd.Parameters.AddWithValue("@Branchid", lblBranchIds.Text);
            cmd.Parameters.AddWithValue("@Medium", lblMediums.Text);
            cmd.Parameters.AddWithValue("@EvaluationHead", txtEvaluationHead0.Text.Trim());
            cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"].ToString());
            cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
            cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());
            cmd.Parameters.AddWithValue("@Action", "update");
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            displayEmpInfo();
            Campus camp = new Campus(); camp.msgbox(Page, divMsg, "Updated successfully.", "S");
        }
        catch (Exception ex)
        {
        }
    }


    protected void LinkButton5_Click(object sender, EventArgs e)
    {

    }

}
