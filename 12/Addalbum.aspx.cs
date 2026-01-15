using System.Data;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System;
using System.IO;
using System.Drawing;

public partial class website_AddAlbum : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection();
    Campus oo = new Campus();
    string sql = string.Empty;
    DataTable dt = new DataTable();
    string filePath = "";
    string fileName = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        Session.LCID = 2057;

        con = oo.dbGet_connection();
        Campus camp = new Campus(); camp.LoadLoader(loader);
        if (Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
        {
            Response.Redirect("default.aspx");
        }
        camp.LoadLoader(loader);

        if (!IsPostBack)
        {
            bindData();
        }
    }

    protected void bindData()
    {
        
        Repeater1.DataSource = null;
        Repeater1.DataBind();
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "w_spPhotoAlbum";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Connection = con;
        cmd.Parameters.AddWithValue("@Action", "select");
        SqlDataAdapter da = new SqlDataAdapter();
        da.SelectCommand = cmd;
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            GRID.Visible = true;
            Repeater1.DataSource = dt;
            Repeater1.DataBind();
        }
        else
        {
            GRID.Visible = false;
        }
     
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            sql = "select AlbumId from PhotoAlbum where AlbumName='" + txtImageName.Text + "'";
            if (oo.Duplicate(sql))
            {
               // oo.MessageBox("This TitelName alredy in used,choose other...", this.Page);
                Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "This Titel Name alredy in used,choose other...", "W");
                oo.ClearControls(this.Page);
                oo.MessageBox("", this.Page);
                txtImageName.Text = "";
            }

            else if (txtImageName.Text == "")
            {
               // oo.MessageBox("Fill the Title Name", this.Page);
                Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "This Titel Name alredy in used,choose other...", "W");
                oo.ClearControls(this.Page);
                oo.MessageBox("", this.Page);
            }
            else
            {
                string base64std = Convert.ToString(hfFile.Value);
                string fileExtention = hdfilefileExtention.Value;

                if (base64std != string.Empty)
                {
                    filePath = @"../Uploads/pics/album/";

                    fileName = txtImageName.Text.Trim() + "-" + DateTime.Today.ToShortDateString().Replace("/", "-").ToString() + fileExtention;

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
                //System.IO.FileStream newFile = new System.IO.FileStream(Server.MapPath(filePath + fileName), System.IO.FileMode.Create);
                //bool success = ResizeImageAndUpload(newFile, filePath + (txtImageName.Text.Trim() + "-" + DateTime.Today.ToShortDateString().Replace("/", "-").ToString()), 1000, 1000, fileExtention);

                SqlCommand cmd = new SqlCommand();
              
                cmd.CommandText = "w_spPhotoAlbum";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@Action", "insert");
                cmd.Parameters.AddWithValue("@Photo", PhotoPath);
                cmd.Parameters.AddWithValue("@Title", txtImageName.Text.Trim().Replace("'",""));
                cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());

                cmd.Connection = con;
                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    bindData();
                    Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Submitted successfully.", "S");
                    oo.ClearControls(this.Page);
                    oo.MessageBox("", this.Page);
                }
                catch (SqlException ee) { throw ee; }
              
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public bool ResizeImageAndUpload(System.IO.FileStream newFile, string folderPathAndFilenameNoExtension, double maxHeight, double maxWidth, string fileExt)
    {
        try
        {
            // Declare variable for the conversion
            float ratio;
            // Create variable to hold the image
            System.Drawing.Image thisImage = System.Drawing.Image.FromStream(newFile);
            // Get height and width of current image
            int width = (int)thisImage.Width;
            int height = (int)thisImage.Height;
            // Ratio and conversion for new size
            if (width > maxWidth)
            {
                ratio = (float)width / (float)maxWidth;
                width = (int)(width / ratio);
                height = (int)(height / ratio);
            }
            // Ratio and conversion for new size
            if (height > maxHeight)
            {
                ratio = (float)height / (float)maxHeight;
                height = (int)(height / ratio);
                width = (int)(width / ratio);
            }
            // Create "blank" image for drawing new image
            Bitmap outImage = new Bitmap(width, height);
            Graphics outGraphics = Graphics.FromImage(outImage);
            SolidBrush sb = new SolidBrush(System.Drawing.Color.White);
            // Fill "blank" with new sized image
            outGraphics.FillRectangle(sb, 0, 0, outImage.Width, outImage.Height);
            outGraphics.DrawImage(thisImage, 0, 0, outImage.Width, outImage.Height);
            sb.Dispose();
            outGraphics.Dispose();
            thisImage.Dispose();
            // Save new image as jpg
            outImage.Save(Server.MapPath(folderPathAndFilenameNoExtension + fileExt), System.Drawing.Imaging.ImageFormat.Jpeg);
            outImage.Dispose();
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        LinkButton lnk = (LinkButton)sender;
        var currentrow = (RepeaterItem)lnk.NamingContainer;
        Label lblId = (Label)currentrow.FindControl("lblId");
        string ss = lblId.Text;
        lblID.Text = ss;
       // sql = "Select AlbumName,ImagePath from PhotoAlbum where AlbumId='" + lblId.Text + "'";
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "w_spPhotoAlbum";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Connection = con;
        cmd.Parameters.AddWithValue("@Action", "select");
        cmd.Parameters.AddWithValue("@id", lblId.Text);
        SqlDataAdapter da = new SqlDataAdapter();
        da.SelectCommand = cmd;
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            txtEditAlbumName.Text = dt.Rows[0]["Title"].ToString() ;
            Image2.ImageUrl = dt.Rows[0]["Photo"].ToString();
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
                string base64std = Convert.ToString(hfFile.Value);
                string fileExtention = hdfilefileExtention.Value;

                if (base64std != string.Empty)
                {
                    filePath = @"../Uploads/pics/album/";

                    fileName = txtEditAlbumName.Text.Trim() + "-" + DateTime.Today.ToShortDateString().Replace("/", "-").ToString() + fileExtention;
      
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
              
                cmd.CommandText = "w_spPhotoAlbum";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@Action", "update");
                cmd.Parameters.AddWithValue("@id", lblID.Text);
                cmd.Parameters.AddWithValue("@Photo", PhotoPath);
                cmd.Parameters.AddWithValue("@Title", txtEditAlbumName.Text);
                cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());

                cmd.Connection = con;
                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    bindData();

                    Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Updated successfully.", "U");
                    oo.ClearControls(this.Page);
                    oo.MessageBox("", this.Page);
                }
                catch (SqlException ee) { throw ee; }
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
        cmd.CommandText = "w_spPhotoAlbum";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Connection = con;

        cmd.Parameters.AddWithValue("@Action", "delete");
        cmd.Parameters.AddWithValue("@id", lblvalue.Text);

        try
        {
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            bindData();
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Deleted successfully.", "W");
        }
        catch (SqlException ee) { throw ee; }
        finally { if (con.State == ConnectionState.Open) { con.Close(); } }
    }

    public override void Dispose()
    {
        dt.Dispose();
    }
}