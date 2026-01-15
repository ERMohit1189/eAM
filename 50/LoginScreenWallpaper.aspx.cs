using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

public partial class admin_LoginScreenWallpaper : Page
{
    SqlConnection con = new SqlConnection();
    Campus oo = new Campus();
    string sql = string.Empty;
    DataTable dt = new DataTable();
    string filePath = "";
    string fileName, colorname, balckwhite, pathname = String.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        con = oo.dbGet_connection();
        Campus camp = new Campus(); camp.LoadLoader(loader);

        camp.LoadLoader(loader);
        if (!IsPostBack)
        {
            bindwallpaper();
        }
    }
    protected void bindwallpaper()
    {
        var cmd = new SqlCommand
        {
            CommandText = "USP_Login_Wallpaper",
            CommandType = CommandType.StoredProcedure,
            Connection = con
        };
        cmd.Parameters.AddWithValue("@QueryFor", "S");
        using (var da = new SqlDataAdapter())
        {
            da.SelectCommand = cmd;
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                var imgshow = dt.Rows[0]["Wallpaper_Path"].ToString();
                if (imgshow != "" && imgshow != "no")
                {
                    Avatar.Visible = true;
                    RadioButtonList1.SelectedValue = "1";
                    Avatar.ImageUrl = dt.Rows[0]["Wallpaper_Path"].ToString();
                    div001.Visible = true;
                    div002.Visible = true;
                    div003.Visible = true;
                }
                else if (imgshow == "no")
                {
                    RadioButtonList1.SelectedValue = "0";
                    Avatar.Visible = false;
                    div001.Visible = false;
                    div002.Visible = true;
                    div003.Visible = false;
                }
                else
                {
                    RadioButtonList1.SelectedValue = "0";
                    div001.Visible = false;
                    div002.Visible = false;
                    div003.Visible = false;
                }
            }
            else
            {
                Avatar.Visible = true;
                Avatar.ImageUrl = "../Uploads/LoginWallpaper/eAM_Default_bg.png";
            }
        }
    }


    protected void Button1_Click(object sender, EventArgs e)
    {
        var base64std = Convert.ToString(hdStPhoto.Value);
        var sdaa = new SqlDataAdapter("select Black_White from Login_Wallpager", con);
        var cmd = new SqlCommand();
        var photoPath = "";
        if (base64std != "" && RadioButtonList1.SelectedValue == "1")
        {
            var fileExtention = ".jpg";// hdfilefileExtention.Value;
            if (base64std != string.Empty)
            {
                filePath = @"../Uploads/LoginWallpaper/";
                var rnd = new Random();
                var idno = rnd.Next(1, 10000);
                fileName = idno + "-" + DateTime.Today.ToShortDateString().Replace("/", "-") + fileExtention;

                using (FileStream fs = new FileStream(Server.MapPath(ResolveClientUrl(filePath + fileName)), FileMode.Create, FileAccess.ReadWrite))
                {
                    using (BinaryWriter bw = new BinaryWriter(fs))
                    {
                        var data = Convert.FromBase64String(base64std);
                        bw.Write(data);
                        bw.Close();
                    }
                }
            }
            photoPath = filePath + fileName;
        }
        if ((base64std == "" || base64std == string.Empty) && RadioButtonList1.SelectedValue == "1")
        {
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Select Wallpaper", "W");
            return;
        }
        var blackwhitename = String.Empty;
        var dtt = new DataTable();
        sdaa.Fill(dtt);
        if (dtt.Rows.Count > 0)
        {
            if (dtt.Rows[0]["Black_White"].ToString().Trim() != "")
            {
                blackwhitename = dtt.Rows[0]["Black_White"].ToString();
            }
        }
        if (blackwhitename != "")
        {
            blackwhitename = ".backbg::after { content: ''; background: url(" + photoPath + ") no-repeat center center fixed !important; background-size:100%; top: 0; left: 0; bottom: 0; right: 0; position: absolute; z-index: -1; -webkit-filter: grayscale(1); filter: grayscale(1); display: block !important; -webkit-background-size: cover; -moz-background-size: cover; -o-background-size: cover; background-size: cover; }";
        }
        else
        {
            blackwhitename = "";
        }
        cmd.CommandText = "USP_Login_Wallpaper";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Connection = con;
        cmd.Parameters.AddWithValue("@QueryFor", "SU");
        cmd.Parameters.AddWithValue("@Color", null);
        cmd.Parameters.AddWithValue("@Black_White", blackwhitename);
        if (RadioButtonList1.SelectedValue == "1")
        {
            cmd.Parameters.AddWithValue("@Wallpaper_Path", photoPath);
        }
        else
        {
            cmd.Parameters.AddWithValue("@Wallpaper_Path", "no");
        }
        cmd.Connection = con;
        try
        {
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            bindwallpaper();
            Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Submitted successfully.", "S");
        }
        catch (SqlException ee) { throw new Exception("some reason to rethrow", ee); }

    }



    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        try
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "USP_Login_Wallpaper";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@QueryFor", "DD");
            cmd.Connection = con;
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                bindwallpaper();
                Campus camp = new Campus(); camp.msgbox(this.Page, msgdefault, "Default wallpaper set successfully.", "S");
            }
            catch (SqlException ee) { throw ee; }

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (RadioButtonList1.SelectedValue == "0")
        {
            Avatar.Visible = false;
            div001.Visible = false;
            div002.Visible = true;
            div003.Visible = false;
        }
        else
        {
            Avatar.Visible = true;
            div001.Visible = true;
            div002.Visible = true;
            div003.Visible = true;
        }
    }
}