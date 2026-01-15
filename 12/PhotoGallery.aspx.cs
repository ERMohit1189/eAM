using System.Data;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System;
using System.IO;

public partial class website_PhotoGallery : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection();
    Campus oo = new Campus();
    string sql = string.Empty;
    DataTable dt = new DataTable();
    string filePath , filePath1 = string.Empty;
    string fileName , fileName1=string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        Session.LCID = 2057;

        con = oo.dbGet_connection();
        Campus camp = new Campus(); camp.LoadLoader(loader);
        if (Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
        {
            Response.Redirect("default.aspx");
        }
        Campus camp1 = new Campus(); camp1.LoadLoader(loader);

        if (!IsPostBack)
        {
            loadAlbum();
            loadImages();
        }
    }

    public void loadAlbum()
    {
        sql = "Select Id, Title from w_PhotoAlbum";
        oo.FillDropDown_withValue(sql, drpAlbumName, "Title", "Id");
        drpAlbumName.Items.Insert(0, new ListItem("<--Select-->", "0"));
        oo.FillDropDown_withValue(sql, drpImageName, "Title", "Id");
    }

    public void loadImages()
    {
        Repeater1.DataSource = null;
        Repeater1.DataBind();
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "w_spPhotoGallery";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Connection = con;
        cmd.Parameters.AddWithValue("@Action", "select");
        if (drpAlbumName.SelectedIndex != 0)
        {
            cmd.Parameters.AddWithValue("@AlbumId", drpAlbumName.SelectedValue);
        }
        SqlDataAdapter da = new SqlDataAdapter();
        da.SelectCommand = cmd;
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            divList.Visible = true;
            Repeater1.DataSource = dt;
            Repeater1.DataBind();
        }
        else
        {
            divList.Visible = false;
        }
    }

    protected void drpAlbumName_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadImages();
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            if (drpAlbumName.SelectedIndex != 0)
            {

                string base64std = Convert.ToString(hfFile.Value);
                string fileExtention = hdfilefileExtention.Value;

                if (base64std != string.Empty)
                {
                    filePath = @"../Uploads/pics/photo_gallery/";

                    fileName = txtImageTitle.Text.Trim() + "-" + DateTime.Today.ToShortDateString().Replace("/", "-").ToString() + fileExtention;

                    using (FileStream fs = new FileStream(Server.MapPath(ResolveClientUrl(filePath + fileName)), FileMode.Create, FileAccess.ReadWrite))
                    {
                        using (BinaryWriter bw = new BinaryWriter(fs))
                        {
                            byte[] data = Convert.FromBase64String(base64std);
                            bw.Write(data);
                            bw.Close();
                        }
                    }

                }

                string PhotoPath = filePath + fileName;

                SqlCommand cmd = new SqlCommand();

                cmd.CommandText = "w_spPhotoGallery";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@Action", "insert");
                cmd.Parameters.AddWithValue("@Photo", PhotoPath);
                cmd.Parameters.AddWithValue("@AlbumId", drpAlbumName.SelectedValue);
                if (txtImageTitle.Text!="")
                {
                    cmd.Parameters.AddWithValue("@Title", txtImageTitle.Text.Trim().Replace("'", ""));
                }
                cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());

                cmd.Connection = con;
                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    loadImages();
                    Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Submitted successfully.", "S");
                    oo.ClearControls(this.Page);
                    oo.MessageBox("", this.Page);
                }
                catch (SqlException ee) { throw ee; }

            }
            else
            {
                Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Select Album In List", "W");
                oo.ClearControls(this.Page);
                oo.MessageBox("", this.Page);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        LinkButton lnk = (LinkButton)sender;
        var currentrow = (RepeaterItem)lnk.NamingContainer;
        Label lblId = (Label)currentrow.FindControl("lblId");
        string ss = lblId.Text;
        lblID.Text = ss;
       // sql = "Select AlbumName,Title,ImagePath from MainPhoto where ImageId='" + lblId.Text + "'";
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "w_spPhotoGallery";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Connection = con;
        cmd.Parameters.AddWithValue("@Action", "select");
        cmd.Parameters.AddWithValue("@id", lblId.Text);
        SqlDataAdapter da = new SqlDataAdapter();
        da.SelectCommand = cmd;
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            drpImageName.Text = dt.Rows[0]["AlbumId"].ToString() ;
            txtTitle.Text = dt.Rows[0]["Title"].ToString();
            Image2.ImageUrl = dt.Rows[0]["Photo"].ToString() ;
            Panel1_ModalPopupExtender.Show();
        }
    }

    protected void lnkDelete_Click(object sender, EventArgs e)
    {
        LinkButton chk = (LinkButton)sender;
        Label lblId = (Label)chk.NamingContainer.FindControl("lblid");
        string ss = lblId.Text;

        lblvalue.Text = ss.ToString();
        Panel2_ModalPopupExtender.Show();
    }

    protected void btnupdate_Click(object sender, EventArgs e)
    {
        try
        {
            string base64std1 = Convert.ToString(hfFile1.Value);
            string fileExtention1 = hdfilefileExtention1.Value;

            if (base64std1 != string.Empty)
            {
                filePath1 = @"../Uploads/pics/photo_gallery/";

                fileName1 = txtTitle.Text.Trim() + "-" + DateTime.Today.ToShortDateString().Replace("/", "-").ToString() + fileExtention1;

                using (FileStream fs1 = new FileStream(Server.MapPath(filePath1 + fileName1), FileMode.Create, FileAccess.ReadWrite))
                {
                    using (BinaryWriter bw = new BinaryWriter(fs1))
                    {
                        byte[] data = Convert.FromBase64String(base64std1);
                        bw.Write(data);
                        bw.Close();
                    }
                }
            }

            string PhotoPath1 = filePath1 + fileName1;

            SqlCommand cmd = new SqlCommand();

            cmd.CommandText = "w_spPhotoGallery";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@Action", "update");
            cmd.Parameters.AddWithValue("@id", lblID.Text);
            cmd.Parameters.AddWithValue("@Photo", PhotoPath1);
            cmd.Parameters.AddWithValue("@AlbumId", drpImageName.SelectedValue);
            if (txtTitle.Text != "")
            {
                cmd.Parameters.AddWithValue("@Title", txtTitle.Text.Trim().Replace("'", ""));
            }
            cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());
            cmd.Connection = con;
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                loadImages();
                Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Updated successfully.", "U");
                oo.ClearControls(this.Page);
                oo.MessageBox("", this.Page);
            }
            catch (SqlException ee) { throw ee; }
        }
        catch (Exception ex) { throw ex; }
    }

    protected void Button4_Click(object sender, EventArgs e)
    {

    }

    protected void Button8_Click(object sender, EventArgs e)
    {

    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "w_spPhotoGallery";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Connection = con;

        cmd.Parameters.AddWithValue("@Action", "delete");
        cmd.Parameters.AddWithValue("@id", lblvalue.Text);

        try
        {
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            loadImages();
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Deleted successfully.", "W");
        }
        catch (SqlException ee) { throw ee; }
        finally { if (con.State == ConnectionState.Open) { con.Close(); } }
    }

}