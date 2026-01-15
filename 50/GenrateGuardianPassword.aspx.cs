using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_GenrateGuardianPassword : Page
{
    Campus oo = new Campus();
    protected void Page_Load(object sender, EventArgs e)
    {
        Campus camp = new Campus(); camp.LoadLoader(loader);
        if (!IsPostBack)
        {
            string sql = "Select BranchId, BranchName from Branchtab";
            var dt = oo.Fetchdata(sql);
            ddlBranch.DataSource = dt;
            ddlBranch.DataTextField = "BranchName";
            ddlBranch.DataValueField = "BranchId";
            ddlBranch.DataBind();
            Session();
            
            //genratePasswordDetails();
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
        DrpSessionName.SelectedIndex = (DrpSessionName.Items.Count - 1);
        BLL.BLLInstance.loadClass2(drpClass, DrpSessionName.SelectedValue, ddlBranch.SelectedValue);
    }
    public void genratePasswordDetails()
    {
        List<SqlParameter> param = new List<SqlParameter>();

        param.Add(new SqlParameter("@QueryFor", "S"));
        param.Add(new SqlParameter("@Classid", drpClass.SelectedValue));
        param.Add(new SqlParameter("@SessionName", DrpSessionName.SelectedValue.ToString()));
        param.Add(new SqlParameter("@BranchCode", ddlBranch.SelectedValue));

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
    protected void lnkSubmit_Click(object sender, EventArgs e)
    {
        if (ddlBranch.SelectedValue == "")
        {
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Select Branch.", "A");
            genratePasswordDetails();
        }
        else
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
                param.Add(new SqlParameter("@SessionName", DrpSessionName.SelectedValue.ToString()));
                param.Add(new SqlParameter("@LoginName", "SupperAdmin"));
                param.Add(new SqlParameter("@BranchCode", ddlBranch.SelectedValue));

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
    }
    protected void drpClass_SelectedIndexChanged(object sender, EventArgs e)
    {
        genratePasswordDetails();
    }

    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session();
        drpClass.Items.Clear();
        BLL.BLLInstance.loadClass2(drpClass, DrpSessionName.SelectedValue.ToString(), ddlBranch.SelectedValue);
    }

    protected void DrpSessionName_SelectedIndexChanged(object sender, EventArgs e)
    {
        drpClass.Items.Clear();
        BLL.BLLInstance.loadClass2(drpClass, DrpSessionName.SelectedValue.ToString(), ddlBranch.SelectedValue);
    }
}