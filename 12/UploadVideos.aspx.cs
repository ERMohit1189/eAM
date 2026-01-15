using System.Data;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System;

public partial class website_UploadVideos : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection();
    Campus oo = new Campus();
    string sql = string.Empty;
    DataTable dt = new DataTable();
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
        try
        {
            Repeater1.DataSource = null;
            Repeater1.DataBind();
            string frtodate = DateTime.Now.ToString("dd-MMM-yyyy");
            TextBox1.Text = frtodate;

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "w_spVideoGallery";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@Action", "select");
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
        catch (Exception)
        {
            //throw ex;
        }
        finally
        {
            if (con.State == ConnectionState.Open) { con.Close(); }
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        sql = "select Id,YouTubeFrame,Title,CurDate  from w_VideoGallery  ";
        sql = sql + " where Title='" + txtImageTitle.Text + "'";

        if (oo.Duplicate(sql) == false)
        {
            try
            {
                string IframeVideo = "";
                string[] xx = txtYouTubeIFrame.Text.Trim().Split('/');
                string name = xx[xx.Length - 1];
                IframeVideo = "<iframe width='100%' height='170' src='https://www.youtube.com/embed/"+ name + "' frameborder='0' allowfullscreen></iframe>";

                if (txtImageTitle.Text == "")
                {
                    Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Fill the Title Name", "W");
                    oo.ClearControls(this.Page);
                    oo.MessageBox("", this.Page);
                }
                else
                {
                    string dd = "";
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = "w_spVideoGallery";
                    cmd.CommandType = CommandType.StoredProcedure;
                    dd = TextBox1.Text;
                    cmd.Parameters.AddWithValue("@Action", "insert");
                    cmd.Parameters.AddWithValue("@YouTubeLink", txtYouTubeIFrame.Text.Trim());
                    cmd.Parameters.AddWithValue("@Title", txtImageTitle.Text.Trim().Replace("'",""));
                    cmd.Parameters.AddWithValue("@CurDate", dd);
                    cmd.Parameters.AddWithValue("@YouTubeFrame", IframeVideo);
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
                    catch (Exception ex) {
                        //throw ex;
                    }
                }
            }
            catch (Exception) {
                //throw ex;
            }
        }

        else
        {
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Already Uploaded!", "W");
            oo.ClearControls(this.Page);
            oo.MessageBox("", this.Page);
         //   oo.MessageBox("Already Uploaded!", this.Page);
        }
    }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        LinkButton lnk = (LinkButton)sender;
        var currentrow = (RepeaterItem)lnk.NamingContainer;
        Label lblId = (Label)currentrow.FindControl("lblId");
        string ss = lblId.Text;
        lblID.Text = ss;
       // sql = "Select Id ,YouTubeFrame,Title from videogallery where Id='" + lblId.Text + "'";
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "w_spVideoGallery";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Connection = con;
        cmd.Parameters.AddWithValue("@Action", "select");
        cmd.Parameters.AddWithValue("@id", lblId.Text);
        SqlDataAdapter da = new SqlDataAdapter();
        da.SelectCommand = cmd;
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            txtEditAlbumName.Text = dt.Rows[0]["Title"].ToString();
            videolink.Text = dt.Rows[0]["YouTubeLink"].ToString();
            string urlshaow =dt.Rows[0]["YouTubeFrame"].ToString();
            ltrshow.Text = urlshaow;
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
        string IframeVideo1 = "";
        string[] xx = videolink.Text.Trim().Split('/');
        string name = xx[xx.Length - 1];
        IframeVideo1 = "<iframe width='100%' height='170' src='https://www.youtube.com/embed/" + name + "' frameborder='0' allowfullscreen></iframe>";

        
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = "w_spVideoGallery";
                    cmd.CommandType = CommandType.StoredProcedure;
                  
                    cmd.Parameters.AddWithValue("@Action", "update");
                    cmd.Parameters.AddWithValue("@Id", lblID.Text);
                    cmd.Parameters.AddWithValue("@YouTubeLink", videolink.Text.Trim());
                    cmd.Parameters.AddWithValue("@Title", txtEditAlbumName.Text.Trim());
                    cmd.Parameters.AddWithValue("@YouTubeFrame", IframeVideo1);
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
                    catch (Exception) {
            //throw ex;
        }
                

    }


    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "w_spVideoGallery";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@Action", "delete");
            cmd.Parameters.AddWithValue("@id", lblvalue.Text);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Deleted successfully.", "W");
            oo.ClearControls(this.Page);
            oo.MessageBox("", this.Page);
            bindData();
        }
        catch (SqlException) {
            //throw ee;
        }
        finally { if (con.State == ConnectionState.Open) { con.Close(); } }
    }
}