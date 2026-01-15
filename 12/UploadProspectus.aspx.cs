using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System;
using System.Data;
using System.IO;

public partial class website_UploadProspectus : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection();
    Campus oo = new Campus();
    string sql = string.Empty;
    string filePath, fileName = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
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
        Repeater1.DataSource = null;
        Repeater1.DataBind();
        sql = "Select ROW_NUMBER() OVER (ORDER BY Id ASC) AS SrNo , Id,title,ISNULL(DocNamePath,DocNamePath) as DocName from w_Prospectus order by Id desc ";
        var ds = oo.GridFill(sql);
        if (ds != null)
        {
            divList.Visible = true;
            if (ds.Tables[0].Rows.Count > 0)
            {
                divList.Visible = true;
                Repeater1.DataSource = ds;
                Repeater1.DataBind();
            }
            else
            {
                divList.Visible = false;
            }
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
            string base64std = Convert.ToString(hfFile.Value);
            string fileExtention = hdfilefileExtention.Value;
            if (fileExtention == ".pdf" || fileExtention == ".PDF")
            {
                if (base64std != string.Empty)
                {
                    filePath = @"../Uploads/docs/pdf/";
                    Random rnd = new Random();
                    int month = rnd.Next(1, 10000);
                    fileName = "UploadProspectus - " + month + " - " + DateTime.Today.ToShortDateString().Replace("/", "-").ToString() + fileExtention;
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
            }
            else
            {
                Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "File should be .pdf or .PDF Format", "W");
            }

            string PhotoPath = filePath + fileName;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "w_spProspectus";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@Action", "insert");
            cmd.Parameters.AddWithValue("@title", txtImageName.Text);
            cmd.Parameters.AddWithValue("@DocNamePath", PhotoPath);
            cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                bindData();
                Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "File Upload successfully.", "S");
                oo.ClearControls(this.Page);
            }
            catch
            {
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
        cmd.CommandText = "w_spProspectus";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Connection = con;
        cmd.Parameters.AddWithValue("@Action", "delete");
        cmd.Parameters.AddWithValue("@Id", lblvalue.Text);
        try
        {
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            bindData();
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Deleted successfully.", "W");
            oo.ClearControls(this.Page);
        }
        catch (SqlException ee) { throw ee; }
        finally { if (con.State == ConnectionState.Open) { con.Close(); } }
    }

}