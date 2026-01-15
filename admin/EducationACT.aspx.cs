using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System;
using System.IO;

public partial class EducationACT : Page
{
    SqlConnection con = new SqlConnection();
    Campus oo = new Campus();
    string sql = "";

    protected void Page_PreInIt(object sender, EventArgs e)
    {
        if (Session["Logintype"] == null)
        {
            Response.Redirect("~/default.aspx");
        }
        if (Session["Logintype"].ToString() == "SuperAdmin")
        {
            MasterPageFile = "~/50/sadminRootManager.master";
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        con = oo.dbGet_connection();
        Campus camp = new Campus(); camp.LoadLoader(loader);
        if (!IsPostBack)
        {
            txtActName.Focus();
            string sql = "Select BranchId, BranchName from Branchtab";
            var dt = oo.Fetchdata(sql);
            ddlBranch.DataSource = dt;
            ddlBranch.DataTextField = "BranchName";
            ddlBranch.DataValueField = "BranchId";
            ddlBranch.DataBind();
            ddlBranch.Items.Insert(0, new ListItem("<--Select-->", ""));
            if (Session["LoginType"].ToString() == "Admin")
            {
                divBranch.Visible = false;
                //divSession.Visible = false;
                ddlBranch.SelectedValue = Session["BranchCode"].ToString();

                //string sqls = "select SessionName from SessionMaster where BranchCode=" + ddlBranch.SelectedValue + "";
                //var dt2 = oo.Fetchdata(sqls);
                //DrpSessionName.DataSource = dt2;
                //DrpSessionName.DataTextField = "SessionName";
                //DrpSessionName.DataValueField = "SessionName";
                //DrpSessionName.DataBind();
                //DrpSessionName.Items.Insert(0, new ListItem("<--Select Session-->", ""));
                //DrpSessionName.SelectedIndex = (DrpSessionName.Items.Count - 1);
                //if (Session["LoginType"].ToString() == "Admin")
                //{
                //    DrpSessionName.SelectedValue = Session["SessionName"].ToString();
                //}
            }

            //DrpSessionName.Items.Insert(0, new ListItem("<--Select-->", ""));

            DisplayInformation();
        }
    }
    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if (ddlBranch.SelectedIndex == 0)
        //{
        //    DrpSessionName.Items.Clear();
        //    DrpSessionName.Items.Insert(0, new ListItem("<--Select Session-->", ""));
        //    return;
        //}
        //string sql = "select SessionName from SessionMaster where BranchCode=" + ddlBranch.SelectedValue + "";
        //var dt2 = oo.Fetchdata(sql);
        //DrpSessionName.DataSource = dt2;
        //DrpSessionName.DataTextField = "SessionName";
        //DrpSessionName.DataValueField = "SessionName";
        //DrpSessionName.DataBind();
        //DrpSessionName.Items.Insert(0, new ListItem("<--Select Session-->", ""));
        //DrpSessionName.SelectedIndex = (DrpSessionName.Items.Count - 1);
        //if (Session["LoginType"].ToString() == "Admin")
        //{
        //    DrpSessionName.SelectedValue = Session["SessionName"].ToString();
        //}

    }
    public void DisplayInformation()
    {
        sql = "select *,(Select  BranchName from Branchtab where BranchId=tblEducationAct.BranchCode)BranchName,(Select  BranchId from Branchtab where BranchId=tblEducationAct.BranchCode)BranchId  from tblEducationAct ";
        Grd.DataSource = oo.GridFill(sql);
        Grd.DataBind();
        for (int i = 0; i < Grd.Rows.Count; i++)
        {
            LinkButton LinkButton3 = (LinkButton)Grd.Rows[i].FindControl("LinkButton3");
            Label id = (Label)Grd.Rows[i].FindControl("Label36");
            sql = "select EducationActId from StudentOfficialDetails where   BranchCode=" + ddlBranch.SelectedValue + " and EducationActId="+ id.Text+"";
            if (oo.Duplicate(sql))
            {
                LinkButton3.Text = "<i class='fa fa-lock'></i>";
                LinkButton3.Enabled = false;
            }
        }
    }
    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        LinkButton chk = (LinkButton)sender;
        Label lblId = (Label)chk.NamingContainer.FindControl("Label36");
        Label Label3 = (Label)chk.NamingContainer.FindControl("Label3");
        
