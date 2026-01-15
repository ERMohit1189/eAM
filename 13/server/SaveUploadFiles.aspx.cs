using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Web;

public partial class SaveUploadFiles : System.Web.UI.Page
{
    private SqlConnection _con = new SqlConnection();
    private readonly Campus _oo = new Campus();
    private string _sql, _sql1 = String.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        _con = _oo.dbGet_connection();
        string ExamID = Request.Form["ExamID"].ToString().Trim();
        string SectionId = Request.Form["SectionId"].ToString().Trim();
        HttpFileCollection files = Request.Files;
        string filename = ""; string MainFile = "";
        string path = System.Web.HttpContext.Current.Server.MapPath("~/uploads/UploadTestDocs/");

        for (int i = 0; i < files.Count; i++)
        {
            string extension = Path.GetExtension(files[i].FileName);
            MainFile = (ExamID + "_" + SectionId + "_" + Session["LoginName"].ToString().Replace("/", "")+"_"+ Session["BranchCode"].ToString()+"_"+(i+1) + extension);
            if (i != (files.Count - 1))
            {
                filename = filename+(MainFile + "##");
            }
            else
            {
                filename = filename + MainFile;
            }
            files[i].SaveAs(path+MainFile);
        }
        using (SqlCommand cmd = new SqlCommand())
        {
            cmd.CommandText = "OT_ExamAnswerResultProc";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@SrNO", Session["LoginName"].ToString());
            cmd.Parameters.AddWithValue("@SectionId", SectionId);
            cmd.Parameters.AddWithValue("@ExamID", ExamID.Trim());
            cmd.Parameters.AddWithValue("@UploadFiles", filename.Trim());
            cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
            cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"].ToString());
            cmd.Parameters.AddWithValue("@Loginname", Session["LoginName"].ToString());
            cmd.Parameters.AddWithValue("@Action", "saveFiles");
            cmd.Connection = _con;
            try
            {
                _con.Open();
                cmd.ExecuteNonQuery();
                _con.Close();
            }
            catch (Exception ex)
            {
            }
        }
       
    }

}