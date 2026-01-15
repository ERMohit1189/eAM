using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System;
using System.IO;

public partial class website_MediaGallery : Page
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
        // sql = "Select  ROW_NUMBER() OVER (ORDER BY ImageId ASC) AS SrNo , ImageId,Title,ImagePath from MediaPhoto";
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "w_spMediaGallery";
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

    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            sql = "select id from MediaPhoto where Title='" + txtImageTitle.Text + "'";
            if (oo.Duplicate(sql))
            {
                // oo.MessageBox("This TitelName alredy in used,choose other...", this.Page);
                Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "This TitelName alredy in used,choose other...", "W");
                oo.ClearControls(this.Page);
                oo.MessageBox("", this.Page);
                txtImageTitle.Text = "";
            }

            else if (txtImageTitle.Text == "")
            {
                // oo.MessageBox("Fill the Title Name", this.Page);
                Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Please Enter TitelName.", "W");
                oo.ClearControls(this.Page);
                oo.MessageBox("", this.Page);
            }
            else
            {
                string base64std = Convert.ToString(hfFile.Value);
                string fileExtention = hdfilefileExtention.Value;

                if (base64std != string.Empty)
                {
                    filePath = @"../Uploads/pics/media_gallery/";

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

                cmd.CommandText = "w_spMediaGallery";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@Action", "insert");
                cmd.Parameters.AddWithValue("@Photo", PhotoPath);
                cmd.Parameters.AddWithValue("@Title", txtImageTitle.Text.Trim().Replace("'", ""));
                cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());

                cmd.Connection = con;
                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

                    bindData();

                    Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Submitted successfully.", "select");
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

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        LinkButton lnk = (LinkButton)sender;
        var currentrow = (RepeaterItem)lnk.NamingContainer;
        Label lblId = (Label)currentrow.FindControl("lblId");
        string ss = lblId.Text;
        lblID.Text = ss;
        //sql = "Select Title,ImagePath from MediaPhoto where ImageId='" + lblId.Text + "'";
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "w_spMediaGallery";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Connection = con;
        cmd.Parameters.AddWithValue("@Action", "select");
        cmd.Parameters.AddWithValue("@id", lblId.Text);
        SqlDataAdapter da = new SqlDataAdapter();
        da.SelectCommand = cmd;
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            txtTitle.Text = dt.Rows[0]["Title"].ToString();
            Image2.ImageUrl =dt.Rows[0]["Photo"].ToString();
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
                string base64std = Convert.ToString(hfFile.Value);
                string fileExtention = hdfilefileExtention.Value;

                if (base64std != string.Empty)
                {
                    filePath = @"../Uploads/pics/media_gallery/";

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

                cmd.CommandText = "w_spMediaGallery";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@Action", "update");
                cmd.Parameters.AddWithValue("@id", lblID.Text);
                cmd.Parameters.AddWithValue("@Photo", PhotoPath);
                cmd.Parameters.AddWithValue("@Title", txtTitle.Text.Trim().Replace("'", ""));
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
      
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "w_spMediaGallery";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Connection = con;

        cmd.Parameters.AddWithValue("@Action", "delete");
        cmd.Parameters.AddWithValue("@id", lblvalue.Text);

        try
        {
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Deleted successfully.", "W");
            oo.ClearControls(this.Page);

            oo.MessageBox("", this.Page);

            bindData();
        }
        catch (SqlException ee) { throw ee; }
        finally { if (con.State == ConnectionState.Open) { con.Close(); } }
    }

}