        string ss = lblId.Text;
        lblID.Text = ss;
        string sss = Label3.Text;
        lblbranchID.Text = sss;
        sql = "select * from tblEducationAct where   BranchCode=" + sss + "";
        sql = sql + " and id=" + ss;

        TextBox1.Text = oo.ReturnTag(sql, "Actname");
        txtRemarkPanel.Text = oo.ReturnTag(sql, "remark");

        Panel1_ModalPopupExtender.Show();
    }
    protected void LinkButton3_Click(object sender, EventArgs e)
    {
        Button8.Focus();
        LinkButton chk = (LinkButton)sender;
        Label lblId = (Label)chk.NamingContainer.FindControl("Label37");
        var ss = lblId.Text;
        lblvalue.Text = ss;
        Panel2_ModalPopupExtender.Show();
    }
    protected void lnkSubmit_Click(object sender, EventArgs e)
    {
        string qry = string.Empty;
        sql = "select ActName from tblEducationAct where ActName='" + txtActName.Text.Trim() + "' and   BranchCode=" + ddlBranch.SelectedValue + "";
        if (oo.Duplicate(sql))
        {
            oo.msgbox(Page, msgbox, "Act Name already exists!", "A");
        }
        else
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SpEducationAct";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@ActName", txtActName.Text.Trim());
            cmd.Parameters.AddWithValue("@remark", txtRemark.Text.Trim());
           // cmd.Parameters.AddWithValue("@SessionName", DrpSessionName.SelectedValue);
            cmd.Parameters.AddWithValue("@BranchCode", ddlBranch.SelectedValue);
            cmd.Parameters.AddWithValue("@Action", "insert");
            con.Open();
            cmd.ExecuteNonQuery();
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Submitted successfully.", "S");
            con.Close();
            DisplayInformation();
        }
    }
    protected void lnkUpdate_Click(object sender, EventArgs e)
    {
        sql = "select ActName from tblEducationAct where ActName='" + TextBox1.Text.Trim() + "' and id<>"+ lblID.Text.Trim()+ "  and  BranchCode=" + lblbranchID.Text + "";
        if (oo.Duplicate(sql))
        {
            oo.msgbox(Page, msgbox, "Duplicate record!", "A");
            return;
        }
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "SpEducationAct";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Connection = con;
        cmd.Parameters.AddWithValue("@id", lblID.Text.Trim());
        cmd.Parameters.AddWithValue("@ActName", TextBox1.Text.Trim());
        cmd.Parameters.AddWithValue("@remark", txtRemarkPanel.Text.Trim());
     //   cmd.Parameters.AddWithValue("@SessionName", DrpSessionName.SelectedValue);
        cmd.Parameters.AddWithValue("@BranchCode", lblbranchID.Text);
        cmd.Parameters.AddWithValue("@Action", "update");
        con.Open();
        cmd.ExecuteNonQuery();
        Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Updated successfully.", "S");
        con.Close();
        DisplayInformation();
    }

    protected void Button4_Click(object sender, EventArgs e)
    {

    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        sql = "Delete from tblEducationAct where Id=" + lblvalue.Text+ "  and  BranchCode=" + ddlBranch.SelectedValue + "";

        using (SqlCommand cmd = new SqlCommand())
        {
            cmd.CommandText = sql;
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                //oo.MessageBox("Deleted successfully.", this.Page);
                Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Deleted successfully.", "S");
                DisplayInformation();
            }
            // ReSharper disable once RedundantCatchClause
            catch (Exception)
            {
                throw;
            }
        }

    }

    protected void Button8_Click(object sender, EventArgs e)
    {

    }
}
