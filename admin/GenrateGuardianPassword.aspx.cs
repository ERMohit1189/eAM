using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_GenrateGuardianPassword : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Campus camp = new Campus(); camp.LoadLoader(loader);
        if (!IsPostBack)
        {
            BLL.BLLInstance.loadClass(drpClass,Session["SessionName"].ToString());
            ddlSection.Items.Insert(0, new ListItem("<--Select-->", ""));
            ddlBranch.Items.Insert(0, new ListItem("<--Select-->", ""));
            //genratePasswordDetails();
        }
    }

    //load details
    public void genratePasswordDetails()
    {
        if (drpClass.SelectedIndex != 0)
        {
            List<SqlParameter> param = new List<SqlParameter>();

            param.Add(new SqlParameter("@QueryFor", "S"));
            param.Add(new SqlParameter("@Classid", drpClass.SelectedValue));
            if (ddlSection.SelectedIndex != 0)
            {
                param.Add(new SqlParameter("@SectionID", ddlSection.SelectedValue));

            }
            if (ddlBranch.SelectedIndex != 0)
            {
                param.Add(new SqlParameter("@BranchId", ddlBranch.SelectedValue));

            }
            param.Add(new SqlParameter("@SessionName", Session["SessionName"].ToString()));
            param.Add(new SqlParameter("@BranchCode", Session["BranchCode"].ToString()));

            grd.DataSource = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("GerrateGuardianLoginandPassword_Proc", param);
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
        else
        {
            grd.DataSource = null;
            grd.DataBind();
        }
    }
    protected void lnkSubmit_Click(object sender, EventArgs e)
    {
        string Msg = "";

        for (int i = 0; i < grd.Rows.Count; i++)
        {
            List<SqlParameter> param = new List<SqlParameter>();

            Label lblSrno = (Label)grd.Rows[i].FindControl("lblSrno");
            Label lblUsername = (Label)grd.Rows[i].FindControl("lblUsername");
            Label lblPassword = (Label)grd.Rows[i].FindControl("lblPassword");

            param.Add(new SqlParameter("@QueryFor", "I"));
            param.Add(new SqlParameter("@Srno", lblSrno.Text.Trim()));
            param.Add(new SqlParameter("@UserName", lblUsername.Text.Trim()));
            param.Add(new SqlParameter("@Password", lblPassword.Text.Trim()));
            param.Add(new SqlParameter("@SessionName", Session["SessionName"].ToString()));
            param.Add(new SqlParameter("@LoginName", Session["LoginName"].ToString()));
            param.Add(new SqlParameter("@BranchCode", Session["BranchCode"].ToString()));

            SqlParameter para = new SqlParameter("@Msg", "");
            para.Direction = ParameterDirection.Output;
            para.Size = 0x100;

            param.Add(para);

            Msg = DLL.objDll.Sp_Insert_Update_Delete_usingExecuteNonQuery("GerrateGuardianLoginandPassword_Proc", param);
        }

        if (Msg == "S")
        {
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Submitted successfully.", "S");
        }
        else
        {
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Sorry, not submitted!", "W");
        }

        genratePasswordDetails();
    }
    protected void drpClass_SelectedIndexChanged(object sender, EventArgs e)
    {
        BLL.BLLInstance.loadSection(ddlSection, Session["SessionName"].ToString(), drpClass.SelectedValue);
        BLL.BLLInstance.loadBranch(ddlBranch, Session["SessionName"].ToString(), drpClass.SelectedValue);
        genratePasswordDetails();
    }

    protected void ddlSection_SelectedIndexChanged(object sender, EventArgs e)
    {
        genratePasswordDetails();
    }

    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        genratePasswordDetails();
    }
}