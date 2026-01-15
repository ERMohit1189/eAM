using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

public partial class ClassGroupMaster : System.Web.UI.Page
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
    }

    protected void loadClass()
    {
        sql = "select Id, ClassName from classmaster where SessionName='"+Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
        var dt = oo.Fetchdata(sql);
        ChkClass.DataSource = dt;
        ChkClass.DataTextField = "ClassName";
        ChkClass.DataValueField = "Id";
        ChkClass.DataBind();
    }
    protected void rdoGroup_SelectedIndexChanged(object sender, EventArgs e)
    {
        clasbox.Visible = true;
        loadClass();
        LoadCheckedClasses();
        LinkButton1.Visible = true;
    }
    protected void LoadCheckedClasses()
    {
        for (int i = 0; i < ChkClass.Items.Count; i++)
        {
            
            string ss = "select classid from ICSEClassGroupMaster where GroupName='" + rdoGroup.SelectedValue+"' and classid="+ ChkClass.Items[i].Value + " and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
            if (oo.Duplicate(ss))
            {
                ChkClass.Items[i].Selected = true;
            }
        }
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        int sts = 0;
        try
        {
            for (int i = 0; i < ChkClass.Items.Count; i++)
            {
                if (ChkClass.Items[i].Selected)
                {
                    cmd = new SqlCommand();
                    cmd.CommandText = "ICSEClassGroupMasterProc";
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@GroupName", rdoGroup.SelectedValue);
                    cmd.Parameters.AddWithValue("@ClassId", ChkClass.Items[i].Value);
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
        catch (Exception ex)
        {
        }
        if (sts > 0)
        {
            LinkButton1.Visible = false;
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Submitted successfully", "S");
        }
    }
}