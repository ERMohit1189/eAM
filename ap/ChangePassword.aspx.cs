using System;
using System.Data;
using System.Data.SqlClient;

public partial class admin_ChangePassword : System.Web.UI.Page
{
    SqlConnection _con;
    readonly Campus _oo;
    string _sql, _oldPass  = "";
    public admin_ChangePassword()
    {
        _con = new SqlConnection();
        _oo = new Campus();
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        _con = _oo.dbGet_connection();
        Campus camp = new Campus(); camp.LoadLoader(loader);
        if (!IsPostBack)
        {
        }
        LblMatch.Text = "";
    }
    
    protected void Button1_OnClick(object sender, EventArgs e)
    {
        string mobile = "";
        mobile = Session["UserMobile"].ToString();
        
        string sql = "select PASSWORD from REGISTRATION_ADMISSION where PASSWORD=" + "'" + txtOldPanelss.Text.Trim() + "' and MOBILE='" + mobile + "'";
        if (!_oo.Duplicate(sql))
        {
            LblMatch.Text = "Incorrect old password!";
        }
        else if (txtNewPanel.Text.Trim()!= TextConfNewPassw.Text.Trim())
        {
            LblMatch.Text = "Password not matched!";
        }
        else
        {
            using (var cmd = new SqlCommand())
            {
                if (txtNewPanel.Text != "" && TextConfNewPassw.Text != "")
                {
                    _sql = "update REGISTRATION_ADMISSION set PASSWORD='" + txtNewPanel.Text.Trim() + "'   where  PASSWORD=" + "'" + txtOldPanelss.Text.Trim() + "' and MOBILE='" + mobile + "' ";
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = _sql;
                    cmd.Connection = _con;
                    try
                    {
                        _con.Open();
                        cmd.ExecuteNonQuery();
                        _con.Close();
                        Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Updated successfully.", "S");
                        Clear();
                    }
                    catch (SqlException ee) { LblMatch.Text = ee.Message; }
                }
                else
                {
                    Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Can't leave blank!", "W");
                }
            }
        }
    }
    protected void Button2_OnClick(object sender, EventArgs e)
    {
        Clear();
    }
    protected void Clear()
    {
        LblMatch.Text = "";
        txtNewPanel.Text = "";
        TextConfNewPassw.Text = "";
        txtOldPanelss.Text = "";
        txtOldPanelss.Focus();
    }
    public override void Dispose()
    {
        _con.Dispose();
        _oo.Dispose();
    }
}