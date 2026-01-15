using System.Data;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System;
using System.IO;

public partial class website_StudentTestimonial : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection();
    Campus oo = new Campus();
    DataTable dt = new DataTable();
    string filePath , fileName , filePath1 ,fileName1 = string.Empty;
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
        string frtodate = DateTime.Now.ToString("dd-MMM-yyyy");

        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "w_spStudentTestimonial";
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
        if (txtHeading.Text != "" && txtDescription.Text != "")
        {
            string base64std = Convert.ToString(hfFile.Value);
            string fileExtention = hdfilefileExtention.Value;

            if (base64std != string.Empty)
            {
                filePath = @"../Uploads/pics/photo_gallery/";

                fileName = TextBox1.Text.Trim() + "-" + DateTime.Today.ToShortDateString().Replace("/", "-").ToString() + fileExtention;

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
            cmd.CommandText = "w_spStudentTestimonial";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@Action", "insert");
            cmd.Parameters.AddWithValue("@StudentName", TextBox1.Text.ToString().Trim().Replace("'", "").ToUpper());
            cmd.Parameters.AddWithValue("@Class", TextBox2.Text.ToString().Trim().Replace("'", ""));
            cmd.Parameters.AddWithValue("@Batch", txtHeading.Text.ToString().Trim().Replace("'", ""));
            cmd.Parameters.AddWithValue("@NoticeHeading", "");
            cmd.Parameters.AddWithValue("@NoticeMessage", txtDescription.Text.ToString().Trim().Replace("'", ""));
            cmd.Parameters.AddWithValue("@ImagePath", filePath + fileName);
            cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());

            try
            {

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                bindData();
                Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Submitted successfully.", "S");
                oo.ClearControls(this.Page);
                txtHeading.Text = "";
                txtDescription.Text = "";

                
            }
            catch (SqlException ee) { throw ee; }
            finally { if (con.State == ConnectionState.Open) { con.Close(); } }
        }
        else
        {
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Please Fill News Title and News Text.", "W");
        }
    }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        LinkButton lnk = (LinkButton)sender;
        var currentrow = (RepeaterItem)lnk.NamingContainer;
        Label lblId = (Label)currentrow.FindControl("lblId");
        string ss = lblId.Text;
        lblID.Text = ss;

        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "w_spStudentTestimonial";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Connection = con;
        cmd.Parameters.AddWithValue("@Action", "select");
        cmd.Parameters.AddWithValue("@NoticeId", lblId.Text);
        SqlDataAdapter da = new SqlDataAdapter();
        da.SelectCommand = cmd;
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            TextBox3.Text = dt.Rows[0]["StudentName"].ToString();
            TextBox4.Text = dt.Rows[0]["Class"].ToString() ;
            txtHeadingUpdate.Text = dt.Rows[0]["Batch"].ToString();
            txtDescriptionUPdate.Text = dt.Rows[0]["NoticeMessage"].ToString();
            Image2.ImageUrl = dt.Rows[0]["ImagePath"].ToString();
            Panel1_ModalPopupExtender.Show();
        }
    }

    protected void Button4_Click(object sender, EventArgs e)
    {

    }

    protected void btnupdate_Click(object sender, EventArgs e)
    {
        string base64std1 = Convert.ToString(hfFile1.Value);
        string fileExtention1 = hdfilefileExtention1.Value;

        if (base64std1 != string.Empty)
        {
           filePath1 = @"../Uploads/pics/photo_gallery/";

           fileName1 = TextBox3.Text.Trim() + "-" + DateTime.Today.ToShortDateString().Replace("/", "-").ToString() + fileExtention1;

           using (FileStream fs = new FileStream(Server.MapPath(ResolveClientUrl(filePath1 + fileName1)), FileMode.Create, FileAccess.ReadWrite))
            {
                using (BinaryWriter bw = new BinaryWriter(fs))
                {
                    byte[] data = Convert.FromBase64String(base64std1);
                    bw.Write(data);
                    bw.Close();
                }
            }
        }
        string PhotoPath1 = filePath1 + fileName1;

        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "w_spStudentTestimonial";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Connection = con;

        cmd.Parameters.AddWithValue("@Action", "update");
        cmd.Parameters.AddWithValue("@NoticeId", lblID.Text);

        cmd.Parameters.AddWithValue("@StudentName", TextBox3.Text.ToString().Trim().Replace("'", "").ToUpper());
        cmd.Parameters.AddWithValue("@Class", TextBox4.Text.ToString().Trim().Replace("'", ""));
        cmd.Parameters.AddWithValue("@Batch", txtHeadingUpdate.Text.ToString().Trim().Replace("'", ""));
        cmd.Parameters.AddWithValue("@NoticeHeading", "");
        cmd.Parameters.AddWithValue("@NoticeMessage", txtDescriptionUPdate.Text.ToString().Trim().Replace("'", ""));
        cmd.Parameters.AddWithValue("@ImagePath", filePath1 + fileName1);
        cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());

        try
        {
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            bindData();
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Updated successfully.", "U");
            oo.ClearControls(this.Page);
        }
        catch (SqlException ee) { throw ee; }
        finally { if (con.State == ConnectionState.Open) { con.Close(); } }

      
    }

    protected void lnkDelete_Click(object sender, EventArgs e)
    {
        LinkButton chk = (LinkButton)sender;
        Label lblId = (Label)chk.NamingContainer.FindControl("lblid");
        string ss = lblId.Text;

        lblvalue.Text = ss.ToString();
        Panel2_ModalPopupExtender.Show();
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "w_spStudentTestimonial";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Connection = con;
        cmd.Parameters.AddWithValue("@Action", "delete");
        cmd.Parameters.AddWithValue("@NoticeId", lblvalue.Text);      
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

    protected void Button8_Click(object sender, EventArgs e)
    {

    }
}