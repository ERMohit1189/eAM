using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Web.UI.WebControls;

public partial class SocietyOrTrust : System.Web.UI.Page
{
    private SqlConnection _con;
    private readonly Campus _oo;
    private string _sql, sql = String.Empty;

    public SocietyOrTrust()
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
            LoadData();
        }
    }
    private void LoadData()
    {
        _sql = "select * from tbl_SocietyOrTrust";
        Grd.DataSource = _oo.GridFill(_sql);
        Grd.DataBind();
        txtOrganization.Text = "";
        txtAddress.Text = "";
    }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        var SchoolLogoDirectoryPath = Server.MapPath(string.Format("~/Uploads/SchoolLogo/"));
        string folderPath = "~/Uploads/SchoolLogo/";
        if (!Directory.Exists(SchoolLogoDirectoryPath))
        {
            Directory.CreateDirectory(SchoolLogoDirectoryPath);
        }
        var photoext = "";
        if (TextBox5.HasFile)
        {
            photoext = Path.GetExtension(TextBox5.FileName);
            TextBox5.SaveAs(SchoolLogoDirectoryPath + "SchoolLogo"+ photoext);
        }

        var SchoolAccreditationLogoDirectoryPath = Server.MapPath(string.Format("~/Uploads/SchoolAccreditationLogo/"));
        string folderPath1 = "~/Uploads/SchoolAccreditationLogo/";
        if (!Directory.Exists(SchoolAccreditationLogoDirectoryPath))
        {
            Directory.CreateDirectory(SchoolAccreditationLogoDirectoryPath);
        }
        var photoext1 = "";
        if (TextBox6.HasFile)
        {
            photoext1 = Path.GetExtension(TextBox6.FileName);
            TextBox6.SaveAs(SchoolAccreditationLogoDirectoryPath + "SchoolAccreditationLogo" + photoext1);
        }

        var SchoolSignatureDirectoryPath = Server.MapPath(string.Format("~/Uploads/SchoolSignature/"));
        string folderPath11 = "~/Uploads/SchoolSignature/";
        if (!Directory.Exists(SchoolSignatureDirectoryPath))
        {
            Directory.CreateDirectory(SchoolSignatureDirectoryPath);
        }
        var photoext11 = "";

        if (TextBox7.HasFile)
        {
            photoext11 = Path.GetExtension(TextBox7.FileName);
            TextBox7.SaveAs(SchoolSignatureDirectoryPath + "SchoolSignature"+photoext11);
        }
        using (SqlCommand cmd = new SqlCommand())
        {
            cmd.CommandText = "sp_SocietyOrTrust";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = _con;
            cmd.Parameters.AddWithValue("@Organization", txtOrganization.Text.Trim());
            cmd.Parameters.AddWithValue("@Address", txtAddress.Text.Trim());
            cmd.Parameters.AddWithValue("@RegistrationNumber", TextBox1.Text.Trim());
            cmd.Parameters.AddWithValue("@PhoneNumber", TextBox2.Text.Trim());
            cmd.Parameters.AddWithValue("@Email", TextBox3.Text.Trim());
            cmd.Parameters.AddWithValue("@Website", TextBox4.Text.Trim());
            cmd.Parameters.AddWithValue("@Logo", TextBox5.HasFile ? folderPath+ "SchoolLogo" + photoext : DBNull.Value.ToString(CultureInfo.InvariantCulture));
            cmd.Parameters.AddWithValue("@AccreditationLogo", TextBox6.HasFile ? folderPath1 + "SchoolAccreditationLogo"+photoext1 : DBNull.Value.ToString(CultureInfo.InvariantCulture));
            cmd.Parameters.AddWithValue("@Signature", TextBox7.HasFile ? folderPath11 + "SchoolSignature" + photoext11 : DBNull.Value.ToString(CultureInfo.InvariantCulture));
            try
            {
                _con.Open();
                cmd.ExecuteNonQuery();
                _con.Close();
                Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Submitted successfully.", "S");
                LoadData();
            }
            catch (Exception)
            {
            }
        }
    }
    protected void LinkButton3_Click(object sender, EventArgs e)
    {
        Button8.Focus();
        LinkButton chk = (LinkButton)sender;
        Panel2_ModalPopupExtender.Show();
    }
    
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        _sql = "truncate table tbl_SocietyOrTrust";

        using (SqlCommand cmd = new SqlCommand())
        {
            cmd.CommandText = _sql;
            cmd.CommandType = CommandType.Text;
            cmd.Connection = _con;
            try
            {
                _con.Open();
                cmd.ExecuteNonQuery();
                _con.Close();
                Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Deleted successfully.", "S");
                LoadData();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
    protected void Button8_Click(object sender, EventArgs e)
    {
    }
    public override void Dispose()
    {
        _con.Dispose();
        _oo.Dispose();
    }

    
}