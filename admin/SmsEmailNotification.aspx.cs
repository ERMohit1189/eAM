using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class SmsEmailNotification : Page
{
    SqlConnection con = new SqlConnection();
    Campus oo = new Campus();
    string sql = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        con = oo.dbGet_connection();
        Campus camp = new Campus(); camp.LoadLoader(loader);
        if (!IsPostBack)
        {
            DisplayGrid();
        }
    }
    public void DisplayGrid()
    {
        sql = "Select Id,SmsTitle,SmsSent,EmailSent From SmsEmailNotification";
        Grd.DataSource = oo.GridFill(sql);
        Grd.DataBind();
        if (Grd.Rows.Count > 0)
        {
            for (int i = 0; i < Grd.Rows.Count; i++)
            {
                Label lblid = (Label)Grd.Rows[i].FindControl("lblId");
                CheckBox chkSmsSent = (CheckBox)Grd.Rows[i].FindControl("chkSmsSent");
                CheckBox chkEmailSent = (CheckBox)Grd.Rows[i].FindControl("chkEmailSent");
                sql = "Select SmsSent,EmailSent From SmsEmailNotification where Id=" + lblid.Text;
                chkSmsSent.Checked = (oo.ReturnTag(sql, "SmsSent").ToString() == "False" ? false : true);
                chkEmailSent.Checked = (oo.ReturnTag(sql, "EmailSent").ToString() == "False" ? false : true);
            }
        }
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        for (int i = 0; i < Grd.Rows.Count; i++)
        {
            Label lblid = (Label)Grd.Rows[i].FindControl("lblId");
            CheckBox chkSmsSent = (CheckBox)Grd.Rows[i].FindControl("chkSmsSent");
            CheckBox chkEmailSent = (CheckBox)Grd.Rows[i].FindControl("chkEmailSent");
            string Sms = "";
            string Email = "";
            if (chkSmsSent.Checked)
            {
                Sms = "1";
            }
            else
            {
                Sms = "0";
            }
            if (chkEmailSent.Checked)
            {
                Email = "1";
            }
            else
            {
                Email = "0";
            }
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "update SmsEmailNotification set SmsSent=" + Sms + ", EmailSent=" + Email + " where id=" + lblid.Text.Trim() + "";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }
        DisplayGrid();
        Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Updated successfully", "S");
    }
}