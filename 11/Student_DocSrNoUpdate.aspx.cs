using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System;
using c4SmsNew;
using System.Net.Mail;
using System.Collections.Generic;

public partial class Student_DocSrNoUpdate : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection();
    Campus oo = new Campus();
    string sql = "";
    DataTable dt;
    protected void Page_PreInIt(object sender, EventArgs e)
    {
        if (Session["Logintype"] == null) { Response.Redirect("~/default.aspx"); }
        if (Session["Logintype"].ToString() == "Admin")
        {
            this.MasterPageFile = "~/Master/admin_root-manager.master";
        }
        else if (Session["Logintype"].ToString() == "Staff")
        {
            this.MasterPageFile = "~/Staff/staff_root-manager.master";
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        con = oo.dbGet_connection();
        Campus camp = new Campus(); camp.LoadLoader(loader);
        if (!IsPostBack)
        {
            BLL.BLLInstance.loadClass(DrpAtteClass, Session["SessionName"].ToString());
            
        }
    }

    protected void DrpAtteClass_SelectedIndexChanged(object sender, EventArgs e)
    {
        BLL.BLLInstance.loadSection(DrpAttenSection, Session["SessionName"].ToString(), DrpAtteClass.SelectedValue);
    }
    

    protected void btnShow_Click(object sender, EventArgs e)
    {
        if (Session["SessionName"].ToString()!="2019-2020")
        {
            Grd.DataSource = null;
            Grd.DataBind();
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Select session 2019-2020", "A");
            return;
        }
        if (DrpAtteClass.SelectedItem.Text == "<--Select-->" || DrpAttenSection.SelectedItem.Text == "<--Select-->")
        {
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Please Select :<--Select-->:", "A");
        }
        else
        {
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter("@sessionName", Session["SessionName"].ToString()));
            param.Add(new SqlParameter("@branchCode", Session["BranchCode"].ToString()));
            param.Add(new SqlParameter("@ClassId", DrpAtteClass.SelectedValue.ToString()));
            param.Add(new SqlParameter("@SectionName", DrpAttenSection.SelectedItem.ToString()));
            param.Add(new SqlParameter("@action", "student"));
            DataSet ds = new DataSet();
            ds = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("USP_DocSrnoUpdate", param);
            if (ds.Tables.Count > 0)
            {
                LinkSubmit.Visible = true;
                Grd.DataSource = ds;
                Grd.DataBind();
            }
            else
            {
                LinkSubmit.Visible = false;
            }
        }
    }
    
    protected void LinkSubmit_Click(object sender, EventArgs e)
    {
        if (Grd.Rows.Count > 0)
        {
            LinkSubmit.Visible = true;
            for (int i = 0; i < Grd.Rows.Count; i++)
            {
                Label newsrno = (Label)Grd.Rows[i].FindControl("lblsrno");
                TextBox oldsrno = (TextBox)Grd.Rows[i].FindControl("txtOldSrno");
                if (oldsrno.Text.Trim() != "")
                {
                    sql = "update StudentDocs set Srno = '" + newsrno.Text.Trim() + "' where Srno = '" + oldsrno.Text.Trim() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";

                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = sql;
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = con;
                    try
                    {
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                    catch (SqlException) { }
                }
                Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Updated successfully.", "S");
            }
        }
        else
        {
            LinkSubmit.Visible = false;
            return;
        }
    }
}

