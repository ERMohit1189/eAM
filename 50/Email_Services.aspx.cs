using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class SuperAdmin_Sms_Services : Page
{
    SqlConnection con = new SqlConnection();
    Campus oo = new Campus();
    string sql = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        con = oo.dbGet_connection();
        Campus camp = new Campus(); camp.LoadLoader(loader);  //in cs file

        if(!IsPostBack)
        {
            sql = "Select BranchId, BranchName from Branchtab";
            var dt = oo.Fetchdata(sql);
            ddlBranch.DataSource = dt;
            ddlBranch.DataTextField = "BranchName";
            ddlBranch.DataValueField = "BranchId";
            ddlBranch.DataBind();
            DisplayGrid();      
        }
    }

    public void DisplayGrid()
    {
        sql = "Select Id,SmsTitle,EmailSent From SmsEmailMaster where BranchCode=" + ddlBranch.SelectedValue + "";
        Grd.DataSource = oo.GridFill(sql);
        Grd.DataBind();

        if (Grd.Rows.Count > 0)
        {
            for (int i = 0; i < Grd.Rows.Count; i++)
            {
                Label lblid=(Label)Grd.Rows[i].FindControl("lblId");
                RadioButtonList rblad1 = (RadioButtonList)Grd.Rows[i].FindControl("rblad1");
                sql = "Select EmailSent From SmsEmailMaster where Id="+lblid.Text+ " and BranchCode=" + ddlBranch.SelectedValue + "";
                string value1 = oo.ReturnTag(sql, "EmailSent");
                if (value1 == "true")
                {
                    rblad1.Items[0].Selected = true;
                }
                else
                {
                    rblad1.Items[1].Selected = true;
                }
            }
        }
    }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        for (int i = 0; i < Grd.Rows.Count; i++)
        {
            Label lblid = (Label)Grd.Rows[i].FindControl("lblId");
            Label lblTitle = (Label)Grd.Rows[i].FindControl("lblTitle");
            RadioButtonList rblad1 = (RadioButtonList)Grd.Rows[i].FindControl("rblad1");
            string Emailsent = "";
            if (rblad1.Items[0].Selected)
            {
                Emailsent = "true";
            }
            else
            {
                Emailsent = "false";
            }
            
            SqlCommand cmd= new SqlCommand();
            cmd.CommandText = "USP_SmsEmailMasterUpdateProc";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@QueryFor", "E");
            cmd.Parameters.AddWithValue("@Id", lblid.Text);
            cmd.Parameters.AddWithValue("@SmsTitle", lblTitle.Text);
            cmd.Parameters.AddWithValue("@BranchCode", ddlBranch.SelectedValue);
            cmd.Parameters.AddWithValue("@EmailSent", Emailsent);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }
        Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Updated successfully", "S");       
        DisplayGrid();
    }

    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        DisplayGrid();
    }
}