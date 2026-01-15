using System;
using System.Data;
using System.Data.SqlClient;

namespace admin
{
    public partial class Restore : System.Web.UI.Page
    {
        private SqlConnection _con;
        private readonly Campus _oo = new Campus();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["LoginName"] == null)
            {
                Response.Redirect("default.aspx");
            }
            Campus camp = new Campus(); camp.LoadLoader(loader);
        }
        protected void lnkSubmit_Click(object sender, EventArgs e)
        {
            _con = new SqlConnection(@"Data Source=" + _oo.dbGet_connection().DataSource + ";Initial Catalog=Master;User Id=sa; Password=vikash");       
            var cmd = new SqlCommand
            {
                CommandText = "USP_RestoreDataBase",
                CommandType = CommandType.StoredProcedure,
                Connection = _con
            };
            try
            {
                cmd.Parameters.AddWithValue("@dbname", "eAMdb");
                cmd.Parameters.AddWithValue("@dbPath", txtDataBasePath.Text);
                _con.Open();
                cmd.ExecuteNonQuery();
                _con.Close();
                Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Restored successfully.", "S");
                txtDataBasePath.Text = string.Empty;
            }
            catch
            {
                Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Sorry, Database not restored!", "A");
            }
        }
    }
}