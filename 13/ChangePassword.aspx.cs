using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System;


public partial class admin_ChangePassword : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection();
    Campus oo = new Campus();
    string sql = "";
    protected void Page_PreInIt(object sender, EventArgs e)
    {
        if (Session["Logintype"] == null) { Response.Redirect("~/default.aspx"); }
        if (Session["Logintype"].ToString() == "Admin")
        {
            this.MasterPageFile = "~/Master/admin_root-manager.master";
        }
        else if (Session["Logintype"].ToString() == "Administrator")
        {
            this.MasterPageFile = "~/Administrator/administrato_root-manager.master";
        }
        else if (Session["Logintype"].ToString() == "Staff")
        {
            this.MasterPageFile = "~/staff/staff_root-manager.master";
        }
        else if (Session["Logintype"].ToString() == "Guardian")
        {
            this.MasterPageFile = "~/sp/sp_root-manager-dashboard.master";
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {

        con = oo.dbGet_connection();
        Campus camp = new Campus(); camp.LoadLoader(loader);

        if (!IsPostBack)
        {
            txtOldPanel.Focus();
            LblMatch.Text = "";
        }
    }

    protected void txtOldPanel_TextChanged(object sender, EventArgs e)
    {

        Session["OldPass"] = txtOldPanel.Text.Trim();
        string qry = "select Pass from LoginTab where  Pass=" + "'" + txtOldPanel.Text + "' and LoginName='" + Session["LoginName"] + "' and branchId=" + Session["BranchCode"].ToString();

        if (oo.Duplicate(qry))
        {
            LblMatch.Text = "Correct Password";
            txtNewPanel.ReadOnly = false;
            TextConfNewPassw.ReadOnly = false;
            txtOldPanel.Enabled = false;
            txtOldPanel.Attributes["value"] = Session["OldPass"].ToString();
            txtNewPanel.Focus();
        }
        else
        {
            LblMatch.Text = "Incorrect Password!";
            txtNewPanel.ReadOnly = true;
            TextConfNewPassw.ReadOnly = true;
            txtOldPanel.Enabled = true;
            txtOldPanel.Focus();
        }



    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        if (txtNewPanel.Text == "")
        {
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Can not leave blank!", "A");
            txtOldPanel.Enabled = true;
        }

        else
        {
            string qry = "update LoginTab set Pass='" + txtNewPanel.Text + "'   where  Pass=" + "'" + Session["OldPass"] + "' and LoginName='" + Session["LoginName"] + "'  and branchId=" + Session["BranchCode"].ToString()+"  and  logintypeid=4 ";
            
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = qry;
            cmd.Connection = con;
            try
            {
                int action = 0;
                con.Open();
                action = cmd.ExecuteNonQuery();
                con.Close();
                
                if (Session["Logintype"].ToString() == "Student")
                {
                    sql = "update StudentOfficialDetails set StudentPassword='" + txtNewPanel.Text + "'   where srno='" + Session["LoginName"] + "' and BranchCode=" + Session["BranchCode"].ToString() + " update StudentLoginandPassword set Password='" + txtNewPanel.Text + "' where Srno='" + Session["LoginName"] + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
                    cmd = new SqlCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = sql;
                    cmd.Connection = con;
                    con.Open();
                    action = cmd.ExecuteNonQuery();
                    con.Close();
                }
                if (action > 0)
                {
                    txtOldPanel.Focus();
                    txtOldPanel.Enabled = true;
                    Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Password changed successfully.", "S");
                    LblMatch.Text = "";
                    Session["OldPass"] = "";
                }
                else
                {
                    Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Sorry, Password not changed.", "W");
                }

            }
            catch (SqlException ee) { LblMatch.Text = ee.Message.ToString(); }
        }

    }
}
