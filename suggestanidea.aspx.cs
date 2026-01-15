using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.IO;


public partial class SuggestEnIdea : Page
{
    private SqlConnection _con;
    private readonly Campus _oo;
    private string _sql = String.Empty;
    public SuggestEnIdea()
    {
        _con = new SqlConnection();
        _oo = new Campus();
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
        {
            Response.Redirect("default.aspx");
        }
        _con = _oo.dbGet_connection();
        if (!IsPostBack)
        {
            LoadGrid();
        }
    }
    public void LoadGrid()
    {
        _sql = "select * from suggestanidea where LoginName='"+ Session["LoginName"].ToString() + "'";
        Grd.DataSource = _oo.GridFill(_sql);
        Grd.DataBind();
        if (Grd.Rows.Count == 0)
        {
            Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Sorry, No Record found!", "A");
        }
    }
    protected void submit_Click(object sender, EventArgs e)
    {
        if (txtIdea.Text.Trim() == "")
        {
            Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Enter Your Idea!", "A");
        }
        else if (txtIdeaDesc.Text.Trim() == "")
        {
            Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Describe Your Idea!", "A");
        }
        else if (txtIdea.Text.Trim().Length > 100)
        {
            Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Enter maximum 100 characters are allowed!", "A");
        }
        else if (txtIdeaDesc.Text.Trim().Length > 500)
        {
            Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Enter maximum 500 characters are allowed!", "A");
        }
        else if (txtEmail.Text.Trim()=="")
        {
            Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Enter email!", "A");
        }
        else
        {
            string folderPath = Server.MapPath("~/uploads/FeedBack/");
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_suggestanidea";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = _con;
            cmd.Parameters.AddWithValue("@idea ", txtIdea.Text.Trim());
            cmd.Parameters.AddWithValue("@ideaDescription", txtIdeaDesc.Text.Trim());
            cmd.Parameters.AddWithValue("@email", txtEmail.Text.Trim());
            cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
            cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"]);
            cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());
            if (fileAttachment.HasFile)
            {
                cmd.Parameters.AddWithValue("@attachment", fileAttachment.FileName.Trim());
                string extension = Path.GetExtension(fileAttachment.FileName);
                fileAttachment.SaveAs(folderPath + DateTime.Now.ToString("yyyyMMddHHmmss") + extension);
            }
            cmd.Parameters.AddWithValue("@action", "save");
            try
            {
                _con.Open();
                cmd.ExecuteNonQuery();
                _con.Close();
                Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Submitted successfully.", "S");
                _oo.ClearControls(Page);
                LoadGrid();
            }
            catch (Exception)
            {
            }
        }
    }
}