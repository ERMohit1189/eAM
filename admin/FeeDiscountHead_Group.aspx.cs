using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_FeeDiscountHead_Group : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Campus camp = new Campus(); camp.LoadLoader(loader);
        if (!IsPostBack)
        {
            loadGroup();
        }
        txtGroupName.Focus();
    }

    private void loadGroup()
    {
        List<SqlParameter> param = new List<SqlParameter>();
        param.Add(new SqlParameter("@Sql", "S"));

        rpt.DataSource = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("USP_F08_FeeDiscountHead_Group", param);
        rpt.DataBind();
    }

    protected void lnkSubmit_Click(object sender, EventArgs e)
    {
        insert();
    }

    private void insert()
    {
        string msg = "";

        List<SqlParameter> param = new List<SqlParameter>();
        param.Add(new SqlParameter("@Sql", "I"));
        param.Add(new SqlParameter("@GroupName", txtGroupName.Text.Trim()));
        param.Add(new SqlParameter("@SessionName", Session["SessionName"].ToString()));
        param.Add(new SqlParameter("@BranchCode", Session["BranchCode"].ToString()));
        param.Add(new SqlParameter("@LoginName", Session["LoginName"].ToString()));

        SqlParameter para = new SqlParameter("@Msg", "");
        para.Direction = ParameterDirection.Output;
        param.Add(para);

        msg = DLL.objDll.Sp_Insert_Update_Delete_usingExecuteNonQuery("USP_F08_FeeDiscountHead_Group", param);

        string msgcolor = "";
        if (msg == "S")
        {
            msgcolor = "S";
            msg = "Submitted successfully.";
        }
        else if (msg == "D")
        {
            msgcolor = "A";
            msg = "Sorry, Duplicate Record!";
        }
        else
        {
            msgcolor = "W";
        }

        Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, msg, msgcolor);

        loadGroup();
    }

    protected void lnkEdit_Click(object sender, EventArgs e)
    {
        LinkButton lnk=(LinkButton)sender;
        Label lblId = (Label)lnk.NamingContainer.FindControl("lblId");
        lblID.Text = lblId.Text;

        List<SqlParameter> param = new List<SqlParameter>();
        param.Add(new SqlParameter("@Sql", "S"));
        param.Add(new SqlParameter("@F08ID", lblID.Text.Trim()));

        DataSet ds = new DataSet();

        ds = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("USP_F08_FeeDiscountHead_Group", param);

        if (ds != null)
        {
            DataTable dt = new DataTable();
            dt = ds.Tables[0];

            if (dt.Rows.Count>0)
            {
                txtGroupNamePanel.Text = dt.Rows[0]["GroupName"].ToString();
            }
        }

        Panel1_ModalPopupExtender.Show();
    }

    protected void lnkUpdate_Click(object sender, EventArgs e)
    {
        LinkButton lnk = (LinkButton)sender;
        Label lblId = (Label)lnk.NamingContainer.FindControl("lblId");
        lblID.Text = lblId.Text;

        Update();
    }

    private void Update()
    {
        string msg = "";

        List<SqlParameter> param = new List<SqlParameter>();
        param.Add(new SqlParameter("@Sql", "U"));
        param.Add(new SqlParameter("@F08ID", lblID.Text.Trim()));
        param.Add(new SqlParameter("@GroupName", txtGroupNamePanel.Text.Trim()));
        param.Add(new SqlParameter("@SessionName", Session["SessionName"].ToString()));

        SqlParameter para = new SqlParameter("@Msg", "");
        para.Direction = ParameterDirection.Output;
        param.Add(para);

        msg = DLL.objDll.Sp_Insert_Update_Delete_usingExecuteNonQuery("USP_F08_FeeDiscountHead_Group", param);

        string msgcolor = "";
        if (msg == "S")
        {
            msgcolor = "S";
            msg = "Updated successfully.";
        }
        else
        {
            msgcolor = "W";
        }

        Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, msg, msgcolor);

        loadGroup();
        txtGroupName.Text = "";
    }

    protected void lnkDelete_Click(object sender, EventArgs e)
    {
        LinkButton lnk = (LinkButton)sender;
        Label lblId = (Label)lnk.NamingContainer.FindControl("lblId");
        lblvalue.Text = lblId.Text;

        Panel2_ModalPopupExtender.Show();
    }

    private void Delete()
    {
        string msg = "";

        List<SqlParameter> param = new List<SqlParameter>();
        param.Add(new SqlParameter("@Sql", "D"));
        param.Add(new SqlParameter("@F08ID", lblvalue.Text.Trim()));
        param.Add(new SqlParameter("@SessionName", Session["SessionName"].ToString()));

        SqlParameter para = new SqlParameter("@Msg", "");
        para.Direction = ParameterDirection.Output;
        param.Add(para);

        msg = DLL.objDll.Sp_Insert_Update_Delete_usingExecuteNonQuery("USP_F08_FeeDiscountHead_Group", param);

        string msgcolor = "";
        if (msg == "S")
        {
            msgcolor = "S";
            msg = "Deleted successfully.";
        }
        else
        {
            msgcolor = "W";
        }

        Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, msg, msgcolor);

        loadGroup();
    }

    protected void lnkYes_Click(object sender, EventArgs e)
    {
        Delete();
    }
}