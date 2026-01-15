using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SuperAdmin_Sms_Services : Page
{
    SqlConnection con = new SqlConnection();
    Campus oo = new Campus();
    string sql = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        con = oo.dbGet_connection();
        Campus camp = new Campus(); camp.LoadLoader(loader);  //in cs file

        if (!IsPostBack)
        {
            DisplayGrid();
        }
    }

    public void DisplayGrid()
    {
        sql = "Select Id,SmsTitle,template From SmsEmailMaster where BranchCode=" + Session["BranchCode"] + " order by displayorder";
        Grd.DataSource = oo.GridFill(sql);
        Grd.DataBind();

        if (Grd.Rows.Count > 0)
        {
            for (int i = 0; i < Grd.Rows.Count; i++)
            {
                Label lblid = (Label)Grd.Rows[i].FindControl("lblId");
                RadioButtonList rblad = (RadioButtonList)Grd.Rows[i].FindControl("rblad");
                RadioButtonList rblWhatsApp = (RadioButtonList)Grd.Rows[i].FindControl("rblWhatsApp");
                RadioButtonList rblEmail = (RadioButtonList)Grd.Rows[i].FindControl("rblEmail");
                sql = "Select SmsSent From SmsEmailMaster where Id=" + lblid.Text + " and BranchCode=" + Session["BranchCode"] + "";
                string value = oo.ReturnTag(sql, "SmsSent");
                if (value == "true")
                {
                    rblad.Items[0].Selected = true;
                }
                else
                {
                    rblad.Items[1].Selected = true;
                }
                sql = "Select WhatsAppSent From SmsEmailMaster where Id=" + lblid.Text + " and BranchCode=" + Session["BranchCode"] + "";
                value = oo.ReturnTag(sql, "WhatsAppSent");
                if (value == "true")
                {
                    rblWhatsApp.Items[0].Selected = true;
                }
                else
                {
                    rblWhatsApp.Items[1].Selected = true;
                }
            }
        }

        LinkButton1.Visible = Grd.Rows.Count > 0;
        LinkButton2.Visible = Grd.Rows.Count == 0;
    }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        for (int i = 0; i < Grd.Rows.Count; i++)
        {
            Label lblid = (Label)Grd.Rows[i].FindControl("lblId");
            Label lblTitle = (Label)Grd.Rows[i].FindControl("lblTitle");
            RadioButtonList rblad = (RadioButtonList)Grd.Rows[i].FindControl("rblad");
            RadioButtonList rblWhatsApp = (RadioButtonList)Grd.Rows[i].FindControl("rblWhatsApp");
            TextBox temp = (TextBox)Grd.Rows[i].FindControl("txtTemplate");
            string Smssent = "";

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "USP_SmsEmailMasterUpdateProc";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@QueryFor", "S");
            cmd.Parameters.AddWithValue("@Id", lblid.Text);
            cmd.Parameters.AddWithValue("@SmsTitle", lblTitle.Text);
            cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"]);
            if (rblad.Items[0].Selected)
            {
                Smssent = "true";
            }
            else
            {
                Smssent = "false";
            }
            cmd.Parameters.AddWithValue("@SmsSent", Smssent);
            if (rblWhatsApp.Items[0].Selected)
            {
                Smssent = "true";
            }
            else
            {
                Smssent = "false";
            }
            cmd.Parameters.AddWithValue("@WhatsAppSent", Smssent);
            cmd.Parameters.AddWithValue("@template", temp.Text);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }
        Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Updated successfully", "S");
        DisplayGrid();
    }

    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "USP_CreateSMSDefaultSettings";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Connection = con;
        cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"]);
        con.Open();
        int result = cmd.ExecuteNonQuery();
        con.Close();

        DisplayGrid();
    }
}