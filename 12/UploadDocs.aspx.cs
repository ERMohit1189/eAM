using System.Data;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System;
using System.IO;


public partial class website_AddAlbum : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection();
    Campus oo = new Campus();
    string sql = string.Empty;
    DataTable dt = new DataTable();
    string filePath , fileName = string.Empty;
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
            bindData();
        }
    }

    protected void bindData()
    {
      //  sql = "Select ROW_NUMBER() OVER (ORDER BY Id ASC) AS SrNo , Id,Titel,ISNULL(DocName,Path) as DocName from Download order by Id desc ";
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "USP_Download";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Connection = con;
        cmd.Parameters.AddWithValue("@QueryFor", "S");
        SqlDataAdapter da = new SqlDataAdapter();
        da.SelectCommand = cmd;
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            Repeater1.DataSource = dt;
            Repeater1.DataBind();
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            sql = "select id from Download where Titel='" + txtImageName.Text + "'";
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
                Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "This TitelName alredy in used,choose other...", "W");
                oo.ClearControls(this.Page);
                oo.MessageBox("", this.Page);
            }
            else
            {
                string base64std = Convert.ToString(hfFile.Value);
                string fileExtention = hdfilefileExtention.Value;

                if (base64std != string.Empty)
                {
                    filePath = @"../Uploads/docs/pdf/";
                    Random rd = new Random();
                    int ss = rd.Next(1,1099);

                    fileName = ss + "-" + DateTime.Today.ToShortDateString().Replace("/", "-").ToString() + fileExtention;
      
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

                cmd.CommandText = "USP_Download";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@QueryFor", "I");
                cmd.Parameters.AddWithValue("@Path", PhotoPath);
                cmd.Parameters.AddWithValue("@Titel", txtImageName.Text.Trim().Replace("'", ""));
                cmd.Parameters.AddWithValue("@DocName", fileName);
                 cmd.Parameters.AddWithValue("@RecordDate", DateTime.Now.ToString());
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

   
    protected void lnkDelete_Click(object sender, EventArgs e)
    {
        LinkButton chk = (LinkButton)sender;
        Label lblId = (Label)chk.NamingContainer.FindControl("lblid");
        string ss = lblId.Text;

        lblvalue.Text = ss.ToString();
        Panel2_ModalPopupExtender.Show();
    }

    protected void Button8_Click(object sender, EventArgs e)
    {
        
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "USP_Download";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Connection = con;

        cmd.Parameters.AddWithValue("@QueryFor", "D");
        cmd.Parameters.AddWithValue("@Id", lblvalue.Text);

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