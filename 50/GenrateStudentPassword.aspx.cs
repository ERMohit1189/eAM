using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_GenrateStudentPassword : Page
{
    Campus oo = new Campus();
    protected void Page_Load(object sender, EventArgs e)
    {
        oo.LoadLoader(loader);
        if (!IsPostBack)
        {
            string sql = "Select BranchId, BranchName from Branchtab";
            var dt = oo.Fetchdata(sql);
            ddlBranch.DataSource = dt;
            ddlBranch.DataTextField = "BranchName";
            ddlBranch.DataValueField = "BranchId";
            ddlBranch.DataBind();
            Session();
            ddlSection.Items.Insert(0, new ListItem("<--Select-->", ""));
            ddlBranchs.Items.Insert(0, new ListItem("<--Select-->", ""));
        }
    }
    protected void Session()
    {
        List<SqlParameter> param = new List<SqlParameter>();
        param.Add(new SqlParameter("@Queryfor", "S"));
        param.Add(new SqlParameter("@BranchCode", ddlBranch.SelectedValue));
        DataSet ds = new DataSet();
        ds = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("Get_GenralInfo", param);
        DrpSessionName.DataSource = ds.Tables[1];
        DrpSessionName.DataTextField = "SessionName";
        DrpSessionName.DataValueField = "SessionName";
        DrpSessionName.DataBind();
        drpClass.Items.Clear();
        DrpSessionName.SelectedIndex = (DrpSessionName.Items.Count - 1);
        BLL.BLLInstance.loadClass2(drpClass, DrpSessionName.SelectedValue.ToString(), ddlBranch.SelectedValue);
    }
    public void genratePasswordDetails()
    {
        List<SqlParameter> param = new List<SqlParameter>();

        param.Add(new SqlParameter("@QueryFor", "S"));
        param.Add(new SqlParameter("@Classid", drpClass.SelectedValue));
        param.Add(new SqlParameter("@SessionName", DrpSessionName.SelectedValue.ToString()));
        param.Add(new SqlParameter("@BranchCode", ddlBranch.SelectedValue));

        grd.DataSource = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("GerrateStudentLoginandPassword_Proc", param);
        grd.DataBind();
        if (grd.Rows.Count > 0)
        {
            for (int i = 0; i < grd.Rows.Count; i++)
            {
                var password = Campus.GenerateRandomNo(8);
                Label lblPassword = (Label)grd.Rows[i].FindControl("lblPassword");
                lblPassword.Text = password;
            }
        }
        else
        {
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Record(s) not found!", "A");
        }

    }
    protected void lnkSubmit_Click(object sender, EventArgs e)
    {
        string Msg = "";

        for (int i = 0; i < grd.Rows.Count; i++)
        {
            List<SqlParameter> param = new List<SqlParameter>();

            Label lblUsername = (Label)grd.Rows[i].FindControl("lblUsername");
            Label lblPassword = (Label)grd.Rows[i].FindControl("lblPassword");

            param.Add(new SqlParameter("@QueryFor", "I"));
            param.Add(new SqlParameter("@UserName", lblUsername.Text.Trim()));
            param.Add(new SqlParameter("@Password", lblPassword.Text.Trim()));
            param.Add(new SqlParameter("@SessionName", DrpSessionName.SelectedValue.ToString()));
            param.Add(new SqlParameter("@LoginName", "SupperAdmin"));
            param.Add(new SqlParameter("@BranchCode", ddlBranch.SelectedValue));

            SqlParameter para = new SqlParameter("@Msg", "");
            para.Direction = ParameterDirection.Output;
            para.Size = 0x100;

            param.Add(para);

            Msg = DLL.objDll.Sp_Insert_Update_Delete_usingExecuteNonQuery("GerrateStudentLoginandPassword_Proc", param);
        }

        if (Msg == "S")
        {
            oo.msgbox(this.Page, msgbox, "Submitted successfully.", "S");
        }
        else
        {
            oo.msgbox(this.Page, msgbox, "Sorry, not submitted!", "W");
        }

        genratePasswordDetails();
    }

    
    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        drpClass.Items.Clear();
        Session();
    }

    protected void DrpSessionName_SelectedIndexChanged(object sender, EventArgs e)
    {
        drpClass.Items.Clear();
        BLL.BLLInstance.loadClass2(drpClass, DrpSessionName.SelectedValue.ToString(), ddlBranch.SelectedValue);
    }

    protected void drpClass_SelectedIndexChanged(object sender, EventArgs e)
    {
        BLL.BLLInstance.loadSection2(ddlSection, DrpSessionName.SelectedItem.Text, drpClass.SelectedValue, ddlBranch.SelectedValue);
        BLL.BLLInstance.loadBranch2(ddlBranchs, DrpSessionName.SelectedItem.Text, drpClass.SelectedValue,  ddlBranch.SelectedValue);
        genratePasswordDetails();
    }
    protected void ddlSection_SelectedIndexChanged(object sender, EventArgs e)
    {
        genratePasswordDetails();
    }

    protected void ddlBranchs_SelectedIndexChanged(object sender, EventArgs e)
    {
        genratePasswordDetails();
    }
}